using System;
using System.Diagnostics.CodeAnalysis;
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
        public async Task FindEventsInCustomerCity_ShouldReturnEmpty_WhenNoCityExist()
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

        [Fact]
        public async Task FindEventsInCustomerCity_ShouldReturnEvents_WhenCityExist()
        {
            var customer = new Customer
            {
                Name = "Mr Fake",
                City = "New York"
            };

            var @event = new Event
            {
                Name = "Metallica",
                City = "New York"
            };

            var result = await _sut.FindEventsInCustomerCity(customer);

            result.Should().ContainEquivalentOf(@event);
        }
        
        [Fact]
        public async Task GetClosestEventsInCities_ShouldReturnFiveClosestEvents_WhenCustomerCityExist()
        {
            var customer = new Customer
            {
                Name = "Mr Fake",
                City = "New York"
            };

            var @event = new Event
            {
                Name = "Metallica",
                City = "New York",
                Date = new DateTime(2022, 6, 18)
            };

            var result = await _sut.GetClosestEventsInCities(customer, 5);

            result.Should().ContainEquivalentOf(@event).
                And.HaveCount(5);   
        }
        
        [Fact]
        public async Task GetClosestEventsInCities_ShouldReturnNoClosestEvents_WhenNoCustomerCityExist()
        {
            Func<Task> result = () => _sut.FindEventsInCustomerCity(null);
            await result.Should().ThrowAsync<Exception>();
        }
        
        [Fact]
        public async Task GetClosestEventsWithBirthday_ShouldReturnClosestBirthdayEvents_WhenCustomerCityExist()
        {
            var customer = new Customer
            {
                Name = "Mr Fake",
                City = "New York",
                Birthday = new DateTime(2024, 7, 30)
            };

            var result = await _sut.GetClosestBirthdayWithEvents(customer, 5);

            result.Should().Contain(x => x.Date <= customer.Birthday);
        }
    }
}