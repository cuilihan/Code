using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单利润统计
    /// </summary>
    public class OrderProfitDAL
    {
        /// <summary>
        /// 订单类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable OrderTypeStatistic(string strWhere)
        {
            var sql = "select ISNULL(t.OrderAmt,0) OrderAmt,ISNULL(t.OrderCost,0) OrderCost,ISNULL(t.Profit,0) Profit,ISNULL(t.AdultNum,0) AdultNum,ISNULL(t.ChildNum,0) ChildNum,t.OrderType From (SELECT SUM(OrderAmt) OrderAmt,SUM(OrderCost) OrderCost,SUM(OrderAmt-OrderCost) Profit,SUM(AdultNum) AdultNum,SUM(a.ChildNum) ChildNum ,OrderType FROM Ord_OrderInfo a WHERE OrderStatus!=4 {0} GROUP BY OrderType UNION ALL SELECT SUM(OrderAmt) OrderAmt,SUM(OrderCost) OrderCost,SUM(OrderAmt-OrderCost) Profit,SUM(AdultNum) AdultNum,0 ChildNum ,6 OrderType FROM Ord_TicketOrder a WHERE OrderStatus!=4 {0} UNION ALL SELECT SUM(a.OrderAmt) OrderAmt,SUM(a.OrderCost) OrderCost,SUM(a.orderAmt-a.orderCost) Profit,SUM(AdultNum) AdultNum,SUM(a.ChildNum) ChildNum ,4 OrderType FROM Ord_OrderExtend a INNER JOIN Pro_TourInfo b ON a.OrderID=b.ID where 1=1 {0}) AS t order by OrderType asc";
            sql = string.Format(sql, strWhere.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 部门业务统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable DeptBizStatisitic(string strWhere, string orgID)
        {
            var sql = @"SELECT * FROM  (SELECT a.DeptID,SUM(a.OrderAmt) OrderAmt,SUM(a.Profit) Profit,SUM(a.VisitorNum) VisitorNum 
                    FROM(
	                    SELECT DeptID,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum+ChildNum) VisitorNum 
	                    FROM Ord_OrderInfo a WHERE OrderStatus!=4 {0} GROUP BY DeptID
                        UNION ALL
	                    SELECT DeptID,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum) VisitorNum 
	                    FROM Ord_TicketOrder a WHERE OrderStatus!=4 {0} GROUP BY DeptID
	                    UNION ALL
	                    SELECT b.DeptID,SUM(a.OrderAmt-a.OrderCost) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum+ChildNum) VisitorNum
	                    FROM Ord_OrderExtend a INNER JOIN dbo.Pro_TourInfo b ON a.OrderID=b.ID
	                    WHERE 1=1 {0} GROUP BY b.DeptID 
                    ) AS a GROUP BY a.DeptID) AS a Right JOIN Sys_Department b ON a.DeptID=b.ID WHERE b.OrgID='" + orgID + "'";

            sql = string.Format(sql, strWhere.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 部门业务按订单类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable DeptBizStatisiticByRouteType(string sDate, string eDate,string sCreateDate,string eCreateDate, string orgID)
        {
            var sql = "SELECT * FROM(SELECT DeptID,RouteTypeID,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum+ChildNum) VisitorNum FROM Ord_OrderInfo a WHERE OrderStatus!=4 {0} GROUP BY DeptID,RouteTypeID UNION ALL SELECT a.DeptID,c.RouteTypeID, SUM(b.OrderAmt-b.OrderCost) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum+ChildNum) VisitorNum FROM Ord_OrderExtend b INNER JOIN dbo.Pro_TourInfo a ON b.OrderID=a.ID INNER JOIN dbo.Pro_RouteInfo c ON a.RouteID=c.ID WHERE 1=1 {0} GROUP BY a.DeptID,RouteTypeID) AS a ORDER BY Profit desc";

            var sb = new StringBuilder();
            sb.AppendFormat(" and a.OrgID='{0}'", orgID);
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and TourDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(sCreateDate))
                sb.AppendFormat(" and a.CreateDate>='{0}'", sCreateDate);
            if (!string.IsNullOrEmpty(eCreateDate))
                sb.AppendFormat(" and a.CreateDate<'{0}'", Convert.ToDateTime(eCreateDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 订单订单的金额与成本统计
        /// </summary>
        /// <param name="sDAte"></param>
        /// <param name="eDate"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable TicketOrderStatistic(string sDate, string eDate, string sCreateDate, string eCreateDate, string orgID)
        {
            var sql = "SELECT SUM(OrderAmt) OrderAmt,SUM(OrderCost) OrderCost,DeptID FROM dbo.Ord_TicketOrder WHERE OrgID='{0}'";
            sql = string.Format(sql, orgID);
            if (!string.IsNullOrEmpty(sDate))
                sql+=string.Format(" and TourDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sql+=string.Format(" and TourDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(sCreateDate))
                sql += string.Format(" and CreateDate>='{0}'", sCreateDate);
            if (!string.IsNullOrEmpty(eCreateDate))
                sql += string.Format(" and CreateDate<'{0}'", Convert.ToDateTime(eCreateDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql += " Group by DeptID";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }


        /// <summary>
        /// 员工业务统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable UserBizStatisitic(string strWhere)
        {
            var sql = "SELECT SUM(Profit) Profit,SUM(OrderAmt) OrderAmt,SUM(VisitorNum) VisitorNum,CreateUserName FROM(SELECT CreateUserName,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum+ChildNum) VisitorNum FROM Ord_OrderInfo a WHERE OrderStatus!=4 {0} GROUP BY CreateUserName UNION ALL SELECT CreateUserName,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrdeAmt,SUM(AdultNum) VisitorNum FROM dbo.Ord_TicketOrder a WHERE OrderStatus!=4 {0} GROUP BY CreateUserName UNION ALL SELECT b.CreateUserName,SUM(a.OrderAmt-OrderCost) Profit,SUM(OrderCost) OrderCost,SUM(AdultNum+ChildNum) VisitorNum FROM dbo.Ord_OrderExtend a INNER JOIN dbo.Pro_TourInfo b ON a.OrderID=b.ID WHERE 1=1 {0} GROUP BY CreateUserName) AS T GROUP BY T.CreateUserName ORDER BY Profit DESC";

            sql = string.Format(sql, strWhere.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }


        /// <summary>
        /// 员工业绩按订单类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable UserBizStatisiticByRouteType(string sDate, string eDate, string sCreateDate, string eCreateDate, string orgID)
        {
            var sql = "SELECT * FROM(SELECT CreateUserID,RouteTypeID,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum+ChildNum) VisitorNum FROM Ord_OrderInfo a where OrderStatus!=4 {0} GROUP BY RouteTypeID,CreateUserID UNION ALL SELECT a.CreateUserID,c.RouteTypeID,SUM(b.OrderAmt-b.OrderCost) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum+ChildNum) VisitorNum FROM dbo.Ord_OrderExtend b INNER JOIN dbo.Pro_TourInfo a ON b.OrderID=a.ID INNER JOIN dbo.Pro_RouteInfo c ON a.RouteID=c.ID WHERE 1=1 {0} GROUP BY RouteTypeID,a.CreateUserID) AS T ORDER BY Profit DESC";
            var sb = new StringBuilder();
            sb.AppendFormat(" and a.OrgID='{0}'", orgID);
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and TourDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(sCreateDate))
                sb.AppendFormat(" and a.CreateDate>='{0}'", sCreateDate);
            if (!string.IsNullOrEmpty(eCreateDate))
                sb.AppendFormat(" and a.CreateDate<'{0}'", Convert.ToDateTime(eCreateDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 机票订单按员工统计业绩
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable UserTicketOrderStatistic(string sDate, string eDate, string sCreateDate, string eCreateDate, string orgID)
        {
            var sql = "SELECT CreateUserID,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,SUM(AdultNum) VisitorNum FROM Ord_TicketOrder a where OrderStatus!=4 {0} GROUP BY CreateUserID"; 
            var sb = new StringBuilder();
            sb.AppendFormat(" and OrgID='{0}'", orgID);
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and TourDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(sCreateDate))
                sb.AppendFormat(" and CreateDate>='{0}'", sCreateDate);
            if (!string.IsNullOrEmpty(eCreateDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(eCreateDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 旅游订单收入支出统计（按月份统计）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataTable OrderProfitStatisticByMonth(int year, string orgID, string deptID)
        {
            var sql = "SELECT SUM(OrderAmt) OrderAmt,SUM(OrderCost) OrderCost,MONTH(TourDate) M,OrderType FROM dbo.Ord_OrderInfo a WHERE OrderStatus!=4 {0} GROUP BY MONTH(TourDate),OrderType UNION ALL SELECT SUM(a.OrderAmt) OrderAmt,SUM(OrderCost) OrderCost,MONTH(TourDate) M,4 OrderType FROM dbo.Ord_OrderExtend a INNER JOIN dbo.Pro_TourInfo b ON a.OrderID=b.ID WHERE 1=1 {0} GROUP BY MONTH(TourDate)";
            var sb = new StringBuilder();
            sb.AppendFormat("and YEAR(TourDate)={0} and a.OrgID='{1}'", year, orgID);
            if (!string.IsNullOrEmpty(deptID))
                sb.AppendFormat(" and DeptID='{0}'", deptID);
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 机票订单收入支出统计（按月份统计）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataTable TicketOrderProfitStatisticByMonth(int year, string orgID, string deptID)
        {
            var sql = "SELECT SUM(OrderAmt) OrderAmt,SUM(OrderCost) OrderCost,MONTH(TourDate) M FROM Ord_TicketOrder a WHERE OrderStatus!=4 {0} GROUP BY MONTH(TourDate)";
            var sb = new StringBuilder();
            sb.AppendFormat(" and YEAR(TourDate)={0} and a.OrgID='{1}'", year, orgID);
            if (!string.IsNullOrEmpty(deptID))
                sb.AppendFormat(" and DeptID='{0}'", deptID);
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }
    }
}
