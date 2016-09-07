using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Framework.Core;
using DRP.Framework;
using ServiceStack.Redis;
using System.Web;
using ServiceStack.Redis.Support;

namespace DRP.Cached
{
    /// <summary>
    /// 缓存类型，是WEB本地缓存还是专用媒体缓存
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// Web服务器本身缓存（导航、权限）
        /// </summary>
        LocalCache,
        /// <summary>
        /// Redis服务器缓存(业务数据）
        /// </summary>
        RedisCache
    }

    /// <summary>
    /// 缓存管理
    /// 创建人：李金友
    /// 创建日期：2014-10-19
    /// T:可以序列化的对象
    /// </summary>
    public static class CacheHelper
    { 

        #region << Get >>

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheType"></param>
        /// <returns></returns>
        public static T Get<T>(string key, CacheType cacheType)
        {
            try
            {
                if (cacheType == CacheType.LocalCache)
                    return LocalCacheHelper.Get<T>(key);
                else
                    return RedisCacheHelper.Get<T>(key);
            }
            catch (Exception ex)
            {
                var url = "/Error.aspx?msg=" + Security.Base64Encrypt(ex.Message);
                HttpContext.Current.Response.Redirect(url);
            }
            return default(T);
        }


        #endregion

        #region << Set >>

        /// <summary>
        /// 设置或写入缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="cacheType"></param>
        public static bool Set<T>(string key, T obj, CacheType cacheType)
        {
            var isResult = true;
            try
            {
                if (cacheType == CacheType.LocalCache)
                {
                    LocalCacheHelper.Insert<T>(key, obj);
                }
                else
                {
                    RedisCacheHelper.Set<T>(key, obj);
                }
            }
            catch (Exception ex)
            {
                isResult = false;
                var url = "/Error.aspx?msg=" + Security.Base64Encrypt(ex.Message);
                HttpContext.Current.Response.Redirect(url);
            }
            return isResult;
        }

        /// <summary>
        /// 跳转错误页面
        /// </summary>
        /// <param name="msg"></param>
        public static void UrlReturn(string msg = "")
        {
            var url = "/Error.aspx";
            if (!string.IsNullOrEmpty(msg))
                url += "?msg=" + Security.Base64Encrypt(msg);
            HttpContext.Current.Response.Redirect(url);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <returns></returns>
        public static void ClearCache(CacheType cacheType)
        {
            if (cacheType == CacheType.LocalCache)
                LocalCacheHelper.Clear();
            else
                RedisCacheHelper.Clear();
        }

        /// <summary>
        /// 删除指定键的缓存
        /// </summary>
        /// <returns></returns>
        public static void RemoveCache(CacheType cacheType,string cacheKey)
        {
            if (cacheType == CacheType.LocalCache)
            {
                LocalCacheHelper.Remove(cacheKey);
            }
            else
            {
                RedisCacheHelper.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        /// <returns></returns>
        public static void ClearAllCache()
        {
            LocalCacheHelper.Clear();
            RedisCacheHelper.Clear();
        }
        #endregion
    }
}
