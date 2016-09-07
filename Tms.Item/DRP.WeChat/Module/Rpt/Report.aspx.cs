using DRP.BF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WeChat.Module
{
    public partial class Report : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override string NavigateID
        {
            get
            {
                return "wechat";
            }
        }
    }
}