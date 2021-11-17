using DDDExample.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DDDExample.Domain.Dtos
{
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
