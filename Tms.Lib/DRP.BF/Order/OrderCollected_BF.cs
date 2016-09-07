using DRP.BF.Fin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace DRP.BF.Order
{
    /// <summary>
    /// 订单收款管理
    /// </summary>
    public class OrderCollected_BF
    {
        /// <summary>
        /// 订单收款记录详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Ord_OrderCollection Get(string keyID)
        {
            return DAL.Ord_OrderCollection.SingleOrDefault(x => x.ID == keyID);
        }


        /// <summary>
        /// 从认领记录查找收款详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Ord_OrderCollection GetOrderCollection(string claimID)
        {
            return DAL.Ord_OrderCollection.SingleOrDefault(x => x.ClaimID == claimID);
        }

        /// <summary>
        /// 从认领记录查找已认领的收款
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Ord_OrderCollection GetOrderCollection(string claimID, CollectedStatus status = CollectedStatus.Claimed)
        {
            return DAL.Ord_OrderCollection.SingleOrDefault(x => x.ClaimID == claimID && x.CollectStatus == (int)status);
        }



        /// <summary>
        /// 订单收款明细
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderCollection> GetOrderCollectedList(string orderID)
        {
            return DAL.Ord_OrderCollection.Find(x => x.OrderID == orderID).OrderBy(x => x.CollectDate).ToList();
        }
    }
}
