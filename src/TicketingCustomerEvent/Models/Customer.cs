using System;

namespace TicketingCustomerEvent.Models
{
    public class Customer
    {
        public string? Name { get; set; }
        public string? City {  get; set; }

        public DateTime Birthday { get; set; }
    }
}