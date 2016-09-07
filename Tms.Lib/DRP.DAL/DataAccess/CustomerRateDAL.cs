using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 客户增长率
    /// </summary>
    public class CustomerRateDAL
    {
        /// <summary>
        /// 客户增长率统计
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable CustomerRate(int year,string orgID)
        {
            var sql = "SELECT COUNT(1) iData,MONTH(CreateDate) M FROM Crm_Customer WHERE YEAR(CreateDate)=@year and OrgID=@OrgID GROUP BY MONTH(CreateDate)";
            return new SubSonic.Query.CodingHorror(sql, year, orgID).ExecuteDataSet().Tables[0];
        }
    }
}
