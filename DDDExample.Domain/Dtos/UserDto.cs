namespace DDDExample.Domain.Dtos
{
    using System.Text.Json.Serialization;

    public class UserDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("salt")]
        public string Salt { get; set; }
    }
}