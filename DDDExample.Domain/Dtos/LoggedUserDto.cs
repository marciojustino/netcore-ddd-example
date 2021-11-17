using DDDExample.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DDDExample.Domain.Dtos
{
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
