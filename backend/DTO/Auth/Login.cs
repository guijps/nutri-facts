using System.Text.Json.Serialization;

namespace NutriFacts.DTO.Auth
{
    [JsonSerializable(typeof(LoginDto))]
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}