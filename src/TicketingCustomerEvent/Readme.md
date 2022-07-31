##Questions answered

#Part I

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

> What is the expected output if we only have the client John Smith?

It will only send mail for events in John's city. 

> Do you believe there is a way to improve the code you first wrote?

Sure, we could use a database to store the event which makes it easier to fetch in future occurrence. We could also
use **Parallel.ForEach** to send the mail on multiple thread based on the system processor & environment.
 
#Part II

> What should be your approach to getting the distance between the customer’s city and
the other cities on the list?

Create a dictionary with Events as key and distance as values. 
Then afterwards get the distance for each events relative to the customer's city. Since we have it all in 
a hashmap, we could sort by ascending order 
