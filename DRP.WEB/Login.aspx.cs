using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.CheckAccount;
using DRP.BF.OmMrg;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckDomain();
                BindOrgID();
                RecoverUserKey();
            }
        }

        /// <summary>
        /// 检查域名是否存在
        /// </summary>
        private void CheckDomain()
        {
            try
            {
                var orgID = new Permisstion_BF().CheckDomain();
                if (!string.IsNullOrEmpty(orgID))
                {
                    var fileName = "/Files/Wechat/" + orgID + ".jpg";
                    if (File.Exists(Server.MapPath(fileName)))
                    {
                        QRCode.ImageUrl = fileName;
                        QRCode.Visible = true;
                    }
                }
                else
                {
                    lblQRCode.Text = "未查询到机构二维码";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Error.aspx?msg=" + Security.Base64Encrypt(ex.Message));
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var userType = hfUserType.Value.ToInt();
            if (userType == 1) //系统用户登录
            {
                LoginValid(txtUserID.Text.Trim(), txtPwd.Text.Trim());
            }
            else //导游报账登录
            {
                GuideLogin(txtUserID.Text.Trim(), txtPwd.Text.Trim());
            }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        private void LoginValid(string userId, string pwd)
        {
            bool isValid = true;
            var orgID = OrgID.SelectedValue;
            // var orgID = "";
            if (string.IsNullOrEmpty(orgID))
            {
                var org = OrgInfo_BF.DomainOrgInfo();
                orgID = org.ID;
            }

            try
            {
                isValid = new UserLogin_BF().Login(userId, pwd, orgID, out userId);
            }
            catch
            {
                isValid = false;
            }
            if (isValid)
            {
                AuthenticationPage.RemoveCacheKey(userId);

                OmScan_BF.LoginTrace(userId, orgID);//记录用户的登录次数

                #region 记住账号
                CookieHelper.AddCookie("DRP_User_Key", txtUserID.Text, DateTime.Now.AddDays(10));
                #endregion

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId,
                    DateTime.Now, DateTime.Now.AddMinutes(120), true, userId);

                string cookieStr = FormsAuthentication.Encrypt(ticket);//对票据进行加密
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieStr);
                cookie.Expires = ticket.Expiration;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(cookie);

                #region 判断用户是否具有多个角色：限定每次只能以一个角色登录系统
                var userRoles = new User_BF().GetUserRoles(userId);
                if (userRoles.Count > 1)
                {
                    var sb = new StringBuilder();
                    var idx = 1;
                    userRoles.ForEach(x =>
                    {
                        sb.AppendFormat("<li>{0}、<a href='/Index.aspx?id={1}'>{2}</a></li>", idx++, x.ID, x.RoleName);
                    });
                    lblRoles.Text = sb.ToString();

                    JScript.ScriptStartUp("fnUserRolesSelect();");
                }
                else
                    Response.Redirect("/");
                #endregion
            }
            else
            {
                lblTip.Text = "账号或密码错误";
            }
        }

        /// <summary>
        /// 导游报账登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwdID"></param>
        private void GuideLogin(string userId, string pwd)
        {
            var iCount = 0;
            var orgID = OrgID.SelectedValue;
            // var orgID = "";
            if (string.IsNullOrEmpty(orgID))
            {
                var org = OrgInfo_BF.DomainOrgInfo();
                orgID = org.ID;
            }
            try
            {
                iCount = new GuideLogin().Login(userId, pwd, orgID);
            }
            catch
            {
                iCount = 0;
            }
            if (iCount > 0)
            {

                #region 记住账号
                CookieHelper.AddCookie("DRP_User_Key", userId, DateTime.Now.AddDays(10));
                #endregion

                var userData = userId + "@" + orgID + "@" + pwd;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userData,
                    DateTime.Now, DateTime.Now.AddMinutes(60), true, userData);

                string cookieStr = FormsAuthentication.Encrypt(ticket);//对票据进行加密
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieStr);
                cookie.Expires = ticket.Expiration;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(cookie);

                Response.Redirect("/Module/CheckAccount/OrderSelect.aspx");
            }
            else
            {
                lblTip.Text = "账号或密码错误";
            }
        }

        /// <summary>
        /// 恢复登录账号
        /// </summary>
        private void RecoverUserKey()
        {
            var uid = CookieHelper.GetCookieValue("DRP_User_Key");
            if (!string.IsNullOrEmpty(uid))
            {
                txtUserID.Text = uid;
            }
        }

        #region << 模拟子系统的业务数据 >>
        private void BindOrgID()
        {
#if DEBUG
            OrgID.Visible = true;
            OrgID.DataSource = new OrgInfo_BF().GetOrgInfo();
            OrgID.DataValueField = "ID";
            OrgID.DataTextField = "Name";
            OrgID.DataBind();
#else
            OrgID.Visible=false;
#endif
        }
        #endregion
    }
}