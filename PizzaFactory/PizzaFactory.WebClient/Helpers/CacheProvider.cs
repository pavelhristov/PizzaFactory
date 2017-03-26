using PizzaFactory.WebClient.Helpers.Contracts;
using System;
using System.Web;

namespace PizzaFactory.WebClient.Helpers
{
    public class CacheProvider : ICacheProvider
    {
        public object GetItem(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }

        public void SetItem(string key, object obj, double minDuration = 10)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, DateTime.MaxValue, TimeSpan.FromMinutes(minDuration));
        }
    }
}