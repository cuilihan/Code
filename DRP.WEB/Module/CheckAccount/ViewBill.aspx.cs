using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF.CheckAccount;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.Framework;
using DRP.Message.Core;

namespace DRP.WEB.Module.CheckAccount
{
    public partial class ViewBill : System.Web.UI.Page
    {
        CheckAccount_BF dal = new CheckAccount_BF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                if (!string.IsNullOrEmpty(Request["mid"]))
                {
                    new MessageBiz().SetMessageStatus(Request["mid"]);
                }
            }
        }

        private void BindData()
        {
            var keyID = Request["id"];
            var orderType = (OrderType)(Request["xType"].ToInt());
           
            var e = dal.GetOrderGuide(keyID);
            if (e == null) return;
            switch (e.IsOver)
            {
                case 0: lblStatus.Text = "未报账"; break;
                case 1: lblStatus.Text = "已报账"; break;
                case 2: lblStatus.Text = "已确认"; break;
            }

            GuideName.Text = e.GuideName;
            GuidePhone.Text = e.Mobile;
            CreateDate.Text =((DateTime)e.CreateDate).ToString("yyyy-MM-dd");

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