using System;
using System.Threading.Tasks;
using FluentAssertions;
using TicketingCustomerEvent.Models;
using TicketingCustomerEvent.Services.Implementation;
using Xunit;

namespace TicketingCustomerEvent.Unit
{
    public class EventRepositoryTest
    {
        private readonly EventRepository _sut;
        
        public EventRepositoryTest()
        {
            _sut = new EventRepository();
        }
        
        [Fact]
        public async Task FindEventsInCustomerCity_ShouldReturnEmpty_WhenCityNotExist()
        {
            var customer = new Customer
            {
                Name = "Mr Fake",
                City = "NewYork"
            };
            
            var result = await _sut.FindEventsInCustomerCity(customer);

            result.Should().BeEmpty();
        }
        
        [Fact]
        public async Task FindEventsInCustomerCity_ShouldThrow_WhenCustomerIsNull()
        {
            Func<Task> result = () => _sut.FindEventsInCustomerCity(null);

            await result.Should().ThrowAsync<ArgumentException>().WithMessage("Customer cannot be null");
        }
    }
}