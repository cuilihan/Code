using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB.Module.Guide
{
    public partial class Help : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserName.Text = AuthenticationPage.UserInfo.UserName;
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}