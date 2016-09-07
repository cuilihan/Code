using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.BF.Order;

namespace DRP.WEB.Module.Res
{
    public partial class HotelEdit : Pagebase
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
            StarLv.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Res_HotelStar);
            StarLv.DataTextField = StarLv.DataValueField = "Name";
            StarLv.DataBind();

            var e = new Hotel_BF().Get(KeyID);
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
                return "hotel";
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