using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 预决算
    /// </summary>
    public class BudgetDAL
    {
        /// <summary>
        /// 变更订单预决算状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dataStatus">-1：无效 1：预算 2：决算</param> 
        public void UpdateBudgetStatus(string orderId, int dataStatus)
        {
            var sql = "Update Ord_Budget Set DataStatus=@DataStatus where 1=1 and OrderID=@OrderID";
            new SubSonic.Query.CodingHorror(sql, dataStatus, orderId).Execute();
        }
    }
}
