using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingCustomerEvent.Models;
using TicketingCustomerEvent.Services.Interface;

namespace TicketingCustomerEvent.Services
{
    public class EventServices
    {
        private readonly IEventRepository _eventRepository;

        public EventServices(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetCustomerEvents(Customer searchedCustomer)
        {
           var events = await _eventRepository.FindEventsInCustomerCity(searchedCustomer);

           return events;
        }

        public async Task<IEnumerable<Event>> RetrieveSpecifiedClosestEventsInCities(Customer searchedCustomer, int numbers) => await _eventRepository.GetClosestEventsInCities(searchedCustomer, numbers);
    }
}