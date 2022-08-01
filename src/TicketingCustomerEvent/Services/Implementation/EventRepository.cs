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
                if (@event.City == null || customer.City == null) continue;

                var distance = new CacheSystem<string>().Get(UtilManager.GetKey(customer.City, @event.City),
                    () => WorkingTemplateGiven.GetDistance(customer.City, @event.City));

                if (distance < 0) continue;
                
                eventDistance.Add(@event, distance);
            }

            var fiveClosestEvents = eventDistance.OrderBy(x => x.Value).Take(numbers);

            return Task.FromResult(fiveClosestEvents.Select(x => x.Key));
        }

        public Task<IEnumerable<Event>> GetClosestBirthdayWithEvents(Customer customer, int numbers)
        {
            var closeBirthdays = _events.OrderBy(x => Math.Abs((x.Date - customer.Birthday).Ticks)).AsEnumerable();
            return Task.FromResult(closeBirthdays);
        }
    }
}