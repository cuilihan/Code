using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 供应商付款发票
    /// </summary>
    public class SupplierInvoiceDAL
    {
        /// <summary>
        /// 供应商开票金额
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public decimal GetInvoiceAmt(string orderID, string orgID)
        {
            var sql = "SELECT SUM(InvoiceAmt) Amt FROM Ord_SupplierInvoice where OrgID=@OrgID and OrderID=@OrderID";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, orgID, orderID).ExecuteReader())
            {
                if (reader.Read())
                    return Convert.ToDecimal(reader["Amt"].ToString());
                else
                    return 0;
            }
        }
    }
}
