using DRP.BF;
using DRP.WeChat.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WeChat
{
    public partial class LoginOut : WeChatBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var orgID = "";
            try
            {
                var user = AuthenticationPage.UserInfo;
                orgID = user.OrgID;
                AuthenticationPage.RemoveCacheKey(user.UserID);
                //PassPort myPassPort = new PassPort();
                //myPassPort.Logout(user.UserID.ToString());
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Error.aspx?msg=" + DRP.Framework.Core.Security.Base64Encrypt(ex.Message));
            }
            Response.Redirect(ResolveUrl("login.aspx?orgID=" + orgID));
        }


    }
}