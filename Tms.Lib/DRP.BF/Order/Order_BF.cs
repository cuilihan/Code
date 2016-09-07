using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.BF.Crm;
using DRP.BF.ProMrg;
using DRP.BF.ResMrg;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.Framework;
using DRP.Framework.Core;
using System.Xml;
using SubSonic.Query;
using System.IO;
using Novacode;

namespace DRP.BF.Order
{
    //                            _ooOoo_                                
    //                           o8888888o                                        
    //                           88" . "88
    //                           (| -_- |)
    //                            O\ = /O
    //                        ____/`---'\____
    //                      .   ' \\| |// `.
    //                       / \\||| : |||// \
    //                     / _||||| -:- |||||- \
    //                       | | \\\ - /// | |
    //                     | \_| ''\---/'' | |
    //                      \ .-\__ `-` ___/-. /
    //                   ___`. .' /--.--\ `. . __
    //                ."" '< `.___\_<|>_/___.' >'"".
    //               | | : `- \`.;`\ _ /`;.`/ - ` : | |
    //                 \ \ `-. \_ __\ /__ _/ .-` / /
    //         ======`-.____`-.___\_____/___.-`____.-'======
    //                            `=---='
    //
    //         .............................................
    //                  佛祖镇楼                  BUG辟易
    //          佛曰:
    //                  写字楼里写字间，写字间里程序员；
    //                  程序人员写程序，又拿程序换酒钱。
    //                  酒醒只在网上坐，酒醉还来网下眠；
    //                  酒醉酒醒日复日，网上网下年复年。
    //                  但愿老死电脑间，不愿鞠躬老板前；
    //                  奔驰宝马贵者趣，公交自行程序员。
    //                  别人笑我忒疯癫，我笑自己命太贱；
    //                  不见满街漂亮妹，哪个归得程序员？


    /// <summary>
    /// 订单管理
    /// </summary>
    public class Order_BF
    {
        DRPDB db = new DRPDB();

        #region << 订单查询 >>

        /// <summary>
        /// 组合客户查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string RouteQueryCondition(OrderCriteria qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);

            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    if (!string.IsNullOrEmpty(user.PartDeptID))
                        sb.AppendFormat(" and (DeptID='{0}' or DeptID='{1}')", user.DeptID, user.PartDeptID);
                    else
                        sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion

            #region 线路类型权限
            if (user.RouteTypePermission.Count > 0 && qry.OrdType != OrderType.DXYW && qry.OrdType != OrderType.AirTicket)
            {
                if (user.RouteTypePermission.Count == 1)
                    sb.AppendFormat(" and RouteTypeID='{0}'", user.RouteTypePermission[0]);
                else
                {
                    var rArr = new List<string>();
                    user.RouteTypePermission.ForEach(p =>
                    {
                        rArr.Add(string.Format("'{0}'", p));
                    });
                    sb.AppendFormat(" and RouteTypeID in ({0})", string.Join(",", rArr));
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(qry.RouteTypeID))
                sb.AppendFormat(" and RouteTypeID= '{0}'", qry.RouteTypeID);
            if (!string.IsNullOrEmpty(qry.DestinationID))
                sb.AppendFormat(" and DestinationPath like '%{0}%'", qry.DestinationID);
            if (qry.OrdType != OrderType.All)
                sb.AppendFormat(" and OrderType={0}", (int)qry.OrdType);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.OrderNo))
                sb.AppendFormat(" and OrderNo like '%{0}%'", qry.OrderNo);
            if (!string.IsNullOrEmpty(qry.CusotmerName))
                sb.AppendFormat(" and CustomerName like '%{0}%'", qry.CusotmerName);
            if (!string.IsNullOrEmpty(qry.Supplier))
                sb.AppendFormat(" and SupplierName like '%{0}%'", qry.Supplier);
            if (!string.IsNullOrEmpty(qry.Company))
                sb.AppendFormat(" and Company like '%{0}%'", qry.Company);
            if (!string.IsNullOrEmpty(qry.UpdateUserName))
                sb.AppendFormat(" and UpdateUserName like '%{0}%'", qry.UpdateUserName);
            if (!string.IsNullOrEmpty(qry.sUnCollectedAmt))
                sb.AppendFormat(" and (OrderAmt-CollectedAmt-ToConfirmCollectedAmt) >= '{0}'", qry.sUnCollectedAmt);
            if (!string.IsNullOrEmpty(qry.eUnCollectedAmt))
                sb.AppendFormat(" and (OrderAmt-CollectedAmt-ToConfirmCollectedAmt) < '{0}'", qry.eUnCollectedAmt);

            if (qry.QueryDateScope != null)
            {
                if (qry.DateType == 1) //录入日期
                {
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                        sb.AppendFormat(" and CreateDate>='{0}'", qry.QueryDateScope.sDate);
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                        sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
                if (qry.DateType == 2) //出团日期
                {
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                        sb.AppendFormat(" and TourDate>='{0}'", qry.QueryDateScope.sDate);
                    if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                        sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
            }
            if (qry.OrdStatus != OrderStatus.All)
                sb.AppendFormat(" and OrderStatus={0}", (int)qry.OrdStatus);
            else
            {
                if (qry.OrdStatus != OrderStatus.Canceled)
                    sb.AppendFormat(" and OrderStatus<4");
            }
            if (qry.BudgetStatus != OrderBudgetStatus.All)
                sb.AppendFormat(" and BudgetStatus={0}", (int)qry.BudgetStatus);

            if (qry.PartStatus == "1")
            {
                if (!string.IsNullOrEmpty(qry.PartDeptID))
                    sb.AppendFormat(" and PartDeptID= '{0}'", qry.PartDeptID);
                if (!string.IsNullOrEmpty(qry.ParticipantID))
                    sb.AppendFormat(" and ParticipantID='{0}'", qry.ParticipantID);
            }
            else
            {
                if (!string.IsNullOrEmpty(qry.DeptID))
                    sb.AppendFormat(" and DeptID= '{0}'", qry.DeptID);
                if (!string.IsNullOrEmpty(qry.CreateUserID))
                    sb.AppendFormat(" and CreateUserID='{0}'", qry.CreateUserID);
            }

            if (!string.IsNullOrEmpty(qry.OrderSourceName) && !qry.OrderSourceName.Equals("所有"))
                sb.AppendFormat(" and SourceName like '%{0}%'", qry.OrderSourceName);
            if (qry.OrderAmt > 0)
                sb.AppendFormat(" and OrderAmt={0}", qry.OrderAmt);
            if (qry.OrderClaimQry) //收款认领时，未收款=0的不显示
                sb.AppendFormat(" and (OrderAmt-CollectedAmt-ToConfirmCollectedAmt!=0)");
            return sb.ToString();
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryOrder(OrderCriteria qry, out int record)
        {
            var tblName = "V_OrderInfo_SK";
            if (qry.OrdType == OrderType.QYT)
                tblName = "V_OrderInfo_QYT";
            if (qry.OrdType == OrderType.ZZBT)
                tblName = "V_OrderInfo_ZZBT";
            if (qry.OrdType == OrderType.DXYW)
                tblName = "V_OrderInfo_DXYW";
            if (qry.OrdType == OrderType.AirTicket) //机票订单
                tblName = "V_OrderInfo_Ticket";
            return db.GetPagination(tblName, qry.pageIndex, qry.pageSize, out record, RouteQueryCondition(qry), qry.SortExpress);
        }

        public DataTable QueryOrder_Sum(OrderCriteria qry)
        {
            var sql = string.Format(@"select isnull(SUM(AdultNum),0) AdultNum
                                            ,isnull(SUM(ChildNum),0) ChildNum
                                            ,isnull(SUM(OrderAmt),0) OrderAmt
                                            ,isnull(SUM(CollectedAmt),0) CollectedAmt
                                            ,isnull(SUM(OrderCost),0) OrderCost
                                            ,isnull(SUM(ToConfirmCollectedAmt),0) ToConfirmCollectedAmt
                                            ,isnull(SUM(CostInvoiceAmt),0) CostInvoiceAmt
                                            from V_OrderInfo_SK where {0} ", RouteQueryCondition(qry));
            if (qry.OrdType == OrderType.QYT)
                sql = string.Format(@"select isnull(SUM(AdultNum),0) AdultNum
                                            ,isnull(SUM(ChildNum),0) ChildNum
                                            ,isnull(SUM(OrderAmt),0) OrderAmt
                                            ,isnull(SUM(CollectedAmt),0) CollectedAmt
                                            ,isnull(SUM(OrderCost),0) OrderCost
                                            ,isnull(SUM(ToConfirmCollectedAmt),0) ToConfirmCollectedAmt
                                            ,isnull(SUM(CostInvoiceAmt),0) CostInvoiceAmt
                                            from V_OrderInfo_QYT where {0} ", RouteQueryCondition(qry));
            if (qry.OrdType == OrderType.ZZBT)
                sql = string.Format(@"select isnull(SUM(AdultNum),0) AdultNum
                                            ,isnull(SUM(ChildNum),0) ChildNum
                                            ,isnull(SUM(OrderAmt),0) OrderAmt
                                            ,isnull(SUM(CollectedAmt),0) CollectedAmt
                                            ,isnull(SUM(OrderCost),0) OrderCost
                                            ,isnull(SUM(ToConfirmCollectedAmt),0) ToConfirmCollectedAmt
                                            ,isnull(SUM(CostInvoiceAmt),0) CostInvoiceAmt
                                            from V_OrderInfo_ZZBT where {0} ", RouteQueryCondition(qry));
            if (qry.OrdType == OrderType.DXYW)
                sql = string.Format(@"select isnull(SUM(OrderAmt),0) OrderAmt
                                            ,isnull(SUM(CollectedAmt),0) CollectedAmt
                                            ,isnull(SUM(OrderCost),0) OrderCost
                                            ,isnull(SUM(ToConfirmCollectedAmt),0) ToConfirmCollectedAmt
                                            ,isnull(SUM(CostInvoiceAmt),0) CostInvoiceAmt
                                            from V_OrderInfo_DXYW where {0}", RouteQueryCondition(qry));
            //            if (qry.OrdType == OrderType.AirTicket) //机票订单
            //                sql = string.Format(@"select isnull(SUM(OrderAmt),0) OrderAmt
            //                                            ,isnull(SUM(CollectedAmt),0) CollectedAmt
            //                                            ,isnull(SUM(ToConfirmCollectedAmt),0) ToConfirmCollectedAmt
            //                                            from V_OrderInfo_Ticket where {0} ", RouteQueryCondition(qry));
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 查询订单编号
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public List<string> QueryOrderNo(string[] orderIDs)
        {
            return new OrdOrderInfoDAL().QueryOrderNo(orderIDs);
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Ord_OrderInfo GetOrderInfo(string keyID)
        {
            return DAL.Ord_OrderInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 自主班团订单详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Pro_TourInfo GetTourInfo(string keyID)
        {
            return DAL.Pro_TourInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 自主班团线路详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Pro_RouteInfo GetRouteInfo(string keyID)
        {
            return DAL.Pro_RouteInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 订单客户列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderCustomer> GetOrderCustomer(string orderID)
        {
            return DAL.Ord_OrderCustomer.Find(x => x.OrderID == orderID).OrderBy(x => x.OrderIndex).ToList();
        }

        /// <summary>
        /// 订单成本列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderCostItem> GetOrderCost(string orderID)
        {
            return DAL.Ord_OrderCostItem.Find(x => x.OrderID == orderID).OrderBy(x => x.OrderIndex).ToList();
        }

        /// <summary>
        /// 同行散客订单成本信息，包含已付款金额，用于订单详情显示
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DataTable GetTHSKOrderCost(string orderID)
        {
            return new OrderCostDAL().GetOrderCostItem(orderID, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 订单价格明细
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderPrice> GetOrderPrice(string orderID)
        {
            return DAL.Ord_OrderPrice.Find(x => x.OrderID == orderID).ToList();
        }


        /// <summary>
        /// 订单价格明细
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderPrice> GetOrderPriceForEdit(string orderID,string tourid)
        {
            var sql = string.Format(@"select  ISNULL(c.ID,NEWID()) ID, isnull(c.OrderID,'{0}') OrderID ,ISNULL(c.Name,a.Name) Name
                            ,ISNULL(c.SalePrice,a.SalePrice) SalePrice
                            ,ISNULL(c.Rebate,a.Rebate) Rebate
                            ,ISNULL(c.VisitorNum,0) VisitorNum
                            ,ISNULL(c.RoomRate,a.RoomRate) RoomRate
                            ,ISNULL(c.IsSeat,a.IsSeat) IsSeat
                            ,ISNULL(c.IsChild,a.IsChild) IsChild
                            ,ISNULL(c.InsuranceAmt,0) InsuranceAmt
                            ,ISNULL(c.InsuranceCost,0) InsuranceCost
                            ,ISNULL(c.TourPriceID,a.ID) TourPriceID
                             from Pro_TourPrice  as a left join  Ord_OrderInfo as b on a.TourID=b.TourID and b.id='{0}'
                            left join Ord_OrderPrice as c on a.ID=c.TourPriceID and c.OrderId='{0}'
                            where b.ID='{0}' and a.tourid='{1}'", orderID, tourid);
            return new SubSonic.Query.CodingHorror(sql).ExecuteTypedList<DAL.Ord_OrderPrice>().ToList();
        }

        #endregion

        #region << 订单操作 >>

        /// <summary>
        /// 订单日志
        /// </summary>
        /// <param name="orderID"></param>
        public void InsertLog(string orderID, string logTitle)
        {
            try
            {
                var user = AuthenticationPage.UserInfo;
                var log = new DAL.Ord_OrderLog();
                log.ID = Guid.NewGuid().ToString();
                log.OrderID = orderID;
                log.Title = logTitle;
                log.OpIP = IP.GetClientIp();
                log.OpBrowser = IP.GetBrowserType();
                log.CreateDate = DateTime.Now;
                log.OrgID = user.OrgID;
                log.CreateUserID = user.UserID;
                log.CreateUserName = user.UserName;
                log.Save();
            }
            catch { }
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="status"></param>
        public void UpdateOrderStatus(string orderID, int status)
        {
            var order = GetOrderInfo(orderID);
            order.OrderStatus = status;
            order.Save();
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool OrderCanceled(string orderID)
        {
            var e = GetOrderInfo(orderID);
            if (e == null) return false;
            var user = AuthenticationPage.UserInfo;
            var isOk = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    e.OrderStatus = (int)OrderStatus.Canceled;
                    e.Save();

                    //团队订单取消时，也须将预决算状态变更为取消状态
                    if (e.OrderType == (int)OrderType.QYT)
                    {
                        new OrderBudget_BF().UpdateBudgetStatus(orderID, -1);
                    }

                    //客户的消费金额减少
                    UpdateCustomerInfo(GetOrderCustomer(orderID), e.OrderAmt, user);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "取消订单时发生错误");
                isOk = false;
            }
            finally
            {
                if (isOk)
                {
                    InsertLog(orderID, "取消订单");
                }
            }
            return isOk;
        }

        /// <summary>
        /// 订单类型
        /// </summary>
        /// <returns></returns>
        public static string ConvertToOrderType(int orderType)
        {
            var xType = (OrderType)orderType;
            var title = "";
            switch (xType)
            {
                case OrderType.THSK:
                    title = "同行散客订单";
                    break;
                case OrderType.ZZBSK:
                    title = "自主班散客订单";
                    break;
                case OrderType.QYT:
                    title = "企业团订单";
                    break;
                case OrderType.ZZBT:
                    title = "自主班团订单";
                    break;
                case OrderType.DXYW:
                    title = "单项业务订单";
                    break;
            }
            return title;
        }

        /// <summary>
        /// 订单余款结清
        /// </summary>
        /// <param name="orderType"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool OrderCollectClosed(OrderType orderType, string orderID)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                if (OrderType.QYT == orderType)
                {
                    var e = GetOrderInfo(orderID);
                    e.IsCloseCollected = 1;
                    e.Save();
                }
                else
                {
                    var e = GetOrderExtend(orderID);
                    if (e == null)
                    {
                        e = new Ord_OrderExtend();
                        e.ID = e.OrderID = orderID;
                        e.AdultNum = e.ChildNum = 0;
                        e.OrderAmt = e.OrderCost = e.OrderProfit = e.DrawMoneyAmt = 0;
                        e.OrgID = user.OrgID;
                        e.PaidAmt = 0;
                        e.OrderInvoiceAmt = 0;
                        e.CostInvoiceAmt = 0;
                        e.OrderStatus = (int)OrderStatus.Confirmed;
                        e.BudgetStatus = 1;
                        e.IsCloseCollected = 1;
                        e.Save();
                    }
                    else
                    {
                        e.IsCloseCollected = 1;
                        e.Save();
                    }
                }
                InsertLog(orderID, "将订单收款设为“余款结清”");
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "余款结清操作时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 更新订单日期
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="tourDate"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public bool UpdateOrderDate(string orderID, DateTime tourDate, DateTime createDate)
        {
            try
            {

                var e = GetOrderInfo(orderID);
                e.TourDate = tourDate;
                e.CreateDate = createDate;
                var oriTourDate = e.TourDate;
                var oriCreateDate = e.CreateDate;
                e.Save();

                InsertLog(orderID, string.Format("更新订单日期{0}->{1}，创建日期{2}->{3}",
                    oriCreateDate.ToString("yyyy-MM-dd"), tourDate.ToString("yyyy-MM-dd"),
                    oriCreateDate.ToString("yyyy-MM-dd"), createDate.ToString("yyyy-MM-dd")));
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "更新日期时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 更新客户
        /// 如果客户基础资料中没有此客户，则将客户写入客户基础资料中，如果客户存在，则更新消息行为
        /// </summary>
        /// <param name="c"></param>
        public void UpdateCustomerInfo(DAL.Ord_OrderCustomer c, decimal orderAmt, UserInfo user)
        {
            try
            {
                var dal = new Customer_BF();
                var e = dal.Get(c.CustomerID);
                if (e == null)
                {
                    e = dal.GetCustomerByMobile(c.Mobile);
                    if (e == null)
                    {
                        e = new Crm_Customer();
                        e.ID = c.CustomerID;
                        e.Name = c.Name;
                        e.Company = c.Company;
                        e.Sex = c.Sex;
                        e.Mobile = c.Mobile;
                        if (c.IDType.Contains("身份证"))
                        {
                            e.IDNum = c.IDNo;
                        }
                        e.CustomerType = "";
                        e.TradeAmt = orderAmt;
                        e.TradeNum = 1;
                        e.CommunicateNum = 0;
                        e.CreateDate = DateTime.Now;
                        e.CreateUserID = user.UserID;
                        e.CreateUserName = user.UserName;
                        e.DeptID = user.DeptID;
                        e.OrgID = user.OrgID;
                        e.Save();
                    }
                    else
                    {
                        var trade = new CustomerDAL().GetTradeOrder(e.ID);
                        e.TradeNum = trade.TradeNum;
                        e.TradeAmt = trade.TradeAmt;
                        e.Save();
                    }
                }
                else
                {
                    var trade = new CustomerDAL().GetTradeOrder(e.ID);
                    e.TradeNum = trade.TradeNum;
                    e.TradeAmt = trade.TradeAmt;
                    e.Save();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "同步订单的客户资料时发生错误");
            }
        }

        /// <summary>
        /// 删除客户的消费记录，用于修改订单时移除原选择的客户
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="orderAmt"></param>
        internal void RemoveCustomerTrade(string customerID, decimal orderAmt)
        {
            var customer = DAL.Crm_Customer.SingleOrDefault(x => x.ID == customerID);
            if (customer != null)
            {
                customer.TradeNum = customer.TradeNum - 1;
                customer.TradeAmt = customer.TradeAmt - orderAmt;
                if (customer.TradeNum < 0) customer.TradeNum = 0;
                if (customer.TradeAmt < 0) customer.TradeAmt = 0;
                customer.Save();
            }
        }

        /// <summary>
        /// 更新客户的消息金额（当订单取消时，金额发生变化) 
        /// </summary>
        /// <param name="c"></param>
        public void UpdateCustomerInfo(List<DAL.Ord_OrderCustomer> list, decimal orderAmt, UserInfo user)
        {
            try
            {
                var dal = new Customer_BF();
                list.ForEach(x =>
                {
                    var e = dal.Get(x.CustomerID);
                    if (e != null)
                    {
                        e.TradeNum = e.TradeNum - 1;
                        e.TradeAmt = e.TradeAmt - orderAmt;
                        if (e.TradeNum >= 0 && e.TradeAmt >= 0)
                        {
                            e.Save();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "取消订单操作时同步客户资料时发生错误");
            }
        }


        /// <summary>
        /// 更新供应商的成本金额与合作次数
        /// 则更新消息行为
        /// </summary>
        /// <param name="c"></param>
        internal void UpdateResourceInfo(ResourceType xType, string resourceID, UserInfo user)
        {
            try
            {
                new ResourceUtility().UpdateTraceInfo(xType, resourceID);
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "同步订单的供应商往来时发生错误");
            }
        }

        /// <summary>
        /// 获取团队订单导游列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderGuide> GetOrderGuide(string orderID)
        {
            return DAL.Ord_OrderGuide.Find(x => x.OrderID == orderID).ToList();
        }

        /// <summary>
        /// 订单审核
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool OrderAudit(string orderID, string remark)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var order = GetOrderInfo(orderID);
                order.OrderStatus = (int)OrderStatus.Confirmed;
                order.AuditRemark = remark;
                order.Save();

                InsertLog(orderID, "确认订单：" + remark);
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "确认订单时发生错误");
                return false;
            }
        }
        #endregion

        #region << 同行散客订单 >>

        /// <summary>
        /// 保存同行散客订单
        /// </summary>
        /// <param name="e">订单信息</param>
        /// <param name="customerInfo">客户信息</param>
        /// <param name="costInfo">成本项目</param>
        /// <returns></returns>
        public bool SaveOrderTHSK(DAL.Ord_OrderInfo e, string customerInfo, string costInfo, string fileID)
        {
            var isRec = true;
            var itemArr = new List<DAL.Ord_OrderCostItem>();
            var user = AuthenticationPage.UserInfo;
            var listCustomer = new OrderUtility().ConverToCustomerEntity(customerInfo);
            var listCostItem = new OrderUtility().ConverToCostItemEntity(costInfo);
            var OrderCustomerData = DAL.Ord_OrderCustomer.Find(x => x.OrderID == e.ID).ToList();//原订单客户信息，用于修改订单时，删除客户同步减去消息信息

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var isInsert = false;

                    #region 订单客户

                    DAL.Ord_OrderCustomer.Delete(x => x.OrderID == e.ID);
                    var idx = 0;
                    var nId = "";

                    listCustomer.ForEach(c =>
                    {
                        var en = Get(c.CustomerID);
                        if (en == null)
                        {
                            en = new DAL.Crm_Customer();
                            en.ID = c.CustomerID;
                            en.OrgID = user.OrgID;
                            en.DeptID = user.DeptID;
                            en.CreateUserID = user.UserID;
                            en.CreateUserName = user.UserName;
                            en.CreateDate = DateTime.Now;
                            en.TradeNum = 0;
                            en.TradeAmt = 0;
                            en.CommunicateNum = 0;
                            nId = en.ID;
                        }
                        en.Mobile = c.Mobile;
                        en.Name = c.Name;
                        en.Sex = c.Sex;
                        en.IDNum = c.IDNo;
                        en.Save();

                        var cEntity = new DAL.Ord_OrderCustomer();
                        cEntity.ID = c.ID;
                        cEntity.CustomerID = c.CustomerID;
                        cEntity.OrderID = e.ID;
                        cEntity.Name = c.Name;
                        cEntity.Sex = c.Sex;
                        cEntity.Mobile = c.Mobile;
                        cEntity.IDType = c.IDType;
                        cEntity.IDNo = c.IDNo;
                        cEntity.Company = c.Company;
                        cEntity.Comment = c.Comment;
                        cEntity.IsLeader = c.IsLeader;
                        cEntity.OrgID = user.OrgID;
                        cEntity.OrderIndex = c.OrderIndex;
                        cEntity.Save();
                        if (idx++ == 0)
                        {
                            e.CustomerName = cEntity.Name;
                            e.CustomerID = cEntity.CustomerID;
                        }
                    });
                    #endregion

                    #region 订单成本
                    decimal totalCost = 0; //成本总额
                    listCostItem.ForEach(i =>
                    {
                        var item = DAL.Ord_OrderCostItem.SingleOrDefault(x => x.ID == i.ID);
                        if (item == null)
                            item = new DAL.Ord_OrderCostItem();
                        item.ID = i.ID;
                        item.OrderID = e.ID;
                        item.ItemType = i.ItemType;
                        item.ItemName = i.ItemName;
                        item.SupplierID = i.SupplierID;
                        item.Supplier = i.Supplier;
                        item.CostAmt = i.CostAmt;
                        item.Comment = i.Comment;
                        item.OrderIndex = i.OrderIndex;
                        item.OrgID = user.OrgID;
                        item.Save();
                        totalCost += i.CostAmt;

                        itemArr.Add(item);
                    });
                    e.OrderCost = totalCost;
                    #endregion

                    #region 订单主表
                    //供应商
                    var itemEntity = listCostItem.Find(x => x.ItemType == (int)ResourceType.TravelAgency);
                    var supplierName = "";
                    if (itemEntity == null)
                    {
                        var item = listCostItem.Count > 0 ? listCostItem.First() : null;
                        supplierName = item == null ? "" : item.Supplier;
                    }
                    else
                        supplierName = itemEntity.Supplier;

                    var entity = GetOrderInfo(e.ID);
                    if (entity == null)
                    {
                        isInsert = true;
                        entity = new DAL.Ord_OrderInfo();
                        entity.DeptID = user.DeptID;
                        entity.OrgID = user.OrgID;
                        entity.CreateDate = DateTime.Now;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CollectedAmt = 0;
                        entity.ToConfirmCollectedAmt = 0;
                        entity.PaidAmt = 0;
                        entity.OrderInvoiceAmt = 0;
                        entity.CostInvoiceAmt = 0;
                        entity.Departure = "";
                        entity.DepartureID = "";
                        entity.PickAmt = 0;
                        entity.SendAmt = 0;
                        entity.AdjustAmt = 0;
                        entity.BudgetStatus = -1;
                        entity.OrderNo = SerialNumberHelper.GetInstance().CreateOrderNo(e.RouteTypeName, e.TourDate, e.TourDays);
                    }
                    entity.ID = e.ID;
                    entity.OrderType = e.OrderType;
                    entity.RouteTypeID = e.RouteTypeID;
                    entity.RouteTypeName = e.RouteTypeName;
                    entity.DestinationID = e.DestinationID;
                    entity.DestinationName = e.DestinationName;
                    entity.DestinationPath = e.DestinationPath;
                    entity.OrderName = e.OrderName;
                    entity.AdultNum = e.AdultNum;
                    entity.ChildNum = e.ChildNum;
                    entity.OrderAmt = e.OrderAmt;
                    entity.OrderCost = e.OrderCost;
                    entity.TourDate = e.TourDate;
                    entity.TourDays = e.TourDays;
                    entity.ReturnDate = e.TourDate.AddDays(e.TourDays - 1);
                    entity.OrderStatus = e.OrderStatus;
                    entity.Remark = e.Remark;
                    entity.SourceID = e.SourceID;
                    entity.SourceName = e.SourceName;
                    entity.SeatNum = 0;
                    entity.CustomerName = e.CustomerName;
                    entity.CustomerID = nId == "" ? e.CustomerID : nId;
                    //entity.CustomerID = e.CustomerID;
                    entity.SupplierName = supplierName;
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateUserName = user.UserName;
                    entity.UpdateUserID = user.UserID;
                    entity.Participant = e.Participant;
                    entity.DeptName = e.DeptName;
                    entity.PartDeptID = e.PartDeptID;
                    entity.ParticipantID = e.ParticipantID;

                    entity.Save();
                    #endregion


                    #region 订单附件

                    //删除
                    DAL.Ord_OrderFile.Find(x => x.OrderID == e.ID).ToList().ForEach(x =>
                    {
                        DAL.Ord_OrderFile.Delete(y => y.ID == x.ID);
                    });


                    if (!string.IsNullOrEmpty(fileID))
                    {
                        foreach (var f in fileID.Split(','))
                        {
                            var fentity = new DAL.Ord_OrderFile();
                            fentity.ID = Guid.NewGuid().ToString();
                            fentity.OrderID = e.ID;
                            fentity.FilleD = f;
                            fentity.OrgID = user.OrgID;
                            fentity.CreateUserID = user.UserID;
                            fentity.CreateUserName = user.UserName;
                            fentity.Save();
                        }
                    }
                    #endregion

                    #region 订单日志
                    var title = isInsert ? "提交新订单" : "修改订单";
                    InsertLog(e.ID, title);
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存同行散客订单时发生错误");
                isRec = false;
            }
            finally
            {
                if (isRec)
                {
                    //如果客户基础资料中没有此客户，则将客户写入客户基础资料中
                    listCustomer.ForEach(c =>
                    {
                        UpdateCustomerInfo(c, e.OrderAmt, user);

                        //修改订单时，删除订单客户同步删除客户的消费信息
                        var a = OrderCustomerData.Find(item => item.CustomerID == c.CustomerID);
                        if (a != null) OrderCustomerData.Remove(a);
                        OrderCustomerData.ForEach(b =>
                        {
                            RemoveCustomerTrade(b.CustomerID, e.OrderAmt);
                        });
                    });

                    //更新供应商交易金额
                    itemArr.ForEach(item =>
                    {
                        UpdateResourceInfo((ResourceType)item.ItemType, item.SupplierID, user);
                    });
                }
            }
            return isRec;
        }


        #endregion

        #region << 自主班散客订单 >>

        /// <summary>
        /// 保存自主班散客订单
        /// </summary>
        /// <param name="e">订单信息</param>
        /// <param name="customerInfo">客户信息</param>
        /// <param name="costInfo">成本项目</param>
        /// <returns></returns>
        public bool SaveOrderZZBSK(DAL.Ord_OrderInfo e, string customerInfo, string priceInfo, string strSeatNo, string fileID)
        {
            var isRec = true;
            var user = AuthenticationPage.UserInfo;
            var isInsert = false;
            var listCustomer = new OrderUtility().ConverToCustomerEntity(customerInfo);
            var listPrice = new OrderUtility().ConverToOrderPriceEntity(priceInfo);
            var OrderCustomerData = DAL.Ord_OrderCustomer.Find(x => x.OrderID == e.ID).ToList();//原订单客户信息，用于修改订单时，删除客户同步减去消息信息

            int adultNum = 0;
            int childNum = 0;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 订单客户
                    DAL.Ord_OrderCustomer.Delete(x => x.OrderID == e.ID);
                    var idx = 0;
                    var nId = "";
                    listCustomer.ForEach(c =>
                    {
                        var en = Get(c.CustomerID);
                        if (en == null)
                        {
                            en = new DAL.Crm_Customer();
                            en.ID = c.CustomerID;
                            en.OrgID = user.OrgID;
                            en.DeptID = user.DeptID;
                            en.CreateUserID = user.UserID;
                            en.CreateUserName = user.UserName;
                            en.CreateDate = DateTime.Now;
                            en.TradeNum = 0;
                            en.TradeAmt = 0;
                            en.CommunicateNum = 0;
                            nId = en.ID;
                        }
                        en.Mobile = c.Mobile;
                        en.Name = c.Name;
                        en.Sex = c.Sex;
                        en.IDNum = c.IDNo;
                        en.Save();

                        var cEntity = new DAL.Ord_OrderCustomer();
                        cEntity.ID = c.ID;
                        cEntity.CustomerID = c.CustomerID;
                        cEntity.OrderID = e.ID;
                        cEntity.Name = c.Name;
                        cEntity.Sex = c.Sex;
                        cEntity.Mobile = c.Mobile;
                        cEntity.IDType = c.IDType;
                        cEntity.IDNo = c.IDNo;
                        cEntity.Company = c.Company;
                        cEntity.Comment = c.Comment;
                        cEntity.IsLeader = c.IsLeader;
                        cEntity.OrgID = user.OrgID;
                        cEntity.OrderIndex = c.OrderIndex;
                        cEntity.Save();
                        if (idx++ == 0)
                        {
                            e.CustomerName = cEntity.Name;
                            e.CustomerID = cEntity.CustomerID;
                        }
                    });
                    #endregion

                    decimal orderCost = 0;//订单成本

                    #region 价格策略
                    DAL.Ord_OrderPrice.Delete(x => x.OrderID == e.ID);
                    listPrice.ForEach(p =>
                    {
                        var pEntity = new DAL.Ord_OrderPrice();
                        pEntity.ID = Guid.NewGuid().ToString();
                        pEntity.OrderID = e.ID;
                        pEntity.TourPriceID = p.TourPriceID;
                        pEntity.Name = p.Name;
                        pEntity.SalePrice = p.SalePrice;
                        pEntity.Rebate = p.Rebate;
                        pEntity.VisitorNum = p.VisitorNum;
                        pEntity.RoomRate = p.RoomRate;
                        pEntity.IsSeat = p.IsSeat;
                        pEntity.IsChild = p.IsChild;
                        pEntity.InsuranceAmt = p.InsuranceAmt;
                        pEntity.InsuranceCost = p.InsuranceCost;
                        pEntity.Save();
                        if (pEntity.IsChild)
                            childNum += pEntity.VisitorNum;
                        else
                            adultNum += pEntity.VisitorNum;

                        //成本=（市场价-返佣）*人数+保险成本+单房差+接送价
                        orderCost += ((pEntity.SalePrice - pEntity.Rebate) * pEntity.VisitorNum + (decimal)pEntity.InsuranceCost + pEntity.RoomRate);
                    });
                    #endregion

                    #region 订单主表

                    var entity = GetOrderInfo(e.ID);
                    var tourID = e.TourID;
                    if (entity != null)
                        tourID = entity.TourID;
                    var tour = new TourInfo_BF().Get(tourID);
                    var route = new RouteInfo_BF().Get(tour.RouteID);
                    if (entity == null)
                    {
                        isInsert = true;
                        entity = new DAL.Ord_OrderInfo();
                        entity.DeptID = user.DeptID;
                        entity.OrgID = user.OrgID;
                        entity.CreateDate = DateTime.Now;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CollectedAmt = 0;
                        entity.ToConfirmCollectedAmt = 0;
                        entity.PaidAmt = 0;
                        entity.OrderInvoiceAmt = 0;
                        entity.CostInvoiceAmt = 0;
                        entity.TourID = e.TourID;
                        entity.OrderType = (int)OrderType.ZZBSK;
                        entity.RouteTypeID = route.RouteTypeID;
                        entity.RouteTypeName = route.RouteType;
                        entity.DestinationID = route.DestinationID;
                        entity.DestinationName = route.Destination;
                        entity.DestinationPath = route.DestinationPath;
                        entity.Departure = e.Departure;
                        entity.DepartureID = e.DepartureID;
                        entity.BudgetStatus = -1;
                        entity.OrderNo = SerialNumberHelper.GetInstance().CreateOrderNo(entity.RouteTypeName, tour.TourDate, tour.TourDays);
                    }

                    entity.ID = e.ID;
                    entity.OrderName = tour.TourName;
                    entity.AdultNum = adultNum;
                    entity.ChildNum = childNum;
                    entity.AdjustAmt = e.AdjustAmt;
                    entity.OrderAmt = e.OrderAmt;
                    entity.OrderCost = orderCost + (e.AdultNum + e.ChildNum) * (e.PickAmt + e.SendAmt);//成本=（市场价-返佣）*人数+保险成本+单房差+接送价
                    entity.TourDate = tour.TourDate;
                    entity.TourDays = tour.TourDays;
                    entity.ReturnDate = tour.TourDate.AddDays(tour.TourDays - 1);
                    entity.OrderStatus = e.OrderStatus;
                    entity.Remark = e.Remark;
                    entity.PickAmt = e.PickAmt;
                    entity.SendAmt = e.SendAmt;
                    entity.SourceID = e.SourceID;
                    entity.SourceName = e.SourceName;
                    entity.SeatNum = e.SeatNum;
                    entity.VenueName = e.VenueName;
                    entity.CollectTime = e.CollectTime;
                    entity.CustomerName = e.CustomerName;
                    entity.CustomerID = nId == "" ? e.CustomerID : nId;
                    entity.SupplierName = tour.TourNo;//供应商写团号
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateUserName = user.UserName;
                    entity.UpdateUserID = user.UserID;
                    entity.Participant = e.Participant;
                    entity.DeptName = e.DeptName;
                    entity.PartDeptID = e.PartDeptID;
                    entity.ParticipantID = e.ParticipantID;

                    entity.Save();
                    #endregion

                    #region 订单附件

                    //删除
                    DAL.Ord_OrderFile.Find(x => x.OrderID == e.ID).ToList().ForEach(x =>
                    {
                        DAL.Ord_OrderFile.Delete(y => y.ID == x.ID);
                    });


                    if (!string.IsNullOrEmpty(fileID))
                    {
                        foreach (var f in fileID.Split(','))
                        {
                            var fentity = new DAL.Ord_OrderFile();
                            fentity.ID = Guid.NewGuid().ToString();
                            fentity.OrderID = e.ID;
                            fentity.FilleD = f;
                            fentity.OrgID = user.OrgID;
                            fentity.CreateUserID = user.UserID;
                            fentity.CreateUserName = user.UserName;
                            fentity.Save();
                        }
                    }
                    #endregion

                    #region 座位号
                    if (route.RouteType.Contains("短线") && tour.SeatNum > 0 && !string.IsNullOrEmpty(strSeatNo))
                    {
                        var arrSeat = strSeatNo.Split(',');
                        DAL.Ord_OrderSeat.Delete(x => x.OrderID == e.ID);
                        foreach (var n in arrSeat)
                        {
                            var nEntity = new DAL.Ord_OrderSeat();
                            nEntity.ID = Guid.NewGuid().ToString();
                            nEntity.OrgID = user.OrgID;
                            nEntity.SeatNum = n.ToInt();
                            nEntity.OrderID = e.ID;
                            nEntity.TourID = tour.ID;
                            nEntity.CreateDate = DateTime.Now;
                            nEntity.CreateUserID = user.UserID;
                            nEntity.CreateUserName = user.UserName;
                            nEntity.Save();
                        }
                    }
                    #endregion

                    #region 订单日志
                    var title = isInsert ? "提交新订单" : "修改订单";
                    InsertLog(e.ID, title);
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存自主班散客订单时发生错误");
                isRec = false;
            }
            finally
            {
                if (isRec)
                {
                    //如果客户基础资料中没有此客户，则将客户写入客户基础资料中
                    listCustomer.ForEach(c =>
                    {
                        UpdateCustomerInfo(c, e.OrderAmt, user);

                        //修改订单时，删除订单客户同步删除客户的消费信息
                        var a = OrderCustomerData.Find(item => item.CustomerID == c.CustomerID);
                        if (a != null) OrderCustomerData.Remove(a);
                        OrderCustomerData.ForEach(b =>
                        {
                            RemoveCustomerTrade(b.CustomerID, e.OrderAmt);
                        });
                    });
                }
            }
            return isRec;
        }

        /// <summary>
        /// 订单占座位号
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderSeat> GetOrderSeat(string orderID)
        {
            return DAL.Ord_OrderSeat.Find(x => x.OrderID == orderID).ToList();
        }

        /// <summary>
        /// 整团占座位号
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderSeat> GetTourOrderSeat(string tourID)
        {
            return new OrderSeatDAL().GetTourOrderSeat(tourID);
        }

        /// <summary>
        /// 自主班团订单的应收款与人数
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public DataTable CalculateZZBOrder(string tourID)
        {
            return new TourOrderDAL().TourOrderAmt(tourID, AuthenticationPage.UserInfo.OrgID);
        }
        #endregion

        #region << 企业团订单 >>

        /// <summary>
        /// 保存企业团订单
        /// </summary>
        /// <param name="e">订单信息</param>
        /// <param name="customerInfo">客户信息</param>
        /// <param name="costInfo">成本项目</param>
        /// <returns></returns>
        public bool SaveOrderQYT(DAL.Ord_OrderInfo e, string customerInfo, string fileID)
        {
            var isRec = true;
            var user = AuthenticationPage.UserInfo;
            var listCustomer = new OrderUtility().ConverToCustomerEntity(customerInfo);
            var OrderCustomerData = DAL.Ord_OrderCustomer.Find(x => x.OrderID == e.ID).ToList();//原订单客户信息，用于修改订单时，删除客户同步减去消息信息

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var isInsert = false;
                    #region 订单客户
                    DAL.Ord_OrderCustomer.Delete(x => x.OrderID == e.ID);
                    var idx = 0;
                    var nId = "";
                    listCustomer.ForEach(c =>
                    {
                        var en = Get(c.CustomerID);
                        if (en == null)
                        {
                            en = new DAL.Crm_Customer();
                            en.ID = c.CustomerID;
                            en.OrgID = user.OrgID;
                            en.DeptID = user.DeptID;
                            en.CreateUserID = user.UserID;
                            en.CreateUserName = user.UserName;
                            en.CreateDate = DateTime.Now;
                            en.TradeNum = 0;
                            en.TradeAmt = 0;
                            en.CommunicateNum = 0;
                            nId = en.ID;
                        }
                        en.Mobile = c.Mobile;
                        en.Name = c.Name;
                        en.Sex = c.Sex;
                        en.IDNum = c.IDNo;
                        en.Save();

                        var cEntity = new DAL.Ord_OrderCustomer();
                        cEntity.ID = c.ID;
                        cEntity.OrderID = e.ID;
                        cEntity.CustomerID = c.CustomerID;
                        cEntity.Name = c.Name;
                        cEntity.Sex = c.Sex;
                        cEntity.Mobile = c.Mobile;
                        cEntity.IDType = c.IDType;
                        cEntity.IDNo = c.IDNo;
                        cEntity.Company = c.Company;
                        cEntity.Comment = c.Comment;
                        cEntity.IsLeader = c.IsLeader;
                        cEntity.OrgID = user.OrgID;
                        cEntity.OrderIndex = c.OrderIndex;
                        cEntity.Save();
                        if (idx++ == 0)
                        {
                            e.CustomerName = string.IsNullOrEmpty(cEntity.Company) ? cEntity.Name : cEntity.Company;
                            e.CustomerID = cEntity.CustomerID;
                        }
                    });
                    #endregion

                    #region 订单主表

                    var entity = GetOrderInfo(e.ID);
                    if (entity == null)
                    {
                        isInsert = true;
                        entity = new DAL.Ord_OrderInfo();
                        entity.DeptID = user.DeptID;
                        entity.OrgID = user.OrgID;
                        entity.CreateDate = DateTime.Now;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CollectedAmt = 0;
                        entity.ToConfirmCollectedAmt = 0;
                        entity.PaidAmt = 0;
                        entity.OrderInvoiceAmt = 0;
                        entity.CostInvoiceAmt = 0;
                        entity.AdultNum = 0;
                        entity.ChildNum = 0;
                        entity.AdjustAmt = 0;
                        entity.OrderAmt = 0;
                        entity.OrderCost = 0;
                        entity.Departure = "";
                        entity.DepartureID = "";
                        entity.BudgetStatus = 1;
                        entity.PickAmt = 0;
                        entity.SendAmt = 0;
                        entity.SeatNum = 0;
                        entity.OrderNo = SerialNumberHelper.GetInstance().CreateOrderNo(e.RouteTypeName, e.TourDate, e.TourDays);
                    }
                    entity.ID = e.ID;
                    entity.OrderType = e.OrderType;
                    entity.RouteTypeID = e.RouteTypeID;
                    entity.RouteTypeName = e.RouteTypeName;
                    entity.DestinationID = e.DestinationID;
                    entity.DestinationName = e.DestinationName;
                    entity.DestinationPath = e.DestinationPath;
                    entity.OrderName = e.OrderName;
                    entity.TourDate = e.TourDate;
                    entity.TourDays = e.TourDays;
                    entity.ReturnDate = e.TourDate.AddDays(e.TourDays - 1);
                    entity.OrderStatus = e.OrderStatus;
                    entity.Remark = e.Remark;
                    entity.SourceID = e.SourceID;
                    entity.SourceName = e.SourceName;
                    entity.CustomerName = e.CustomerName;
                    entity.CustomerID = nId == "" ? e.CustomerID : nId;
                    entity.VenueName = e.VenueName;
                    entity.CollectTime = e.CollectTime;
                    entity.Schedule = e.Schedule;
                    entity.SupplierName = "";
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateUserName = user.UserName;
                    entity.UpdateUserID = user.UserID;
                    entity.Participant = e.Participant;
                    entity.DeptName = e.DeptName;
                    entity.PartDeptID = e.PartDeptID;
                    entity.ParticipantID = e.ParticipantID;

                    entity.Save();
                    #endregion

                    #region 订单附件

                    //删除
                    DAL.Ord_OrderFile.Find(x => x.OrderID == e.ID).ToList().ForEach(x =>
                    {
                        DAL.Ord_OrderFile.Delete(y => y.ID == x.ID);
                    });


                    if (!string.IsNullOrEmpty(fileID))
                    {
                        foreach (var f in fileID.Split(','))
                        {
                            var fentity = new DAL.Ord_OrderFile();
                            fentity.ID = Guid.NewGuid().ToString();
                            fentity.OrderID = e.ID;
                            fentity.FilleD = f;
                            fentity.OrgID = user.OrgID;
                            fentity.CreateUserID = user.UserID;
                            fentity.CreateUserName = user.UserName;
                            fentity.Save();
                        }
                    }
                    #endregion

                    #region 订单日志
                    var title = isInsert ? "提交新订单" : "修改订单";
                    InsertLog(e.ID, title);
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存企业团订单时发生错误");
                isRec = false;
            }
            finally
            {
                if (isRec)
                {
                    //如果客户基础资料中没有此客户，则将客户写入客户基础资料中
                    listCustomer.ForEach(c =>
                    {
                        UpdateCustomerInfo(c, e.OrderAmt, user);

                        //修改订单时，删除订单客户同步删除客户的消费信息
                        var a = OrderCustomerData.Find(item => item.CustomerID == c.CustomerID);
                        if (a != null) OrderCustomerData.Remove(a);
                        OrderCustomerData.ForEach(b =>
                        {
                            RemoveCustomerTrade(b.CustomerID, e.OrderAmt);
                        });
                    });
                }
            }

            return isRec;
        }
        #endregion

        #region << 单项业务订单 >>

        /// <summary>
        /// 保存单项业务订单
        /// </summary>
        /// <param name="e">订单信息</param>
        /// <param name="customerInfo">客户信息</param>
        /// <param name="costInfo">成本项目</param>
        /// <returns></returns>
        public bool SaveOrderBiz(DAL.Ord_OrderInfo e, string customerInfo, string costInfo, string fileID)
        {
            var isRec = true;
            var user = AuthenticationPage.UserInfo;
            var isInsert = false;
            var listCustomer = new OrderUtility().ConverToCustomerEntity(customerInfo);
            var listCostItem = new OrderUtility().ConverToCostItemEntity(costInfo);
            var itemArr = new List<DAL.Ord_OrderCostItem>();
            var OrderCustomerData = DAL.Ord_OrderCustomer.Find(x => x.OrderID == e.ID).ToList();//原订单客户信息，用于修改订单时，删除客户同步减去消息信息
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 订单客户
                    DAL.Ord_OrderCustomer.Delete(x => x.OrderID == e.ID);
                    var idx = 0;
                    var nId = "";

                    listCustomer.ForEach(c =>
                    {
                        var en = Get(c.CustomerID);
                        if (en == null)
                        {
                            en = new DAL.Crm_Customer();
                            en.ID = c.CustomerID;
                            en.OrgID = user.OrgID;
                            en.DeptID = user.DeptID;
                            en.CreateUserID = user.UserID;
                            en.CreateUserName = user.UserName;
                            en.CreateDate = DateTime.Now;
                            en.TradeNum = 0;
                            en.TradeAmt = 0;
                            en.CommunicateNum = 0;
                            nId = en.ID;
                        }
                        en.Mobile = c.Mobile;
                        en.Name = c.Name;
                        en.Sex = c.Sex;
                        en.IDNum = c.IDNo;
                        en.Save();

                        var cEntity = new DAL.Ord_OrderCustomer();
                        cEntity.ID = c.ID;
                        cEntity.OrderID = e.ID;
                        cEntity.CustomerID = c.CustomerID;
                        cEntity.Name = c.Name;
                        cEntity.Sex = c.Sex;
                        cEntity.Mobile = c.Mobile;
                        cEntity.IDType = c.IDType;
                        cEntity.IDNo = c.IDNo;
                        cEntity.Company = c.Company;
                        cEntity.Comment = c.Comment;
                        cEntity.IsLeader = c.IsLeader;
                        cEntity.OrgID = user.OrgID;
                        cEntity.OrderIndex = c.OrderIndex;
                        cEntity.Save();
                        if (idx++ == 0)
                        {
                            e.CustomerName = cEntity.Name;
                            e.CustomerID = cEntity.CustomerID;
                        }
                    });
                    #endregion

                    #region 订单成本
                    decimal totalCost = 0; //成本总额
                    listCostItem.ForEach(i =>
                    {
                        var item = DAL.Ord_OrderCostItem.SingleOrDefault(x => x.ID == i.ID);
                        if (item == null) item = new DAL.Ord_OrderCostItem();
                        item.ID = i.ID;
                        item.OrderID = e.ID;
                        item.ItemType = i.ItemType;
                        item.ItemName = i.ItemName;
                        item.SupplierID = i.SupplierID;
                        item.Supplier = i.Supplier;
                        item.CostAmt = i.CostAmt;
                        item.Comment = i.Comment;
                        item.OrderIndex = i.OrderIndex;
                        item.OrgID = user.OrgID;
                        item.Save();
                        totalCost += i.CostAmt;

                        itemArr.Add(item);
                    });
                    e.OrderCost = totalCost;
                    #endregion

                    #region 订单主表
                    //供应商 
                    var a = listCostItem.Count > 0 ? listCostItem.First() : null;
                    var supplierName = a == null ? "" : a.Supplier;

                    var entity = GetOrderInfo(e.ID);
                    if (entity == null)
                    {
                        isInsert = true;
                        entity = new DAL.Ord_OrderInfo();
                        entity.DeptID = user.DeptID;
                        entity.OrgID = user.OrgID;
                        entity.CreateDate = DateTime.Now;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CollectedAmt = 0;
                        entity.ToConfirmCollectedAmt = 0;
                        entity.PaidAmt = 0;
                        entity.OrderInvoiceAmt = 0;
                        entity.CostInvoiceAmt = 0;
                    }
                    entity.ID = e.ID;
                    entity.OrderType = (int)OrderType.DXYW;
                    entity.RouteTypeID = e.RouteTypeID;
                    entity.RouteTypeName = e.RouteTypeName;
                    entity.DestinationID = e.DestinationID;
                    entity.DestinationName = e.DestinationName;
                    entity.DestinationPath = e.DestinationPath;
                    entity.OrderName = e.OrderName;
                    entity.OrderNo = SerialNumberHelper.GetInstance().CreateOrderNo("", e.TourDate, 0);
                    entity.Departure = "";
                    entity.DepartureID = "";
                    entity.AdultNum = e.AdultNum;
                    entity.ChildNum = e.ChildNum;
                    entity.AdjustAmt = 0;
                    entity.OrderAmt = e.OrderAmt;
                    entity.OrderCost = e.OrderCost;
                    entity.TourDate = e.TourDate;
                    entity.TourDays = e.TourDays;
                    entity.ReturnDate = e.TourDate.AddDays(e.TourDays - 1);
                    entity.OrderStatus = e.OrderStatus;
                    entity.BudgetStatus = -1;
                    entity.Remark = e.Remark;
                    entity.PickAmt = 0;
                    entity.SendAmt = 0;
                    entity.SourceID = e.SourceID;
                    entity.SourceName = e.SourceName;
                    entity.SeatNum = 0;
                    entity.CustomerName = e.CustomerName;
                    entity.CustomerID = nId == "" ? e.CustomerID : nId;
                    entity.SupplierName = supplierName;
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateUserName = user.UserName;
                    entity.UpdateUserID = user.UserID;
                    entity.Participant = e.Participant;
                    entity.DeptName = e.DeptName;
                    entity.PartDeptID = e.PartDeptID;
                    entity.ParticipantID = e.ParticipantID;

                    entity.Save();
                    #endregion

                    #region 订单附件

                    //删除
                    DAL.Ord_OrderFile.Find(x => x.OrderID == e.ID).ToList().ForEach(x =>
                    {
                        DAL.Ord_OrderFile.Delete(y => y.ID == x.ID);
                    });


                    if (!string.IsNullOrEmpty(fileID))
                    {
                        foreach (var f in fileID.Split(','))
                        {
                            var fentity = new DAL.Ord_OrderFile();
                            fentity.ID = Guid.NewGuid().ToString();
                            fentity.OrderID = e.ID;
                            fentity.FilleD = f;
                            fentity.OrgID = user.OrgID;
                            fentity.CreateUserID = user.UserID;
                            fentity.CreateUserName = user.UserName;
                            fentity.Save();
                        }
                    }
                    #endregion

                    #region 订单日志
                    var title = isInsert ? "提交新订单" : "修改订单";
                    InsertLog(e.ID, title);
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存单项业务订单时发生错误");
                isRec = false;
            }
            finally
            {
                if (isRec)
                {
                    //如果客户基础资料中没有此客户，则将客户写入客户基础资料中
                    listCustomer.ForEach(c =>
                    {
                        UpdateCustomerInfo(c, e.OrderAmt, user);

                        //修改订单时，删除订单客户同步删除客户的消费信息
                        var a = OrderCustomerData.Find(item => item.CustomerID == c.CustomerID);
                        if (a != null) OrderCustomerData.Remove(a);
                        OrderCustomerData.ForEach(b =>
                        {
                            RemoveCustomerTrade(b.CustomerID, e.OrderAmt);
                        });
                    });

                    //更新供应商交易金额
                    itemArr.ForEach(item =>
                    {
                        UpdateResourceInfo((ResourceType)item.ItemType, item.SupplierID, user);
                    });
                }
            }
            return isRec;
        }
        #endregion

        #region << 自主班团 >>

        /// <summary>
        /// 自主班订单扩展表详情有
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DAL.Ord_OrderExtend GetOrderExtend(string orderID)
        {
            return DAL.Ord_OrderExtend.SingleOrDefault(x => x.ID == orderID);
        }

        /// <summary>
        /// 查询自主班团下的订单列表
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderInfo> GetTourOrderList(string tourID)
        {
            return DAL.Ord_OrderInfo.Find(x => x.TourID == tourID && x.OrderType == (int)OrderType.ZZBSK
                && x.OrderStatus != (int)OrderStatus.Canceled).OrderBy(x => x.TourDate).ToList();
        }

        /// <summary>
        /// 自主班团订单的已收款
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public decimal GetTourOrderAmt(string orderID)
        {
            return new TourOrderDAL().TourCollectedAmt(orderID, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 报名人数
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public int OrderVisitorNum(string tourID)
        {
            return new OrdOrderInfoDAL().OrderVisitorNum(tourID);
        }
        #endregion


        /// <summary>
        /// 订单日志
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderLog> GetOrderLog(string orderID)
        {
            return DAL.Ord_OrderLog.Find(x => x.OrderID == orderID).OrderBy(x => x.CreateDate).ToList();
        }

        public DataTable QueryOrder_All(OrderCriteria qry)
        {
            var sql = string.Format("select * from V_OrderInfo_SK where {0}", RouteQueryCondition(qry));
            if (qry.OrdType == OrderType.QYT)
                sql = string.Format(@"select * from V_OrderInfo_QYT where {0} ", RouteQueryCondition(qry));
            if (qry.OrdType == OrderType.ZZBT)
                sql = string.Format(@"select * from V_OrderInfo_ZZBT where {0} ", RouteQueryCondition(qry));
            if (qry.OrdType == OrderType.DXYW)
                sql = string.Format(@"select * from V_OrderInfo_DXYW where {0}", RouteQueryCondition(qry));

            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        public DAL.Crm_Customer Get(string keyID)
        {
            return DAL.Crm_Customer.SingleOrDefault(x => x.ID == keyID);
        }

        #region 订单附件
        public List<DAL.Glo_File> GetOrderFile(string orderID)
        {
            var sql = "select b.ID,b.CreateDate,b.CreateUserName,b.FileName,b.FileSize,b.FileType,b.FilePath from Ord_OrderFile a inner join Glo_Files b on a.FilleD=b.ID where OrderID='{0}'";
            sql = string.Format(sql, orderID);
            return new SubSonic.Query.CodingHorror(sql).ExecuteTypedList<DAL.Glo_File>();
        }
        #endregion
    }
}
