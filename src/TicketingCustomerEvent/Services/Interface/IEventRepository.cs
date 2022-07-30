using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingCustomerEvent.Models;

namespace TicketingCustomerEvent.Services.Interface
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> FindEventsInCustomerCity(Customer customer);
        Task<IEnumerable<Event>> GetClosestEventsInCities(Customer customer, int numbers);
        Task<IEnumerable<Event>> GetClosestBirthdayWithEvents(Customer customer, int numbers);
    }
}