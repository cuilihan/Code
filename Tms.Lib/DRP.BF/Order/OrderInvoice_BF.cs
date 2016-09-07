using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.DAL;
using DRP.DAL.DataAccess;

namespace DRP.BF.Order
{
    /// <summary>
    /// 开票订单项目
    /// </summary>
    public struct OrderInvoiceItem
    {
        public string OrderID { get; set; }

        public decimal InvoiceAmt { get; set; }
    }

    /// <summary>
    /// 发票状态
    /// </summary>
    public enum InvoiceStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 申请开票
        /// </summary>
        Submit = 1,
        /// <summary>
        /// 已开票
        /// </summary>
        Done,
        /// <summary>
        /// 拒绝开票
        /// </summary>
        Deny,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled
    }

    /// <summary>
    /// 发票查询条件
    /// </summary>
    public class InvoiceCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 开票状态
        /// </summary>
        public InvoiceStatus Status { get; set; }

        /// <summary>
        /// 发票编号
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 开票状态
        /// </summary>
        public string InvoiceName { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateScope InvoiceDate { get; set; }
    }

    /// <summary>
    /// 订单发票管理
    /// </summary>
    public class OrderInvoice_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public DataTable QueryOrder(string orderID)
        {
            return new OrdOrderInvoiceDAL().QueryOrder(orderID.Split(','), AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 根据发票ID 查询相关的订单
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <returns></returns>
        public DataTable QueryInvoiceOrder(string invoiceID)
        {
            return new OrdOrderInvoiceDAL().QueryInvoiceOrder(invoiceID, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 提交发票申请
        /// </summary>
        /// <param name="e"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool SaveInvoice(OrderType ordType, DAL.Ord_OrderInvoice e, List<OrderInvoiceItem> list)
        {
            var user = AuthenticationPage.UserInfo;
            var orderDAL = new Order_BF();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    decimal invoiceAmt = 0;
                    var orderName = "";

                    #region 发票相关联的订单表[Ord_OrderInvoiceItem]
                    list.ForEach(x =>
                    {
                        var item = new DAL.Ord_OrderInvoiceItem();
                        item.ID = Guid.NewGuid().ToString();
                        item.OrderID = x.OrderID;
                        item.InvoiceID = e.ID;
                        item.InvoiceAmt = x.InvoiceAmt;
                        item.OrgID = user.OrgID;
                        item.Save();


                        //订单日志
                        orderDAL.InsertLog(e.ID, "申请开票金额：" + x.InvoiceAmt);

                        invoiceAmt += x.InvoiceAmt;

                        if (string.IsNullOrEmpty(orderName))//多个订单时只显示第一个订单的名称
                        {
                            if (ordType == OrderType.AirTicket)
                                orderName = new TicketOrder_BF().Get(x.OrderID).OrderName;
                            else
                            {
                                var order = orderDAL.GetOrderInfo(x.OrderID);
                                orderName = order.OrderName;
                            }
                        }
                    });
                    #endregion

                    #region 发票主表
                    var entity = new DAL.Ord_OrderInvoice();
                    entity.ID = e.ID;
                    entity.InvoiceName = e.InvoiceName;
                    entity.InvoiceAmt = invoiceAmt;
                    entity.InvoiceItem = e.InvoiceItem;
                    entity.FetchType = e.FetchType;
                    entity.InvoiceUnit = e.InvoiceUnit;
                    entity.InvoiceStatus = (int)InvoiceStatus.Submit;
                    entity.Comment = e.Comment;
                    entity.IsOverAmt = e.IsOverAmt;
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                    entity.CreateUserName = user.UserName;
                    entity.DeptID = user.DeptID;
                    entity.OrgID = user.OrgID;
                    entity.OrderNum = list.Count;
                    entity.OrderName = orderName;
                    entity.Save();
                    #endregion

                    scope.Complete();

                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "提交开票申请时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 发票的状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string ConvertInvoiceStatus(InvoiceStatus status)
        {
            var s = "";
            switch (status)
            {
                case InvoiceStatus.Submit:
                    s = "申请中";
                    break;
                case InvoiceStatus.Done:
                    s = "已开票";
                    break;
                case InvoiceStatus.Deny:
                    s = "已退回";
                    break;
                case InvoiceStatus.Canceled:
                    s = "已作废";
                    break;
            }
            return s;
        }

        /// <summary>
        /// 开发票
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool CreateInvoice(string keyID, string invoiceNo, string invoiceDate, string comment)
        {
            var user = AuthenticationPage.UserInfo;
            var e = GetInvoice(keyID);
            if (e == null) return false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    e.InvoiceStatus = (int)InvoiceStatus.Done;
                    e.InvoiceNo = invoiceNo;
                    if (!string.IsNullOrEmpty(invoiceDate))
                        e.InvoiceDate = Convert.ToDateTime(invoiceDate);
                    e.AuditDate = DateTime.Now;
                    e.Auditor = user.UserName;
                    e.AuditorID = user.DeptID;
                    e.AuditRemark = comment;
                    e.Save();

                    //更新订单开票金额
                    var orderDAL = new Order_BF();
                    var list = DAL.Ord_OrderInvoiceItem.Find(x => x.InvoiceID == e.ID).ToList();
                    list.ForEach(x =>
                    {
                        var order = orderDAL.GetOrderInfo(x.OrderID);
                        if (order == null)
                        {
                            var tOrder = new TicketOrder_BF().Get(x.OrderID);
                            tOrder.OrderInvoiceAmt = tOrder.OrderInvoiceAmt + x.InvoiceAmt;
                            tOrder.Save();
                        }
                        else
                        {
                            order.OrderInvoiceAmt = order.OrderInvoiceAmt + x.InvoiceAmt;
                            order.Save();
                        }
                    });

                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "开具发票时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 发票作废退回或退回开票申请
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="status"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool InvoiceOperate(string keyID, InvoiceStatus status, string comment)
        {
            var user = AuthenticationPage.UserInfo;
            var e = GetInvoice(keyID);
            if (e == null) return false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    e.InvoiceStatus = (int)status;
                    e.AuditDate = DateTime.Now;
                    e.Auditor = user.UserName;
                    e.AuditorID = user.DeptID;
                    e.AuditRemark = comment;
                    e.Save();

                    //作废时更新订单开票金额（减少作废的金额)
                    if (status == InvoiceStatus.Canceled)
                    {
                        var orderDAL = new Order_BF();
                        var list = DAL.Ord_OrderInvoiceItem.Find(x => x.InvoiceID == e.ID).ToList();
                        list.ForEach(x =>
                        {
                            var order = orderDAL.GetOrderInfo(x.OrderID);
                            if (order == null)
                            {
                                var tOrder = new TicketOrder_BF().Get(x.OrderID);
                                tOrder.OrderInvoiceAmt = tOrder.OrderInvoiceAmt - x.InvoiceAmt;
                                tOrder.Save();
                            }
                            else
                            {
                                order.OrderInvoiceAmt = order.OrderInvoiceAmt - x.InvoiceAmt;
                                order.Save();
                            }
                        });
                    }

                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "发票操作时发生错误");
                return false;
            }
        }

        #region << 发票查询 >>

        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string QueryCondition(InvoiceCriteria qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);

            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion

            if (qry.QueryDateScope != null)
            {
                if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                    sb.AppendFormat(" and InvoiceDate>='{0}'", qry.QueryDateScope.sDate);
                if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                    sb.AppendFormat(" and InvoiceDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            }
            if (qry.Status != InvoiceStatus.All)
                sb.AppendFormat(" and InvoiceStatus={0}", (int)qry.Status);
            if (!string.IsNullOrEmpty(qry.InvoiceName))
                sb.AppendFormat(" and InvoiceName like '%{0}%'", qry.InvoiceName);
            if (!string.IsNullOrEmpty(qry.InvoiceNo))
                sb.AppendFormat(" and InvoiceNo like '%{0}%'", qry.InvoiceNo);
            return sb.ToString();
        }

        /// <summary>
        /// 发票查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryInvoice(InvoiceCriteria qry, out int record)
        {
            return db.GetPagination("Ord_OrderInvoice", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 发票详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Ord_OrderInvoice GetInvoice(string keyID)
        {
            return DAL.Ord_OrderInvoice.SingleOrDefault(x => x.ID == keyID);
        }


        #endregion
    }
}
