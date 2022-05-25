using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml;
using Models;
using Newtonsoft.Json;

public class PlaneTicket
{
    [JsonProperty("operation_type")]
    public string OperationType { get; set; }
    [JsonProperty("operation_time")]
    public DateTimeOffset OperationTime { get; set; }
    [JsonProperty("operation_place")]
    public string OperationPlace { get; set; }
    [JsonProperty("passenger")]
    public Passenger passenger { get; set; }
    [JsonProperty("routes")]
    public ICollection<Models.Route> routes { get; set; }
}
