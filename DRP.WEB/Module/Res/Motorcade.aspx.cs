using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.ResMrg;

namespace DRP.WEB.Module.Res
{
    public partial class Motorcade : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DepartureID.DataSource = new Departure_BF().GetDeparture();
            DepartureID.DataTextField = "Name";
            DepartureID.DataValueField = "ID";
            DepartureID.DataBind(); 
        } 

        protected override string NavigateID
        {
            get
            {
                return "motorcade";
            }
        }

    }
}