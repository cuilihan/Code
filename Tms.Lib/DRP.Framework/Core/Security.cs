using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace DRP.Framework.Core
{
    /// <summary>
    /// 系统安全类：加（解）密&&（反）序列化
    /// </summary>
    public class Security
    {
        #region << 一般加解密 >>
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
        private static byte[] GetLegalKey(int start)
        {
            string key = CRYPTO_KEY.Substring(start, 32);
            return ASCIIEncoding.ASCII.GetBytes(key);
        }

        /// <summary>
        /// 获得初始向量IV 
        /// </summary>
        /// <param name="start">开始位置,0~50</param>
        /// <returns>返回IV</returns>
        private static byte[] GetLegalIV(int start)
        {
            string iv = CRYPTO_IV.Substring(start, 16);
            return ASCIIEncoding.ASCII.GetBytes(iv);
        }


        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="text">要加密的字串</param>
        /// <param name="keyStart">开始位置,0~50</param>
        /// <param name="ivStart">开始位置,0~50</param>
        /// <returns></returns>
        public static string EncrypByRijndael(string text, int keyStart = 5, int ivStart = 9)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(text);
            MemoryStream ms = null;
            SymmetricAlgorithm cryptoAlg = null;
            CryptoStream cs = null;
            byte[] bytOut = null;

            try
            {
                cryptoAlg = new RijndaelManaged();
                cryptoAlg.Key = GetLegalKey(keyStart);
                cryptoAlg.IV = GetLegalIV(ivStart);
                ICryptoTransform encryptor = cryptoAlg.CreateEncryptor();

                ms = new MemoryStream();
                cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                bytOut = ms.ToArray();
            }
            finally
            {
                if (cs != null)
                    cs.Close();

                if (ms != null)
                    ms.Close();

                if (cryptoAlg != null)
                    cryptoAlg.Clear();
            }

            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="text">要解密的字串</param>
        /// <param name="keyStart">开始位置,0~50</param>
        /// <param name="ivStart">开始位置,0~50</param>
        /// <returns></returns>
        public static string DecrypByRijndael(string text, int keyStart = 5, int ivStart = 9)
        {
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

        #region << MD5加密 >>
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptMD5(string str)
        {
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            byte[] b = ASCIIEncoding.ASCII.GetBytes(str.Trim());
            return ASCIIEncoding.ASCII.GetString(md.ComputeHash(b));
        }
        #endregion

        #region << base64 >>
        /// <summary>
        /// 对字符串进行Base64编码加密 
        /// </summary>
        /// <param name="input">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
                return "";
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(input);
            string text = Convert.ToBase64String(encbuff);
            text = text.Replace('+', '*');
            text = text.Replace('/', '-');
            text = text.Replace('=', '!');
            return text;
        }

        /// <summary>
        /// 对字符串进行Base64编码解密 
        /// </summary>
        /// <param name="input">待解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
                return "";

            //替换回Base64编码字符串中的特殊字符
            input = input.Replace('*', '+');
            input = input.Replace('-', '/');
            input = input.Replace('!', '=');
            byte[] decbuff = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(decbuff, 0, decbuff.Length);
        }
        #endregion

        #region << 序列化 >>

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>序列化对象后的字符串</returns>
        public static string SerializeObject<T>(T obj)
        {
            System.Runtime.Serialization.IFormatter bf = new BinaryFormatter();
            string result = string.Empty;
            using (MemoryStream ms = new System.IO.MemoryStream())
            {
                bf.Serialize(ms, obj);
                byte[] byt = new byte[ms.Length];
                byt = ms.ToArray();
                result = System.Convert.ToBase64String(byt);
                ms.Flush();
            }
            return result;
        }

        /// <summary>
        /// 还原序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns>T对象</returns>
        public static T DeserializeObject<T>(string str) where T : class
        {
            System.Runtime.Serialization.IFormatter bf = new BinaryFormatter();
            byte[] byt = Convert.FromBase64String(str);
            using (MemoryStream ms = new MemoryStream(byt, 0, byt.Length))
            {
                return (T)bf.Deserialize(ms);
            }
        }

        #endregion
    }
}
