using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Web.Caching;

namespace DRP.Framework
{
    /// <summary>
    /// 缓存服务
    /// 应用服务器上的服务器缓存
    /// 创建人：李金友
    /// </summary>
    public class LocalCacheHelper
    {
        private static readonly Cache _cache;
        private static readonly object __obj = new object();

        /// <summary>
        /// 静态缓存类初始化
        /// </summary>
        static LocalCacheHelper()
        {
            HttpContext content = HttpContext.Current;
            if (content != null)
                _cache = content.Cache;
            else
                _cache = HttpRuntime.Cache;
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            List<string> list = new List<string>();
            while (CacheEnum.MoveNext())
            {
                list.Add(CacheEnum.Key.ToString()); 
            }
            list.ForEach(x => _cache.Remove(x)); 
        }

        /// <summary>
        /// 获取所有的缓存键
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheKeys()
        {
            List<string> list = new List<string>();
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            { 
                list.Add(CacheEnum.Key.ToString());
            }
            return list;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            lock (__obj)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 对象插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="seconds"></param>
        public static void Insert<T>(string key, T obj, int cacheDays = 1)
        {
            if (obj != null)
            {
                lock (__obj)
                {
                    _cache.Insert(key, obj, null, DateTime.UtcNow.AddDays(cacheDays), TimeSpan.Zero);
                }
            }
        }

        /// <summary>
        /// 对象插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="seconds"></param>
        public static void Insert<T>(string key, List<T> list, int cacheDays = 1)
        {
            if (list != null)
            {
                lock (__obj)
                {
                    _cache.Insert(key, list, null, DateTime.UtcNow.AddDays(cacheDays), TimeSpan.Zero);
                }
            }
        }
        
        /// <summary>
        /// 返回缓存中对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return (T)_cache[key];
        }

    }
}
