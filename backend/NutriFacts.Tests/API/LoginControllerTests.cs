using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NutriFacts.Auth;
using NutriFacts.Controllers;
using NutriFacts.DTO.Auth;
using Xunit;

public class LoginControllerTests
{
	private const string TestJwtKey = "test-secret-key-1234567890-abcdefghijklmnopqrstuvwxyz";

	[Fact]
	public async Task Login_ValidCredentials_ReturnsOkWithToken()
	{
		var user = new AppUser
		{
			Id = "user-1",
			Email = "user@example.com",
			UserName = "user@example.com"
		};

		var userManager = new FakeUserManager(user, isPasswordValid: true);
		var controller = new AuthController(userManager, new JwtService(TestJwtKey));

		var result = await controller.Login(new LoginDto
		{
			Email = "user@example.com",
			Password = "valid-password"
		});

		var ok = Assert.IsType<OkObjectResult>(result);
		var tokenProperty = ok.Value!.GetType().GetProperty("token");
		Assert.NotNull(tokenProperty);

		var token = tokenProperty!.GetValue(ok.Value) as string;
		Assert.False(string.IsNullOrWhiteSpace(token));
	}

	[Fact]
	public async Task Login_UserNotFound_ReturnsUnauthorized()
	{
		var userManager = new FakeUserManager(user: null, isPasswordValid: false);
		var controller = new AuthController(userManager, new JwtService(TestJwtKey));

		var result = await controller.Login(new LoginDto
		{
			Email = "missing@example.com",
			Password = "any-password"
		});

		Assert.IsType<UnauthorizedResult>(result);
	}

	[Fact]
	public async Task Login_InvalidPassword_ReturnsUnauthorized()
	{
		var user = new AppUser
		{
			Id = "user-1",
			Email = "user@example.com",
			UserName = "user@example.com"
		};

		var userManager = new FakeUserManager(user, isPasswordValid: false);
		var controller = new AuthController(userManager, new JwtService(TestJwtKey));

		var result = await controller.Login(new LoginDto
		{
			Email = "user@example.com",
			Password = "wrong-password"
		});

		Assert.IsType<UnauthorizedResult>(result);
	}

	private sealed class FakeUserManager : UserManager<AppUser>
	{
		private readonly AppUser? _user;
		private readonly bool _isPasswordValid;

		public FakeUserManager(AppUser? user, bool isPasswordValid)
			: base(
				new FakeUserStore(),
				Microsoft.Extensions.Options.Options.Create(new IdentityOptions()),
				new PasswordHasher<AppUser>(),
				new List<IUserValidator<AppUser>>(),
				new List<IPasswordValidator<AppUser>>(),
				new UpperInvariantLookupNormalizer(),
				new IdentityErrorDescriber(),
				new ServiceCollection().BuildServiceProvider(),
				new Logger<UserManager<AppUser>>(new LoggerFactory()))
		{
			_user = user;
			_isPasswordValid = isPasswordValid;
		}

		public override Task<AppUser?> FindByEmailAsync(string email)
		{
			if (_user != null && string.Equals(_user.Email, email, StringComparison.OrdinalIgnoreCase))
			{
				return Task.FromResult<AppUser?>(_user);
			}

			return Task.FromResult<AppUser?>(null);
		}

		public override Task<bool> CheckPasswordAsync(AppUser user, string password)
		{
			return Task.FromResult(_isPasswordValid);
		}
	}

	private sealed class FakeUserStore : IUserStore<AppUser>
	{
		public void Dispose()
		{
		}

		public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.Id);

		public Task<string?> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.UserName);

		public Task SetUserNameAsync(AppUser user, string? userName, CancellationToken cancellationToken)
			=> Task.CompletedTask;

		public Task<string?> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.UserName?.ToUpperInvariant());

		public Task SetNormalizedUserNameAsync(AppUser user, string? normalizedName, CancellationToken cancellationToken)
			=> Task.CompletedTask;

		public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(IdentityResult.Success);

		public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(IdentityResult.Success);

		public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(IdentityResult.Success);

		public Task<AppUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
			=> Task.FromResult<AppUser?>(null);

		public Task<AppUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
			=> Task.FromResult<AppUser?>(null);
	}
}
