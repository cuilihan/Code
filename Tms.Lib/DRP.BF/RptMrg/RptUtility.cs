using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DRP.BF.Order;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.Framework.Core;
using org.in2bits.MyXls;
using System.Web;
using DRP.Framework;

namespace DRP.BF.RptMrg
{
    /// <summary>
    /// 统计报表
    /// </summary>
    public class RptUtility_BF
    {
        DRPDB db = new DRPDB();

        #region << 订单统计报表 >>

        /// <summary>
        /// 订单查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string OrderQueryCondition(OrderCriteria qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion
            if (!string.IsNullOrEmpty(qry.RouteTypeID))
                sb.AppendFormat(" and RouteTypeID= '{0}'", qry.RouteTypeID);
            if (!string.IsNullOrEmpty(qry.DestinationID))
                sb.AppendFormat(" and DestinationPath like '%{0}%'", qry.DestinationID);
            if (qry.OrdType != OrderType.All)
                sb.AppendFormat(" and OrderType={0}", (int)qry.OrdType);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.OrderNo))
                sb.AppendFormat(" and OrderNo like '%{0}%'", qry.OrderNo);
            if (!string.IsNullOrEmpty(qry.CusotmerName))
                sb.AppendFormat(" and CustomerName like '%{0}%'", qry.CusotmerName);
            if (!string.IsNullOrEmpty(qry.Supplier))
                sb.AppendFormat(" and SupplierName like '%{0}%'", qry.Supplier);
            if (qry.QueryDateScope != null)
            {
                if (qry.DateType == 1) //出团日期（订单日期）
                {
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                        sb.AppendFormat(" and TourDate>='{0}'", qry.QueryDateScope.sDate);
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                        sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
                if (qry.DateType == 2) //创建订单日期
                {
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                        sb.AppendFormat(" and CreateDate>='{0}'", qry.QueryDateScope.sDate);
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                        sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
                if (qry.DateType == 3) //订单收款日期
                {
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                        sb.AppendFormat(" and CollectDate>='{0}'", qry.QueryDateScope.sDate);
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                        sb.AppendFormat(" and CollectDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
                if (qry.DateType == 4) //订单付款日期
                {
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                        sb.AppendFormat(" and PayDate>='{0}'", qry.QueryDateScope.sDate);
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                        sb.AppendFormat(" and PayDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
            }
            if (qry.OrdStatus != OrderStatus.All)
                sb.AppendFormat(" and OrderStatus={0}", (int)qry.OrdStatus);
            else
            {
                if (qry.OrdStatus != OrderStatus.Canceled)
                    sb.AppendFormat(" and OrderStatus<4");
            }
            if (qry.BudgetStatus != OrderBudgetStatus.All)
                sb.AppendFormat(" and BudgetStatus={0}", (int)qry.BudgetStatus);
            if (!string.IsNullOrEmpty(qry.DeptID))
                sb.AppendFormat(" and DeptID= '{0}'", qry.DeptID);
            if (!string.IsNullOrEmpty(qry.CreateUserID))
                sb.AppendFormat(" and CreateUserID='{0}'", qry.CreateUserID);
            if (!string.IsNullOrEmpty(qry.OrderSourceID))
                sb.AppendFormat(" and SourceID='{0}'", qry.OrderSourceID);
            if (qry.OrderAmt > 0)
                sb.AppendFormat(" and OrderAmt={0}", qry.OrderAmt);
            if (!string.IsNullOrEmpty(qry.CreateUserName))
                sb.AppendFormat(" and CreateUserName like '%{0}%'", qry.CreateUserName);
            if (!string.IsNullOrEmpty(qry.Participant))
                sb.AppendFormat(" and Participant like '%{0}%'", qry.Participant);
            if (qry.QryDate != null)
            {
                if (!string.IsNullOrEmpty(qry.QryDate.sDate))
                    sb.AppendFormat(" and OrderCreateDate>='{0}'", qry.QryDate.sDate);
                if (!string.IsNullOrEmpty(qry.QryDate.eDate))
                    sb.AppendFormat(" and OrderCreateDate<'{0}'", Convert.ToDateTime(qry.QryDate.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 订单收款明细报表
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="recored"></param>
        /// <returns></returns>
        public DataTable OrderIncomeRpt(OrderCriteria qry, out int record)
        {
            var strWhere = OrderQueryCondition(qry);
            return db.GetPagination("V_Rpt_OrderIncome", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        #region 收支明细


        /// <summary>
        /// 订单收支明细
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="recored"></param>
        /// <returns></returns>
        public DataTable OrderSheetRpt(OrderCriteria qry, out int record)
        {
            var strWhere = OrderQueryCondition(qry);
            return db.GetPagination("Rpt_OrderSheet", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        public DataTable OrderSheetRpt_Sum(OrderCriteria qry)
        {
            var sql = string.Format(@"select isnull(SUM(OrderAmt),0) OrderAmt
                                            ,isnull(SUM(CollectedAmt),0) CollectedAmt
                                            ,isnull(SUM(OrderCost),0) OrderCost
                                            ,isnull(SUM(Profit),0) Profit
                                            ,isnull(SUM(AdultNum),0) AdultNum
                                            ,isnull(SUM(ChildNum),0) ChildNum
                                            ,isnull(SUM(UnCollectedAmt),0) UnCollectedAmt
                                            ,isnull(SUM(PaidAmt),0) PaidAmt
                                            ,isnull(SUM(UnPaidAmt),0) UnPaidAmt
                                            ,isnull(SUM(OrderInvoiceAmt),0) OrderInvoiceAmt
                                            from Rpt_OrderSheet where {0}", OrderQueryCondition(qry));
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 订单收支明细所有数据
        /// </summary>
        /// <param name="qry"></param>
        public bool OrderSheetExport(OrderCriteria qry)
        {
            var strWhere = OrderQueryCondition(qry);
            try
            {
                var dt = new RptDAL().OrderSheetItem(strWhere);
                var fileName = "订单收支明细表"; 
                var list = new List<ExcelCellFormat>();
                list.Add(new ExcelCellFormat(22, "OrderNo", "订单编号"));
                list.Add(new ExcelCellFormat(25, "OrderName", "订单名称"));
                list.Add(new ExcelCellFormat(14, "TourDate", "出团日期",DbType.DateTime));
                list.Add(new ExcelCellFormat(14, "ReturnDate", "返程日期",DbType.DateTime));
                list.Add(new ExcelCellFormat(10, "OrderType", "订单类型"));
                list.Add(new ExcelCellFormat(25, "SupplierName", "供应商"));
                list.Add(new ExcelCellFormat(15, "CustomerName", "客户"));
                list.Add(new ExcelCellFormat(10, "AdultNum", "成人",DbType.Int16));
                list.Add(new ExcelCellFormat(10, "ChildNum", "儿童",DbType.Int16));
                list.Add(new ExcelCellFormat(12, "OrderAmt", "应收款",DbType.Decimal));
                list.Add(new ExcelCellFormat(12, "CollectedAmt", "已收款", DbType.Decimal));
                list.Add(new ExcelCellFormat(12, "UnCollectedAmt", "未收款", DbType.Decimal));
                list.Add(new ExcelCellFormat(12, "OrderCost", "成本", DbType.Decimal));
                list.Add(new ExcelCellFormat(12, "Profit", "毛利", DbType.Decimal));
                //list.Add(new ExcelCellFormat(12, "ProfitRate", "毛利率"));
                list.Add(new ExcelCellFormat(12, "PaidAmt", "已付款", DbType.Decimal));
                list.Add(new ExcelCellFormat(12, "UnPaidAmt", "未付款", DbType.Decimal));
                list.Add(new ExcelCellFormat(12, "OrderInvoiceAmt", "开票金额"));
                list.Add(new ExcelCellFormat(12, "CreateUserName", "提交人")); 
                NPOIHelper.ExportByWeb(dt, fileName, fileName + ".xls", list);
 
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "导出订单收支明细时发生错误");
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 部门业务统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable DeptBizStatistic(OrderCriteria qry)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" and a.OrgID='{0}'", AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                sb.AppendFormat(" and TourDate>='{0}'", qry.QueryDateScope.sDate);
            if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(qry.QryDate.sDate))
                sb.AppendFormat(" and CreateDate>='{0}'", qry.QryDate.sDate);
            if (!string.IsNullOrEmpty(qry.QryDate.eDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.QryDate.eDate).AddDays(1).ToString("yyyy-MM-dd"));

            return new OrderProfitDAL().DeptBizStatisitic(sb.ToString(), AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 订单类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrderTypeBizStatistic(OrderCriteria qry)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" and a.OrgID='{0}'", AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                sb.AppendFormat(" and TourDate>='{0}'", qry.QueryDateScope.sDate);
            if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(qry.QryDate.sDate))
                sb.AppendFormat(" and CreateDate>='{0}'", qry.QryDate.sDate);
            if (!string.IsNullOrEmpty(qry.QryDate.eDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.QryDate.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            return new OrderProfitDAL().OrderTypeStatistic(sb.ToString());
        }

        /// <summary>
        /// 按线路类型统计部门业务
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable DeptBizStatisticByRouteType(string sDate, string eDate, string sCreateDate, string eCreateDate)
        {
            return new OrderProfitDAL().DeptBizStatisiticByRouteType(sDate, eDate,sCreateDate,eCreateDate, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 机票订单的金额与成本统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable TicketOrderStatistic(string sDate, string eDate, string sCreateDate, string eCreateDate)
        {
            return new OrderProfitDAL().TicketOrderStatistic(sDate, eDate, sCreateDate, eCreateDate, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 人员业务统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable UserBizStatistic(OrderCriteria qry)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" and a.OrgID='{0}'", AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                sb.AppendFormat(" and TourDate>='{0}'", qry.QueryDateScope.sDate);
            if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(qry.QryDate.sDate))
                sb.AppendFormat(" and CreateDate>='{0}'", qry.QryDate.sDate);
            if (!string.IsNullOrEmpty(qry.QryDate.eDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.QryDate.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            return new OrderProfitDAL().UserBizStatisitic(sb.ToString());
        }

        /// <summary>
        /// 按线路类型统计员工业务
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable UserBizStatisticByRouteType(string sDate, string eDate, string sCreateDate, string eCreateDate)
        {
            return new OrderProfitDAL().UserBizStatisiticByRouteType(sDate, eDate,sCreateDate,eCreateDate, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 员工绩效统计（机票订单）
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable UserTicketOrderStatistic(string sDate, string eDate, string sCreateDate, string eCreateDate)
        {
            return new OrderProfitDAL().UserTicketOrderStatistic(sDate, eDate, sCreateDate, eCreateDate, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 订单收入、支出统计（按月份统计）
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrderProfitStatisticByMonth(int year, string deptID)
        {
            return new OrderProfitDAL().OrderProfitStatisticByMonth(year, AuthenticationPage.UserInfo.OrgID, deptID);
        }

        /// <summary>
        /// 订单订单收入、支出统计（按月份统计）
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable TicketOrderProfitStatisticByMonth(int year, string deptID)
        {
            return new OrderProfitDAL().TicketOrderProfitStatisticByMonth(year, AuthenticationPage.UserInfo.OrgID, deptID);
        }

        /// <summary>
        /// 订单付款（支出）明细报表
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="recored"></param>
        /// <returns></returns>
        public DataTable OrderPaidRpt(OrderCriteria qry, out int record)
        {
            var strWhere = OrderQueryCondition(qry);
            return db.GetPagination("V_Fin_SupplierPaidItem", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }
        #endregion

        #region << 数据分析统计表 >>

        /// <summary>
        /// 订单成交量统计，应用于首页的图表，有权限过滤
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable OrderStatistic(int year, int orderType)
        {
            var deptID = "";
            var userID = "";
            #region 用户权限
            var user = AuthenticationPage.UserInfo;
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    deptID = user.DeptID;
                    break;
                case SysMrg.DataPermission.Private:
                    userID = user.UserID;
                    break;
            }
            #endregion
            return new OrderQuantityDAL().OrderQuantityStatisticByPermission(year, orderType, user.OrgID, deptID, userID);
        }


        /// <summary>
        /// 订单成交量统计
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable OrderQuantityStatistic(int year, int orderType)
        {
            return new OrderQuantityDAL().OrderQuantityStatistic(year, orderType, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 客户增长率
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable CustomerRateStatistic(int year)
        {
            return new CustomerRateDAL().CustomerRate(year, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 订单来源统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrderSourceStatistic(string sDate, string eDate)
        {
            return new OrderSourceDAL().OrderSourceStatistic(sDate, eDate, AuthenticationPage.UserInfo.OrgID);
        }

        #endregion
    }
}
