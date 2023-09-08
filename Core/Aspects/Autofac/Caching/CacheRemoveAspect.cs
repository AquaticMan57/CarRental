using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private IMemoryCache _memoryCache;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _memoryCache= ServiceTool.ServiceProvider.GetService<IMemoryCache>()!;
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            dynamic cacheEntriesCollection = null;
            var prop = _memoryCache.GetType().GetProperty("EntriesCollection", BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Public);

            var cacheEntriesFieldCollectionDefinition = typeof(MemoryCache).GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance);
            var coherentStateValueCollection = cacheEntriesFieldCollectionDefinition.GetValue(_memoryCache);
            var entriesCollectionValueCollection = coherentStateValueCollection.GetType().GetProperty
                (
                    "EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance
                );
            cacheEntriesCollection = entriesCollectionValueCollection.GetValue(coherentStateValueCollection)!;

            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            var regex = new Regex(_pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString()!)).Select(d => d.Key)
                .ToList();
            foreach (var keyToRemove in keysToRemove)
            {
                _memoryCache.Remove(keyToRemove);
            }
            invocation.Proceed();
        }

    }
}
