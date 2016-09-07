using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.PageBase;
using DRP.BF.CheckIn;
using DRP.Framework.Core;
using DRP.BF.Glo;
using DRP.BF.RptMrg;
using System.Text;
using System.Data;
using DRP.BF.Order;
using DRP.BF;
using DRP.BF.ResMrg;
using DRP.BF.Fin;
using DRP.BF.SysMrg;
using DRP.DAL.DataAccess;

namespace DRP.WEB.Module.Rpt.Service
{
    /// <summary>
    /// 统计报表
    /// </summary>
    public class RptUtility : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://按线路类型统计员工业务
                    UserBizStatisitc(context);
                    break;
                case 2://按线路类型统计部门业务
                    DeptStatisitc(context);
                    break;
                case 3://部门业务统计
                    DeptProfitRpt(context);
                    break;
                case 4://员工业务统计
                    UserProfitRpt(context);
                    break;
                case 5://应付款汇总统计
                    SupplierPayableStatistic(context);
                    break;
                case 6://订单付款明细
                    OrderPaidRpt(context);
                    break;
                case 7://订单收款明细
                    OrderIncomeRpt(context);
                    break;
                case 8://订单成交量分析
                    OrderQuantity(context);
                    break;
                case 9://客户增长率
                    CustomerRateStatistic(context);
                    break;
                case 10://订单来源统计
                    OrderSourceStatistic(context);
                    break;
                case 11: //非订单收入汇总
                    NonOrderIncomeCheckIn(context);
                    break;
                case 12://非订单支出汇总
                    NonOrderPayCheckIn(context);
                    break;
                case 13://订单类型统计
                    OrderTypeRpt(context);
                    break;
                case 14://综合业务报表
                    CommonBizRpt(context);
                    break;
                case 15://订单收支明细表
                    OrderSheetRpt(context);
                    break;
            }
        }

        #region << 综合业务统计报表 >>
        /// <summary>
        /// 综合业务统计报表
        /// </summary>
        /// <param name="context"></param>
        private void CommonBizRpt(HttpContext context)
        {
            var deptID = context.Request["deptID"];
            var year = context.Request["y"].ToInt();

            var sb = new StringBuilder();

            #region 数据
            var basic = new BasicInfo_BF();
            var incomeItem = basic.GetBasicInfo(BasicType.CheckIn_IncomeSign);//非订单收入类型
            var costItem = basic.GetBasicInfo(BasicType.CheckIn_PayableSign);//非订单支出类型
            var dtOrder = new RptUtility_BF().OrderProfitStatisticByMonth(year, deptID);//订单收入支出
            var dtTicketOrder = new RptUtility_BF().TicketOrderProfitStatisticByMonth(year, deptID);//机票订单
            var dtIncome = new IncomeCheckIn_BF().IncomeCheckInStatisticByMonth(year, deptID);//非订单收入统计
            var dtPay = new PayCheckIn_BF().PayCheckInStatisticByMonth(year, deptID);//非订单支出统计
            #endregion

            #region 表头
            sb.Append("<tr>");
            sb.Append("<th rowspan='2'>月度</th>");
            sb.Append("<th rowspan='2' style='background-color:#FFE48D;'>收入合计</th>");
            sb.AppendFormat("<th colspan='{0}'>收入项目</th>", incomeItem.Count + 3);
            sb.Append("<th rowspan='2' style='background-color:#EBFFFF;'>成本合计</th>");
            sb.AppendFormat("<th colspan='{0}'>成本项目</th>", costItem.Count + 3);
            sb.Append("<th rowspan='2'>毛利</th>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th>订单类</th>");
            sb.Append("<th>单项业务</th>");
            sb.Append("<th>机票订单</th>");
            incomeItem.ForEach(x =>
            {
                sb.AppendFormat("<th>{0}</th>", x.Name);
            });
            sb.Append("<th>订单类</th>");
            sb.Append("<th>单项业务</th>");
            sb.Append("<th>机票订单</th>");
            costItem.ForEach(x =>
            {
                sb.AppendFormat("<th>{0}</th>", x.Name);
            });
            sb.Append("</tr>");
            #endregion

            decimal tIncome = 0;
            decimal tCost = 0;
            decimal tTicketOrderIncome = 0;
            decimal tTicketOrderCost = 0;

            #region 表体
            for (var i = 1; i < 13; i++)
            {
                decimal totalIncome = 0;//收入合计
                decimal totalPay = 0;//支出合计

                #region 收入
                var strIncome = new StringBuilder();
                var orderAmt = FindBizOrderAmt(dtOrder, i);//订单收入
                var bizAmt = FindBizOrderAmt(dtOrder, i, (int)OrderType.DXYW);//单项订单收入  
                var ticketAmt = FindTicketOrderAmt(dtTicketOrder, i);//机票订单收入

                incomeItem.ForEach(a =>//非订单收入
                {
                    var amt = FindSignIncomeAmt(dtIncome, i, a.ID);
                    totalIncome += amt;
                    strIncome.AppendFormat("<td style='text-align:right;'>{0}</td>", amt);
                });
                totalIncome += orderAmt;
                totalIncome += bizAmt;
                totalIncome += ticketAmt;
                tTicketOrderIncome += ticketAmt;

                #endregion

                #region 支出
                var strCost = new StringBuilder();
                var orderCost = FindBizOrderCost(dtOrder, i);//订单成本
                var bizCost = FindBizOrderCost(dtOrder, i, (int)OrderType.DXYW);//单项订单成本
                var ticketCost = FindTicketOrderCost(dtTicketOrder, i);//机票订单成本
                costItem.ForEach(a =>
                {
                    var amt = FindSignPayAmt(dtPay, i, a.ID);
                    totalPay += amt;
                    strCost.AppendFormat("<td style='text-align:right;'>{0}</td>", amt);
                });
                totalPay += orderCost;
                totalPay += bizCost;
                totalPay += ticketCost;
                #endregion

                sb.Append("<tr>");
                //收入
                sb.AppendFormat("<th>{0}月</th>", i);
                sb.AppendFormat("<td style='background-color:#FFE48D;text-align:right;'>{0}</td>", totalIncome.ToString("f2"));
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", orderAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", bizAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", ticketAmt);
                sb.Append(strIncome.ToString());
                //支出
                sb.AppendFormat("<td style='background-color:#EBFFFF;text-align:right;'>{0}</td>", totalPay.ToString("f2"));
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", orderCost);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", bizCost);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", ticketCost);
                sb.Append(strCost.ToString());
                //毛利
                sb.AppendFormat("<td style='text-align:right; color:red;'>{0}</td>", (totalIncome - totalPay).ToString("f2"));
                sb.Append("</tr>");

                tIncome += totalIncome;
                tCost += totalPay;
                tTicketOrderCost += ticketCost;
            }
            #endregion

            #region 页脚
            sb.Append("<tr>");
            //收入            
            sb.Append("<th>合计</th>");
            sb.AppendFormat("<td style='background-color:#FFE48D;text-align:right;'>{0}</td>", tIncome);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", FindBizOrderAmt(dtOrder));
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", FindBizOrderSumAmt(dtOrder, (int)OrderType.DXYW));
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tTicketOrderIncome);//机票
            incomeItem.ForEach(x =>
            {
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", FindSignIncomeAmt(dtIncome, x.ID));
            });
            //支出
            sb.AppendFormat("<td style='background-color:#EBFFFF;text-align:right;'>{0}</td>", tCost.ToString("f2"));
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", FindBizOrderCost(dtOrder));
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", FindBizOrderSumCost(dtOrder, (int)OrderType.DXYW));
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tTicketOrderCost);//机票
            costItem.ForEach(x =>
            {
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", FindSignPayAmt(dtPay, x.ID));
            });
            //毛利
            sb.AppendFormat("<td style='text-align:right; color:red;'>{0}</td>", (tIncome - tCost).ToString("f2"));
            sb.Append("</tr>");
            #endregion

            context.Response.Write(sb.ToString());
        }

        #region 获取收入金额

        /// <summary>
        /// 单项订单收入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderAmt(DataTable dt, int m, int orderType)
        {
            var rows = dt.Select(string.Format("M={0} and OrderType={1}", m, orderType));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderAmt"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 单项订单收入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderSumAmt(DataTable dt, int orderType)
        {
            var rows = dt.Select(string.Format("OrderType={0}", orderType));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderAmt"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 订单收入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderAmt(DataTable dt)
        {
            var rows = dt.Select("OrderType<5");
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderAmt"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 非单项订单收入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderAmt(DataTable dt, int m)
        {
            var rows = dt.Select(string.Format("M={0} and OrderType<5", m));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderAmt"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 非订单收入登记
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private decimal FindSignIncomeAmt(DataTable dt, int m, string incomeTypeID)
        {
            var rows = dt.Select(string.Format("M={0} and IncomeTypeID='{1}'", m, incomeTypeID));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["IncomeAmt"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 非订单收入登记
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private decimal FindSignIncomeAmt(DataTable dt, string incomeTypeID)
        {
            var rows = dt.Select(string.Format("IncomeTypeID='{0}'", incomeTypeID));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["IncomeAmt"].ToString().ToDecimal();
            }
            return d;
        }

        #endregion

        #region 获取支出金额

        /// <summary>
        /// 单项订单支出
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderCost(DataTable dt, int m, int orderType)
        {
            var rows = dt.Select(string.Format("M={0} and OrderType={1}", m, orderType));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderCost"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 单项订单成本
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderSumCost(DataTable dt, int orderType)
        {
            var rows = dt.Select(string.Format("OrderType={0}", orderType));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderCost"].ToString().ToDecimal();
            }
            return d;
        }
        
        /// <summary>
        /// 非单项订单成本
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderCost(DataTable dt, int m)
        {
            var rows = dt.Select(string.Format("M={0} and OrderType<5", m));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderCost"].ToString().ToDecimal();
            }
            return d;
        }


        /// <summary>
        /// 机票单项订单收入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindTicketOrderAmt(DataTable dt, int m)
        {
            var rows = dt.Select(string.Format("M={0}", m));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderAmt"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 机票单项订单成本
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindTicketOrderCost(DataTable dt, int m)
        {
            var rows = dt.Select(string.Format("M={0}", m));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderCost"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 订单成本
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private decimal FindBizOrderCost(DataTable dt)
        {
            var rows = dt.Select("OrderType<5");
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["OrderCost"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 非订单支出登记
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private decimal FindSignPayAmt(DataTable dt, int m, string payTypeID)
        {
            var rows = dt.Select(string.Format("M={0} and PayTypeID='{1}'", m, payTypeID));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["PayAmt"].ToString().ToDecimal();
            }
            return d;
        }

        /// <summary>
        /// 非订单支出登记
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private decimal FindSignPayAmt(DataTable dt, string payTypeID)
        {
            var rows = dt.Select(string.Format("PayTypeID='{0}'", payTypeID));
            if (rows.Length == 0) return 0;
            decimal d = 0;
            foreach (DataRow row in rows)
            {
                d += row["PayAmt"].ToString().ToDecimal();
            }
            return d;
        }

        #endregion 

        #endregion

        #region << 订单收支明细表 >>

        /// <summary>
        /// 订单收支明细表
        /// </summary>
        /// <param name="context"></param>
        private void OrderSheetRpt(HttpContext context)
        {
            var qry = new OrderCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderName = context.Request["OrderName"];
            qry.OrderNo = context.Request["OrderNo"];
            qry.OrdType = (OrderType)context.Request["OrderType"].ToInt();
            qry.CusotmerName = context.Request["Customer"];
            qry.Supplier = context.Request["Supplier"];
            qry.DateType = 1;//出团日期
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            dateScope = new DateScope();
            dateScope.sDate = context.Request["sCreateDate"];
            dateScope.eDate = context.Request["eCreateDate"];
            qry.QryDate = dateScope;
            qry.OrdStatus = (OrderStatus)context.Request["Status"].ToInt();
            qry.CreateUserName = context.Request["CreateUserName"];
            qry.Participant = context.Request["Participant"];

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new RptUtility_BF().OrderSheetRpt(qry, out total);
            var t = new RptUtility_BF().OrderSheetRpt_Sum(qry).Rows[0];
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + ",\"footer\":[{\"AdultNum\":" + t["AdultNum"] + ",\"ChildNum\":" + t["ChildNum"] + ",\"OrderAmt\":" + t["OrderAmt"] + ",\"CollectedAmt\":" + t["CollectedAmt"] + ",\"OrderCost\":" + t["OrderCost"] + ",\"Profit\":" + t["Profit"] + ",\"UnCollectedAmt\":" + t["UnCollectedAmt"] + ",\"PaidAmt\":" + t["PaidAmt"] + ",\"UnPaidAmt\":" + t["UnPaidAmt"] + ",\"OrderInvoiceAmt\":" + t["OrderInvoiceAmt"] + ",\"tDate\":\"合计:\"}]}";
            context.Response.Write(s);
        }
        #endregion

        #region << 订单统计报表 >>

        /// <summary>
        /// 订单类型统计报表
        /// </summary>
        /// <param name="context"></param>
        private void OrderTypeRpt(HttpContext context)
        {
            var qry = new OrderCriteria();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            dateScope = new DateScope();
            dateScope.sDate = context.Request["sCreateDate"];
            dateScope.eDate = context.Request["eCreateDate"];
            qry.QryDate = dateScope;

            var dt = new RptUtility_BF().OrderTypeBizStatistic(qry);
            context.Response.Write(ConvertJson.ToJson(dt));
        }

        /// <summary>
        /// 员工业务统计报表
        /// </summary>
        /// <param name="context"></param>
        private void UserProfitRpt(HttpContext context)
        {
            var qry = new OrderCriteria();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            dateScope = new DateScope();
            dateScope.sDate = context.Request["sCreateDate"];
            dateScope.eDate = context.Request["eCreateDate"];
            qry.QryDate = dateScope;

            var dt = new RptUtility_BF().UserBizStatistic(qry);
            context.Response.Write(ConvertJson.ToJson(dt));
        }

        /// <summary>
        /// 部门业务统计报表
        /// </summary>
        /// <param name="context"></param>
        private void DeptProfitRpt(HttpContext context)
        {
            var qry = new OrderCriteria();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            dateScope = new DateScope();
            dateScope.sDate = context.Request["sCreateDate"];
            dateScope.eDate = context.Request["eCreateDate"];
            qry.QryDate = dateScope;

            var dt = new RptUtility_BF().DeptBizStatistic(qry);
            context.Response.Write(ConvertJson.ToJson(dt));
        }

        /// <summary>
        /// 按线路类型统计部门业务
        /// </summary>
        /// <param name="context"></param>
        private void DeptStatisitc(HttpContext context)
        {
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var sCreateDate = context.Request["sCreateDate"];
            var eCreateDate = context.Request["eCreateDate"];
            var dt = new RptUtility_BF().DeptBizStatisticByRouteType(sDate, eDate, sCreateDate, eCreateDate);
            var dtTicketOrder = new RptUtility_BF().TicketOrderStatistic(sDate, eDate, sCreateDate, eCreateDate);//机票订单
            var routeTypeList = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            var deptList = new Dept_BF().GetDepartment();
            // deptList.Add(new DAL.Sys_Department() { ID=AuthenticationPage.UserInfo.DeptID,Name=AuthenticationPage.UserInfo.DeptName });
            var sb = new StringBuilder();

            #region 抬头
            sb.Append("<tr>");
            sb.Append("<th rowspan='2'>部门名称</th>");
            routeTypeList.ForEach(x =>
            {
                sb.AppendFormat("<th colspan='3'>{0}</th>", x.Name);

            });
            sb.AppendFormat("<th colspan='2'>单项业务</th>");
            sb.AppendFormat("<th colspan='2'>机票订单</th>");
            sb.AppendFormat("<th colspan='3'>合计</th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            routeTypeList.ForEach(x =>
            {
                sb.Append("<th>人数</th>");
                sb.Append("<th>金额</th>");
                sb.Append("<th>毛利</th>");
            });
            sb.Append("<th>金额</th>");
            sb.Append("<th>毛利</th>");
            sb.Append("<th>金额</th>");
            sb.Append("<th>毛利</th>");
            sb.Append("<th>人数</th>");
            sb.Append("<th>金额</th>");
            sb.Append("<th>毛利</th>");
            sb.Append("</tr>");
            #endregion

            #region 表体
            var totalNum = 0;
            decimal totalProfit = 0;
            decimal totalAmt = 0;
            deptList.ForEach(d =>
            {
                var tVisitorNum = 0;
                decimal tProfit = 0;
                decimal tAmt = 0;
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", d.Name);
                routeTypeList.ForEach(x =>
                {
                    var visitorNum = GetDataTableColValue(dt, d.ID, x.ID, "VisitorNum").ToInt();
                    var profit = GetDataTableColValue(dt, d.ID, x.ID, "Profit").ToDecimal();
                    var amt = GetDataTableColValue(dt, d.ID, x.ID, "OrderAmt").ToDecimal();
                    tVisitorNum += visitorNum;
                    tProfit += profit;
                    tAmt += amt;
                    sb.AppendFormat("<td style='text-align:right;'>{0}</td>", visitorNum);
                    sb.AppendFormat("<td style='text-align:right;'>{0}</td>", amt);
                    sb.AppendFormat("<td style='text-align:right;'>{0}</td>", profit);
                });
                //单项 
                var vProfit = GetDataTableColValue(dt, d.ID, Guid.Empty.ToString(), "Profit").ToDecimal();
                var vAmt = GetDataTableColValue(dt, d.ID, Guid.Empty.ToString(), "OrderAmt").ToDecimal();
                tProfit += vProfit;
                tAmt += vAmt;
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", vAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", vProfit);

                //机票
                var mOrderAmt = GetDataTableColValue(dtTicketOrder, d.ID, "OrderAmt").ToDecimal();
                var mOrderCost = GetDataTableColValue(dtTicketOrder, d.ID, "OrderCost").ToDecimal();
                var mProfit = mOrderAmt - mOrderCost;
                tProfit += mProfit;
                tAmt += mOrderAmt;
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", mOrderAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", mProfit);

                //合计  
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tVisitorNum);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tProfit);
                sb.Append("</tr>");
                totalAmt += tAmt;
                totalProfit += tProfit;
                totalNum += tVisitorNum;
            });
            #endregion

            #region 页脚

            sb.Append("<tr>");
            sb.Append("<td>合计</td>");
            routeTypeList.ForEach(x =>
            {
                var visitorNum = GetDataTableSumValue(dt, x.ID, "VisitorNum");
                var profit = GetDataTableSumValue(dt, x.ID, "Profit");
                var amt = GetDataTableSumValue(dt, x.ID, "OrderAmt");
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", visitorNum);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", amt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", profit);
            });
            //单项 
            var a = GetDataTableSumValue(dt, Guid.Empty.ToString(), "Profit");
            var b = GetDataTableSumValue(dt, Guid.Empty.ToString(), "OrderAmt");
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", b);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", a);

            //机票
            var t_Amt = GetDataTableSumValue(dtTicketOrder, "OrderAmt");
            var t_Cost = GetDataTableSumValue(dtTicketOrder, "OrderCost");
            var t_Profit = t_Amt - t_Cost;
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", t_Amt);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", t_Profit);

            //合计  
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", totalNum);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", totalAmt);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", totalProfit);
            sb.Append("</tr>");
            #endregion

            context.Response.Write(sb.ToString());

        }

        /// <summary>
        /// 按线路类型统计员工业绩
        /// </summary>
        /// <param name="context"></param>
        private void UserBizStatisitc(HttpContext context)
        {
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var sCreateDate = context.Request["sCreateDate"];
            var eCreateDate = context.Request["eCreateDate"];
            var dt = new RptUtility_BF().UserBizStatisticByRouteType(sDate, eDate, sCreateDate, eCreateDate);
            var dtTicketOrder = new RptUtility_BF().UserTicketOrderStatistic(sDate, eDate, sCreateDate, eCreateDate);//机票订单
            var routeTypeList = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            var userList = new User_BF().GetSysUser();
            var sb = new StringBuilder();

            #region 抬头
            sb.Append("<tr>");
            sb.Append("<th rowspan='2'>姓名</th>");
            routeTypeList.ForEach(x =>
            {
                sb.AppendFormat("<th colspan='3'>{0}</th>", x.Name);

            });
            sb.AppendFormat("<th colspan='2'>单项业务</th>");
            sb.AppendFormat("<th colspan='2'>机票订单</th>");
            sb.AppendFormat("<th colspan='3'>合计</th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            routeTypeList.ForEach(x =>
            {
                sb.Append("<th>人数</th>");
                sb.Append("<th>金额</th>");
                sb.Append("<th>毛利</th>");
            });
            sb.Append("<th>金额</th>");
            sb.Append("<th>毛利</th>");
            sb.Append("<th>金额</th>");
            sb.Append("<th>毛利</th>");
            sb.Append("<th>人数</th>");
            sb.Append("<th>金额</th>");
            sb.Append("<th>毛利</th>");
            sb.Append("</tr>");
            #endregion

            #region 表体
            var totalNum = 0;
            decimal totalProfit = 0;
            decimal totalAmt = 0;
            userList.ForEach(d =>
            {
                var tVisitorNum = 0;
                decimal tProfit = 0;
                decimal tAmt = 0;
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", d.Name);
                routeTypeList.ForEach(x =>
                {
                    var visitorNum = GetUserDataTableValue(dt, d.ID, x.ID, "VisitorNum").ToInt();
                    var profit = GetUserDataTableValue(dt, d.ID, x.ID, "Profit").ToDecimal();
                    var amt = GetUserDataTableValue(dt, d.ID, x.ID, "OrderAmt").ToDecimal();
                    tVisitorNum += visitorNum;
                    tProfit += profit;
                    tAmt += amt;
                    sb.AppendFormat("<td style='text-align:right;'>{0}</td>", visitorNum);
                    sb.AppendFormat("<td style='text-align:right;'>{0}</td>", amt);
                    sb.AppendFormat("<td style='text-align:right;'>{0}</td>", profit);
                });
                //单项 
                var vProfit = GetUserDataTableValue(dt, d.ID, Guid.Empty.ToString(), "Profit").ToDecimal();
                var vAmt = GetUserDataTableValue(dt, d.ID, Guid.Empty.ToString(), "OrderAmt").ToDecimal();
                tProfit += vProfit;
                tAmt += vAmt;
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", vAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", vProfit);

                //机票
                vProfit = GetUserDataTableValue(dtTicketOrder, d.ID, "Profit").ToDecimal();
                vAmt = GetUserDataTableValue(dtTicketOrder, d.ID, "OrderAmt").ToDecimal();
                tProfit += vProfit;
                tAmt += vAmt;
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", vAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", vProfit);

                //合计  
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tVisitorNum);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tAmt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", tProfit);
                sb.Append("</tr>");
                totalAmt += tAmt;
                totalProfit += tProfit;
                totalNum += tVisitorNum;
            });
            #endregion

            #region 页脚

            sb.Append("<tr>");
            sb.Append("<td>合计</td>");
            routeTypeList.ForEach(x =>
            {
                var visitorNum = GetDataTableSumValue(dt, x.ID, "VisitorNum");
                var profit = GetDataTableSumValue(dt, x.ID, "Profit");
                var amt = GetDataTableSumValue(dt, x.ID, "OrderAmt");
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", visitorNum);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", amt);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", profit);
            });
            //单项 
            var a = GetDataTableSumValue(dt, Guid.Empty.ToString(), "Profit");
            var b = GetDataTableSumValue(dt, Guid.Empty.ToString(), "OrderAmt");
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", b);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", a);

            //机票
            a = GetDataTableSumValue(dtTicketOrder, "Profit");
            b = GetDataTableSumValue(dtTicketOrder, "OrderAmt");
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", b);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", a);

            //合计  
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", totalNum);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", totalAmt);
            sb.AppendFormat("<td style='text-align:right;'>{0}</td>", totalProfit);
            sb.Append("</tr>");
            #endregion

            context.Response.Write(sb.ToString());

        }

        private string GetDataTableColValue(DataTable dt, string deptID, string routeTypeID, string colName)
        {
            var rows = dt.Select(string.Format("DeptID='{0}' and RouteTypeID='{1}'", deptID, routeTypeID));
            if (rows.Length == 0) return "";
            else
            {
                return rows[0][colName].ToString();
            }
        }

        /// <summary>
        /// 机票订单
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="deptID"></param>
        /// <param name="routeTypeID"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        private string GetDataTableColValue(DataTable dt, string deptID, string colName)
        {
            var rows = dt.Select(string.Format("DeptID='{0}'", deptID));
            if (rows.Length == 0) return "";
            else
            {
                return rows[0][colName].ToString();
            }
        }

        private string GetUserDataTableValue(DataTable dt, string userID, string routeTypeID, string colName)
        {
            var rows = dt.Select(string.Format("CreateUserID='{0}' and RouteTypeID='{1}'", userID, routeTypeID));
            if (rows.Length == 0) return "";
            else
            {
                return rows[0][colName].ToString();
            }
        }

        /// <summary>
        /// 适用于机票订单的数据获取
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="userID"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        private string GetUserDataTableValue(DataTable dt, string userID, string colName)
        {
            var rows = dt.Select(string.Format("CreateUserID='{0}'", userID));
            if (rows.Length == 0) return "";
            else
            {
                return rows[0][colName].ToString();
            }
        }

        private decimal GetDataTableSumValue(DataTable dt, string routeTypeID, string colName)
        {
            var rows = dt.Select(string.Format("RouteTypeID='{0}'", routeTypeID));
            decimal t = 0;
            foreach (DataRow row in rows)
            {
                t += row[colName].ToString().ToDecimal();
            }
            return t;
        }

        private decimal GetDataTableSumValue(DataTable dt, string colName)
        {
            decimal t = 0;
            foreach (DataRow row in dt.Rows)
            {
                t += row[colName].ToString().ToDecimal();
            }
            return t;
        }

        /// <summary>
        /// 订单收款明细报表
        /// </summary>
        /// <param name="context"></param>
        private void OrderIncomeRpt(HttpContext context)
        {
            var qry = new OrderCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderName = context.Request["OrderName"];
            qry.OrderNo = context.Request["OrderNo"];
            qry.OrdType = (OrderType)context.Request["OrderType"].ToInt();
            qry.CusotmerName = context.Request["Customer"];
            qry.Supplier = context.Request["Supplier"];
            qry.DateType = context.Request["DateType"].ToInt();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            qry.DeptID = context.Request["DeptID"];
            qry.CreateUserID = context.Request["UserID"];

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CollectDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new RptUtility_BF().OrderIncomeRpt(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 订单支出（付款)明细报表
        /// </summary>
        /// <param name="context"></param>
        private void OrderPaidRpt(HttpContext context)
        {
            var qry = new OrderCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderName = context.Request["OrderName"];
            qry.OrderNo = context.Request["OrderNo"];
            qry.Supplier = context.Request["Supplier"];
            qry.DateType = context.Request["DateType"].ToInt();
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope;
            qry.DeptID = context.Request["DeptID"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "PayDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new RptUtility_BF().OrderPaidRpt(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 供应商付款总额
        /// </summary>
        /// <param name="context"></param>
        private void SupplierPayableStatistic(HttpContext context)
        {
            var qry = new PayableCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.SupplierName = context.Request["Name"];
            qry.SupplierType = (ResourceType)context.Request["ResType"].ToInt();

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "Spell" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Asc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new OrderPayable_BF().QueryPayableStatisticData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }
        #endregion

        #region << 非订单统计报表 >>
        /// <summary>
        /// 非订单收入登记
        /// </summary>
        /// <param name="context"></param>
        private void NonOrderIncomeCheckIn(HttpContext context)
        {
            var category = context.Request["category"].ToInt();//1：收入类型 2：收入部门
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var dal = new IncomeCheckIn_BF();
            var dt = category == 1 ? dal.IncomeDataStatisticByIncomeType(sDate, eDate) : dal.IncomeDataStatisticByDept(sDate, eDate);
            context.Response.Write(ConvertJson.ToJson(dt));
        }

        /// <summary>
        /// 非订单支出登记 
        /// </summary>
        /// <param name="context"></param>
        private void NonOrderPayCheckIn(HttpContext context)
        {
            var category = context.Request["category"].ToInt();//1：支出类型 2：支出部门
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var dal = new PayCheckIn_BF();
            var dt = category == 1 ? dal.PayDataStatisticByPayType(sDate, eDate) : dal.PayDataStatisticByDept(sDate, eDate);
            context.Response.Write(ConvertJson.ToJson(dt));
        }
        #endregion

        #region << 智能分析报表 >>

        /// <summary>
        /// 订单成交量分析
        /// </summary>
        /// <param name="context"></param>
        private void OrderQuantity(HttpContext context)
        {
            var year = context.Request["year"].ToInt();
            var orderType = context.Request["orderType"].ToInt();

            var dal = new RptUtility_BF();
            var dt = dal.OrderQuantityStatistic(year, orderType);
            var coll = new List<string>();
            var list = new List<string>();
            list.Add("一月");
            list.Add("二月");
            list.Add("三月");
            list.Add("四月");
            list.Add("五月");
            list.Add("六月");
            list.Add("七月");
            list.Add("八月");
            list.Add("九月");
            list.Add("十月");
            list.Add("十一月");
            list.Add("十二月");
            var i = 1;
            list.ForEach(x =>
            {
                var strJson = "\"Month\":\"{0}\",\"Profit\":\"{1}\",\"OrderAmt\":\"{2}\",\"OrderNum\":\"{3}\"";
                var p = FindDataTableValue(dt, "M", i.ToString(), "Profit");
                var a = FindDataTableValue(dt, "M", i.ToString(), "OrderAmt");
                var n = FindDataTableValue(dt, "M", i.ToString(), "OrderNum");
                var json = string.Format(strJson, x, p, a, n);
                coll.Add("{" + json + "}");
                i++;
            });
            var s = "[" + string.Join(",", coll) + "]";
            context.Response.Write(s);
        }

        /// <summary>
        /// 订单来源分析
        /// </summary>
        /// <param name="context"></param>
        private void OrderSourceStatistic(HttpContext context)
        {
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_OrderSource);
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var dal = new RptUtility_BF();
            var dt = dal.OrderSourceStatistic(sDate, eDate);
            var coll = new List<string>();
            list.ForEach(x =>
            {
                var strJson = "\"SourceName\":\"{0}\",\"Profit\":\"{1}\",\"OrderAmt\":\"{2}\",\"OrderNum\":\"{3}\"";
                var profit = FindDataTableValue(dt, "SourceName", x.Name, "Profit");
                var amt = FindDataTableValue(dt, "SourceName", x.Name, "OrderAmt");
                var num = FindDataTableValue(dt, "SourceName", x.Name, "OrderNum");
                var json = string.Format(strJson, x.Name, profit, amt, num);
                coll.Add("{" + json + "}");
            });
            var s = "[" + string.Join(",", coll) + "]";
            context.Response.Write(s);
        }

        /// <summary>
        /// 客户增长率统计
        /// </summary>
        /// <param name="context"></param>
        private void CustomerRateStatistic(HttpContext context)
        {
            var year = context.Request["year"].ToInt();
            var dal = new RptUtility_BF();
            var dt = dal.CustomerRateStatistic(year);
            var coll = new List<string>();
            var list = new List<string>();
            list.Add("一月");
            list.Add("二月");
            list.Add("三月");
            list.Add("四月");
            list.Add("五月");
            list.Add("六月");
            list.Add("七月");
            list.Add("八月");
            list.Add("九月");
            list.Add("十月");
            list.Add("十一月");
            list.Add("十二月");
            var i = 1;
            list.ForEach(x =>
            {
                var strJson = "\"Month\":\"{0}\",\"Data\":\"{1}\"";
                var v = FindDataTableValue(dt, "M", (i++).ToString(), "iData");
                var json = string.Format(strJson, x, v);
                coll.Add("{" + json + "}");
            });
            var s = "[" + string.Join(",", coll) + "]";
            context.Response.Write(s);
        }

        #endregion

        #region << 辅助方法 >>
        /// <summary>
        /// 查询表格数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="col">列名</param>
        /// <param name="key">列名对应的值</param>
        /// <param name="field">待显示的列</param>
        /// <returns></returns>
        private string FindDataTableValue(DataTable dt, string col, string key, string field)
        {
            var rows = dt.Select(string.Format("{0}='{1}'", col, key));
            if (rows.Length > 0) return rows[0][field].ToString();
            else return "";
        }
        #endregion

        protected override string GetNavigateID(HttpContext context)
        {
            var xType = context.Request["xType"].ToInt();
            var pageID = "";
            switch (xType)
            {
                case 1://同行散客订单明细报表
                    pageID = "rptskorder";
                    break;
                case 2://自主班散客订单明细报表
                    pageID = "rptzzbsk";
                    break;
                case 3://企业团订单明细报表
                    pageID = "rptteamorder";
                    break;
                case 4://自主班团订单明细报表
                    pageID = "rptbizorder";
                    break;
                case 5://单项业务订单明细报表
                    pageID = "rptbizorder";
                    break;
                case 6://非订单收入汇总报表
                    pageID = "checkinincomerpt";
                    break;
                case 7://非订单支出汇总报表
                    pageID = "checkinpayrpt";
                    break;
                case 8://订单来源统计
                    pageID = "rptordersource";
                    break;
                case 9://客户增长率
                    pageID = "rptcustomerrate";
                    break;
                case 10://订单成交量分析
                    pageID = "rptorderquantity";
                    break;
                case 11://订单收款明细
                    pageID = "rptorderincome";
                    break;
                case 12://订单付款明细
                    pageID = "rptorderpaid";
                    break;
                case 13://应付款汇总统计
                    pageID = "rptsupplierpayable";
                    break;
                case 14://部门业务统计
                    pageID = "rptdeptprofit";
                    break;
                case 15://员工业务统计
                    pageID = "rptuserprofit";
                    break;
                case 16://订单类型统计
                    pageID = "rptstatistic";
                    break;
                case 17://综合业务报表
                    pageID = "rptmultiple";
                    break;
                case 18://订单收支明细
                    pageID = "rptordersheet";
                    break;
            }
            return pageID;
        }
    }
}