using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Om
{
    public partial class QRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                QRCodeImg.Visible = true;
                QRCodeImg.ImageUrl = "/Files/Wechat/" + Request["id"] + ".jpg";
            }
        }
    }
}