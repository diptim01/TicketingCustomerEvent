using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using TicketingCustomerEvent.Models;
using TicketingCustomerEvent.Services;
using TicketingCustomerEvent.Services.Interface;
using Xunit;

namespace TicketingCustomerEvent.Unit
{
    public class EventServiceTest
    {
        private readonly EventServices _sut;
        private readonly IEventRepository _eventRepository = Substitute.For<IEventRepository>();

        public EventServiceTest()
        {
            _sut = new EventServices(_eventRepository);
        }

        [Fact]
        public async Task GetCustomerEvents_ShouldReturnEmptyEvent_WhenInvalidCustomerCityExist()
        {
            var customer = new Customer
            {
                Name = "Mr Fake",
                City = "United States"
            };

            _eventRepository.FindEventsInCustomerCity(customer).Returns(Enumerable.Empty<Event>());

            var result = await _sut.GetCustomerEvents(customer);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCustomerEvents_ShouldReturnEvent_WhenCustomerCityExist()
        {
            var customer = new Customer
            {
                Name = "Mr Fake",
                City = "United States"
            };
            
            var @event = new Event
            {
                Name = "Phantom of the Opera",
                City = "New York"
            };

            var expectedEvents = new[] {@event};

             _eventRepository.FindEventsInCustomerCity(customer).Returns(expectedEvents);

            var result = await _sut.GetCustomerEvents(customer);

            var enumerable = result as Event[] ?? result.ToArray();
            
            enumerable.Should().BeEquivalentTo(expectedEvents);
            enumerable.Should().Contain(expectedEvents);
        }
    }
}