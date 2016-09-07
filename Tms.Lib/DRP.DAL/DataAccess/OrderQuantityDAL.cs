using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单成交量统计
    /// </summary>
    public class OrderQuantityDAL
    {
        /// <summary>
        /// 订单成交量统计
        /// </summary> 
        /// <returns></returns>
        public DataTable OrderQuantityStatistic(int year, int orderType, string orgID)
        {
            var sql = "";
            if (orderType > 0)
            {
                #region 按订单类型查询
                if (orderType != 4)
                {
                    #region 指定订单类型统计

                    if (orderType == 6) //机票
                    {
                        sql = "SELECT MONTH(TourDate) M,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,COUNT(1) OrderNum FROM dbo.Ord_TicketOrder a Where OrderStatus!=4 AND a.OrgID='{0}' AND YEAR(TourDate)={1} GROUP BY MONTH(TourDate)";
                        sql = string.Format(sql, orgID, year);
                    }
                    else
                    {
                        sql = "SELECT MONTH(TourDate) M,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,COUNT(1) OrderNum FROM Ord_OrderInfo a Where OrderStatus!=4 AND a.OrgID='{0}' AND YEAR(TourDate)={1} and OrderType={2} GROUP BY MONTH(TourDate)";
                        sql = string.Format(sql, orgID, year, orderType);
                    }
                    #endregion
                }
                else //自主班团订单统计
                {
                    sql = "SELECT MONTH(TourDate) M,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,COUNT(1) OrderNum FROM dbo.Ord_OrderExtend a INNER JOIN dbo.Pro_TourInfo b ON a.OrderID=b.ID WHERE 1=1 AND a.OrgID='{0}' AND YEAR(TourDate)={1} GROUP BY MONTH(TourDate)";
                    sql = string.Format(sql, orgID, year);
                }
                #endregion
            }
            else //不分类型
            {
                #region 所有订单
                sql = "SELECT * FROM(SELECT MONTH(TourDate) M,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,COUNT(1) OrderNum FROM Ord_OrderInfo a Where OrderStatus!=4 {0} GROUP BY MONTH(TourDate) UNION ALL SELECT MONTH(TourDate) M,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,COUNT(1) OrderNum FROM Ord_TicketOrder a Where OrderStatus!=4 {0} GROUP BY MONTH(TourDate) UNION ALL SELECT MONTH(TourDate) M,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,COUNT(1) OrderNum FROM dbo.Ord_OrderExtend a INNER JOIN dbo.Pro_TourInfo b ON a.OrderID=b.ID WHERE 1=1 {0} GROUP BY MONTH(TourDate)) AS T";
                var sb = new StringBuilder();
                sb.AppendFormat(" and a.OrgID='{0}' and YEAR(TourDate)={1}", orgID, year);
                sql = string.Format(sql, sb.ToString());
                #endregion
            }
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }


        /// <summary>
        /// 订单成交量统计(有权限过滤)-金额与利润
        /// </summary> 
        /// <returns></returns>
        public DataTable OrderQuantityStatisticByPermission(int year, int orderType, string orgID, string deptID, string userID)
        {
            var sql = "";
            if (orderType == 6) //机票订单
            {
                sql = "SELECT MONTH(TourDate) M,SUM(OrderAmt) OrderAmt,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit FROM Ord_TicketOrder Where 1=1 and OrgID=@OrgID and OrderStatus!=4 and YEAR(TourDate)=@Year";
                if (!string.IsNullOrEmpty(deptID))
                    sql += string.Format(" and DeptID='{0}'", deptID);
                if (!string.IsNullOrEmpty(userID))
                    sql += string.Format(" and CreateUserID='{0}'", userID);
                sql += " GROUP BY MONTH(TourDate)";
            }
            else
            {
                sql = "SELECT MONTH(TourDate) M,SUM(OrderAmt) OrderAmt,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit FROM Ord_OrderInfo Where 1=1 and OrgID=@OrgID and OrderStatus!=4 and YEAR(TourDate)=@Year";

                if (orderType > 0)
                    sql += string.Format(" and OrderType={0}", orderType);
                if (!string.IsNullOrEmpty(deptID))
                    sql += string.Format(" and DeptID='{0}'", deptID);
                if (!string.IsNullOrEmpty(userID))
                    sql += string.Format(" and CreateUserID='{0}'", userID);

                sql += " GROUP BY MONTH(TourDate)";
            }
            return new SubSonic.Query.CodingHorror(sql, orgID, year).ExecuteDataSet().Tables[0];
        }
    }
}
