using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TicketingCustomerEvent;
using TicketingCustomerEvent.Models;
using TicketingCustomerEvent.Services;
using TicketingCustomerEvent.Services.Implementation;
using TicketingCustomerEvent.Services.Interface;


Console.WriteLine("");

Customer customer = new()
{
    Name = "Mr Fake",
    City = "New York",
    Birthday = new DateTime(2022, 8, 1)
};
    
    
//Task 1: I prefer to insolate the calls and use for a single customer
var eventManager = new EventServices(new EventRepository());
var @events = await eventManager.GetCustomerEvents(customer);
foreach (var @event in events)
{
    //Task 2
    WorkingTemplateGiven.AddToEmail(customer, @event);

}

Console.WriteLine("******Task2*********");

var fiveClosestEvents = await eventManager.RetrieveSpecifiedClosestEventsInCities(customer, 5);
var closestEvents = fiveClosestEvents as Event[] ?? fiveClosestEvents.ToArray();

foreach (var @event in closestEvents)
{
    WorkingTemplateGiven.AddToEmail(customer, @event);
}

Console.WriteLine("******Sort by Price*********");

foreach (var @event in closestEvents.SortEventsByParams("Price", true))
{
    //Task 2
    Console.Write("{0} - ", @event.Price);
    WorkingTemplateGiven.AddToEmail(customer, @event);
    
}

Console.WriteLine("******Closest by birthday date*********");

var birthdaysEvents = await eventManager.RetrieveClosestEventsWithBirthdays(customer, 15);
foreach (var @event in birthdaysEvents)
{
    //Task 2
    Console.Write("{0} - ", @event.Date);
    WorkingTemplateGiven.AddToEmail(customer, @event);
    
}
