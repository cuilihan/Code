using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DRP.BF;
using DRP.BF.CheckAccount;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.BF.ProMrg;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.Framework.Core;
using System.Xml;

namespace DRP.WEB.Module.Order.Service
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
                case 2://保存同行散客订单
                    SaveOrderTHSK(context);
                    break;
                case 3://短线座位表
                    SeatChartDrawing(context);
                    break;
                case 4://自主班预订上车地点
                    TourVenue(context);
                    break;
                case 5://自主班散客订单保存
                    SaveOrderZZBSK(context);
                    break;
                case 6://保存单项业务订单
                    SaveOrderBiz(context);
                    break;
                case 7://保存企业团订单
                    SaveOrderQYT(context);
                    break;
                case 10://取消订单
                    OrderCancel(context);
                    break;
                case 11://提交订单预算
                    SaveOrderBudget(context);
                    break;
                case 12://团队订单的余款结清
                    OrderCollectionClosed(context);
                    break;
                case 13://自主班团队订单列表
                    TourOrderList(context);
                    break;
                case 14://团队订单安排导游
                    SaveOrderGuide(context);
                    break;
                case 15://预决算单据
                    PrintOrderbudget(context);
                    break;
                case 16://导游报账单确认
                    OrderCheckAccount(context);
                    break;
                case 17://判断报名人数是否超过计划人数
                    OrderNumIsOverPlanNum(context);
                    break;
                case 18://更新订单状态
                    ChageOrderStatus(context);
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

            qry.RouteTypeID = context.Request["RouteTypeID"];
            qry.DestinationID = context.Request["DestinationID"];
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

        #region 同行散客订单

        /// <summary>
        /// 保存同行散客订单
        /// </summary>
        /// <param name="context"></param>
        private void SaveOrderTHSK(HttpContext context)
        {
            var e = new DAL.Ord_OrderInfo();
            e.ID = context.Request["ID"];
            e.ID = string.IsNullOrEmpty(e.ID) ? Guid.NewGuid().ToString() : e.ID;
            e.OrderType = context.Request["OrderType"].ToInt();
            e.RouteTypeID = context.Request["RouteTypeID"];
            var p = new BasicInfo_BF();
            var rEntity = p.Get(e.RouteTypeID);
            e.RouteTypeName = rEntity == null ? "" : rEntity.Name;
            e.DestinationID = context.Request["DestinationID"];
            e.DestinationName = new Destination_BF().Get(e.DestinationID).Name;
            e.DestinationPath = new Destination_BF().GetDestinationPathID(e.DestinationID);
            e.OrderName = context.Request["OrderName"];
            e.TourDate = (DateTime)context.Request["TourDate"].ToDate();
            e.TourDays = context.Request["TourDays"].ToInt();
            e.SourceID = context.Request["SourceID"];
            e.SourceName = context.Request["SourceName"];
            var strCustomerInfo = context.Request["CustomerInfo"];
            var strCostInfo = context.Request["CostItem"];
            e.Remark = context.Request["Remark"];
            e.AdultNum = context.Request["AdultNum"].ToInt();
            e.ChildNum = context.Request["ChildNum"].ToInt();
            e.OrderAmt = context.Request["OrderAmt"].ToDecimal();
            e.OrderStatus = context.Request["OrderStatus"].ToInt();
            e.Participant = context.Request["Participant"];
            e.DeptName = context.Request["DeptName"];
            e.ParticipantID = context.Request["ParticipantID"];
            e.PartDeptID = context.Request["PartDeptID"];
            var fileID = context.Request["FileID"];

            var isOk = new Order_BF().SaveOrderTHSK(e, strCustomerInfo, strCostInfo, fileID);
            context.Response.Write(isOk ? "1" : "0");
        }

        #endregion

        #region 企业团订单

        /// <summary>
        /// 保存企业团订单
        /// </summary>
        /// <param name="context"></param>
        private void SaveOrderQYT(HttpContext context)
        {
            var e = new DAL.Ord_OrderInfo();
            e.ID = context.Request["ID"];
            e.ID = string.IsNullOrEmpty(e.ID) ? Guid.NewGuid().ToString() : e.ID;
            e.OrderType = context.Request["OrderType"].ToInt();
            e.RouteTypeID = context.Request["RouteTypeID"];
            var p = new BasicInfo_BF();
            var rEntity = p.Get(e.RouteTypeID);
            e.RouteTypeName = rEntity == null ? "" : rEntity.Name;
            e.DestinationID = context.Request["DestinationID"];
            e.DestinationName = new Destination_BF().Get(e.DestinationID).Name;
            e.DestinationPath = new Destination_BF().GetDestinationPathID(e.DestinationID);
            e.OrderName = context.Request["OrderName"];
            e.TourDate = (DateTime)context.Request["TourDate"].ToDate();
            e.TourDays = context.Request["TourDays"].ToInt();
            e.SourceID = context.Request["SourceID"];
            e.SourceName = context.Request["SourceName"];
            e.VenueName = context.Request["VenueName"];
            e.CollectTime = context.Request["CollectTime"];
            e.Schedule = context.Request["Schedule"];
            e.Participant = context.Request["Participant"];
            e.DeptName = context.Request["DeptName"];
            e.ParticipantID = context.Request["ParticipantID"];
            e.PartDeptID = context.Request["PartDeptID"];
            var fileID = context.Request["FileID"];

            var strCustomerInfo = context.Request["CustomerInfo"];
            e.Remark = context.Request["Remark"];
            e.OrderStatus = context.Request["OrderStatus"].ToInt();
            var isOk = new Order_BF().SaveOrderQYT(e, strCustomerInfo, fileID);
            context.Response.Write(isOk ? e.ID : "0");
        }

        #endregion

        #region 自主班散客订单


        #region << 绘制座位表 >>

        /// <summary>
        /// 短线座位表
        /// </summary>
        /// <param name="context"></param>
        private void SeatChartDrawing(HttpContext context)
        {
            var seatNum = context.Request["seatNum"].ToInt();
            var tourID = context.Request["tourID"];
            var orderID = context.Request["orderID"];
            var sb = new StringBuilder();
            var rows = seatNum % 4 == 0 ? seatNum / 4 : (seatNum / 4 + 1);
            var listOrderSeat = new Order_BF().GetTourOrderSeat(tourID);//已预订座位
            var listLockSeat = new TourInfo_BF().GetLockSeat(tourID);//预留座
            for (int i = 0; i < rows; i++)
            {
                var num = i * 4 + 1;
                sb.Append("<tr>");
                var a = SetSeatNo(seatNum, num);
                var j = a;
                var clsName = GetClassName(listOrderSeat, listLockSeat, num, orderID);
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td style='text-align:center;' class='{0}' tag='{2}'>{1}</td>", clsName, a, j);

                num++;

                a = SetSeatNo(seatNum, num);
                j = a;
                clsName = GetClassName(listOrderSeat, listLockSeat, num, orderID);
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td style='text-align:center;' class='{0}' tag='{2}'>{1}</td>", clsName, a, j);

                sb.Append("<td style='text-align:center;' class='empty'></td>");

                num++;
                a = SetSeatNo(seatNum, num);
                j = a;
                clsName = GetClassName(listOrderSeat, listLockSeat, num, orderID);
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td style='text-align:center;' class='{0}' tag='{2}'>{1}</td>", clsName, a, j);

                num++;
                a = SetSeatNo(seatNum, num);
                j = a;
                clsName = GetClassName(listOrderSeat, listLockSeat, num, orderID);
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td style='text-align:center;' class='{0}' tag='{2}'>{1}</td>", clsName, a, j);

                sb.Append("</tr>");
            }

            context.Response.Write(sb.ToString());
        }

        private string SetSeatNo(int seatNum, int n)
        {
            return n <= seatNum ? n.ToString() : "";
        }

        private string GetClassName(List<DAL.Ord_OrderSeat> list, List<DAL.Pro_TourSeatLock> lockSeat, int no, string orderID)
        {
            var e = list.Find(x => x.SeatNum == no);
            if (e == null)
            {
                var isLock = lockSeat.Exists(x => x.SeatNo == no);
                if (isLock) return "lock";
                else return "seat";
            }
            else
            {
                if (e.OrderID == orderID) return "bczt";
                else return "yzz";
            }
        }
        #endregion

        /// <summary>
        /// 判断报名人数是否超过计划人数
        /// </summary>
        /// <param name="context"></param>
        private void OrderNumIsOverPlanNum(HttpContext context)
        {
            var tourID = context.Request["tourID"];
            var vNum = context.Request["vNum"].ToInt(); //本次报名人数
            var n = new Order_BF().OrderVisitorNum(tourID);//已报名人数
            var planNum = new TourInfo_BF().Get(tourID).PlanNum;
            var s = planNum - n - vNum;
            context.Response.Write(s >= 0 ? "0" : "1");
        }

        /// <summary>
        /// 上车地点
        /// </summary>
        /// <param name="context"></param>
        private void TourVenue(HttpContext context)
        {
            var tourID = context.Request["tourID"];
            var departureID = context.Request["departureID"];
            if (!string.IsNullOrEmpty(departureID))
            {
                var list = new TourInfo_BF().GetTourVenue(tourID, departureID);
                context.Response.Write(ConvertJson.ListToJson(list));
            }
        }

        /// <summary>
        /// 保存自主班散客订单
        /// </summary>
        /// <param name="context"></param>
        private void SaveOrderZZBSK(HttpContext context)
        {
            var entity = new DAL.Ord_OrderInfo();
            entity.ID = string.IsNullOrEmpty(context.Request["ID"]) ? Guid.NewGuid().ToString() : context.Request["ID"];
            entity.TourID = context.Request["TourID"];
            entity.Departure = context.Request["DepartureName"];
            entity.DepartureID = context.Request["DepartureID"];
            entity.SourceID = context.Request["SourceID"];
            entity.SourceName = context.Request["SourceName"];
            entity.Remark = context.Request["Remark"];
            entity.AdjustAmt = context.Request["AdjustAmt"].ToDecimal();
            entity.OrderAmt = context.Request["OrderAmt"].ToDecimal();
            entity.OrderStatus = context.Request["OrderStatus"].ToInt();
            var strTourVenue = context.Request["TourVenue"];
            entity.Participant = context.Request["Participant"];
            entity.DeptName = context.Request["DeptName"];
            entity.ParticipantID = context.Request["ParticipantID"];
            entity.PartDeptID = context.Request["PartDeptID"];
            var fileID = context.Request["FileID"];

            if (!string.IsNullOrEmpty(strTourVenue))
            {
                var arrVenue = strTourVenue.Split(',');
                if (arrVenue.Length == 4)
                {
                    entity.VenueName = arrVenue[0];
                    entity.CollectTime = arrVenue[1];
                    entity.PickAmt = arrVenue[2].ToInt();
                    entity.SendAmt = arrVenue[3].ToInt();
                }
            }
            var xmlVisitor = context.Request["CustomerInfo"];
            var xmlPrice = context.Request["PriceInfo"];
            var strSeatNo = context.Request["SeatNo"];//座位号

            var isOk = new Order_BF().SaveOrderZZBSK(entity, xmlVisitor, xmlPrice, strSeatNo, fileID);
            context.Response.Write(isOk ? entity.ID.ToString() : "");
        }
        #endregion

        #region 单项业务订单
        /// <summary>
        /// 保存单项业务订单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void SaveOrderBiz(HttpContext context)
        {
            var e = new DAL.Ord_OrderInfo();
            e.ID = context.Request["ID"];
            e.ID = string.IsNullOrEmpty(e.ID) ? Guid.NewGuid().ToString() : e.ID;
            e.OrderType = (int)OrderType.DXYW;
            e.RouteTypeID = e.DestinationID = Guid.Empty.ToString();
            e.OrderName = context.Request["OrderName"];
            e.TourDate = (DateTime)context.Request["TourDate"].ToDate();
            e.TourDays = 0;
            e.SourceID = context.Request["SourceID"];
            e.SourceName = context.Request["SourceName"];
            var strCustomerInfo = context.Request["CustomerInfo"];
            var strCostInfo = context.Request["CostItem"];
            e.Remark = context.Request["Remark"];
            e.AdultNum = 0;
            e.ChildNum = 0;
            e.OrderAmt = context.Request["OrderAmt"].ToDecimal();
            e.OrderStatus = context.Request["OrderStatus"].ToInt();
            e.Participant = context.Request["Participant"];
            e.DeptName = context.Request["DeptName"];
            e.ParticipantID = context.Request["ParticipantID"];
            e.PartDeptID = context.Request["PartDeptID"];
            var fileID = context.Request["FileID"];

            var isOk = new Order_BF().SaveOrderBiz(e, strCustomerInfo, strCostInfo, fileID);
            context.Response.Write(isOk ? "1" : "0");
        }
        #endregion

        #region 订单操作

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="context"></param>
        private void ChageOrderStatus(HttpContext context)
        {
            var orderID = context.Request["orderID"];
            var orderStatus = context.Request["status"].ToInt();
            new Order_BF().UpdateOrderStatus(orderID, orderStatus);
            context.Response.Write("1");
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

        /// <summary>
        /// 安排导游
        /// </summary>
        /// <param name="context"></param>
        private void SaveOrderGuide(HttpContext context)
        {
            var orderID = context.Request["OrderID"];
            var xmlData = context.Request["xmlData"];
            var isOk = new OrderGuide_BF().Save(xmlData, orderID);
            context.Response.Write(isOk ? "1" : "0");
        }
        #endregion

        #region 订单预决算
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

        /// <summary>
        /// 预决算单据
        /// </summary>
        /// <param name="context"></param>
        private void PrintOrderbudget(HttpContext context)
        {
            var orderID = context.Request["ID"];
            var orderType = context.Request["xType"].ToInt();
            var sb = new StringBuilder();
            var dal = new OrderBudget_BF();
            var budget = dal.GetBudget(orderID, 1);//预决
            var final = dal.GetBudget(orderID, 2);//决算 
            List<DAL.Ord_OrderCostItem> budgetCost = new List<DAL.Ord_OrderCostItem>();//预算成本
            List<DAL.Ord_OrderCostItem> finalCost = new List<DAL.Ord_OrderCostItem>();//决算成本

            if (budget == null)
            {
                sb.Append("<tr><td>尚未做预决算</td></tr>");
            }
            else
            {
                budgetCost = dal.GetOrderCost(budget.ID);//预算成本
                if (final != null)
                    finalCost = dal.GetOrderCost(final.ID);//决算成本
                var maxRow = budgetCost.Count > finalCost.Count ? budgetCost.Count : finalCost.Count;
                decimal budgetReceivableAmt = budget.OrderAmt;//预算应收
                decimal finalReceivabelAmt = final == null ? 0 : final.OrderAmt;//决算应收
                decimal collectedAmt = 0; //已收 
                decimal tBudgetCost = 0;//预算成本合计
                decimal tFinalCost = 0;//决算成本合计

                #region 已收款
                if (orderType == (int)OrderType.QYT)
                {
                    var order = new Order_BF().GetOrderInfo(orderID);
                    if (order != null)
                    {
                        collectedAmt = order.CollectedAmt;
                    }
                }
                else
                {
                    var e = new Order_BF().GetOrderExtend(orderID);
                    if (e != null)
                    {
                        collectedAmt = new Order_BF().GetTourOrderAmt(orderID);
                    }
                }

                #endregion

                #region Table Header
                var sbComment = new StringBuilder();
                if (!string.IsNullOrEmpty(budget.Comment))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(budget.Comment);
                    var nodes = doc.SelectNodes("document/data");
                    sbComment.AppendFormat("<tr><td colspan='2' rowspan='{0}' style='text-align:center;font-weight: bold;'>备注</td><th>备注内容</th><th>类型</th><th colspan='2'>备注内容</th></tr>", nodes.Count + 2);
                    var i = 0;
                    foreach (XmlNode node in nodes)
                    {
                        sbComment.Append("<tr>");
                        sbComment.AppendFormat("<td>{1}</td><td>{0}</td>", XmlHelper.GetNodeValue(node, "name"), XmlHelper.GetNodeValue(node, "comment"));
                        if (i==0)
                        {
                            if (final == null)
                            {
                                sbComment.AppendFormat("<td colspan='2' rowspan='{0}'>{1}</td>", nodes.Count + 2, "");
                            }
                            else
                                sbComment.AppendFormat("<td colspan='2' rowspan='{0}'>{1}</td>", nodes.Count + 2, final.Comment);
                            i = 1;
                        }
                        sbComment.Append("</tr>");
                    }
                }
                else
                {
                    if (final != null)
                    {
                        if (!string.IsNullOrEmpty(final.Comment))
                        {
                            sbComment.Append("<tr><td colspan='2' rowspan='2' style='text-align:center;font-weight: bold;'>备注</td><th>备注内容</th><th>类型</th><th colspan='2'>备注内容</th></tr>");
                            sbComment.Append("<tr>");
                            sbComment.AppendFormat("<td></td><td></td><td colspan='2'>{0}</td>", final.Comment);
                            sbComment.Append("</tr>");
                        }
                    }
                }

                sb.Append("<tr>");
                sb.Append("<th colspan=\"2\">&nbsp;</th>");
                sb.Append("<th colspan=\"2\">预算</th>");
                sb.Append("<th colspan=\"2\">决算</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th colspan=\"2\">人数</th>");
                sb.AppendFormat("<td colspan=\"2\">成人：{0}<span style='padding-left:2em;'>儿童：</span>{1}</td>", budget.AdultNum, budget.ChildNum);
                sb.AppendFormat("<td colspan=\"2\">成人：{0}<span style='padding-left:2em;'>儿童：</span>{1}</td>",
                    (final == null ? "0" : final.AdultNum.ToString()), (final == null ? "0" : final.ChildNum.ToString()));
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th colspan=\"2\">收入</th>");
                sb.AppendFormat("<td colspan=\"2\">应收：{0}<br />已收：{1}<br />未收：{2}</td>",
                    budgetReceivableAmt, collectedAmt, budgetReceivableAmt - collectedAmt);
                var d = finalReceivabelAmt - collectedAmt;
                sb.AppendFormat("<td colspan=\"2\">应收：{0}<br />已收：{1}<br />未收：{2}</td>",
                    finalReceivabelAmt, collectedAmt, d > 0 ? d.ToString() : "0");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.AppendFormat("<th rowspan=\"{0}\" style=\"width: 20px; text-align: center; vertical-align: middle;\">成本</th>", maxRow + 2);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style=\"width: 70px;\">项目</th>");
                sb.Append("<th>供应商</th>");
                sb.Append("<th style=\"width: 50px;\">成本金额</th>");
                sb.Append("<th style=\"width: 250px;\">供应商</th>");
                sb.Append("<th style=\"width: 50px;\">成本金额</th>");
                sb.Append("</tr>");

                #endregion

                #region Table Body
                for (var i = 0; i < maxRow; i++)
                {
                    var budgetItem = budgetCost.Count > i ? budgetCost[i] : null;
                    var finalItem = finalCost.Count > i ? finalCost[i] : null;
                    var itemName = budgetItem == null ? "" : budgetItem.ItemName;
                    if (string.IsNullOrEmpty(itemName))
                        itemName = finalItem == null ? "" : finalItem.ItemName;

                    var bComment = budgetItem == null ? "" : budgetItem.Comment;
                    var fComment = finalItem == null ? "" : finalItem.Comment;
                    sb.Append("<tr>");
                    sb.AppendFormat("<td style='text-align:center;'>{0}</td>", itemName);
                    sb.AppendFormat("<td>{0}{1}</td>", budgetItem == null ? "" : budgetItem.Supplier,
                        string.IsNullOrEmpty(bComment) ? "" : "<div>(" + bComment + ")</div>");
                    sb.AppendFormat("<td style=\"text-align: right;\">{0}</td>", budgetItem == null ? 0 : budgetItem.CostAmt);
                    sb.AppendFormat("<td>{0}{1}</td>", finalItem == null ? "" : finalItem.Supplier,
                        string.IsNullOrEmpty(fComment) ? "" : "<div>" + fComment + "</div>");
                    sb.AppendFormat("<td style=\"text-align: right;\">{0}</td>", finalItem == null ? 0 : finalItem.CostAmt);
                    sb.Append("</tr>");
                    tBudgetCost += budgetItem == null ? 0 : budgetItem.CostAmt;
                    tFinalCost += finalItem == null ? 0 : finalItem.CostAmt;
                }
                #endregion

                #region Table Footer
                sb.Append("<tr>");
                sb.Append("<th colspan='2'>合计</th>");
                sb.Append("<td>&nbsp;</td>");
                sb.AppendFormat("<td style=\"font-weight: bold;font-family:Arial; text-align: right;\">{0}</td>", tBudgetCost.ToString("f2"));  //预算合计
                sb.Append("<td>&nbsp;</td>");
                sb.AppendFormat("<td style=\"font-weight: bold;font-family:Arial;text-align: right;\">{0}</td>", tFinalCost.ToString("f2"));  //决算合计 
                sb.Append("</tr>");

                var budgetProfit = budgetReceivableAmt - tBudgetCost;
                var finalProfit = finalReceivabelAmt - tFinalCost;
                var budget_num = budget == null ? 0 : (budget.AdultNum + budget.ChildNum);
                var final_num = final == null ? 0 : (final.AdultNum + final.ChildNum);
                decimal budgetAvg = 0;
                decimal finalAvg = 0;
                if (budget_num != 0) budgetAvg = budgetProfit / (decimal)budget_num;
                if (final_num != 0) finalAvg = finalProfit / (decimal)final_num;

                sb.Append("<tr>");
                sb.Append("<th rowspan=\"2\" colspan=\"2\">利润</th>");
                sb.Append("<th>预算毛利</th>");
                sb.AppendFormat("<th style=\"font-weight: bold;font-family:Arial; text-align: right;\">{0}</th>", budgetProfit.ToString("f2"));//预算毛利 
                sb.Append("<th>决算毛利</th>");
                sb.AppendFormat("<th style=\"font-weight: bold;font-family:Arial; text-align: right;\">{0}</th>", finalProfit.ToString("f2"));//决算毛利
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<th>人均毛利</th>");
                sb.AppendFormat("<td style=\"font-weight: bold;font-family:Arial; text-align: right;\">{0}</td>", budgetAvg.ToString("f2"));
                sb.Append("<th>人均毛利</th>");
                sb.AppendFormat("<td  style=\"font-weight: bold;font-family:Arial; text-align: right;\">{0}</td>", finalAvg.ToString("f2"));
                sb.Append("</tr>");
                sb.Append(sbComment.ToString());
                #endregion
            }
            context.Response.Write(sb.ToString());
        }

        #endregion

        #region 余款结清
        /// <summary>
        /// 余款结清
        /// </summary>
        /// <param name="context"></param>
        private void OrderCollectionClosed(HttpContext context)
        {
            var orderID = context.Request["id"];
            var orderType = (OrderType)context.Request["xType"].ToInt();
            var isOk = new Order_BF().OrderCollectClosed(orderType, orderID);
            context.Response.Write(isOk ? "1" : "0");
        }
        #endregion

        #region 自主班团
        /// <summary>
        /// 自主班团订单列表
        /// </summary>
        /// <param name="context"></param>
        private void TourOrderList(HttpContext context)
        {
            var sb = new StringBuilder();
            var tourID = context.Request["id"];

            var deptDAL = new Dept_BF();
            var orderDAL = new Order_BF();
            var orderList = orderDAL.GetTourOrderList(tourID);
            orderList.ForEach(x =>
            {
                var customerList = orderDAL.GetOrderCustomer(x.ID);
                var rowSpan = customerList.Count;
                var dept = deptDAL.Get(x.DeptID);
                var seatList = orderDAL.GetOrderSeat(x.ID);//座位号

                #region 散客订单
                sb.Append("<tr>");
                sb.AppendFormat("<td rowspan='{0}'>", rowSpan);
                sb.AppendFormat("<div>{0}</div>", x.OrderNo);
                sb.AppendFormat("<div>收客部门：{0}</div>", dept == null ? "" : dept.Name);
                sb.AppendFormat("<div>业务员：{0}</div>", x.CreateUserName);
                sb.Append("</td>");
                sb.AppendFormat("<td rowspan='{0}'>", rowSpan);
                sb.AppendFormat(" {0}&nbsp;{1}", x.VenueName, x.CollectTime);
                sb.Append("</td>");
                sb.AppendFormat("<td rowspan='{0}'>", rowSpan);
                sb.AppendFormat("<div>成人:{0} 儿童:{1}</div>", x.AdultNum, x.ChildNum);
                var seat = GetOrderSeat(seatList);
                if (!string.IsNullOrEmpty(seat))
                    sb.AppendFormat("<div>座号：{0}</div>", seat);
                sb.Append("</td>");

                var c = rowSpan > 0 ? customerList.First() : null;
                sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", c == null ? "" : c.Name);
                sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", c == null ? "" : c.Mobile);
                sb.AppendFormat("<td>{0}</td>", c == null ? "" : c.IDNo);
                sb.AppendFormat("<td>{0}</td>", c == null ? "" : c.Comment);
                sb.Append("</tr>");

                for (var i = 1; i < rowSpan; i++)
                {
                    var entity = customerList[i];
                    sb.Append("<tr>");
                    sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", entity.Name);
                    sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", entity.Mobile);
                    sb.AppendFormat("<td>{0}</td>", c == null ? "" : entity.IDNo);
                    sb.AppendFormat("<td>{0}</td>", c == null ? "" : entity.Comment);
                    sb.Append("</tr>");
                }
                #endregion
            });

            #region 按上车地点合计
            sb.Append("<tr>");
            sb.Append("<th colspan=\"6\">上车地点</th>");
            sb.Append("<th>人数合计</th>");
            sb.Append("</tr>");
            sb.Append(SetVenueInfo(orderList));
            #endregion

            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 按集合地点统计
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string SetVenueInfo(List<DAL.Ord_OrderInfo> list)
        {
            var dict = new Dictionary<string, int>();
            list.ForEach(x =>
            {
                var key = x.VenueName + "@" + x.CollectTime;
                var n = x.AdultNum + x.ChildNum;
                if (dict.ContainsKey(key))
                {
                    dict[key] = dict[key] + n;
                }
                else
                    dict[key] = n;
            });
            var sb = new StringBuilder();
            foreach (var kp in dict)
            {
                var arr = kp.Key.Split('@');
                var venue = string.IsNullOrEmpty(arr[0]) ? "未知" : arr[0];
                sb.Append("<tr>");
                sb.AppendFormat("<td colspan='6'>{0}{1}</td>", venue, arr[1]);
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", kp.Value);
                sb.Append("</tr>");
            }
            return sb.ToString();
        }

        private string GetOrderSeat(List<DAL.Ord_OrderSeat> list)
        {
            var arr = new List<int>();
            foreach (var e in list)
            {
                arr.Add(e.SeatNum);
            }
            return SeatNoGroup(arr.ToArray());
        }

        /// <summary>
        /// 座位号分组显示
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private string SeatNoGroup(int[] arrData)
        {
            var arr = new List<string>();
            var len = arrData.Length;
            for (var i = 0; i < len; i++)
            {
                var list = new List<int>();
                list.Add(arrData[i]);
                for (var j = i + 1; j < arrData.Length; j++, i++)
                {
                    var __v = arrData[j];
                    var __preVal = arrData[j - 1];
                    if (__preVal + 1 != __v)
                    {
                        if (!list.Exists(x => x.Equals(__preVal)))
                            list.Add(__preVal);
                        break;
                    }
                    else if (j == len - 1)
                        list.Add(arrData[len - 1]);

                }
                arr.Add(list.Count == 1 ? list.First().ToString() : string.Join("-", list));
            }
            return string.Join(",", arr);
        }
        #endregion

        #region 导游报账单确认
        /// <summary>
        /// 导游报账单确认
        /// </summary>
        /// <param name="context"></param>
        private void OrderCheckAccount(HttpContext context)
        {
            var keyID = context.Request["id"];
            var isOk = new CheckAccount_BF().CheckAccountConfirm(keyID);
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
            }
            return pageID;
        }
    }
}