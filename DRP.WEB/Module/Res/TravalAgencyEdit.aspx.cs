using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.ResMrg;
using DRP.BF.Order;

namespace DRP.WEB.Module.Res
{
    public partial class TravalAgencyEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFile(KeyID);
                BindData();
            }
        }

        private void BindData()
        {
            var e = new TravelAgency_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                hfRouteTypeID.Value = e.RouteTypeID;
                hfDestinationID.Value = e.DestinationID;
                rptData.DataSource = new ResourceUtility().GetBizContact(e.ID);
                rptData.DataBind();
            }
        }
        private void BindFile(string orderID)
        {
            rptFile.DataSource = new Order_BF().GetOrderFile(orderID);
            rptFile.DataBind();
        }


        protected override string NavigateID
        {
            get
            {
                return "travelagency";
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }
    }
}