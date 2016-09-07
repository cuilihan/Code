using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;

namespace DRP.WEB.Module.Order.Service
{
    /// <summary>
    /// 发票管理
    /// </summary>
    public class OrderInvoice : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://提交发票申请 
                    SaveInvoice(context);
                    break;
            }
        }

        #region 发票申请

        /// <summary>
        /// 提交发票申请
        /// </summary>
        /// <param name="context"></param>
        private void SaveInvoice(HttpContext context)
        {
            var e = new DAL.Ord_OrderInvoice();
            e.ID = Guid.NewGuid().ToString();
            e.InvoiceName = context.Request["InvoiceName"];
            e.InvoiceUnit = context.Request["InvoiceUnit"];
            e.InvoiceItem = context.Request["InvoiceItem"];
            e.FetchType = context.Request["FetchType"];
            e.Comment = context.Request["Comment"];
            e.IsOverAmt = context.Request["IsOver"].ToBoolen();
            var xml = context.Request["XmlItem"];
            if (string.IsNullOrEmpty(xml))
                return;

            var list = new List<OrderInvoiceItem>();
            var doc = new XmlDocument();
            var xType = (OrderType)context.Request["xType"].ToInt();
            doc.LoadXml(xml);
            var nodes = doc.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var a = new OrderInvoiceItem();
                a.OrderID = node.GetNodeValue("orderID");
                a.InvoiceAmt = node.GetNodeValue("invoiceAmt").ToDecimal();
                list.Add(a);
            }
            var isOk = new OrderInvoice_BF().SaveInvoice((OrderType)xType, e, list);
            context.Response.Write(isOk ? "1" : "0");
        }

        #endregion

        protected override string GetNavigateID(HttpContext context)
        {
            var xType = (OrderType)context.Request["xType"].ToInt();
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
}