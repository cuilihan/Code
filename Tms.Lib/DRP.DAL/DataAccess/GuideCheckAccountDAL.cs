using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DRP.DAL.Model;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 订单人数
    /// </summary>
    public class OrderVisitorNum
    {
        public int AdultNum { get; set; }

        public int ChildNum { get; set; }
    }

    /// <summary>
    /// 导游报账
    /// </summary>
    public class GuideCheckAccountDAL
    {
        /// <summary>
        /// 导游报账登录
        /// </summary>
        /// <param name="guideMobile"></param>
        /// <param name="pwd"></param>
        /// <param name="orgID"></param>
        /// <returns>0：登录失败 1：只有一个报账订单  2：多个报账订单</returns>
        public int Login(string guideMobile, string pwd, string orgID)
        {
            var sql = "Select count(1) iCount from Ord_OrderGuide where 1=1 and OrgID=@orgID and Mobile=@mobile and AcctPwd=@pwd";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, orgID, guideMobile, pwd).ExecuteReader())
            {
                if (reader.Read())
                    return Convert.ToInt16(reader["iCount"]);
                else
                    return 0;
            }
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="guideMobile"></param>
        /// <param name="acctPwd"></param>
        /// <param name="orgID"></param>
        /// <param name="flag">0：本月 1：一月前</param>
        /// <returns></returns>
        public List<V_Ord_CheckAccountEntity> GetOrderCheckAccount(string guideMobile, string orgID, string pwd, int flag)
        {
            var sql = "select * from V_Ord_CheckAccount where Mobile=@Mobile and OrgID=@OrgID and AcctPwd=@AcctPwd";
            if (flag == 0)
                sql = sql + " and DATEDIFF(MM,TourDate,GETDATE())<=0";
            else
                sql = sql + " and DATEDIFF(MM,TourDate,GETDATE())>0";
            sql += " Order By TourDate Desc";
            return new SubSonic.Query.CodingHorror(sql, guideMobile, orgID, pwd).ExecuteTypedList<V_Ord_CheckAccountEntity>();
        }

        /// <summary>
        /// 按订单计算导游报账项目的金额，待导入至预算表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<GuideCheckAccountEntity> OrderCheckAccountItem(string orderID)
        {
            var sql = "SELECT SUM(a.ItemAmt) ItemAmt,b.ItemName FROM(SELECT SUM(ItemAmt) ItemAmt,ItemID FROM Ord_OrderBalanceItemData a GROUP BY a.ItemID) AS a INNER JOIN Ord_OrderBalanceItem b ON a.ItemID=b.ID INNER JOIN Ord_OrderBalance c ON b.OrderBalanceID=c.ID WHERE c.OrderID=@orderID AND a.ItemAmt>0 GROUP BY b.ItemName";
            return new SubSonic.Query.CodingHorror(sql, orderID).ExecuteTypedList<GuideCheckAccountEntity>();
        }

        /// <summary>
        /// 导游报账游客人数
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderVisitorNum OrderCheckAccountVisitorNum(string orderID)
        {
            var e = new OrderVisitorNum();
            var sql = "SELECT SUM(AdultNum) AdultNum,SUM(ChildNum) ChildNum FROM dbo.Ord_OrderBalance WHERE OrderID=@OrderID";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, orderID).ExecuteReader())
            {
                if (reader.Read())
                {
                    var strAdult = reader["AdultNum"].ToString();
                    var strChild = reader["ChildNum"].ToString();
                    if (!string.IsNullOrEmpty(strAdult)) 
                        e.AdultNum = Convert.ToInt16(strAdult); 
                    if (!string.IsNullOrEmpty(strChild))
                        e.ChildNum = Convert.ToInt16(strChild); 
                }
            }
            return e;
        }

        /// <summary>
        /// 报账单成本总额
        /// </summary>
        /// <param name="orderBalanceID"></param>
        /// <returns></returns>
        public decimal OrderCheckAccountCostAmount(string orderBalanceID)
        {
            var sql = "SELECT SUM(ItemAmt) Amt FROM dbo.Ord_OrderBalanceItemData WHERE OrderBalanceID=@orderBalanceID";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, orderBalanceID).ExecuteReader())
            {
                if (reader.Read())
                    return Convert.ToDecimal(reader["Amt"].ToString());
                else
                    return 0;
            }
        }
    }
}
