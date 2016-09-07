using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.Framework;

namespace DRP.WEB.Module.Order
{
    public partial class BudgetBill : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var xType = Request["xType"].ToInt();
            if (xType == (int)OrderType.QYT)
            {
                var order = new Order_BF().GetOrderInfo(KeyID);
                if (order == null) return;
                OrderName.Text = order.OrderName;
                TourNo.Text = order.OrderNo;
                Sales.Text = order.CreateUserName; 
            }
            else
            {
                var tour = new TourInfo_BF().Get(KeyID);
                if (tour == null) return;
                OrderName.Text = tour.TourName;
                TourNo.Text = tour.TourNo;
                Sales.Text = tour.CreateUserName;
            }
            printDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            UserName.Text = AuthenticationPage.UserInfo.UserName;
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