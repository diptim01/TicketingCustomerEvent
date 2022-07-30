using System.Collections;
using System.Collections.Generic;

namespace TicketingCustomerEvent.Models
{
    public class Event
    {
        public string? Name { get; private set; }
        public string? City { get; private set; }

        public static IEnumerable<Event> GetEvents()
        {
            return new List<Event>
            {
                new() {Name = "Phantom of the Opera", City = "New York"},
                new() {Name = "Metallica", City = "Los Angeles"},
                new() {Name = "Metallica", City = "New York"},
                new() {Name = "Metallica", City = "Boston"},
                new() {Name = "LadyGaGa", City = "New York"},
                new() {Name = "LadyGaGa", City = "Boston"},
                new() {Name = "LadyGaGa", City = "Chicago"},
                new() {Name = "LadyGaGa", City = "San Francisco"},
                new() {Name = "LadyGaGa", City = "Washington"}
            };
        }
    }
}