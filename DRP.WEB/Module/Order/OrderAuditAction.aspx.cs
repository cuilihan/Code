using DRP.BF;
using DRP.BF.Order;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Order
{
    public partial class OrderAuditAction : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var order = new Order_BF().GetOrderInfo(KeyID);
            OrderName.Text = order.OrderName;
            var s = "";
            switch (order.OrderStatus)
            {
                case 1: s = "待确认"; break;
                case 2: s = "已确认"; break;
                case 3: s = "已完成"; break;
                case 4: s = "已取消"; break;
            }
            OrderStatus.Text = s;
            AuditComment.Text = order.AuditRemark;
            btnSave.Visible = order.OrderStatus == 1;
        }

        protected override string NavigateID
        {
            get
            {
                return "zzborderaudit";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk=new Order_BF().OrderAudit(KeyID, AuditComment.Text);
            JScript.CloseDialogWin(isOk ? "确认成功" : "确认失败");
        }
    }
}