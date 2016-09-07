using DRP.BF.ResMrg;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml;

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
    //
    //
    //
    //          先娱乐一下，有个好心情再Review代码！--Kimlee
    //
    //

    /// <summary>
    /// 机票订单查询条件
    /// </summary>
    public class TicketOrderCriteria : QueryCriteriaBase
    {
        public string PNR { get; set; }

        public string OrderName { get; set; }

        public string FlightInfo { get; set; }

        public string FlightLeg { get; set; }
        /// <summary>
        /// 航班日期
        /// </summary>
        public string sDate { get; set; }

        /// <summary>
        /// 航班日期
        /// </summary>
        public string eDate { get; set; }

        /// <summary>
        /// 下单日期
        /// </summary>
        public string csDate { get; set; }

        /// <summary>
        /// 下单日期
        /// </summary>
        public string ceDate { get; set; }

        public string Contact { get; set; }

        public string Supplier { get; set; }

        public OrderStatus OrdStatus { get; set; }

        public string Company { get; set; }

        public string UpdateUserName { get; set; }

        /// <summary>
        /// 未付款区间
        /// </summary>
        public string sUnCollectedAmt { get; set; }

        public string eUnCollectedAmt { get; set; }
    }

    /// <summary>
    /// 机票订单管理
    /// </summary>
    public class TicketOrder_BF
    {
        #region << 订单查询 >>

        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string QueryCondition(TicketOrderCriteria qry)
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

            if (qry.OrdStatus != OrderStatus.All)
                sb.AppendFormat(" and OrderStatus={0}", (int)qry.OrdStatus);
            else
            {
                sb.AppendFormat(" and OrderStatus<>4", (int)qry.OrdStatus);
            }
            if (!string.IsNullOrEmpty(qry.PNR))
                sb.AppendFormat(" and PNR like '%{0}%'", qry.PNR);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.FlightLeg))
                sb.AppendFormat(" and ToFlightLeg like '%{0}%'", qry.FlightLeg);
            if (!string.IsNullOrEmpty(qry.Contact))
                sb.AppendFormat(" and Contact like '%{0}%'", qry.Contact);
            if (!string.IsNullOrEmpty(qry.Supplier))
                sb.AppendFormat(" and SupplierName like '%{0}%'", qry.Supplier);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and TourDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(qry.csDate))
                sb.AppendFormat(" and CreateDate>='{0}'", qry.csDate);
            if (!string.IsNullOrEmpty(qry.ceDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.ceDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(qry.Company))
                sb.AppendFormat(" and Company like '%{0}%'", qry.Company);
            if (!string.IsNullOrEmpty(qry.UpdateUserName))
                sb.AppendFormat(" and UpdateUserName like '%{0}%'", qry.UpdateUserName);
            if (!string.IsNullOrEmpty(qry.sUnCollectedAmt))
                sb.AppendFormat(" and (OrderAmt-CollectedAmt-ToConfirmCollectedAmt) >= '{0}'", qry.sUnCollectedAmt);
            if (!string.IsNullOrEmpty(qry.eUnCollectedAmt))
                sb.AppendFormat(" and (OrderAmt-CollectedAmt-ToConfirmCollectedAmt) < '{0}'", qry.eUnCollectedAmt);

            return sb.ToString();
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryOrder(TicketOrderCriteria qry, out int record)
        {
            var db = new DRPDB();

            var field = "ID,PNR,OrderName,ToFlightLeg,ToAirport,ToAirLine,ToCabin,ToTicketPrice,FromFlightLeg,FromAirport,FromAirLine,FromCabin,FromTicketPrice,Contact,ContactPhone,AdultNum,ChildNum,OrderAmt,OrderCost,TourDate,ReturnDate,OrderStatus,SupplierName,CollectedAmt,ToConfirmCollectedAmt,OrderInvoiceAmt,CostInvoiceAmt,CreateUserName,CreateDate,ToFlightInfo,FromFlightInfo,Participant,Company,UpdateUserName";
            return db.GetPagination("Ord_TicketOrder", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress, field);
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Ord_TicketOrder Get(string keyID)
        {
            return DAL.Ord_TicketOrder.SingleOrDefault(x => x.ID == keyID);
        }

        #endregion

        #region 订单操作

        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="e">订单信息</param>
        /// <param name="customerInfo">客户信息</param>
        /// <param name="costInfo">成本项目</param>
        /// <returns></returns>
        public bool SaveOrder(DAL.Ord_TicketOrder e, string customerInfo, string costInfo, string fileID)
        {
            var isRec = true;
            var isInsert = false;
            var user = AuthenticationPage.UserInfo;
            var itemArr = new List<DAL.Ord_OrderCostItem>();
            var listCustomer = new OrderUtility().ConverToCustomerEntity(customerInfo);
            var listCostItem = new OrderUtility().ConverToCostItemEntity(costInfo);
            var OrderCustomerData = DAL.Ord_OrderCustomer.Find(x => x.OrderID == e.ID).ToList();//原订单客户信息，用于修改订单时，删除客户同步减去消息信息

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 订单客户
                    DAL.Ord_OrderCustomer.Delete(x => x.OrderID == e.ID);
                    listCustomer.ForEach(c =>
                    {
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
                    });
                    #endregion

                    #region 订单成本
                    //var item = DAL.Ord_OrderCostItem.SingleOrDefault(x => x.ID == e.OrderCostID);
                    //if (item == null)
                    //{
                    //    item = new DAL.Ord_OrderCostItem();
                    //    item.ID = e.OrderCostID;
                    //}
                    //item.OrderID = e.ID;
                    //item.ItemType = (int)ResourceType.TicketAgency;
                    //item.ItemName = "票务机构";
                    //item.SupplierID = e.SupplierID;
                    //item.Supplier = e.SupplierName;
                    //item.CostAmt = e.OrderCost;
                    //item.OrderIndex = 0;
                    //item.OrgID = user.OrgID;
                    //item.Save();
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
                    var itemEntity = listCostItem.Find(x => x.ItemType == (int)ResourceType.TicketAgency);
                    var supplierName = "";
                    var supplierID = "";
                    if (itemEntity == null)
                    {
                        var item = listCostItem.Count > 0 ? listCostItem.First() : null;
                        supplierName = item == null ? "" : item.Supplier;
                        supplierID = item == null ? Guid.Empty.ToString() : item.ID;
                    }
                    else
                    {
                        supplierName = itemEntity.Supplier;
                        supplierID = itemEntity.ID;
                    }

                    var entity = Get(e.ID);
                    if (entity == null)
                    {
                        isInsert = true;
                        entity = new DAL.Ord_TicketOrder();
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
                    entity.PNR = e.PNR;
                    entity.OrderNo = e.PNR;
                    entity.Contact = e.Contact;
                    entity.ContactPhone = e.ContactPhone;
                    entity.TicketType = e.TicketType;
                    entity.ToFlightLeg = e.ToFlightLeg;
                    entity.ToFlightInfo = e.ToFlightInfo;
                    entity.ToAirport = e.ToAirport;
                    entity.ToAirLine = e.ToAirLine;
                    entity.ToTicketPrice = e.ToTicketPrice;
                    entity.ToCabin = e.ToCabin;
                    entity.FromFlightLeg = e.FromFlightLeg;
                    entity.FromFlightInfo = e.FromFlightInfo;
                    entity.FromAirLine = e.FromAirLine;
                    entity.FromAirport = e.FromAirport;
                    entity.FromTicketPrice = e.FromTicketPrice;
                    entity.FromCabin = e.FromCabin;
                    entity.ID = e.ID;
                    entity.OrderName = e.OrderName;
                    entity.AdultNum = e.AdultNum;
                    entity.ChildNum = e.ChildNum;
                    entity.OrderAmt = e.OrderAmt;
                    entity.OrderCost = e.OrderCost;
                    entity.TourDate = e.TourDate;
                    entity.ReturnDate = e.ReturnDate;
                    entity.OrderStatus = e.OrderStatus;
                    entity.Remark = e.Remark;
                    entity.SupplierID = supplierID;
                    entity.SupplierName = supplierName;
                    entity.OrderCostID = Guid.Empty.ToString();//原只有一个成本，现修改为多个成本，所以无须此数据
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateUserName = user.UserName;
                    entity.UpdateUserID = user.UserID;
                    entity.Participant = e.Participant;
                    entity.DeptName = e.DeptName;
                    entity.PartDeptID = e.PartDeptID;
                    entity.ParticipantID = e.ParticipantID;
                    entity.Company = e.Company;

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

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存机票订单时发生错误");
                isRec = false;
            }
            finally
            {
                if (isRec)
                {

                    var title = isInsert ? "提交新订单" : "修改订单";
                    new Order_BF().InsertLog(e.ID, title);

                    try
                    {
                        //如果客户基础资料中没有此客户，则将客户写入客户基础资料中
                        listCustomer.ForEach(c =>
                        {
                            new Order_BF().UpdateCustomerInfo(c, e.OrderAmt, user);

                            //修改订单时，删除订单客户同步删除客户的消费信息
                            var a = OrderCustomerData.Find(item => item.CustomerID == c.CustomerID);
                            if (a != null) OrderCustomerData.Remove(a);
                            OrderCustomerData.ForEach(b =>
                            {
                                new Order_BF().RemoveCustomerTrade(b.CustomerID, e.OrderAmt);
                            });
                        });

                        //更新供应商交易金额 
                        itemArr.ForEach(item =>
                        {
                            new Order_BF().UpdateResourceInfo((ResourceType)item.ItemType, item.SupplierID, user);
                        }); 
                    }
                    catch (Exception ex)
                    {
                        BizUtility.ExceptionHandler(user, ex, "同步更新机票订单的更新合作情况时发生错误");
                    }
                }
            }
            return isRec;
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
                var e = Get(orderID);
                e.TourDate = tourDate;
                e.CreateDate = createDate;
                var oriTourDate = e.TourDate;
                var oriCreateDate = e.CreateDate;
                e.Save();

                new Order_BF().InsertLog(orderID, string.Format("更新订单日期{0}->{1}，创建日期{2}->{3}",
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
        /// 取消订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool OrderCanceled(string orderID)
        {
            var e = Get(orderID);
            if (e == null) return false;
            var user = AuthenticationPage.UserInfo;
            var orderDAL = new Order_BF();
            var isOk = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    e.OrderStatus = (int)OrderStatus.Canceled;
                    e.Save();

                    //客户的消费金额减少
                    orderDAL.UpdateCustomerInfo(orderDAL.GetOrderCustomer(orderID), e.OrderAmt, user);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "取消机票订单时发生错误");
                isOk = false;
            }
            finally
            {
                if (isOk)
                {
                    orderDAL.InsertLog(orderID, "取消机票订单");
                }
            }
            return isOk;
        }
        #endregion

        public DataTable QueryOrder_Sum(TicketOrderCriteria qry)
        {
            var sql = string.Format(@"select isnull(SUM(OrderAmt),0) OrderAmt
                                            ,isnull(SUM(CollectedAmt),0) CollectedAmt
                                            ,isnull(SUM(ToConfirmCollectedAmt),0) ToConfirmCollectedAmt
                                            ,isnull(SUM(OrderCost),0) OrderCost
                                            ,isnull(SUM(CostInvoiceAmt),0) CostInvoiceAmt
                                            ,isnull(SUM(AdultNum),0) AdultNum
                                            from Ord_TicketOrder where {0} ", QueryCondition(qry));
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        public DataTable QueryOrder_All(TicketOrderCriteria qry)
        {
            var sql = string.Format("select * from Ord_TicketOrder where {0}", QueryCondition(qry));

            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }
    }
}
