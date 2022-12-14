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

> Q:  it should trigger the events closest to client birthday (assume any birthdate of your choice)

A: Select only dates that are above or equal to the customer birthday date.

```
 var closeBirthdays = _events.Where(x => x.Date >= customer.Birthday).OrderBy(x=>x.Date); 
```

-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part III

> Q: If the GetDistance method is an API call which could fail or is too expensive, how will u
improve the code written in 2? Write the code.

A: I could cache the city-city distance calls. That will reduce the multiple number of calls to the 
database or API. Since this a console system, I built a simple caching method to check from the list
of already checked existing cities distance before making a call to the `GetDistance` endpoint.

Code:
Click to see the [Cache System](https://github.com/diptim01/TicketingCustomerEvent/blob/master/src/TicketingCustomerEvent/Services/CacheSystem.cs)

```
if (@event.City == null || customer.City == null) continue;
                
var distance = new CacheSystem<string>().Get(UtilManager.GetKey(customer.City, @event.City),
() => WorkingTemplateGiven.GetDistance(customer.City, @event.City));

eventDistance.Add(@event, distance);

public static string GetKey(string city1, string city2)
{
    var fullString = city1.ToLower() + city2.ToLower();
    
    char[] characters = fullString.ToCharArray();
    Array.Sort(characters);
    
    var uniqueKey =  new string(characters);
    return uniqueKey;
}
```


-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:



#Part IV

> Q: If the GetDistance method can fail, we don't want the process to fail. What can be done?
 Code it. (Ask clarifying questions to be clear about what is expected business-wise)

A: For exceptions, I will have it in a try catch block, (`GetDistance` endpoint), log the error, and 
make the distance negative value (int.MinValue) so it won't appear on customer events. It is not suppose
to read a negative distance value.

Likewise, I could exempt the process from adding the event for the user and also the cache when it is negative distance - {error value}

code: 
[Get Distance endpoint](https://github.com/diptim01/TicketingCustomerEvent/blob/master/src/TicketingCustomerEvent/Services/WorkingTemplateGiven.cs)
& other implementations are in the eventRepository and Cache sytem

-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part V

> Q: 5. If we also want to sort the resulting events by other fields like price, etc. to determine which
ones to send to the customer, how would you implement it? Code it.

A: Make a call to the `GetPrice` endpoint provided. I made an extension method for the orderBy in which
we could supply the order parameter and if it ascending or descending.

Click to see [Sorting Implementation for Price](https://github.com/diptim01/TicketingCustomerEvent/blob/master/src/TicketingCustomerEvent/UtilManager.cs) & [Event model](https://github.com/diptim01/TicketingCustomerEvent/blob/master/src/TicketingCustomerEvent/Models/Event.cs)
```
 public static IEnumerable<Event> SortEventsByParams(this IEnumerable<Event> events, string sortValue,
            bool isAscending)
{
    return isAscending ? events.OrderBy(s => s.GetType().GetProperty(sortValue)?.GetValue(s)) : 
       events.OrderByDescending(s => s.GetType().GetProperty(sortValue)?.GetValue(s));
}
```

-----------------
:wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash: :wavy_dash:

#Part VI

> Q: One of the questions is: how do you verify that what you’ve done is correct.

A: I unit tested the code base to be sure the method are doing what they are suppose to do. Furthermore, mocked the event repository to assert results for the services

Click to see the [Unit test](https://github.com/diptim01/TicketingCustomerEvent/tree/master/test/TicketingCustomerEvent.Test.Unit)



