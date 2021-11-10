namespace DDDExample.Domain.Entities
{
    using System;
    using System.Text.Json.Serialization;

    public abstract class BaseEntity
    {
        [JsonPropertyName("id")]
        public virtual Guid Id { get; set; }
    }
}