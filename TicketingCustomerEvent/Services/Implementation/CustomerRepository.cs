using System.Collections;
using System.Collections.Generic;
using TicketingCustomerEvent.Models;
using TicketingCustomerEvent.Services.Interface;

namespace TicketingCustomerEvent.Services.Implementation
{
    public class CustomerRepository 
    {
        private readonly IEventRepository _eventRepository;

        public CustomerRepository(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        
       
    }
}