using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.Framework.OnLineUser;

namespace DRP.WEB
{
    public partial class Logout : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var user = AuthenticationPage.UserInfo;
                AuthenticationPage.RemoveCacheKey(user.UserID);
                //PassPort myPassPort = new PassPort();
                //myPassPort.Logout(user.UserID.ToString());
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            catch(Exception ex)
            {
                Response.Redirect("/Error.aspx?msg=" + DRP.Framework.Core.Security.Base64Encrypt(ex.Message));
            }
            Response.Redirect(ResolveUrl("~/"));
        }

        protected override string NavigateID
        {
            get
            {
                return "index";
            }
        }
    }
}