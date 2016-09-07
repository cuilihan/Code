using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    ///运维统计报表
    /// </summary>
    public class OmRptDAL
    {
        /// <summary>
        /// 按地区统计机构数
        /// </summary>
        /// <returns></returns>
        public DataTable OrgStatistic()
        {
            var sql = "SELECT COUNT(a.ID) iCount,AreaName,SUM(b.UserNum) UserNum  FROM Om_OrgInfo a LEFT JOIN(SELECT COUNT(ID) UserNum,OrgID FROM Sys_UserInfo GROUP BY OrgID) AS b ON a.ID=b.OrgID GROUP BY AreaName";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 机构月度开通情况统计
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public DataTable OrgOpenQuantityStatistic(int y)
        {
            var sql = "SELECT MONTH(CreateDate) M,COUNT(1) iCount FROM Om_OrgInfo WHERE YEAR(CreateDate)=@Y GROUP BY MONTH(CreateDate)";
            return new SubSonic.Query.CodingHorror(sql, y).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 机构的订单量统计
        /// </summary>
        /// <param name="orgName"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrgOrderQuantityStatistic(string orgName, string sDate, string eDate)
        {
            var sql = "SELECT ISNULL(a.iCount,0) iCount,b.Name,b.AreaName,b.CreateDate,b.ExpiryDate,OrgContact,ContactPhone FROM(SELECT SUM(t.iCount) iCount,OrgID FROM(SELECT COUNT(1) iCount, OrgID FROM Ord_OrderInfo WHERE 1=1 {0} GROUP BY OrgID UNION ALL SELECT COUNT(1) iCount, OrgID FROM Ord_TicketOrder WHERE 1=1 {0} GROUP BY OrgID) AS t GROUP BY t.OrgID) AS a RIGHT JOIN Om_OrgInfo b ON a.OrgID=b.ID";
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and CreateDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql = string.Format(sql, sb.ToString());
            if (!string.IsNullOrEmpty(orgName))
                sql += string.Format(" where Name like '%{0}%'", orgName);
            sql += " ORDER BY iCount DESC ";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 订单的应收款与人数的统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrderReceivableStatistic(string sDate, string eDate)
        {
            var sql = "SELECT SUM(OrderAmt) OrderAmt,SUM(ChildNum+ChildNum) VisitorNum,Count(1) OrderNum,MONTH(CreateDate) M FROM Ord_OrderInfo WHERE OrderStatus!=4 {0} GROUP BY MONTH(CreateDate) UNION ALL SELECT SUM(OrderAmt) OrderAmt,SUM(ChildNum) VisitorNum,Count(1) OrderNum,MONTH(CreateDate) M FROM Ord_TicketOrder WHERE OrderStatus!=4 {0} GROUP BY MONTH(CreateDate)";
            var sb = new StringBuilder();
            sb.Append(" and 1=1");
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and CreateDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 针对机构收取的服务费用统计 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable OrgReceiptAmoutStatistic(int year)
        {
            var sql = "SELECT MONTH(ReceiveDate) M,SUM(PaidAmt) Amt FROM Om_OrgReceipt WHERE YEAR(ReceiveDate)=@year GROUP BY MONTH(ReceiveDate)";
            return new SubSonic.Query.CodingHorror(sql, year).ExecuteDataSet().Tables[0];
        }
    }
}
