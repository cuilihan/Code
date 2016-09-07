using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace DRP.Framework.SSO
{
    [Serializable]
    public class SSORequest : MarshalByRefObject
    {
        /// <summary>
        /// 系统标识ID
        /// </summary>
        public string IASID { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 系统访问URL
        /// </summary>
        public string AppUrl { get; set; }

        /// <summary>
        /// 系统认证Token
        /// </summary>
        public string Authenticator { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserAccount { get; set; } 

        /// <summary>
        /// 系统IP地址
        /// </summary>
        public string IPAddress { get; set; } 
    }
}
