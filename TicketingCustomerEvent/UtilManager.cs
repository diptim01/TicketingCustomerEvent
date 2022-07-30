using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TicketingCustomerEvent.Models;

namespace TicketingCustomerEvent
{
    public static class UtilManager
    {
        public static IEnumerable<Event> SortEventsByParams(this IEnumerable<Event> events, string sortValue,
            bool isAscending)
        {
            return isAscending ? events.OrderBy(s => s.GetType().GetProperty(sortValue)?.GetValue(s)) : 
                events.OrderByDescending(s => s.GetType().GetProperty(sortValue)?.GetValue(s));
        }
    }
}