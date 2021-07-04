using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeProjectCache
{
    public class Cache<TKey, TObject>
    {
        private Dictionary<TKey, TObject> _cache = new();

        public void AddOrUpdate(TKey key, TObject itemToCache)
        {
            if(_cache.ContainsKey(key))
            {
                _cache[key] = itemToCache;
            }
            else
            {
                _cache.Add(key, itemToCache);
            }
        }

        public TObject Get(TKey key)
        {
            if(_cache.ContainsKey(key))
            {
                return _cache[key];
            }

            return default(TObject);
        }
    }


    public class Cache : Cache<string, object>
    {
        private static Lazy<Cache> _globalCache = new Lazy<Cache>();
        public static Cache Global => _globalCache.Value;
    }

}
