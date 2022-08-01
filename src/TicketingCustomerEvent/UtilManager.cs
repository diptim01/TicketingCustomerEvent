using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using TicketingCustomerEvent.Models;

namespace TicketingCustomerEvent
{
    public static class UtilManager
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        
        public static IEnumerable<Event> SortEventsByParams(this IEnumerable<Event> events, string sortValue,
            bool isAscending)
        {
            return isAscending ? events.OrderBy(s => s.GetType().GetProperty(sortValue)?.GetValue(s)) : 
                events.OrderByDescending(s => s.GetType().GetProperty(sortValue)?.GetValue(s));
        }

        public static string GetKey(string city1, string city2)
        {
            if (string.IsNullOrEmpty(city1) || string.IsNullOrEmpty(city2))
                return "";

            var fullString = ReplaceWhitespace(city1.ToLower(), String.Empty) +
                             ReplaceWhitespace(city2.ToLower(), String.Empty);
            
            char[] characters = fullString.ToCharArray();
            Array.Sort(characters);
            
            var uniqueKey =  new string(characters);
            return uniqueKey;
        }
        

        private static string ReplaceWhitespace(string input, string replacement) 
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}