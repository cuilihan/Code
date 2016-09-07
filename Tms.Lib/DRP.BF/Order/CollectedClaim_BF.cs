using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.BF.Fin;
using System.Xml;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.BF.Order
{
    /// <summary>
    /// 收款认领
    /// </summary>
    public class CollectedClaim_BF
    {
        /// <summary>
        /// 检查收款状态，确认没有被认领
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns>True:未认领 False:已认领</returns>
        public bool CheckCollectedNoneClaimed(string keyID)
        {
            var e = new CollectedItem_BF().Get(keyID);
            return e.DataStatus == (int)CollectedStatus.NoneClaim;
        }

        /// <summary>
        /// 保存收款认领:单个订单收款可以使用
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="orderID"></param>
        /// <param name="billNo"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool SaveClaim(string claimID, string orderID, string billNo, string comment, OrderType ordType)
        {
            var isRec = true;
            decimal claimAmt = 0;
            var user = AuthenticationPage.UserInfo;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (OrderType.AirTicket == ordType) //机票订单
                    {
                        #region 机票订单
                        var tOrder = new TicketOrder_BF().Get(orderID);

                        //更新银行收款认领状态
                        var e = new CollectedItem_BF().Get(claimID);
                        e.DataStatus = (int)CollectedStatus.Claimed;
                        e.ClaimUser = AuthenticationPage.UserInfo.UserName;
                        e.BillNo = billNo;
                        e.Comment = comment;
                        e.OrderID = orderID;
                        e.OrderNo = tOrder.OrderNo;
                        e.OrderType = (int)ordType;
                        e.Save();

                        //增加订单收款明细
                        var c = new DAL.Ord_OrderCollection();
                        c.ID = Guid.NewGuid().ToString();
                        c.OrderID = orderID;
                        c.CollectAmt = e.IncomeAmt;
                        c.CollectType = "";
                        c.CollectDate = e.TradeDate;
                        c.CollectBill = e.BillNo;
                        c.Comment = e.Comment;
                        c.ClaimID = e.ID;
                        c.SrcBank = e.BankName;
                        c.SrcName = e.FromAcct;
                        c.CollectStatus = (int)CollectedStatus.Claimed;
                        c.CreateUserID = user.UserID;
                        c.CreateUserName = user.UserName;
                        c.CreateDate = DateTime.Now;
                        c.OrgID = user.OrgID;
                        c.Save();

                        //更新订单状态 
                        tOrder.ToConfirmCollectedAmt = tOrder.ToConfirmCollectedAmt + e.IncomeAmt;
                        tOrder.Save();

                        claimAmt = e.IncomeAmt;
                        #endregion
                    }
                    else
                    {
                        #region 旅游订单

                        //订单信息
                        var order = new Order_BF().GetOrderInfo(orderID);

                        //更新银行收款认领状态
                        var e = new CollectedItem_BF().Get(claimID);
                        e.DataStatus = (int)CollectedStatus.Claimed;
                        e.ClaimUser = AuthenticationPage.UserInfo.UserName;
                        e.BillNo = billNo;
                        e.Comment = comment;
                        e.OrderID = orderID;
                        e.OrderNo = order.OrderNo;
                        e.OrderType = order.OrderType;
                        e.Save();

                        //增加订单收款明细
                        var c = new DAL.Ord_OrderCollection();
                        c.ID = Guid.NewGuid().ToString();
                        c.OrderID = orderID;
                        c.CollectAmt = e.IncomeAmt;
                        c.CollectType = "";
                        c.CollectDate = e.TradeDate;
                        c.CollectBill = e.BillNo;
                        c.Comment = e.Comment;
                        c.ClaimID = e.ID;
                        c.SrcBank = e.BankName;
                        c.SrcName = e.FromAcct;
                        c.CollectStatus = (int)CollectedStatus.Claimed;
                        c.CreateUserID = user.UserID;
                        c.CreateUserName = user.UserName;
                        c.CreateDate = DateTime.Now;
                        c.OrgID = user.OrgID;
                        c.Save();

                        //更新订单状态 
                        order.ToConfirmCollectedAmt = order.ToConfirmCollectedAmt + e.IncomeAmt;
                        order.Save();

                        claimAmt = e.IncomeAmt;
                        #endregion
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "订单收款认领时发生错误");
                isRec = false;
            }
            finally
            {
                if (isRec)
                    new Order_BF().InsertLog(orderID, "收款认领金额：" + claimAmt);
            }
            return isRec;
        }

        #region 收款认领@2014-12-30（支持批量收款）--最终不能使用，原因是取消认领时有问题，不能回到每一笔的订单上

        /// <summary>
        /// 收款订单结构
        /// </summary>
        private struct OrderStruct
        {
            public string OrderID { get; set; }

            public string OrderNo { get; set; }

            public OrderType OrdType { get; set; }

            public decimal OrderAmt { get; set; }
        }

        /// <summary>
        /// 保存收款认领（修改版本：支持批量收款）
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="orderID"></param>
        /// <param name="billNo"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool SaveClaim(string claimID, string orderInfo, string billNo, string comment, bool isBatchColleced)
        {
            var isRec = true;
            var user = AuthenticationPage.UserInfo;
            try
            {
                #region 获取订单结构
                var list = new List<OrderStruct>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(orderInfo);
                var nodes = doc.SelectNodes("document/data");
                foreach (XmlNode node in nodes)
                {
                    var e = new OrderStruct();
                    e.OrderAmt = node.GetNodeValue("orderAmt").ToDecimal();
                    e.OrderID = node.GetNodeValue("orderID");
                    e.OrdType = (OrderType)node.GetNodeValue("orderType").ToInt();
                    e.OrderNo = node.GetNodeValue("orderNo");
                    list.Add(e);
                }
                #endregion

                #region 保存收款
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 更新银行收款认领状态
                    var orderID = "";
                    var orderNo = "";
                    var orderType = OrderType.All;
                    if (isBatchColleced)
                    {
                        var orderIDArr = new List<string>();
                        var orderNoArr = new List<string>();
                        list.ForEach(o =>
                        {
                            orderIDArr.Add(o.OrderID);
                            orderNoArr.Add(o.OrderNo);
                        });
                        orderID = string.Join(",", orderIDArr);
                        orderNo = string.Join(",", orderNoArr);
                    }
                    else
                    {
                        var order = list.First();
                        orderID = order.OrderID;
                        orderNo = order.OrderNo;
                        orderType = order.OrdType;
                    }

                    var e = new CollectedItem_BF().Get(claimID);
                    e.DataStatus = (int)CollectedStatus.Claimed;
                    e.ClaimUser = AuthenticationPage.UserInfo.UserName;
                    e.BillNo = billNo;
                    e.Comment = comment;
                    e.OrderID = orderID;
                    e.OrderNo = orderNo;
                    e.OrderType = (int)orderType;
                    e.BatchCollected = isBatchColleced;
                    e.Save();
                    #endregion

                    list.ForEach(x =>
                    {
                        #region 更新订单状态
                        if (x.OrdType == OrderType.AirTicket)
                        {
                            #region 机票订单
                            var tOrder = new TicketOrder_BF().Get(x.OrderID);

                            #region  增加订单收款明细
                            var c = new DAL.Ord_OrderCollection();
                            c.ID = Guid.NewGuid().ToString();
                            c.OrderID = x.OrderID;
                            if (isBatchColleced) //批量收款时订单的收款金额=未收款金额
                                c.CollectAmt = tOrder.OrderAmt - tOrder.CollectedAmt - tOrder.ToConfirmCollectedAmt;
                            else
                                c.CollectAmt = e.IncomeAmt;
                            c.CollectType = "";
                            c.CollectDate = e.TradeDate;
                            c.CollectBill = e.BillNo;
                            c.Comment = e.Comment;
                            c.ClaimID = e.ID;
                            c.SrcBank = e.BankName;
                            c.SrcName = e.FromAcct;
                            c.CollectStatus = (int)CollectedStatus.Claimed;
                            c.CreateUserID = user.UserID;
                            c.CreateUserName = user.UserName;
                            c.CreateDate = DateTime.Now;
                            c.OrgID = user.OrgID;
                            c.Save();
                            #endregion

                            tOrder.ToConfirmCollectedAmt = tOrder.ToConfirmCollectedAmt + c.CollectAmt;
                            tOrder.Save();

                            #endregion
                        }
                        else //旅游订单
                        {
                            #region 旅游订单
                            var order = new Order_BF().GetOrderInfo(x.OrderID);

                            #region  增加订单收款明细
                            var c = new DAL.Ord_OrderCollection();
                            c.ID = Guid.NewGuid().ToString();
                            c.OrderID = x.OrderID;
                            if (isBatchColleced) //批量收款时订单的收款金额=未收款金额
                                c.CollectAmt = order.OrderAmt - order.CollectedAmt - order.ToConfirmCollectedAmt;
                            else
                                c.CollectAmt = e.IncomeAmt;
                            c.CollectType = "";
                            c.CollectDate = e.TradeDate;
                            c.CollectBill = e.BillNo;
                            c.Comment = e.Comment;
                            c.ClaimID = e.ID;
                            c.SrcBank = e.BankName;
                            c.SrcName = e.FromAcct;
                            c.CollectStatus = (int)CollectedStatus.Claimed;
                            c.CreateUserID = user.UserID;
                            c.CreateUserName = user.UserName;
                            c.CreateDate = DateTime.Now;
                            c.OrgID = user.OrgID;
                            c.Save();
                            #endregion

                            order.ToConfirmCollectedAmt = order.ToConfirmCollectedAmt + c.CollectAmt;
                            order.Save();
                            #endregion
                        }
                        #endregion

                        #region 日志
                        var log = new DAL.Ord_OrderLog();
                        log.ID = Guid.NewGuid().ToString();
                        log.OrderID = x.OrderID;
                        log.Title = "订单认领款：" + e.IncomeAmt;
                        log.OpIP = IP.GetClientIp();
                        log.OpBrowser = IP.GetBrowserType();
                        log.CreateDate = DateTime.Now;
                        log.OrgID = user.OrgID;
                        log.CreateUserID = user.UserID;
                        log.CreateUserName = user.UserName;
                        log.Save();
                        #endregion

                    });
                    scope.Complete();
                }
                #endregion
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "订单收款认领时发生错误");
                isRec = false;
            }
            return isRec;
        }

        #endregion
    }
}
