using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单来源统计
    /// </summary>
    public class OrderSourceDAL
    {
        /// <summary>
        /// 订单来源统计【利润、订单数量、订单金额】
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrderSourceStatistic(string sDate, string eDate, string orgID)
        {
            var sql = "SELECT SourceName,SUM(OrderAmt-ISNULL(OrderCost,0)) Profit,SUM(OrderAmt) OrderAmt,COUNT(1) OrderNum FROM Ord_OrderInfo Where 1=1 and OrgID='{0}' and OrderStatus!=4";
            sql = string.Format(sql, orgID);
            if (!string.IsNullOrEmpty(sDate))
                sql += string.Format(" and TourDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sql += string.Format(" and TourDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql += "  GROUP BY SourceName";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }
    }
}
