using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NutriFacts.Controllers;
using NutriFacts.Service.Parser.OpenFood;
using NutriFacts.Tests.Mocks;
using Xunit;

public class ProductControllerTests
{
	[Fact]
	public async Task GetByBarcode_WhenProductExists_ReturnsOkWithProduct()
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

		var controller = CreateController(db);

		var result = await controller.GetByBarcode("1234567890123");

		var ok = Assert.IsType<OkObjectResult>(result);
		var product = Assert.IsAssignableFrom<IProduct>(ok.Value);
		Assert.Equal("1234567890123", product.Id);
		Assert.Equal("Oats", product.Name);
	}

	[Fact]
	public async Task Search_WhenProductsExist_ReturnsOkWithProducts()
	{
		using var db = CreateDbContext();
		db.Products.AddRange(
			new Product
			{
				Id = "p-1",
				Name = "Chocolate Oats",
				Brand = "Brand A",
				NutritionFacts = new NutritionFacts { Carbohydrates = 20, Fat = 4, Proteins = 7, Calories = 150 }
			},
			new Product
			{
				Id = "p-2",
				Name = "Rice",
				Brand = "Brand B",
				NutritionFacts = new NutritionFacts { Carbohydrates = 30, Fat = 1, Proteins = 3, Calories = 140 }
			}
		);
		await db.SaveChangesAsync();

		var controller = CreateController(db);

		var result = await controller.Search("Chocolate");

		var ok = Assert.IsType<OkObjectResult>(result);
		var products = Assert.IsAssignableFrom<IEnumerable<IProduct>>(ok.Value);
		Assert.Single(products);
		Assert.Contains(products, p => p.Name.Contains("Chocolate", StringComparison.OrdinalIgnoreCase));
	}

	private static ProductController CreateController(AppDbContext db)
	{
		var mockHandler = new MockHttpMessageHandler();
		var httpClient = new HttpClient(mockHandler);
		var parser = new OpenFoodParser();
		var searchEngine = new OpenFoodSearchEngineService(httpClient, parser);
		var repository = new ProductRepository(NullLogger<ProductRepository>.Instance, db, searchEngine);
		var service = new ProductApplicationService(repository, NullLogger<ProductApplicationService>.Instance);

		return new ProductController(service);
	}

	private static AppDbContext CreateDbContext()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		return new AppDbContext(options);
	}
}
