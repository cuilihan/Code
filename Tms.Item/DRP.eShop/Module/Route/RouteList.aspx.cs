using DRP.BF.eShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop.Module.Route
{
    public partial class RouteList : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }
        }

        

        protected override string LocationName
        {
            get
            {
                return "线路查询";
            }
        }
    }
}