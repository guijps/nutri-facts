using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NutriFacts.Controllers;

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly JwtService _jwt;

    public AuthController(
        UserManager<AppUser> userManager,
        JwtService jwt)
    {
        _userManager = userManager;

        _jwt = jwt;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result =
            await _userManager.CreateAsync(
                user,
                dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromHeader(Name = "email")] string email,
        [FromHeader(Name = "password")] string password)
    {
        var user =
            await _userManager.FindByEmailAsync(email);

        if (user == null)
            return Unauthorized();

        var valid =
            await _userManager.CheckPasswordAsync(
                user,
                password);

        if (!valid)
            return Unauthorized();

        var token = _jwt.GenerateToken(user);

        return Ok(new { token });
    }
}