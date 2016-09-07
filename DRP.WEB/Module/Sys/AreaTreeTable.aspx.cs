using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB.Module.Sys
{
    public partial class AreaTreeTable : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var list = DRP.DAL.OTA_Setting.All().ToList();
                ddlOTA.DataSource = list;
                ddlOTA.DataValueField = "ID";
                ddlOTA.DataTextField = "OTAName";
                ddlOTA.DataBind();

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