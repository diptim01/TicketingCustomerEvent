using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingCustomerEvent.Models;
using TicketingCustomerEvent.Services.Interface;

namespace TicketingCustomerEvent.Services.Implementation
{
    public class EventRepository : IEventRepository
    {
        private readonly IEnumerable<Event> _events;

        public EventRepository()
        {
            _events = Event.GetEvents();
        }

        public Task<IEnumerable<Event>> FindEventsInCustomerCity(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("Customer cannot be null");
            
            var retrievedEvents = _events.Where(x =>
                x.City != null && x.City.Equals(customer.City, StringComparison.OrdinalIgnoreCase));
            
            return Task.FromResult(retrievedEvents);
        }

        public Task<IEnumerable<Event>> GetClosestEventsInCities(Customer customer, int numbers)
        {
            Dictionary<Event, int> eventDistance = new();

            foreach (var @event in _events)
            {
                if (@event.City != null)
                    eventDistance.Add(@event, WorkingTemplateGiven.GetDistance(customer.City, @event.City));
            }

            var fiveClosestEvents = eventDistance.OrderBy(x => x.Value).Take(numbers);

            return Task.FromResult(fiveClosestEvents.Select(x => x.Key));
        }
    }
}