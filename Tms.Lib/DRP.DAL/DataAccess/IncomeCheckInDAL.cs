using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 非订单收入登记 
    /// </summary>
    public class IncomeCheckInDAL
    {
        /// <summary>
        /// 按收入类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable IncomeCheckInStatisticByIncomeType(string sDate, string eDate,string orgID)
        {
            var sql = "SELECT SUM(IncomeAmt) IncomeAmt,IncomeType FROM Fin_IncomeCheckIn Where 1=1 and OrgID='{0}'";
            sql = string.Format(sql, orgID);
            if (!string.IsNullOrEmpty(sDate))
                sql += string.Format(" and IncomeDate>='{0}'",sDate);
            if (!string.IsNullOrEmpty(eDate))
                sql += string.Format(" and IncomeDate<'{0}'",Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql += " GROUP BY IncomeType";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }


        /// <summary>
        /// 按收入部门统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable IncomeCheckInStatisticByDept(string sDate, string eDate,string orgID)
        {
            var sql = "SELECT SUM(IncomeAmt) IncomeAmt,DeptName FROM Fin_IncomeCheckIn Where 1=1 and OrgID='{0}'";
            sql = string.Format(sql, orgID);
            if (!string.IsNullOrEmpty(sDate))
                sql += string.Format(" and IncomeDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sql += string.Format(" and IncomeDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            sql += " GROUP BY DeptName";
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 按月份统计非订单收入
        /// </summary>
        /// <param name="year"></param>
        /// <param name="deptID"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable IncomeCheckInStatisticByMonth(int year, string deptID,string orgID)
        {
            var sql = "SELECT SUM(IncomeAmt) IncomeAmt,MONTH(IncomeDate) M,IncomeTypeID FROM dbo.Fin_IncomeCheckIn WHERE {0} GROUP BY MONTH(IncomeDate),IncomeTypeID";
            var sb = new StringBuilder();
            sb.AppendFormat("OrgID='{0}' AND YEAR(IncomeDate)={1}",orgID,year);
            if (!string.IsNullOrEmpty(deptID))
                sb.AppendFormat(" and DeptID='{0}'",deptID);
            sql = string.Format(sql, sb.ToString());
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }
    }
}
