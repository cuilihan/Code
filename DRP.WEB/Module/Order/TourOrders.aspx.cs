using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.ProMrg;

namespace DRP.WEB.Module.Order
{
    public partial class TourOrders :Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var tour = new TourInfo_BF().Get(KeyID);
            lnkToWord.NavigateUrl = "tOrdersToWord.aspx?id=" + tour.ID;
            if (tour != null)
            {
                OrderName.Text = tour.TourName;
                TourDate.Text = tour.TourDate.ToString("yyyy-MM-dd");
                ReturnDate.Text = tour.ReturnDate.ToString("yyyy-MM-dd");

                var route = new RouteInfo_BF().Get(tour.RouteID);
                if (route != null)
                {
                    DestinationName.Text = route.Destination;
                }
            }
            UserName.Text = AuthenticationPage.UserInfo.UserName;
            printDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss");
        }

        protected override string NavigateID
        {
            get
            {
                return "salesorderzzb";
            }
        }
    }
}