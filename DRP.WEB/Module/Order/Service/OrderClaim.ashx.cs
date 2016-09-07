using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;


namespace DRP.WEB.Module.Order.Service
{
    /// <summary>
    /// 订单收款认领
    /// </summary>
    public class OrderClaim : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询订单
                    OrderQuery(context);
                    break;
                case 2://检查收款项目状态，确认没有被其他从认领
                    CheckCollectedItemStatus(context);
                    break;
                case 3://保存订单收款认领
                    SaveClaim(context);
                    break;
            }
        }

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
            qry.OrdType = (OrderType)context.Request["OrderType"].ToInt();
            qry.CusotmerName = context.Request["Customer"];
            qry.DateType = context.Request["DateType"].ToInt();
            qry.OrderAmt = context.Request["OrderAmt"].ToInt();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            qry.OrderClaimQry = true;

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;

            var dt = new Order_BF().QueryOrder(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 检查收款项目状态，确认没有被其他从认领
        /// </summary>
        /// <param name="context"></param>
        private void CheckCollectedItemStatus(HttpContext context)
        {
            var claimID = context.Request["claimID"];
            var isOk = new CollectedClaim_BF().CheckCollectedNoneClaimed(claimID);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 保存订单收款认领
        /// </summary>
        /// <param name="context"></param>
        private void SaveClaim(HttpContext context)
        { 
            var claimID = context.Request["ClaimID"];
            var orderID = context.Request["OrderID"];
            var billNo = context.Request["BillNo"];
            var comment = context.Request["Comment"];
            var orderType = (OrderType)context.Request["OrderType"].ToInt();
            var isOk = new CollectedClaim_BF().SaveClaim(claimID, orderID, billNo, comment, orderType);
            context.Response.Write(isOk ? "1" : "0"); 
        }

        protected override string NavigateID
        {
            get
            {
                return "ordcollectedclaim";
            }
        }
    }
}