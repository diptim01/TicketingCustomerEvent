##Questions answered

-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part I

> Q: What should be your approach to getting the list of events?

A: I had to create a service class which is an implementation against abstraction to get the events
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

> Q: How would you call the AddToEmailmethod in order to send the events in an email?

A: By looping through each events and sending to customer. We could improve the speed by multithreading the email calls

> Q: What is the expected output if we only have the client John Smith?

A: It will only send mail for events in John's city. 

> Q: Do you believe there is a way to improve the code you first wrote?

A: Sure, we could use a database to store the event which makes it easier to fetch in future occurrence. We could also
use `Parallel.ForEach` to send the mail on multiple thread based on the system processor & environment.
 


-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part II

> Q: What should be your approach to getting the distance between the customer’s city and
the other cities on the list?

A: Create a dictionary with Events as key and distance as values. 
Then afterwards get the distance for each events relative to the customer's city. There was an method implementation 
provided to calculate the distance between cities.

> Q: How would you get the 5 closest events and how would you send them to the client in an
email?

A: Since we have the events and their respective distance from the customer's city in a hashmap, 
we could sort in ascending order and pick the first 5 value and call the addToEmail endpoint.


```
var fiveClosestEvents = await eventManager.RetrieveSpecifiedClosestEventsInCities(customer, 5);
var closestEvents = fiveClosestEvents as Event[] ?? fiveClosestEvents.ToArray();

foreach (var @event in closestEvents)
{
    WorkingTemplateGiven.AddToEmail(customer, @event);
}

```

> Q: What is the expected output if we only have the client John Smith?

A: It will only print out the 5 closest cities to the customer in ascending order.

> Q: Do you believe there is a way to improve the code you first wrote?

A: With the use of clean architecture to abstract the implementation logic for separation 
of concern. I could also connect to the database for further optimization as opposed
to the choice of data structure utilized.


-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part III

> Q: If the GetDistance method is an API call which could fail or is too expensive, how will u
improve the code written in 2? Write the code.

A: I could cache the city-city distance calls. That will reduce the multiple number of calls to the 
database. Since this a console system, I built a simple caching method to check from the list
of already existed cities distance before making a call to the `GetDistance` endpoint.

Code:

-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:



#Part IV

> Q: If the GetDistance method can fail, we don't want the process to fail. What can be done?
 Code it. (Ask clarifying questions to be clear about what is expected business-wise)

A: 

-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part V

> Q: 5. If we also want to sort the resulting events by other fields like price, etc. to determine which
ones to send to the customer, how would you implement it? Code it.

A:

-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part VI

> Q: One of the questions is: how do you verify that what you’ve done is correct.

A: 



