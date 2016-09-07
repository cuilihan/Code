using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单发票管理
    /// </summary>
    public class OrdOrderInvoiceDAL
    {
        /// <summary>
        /// 查询订单，用于开票
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable QueryOrder(string[] orderIDs, string orgID)
        {
            var arr = new List<string>();
            foreach (var id in orderIDs)
                arr.Add(string.Format("'{0}'", id));
            var sql = "SELECT ID,OrderType,OrderStatus,OrderName,OrderNo,OrderAmt,CONVERT(NVARCHAR(10),TourDate,23) TourDate,CustomerName,OrderInvoiceAmt FROM V_Ord_OrderInvoiceQry where 1=1 and ID in ({0}) and OrgID=@OrgID";
            sql = string.Format(sql, string.Join(",", arr));
            return new SubSonic.Query.CodingHorror(sql, orgID).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 查询发票相关的订单
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable QueryInvoiceOrder(string invoiceID, string orgID)
        {
            var sql = "SELECT b.ID,b.OrderName,b.OrderNo,b.CustomerID,b.CustomerName,b.OrderAmt,b.OrderCost,b.OrderType,b.OrderStatus FROM Ord_OrderInvoiceItem a INNER JOIN dbo.Ord_OrderInfo b ON a.OrderID=b.ID WHERE a.InvoiceID=@InvoiceID AND b.OrgID=@OrgID";
            return new SubSonic.Query.CodingHorror(sql, invoiceID, orgID).ExecuteDataSet().Tables[0];
        }
    }
}
