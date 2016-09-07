using DRP.BF.Order;
using DRP.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace DRP.BF.CheckAccount
{
    public class OrderBalanceSettlementCriterial : QueryCriteriaBase
    {
        public string OrderName { get; set; }

        public string OrderNo { get; set; }

        public string GuideName { get; set; }

        public string GuideMobile { get; set; }

        public int DataStatus { get; set; }
    }

    /// <summary>
    /// 导游报账结算管理
    /// </summary>
    public class OrderBalanceSettlement_BF
    {

        /// <summary>
        /// 导游报账单确认(由已报账至已确认状态）
        /// </summary>
        /// <param name="orderGuideID"></param>
        /// <param name="orderBalanceID"></param>
        /// <returns></returns>
        public bool OrderBalanceConfirm(string orderGuideID, string bankName, string bankAcct, decimal balanceCost, string comment)
        {
            var orderID = "";
            var user = AuthenticationPage.UserInfo;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region << 1、更新导游报账单的状态（由已报账变更为已确认）>>

                    var orderGuide = DAL.Ord_OrderGuide.SingleOrDefault(x => x.ID == orderGuideID);
                    orderGuide.IsOver = 2;
                    orderGuide.Save();
                    orderID = orderGuide.OrderID;

                    #endregion

                    #region << 2、报账单确认后将对导游进行多退少补的结算 >>
                    var orderBalance = DAL.Ord_OrderBalance.SingleOrDefault(x => x.ID == orderGuide.OrderBalanceID);
                    var e = new DAL.Ord_OrderBalanceSettlement();
                    e.ID = Guid.NewGuid().ToString();
                    e.OrderID = orderGuide.OrderID;
                    e.OrderType = orderBalance.OrderType;
                    e.OrderBalanceID = orderBalance.ID;
                    e.OrgID = orderGuide.OrgID;
                    e.GuideName = orderGuide.GuideName;
                    e.GuideMobile = orderGuide.Mobile;
                    e.BankAcct = bankAcct;
                    e.BankName = bankName;
                    e.DrawMoneyAmt = orderBalance.YLTK == null ? 0 : (decimal)orderBalance.YLTK;
                    e.BalanceIncome = (orderBalance.XSTK == null ? 0 : (decimal)orderBalance.XSTK) + (orderBalance.QTSR == null ? 0 : (decimal)orderBalance.QTSR);
                    e.BalanceCost = balanceCost;
                    var settlementAmt = e.DrawMoneyAmt + e.BalanceIncome - e.BalanceCost;
                    var str = "报账单确认";
                    if (settlementAmt > 0) str = "余款入账";
                    if (settlementAmt < 0)
                    {
                        str = "垫资报销";
                        settlementAmt *= -1;
                    }
                    e.SettlementAmt = settlementAmt;
                    e.SettlementType = str;
                    e.Comment = comment;
                    e.DataStatus = 1;
                    e.CreateDate = DateTime.Now;
                    e.CreateUserID = user.UserID;
                    e.CreateUserName = user.UserName;
                    e.Save();

                    #endregion

                    scope.Complete();

                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "确认导游报账单时发生错误");
                orderID = "";
                return false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(orderID))
                    new Order_BF().InsertLog(orderID, "确认导游报账单");
            }
        }

        public DAL.Ord_OrderBalanceSettlement Get(string keyID)
        {
            return DAL.Ord_OrderBalanceSettlement.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 组合结算单查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string SettlementQueryCondition(OrderBalanceSettlementCriterial qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID); 
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.OrderNo))
                sb.AppendFormat(" and OrderNo like '%{0}%'", qry.OrderNo);          
            if (!string.IsNullOrEmpty(qry.GuideName))
                sb.AppendFormat(" and GuideName like '%{0}%'", qry.GuideName);
            if (!string.IsNullOrEmpty(qry.GuideMobile))
                sb.AppendFormat(" and GuideMobile like '%{0}%'", qry.GuideMobile);            
            if (qry.DataStatus > 0)
                sb.AppendFormat(" and DataStatus={0}", qry.DataStatus);
            return sb.ToString();
        }

        /// <summary>
        /// 报账单结算单查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryData(OrderBalanceSettlementCriterial qry, out int record)
        {
            var db = new DRPDB();
            return db.GetPagination("V_Fin_OrderBalanceSettlement", qry.pageIndex, qry.pageSize, out record, SettlementQueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 更新结算状态
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public bool UpdateStatus(string keyID,int status=2)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var e = Get(keyID);
                e.DataStatus = status;
                e.AuditDate = DateTime.Now;
                e.Auditor = user.UserName;
                e.Save();
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex);
                return false;
            }
        }
    }
}
