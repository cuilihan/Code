using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WeChat
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["msg"]))
            {
                lblError.Text = DRP.Framework.Core.Security.Base64Decrypt(Request["msg"]);
            }
        }
    }
}