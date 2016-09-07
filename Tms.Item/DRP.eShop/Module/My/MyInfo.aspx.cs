using DRP.BF.eShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop.Module.My
{
    public partial class MyInfo : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var user = base.CurrentUser;
            var src = string.IsNullOrEmpty(user.Photo) ? "/Files/Sys/Default.jpg" : user.Photo;
            lblPhoto.Text = string.Format("<img id='imgPhoto' src='{0}' />", src);
            lblUserName.Text = string.IsNullOrEmpty(user.UserName) ? user.NickName : user.UserName;
            lblMobile.Text = user.Mobile;

            var sb = new StringBuilder();
            sb.AppendFormat("<a href=\"Photo.aspx?appid={0}\">更换头像</a>", OrgID);
            sb.AppendFormat("<a href=\"InfoEdit.aspx?appid={0}\">修改资料</a>", OrgID);
            lblButtons.Text = sb.ToString();
        }

        protected override string LocationName
        {
            get
            {
                return "个人中心";
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                var user = base.CurrentUser;
                mPageBase.RemoveUserKey(user.UserID);
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                base.UrlReturn(ex.Message);
            }
            Response.Redirect(ResolveUrl("~/Index.aspx?appid=" + OrgID));
        }
    }
}