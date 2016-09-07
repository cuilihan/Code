using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;

namespace DRP.WEB.Module.Res
{
    public partial class TravelAgency : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
        }
         
        protected override string NavigateID
        {
            get
            {
                return "travelagency";
            }
        }
    }
}