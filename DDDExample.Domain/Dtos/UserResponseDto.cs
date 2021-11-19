namespace DDDExample.Domain.Dtos
{
    using System;
    using System.Text.Json.Serialization;
    using Enums;

    public class UserResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("status")]
        public RegistrationStatus Status { get; set; }

        [JsonPropertyName("lastLoggedAt")]
        public DateTime? LastLoggedAt { get; set; }
    }
}