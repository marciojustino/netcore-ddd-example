namespace DDDExample.Domain.Dtos
{
    using System.Text.Json.Serialization;

    public class UpdateUserDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}