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
    public partial class SelectGuide : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindDeparture();
        }

        private void BindDeparture()
        { 
            Departure.DataSource = new Departure_BF().GetDeparture();
            Departure.DataValueField = "ID";
            Departure.DataTextField = "Name";
            Departure.DataBind();
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