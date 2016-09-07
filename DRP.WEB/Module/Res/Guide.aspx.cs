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
    public partial class Guide :Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DepartureID.DataSource = new Departure_BF().GetDeparture();
            DepartureID.DataValueField = "ID";
            DepartureID.DataTextField = "Name";
            DepartureID.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "guide";
            }
        }
    }
}