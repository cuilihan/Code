using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class UserLogin_BF
    {
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="userAcct">账号</param>
        /// <param name="userPwd">密码</param>
        /// <param name="orgID">机构ID</param>
        /// <returns></returns>
        public bool Login(string userAcct, string userPwd, string orgID, out string userID)
        {
            userID = string.Empty;
            userAcct = HtmlHeler.NoHTML(userAcct);
            userPwd = HtmlHeler.NoHTML(userPwd);
            userPwd = Security.EncrypByRijndael(userPwd);
            if (string.IsNullOrEmpty(orgID)) return false;
            userID = new UserInfoDAL().IsExitUser(userAcct, userPwd, orgID);
            return !string.IsNullOrEmpty(userID);
        }
    }
}
