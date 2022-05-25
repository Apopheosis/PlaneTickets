using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Models
{
    public class Route
    {
        [JsonProperty("airline_code")]
        public String airline_code { get; set; }
        [JsonProperty("flight_num")]
        public int flight_num { get; set; }
        [JsonProperty("depart_place")]
        public String depart_place { get; set; }
        [JsonProperty("depart_datetime")]
        public DateTimeOffset depart_datetime { get; set; }
        [JsonProperty("arrive_place")]
        public String arrive_place { get; set; }
        [JsonProperty("arrive_datetime")]
        public DateTimeOffset arrive_datetime { get; set; }
        [JsonProperty("pnr_id")]
        public String pnr_id { get; set; }
    }
}