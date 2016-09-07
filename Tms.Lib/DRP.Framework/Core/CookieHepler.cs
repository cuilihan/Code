using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DRP.Framework.Core
{
    public class CookieHelper
    {
        #region << 读取Cookie >>

        /// <summary>
        /// 获得Cookie的值 
        /// </summary>
        /// <param name="name">cookie名称</param>
        /// <returns>返回cookie的值，如果cookie为空或已过期则返回空字串</returns>
        public static string GetCookieValue(string name)
        {
            HttpCookie cookie = GetCookie(name);
            if (cookie != null && cookie.Expires < DateTime.Now)
            {
                if (string.IsNullOrEmpty(cookie.Value))
                    return "";
                else
                    return Security.Base64Decrypt(cookie.Value);
            }
            else
                return "";
        }

        /// <summary>
        /// 获得Cookie的值 
        /// </summary>
        /// <param name="name">cookie名称</param>
        /// <param name="key">cookie对应的键</param>
        /// <returns>返回cookie的值，如果cookie为空或已过期则返回空字串</returns>
        public static string GetCookieValue(string name, string key)
        {

            HttpCookie cookie = GetCookie(name);
            if (cookie != null && cookie.Expires < DateTime.Now && cookie.Values.AllKeys.Contains(key))
            {
                if (string.IsNullOrEmpty(cookie.Values[key]))
                    return "";
                else
                    return Security.Base64Decrypt(cookie.Values[key]);
            }
            else
                return "";
        }

        /// <summary>
        /// 获得Cookie
        /// </summary>
        /// <param name="name">cookie名称</param>
        /// <returns>返回cookie对象</returns>
        private static HttpCookie GetCookie(string name)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
            {
                return request.Cookies[name];
            }
            return null;
        }

        #endregion 

        #region << 删除Cookie >>

        /// <summary>
        /// 删除Cookie 
        /// </summary>
        /// <param name="name">cookie名称</param>
        public static void RemoveCookie(string name)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Expires = DateTime.Today.AddDays(-1);
            AddCookie(cookie);
        }

        #endregion

        #region << 添加Cookie >>

        /// <summary>
        /// 添加Cookie 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">到期时间</param>
        public static void AddCookie(string name, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = expires;
            AddCookie(cookie);
        }

        /// <summary>
        /// 添加Cookie 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="key">内容键</param>
        /// <param name="value">内容值</param>
        /// <param name="expires">到期时间</param>
        public static void AddCookie(string name, string key, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Values.Add(key, value);
            cookie.Expires = expires;
            AddCookie(cookie);
        }

        /// <summary>
        /// 添加Cookie 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="key">内容键</param>
        /// <param name="value">内容值</param> 
        public static void AddCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = DateTime.Now.AddYears(1);
            AddCookie(cookie);
        }
 
        /// <summary>
        /// 添加Cookie 
        /// </summary>
        /// <param name="cookie">cookie对象</param>
        private static void AddCookie(HttpCookie cookie)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            { 
                cookie.HttpOnly = true;//指定客户端脚本可以访问 
                cookie.Value =Security.Base64Encrypt(cookie.Value);
                response.Cookies.Add(cookie);
            }
        } 
        #endregion 
    }
}
