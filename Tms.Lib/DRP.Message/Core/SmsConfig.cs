using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace DRP.Message.Core
{
    /// <summary>
    /// 模板短信类型（如注册类、通知类）对应SmsConfig.xml文件
    /// </summary>
    public enum T_Sms_Type
    {
        /// <summary>
        /// 验证类短信
        /// </summary>
        Validate = 1
    }

    /// <summary>
    /// 短信配置属性
    /// </summary>
    internal class SmsConfig
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
        /// 模板类短信账号
        /// </summary>
        public string T_Account_ID { get; set; }

        /// <summary>
        /// 模板类短信密码
        /// </summary>
        public string T_Account_Pwd { get; set; }

        /// <summary>
        /// 短信模板
        /// </summary>
        public Dictionary<T_Sms_Type, string> SmsTemplate { get; set; }

        /// <summary>
        /// 常规类短信账号
        /// </summary>
        public string N_Account_ID { get; set; }

        /// <summary>
        /// 常规类短信密码
        /// </summary>
        public string N_Account_Pwd { get; set; }

        /// <summary>
        /// 短信配置实例
        /// </summary>
        /// <returns></returns>
        internal SmsConfig CreateInstance()
        {
            var e = new SmsConfig();
            e.SmsVersion = Convert.ToDouble(ConfigurationManager.AppSettings["SmsVersion"]);
            e.SmsServerHost = ConfigurationManager.AppSettings["SmsServer"];
            e.T_Account_ID = DecrypByRijndael(ConfigurationManager.AppSettings["Sms_T_Account_ID"]);
            e.T_Account_Pwd = DecrypByRijndael(ConfigurationManager.AppSettings["Sms_T_Account_PWD"]);
            e.N_Account_ID = DecrypByRijndael(ConfigurationManager.AppSettings["Sms_N_Account_ID"]);
            e.N_Account_Pwd = DecrypByRijndael(ConfigurationManager.AppSettings["Sms_N_Account_PWD"]);
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

        #region << 解密 >>
        /// <summary>
        /// 密码KEY
        /// </summary>
        private const string CRYPTO_KEY = "SHvIVME624KWP4F83b2dhM/nqkDXNl6tTwAuHM0Rr7qgn3M0nLEUqrSqJrghhYEYEFZ/guXGgS5FSCGmyoKZEKx7CgByhU8UzRDL7nraPU6y0P5Uw/8UOcO8fEEWciBixdchigSQvDQQtfK4DSQrGmaW/AkuJ9hU9wmDNURIXMCatzHnKBQ0Q/hqPPQl5EH0XERaUJtXzYpHbZ43iWummOnG1S3dFLd4DeAG5mw/d0BJK7z3mwVob3";

        /// <summary>
        /// 密码IV
        /// </summary>
        private const string CRYPTO_IV = "tqjKJrcV5J9V4FDLLTOi6Oebv6Uy5Qt70reWdIA84FQdYMwsyNgDvg7Y5+ZghKWZ6Zcyyek74HhNasqTsjjis3yNT9PskCRaOr0c0NpHubC/OngpnN9S3Yi9ceaJLXcXvLDXKN48GdNOtbKZr4E1M8mzwUS4DIAAAqYYNcdiNx0WLnr15xYqFkLZ5rPgnKAo8/c4jzxYfSwSO7nid8qwMRhmApDQVYuqkQSFMTHVvdD4Fcb5px3QHI1lsgSx0GKA5ehPU6KUHkWv/F0B6UnlTPKwpHlmzLOxFHBI4GS5b3Ba53L12E";

        /// <summary>
        /// 获得密钥 
        /// </summary>
        /// <param name="start">开始位置,0~50</param>
        /// <returns>返回KEY</returns>
        private byte[] GetLegalKey(int start)
        {
            string key = CRYPTO_KEY.Substring(start, 32);
            return ASCIIEncoding.ASCII.GetBytes(key);
        }

        /// <summary>
        /// 获得初始向量IV 
        /// </summary>
        /// <param name="start">开始位置,0~50</param>
        /// <returns>返回IV</returns>
        private byte[] GetLegalIV(int start)
        {
            string iv = CRYPTO_IV.Substring(start, 16);
            return ASCIIEncoding.ASCII.GetBytes(iv);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="text">要解密的字串</param> 
        /// <returns></returns>
        private string DecrypByRijndael(string text)
        {
            int keyStart = 5;
            int ivStart = 9;
            byte[] bytIn = Convert.FromBase64String(text);
            MemoryStream ms = null;
            SymmetricAlgorithm cryptoAlg = null;
            CryptoStream cs = null;
            StreamReader sr = null;
            string outtext = null;

            try
            {
                ms = new MemoryStream(bytIn);
                cryptoAlg = new RijndaelManaged();
                cryptoAlg.Key = GetLegalKey(keyStart);
                cryptoAlg.IV = GetLegalIV(ivStart);
                ICryptoTransform decryptor = cryptoAlg.CreateDecryptor();
                cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                outtext = sr.ReadToEnd();
            }
            finally
            {
                if (sr != null)
                    sr.Close();
                if (cs != null)
                    cs.Close();
                if (ms != null)
                    ms.Close();
                if (cryptoAlg != null)
                    cryptoAlg.Clear();
            }
            return outtext;
        }

        #endregion
    }
}
