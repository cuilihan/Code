using DRP.BF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.BF.Order;

namespace DRP.WEB.Module.Fin
{
    public partial class OrderDate : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var orderType = Request["orderType"].ToInt();
            if (orderType == (int)OrderType.AirTicket)
            {
                var tOrder = new TicketOrder_BF().Get(KeyID);
                if (tOrder != null)
                {
                    OrderName.Text = tOrder.OrderName;
                    TourDate.Text = tOrder.TourDate.ToString("yyyy-MM-dd");
                    CreateDate.Text = tOrder.CreateDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                var order = new Order_BF().GetOrderInfo(KeyID);
                if (order != null)
                {
                    OrderName.Text = order.OrderName;
                    TourDate.Text = order.TourDate.ToString("yyyy-MM-dd");
                    CreateDate.Text = order.CreateDate.ToString("yyyy-MM-dd");
                }
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var orderType = Request["orderType"].ToInt();
            if (orderType == (int)OrderType.AirTicket)
            {
                var isOk=new TicketOrder_BF().UpdateOrderDate(KeyID, DateTime.Parse(TourDate.Text), DateTime.Parse(CreateDate.Text));
                JScript.CloseDialogWin(isOk ? "更新成功" : "更新失败");
            }
            else
            {
                var isOk = new Order_BF().UpdateOrderDate(KeyID, DateTime.Parse(TourDate.Text), DateTime.Parse(CreateDate.Text));
                JScript.CloseDialogWin(isOk ? "更新成功" : "更新失败");
            }
        }
    }
}