using System;
using System.Collections.Generic;
using System.Linq;
using TicketingCustomerEvent.Models;
using TicketingCustomerEvent.Services;
using TicketingCustomerEvent.Services.Implementation;
using TicketingCustomerEvent.Services.Interface;

Console.WriteLine("");

Customer customer = new()
{
    Name = "Mr Fake",
    City = "New York"
};
    
    
//Task 1: I prefer to insolate the calls and use for a single customer
var eventManager = new EventServices(new EventRepository());
var @events = eventManager.GetCustomerEvents(customer);
foreach (var @event in events)
{
    //Task 2
     WorkingTemplateGiven.AddToEmail(customer, @event);

}

Console.WriteLine("******Task2*********");
var fiveClosestevents = eventManager.RetrieveSpecifiedClosestEventsInCities(customer, 5);

foreach (var @event in fiveClosestevents)
{
    //Task 2
    WorkingTemplateGiven.AddToEmail(customer, @event);
}


