namespace DDDExample.Domain.Dtos
{
    using System.Text.Json.Serialization;

    public class AuthDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("salt")]
        public string Salt { get; set; }
    }
}