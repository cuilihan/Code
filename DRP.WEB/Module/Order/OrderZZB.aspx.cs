using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Order
{
    public partial class OrderZZB : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFile(KeyID);
                BindOrderSource();
                BindData();
                var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (en != null && en.xType == 5 && en.xVal == 1)
                {
                    Participant.Style.Value = "";
                }
            }
        }

        private void BindData()
        {
            var order = new Order_BF().GetOrderInfo(KeyID);
            if (order == null) return;

            var dal = new Order_BF();
            var e = dal.GetOrderInfo(KeyID);
            Employee.Value = e.Participant;
            Dept.Value = e.DeptName;
            hfDept.Value = e.PartDeptID;
            hfEmployee.Value = e.ParticipantID;

            BindDeparture(order.TourID);
            OrderNo.Text = order.OrderNo;
            OrderName.Text = order.OrderName;
            TourDate.Text = order.TourDate.ToString("yyyy-MM-dd");
            ReturnDate.Text = ((DateTime)order.ReturnDate).ToString("yyyy-MM-dd");
            OrderSource.SelectedValue = order.SourceID;
            Comment.Text = order.Remark;
            lblBusAmt.Text = (order.PickAmt + order.SendAmt).ToString();
            AdjustAmt.Text = order.AdjustAmt.ToString();
            TourID.Value = order.TourID;
            hfOrderStatus.Value = order.OrderStatus.ToString();
            this.VenueName.Value = order.VenueName;
            this.CollectTime.Value = order.CollectTime;
            this.PickAmt.Value = order.PickAmt.ToString();
            this.SendAmt.Value = order.SendAmt.ToString();

            var tour = new TourInfo_BF().Get(order.TourID);
            var route = new RouteInfo_BF().Get(tour.RouteID);
            if (route != null)
            {
                if (route.RouteType.Contains("短线") && tour.SeatNum > 0)//座位数
                    hfSeatNum.Value = tour.SeatNum.ToString();
            }


            rptPrice.DataSource = new Order_BF().GetOrderPriceForEdit(order.ID, order.TourID);
            rptPrice.DataBind();

            rptCustomer.DataSource = new Order_BF().GetOrderCustomer(order.ID);
            rptCustomer.DataBind();

        }


        #region << 订单来源 >>
        private void BindOrderSource()
        {
            OrderSource.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_OrderSource);
            OrderSource.DataTextField = "Name";
            OrderSource.DataValueField = "ID";
            OrderSource.DataBind();
        }
        #endregion

        #region << 出发地 >>
        private void BindDeparture(string tourID)
        {
            var list = new TourInfo_BF().GetTourVenue(tourID);
            //去除重复
            var coll = new List<DAL.Pro_TourVenue>();
            list.ForEach(x =>
            {
                if (!coll.Exists(a => a.DepartureID == x.DepartureID))
                    coll.Add(x);
            });
        }
        #endregion


        protected void rptPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }




        protected override string NavigateID
        {
            get
            {
                return "salesorderown";
            }
        }

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblSex = e.Item.FindControl("lblSex") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                var sex = DataBinder.Eval(e.Item.DataItem, "Sex").ToString();
                lblSex.Text = "<select style='width: 50px; height: 26px;'>";
                if (sex == "男") lblSex.Text += "<option value=\"男\" selected='selected'>男</option><option value=\"女\">女</option>";
                else lblSex.Text += "<option value=\"男\">男</option><option value=\"女\" selected='selected'>女</option>";
                lblSex.Text += "</select>";
            }
        }

        private void BindFile(string orderID)
        {
            rptFile.DataSource = new Order_BF().GetOrderFile(orderID);
            rptFile.DataBind();
        }
    }
}