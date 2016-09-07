using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.Fin;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderInfo : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询订单
                    OrderQuery(context);
                    break;
                case 2://订单收款确认（按订单确认）
                    OrderCollectedConfired(context);
                    break;
                case 3://订单决算
                    SaveOrderBudget(context);
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
            qry.OrdType = (OrderType)context.Request["xType"].ToInt();
            qry.CusotmerName = context.Request["Customer"];
            qry.Supplier = context.Request["Supplier"];
            qry.DateType = context.Request["DateType"].ToInt();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            qry.OrdStatus = (OrderStatus)context.Request["Status"].ToInt();
            // isQueryCanceledOrder

            qry.PartStatus = context.Request["PartStatus"];

            if (qry.PartStatus == "1")
            {
                qry.PartDeptID = context.Request["DeptID"];
                qry.ParticipantID = context.Request["UserID"];
            }
            else
            {
                qry.DeptID = context.Request["DeptID"];
                qry.CreateUserID = context.Request["UserID"];
            }

            qry.RouteTypeID = context.Request["RouteTypeID"];
            qry.DestinationID = context.Request["DestinationID"];
            qry.DeptID = context.Request["DeptID"];
            qry.CreateUserID = context.Request["UserID"];
            qry.OrderSourceName = context.Request["OrderSourceID"];
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
            var dt = new Order_BF().QueryOrder(qry, out total);
            var t = new Order_BF().QueryOrder_Sum(qry).Rows[0];
            var json = ConvertJson.ToJson(dt);
            var s = "";
            if (qry.OrdType == (OrderType)5)
            {
                s = "{\"total\":" + total + ",\"rows\":" + json + ",\"footer\":[{\"OrderAmt\":" + t["OrderAmt"] + ",\"CollectedAmt\":" + t["CollectedAmt"] + ",\"OrderCost\":" + t["OrderCost"] + ",\"ToConfirmCollectedAmt\":" + t["ToConfirmCollectedAmt"] + ",\"CostInvoiceAmt\":" + t["CostInvoiceAmt"] + ",\"tDate\":\"合计:\"}]}";
            }
            else
            {
                s = "{\"total\":" + total + ",\"rows\":" + json + ",\"footer\":[{\"AdultNum\":" + t["AdultNum"] + ",\"ChildNum\":" + t["ChildNum"] + ",\"OrderAmt\":" + t["OrderAmt"] + ",\"CollectedAmt\":" + t["CollectedAmt"] + ",\"OrderCost\":" + t["OrderCost"] + ",\"ToConfirmCollectedAmt\":" + t["ToConfirmCollectedAmt"] + ",\"CostInvoiceAmt\":" + t["CostInvoiceAmt"] + ",\"tDate\":\"合计:\"}]}";
            }
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


        /// <summary>
        /// 保存订单的预决算
        /// </summary>
        /// <param name="context"></param>
        private void SaveOrderBudget(HttpContext context)
        {
            var orderID = context.Request["orderID"];
            var strCostItem = context.Request["CostItem"];
            var strComment = context.Request["Comment"];
            var dataStatus = context.Request["DataStatus"].ToInt();//1:预算 2：决算
            var adultNum = context.Request["AdultNum"].ToInt();
            var childNum = context.Request["ChildNum"].ToInt();
            var orderAmt = context.Request["OrderAmt"].ToDecimal();
            var drawMoney = context.Request["DrawMoney"].ToDecimal();
            var drawMoneyMethod = context.Request["DrawMoneyMethod"];
            var drawMoneyComment = context.Request["DrawMoneyComment"];
            var orderType = (OrderType)context.Request["xType"].ToInt();
            var budgetStatus = context.Request["BudgetStatus"].ToInt();
            var finalComment = context.Request["Comment"];
            var fileID = context.Request["FileID"];
            var isOk = new OrderBudget_BF().SaveBudget(orderType, orderID, adultNum, childNum, orderAmt,
                drawMoney, drawMoneyMethod, drawMoneyComment, strCostItem, strComment, dataStatus, budgetStatus, finalComment, fileID);
            context.Response.Write(isOk ? "1" : "0");
        }


        protected override string GetNavigateID(HttpContext context)
        {
            var xType = (OrderType)context.Request["xType"].ToInt();
            var pageID = "";
            switch (xType)
            {
                case OrderType.THSK:
                    pageID = "finorderthsk";
                    break;
                case OrderType.ZZBSK:
                    pageID = "finorderzzb";
                    break;
                case OrderType.QYT:
                    pageID = "finteamorder";
                    break;
                case OrderType.ZZBT:
                    pageID = "finorderzzbt";
                    break;
                case OrderType.DXYW:
                    pageID = "finorderbiz";
                    break;
            }
            return pageID;
        }
    }
}