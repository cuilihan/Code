using System;
using System.Collections.Generic;
using System.Linq;
using DRP.Framework.Core;
using System.Text;

namespace DRP.Framework.SSO
{
    /// <summary>
    /// SSO验证中心
    /// </summary>
    public static class SSOAuthentication
    {
        /// <summary>
        /// 创建各分站发往认证中心的 Token
        /// </summary>
        /// <param name="ssoRequest"></param>
        /// <returns></returns>
        public static SSORequest CreateAppToken(this SSORequest ssoRequest)
        {
            string OriginalAuthenticator = ssoRequest.IASID + ssoRequest.TimeStamp + ssoRequest.AppUrl + ssoRequest.UserAccount;
            string AuthenticatorDigest = Security.EncryptMD5(OriginalAuthenticator);
            ssoRequest.Authenticator = AuthenticatorDigest;
            return ssoRequest;
        }


        /// <summary>
        /// 验证从各分站发送过来的 Token
        /// </summary>
        /// <param name="ssoRequest"></param>
        /// <returns></returns>
        public static bool ValidateAppToken(this SSORequest ssoRequest)
        {
            string Authenticator = ssoRequest.Authenticator;
            string OriginalAuthenticator = ssoRequest.IASID + ssoRequest.TimeStamp + ssoRequest.AppUrl + ssoRequest.UserAccount;
            return Authenticator == Security.EncryptMD5(OriginalAuthenticator);
        }

        /// <summary>
        /// 序列化SSO对象
        /// </summary>
        /// <param name="ssoRequest"></param>
        /// <returns></returns>
        public static string SerializeSSORequest(this SSORequest ssoRequest)
        {
            var s = Security.SerializeObject<SSORequest>(ssoRequest);
            var bytes = System.Text.ASCIIEncoding.Default.GetBytes(s);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 反序列化SSO对象
        /// </summary>
        /// <param name="ssoRequest"></param>
        /// <returns></returns>
        public static SSORequest UnSerializeSSORequest(this string ssoRequestString)
        {
            var bytes = Convert.FromBase64String(ssoRequestString);
            var s = System.Text.ASCIIEncoding.Default.GetString(bytes);
            return Security.DeserializeObject<SSORequest>(s);
        }
    }
}
