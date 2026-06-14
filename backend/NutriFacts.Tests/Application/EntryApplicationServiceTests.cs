using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NutriFacts.Domain.Exceptions;
using NutriFacts.Service;
using NutriFacts.Service.Parser.OpenFood;
using System.Net;
using System.Text;
public class EntryApplicationServiceTests
{

	[Fact]
	public async Task AddAsync_WhenProductExists_CreatesEntryForUser()
	{
		using var db = CreateDbContext();
		db.Products.Add(new Product
		{
			Id = "1234567890123",
			Name = "Oats",
			Brand = "Brand",
			NutritionFacts = new NutritionFacts
			{
				Carbohydrates = 10,
				Fat = 5,
				Proteins = 3,
				Calories = 100
			}
		});
		await db.SaveChangesAsync();

		var service = CreateEntryApplicationService(db, "{}");

		await service.AddAsync("1234567890123", "user-1", 2);

		var entries = await db.ProductEntries.AsNoTracking().ToListAsync();
		Assert.Single(entries);
		Assert.Equal("user-1", entries[0].UserId);
		Assert.Equal("1234567890123", entries[0].ProductId);
		Assert.Equal(2, entries[0].Quantity);
	}

	[Fact]
	public async Task AddAsync_WhenProductDoesNotExist_ThrowsException()
	{
		using var db = CreateDbContext();
		var service = CreateEntryApplicationService(db, "{}");

		var ex = await Assert.ThrowsAsync<ProductNotFoundException>(() => service.AddAsync("0000000000000", "user-1", 1));

		Assert.Equal("Product not found for barcode '0000000000000'.", ex.Message);
		Assert.Empty(await db.ProductEntries.ToListAsync());
	}

	[Fact]
	public async Task GetTodayFactsAsync_ReturnsAggregatedNutritionFacts()
	{
		using var db = CreateDbContext();

		var productA = new Product
		{
			Id = "p-1",
			Name = "Food A",
			Brand = "A",
			NutritionFacts = new NutritionFacts
			{
				Carbohydrates = 10,
				Fat = 2,
				Proteins = 4,
				Calories = 100
			}
		};

		var productB = new Product
		{
			Id = "p-2",
			Name = "Food B",
			Brand = "B",
			NutritionFacts = new NutritionFacts
			{
				Carbohydrates = 20,
				Fat = 5,
				Proteins = 8,
				Calories = 200
			}
		};

		db.Products.AddRange(productA, productB);
		db.ProductEntries.AddRange(
			new ProductEntry(productA, 1) { UserId = "user-1", ProductId = productA.Id },
			new ProductEntry(productB, 2) { UserId = "user-1", ProductId = productB.Id },
			new ProductEntry(productA, 3) { UserId = "user-2", ProductId = productA.Id }
		);
		await db.SaveChangesAsync();

		var service = CreateEntryApplicationService(db, "{}");

		var facts = await service.GetTodayFactsAsync("user-1");

		Assert.Equal(50, facts.Carbohydrates);
		Assert.Equal(12, facts.Fat);
		Assert.Equal(20, facts.Proteins);
		Assert.Equal(500, facts.Calories);
	}

	private static AppDbContext CreateDbContext()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		return new AppDbContext(options);
	}

	private static EntryApplicationService CreateEntryApplicationService(AppDbContext db, string responseBody)
	{
		var handler = new StubHttpMessageHandler(responseBody);
		var httpClient = new HttpClient(handler);
		var parser = new OpenFoodParser();
		var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);
		var productRepository = new ProductRepository(NullLogger<ProductRepository>.Instance, db, searchEngine);
		var productApplicationService = new ProductApplicationService(productRepository, NullLogger<ProductApplicationService>.Instance);
		var entryRepository = new EntryRepository(db);

		return new EntryApplicationService(entryRepository, productApplicationService);
	}

	private sealed class StubHttpMessageHandler(string responseBody) : HttpMessageHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(responseBody, Encoding.UTF8, "application/json")
			};

			return Task.FromResult(response);
		}
	}

}
