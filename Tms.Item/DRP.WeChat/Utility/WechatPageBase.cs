using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DRP.BF.OmMrg;
using DRP.BF.SysMrg;
using DRP.DAL;
using DRP.WeChat.Utility;

namespace DRP.WeChat
{
    public class WechatPageBase : Page
    {
        #region 登录用户
        private static Dictionary<string, WechatUserInfo> m_UserInfo = new Dictionary<string, WechatUserInfo>();

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public WechatUserInfo UserInfo
        {
            get
            {
                var userID = Page.User.Identity.Name;
                var e = m_UserInfo[userID];
                if (e == null)
                {
                    e = new WechatUserInfo();
                    var user = new UserInfoDAL().Get(userID);
                    if (user == null) return null;
                    e.UserName = user.UserName;
                    e.UserID = user.UserID;
                    e.OrgName = user.OrgName;
                    e.OrgID = user.OrgID;
                    e.DeptID = user.DeptID;
                    e.DeptName = user.DeptName;

                    m_UserInfo.Add(userID, e);
                }
                return e;
            }
        }

        /// <summary>
        /// 移除登录用户信息
        /// </summary>
        /// <param name="userID"></param>
        public static void RemoveUser(string userID)
        {
            if (m_UserInfo.ContainsKey(userID))
                m_UserInfo.Remove(userID);
        }
        #endregion
        
        #region 权限验证

        protected override void OnPreLoad(EventArgs e)
        {
            try
            {
                AccessAuthority();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Error.aspx?msg=" + DRP.Framework.Core.Security.Base64Encrypt(ex.Message));
            }
            base.OnPreLoad(e);
        }

        /// <summary>
        /// 访问权限
        /// </summary> 
        public void AccessAuthority()
        { 
        }

        #endregion

    }
}