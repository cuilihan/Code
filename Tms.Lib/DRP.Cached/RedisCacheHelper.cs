using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;

namespace DRP.Cached
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCacheHelper
    { 
        /// <summary>
        /// 缓存服务器客户端实例
        /// </summary>
        private static IRedisClient instance = DRPRedisClient.CreateInstance();
             
        #region 缓存数据

        /// <summary>
        /// 插入缓存数据
        /// </summary> 
        /// <returns>是否成功</returns>
        public static bool Set<T>(string key, T value, DateTime? expiresAt = null)
        {
            using (IRedisClient client = DRPRedisClient.CreateInstance())
            {
                return client.Set<T>(key, value);
            }         
        } 
       
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取缓存信息 
        /// <returns>返回缓存的数据</returns>
        public static T Get<T>(string key)
        {
            if (instance == null) return default(T);
            return instance.Get<T>(key);
        }
         
        /// <summary>
        /// 根据键集合获取缓存信息 
        /// </summary> 
        /// <returns>返回缓存的数据</returns>
        public static IDictionary<string, T> Get<T>(List<string> keys)
        {
            if (keys == null) return default(IDictionary<string, T>);
            if (instance == null)
                return instance.GetAll<T>(keys);
            return default(IDictionary<string, T>);
        }

        /// <summary>
        /// 查询数据 
        /// </summary> 
        public static List<T> SearchData<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            if (instance == null) return null;
            var keys = instance.SearchKeys(key);
            return instance.GetValues<T>(keys);
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 根据Key移除缓存 
        /// <returns>是否成功</returns> 
        public static bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            if (instance == null) return false;
            return instance.Remove(key);
        }

        /// <summary>
        /// 根据Keys删除缓存数据 
        public static void Remove(List<string> keys)
        {
            if (keys == null) return;
            if (instance == null) return;
            instance.RemoveAll(keys);
        }

        #endregion

        #region Hash操作数据

        /// <summary>
        /// 获取键值对信息 
        /// </summary> 
        public static Dictionary<string, string> GetEntiriesFromHash(string hashId)
        {
            if (instance == null) return null;
            return instance.GetAllEntriesFromHash(hashId);
        }

        /// <summary>
        /// 获取对象中列的值 
        /// </summary> 
        public static string HashGetValue(string hashID, string key)
        {
            if (instance == null) return "";
            return instance.GetValueFromHash(hashID, key);
        }

        /// <summary>
        /// Hash存储
        /// </summary> 
        /// <returns>返回是否执行成功</returns>
        public static bool HashSetValue(string hashId, string key, string value)
        {
            if (instance == null) return false;
            return instance.SetEntryInHash(hashId, key, value);
        }


        /// <summary>
        /// Hash方式判断对象字段是否存在 
        /// </summary> 
        /// <returns>返回是否存在</returns>
        public static bool HashContainEntry(string hashId, string key)
        {
            if (instance == null)
                return false;
            return instance.HashContainsEntry(hashId, key);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 判断当前缓存中是否存在该键的缓存项 
        /// </summary> 
        public static bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;
            if (instance == null)
                return false;
            return instance.ContainsKey(key);
        }

        /// <summary>
        /// 持久化缓存到文件  
        /// </summary>
        public static void Save()
        {
            if (instance != null)
            {
                instance.Save();
            }
        }

        /// <summary>
        /// 清空缓存 
        /// </summary>
        public static void Clear()
        {
            if (instance != null)
                instance.FlushAll();
        }

        #endregion
    }
}
