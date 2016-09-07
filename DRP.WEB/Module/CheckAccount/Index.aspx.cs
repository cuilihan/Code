using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF.Order;
using DRP.Framework;
using DRP.BF.ProMrg;
using DRP.BF.CheckAccount;

namespace DRP.WEB.Module.CheckAccount
{
    public partial class Index : System.Web.UI.Page
    {
        CheckAccount_BF dal = new CheckAccount_BF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var keyID = Request["id"];
            var orderType = (OrderType)(Request["xType"].ToInt());
            CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            var e = dal.GetOrderGuide(keyID);
            if (e == null) return;
            //是否已报账
            btnSave.Visible = e.IsOver == null ? true : (e.IsOver < 2);

            HasUpLoadData.Value = e.IsOver == null ? "0" : (((int)e.IsOver).ToString());
            GuideDrawAmount.Value = dal.GuideDrawMoney(e.OrderID).ToString();
            GuideName.Text = e.GuideName;
            GuidePhone.Text = e.Mobile;
            OrderID.Value = e.OrderID;
            OrderGuideID.Value = e.ID;

            if (OrderType.QYT == orderType)
            {
                var order = new Order_BF().GetOrderInfo(e.OrderID);
                if (order == null)
                    return;
                OrderName.Text = order.OrderName;
                TourDate.Text = order.TourDate.ToString("yyyy-MM-dd");
            }
            else
            {
                var tour = new TourInfo_BF().Get(e.OrderID);
                if (tour == null)
                    return;
                OrderName.Text = tour.TourName;
                TourDate.Text = tour.TourDate.ToString("yyyy-MM-dd");
            }
            Page.Title = OrderName.Text + "-导游报账";
        }
    }
}