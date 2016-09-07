using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var msg = Request["msg"];
            if (!string.IsNullOrEmpty(msg))
            {
                lblErrInfo.Text = DRP.Framework.Core.Security.Base64Decrypt(msg);                 
            }
        }
    }
}