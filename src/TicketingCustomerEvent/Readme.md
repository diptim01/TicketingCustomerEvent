#Questions answered

> What should be your approach to getting the list of events?

I had to create a service class with is an implementation against abstraction to get the events
and afterwards loop through them to send the email for events close to customers

Click to see the [The service implementation](https://github.com/diptim01/TicketingCustomerEvent/blob/master/src/TicketingCustomerEvent/Services/Implementation/EventRepository.cs) :+1:

```
var eventManager = new EventServices(new EventRepository());
var @events = await eventManager.GetCustomerEvents(customer);
foreach (var @event in events)
{

WorkingTemplateGiven.AddToEmail(customer, @event);

}
```

> How would you call the AddToEmailmethod in order to send the events in an email?

By looping through each events and sending to customer. We could improve the speed by multithreading the email calls


