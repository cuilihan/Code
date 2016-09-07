using DRP.BF;
using DRP.BF.OmMrg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;

namespace DRP.WEB.Module.Om
{
    public partial class ReceiptItem : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }

        private void BindData()
        {
            var e = new OrgInfo_BF().Get(Request["orgID"]);
            ReceiptAmt.Text = e.ReceiptAmt == null ? "0" : e.ReceiptAmt.ToString();
            OrgName.Text = e.Name;
        }
    }
}