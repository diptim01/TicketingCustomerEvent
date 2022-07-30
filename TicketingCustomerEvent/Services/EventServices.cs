using System.Collections.Generic;
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
        
        public IEnumerable<Event> GetCustomerEvents(Customer searchedCustomer) => _eventRepository.FindEventsInCustomerCity(searchedCustomer);
        
        public IEnumerable<Event> RetrieveSpecifiedClosestEventsInCities(Customer searchedCustomer, int numbers) => _eventRepository.GetClosestEventsInCities(searchedCustomer, numbers);
    }
}