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
    public partial class RouteMrg : Pagebase
    { 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindRouteType();
        }

        private void BindRouteType()
        {
            RouteType.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            RouteType.DataTextField = "Name";
            RouteType.DataValueField = "ID";
            RouteType.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "routeinfo";
            }
        }
    }
}