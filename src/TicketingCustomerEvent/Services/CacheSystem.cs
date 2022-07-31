using System;
using System.Collections.Generic;
using TicketingCustomerEvent.Models;

namespace TicketingCustomerEvent.Services
{
    public class CacheSystem<TKey>
    {
        private static readonly Dictionary<TKey, CacheEvent> _cache = new ();
        private readonly TimeSpan _maxCachingTime;
        
        public CacheSystem()
            : this(TimeSpan.MaxValue)
        {
        }
        
        public CacheSystem(TimeSpan maxCachingTime)
        {
            _maxCachingTime = maxCachingTime;
        }
        
        public int Get(TKey key, Func<int> createValue)
        {
            CacheEvent cacheItem;
            
            if (_cache.TryGetValue(key, out cacheItem) && (DateTime.Now - cacheItem.CacheTime) <= _maxCachingTime) {
                return cacheItem.CityDistance;
            }
            
            var value = createValue();
            _cache[key] = new CacheEvent(value);
            return value;
        }
    }
}