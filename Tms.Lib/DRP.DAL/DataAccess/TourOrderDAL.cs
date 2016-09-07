using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 自主班团订单
    /// </summary>
    public class TourOrderDAL
    {
        /// <summary>
        /// 自主班团订单的已收款
        /// </summary>
        /// <param name="tourID"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public decimal TourCollectedAmt(string tourID, string orgID)
        {
            var sql = "SELECT SUM(CollectAmt) Amt FROM dbo.Ord_OrderCollection a INNER JOIN dbo.Ord_OrderInfo b ON a.OrderID=b.ID WHERE b.TourID=@TourID AND b.OrgID=@OrgID AND a.CollectStatus=3";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, tourID, orgID).ExecuteReader())
            {
                if (reader.Read())
                {
                    var amt = reader["Amt"].ToString();
                    decimal a = 0;
                    decimal.TryParse(amt, out a);
                    return a;
                }
                return 0;
            }
        }



        /// <summary>
        /// 自主班团订单的应收款与人数
        /// </summary>
        /// <param name="tourID"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable TourOrderAmt(string tourID, string orgID)
        {
            var sql = "SELECT SUM(OrderAmt) OrderAmt,SUM(AdultNum) AdultNum,SUM(ChildNum) ChildNum FROM Ord_OrderInfo WHERE OrderType=2 AND TourID=@TourID and OrgID=@OrgID";
            return new SubSonic.Query.CodingHorror(sql, tourID, orgID).ExecuteDataSet().Tables[0];
        }
    }
}
