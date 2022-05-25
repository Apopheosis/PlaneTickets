using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public int SerialNumber { get; set; }
        public string OperationType { get; set; }
        public DateTimeOffset OperationTime { get; set; }
        public string OperationPlace { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string DocType { get; set; }
        public string DocNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string PassengerType { get; set; }
        public string TicketNumber { get; set; }
        public int TicketType { get; set; }
        public string AirlineCode { get; set; }
        public int FlightNum { get; set; }
        public string DepartPlace { get; set; }
        public DateTimeOffset DepartDatetime { get; set; }
        public string ArrivePlace { get; set; }
        public DateTimeOffset ArriveDatetime { get; set; }
        public string PnrId { get; set; }
    }
}