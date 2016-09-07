using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var msg = Request["msg"];
            if (msg == "404") lblMessage.Text = "未找到页面";
            else
                lblMessage.Text = DRP.Framework.Core.Security.Base64Decrypt(Request["msg"]);
        }
    }
}