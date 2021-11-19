namespace DDDExample.Domain.Dtos
{
    using System;
    using System.Text.Json.Serialization;
    using Enums;

    public class LoggedUserDto
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("status")]
        public RegistrationStatus? Status { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("lastLoggedAt")]
        public DateTimeOffset? LastLoggedAt { get; set; }
    }
}