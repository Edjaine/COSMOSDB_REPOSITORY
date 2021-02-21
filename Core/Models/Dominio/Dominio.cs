using System;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public abstract class Dominio
    {
        [JsonIgnore]
        public Guid Id { get; set; }                    

    }
}