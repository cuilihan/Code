using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;

namespace DRP.WEB.Module.Pro
{
    public partial class Venue :Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindRouteType();
        }

        private void BindRouteType()
        {
            RouteTypeID.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            RouteTypeID.DataTextField = "Name";
            RouteTypeID.DataValueField = "ID";
            RouteTypeID.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "venue";
            }
        }
    }
}