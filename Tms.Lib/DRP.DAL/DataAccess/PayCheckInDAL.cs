using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 非订单支出登记 
    /// </summary>
    public class PayCheckInDAL
    {
        /// <summary>
        /// 按支出类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable PayCheckInStatisticByPayType(string sDate, string eDate,string orgID)
        {
            var sql = "SELECT SUM(PayAmt) PayAmt,PayType FROM Fin_PayCheckIn Where 1=1 and OrgID='{0}'";
            sql = string.Format(sql, orgID);
            if (!string.IsNullOrEmpty(sDate))
                sql += string.Format(" and PayDate>='{0}'",sDate);
            if (!string.IsNullOrEmpty(eDate))
                sql += string.Format(" and PayDate<'{0}'",Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql += " GROUP BY PayType";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }


        /// <summary>
        /// 按支出部门统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable PayCheckInStatisticByDept(string sDate, string eDate,string orgID)
        {
            var sql = "SELECT SUM(PayAmt) PayAmt,DeptName FROM Fin_PayCheckIn Where 1=1 and OrgID='{0}'";
            sql = string.Format(sql, orgID);
            if (!string.IsNullOrEmpty(sDate))
                sql += string.Format(" and PayDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sql += string.Format(" and PayDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql += " GROUP BY DeptName";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 按月份统计非订单支出
        /// </summary>
        /// <param name="year"></param>
        /// <param name="deptID"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable PayCheckInStatisticByMonth(int year, string deptID, string orgID)
        {
            var sql = "SELECT SUM(PayAmt) PayAmt,MONTH(PayDate) M,PayTypeID FROM Fin_PayCheckIn WHERE {0} GROUP BY MONTH(PayDate),PayTypeID";
            var sb = new StringBuilder();
            sb.AppendFormat("OrgID='{0}' AND YEAR(PayDate)={1}", orgID, year);
            if (!string.IsNullOrEmpty(deptID))
                sb.AppendFormat(" and DeptID='{0}'", deptID);
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }
    }
}
