using System;

namespace TicketingCustomerEvent.Models
{
    public readonly struct CacheEvent
    {
        public CacheEvent(int cityDistance)
            : this()
        {
            CityDistance = cityDistance;
            CacheTime = DateTime.Now;
        }

        public int CityDistance { get; }
        public DateTime CacheTime { get;  }
    }
}