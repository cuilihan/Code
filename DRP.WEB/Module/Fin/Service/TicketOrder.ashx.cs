using DRP.BF.PageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.BF.Order;
using DRP.BF.Fin;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 机票订单管理
    /// </summary>
    public class TicketOrder : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://查询订单
                    QueryOrder(context);
                    break;
                case 2://订单收款确认
                    OrderCollectedConfired(context);
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
            qry.OrdStatus = (OrderStatus)context.Request["Status"].ToInt();
            qry.FlightLeg = context.Request["FlightLeg"];
            qry.csDate = context.Request["csDate"];
            qry.ceDate = context.Request["ceDate"];
            qry.Company = context.Request["Company"];
            qry.UpdateUserName = context.Request["UpdateUserName"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
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
        /// 订单收款确认
        /// </summary>
        /// <param name="context"></param>
        private void OrderCollectedConfired(HttpContext context)
        {
            var ids = context.Request["id"];
            if (string.IsNullOrEmpty(ids))
            {
                context.Response.Write("");
                return;
            }
            try
            {
                new CollectedItem_BF().CollectedConfirmed(ids.Split(','));
                context.Response.Write("1");
            }
            catch
            {
                context.Response.Write("");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "finticketorder";
            }
        }
        
    }
}