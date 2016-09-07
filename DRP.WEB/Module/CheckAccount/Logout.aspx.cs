using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.CheckAccount
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Error.aspx?msg=" + DRP.Framework.Core.Security.Base64Encrypt(ex.Message));
            }
            Response.Redirect(ResolveUrl("~/"));
        }
    }
}