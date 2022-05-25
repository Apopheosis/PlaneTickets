using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models
{
    public class RefundedTicket:PlaneTicket
    {
        [JsonProperty("ticket_number")]
        [MinLength(13)]
        [MaxLength(13)]
        public string TicketNumber { get; set; }
        
    }
}