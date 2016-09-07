using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.Framework;

namespace DRP.WEB.Module.Order
{
    public partial class InvoiceInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OrderInvoice_BF().GetInvoice(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                InvoiceStatus.Text = OrderInvoice_BF.ConvertInvoiceStatus((InvoiceStatus)e.InvoiceStatus);
                BindOrder(e.ID);
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

        /// <summary>
        /// 绑定订单开票的订单
        /// </summary>
        /// <param name="invoiceID"></param>
        private void BindOrder(string invoiceID)
        {
            rptData.DataSource = new OrderInvoice_BF().QueryInvoiceOrder(invoiceID);
            rptData.DataBind();
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblOrderType = e.Item.FindControl("lblOrderType") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                lblOrderType.Text = Order_BF.ConvertToOrderType(DataBinder.Eval(e.Item.DataItem, "OrderType").ToString().ToInt());
            }
        }
    }
}