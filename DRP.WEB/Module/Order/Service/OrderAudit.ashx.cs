using DRP.BF.PageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.Order;
using DRP.BF;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Order.Service
{
    /// <summary>
    /// 自主班订单审核
    /// </summary>
    public class OrderAudit : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://查询待审核的自主班散客订单
                    OrderQuery(context);
                    break;
                case 2://取消订单
                    OrderCancel(context);
                    break;
            }
        }


        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="context"></param>
        private void OrderQuery(HttpContext context)
        {
            var qry = new OrderCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderName = context.Request["OrderName"];
            qry.OrderNo = context.Request["OrderNo"];
            qry.OrdType = OrderType.ZZBSK;
            qry.CusotmerName = context.Request["Customer"];
            qry.Supplier = context.Request["Supplier"];
            qry.DateType = context.Request["DateType"].ToInt();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            qry.OrdStatus = OrderStatus.ToConfirm;           
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Order_BF().QueryOrder(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="context"></param>
        private void OrderCancel(HttpContext context)
        {
            var orderID = context.Request["orderID"];
            var isOk = new Order_BF().OrderCanceled(orderID);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "zzborderaudit";
            }
        }
    }
}