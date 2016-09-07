using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DRP.Framework.Core
{
    public class IP
    {
        /// <summary>
        /// 客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            string str = "";
            //穿过代理服务器取远程用户真实IP地址：
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                str = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else
                str = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            return str;
        }

        /// <summary>
        /// 客户端浏览器
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserType()
        {
            HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
            bc = System.Web.HttpContext.Current.Request.Browser;
            return bc.Type;
        }        

        /// <summary>
        /// 把ip转成数字，即八进制数转换成十进制
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static int IPToInt(string ipAddress)
        {
            string disjunctiveStr = ".,: ";
            char[] delimiter = disjunctiveStr.ToCharArray();
            string[] startIP = null;
            for (int i = 1; i <= 5; i++)
            {
                startIP = ipAddress.Split(delimiter, i);
            }
            if (startIP.Length != 4) return Int32.MaxValue;
            string a1 = startIP[0].ToString();
            string a2 = startIP[1].ToString();
            string a3 = startIP[2].ToString();
            string a4 = startIP[3].ToString();
            int U1 = a1.ToInt();
            int U2 = a2.ToInt(); 
            int U3 = a3.ToInt(); 
            int U4 = a4.ToInt();  

            int U = U1 << 24;
            U += U2 << 16;
            U += U3 << 8;
            U += U4;
            return U;
        }

        /// <summary>
        /// 数字转化成IP地址
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static string IntToIP(int ipAddress)
        {
            long ui1 = ipAddress & 0xFF000000;
            ui1 = ui1 >> 24;
            long ui2 = ipAddress & 0x00FF0000;
            ui2 = ui2 >> 16;
            long ui3 = ipAddress & 0x0000FF00;
            ui3 = ui3 >> 8;
            long ui4 = ipAddress & 0x000000FF;
            string IPstr = "";
            IPstr = System.Convert.ToString(ui1) + "."
                + System.Convert.ToString(ui2) + "."
                + System.Convert.ToString(ui3)
                + "." + System.Convert.ToString(ui4);
            return IPstr;
        }
    }
}
