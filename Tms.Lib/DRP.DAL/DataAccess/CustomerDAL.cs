using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    public struct CustomerTrade
    {
        public decimal TradeAmt { get; set; }

        public int TradeNum { get; set; }
    }

    /// <summary>
    /// 客户管理
    /// </summary>
    public class CustomerDAL
    {
        /// <summary>
        /// 查询符合条件的所有数据，用于导出Excel
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public DataTable QueryAllData(string queryCondition)
        {
            var sql = "select * from Crm_Customer where " + queryCondition;
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }


        /// <summary>
        /// 客户的交易统计
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomerTrade GetTradeOrder(string customerID)
        {
            var sql = "SELECT COUNT(a.ID) TradeNum,ISNULL(SUM(OrderAmt),0) TradeAmt FROM Ord_OrderCustomer a INNER JOIN Ord_OrderInfo b ON a.OrderID=b.ID WHERE a.CustomerID=@CustomerID UNION ALL SELECT COUNT(a.ID) TradeNum,ISNULL(SUM(OrderAmt),0) TradeAmt FROM Ord_OrderCustomer a INNER JOIN Ord_TicketOrder b ON a.OrderID=b.ID WHERE a.CustomerID=@C";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, customerID,customerID).ExecuteReader())
            {
                var e = new CustomerTrade();

                while (reader.Read())
                {
                    e.TradeAmt += Convert.ToDecimal(reader["TradeAmt"].ToString());
                    e.TradeNum += Convert.ToInt16(reader["TradeNum"].ToString());
                }
                return e;
            }
        }
    }
}
