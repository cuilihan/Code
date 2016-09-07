using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.SysMrg;
using DRP.Framework.Core;


namespace DRP.WeChat
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitData();
        }

        private void InitData()
        {
            var orgID = Request["ORGID"];
            if (string.IsNullOrEmpty(orgID))
            {
                lblTips.Text = "缺少机构参数ID";
                btnLogin.Visible = false;
                return;
            }
            var org = new OrgInfo_BF().Get(orgID);
            if (org == null)
            {
                lblTips.Text = "机构ID无效";
                btnLogin.Visible = false;
                return;
            }

            lblOrgInfo.Text = org.Name;
            //var user = RestoreUserInfo(orgID);
            //if (user != null)
            //    UserLogin(user);
        }

        /// <summary>
        /// 从Cookie中还原用户信息
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        private UserInfo RestoreUserInfo(string orgID)
        {
            var user = CookieHelper.GetCookieValue(orgID);
            if (!string.IsNullOrEmpty(user))
            {
                user = Security.DecrypByRijndael(user);
                if (!string.IsNullOrEmpty(user))
                {
                    return Security.DeserializeObject<UserInfo>(user);
                }
            }
            return null;
        }

        /// <summary>
        /// 用户登录系统
        /// </summary>
        /// <param name="user"></param>
        private void UserLogin(string UserID)
        {
            var isValid = true;
            try
            {
                AuthenticationPage.RemoveCacheKey(UserID);

                #region 记住账号
                //  var strUserInfo = Security.SerializeObject(user);
                //  CookieHelper.AddCookie(user.OrgID, Security.EncrypByRijndael(strUserInfo), DateTime.Now.AddDays(1));
                #endregion

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserID,
                    DateTime.Now, DateTime.Now.AddMinutes(60), true, UserID);
                string cookieStr = FormsAuthentication.Encrypt(ticket);//对票据进行加密
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieStr);
                cookie.Expires = ticket.Expiration;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(cookie);
            }
            catch
            {
                isValid = false;
            }
            if (isValid)
                Response.Redirect("/");
            else
                lblTips.Text = "用户登录时发生错误";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var pwd = txtUserPwd.Text.Trim();
            var user = new WechatUser_BF().GetUserInfo(txtUserAcct.Text.Trim(), Security.EncrypByRijndael(pwd), Request["ORGID"]);
            if (user == null)
            {
                lblTips.Text = "账号或密码错误";
            }
            else
            {
                UserLogin(user.UserID);
            }
        }
    }
}