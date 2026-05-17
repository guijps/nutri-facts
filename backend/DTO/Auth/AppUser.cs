using Microsoft.AspNetCore.Identity;
public class AppUser:IdentityUser
{
    public override string? UserName { get; set; } = string.Empty;
    public override string? Email { get; set; } = string.Empty;
}