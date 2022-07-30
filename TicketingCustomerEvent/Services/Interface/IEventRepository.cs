using System.Collections;
using System.Collections.Generic;
using TicketingCustomerEvent.Models;

namespace TicketingCustomerEvent.Services.Interface
{
    public interface IEventRepository
    {
        IEnumerable<Event> FindEventsInCustomerCity(Customer customer);
        IEnumerable<Event> GetClosestEventsInCities(Customer customer, int numbers);
    }
}