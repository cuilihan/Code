using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB.Module.T
{
    public partial class RouteList : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string NavigateID
        {
            get
            {
                return "routetemplate";
            }
        }
    }
}