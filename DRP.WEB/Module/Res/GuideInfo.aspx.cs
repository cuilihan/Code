using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.ResMrg;

namespace DRP.WEB.Module.Res
{
    public partial class GuideInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new Guide_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                IsIDCard.Text = e.IsIDCard == null ? "" : ((bool)e.IsIDCard ? "有" : "");
                IsLeaderCard.Text = e.IsLeaderCard ? "有" : "";
                IsEnable.Text = e.IsEnable ? "启用" : "<span style='color:red;'>禁用</span>";
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