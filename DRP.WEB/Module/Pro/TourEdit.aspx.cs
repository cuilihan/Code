using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.ProMrg;
using DRP.Framework;

namespace DRP.WEB.Module.Pro
{
    public partial class TourEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var tourID = KeyID.IndexOf(',') > -1 ? KeyID.Split(',')[0] : KeyID;

            var dal = new TourInfo_BF();
            var tour = dal.Get(tourID);

            if (tour == null) return;
            LoadData(tour, pnlContainer);
            var route = new RouteInfo_BF().Get(tour.RouteID);
            if (route != null)
            {
                rowSeat.Visible = route.RouteType.Contains("短线");
                BindVenue(route, tour.ID);
            }

            rptPrice.DataSource = dal.GetTourPrice(tour.ID);
            rptPrice.DataBind();
        }

        private void BindVenue(DAL.Pro_RouteInfo route, string tourID)
        {
            var venueList = new Venue_BF().GetVenue(route.RouteTypeID);
            var tourVenue = new TourInfo_BF().GetTourVenue(tourID);
            tourVenue.ForEach(x =>
            {
                var e = venueList.Find(v => v.Name == x.Name);
                if (e != null) venueList.Remove(e);
            });
            venueList.ForEach(x =>
            {
                var a = new DAL.Pro_TourVenue();
                a.Name = x.Name;
                a.MeetTime = x.MeetTime;
                a.PickAmt = x.PickAmt;
                a.SendAmt = x.SendAmt;
                a.Departure = x.Departure;
                a.DepartureID = x.DepartureID;
                tourVenue.Add(a);
            });
            rptVenue.DataSource = tourVenue;
            rptVenue.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "routeinfo";
            }
        }

        protected void rptPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblSeat = e.Item.FindControl("lblSeat") as Literal;
                var lblChild = e.Item.FindControl("lblChild") as Literal;
                var lblDefault = e.Item.FindControl("lblDefault") as Literal;
                var isSeat = DataBinder.Eval(e.Item.DataItem, "IsSeat").ToString().ToBoolen();
                var isChild = DataBinder.Eval(e.Item.DataItem, "IsChild").ToString().ToBoolen();
                var isDefault = DataBinder.Eval(e.Item.DataItem, "IsDefault").ToString().ToBoolen();
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                lblSeat.Text = string.Format("<input type=\"checkbox\" {0} />", isSeat ? "checked=\"checked\"" : "");
                lblChild.Text = string.Format("<input type=\"checkbox\" {0} />", isChild ? "checked=\"checked\"" : "");
                lblDefault.Text = string.Format("<input name='p' type=\"radio\" {0} />", isDefault ? "checked=\"checked\"" : "");
            }
        }
    }
}