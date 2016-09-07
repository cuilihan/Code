using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单占座信息
    /// </summary>
    public class OrderSeatDAL
    {
        /// <summary>
        /// 本团已预订的座位号，释放已取消的订单
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderSeat> GetTourOrderSeat(string tourID)
        {
            var sql = "SELECT a.ID,a.OrderID,a.TourID,a.SeatNum FROM Ord_OrderSeat a INNER JOIN Ord_OrderInfo b ON a.OrderID=b.ID WHERE a.TourID=@TourID AND b.OrderStatus!=4";
            return new SubSonic.Query.CodingHorror(sql, tourID).ExecuteTypedList<DAL.Ord_OrderSeat>();
        }
    }
}
