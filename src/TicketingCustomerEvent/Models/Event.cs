using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TicketingCustomerEvent.Services;

namespace TicketingCustomerEvent.Models
{
    public class Event
    {
        public string? Name { get; set; }
        public string? City { get; set; }

        public DateTime Date { get; set; }

        public decimal Price => WorkingTemplateGiven.GetPrice(this);

        public static IEnumerable<Event> GetEvents()
        {
            return new List<Event>
            {
                new() {Name = "Phantom of the Opera", City = "New York", Date = new DateTime(2022, 4, 30)},
                new() {Name = "Metallica", City = "Los Angeles", Date = new DateTime(1992, 5, 3)},
                new() {Name = "Metallica", City = "New York", Date = new DateTime(2022, 6, 18)},
                new() {Name = "Metallica", City = "Boston", Date = new DateTime(2022, 8, 22)},
                new() {Name = "LadyGaGa", City = "New York", Date = new DateTime(2022, 9, 14)},
                new() {Name = "LadyGaGa", City = "Boston", Date = new DateTime(2022, 7, 26)},
                new() {Name = "LadyGaGa", City = "Chicago", Date = new DateTime(2020, 3, 6)},
                new() {Name = "LadyGaGa", City = "San Francisco", Date = new DateTime(2021, 2, 9)},
                new() {Name = "LadyGaGa", City = "Washington", Date = new DateTime(2023, 1, 6)}
            };
        }
    }
}