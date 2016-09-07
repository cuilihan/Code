using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单成本
    /// </summary>
    public class OrderCostDAL
    {
        /// <summary>
        /// 同行散客订单成本（包含已付款金额）
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DataTable GetOrderCostItem(string orderID,string orgID)
        {
            var sql = "SELECT a.ItemType,a.ItemName,a.Supplier,a.CostAmt,a.Comment,a.OrderID,ISNULL(b.PaidAmt,0) PaidAmt FROM dbo.Ord_OrderCostItem a LEFT JOIN (SELECT OrderCostItemID,SUM(Amount) PaidAmt FROM Fin_OrderPayable GROUP BY OrderCostItemID ) AS b ON a.ID=b.OrderCostItemID WHERE a.OrgID=@orgID AND a.OrderID=@orderID";
            return new SubSonic.Query.CodingHorror(sql, orgID, orderID).ExecuteDataSet().Tables[0];
        }
    }
}
