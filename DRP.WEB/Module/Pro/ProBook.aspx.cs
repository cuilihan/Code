using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.ProMrg;
using DRP.Framework.Core;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Pro
{
    public partial class ProBook : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var tour = new TourInfo_BF().Get(KeyID);
            var route = new RouteInfo_BF().Get(tour.RouteID);
            if (!string.IsNullOrEmpty(route.Remind)) //显示特别提醒
            {
                JScript.Alert(HtmlHeler.NoHTML(route.Remind)); 
            }
            TourNo.Text = tour.TourNo;
            TourName.Text = tour.TourName;
            TourDate.Text = tour.TourDate.ToString("yyyy-MM-dd");
            ReturnDate.Text = tour.ReturnDate.ToString("yyyy-MM-dd");
            Destination.Text = route.Destination;

            if (route.RouteType.Contains("短线") && tour.SeatNum > 0)//座位数
                hfSeatNum.Value = tour.SeatNum.ToString();

            BindDeparture();
            BindOrderSource();
            BindPricePolicy(tour.ID);

            //订单状态是否需要审核确认
            var orgSetting = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.OrderAudit);
            if (orgSetting != null)
            {
                hfOrderStatus.Value = orgSetting.xVal == 1 ? "1" : "2";
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "productbook";
            }
        }

        #region << 订单来源 >>
        private void BindOrderSource()
        {
            OrderSource.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_OrderSource);
            OrderSource.DataTextField = "Name";
            OrderSource.DataValueField="ID";
            OrderSource.DataBind();
        }
        #endregion

        #region << 出发地 >>
        private void BindDeparture()
        {
            var list= new TourInfo_BF().GetTourVenue(KeyID);
            //去除重复
            var coll = new List<DAL.Pro_TourVenue>();
            list.ForEach(x => 
            {
                if (!coll.Exists(a => a.DepartureID == x.DepartureID))
                    coll.Add(x);
            });
            Departure.DataSource = coll;
            Departure.DataTextField = "Departure";
            Departure.DataValueField = "DepartureID";
            Departure.DataBind();
        }
        #endregion

        #region << 价格策略 >>
        private void BindPricePolicy(string tourID)
        {
            rptPrice.DataSource = new TourInfo_BF().GetTourPrice(tourID);
            rptPrice.DataBind();
        }

        protected void rptPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        #endregion
    }
}