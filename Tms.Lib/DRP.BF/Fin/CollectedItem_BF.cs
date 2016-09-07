using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DRP.DAL;
using SubSonic.Query;
using DRP.Framework;
using System.Web.UI.WebControls;
using DRP.Framework.Core;
using DRP.BF.Order;
using System.Transactions;

namespace DRP.BF.Fin
{
    /// <summary>
    /// 收入状态
    /// </summary>
    public enum CollectedStatus
    {
        All = 0,
        /// <summary>
        /// 未认领
        /// </summary>
        NoneClaim = 1,
        /// <summary>
        /// 已认领
        /// </summary>
        Claimed = 2,
        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed = 3,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 4
    }

    /// <summary>
    /// 银行收入明细查询条件
    /// </summary>
    public class CollectionItemCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 认领状态
        /// </summary>
        public CollectedStatus DataStatus { get; set; }

        /// <summary>
        /// 收款银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 收款日期
        /// </summary>
        public string sDate { get; set; }

        /// <summary>
        /// 收款日期
        /// </summary>
        public string eDate { get; set; }

        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal MinIncome { get; set; }

        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal MaxIncome { get; set; }

        /// <summary>
        /// 来源银行名称
        /// </summary>
        public string FromBank { get; set; }

        /// <summary>
        /// 打款人户名
        /// </summary>
        public string FromAcct { get; set; }
    }


    /// <summary>
    /// 银行收入明细管理
    /// </summary>
    public class CollectedItem_BF
    {
        DRPDB db = new DRPDB();

        #region 导入收款明细

        private string[] Columns = { "交易日期", "交易时间", "收入金额", "交易行名", "摘要", "对方户名" };

        /// <summary>
        /// 导入银行收款
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int ImportData(DataTable dt, string bankName)
        {
            var batch = new BatchQuery();
            var sql = string.Empty;
            var provider = db.Provider;
            var user = AuthenticationPage.UserInfo;
            var idx = 0;
            try
            {
                bankName = bankName.Replace("$", "");
                foreach (DataRow row in dt.Rows)
                {
                    var tradeDate = row[Columns[0]].ToString();
                    var tradeTime = row[Columns[1]].ToString();
                    var incomeAmt = row[Columns[2]].ToString().ToDecimal();
                    var targetBank = row[Columns[3]].ToString();
                    var comment = row[Columns[4]].ToString();
                    var targetName = row[Columns[5]].ToString();
                    if (string.IsNullOrEmpty(tradeDate) && string.IsNullOrEmpty(tradeTime) && incomeAmt == 0)
                        continue;
                    DateTime? __tradeDate = null;
                    if (!string.IsNullOrEmpty(tradeDate))
                    {
                        if (tradeDate.Length == 8 && tradeDate.IndexOf("/") == -1)//格式：yyyyMMdd
                        {
                            tradeDate = tradeDate.Substring(0, 4) + "-" + tradeDate.Substring(4, 2) + "-" + tradeDate.Substring(6, 2);
                        }
                        var temp_date = DateTime.Now;
                        if (DateTime.TryParse(tradeDate, out temp_date))
                            __tradeDate = temp_date;
                    }
                    sql = @"INSERT INTO [Fin_CollectedItem]
                                   ([ID]
                                   ,[BankName]
                                   ,[TradeDate]
                                   ,[TradeTime]
                                   ,[Summary]
                                   ,[IncomeAmt] 
                                   ,[FromBank]
                                   ,[FromAcct]
                                   ,[DataStatus] 
                                   ,[Creator]
                                   ,[CreatorID]
                                   ,[CreateDate]
                                   ,[OrgID])
                             VALUES(@ID,@BankName, @TradeDate,@TradeTime,@Summary,@IncomeAmt,@FromBank,@FromAcct,@DataStatus,@Creator,@CreatorID,@CreateDate,@OrgID)";
                    var cmd = new QueryCommand(sql, provider);
                    cmd.Parameters.Add("@ID", Guid.NewGuid().ToString(), DbType.String);
                    cmd.Parameters.Add("@BankName", bankName, DbType.String);
                    cmd.Parameters.Add("@TradeDate", __tradeDate, DbType.DateTime);
                    cmd.Parameters.Add("@TradeTime", tradeTime, DbType.String);
                    cmd.Parameters.Add("@Summary", comment, DbType.String);
                    cmd.Parameters.Add("@IncomeAmt", incomeAmt, DbType.Decimal);
                    cmd.Parameters.Add("@FromBank", targetBank, DbType.String);
                    cmd.Parameters.Add("@FromAcct", targetName, DbType.String);
                    cmd.Parameters.Add("@DataStatus", 1, DbType.Int16);
                    cmd.Parameters.Add("@Creator", user.UserName, DbType.String);
                    cmd.Parameters.Add("@CreatorID", user.UserID, DbType.String);
                    cmd.Parameters.Add("@CreateDate", DateTime.Now, DbType.DateTime);
                    cmd.Parameters.Add("@OrgID", user.OrgID, DbType.String);
                    batch.QueueForTransaction(cmd);
                    idx++;
                }

                if (idx > 0)
                    batch.ExecuteTransaction();
            }
            catch (Exception ex)
            {
                idx = 0;
                BizUtility.ExceptionHandler(user, ex, "导入收款数据时发生错误");
            }
            return idx;
        }

        #endregion

        #region 银行收入流水明细查询

        public DAL.Fin_CollectedItem Get(string keyID)
        {
            return DAL.Fin_CollectedItem.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 收入明细查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string QueryCondition(CollectionItemCriteria qry)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and TradeDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and TradeDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd")); ;
            if ((int)qry.DataStatus > 0)
                sb.AppendFormat(" and DataStatus='{0}'", (int)qry.DataStatus);
            if (!string.IsNullOrEmpty(qry.BankName))
                sb.AppendFormat(" and BankName like '%{0}%'", qry.BankName);
            if (!string.IsNullOrEmpty(qry.FromBank))
                sb.AppendFormat(" and FromBank like '%{0}%'", qry.FromBank);
            if (!string.IsNullOrEmpty(qry.FromAcct))
                sb.AppendFormat(" and FromAcct like '%{0}%'", qry.FromAcct);
            if (qry.MinIncome > 0)
                sb.AppendFormat(" and IncomeAmt>='{0}'", qry.MinIncome);
            if (qry.MaxIncome > 0)
                sb.AppendFormat(" and IncomeAmt<='{0}'", qry.MaxIncome);
            return sb.ToString();
        }

        /// <summary>
        /// 收入明细查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryData(CollectionItemCriteria qry, out int record)
        {
            record = 0;
            var strWhere = QueryCondition(qry);
            var dt = db.GetPagination("Fin_CollectedItem", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
            return dt;
        }

        #endregion

        #region 删除银行收入明细项目
        /// <summary>
        /// 删除银行收入明细项目
        /// </summary>
        /// <remarks>只能删除未认领的项目</remarks>
        /// <param name="listID"></param>
        public void Delete(string[] IDs)
        {
            var sql = "Delete Fin_CollectedItem where OrgID=@OrgID and ID in (@ID) and DataStatus=1";
            new SubSonic.Query.CodingHorror(sql, AuthenticationPage.UserInfo.OrgID, string.Join(",", IDs)).Execute();
        }
        #endregion

        #region 手动增加银行收款项目

        /// <summary>
        ///保存收款流水
        /// </summary>
        /// <param name="basicType"></param>
        /// <param name="paraName"></param>
        /// <param name="orderIndex"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Fin_CollectedItem();
                    entity.OrgID = user.OrgID;
                    entity.CreatorID = user.UserID;
                    entity.Creator = user.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.OrgID = user.OrgID;
                    entity.DataStatus = 1;
                }
                entity = new ReflectHelper<DAL.Fin_CollectedItem>().AssignEntity(entity, pnlControl);
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "保存银行收款项目时发生错误");
            }
            return isSuccess;
        }

        #endregion

        #region 收款确认与取消确认

        /// <summary>
        /// 收款认领确认
        /// </summary>
        /// <param name="collectedItemID"></param>
        /// <param name="isSign">1：手动登记收款 0：认领方式收款</param>
        /// <param name="isFin">true:银行收款明细中确认</param>
        /// <returns></returns>
        public bool CollectedClaimConfirmed(string collectedItemID, bool isSign, bool isFin = false)
        {
            decimal toConfirmAmt = 0;
            var user = AuthenticationPage.UserInfo;
            try
            {
                if (isSign)
                {
                    #region 登记收款
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //1、收款明细表状态变更
                        var c = new OrderCollected_BF().Get(collectedItemID);
                        c.CollectStatus = (int)CollectedStatus.Confirmed;
                        c.AuditDate = DateTime.Now;
                        c.Auditor = user.UserName;
                        c.AuditorID = user.UserID;
                        c.Save();

                        //2、更新订单主表待确认金额与已确认金额
                        var order = new Order_BF().GetOrderInfo(c.OrderID);
                        if (order == null) //机票订单
                        {
                            var tOrder = new TicketOrder_BF().Get(c.OrderID);
                            tOrder.CollectedAmt = tOrder.CollectedAmt + tOrder.ToConfirmCollectedAmt;
                            toConfirmAmt = c.CollectAmt;
                            tOrder.ToConfirmCollectedAmt = 0;
                            tOrder.Save();
                        }
                        else //旅游订单
                        {
                            order.CollectedAmt = order.CollectedAmt + order.ToConfirmCollectedAmt;
                            toConfirmAmt = c.CollectAmt;
                            order.ToConfirmCollectedAmt = 0;
                            order.Save();
                        }


                        //3、更新订单的日志
                        new Order_BF().InsertLog(c.OrderID, "订单认领金额【" + toConfirmAmt + "】已确认");

                        scope.Complete();
                    }
                    #endregion
                }
                else  //认领方式收款
                {
                    #region 认领
                    if (isFin)
                        Fin_CollectedClaimConfirmed(collectedItemID, user);
                    else
                        Ord_CollectedClaimConfirmed(collectedItemID, user);
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "订单收款认领确认时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 订单收款确认[从订单中确认]
        /// </summary>
        /// <param name="collectedItemID"></param>
        /// <param name="user"></param>
        private void Ord_CollectedClaimConfirmed(string collectedItemID, UserInfo user)
        {
            decimal toConfirmAmt = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                //1、收款明细表状态变更
                var c = new OrderCollected_BF().Get(collectedItemID);
                c.CollectStatus = (int)CollectedStatus.Confirmed;
                c.AuditDate = DateTime.Now;
                c.Auditor = user.UserName;
                c.AuditorID = user.UserID;
                c.Save();

                //2、更新银行收款状态为已确认
                var e = Get(c.ClaimID);
                e.DataStatus = (int)CollectedStatus.Confirmed;
                e.Save();

                //3、更新订单主表待确认金额与已确认金额
                var order = new Order_BF().GetOrderInfo(e.OrderID);
                if (order == null) //机票订单
                {
                    var tOrder = new TicketOrder_BF().Get(e.OrderID);
                    tOrder.CollectedAmt = tOrder.CollectedAmt + tOrder.ToConfirmCollectedAmt;
                    toConfirmAmt = c.CollectAmt;
                    tOrder.ToConfirmCollectedAmt = 0;
                    tOrder.Save();
                }
                else //旅游订单
                {
                    order.CollectedAmt = order.CollectedAmt + order.ToConfirmCollectedAmt;
                    toConfirmAmt = c.CollectAmt;
                    order.ToConfirmCollectedAmt = 0;
                    order.Save();
                }

                //4、更新订单的日志
                new Order_BF().InsertLog(e.OrderID, "订单认领金额【" + toConfirmAmt + "】已确认");

                scope.Complete();
            }

        }

        /// <summary>
        /// 订单收款确认[从银行收款明细中确认]
        /// </summary>
        /// <param name="collectedItemID"></param>
        /// <param name="user"></param>
        private void Fin_CollectedClaimConfirmed(string collectedItemID, UserInfo user)
        {
            decimal toConfirmAmt = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                //1、更新银行收款状态为已确认
                var e = Get(collectedItemID);
                e.DataStatus = (int)CollectedStatus.Confirmed;
                e.Save();

                //2、收款明细表状态变更
                var c = new OrderCollected_BF().GetOrderCollection(collectedItemID, CollectedStatus.Claimed);
                c.CollectStatus = (int)CollectedStatus.Confirmed;
                c.AuditDate = DateTime.Now;
                c.Auditor = user.UserName;
                c.AuditorID = user.UserID;
                c.Save();


                //3、更新订单主表待确认金额与已确认金额
                var order = new Order_BF().GetOrderInfo(e.OrderID);
                if (order == null) //机票订单
                {
                    var tOrder = new TicketOrder_BF().Get(e.OrderID);
                    tOrder.CollectedAmt = tOrder.CollectedAmt + tOrder.ToConfirmCollectedAmt;
                    toConfirmAmt = c.CollectAmt;
                    tOrder.ToConfirmCollectedAmt = 0;
                    tOrder.Save();
                }
                else //旅游订单
                {
                    order.CollectedAmt = order.CollectedAmt + order.ToConfirmCollectedAmt;
                    toConfirmAmt = c.CollectAmt;
                    order.ToConfirmCollectedAmt = 0;
                    order.Save();
                }

                //4、更新订单的日志
                new Order_BF().InsertLog(e.OrderID, "订单认领金额【" + toConfirmAmt + "】已确认");

                scope.Complete();
            }

        }

        ///// <summary>
        ///// 收款认领批量确认
        ///// </summary>
        ///// <param name="collectedItemIDs"></param>
        ///// <returns></returns>
        //public void CollectedClaimConfirmed(string[] collectedItemIDs)
        //{
        //    foreach (var id in collectedItemIDs)
        //    {
        //        CollectedClaimConfirmed(id);
        //    }
        //}

        /// <summary>
        /// 取消已确认的收款，取消后可以再次认领
        /// </summary>
        /// <param name="collectedItemID"></param>
        /// <param name="isSign">1:手动登记收款  0：认领方式收款</param>
        /// <param name="isFin">true:银行收款明细中确认</param>
        /// <returns></returns>
        public bool CollectedConfirmedCanceled(string collectedItemID, bool isSign, bool isFin = false)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                if (isSign)
                {
                    #region 手动登记收款

                    using (TransactionScope scope = new TransactionScope())
                    {
                        //1、收款明细表状态变更
                        var c = new OrderCollected_BF().Get(collectedItemID);
                        c.CollectStatus = (int)CollectedStatus.Canceled;
                        c.AuditDate = DateTime.Now;
                        c.Auditor = user.UserName;
                        c.AuditorID = user.UserID;
                        c.Save();

                        //2、订单已确认的收款金额减少取消的金额
                        var order = new Order_BF().GetOrderInfo(c.OrderID);
                        if (order == null)
                        {
                            var tOrder = new TicketOrder_BF().Get(c.OrderID);
                            tOrder.CollectedAmt = tOrder.CollectedAmt - c.CollectAmt;
                            tOrder.ToConfirmCollectedAmt = 0;
                            tOrder.Save();
                        }
                        else
                        {
                            order.CollectedAmt = order.CollectedAmt - c.CollectAmt;
                            order.Save();
                        }

                        //3、更新订单的日志
                        new Order_BF().InsertLog(c.OrderID, "订单确认金额【" + c.CollectAmt + "】已取消");

                        scope.Complete();
                    }

                    #endregion
                }
                else
                {
                    #region 认领方式收款
                    if (isFin)
                        Fin_CollectedConfirmedCanceled(collectedItemID, user);
                    else
                        Ord_CollectedConfirmedCanceled(collectedItemID, user);
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "订单收款取消确认时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 从订单中取消收款确认
        /// </summary>
        /// <param name="collectedItemID"></param>
        /// <param name="user"></param>
        private void Ord_CollectedConfirmedCanceled(string collectedItemID, UserInfo user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //1、收款明细表状态变更
                var c = new OrderCollected_BF().Get(collectedItemID);
                c.CollectStatus = (int)CollectedStatus.Canceled;
                c.AuditDate = DateTime.Now;
                c.Auditor = user.UserName;
                c.AuditorID = user.UserID;
                c.Save();

                //2、取消已确认的收款回滚至未认领的状态，以便其他人可以再次认领
                var e = Get(c.ClaimID);
                e.DataStatus = (int)CollectedStatus.NoneClaim;
                e.OrderNo = e.OrderID = "";
                e.OrderType = 0;
                e.BillNo = "";
                e.ClaimUser = "";
                e.Save();

                //2、订单已确认的收款金额减少取消的金额
                var order = new Order_BF().GetOrderInfo(c.OrderID);
                if (order == null)
                {
                    var tOrder = new TicketOrder_BF().Get(c.OrderID);
                    tOrder.CollectedAmt = tOrder.CollectedAmt - e.IncomeAmt;
                    tOrder.ToConfirmCollectedAmt = 0;
                    tOrder.Save();
                }
                else
                {
                    order.CollectedAmt = order.CollectedAmt - e.IncomeAmt;
                    order.Save();
                }

                //4、更新订单的日志
                new Order_BF().InsertLog(c.OrderID, "订单确认金额【" + e.IncomeAmt + "】已取消");

                scope.Complete();
            }
        }

        /// <summary>
        /// 从银行收款明细中取消收款确认
        /// </summary>
        /// <param name="collectedItemID"></param>
        /// <param name="user"></param>
        private void Fin_CollectedConfirmedCanceled(string collectedItemID, UserInfo user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //1、取消已确认的收款回滚至未认领的状态，以便其他人可以再次认领
                var e = Get(collectedItemID);
                e.DataStatus = (int)CollectedStatus.NoneClaim;
                e.OrderNo = e.OrderID = "";
                e.OrderType = 0;
                e.BillNo = "";
                e.ClaimUser = "";
                e.Save();

                //1、收款明细表状态变更
                var c = new OrderCollected_BF().GetOrderCollection(collectedItemID, CollectedStatus.Confirmed);
                c.CollectStatus = (int)CollectedStatus.Canceled;
                c.AuditDate = DateTime.Now;
                c.Auditor = user.UserName;
                c.AuditorID = user.UserID;
                c.Save();

                //2、订单已确认的收款金额减少取消的金额
                var order = new Order_BF().GetOrderInfo(c.OrderID);
                if (order == null)
                {
                    var tOrder = new TicketOrder_BF().Get(c.OrderID);
                    tOrder.CollectedAmt = tOrder.CollectedAmt - e.IncomeAmt;
                    tOrder.ToConfirmCollectedAmt = 0;
                    tOrder.Save();
                }
                else
                {
                    order.CollectedAmt = order.CollectedAmt - e.IncomeAmt;
                    order.Save();
                }

                //4、更新订单的日志
                new Order_BF().InsertLog(c.OrderID, "订单确认金额【" + e.IncomeAmt + "】已取消");

                scope.Complete();
            }
        }



        /// <summary>
        /// 取消已认领的收款，取消后可以再次认领
        /// </summary>
        /// <param name="collectedItemID"></param>
        public bool CollectedClaimCanceled(string collectedItemID)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var e = Get(collectedItemID);
                var orderType = (OrderType)e.OrderType;
                var orderID = e.OrderID;
                if (e.DataStatus == (int)CollectedStatus.Claimed) //已认领的收款才能取消
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //1、取消已确认的收款回滚至未认领的状态，以便其他人可以再次认领
                        e.DataStatus = (int)CollectedStatus.NoneClaim;
                        e.OrderNo = e.OrderID = "";
                        e.OrderType = 0;
                        e.BillNo = "";
                        e.ClaimUser = "";
                        e.Save();

                        //2、订单待确认的收款金额减少取消的金额
                        if (orderType == OrderType.AirTicket)
                        {
                            var tOrder = new TicketOrder_BF().Get(orderID);
                            tOrder.ToConfirmCollectedAmt = tOrder.ToConfirmCollectedAmt - e.IncomeAmt;
                            tOrder.Save();
                        }
                        else
                        {
                            var order = new Order_BF().GetOrderInfo(orderID);
                            order.ToConfirmCollectedAmt = order.ToConfirmCollectedAmt - e.IncomeAmt;
                            order.Save();
                        }

                        //3、收款明细表状态变更
                        var c = new OrderCollected_BF().GetOrderCollection(collectedItemID, CollectedStatus.Claimed);
                        c.CollectStatus = (int)CollectedStatus.Canceled;
                        c.AuditDate = DateTime.Now;
                        c.Auditor = user.UserName;
                        c.AuditorID = user.UserID;
                        c.Save();

                        //4、更新订单的日志
                        new Order_BF().InsertLog(e.OrderID, "订单收款认领金额【" + e.IncomeAmt + "】已取消");

                        scope.Complete();
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "取消收款认领确认时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 收款确认（按订单ID确认）
        /// </summary>
        /// <param name="collectedItemID"></param>
        /// <returns></returns>
        public void CollectedConfirmed(string[] OrderIDs)
        {
            var user = AuthenticationPage.UserInfo;

            //查询已认领的收款
            foreach (var id in OrderIDs)
            {
                var list = DAL.Ord_OrderCollection.Find(x => x.OrgID == user.OrgID && x.OrderID == id && x.CollectStatus == (int)CollectedStatus.Claimed).ToList();
                list.ForEach(x =>
                {
                    var isSign = x.ClaimID == Guid.Empty.ToString();//登记收款还是认领收款
                    CollectedClaimConfirmed(isSign ? x.ID : x.ClaimID, isSign);
                });
            }
        }

        #endregion

        #region << 收款登记：销售登记->财务确认 >>
        /// <summary>
        /// 收款登记
        /// </summary>
        /// <param name="e"></param>
        /// <param name="status"></param>
        /// <param name="ordType"></param>
        /// <returns></returns>
        public bool OrderCollectedSign(string[] orderIds, DAL.Ord_OrderCollection e,
            OrderType ordType = OrderType.AirTicket, CollectedStatus status = CollectedStatus.Claimed)
        {
            var isRec = true;
            var user = AuthenticationPage.UserInfo;
            foreach (var orderID in orderIds)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (OrderType.AirTicket == ordType) //机票订单
                        {
                            #region 机票订单
                            var tOrder = new TicketOrder_BF().Get(orderID);

                            //增加订单收款明细                           
                            var c = new DAL.Ord_OrderCollection();
                            c.ID = Guid.NewGuid().ToString();
                            c.OrderID = orderID;
                            //收款金额：批量收款时，收款金额=未收款金额
                            c.CollectAmt = orderIds.Length == 1 ? e.CollectAmt : (tOrder.OrderAmt - tOrder.CollectedAmt - tOrder.ToConfirmCollectedAmt);
                            c.CollectType = e.CollectType;
                            c.CollectDate = e.CollectDate;
                            c.CollectBill = e.CollectBill;
                            c.Comment = e.Comment;
                            c.ClaimID = Guid.Empty.ToString();
                            c.SrcBank = e.SrcBank;
                            c.SrcName = e.SrcName;
                            c.CollectStatus = (int)status;
                            c.CreateUserID = user.UserID;
                            c.CreateUserName = user.UserName;
                            c.CreateDate = DateTime.Now;
                            c.OrgID = user.OrgID;
                            c.Save();

                            //更新订单状态 
                            tOrder.ToConfirmCollectedAmt = tOrder.ToConfirmCollectedAmt + c.CollectAmt;
                            tOrder.Save();
                            #endregion
                        }
                        else
                        {
                            #region 旅游订单

                            //订单信息
                            var order = new Order_BF().GetOrderInfo(orderID);

                            //增加订单收款明细                           
                            var c = new DAL.Ord_OrderCollection();
                            c.ID = Guid.NewGuid().ToString();
                            c.OrderID = orderID;
                            //收款金额：批量收款时，收款金额=未收款金额
                            c.CollectAmt = orderIds.Length == 1 ? e.CollectAmt : (order.OrderAmt - order.CollectedAmt - order.ToConfirmCollectedAmt);
                            c.CollectType = e.CollectType;
                            c.CollectDate = e.CollectDate;
                            c.CollectBill = e.CollectBill;
                            c.Comment = e.Comment;
                            c.ClaimID = Guid.Empty.ToString();
                            c.SrcBank = e.SrcBank;
                            c.SrcName = e.SrcName;
                            c.CollectStatus = (int)status;
                            c.CreateUserID = user.UserID;
                            c.CreateUserName = user.UserName;
                            c.CreateDate = DateTime.Now;
                            c.OrgID = user.OrgID;
                            c.Save();

                            //更新订单状态 
                            order.ToConfirmCollectedAmt = order.ToConfirmCollectedAmt + c.CollectAmt;
                            order.Save();

                            #endregion
                        }
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    BizUtility.ExceptionHandler(user, ex, "订单收款登记时发生错误");
                    isRec = false;
                }
                finally
                {
                    if (isRec)
                        new Order_BF().InsertLog(orderID, orderIds.Length == 1 ? "订单收款登记金额：" + e.CollectAmt : "订单批量收款金额：" + e.CollectAmt);
                }
            }
            return isRec;
        }
        #endregion

        public bool DeleteOrderCollectedList(string id, bool isSign, bool isFin = false)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                if (isSign)
                {
                    #region 手动登记收款

                    using (TransactionScope scope = new TransactionScope())
                    {
                        var c = new OrderCollected_BF().Get(id);
                        //2、订单已确认的收款金额减少取消的金额
                        var order = new Order_BF().GetOrderInfo(c.OrderID);

                        order.ToConfirmCollectedAmt = order.ToConfirmCollectedAmt - c.CollectAmt;
                        order.Save();

                        //3、更新订单的日志
                        new Order_BF().InsertLog(c.OrderID, "订单确认金额【" + c.CollectAmt + "】已取消");

                        scope.Complete();
                    }
                    #endregion
                }
                else
                {
                    #region 认领方式收款
                    if (isFin)
                        Fin_CollectedConfirmedCanceled(id, user);
                    else
                        Ord_CollectedConfirmedCanceled(id, user);
                    #endregion
                }

                DAL.Ord_OrderCollection.Delete(x => x.ID == id);
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "订单收款取消确认时发生错误");
                return false;
            }
        }

        public bool DeleteClaimCanceled(string id, string collectedItemID)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var e = Get(collectedItemID);
                var orderType = (OrderType)e.OrderType;
                var orderID = e.OrderID;
                if (orderID != "")
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //1、取消已确认的收款回滚至未认领的状态，以便其他人可以再次认领
                        e.DataStatus = (int)CollectedStatus.NoneClaim;
                        e.OrderNo = e.OrderID = "";
                        e.OrderType = 0;
                        e.BillNo = "";
                        e.ClaimUser = "";
                        e.Save();

                        //2、订单待确认的收款金额减少取消的金额
                        if (orderType == OrderType.AirTicket)
                        {
                            var tOrder = new TicketOrder_BF().Get(orderID);
                            tOrder.ToConfirmCollectedAmt = tOrder.ToConfirmCollectedAmt - e.IncomeAmt;
                            tOrder.Save();
                        }
                        else
                        {
                            var order = new Order_BF().GetOrderInfo(orderID);
                            order.ToConfirmCollectedAmt = order.ToConfirmCollectedAmt - e.IncomeAmt;
                            order.Save();
                        }

                        //4、更新订单的日志
                        new Order_BF().InsertLog(e.OrderID, "订单收款认领金额【" + e.IncomeAmt + "】已取消");

                        scope.Complete();
                    }
                }
                DAL.Ord_OrderCollection.Delete(x => x.ID == id);
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "取消收款认领确认时发生错误");
                return false;
            }
        }
    }
}
