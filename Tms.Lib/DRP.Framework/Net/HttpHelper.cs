using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DRP.Framework.Net
{
    /// <summary>
    /// Post或Get网络服务
    /// </summary>
    public class HttpHelper
    {

        #region 异步Get或Post

        /// <summary>
        /// HttpClient实现Get异步请求
        /// </summary>
        public string GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
        }


        /// <summary>
        /// HttpClient实现Post异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="nameValueCollection"></param>
        /// <returns></returns>
        /// Demo:
        ///  var o = new { ordertype = "散客订单", orderxml = "<document><data>111</data></document>", sign = "密钥" }; 
        ///  PostAsync("http://localhost:8037/osp/ordersync", o);
        public string PostAsync(string url, object entity)
        {
            using (var client = new HttpClient())
            {
                var requestJson = JsonConvert.SerializeObject(entity);
                HttpContent httpContent = new StringContent(requestJson);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return client.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
            }
        }
        #endregion


        /// <summary>
        /// 网站Cookies
        /// </summary>
        public string CookieHeader { get; set; }

        /// <summary>
        /// Post请求，没有登录验证（未使用cookie）的情况
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="postData">提交的数据如：username=aaa&userpwd=bbb</param>
        public string PostRequest(string url, string postData)
        {
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = postBytes.Length;

            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            string result = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary> 
        /// 功能描述：(模拟登录页面)提交数据，并记录Header中的cookie 
        /// </summary> 
        /// <param name="strURL">登录数据提交的页面地址</param> 
        /// <param name="strArgs">用户登录数据,如 string postData = "userName=admin&password=pass&area=2006&Submit=%B5%C7+%C2%BC";</param> 
        /// <param name="strReferer">引用地址</param> 
        /// <returns>可以返回页面内容或不返回</returns> 
        public string AgencyPostData(string strURL, string strArgs, string cookie = "", string strReferer = "")
        {
            string strResult = "";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
            if (!string.IsNullOrEmpty(strReferer))
                myHttpWebRequest.Referer = strReferer;
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.Method = "POST";
            if (myHttpWebRequest.CookieContainer == null)
            {
                myHttpWebRequest.CookieContainer = new CookieContainer();
            }
            if (!string.IsNullOrEmpty(cookie))
            {
                myHttpWebRequest.Headers.Add("cookie:" + cookie);
                myHttpWebRequest.CookieContainer.SetCookies(new Uri(strURL), cookie);
            }
            byte[] postData = Encoding.UTF8.GetBytes(strArgs);
            myHttpWebRequest.ContentLength = postData.Length;
            using (Stream PostStream = myHttpWebRequest.GetRequestStream())
            {
                PostStream.Write(postData, 0, postData.Length);
                PostStream.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse())
            {
                if (myHttpWebRequest.CookieContainer != null)
                {
                    this.CookieHeader = myHttpWebRequest.CookieContainer.GetCookieHeader(new Uri(strURL));
                }
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                }
            }
            return strResult;
        }


        /// <summary> 
        /// 功能描述： 获取网页的内容,如果有登录验证，须在PostLogin成功登录后再获取内容，自动记录下Headers中的cookie 
        /// </summary> 
        /// <param name="strURL">获取网站的某页面的地址</param> 
        /// <param name="strReferer">引用的地址</param> 
        /// <returns>返回页面内容</returns>  
        public string GetRequest(string strURL,string cookie="", string strReferer = "")
        {
            string strResult = "";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.Method = "GET";
            if (myHttpWebRequest.CookieContainer == null)
            {
                myHttpWebRequest.CookieContainer = new CookieContainer();
            }
            if (!string.IsNullOrEmpty(cookie))
            {
                myHttpWebRequest.Headers.Add("cookie:" + cookie);
                myHttpWebRequest.CookieContainer.SetCookies(new Uri(strURL), cookie);
            }

            using (HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse())
            {
                Stream streamReceive;
                string gzip = response.ContentEncoding;
                if (string.IsNullOrEmpty(gzip) || gzip.ToLower() != "gzip")
                {
                    streamReceive = response.GetResponseStream();
                }
                else
                {
                    streamReceive = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                }

                using (StreamReader sr = new StreamReader(streamReceive, Encoding.UTF8))
                {
                    if (response.ContentLength > 1)
                    {
                        strResult = sr.ReadToEnd();
                    }
                    else
                    {
                        char[] buffer = new char[256];
                        int count = 0;
                        StringBuilder sb = new StringBuilder();
                        while ((count = sr.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            sb.Append(new string(buffer));
                        }
                        strResult = sb.ToString();
                    }
                }
            }
            return strResult;
        }
    }
}
