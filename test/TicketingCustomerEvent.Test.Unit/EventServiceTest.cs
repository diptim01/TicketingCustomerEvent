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
        public async Task GetCustomerEvents_ShouldReturnEmptyEvent_WhenNoCustomerCityExist()
        {
            _eventRepository.FindEventsInCustomerCity(null).Returns(Enumerable.Empty<Event>());
            
            var result =  await _sut.GetCustomerEvents(null);

            result.Should().BeEmpty();


        }
    }
}