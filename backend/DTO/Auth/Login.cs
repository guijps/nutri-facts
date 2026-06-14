using System.ComponentModel.DataAnnotations;

namespace NutriFacts.DTO.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
    }
}