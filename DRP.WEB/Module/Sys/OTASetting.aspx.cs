using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.ResMrg;

namespace DRP.WEB.Module.Sys
{
    public partial class OTASetting : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDDL();
            }
        }

        private void BindDDL()
        {
            ddlSupplier.DataSource = new TravelAgency_BF().QueryOTAData();
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "ID";
            ddlSupplier.DataBind();
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