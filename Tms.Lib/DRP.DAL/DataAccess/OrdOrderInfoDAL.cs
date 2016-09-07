using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单相关的操作
    /// </summary>
    public class OrdOrderInfoDAL
    {
        /// <summary>
        /// 自主班团报名人数
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public int OrderVisitorNum(string tourID)
        {
            var sql = "SELECT ISNULL(SUM(a.VisitorNum),0) Num FROM Ord_OrderPrice a INNER JOIN dbo.Ord_OrderInfo b ON a.OrderID=b.ID WHERE a.IsSeat=1 AND b.OrderType=2 AND b.TourID='{0}' AND b.OrderStatus!=4";
            sql = string.Format(sql, tourID);
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql).ExecuteReader())
            {
                if (reader.Read())
                    return Convert.ToInt16(reader["Num"].ToString());
                else
                    return 0;
            }
        }

        /// <summary>
        /// 查询订单编号
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public List<string> QueryOrderNo(string[] orderIds)
        {
            var list = new List<string>();
            var sql = "Select OrderNo From Ord_OrderInfo where ID in ({0})";
            var arr = new List<string>();
            foreach (var id in orderIds)
            {
                arr.Add(string.Format("'{0}'", id));
            }
            sql = string.Format(sql, string.Join(",", arr));
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql).ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader["OrderNo"].ToString());
                }
            }
            return list;
        }
    }
}
