using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRP.WeChat.Utility
{
    /// <summary>
    /// 微信登录用户信息
    /// </summary>
    [Serializable]
    public class WechatUserInfo
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        public string OrgID { get; set; }

        public string OrgName { get; set; }

        public string DeptID { get; set; }

        public string DeptName { get; set; }
    }
}