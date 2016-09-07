using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework.Core;

namespace DRP.WEB.Module.CheckAccount
{
    public partial class ChkAcct : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblProduct.Text = ConfigHelper.GetAppSettingValue("OrgProductName");
                lblOrgInfo.Text = ConfigHelper.GetAppSettingValue("OrgName");
            }
        }
    }
}