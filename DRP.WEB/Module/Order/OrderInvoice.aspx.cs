using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.Framework;

namespace DRP.WEB.Module.Order
{
    public partial class OrderInvoice : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindParameter();
                BindData();
            }
        }

        protected override string NavigateID
        {
            get
            {
                var xType = (OrderType)Request["xType"].ToInt();
                var pageID = "";
                switch (xType)
                {
                    case OrderType.THSK:
                        pageID = "salesordersk";
                        break;
                    case OrderType.ZZBSK:
                        pageID = "salesorderown";
                        break;
                    case OrderType.QYT:
                        pageID = "salesorderqy";
                        break;
                    case OrderType.ZZBT:
                        pageID = "salesorderzzb";
                        break;
                    case OrderType.DXYW:
                        pageID = "salesbizorder";
                        break;
                    case OrderType.AirTicket:
                        pageID = "ticketorder";
                        break;
                }
                return pageID;
            }
        }

        private void BindParameter()
        {
            var dal = new BasicInfo_BF();
            InvoiceItem.DataSource = dal.GetBasicInfo(BasicType.Fin_InvoiceItem);
            InvoiceItem.DataTextField = InvoiceItem.DataValueField = "Name";
            InvoiceItem.DataBind();

            FetchType.DataSource = dal.GetBasicInfo(BasicType.Fin_InvoiceFetchType);
            FetchType.DataTextField = FetchType.DataValueField = "Name";
            FetchType.DataBind();

            InvoiceUnit.Text = AuthenticationPage.UserInfo.OrgName;            
        }

        private void BindData()
        {
            var orderID = Request["id"];
            if (string.IsNullOrEmpty(orderID)) return;
            var dt = new OrderInvoice_BF().QueryOrder(orderID);
            rptData.DataSource = dt;
            rptData.DataBind();

            if (dt.Rows.Count > 0)
            {
                InvoiceName.Text = dt.Rows[0]["CustomerName"].ToString();
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblInput = e.Item.FindControl("lblInput") as Literal;
                var orderAmt = DataBinder.Eval(e.Item.DataItem, "OrderAmt").ToString().ToDecimal();
                var invoceAmt = DataBinder.Eval(e.Item.DataItem, "OrderInvoiceAmt").ToString().ToDecimal();
                var a = orderAmt - invoceAmt;
                lblInput.Text = "<input type='text' class='textbox checkInt' style='width:90px; height:26px;text-align:right;' value='" + a + "' />";
            }
        }
    }
}