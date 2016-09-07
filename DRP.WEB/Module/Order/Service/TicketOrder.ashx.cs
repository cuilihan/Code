using DRP.BF.PageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.Order;
using DRP.Framework.Core;
using DRP.BF.Crm;
using System.Xml;

namespace DRP.WEB.Module.Order.Service
{
    /// <summary>
    /// 机票订单
    /// </summary>
    public class TicketOrder : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询订单
                    QueryOrder(context);
                    break;
                case 2://查询客户的证件号码
                    QueryCustomerCertificate(context);
                    break;
                case 3://保存机票订单
                    SaveOrder(context);
                    break;
                case 4: //取消订单
                    OrderCancel(context);
                    break;
            }
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrder(HttpContext context)
        {
            var qry = new TicketOrderCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderName = context.Request["OrderName"];
            qry.PNR = context.Request["PNR"];
            qry.Contact = context.Request["Contact"];
            qry.Supplier = context.Request["Supplier"];
            qry.sDate = context.Request["sDate"];
            qry.eDate = context.Request["eDate"];
            qry.csDate = context.Request["csDate"];
            qry.ceDate = context.Request["ceDate"];
            qry.OrdStatus = (OrderStatus)context.Request["Status"].ToInt();
            qry.FlightLeg = context.Request["FlightLeg"];            
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            qry.Company = context.Request["Company"];
            qry.UpdateUserName = context.Request["UpdateUserName"];
            qry.sUnCollectedAmt = context.Request["sUnCollectedAmt"];
            qry.eUnCollectedAmt = context.Request["eUnCollectedAmt"];

            var total = 0;
            var dt = new TicketOrder_BF().QueryOrder(qry, out total);
            var t = new TicketOrder_BF().QueryOrder_Sum(qry).Rows[0];
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + ",\"footer\":[{\"AdultNum\":" + t["AdultNum"] + ",\"OrderAmt\":" + t["OrderAmt"] + ",\"CollectedAmt\":" + t["CollectedAmt"] + ",\"ToConfirmCollectedAmt\":" + t["ToConfirmCollectedAmt"] + ",\"OrderCost\":" + t["OrderCost"] + ",\"CostInvoiceAmt\":" + t["CostInvoiceAmt"] + ",\"tDate\":\"合计:\"}]}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 客户的证件号码
        /// </summary>
        /// <param name="context"></param>
        private void QueryCustomerCertificate(HttpContext context)
        {
            var customerID = context.Request["cid"];
            var itemType = HttpUtility.HtmlDecode(context.Request["t"]);
            var e = new Customer_BF().FindCustomerCertificate(customerID, itemType);
            context.Response.Write(e == null ? " " : e.ItemVal);
        }

        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="context"></param>
        private void SaveOrder(HttpContext context)
        {
            var e = new DAL.Ord_TicketOrder();
            e.ID = context.Request["ID"];
            e.ID = string.IsNullOrEmpty(e.ID) ? Guid.NewGuid().ToString() : e.ID;
            e.OrderName = context.Request["OrderName"];
            e.TicketType = context.Request["TicketType"];
            e.PNR = context.Request["PNR"];
            e.TourDate = (DateTime)context.Request["TourDate"].ToDate();
            var strDate = context.Request["ReturnDate"];
            e.ReturnDate = string.IsNullOrEmpty(strDate) ? (DateTime?)null : Convert.ToDateTime(strDate);
            e.AdultNum = context.Request["AdultNum"].ToInt();
            e.ChildNum = 0;
            e.Contact = context.Request["Contact"]; 
            e.ContactPhone = context.Request["ContactPhone"];
            //e.SupplierName = context.Request["SupplierName"];
            //e.SupplierID = context.Request["SupplierID"];
            e.Remark = context.Request["Remark"];
            //e.OrderCost = context.Request["OrderCost"].ToDecimal();
            e.OrderAmt = context.Request["OrderAmt"].ToDecimal();
            e.OrderStatus = (int)OrderStatus.Confirmed;
            //e.OrderCostID = string.IsNullOrEmpty(context.Request["OrderCostID"]) ? Guid.NewGuid().ToString() : context.Request["OrderCostID"];
            var strFlightInfo = context.Request["FlightInfo"];
            var strCustomerInfo = context.Request["CustomerInfo"];
            e.Participant = context.Request["Participant"];
            e.DeptName = context.Request["DeptName"];
            e.ParticipantID = context.Request["ParticipantID"];
            e.PartDeptID = context.Request["PartDeptID"];
            e.Company = context.Request["Company"];
            var fileID = context.Request["FileID"];

            if (!string.IsNullOrEmpty(strFlightInfo))
            {
                var doc = new XmlDocument();
                doc.LoadXml(strFlightInfo);
                var node = doc.SelectSingleNode("document/to");
                if (node != null)
                { 
                    e.ToFlightLeg = node.GetNodeValue("flightleg");
                    e.ToFlightInfo = node.GetNodeValue("flight");
                    if (!string.IsNullOrEmpty(e.ToFlightInfo))
                        e.ToFlightInfo = e.ToFlightInfo.Replace("		", " ");
                    e.ToAirport = node.GetNodeValue("airport");
                    e.ToAirLine = node.GetNodeValue("airline");
                    e.ToCabin = node.GetNodeValue("cabin");
                    e.ToTicketPrice = node.GetNodeValue("price").ToDecimal();
                }
                node = doc.SelectSingleNode("document/from");
                if (node != null)
                {
                    e.FromFlightLeg = node.GetNodeValue("flightleg");
                    e.FromFlightInfo = node.GetNodeValue("flight");
                    if (!string.IsNullOrEmpty(e.FromFlightInfo))
                        e.FromFlightInfo = e.FromFlightInfo.Replace("		", " ");
                    e.FromAirport = node.GetNodeValue("airport");
                    e.FromAirLine = node.GetNodeValue("airline");
                    e.FromCabin = node.GetNodeValue("cabin");
                    e.FromTicketPrice = node.GetNodeValue("price").ToDecimal();
                }
            }
            var strCostInfo = context.Request["CostInfo"];
            var isOk = new TicketOrder_BF().SaveOrder(e, strCustomerInfo, strCostInfo, fileID);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="context"></param>
        private void OrderCancel(HttpContext context)
        {
            var orderID = context.Request["orderID"];
            var isOk = new TicketOrder_BF().OrderCanceled(orderID);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "ticketorder";
            }
        }

    }
}