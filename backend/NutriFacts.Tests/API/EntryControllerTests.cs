using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutriFacts.Controllers;
using System.Security.Claims;
using Xunit;

public class EntryControllerTests
{
	[Fact]
	public async Task GetAll_WithoutAuthenticatedUser_ReturnsUnauthorized()
	{
		var controller = new EntryController(null!)
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			}
		};

		var result = await controller.GetAll();

		Assert.IsType<UnauthorizedResult>(result);
	}
}
