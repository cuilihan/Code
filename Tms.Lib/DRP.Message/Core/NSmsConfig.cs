using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace DRP.Message.Core
{


    /// <summary>
    /// 模板短信类型（如注册类、通知类）对应SmsConfig.xml文件
    /// </summary>
    public enum T_NSms_Type
    {
        /// <summary>
        /// 验证类短信
        /// </summary>
        Validate = 1
    }

    /// <summary>
    /// 短信配置属性
    /// </summary>
    internal class NSmsConfig
    {
        /// <summary>
        /// 短信版本：用于是否更新缓存 
        /// </summary>
        public double SmsVersion { get; set; }

        /// <summary>
        /// 短信服务器地址
        /// </summary>
        public string SmsServerHost { get; set; }

        /// <summary>
        /// 短信账号
        /// </summary>
        public string Account_ID { get; set; }

        /// <summary>
        /// 短信密码
        /// </summary>
        public string Account_Pwd { get; set; }

        /// <summary>
        /// 短信模板
        /// </summary>
        public Dictionary<T_Sms_Type, string> SmsTemplate { get; set; }


        /// <summary>
        /// 短信配置实例
        /// </summary>
        /// <returns></returns>
        internal NSmsConfig CreateInstance()
        {
            var e = new NSmsConfig();
            e.SmsVersion = Convert.ToDouble(ConfigurationManager.AppSettings["SmsVersion"]);
            e.SmsServerHost = ConfigurationManager.AppSettings["NSmsServer"];
            e.Account_ID = ConfigurationManager.AppSettings["Account_ID"];
            e.Account_Pwd = ConfigurationManager.AppSettings["Account_PWD"];
            var dict = new Dictionary<T_Sms_Type, string>();
            var path = ConfigurationManager.AppSettings["Sms_T_Path"];
            if (!string.IsNullOrEmpty(path))
            {
                var doc = new XmlDocument();
                doc.Load(path);
                var nodes = doc.SelectNodes("document/sms");
                foreach (XmlNode node in nodes)
                {
                    var type = node.Attributes["type"].InnerText;
                    var data = node.InnerText;
                    dict.Add((T_Sms_Type)Convert.ToInt16(type), data);
                }
            }
            e.SmsTemplate = dict;
            return e;
        }
    }
}
