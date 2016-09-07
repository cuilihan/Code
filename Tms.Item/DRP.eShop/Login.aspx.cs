using DRP.BF.eShop;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop
{
    public partial class Login1 : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new eShopConfig().Get(OrgID);
            if (e != null)
            {
                if (!string.IsNullOrEmpty(e.Logo))
                    ImgLogo.ImageUrl = e.Logo;
                else
                    ImgLogo.Visible = false;
                lblOrgInfo.Text = e.TravelName;
                lblRegUrl.Text = string.Format("<a href='/Register.aspx?appid={0}'>注册</a>", e.OrgID);
            }
        }

        protected override string LocationName
        {
            get
            {
                return "用户登录";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var entity = new eShopMemeber().Login(txtUserAcct.Text.Trim(), txtUserPwd.Text.Trim(), OrgID);
            if (entity == null)
            {
                lblTips.Text = "密码或密码错误";
            }
            else
            {
                mPageBase.RemoveUserKey(entity.ID); //重置登录用户信息

                #region 记住账号
                CookieHelper.AddCookie("DRP_WECHAT_Key", entity.Mobile, DateTime.Now.AddDays(10));
                #endregion

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, entity.ID, DateTime.Now, DateTime.Now.AddMinutes(60), true, entity.Mobile);
                string cookieStr = FormsAuthentication.Encrypt(ticket);//对票据进行加密
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieStr);
                cookie.Expires = ticket.Expiration;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(cookie);
                var url = Request["ReturnUrl"];
                if (string.IsNullOrEmpty(url)) url = "/Module/My/MyInfo.aspx?appid=" + entity.OrgID;
                Response.Redirect(url);
            }
        }
    }
}