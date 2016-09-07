using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB.Module.Sys.Service
{
    public partial class SelectOTAUser : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDate();
            }
        }

        private void BindDate()
        {
            var list = DRP.DAL.OTA_Setting.All().ToList();
            ddlSupplier.DataSource = list;
            ddlSupplier.DataValueField = "ID";
            ddlSupplier.DataTextField = "OTAName";
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