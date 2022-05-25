using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Models
{
    public class Passenger
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("surname")]
        public string surname { get; set; }
        [JsonProperty("patronymic")]
        public string patronymic { get; set; }
        [JsonProperty("doc_type")]
        public string doc_type { get; set; }
        [JsonProperty("doc_number")]
        public string doc_number { get; set; }
        [JsonProperty("birthdate")]
        public DateTime birthdate { get; set; }
        [JsonProperty("gender")]
        [RegularExpression("M|F")]
        public string gender { get; set; }
        [JsonProperty("passenger_type")]
        public string passenger_type { get; set; }
        [JsonProperty("ticket_number")]
        [MinLength(13)]
        [MaxLength(13)]
        public string ticket_number { get; set; }
        [JsonProperty("ticket_type")]
        public int ticket_type { get; set; }
    }
}