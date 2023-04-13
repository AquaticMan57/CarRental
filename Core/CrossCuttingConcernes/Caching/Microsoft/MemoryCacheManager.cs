using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Core.CrossCuttingConcernes.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheService
    {
        
        private static readonly FieldInfo? cacheEntriesStateDefinition = typeof(MemoryCache).GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly PropertyInfo? cacheEntriesCollectionDefinition = cacheEntriesStateDefinition?.FieldType.GetProperty("InternalDictionary", BindingFlags.NonPublic | BindingFlags.Instance);

        private IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>()!;
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key)!;
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key)!;
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {

            dynamic cacheEntriesCollection = null;
            var cacheEntriesFieldCollectionDefinition = typeof(MemoryCache).GetField("_coherentState",BindingFlags.NonPublic | BindingFlags.Instance);
            var caacheEntriesPropetyCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            if (cacheEntriesFieldCollectionDefinition != null)
            {
                var coherentStateValueCollection = cacheEntriesFieldCollectionDefinition.GetValue(_memoryCache);
                var entriesCollectionValueCollection = coherentStateValueCollection.GetType().GetProperty
                    (
                        "EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance
                    );
                cacheEntriesCollection = entriesCollectionValueCollection.GetValue(coherentStateValueCollection)!;
            }
            
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString()!)).Select(d => d.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
            
        }
    }
}
