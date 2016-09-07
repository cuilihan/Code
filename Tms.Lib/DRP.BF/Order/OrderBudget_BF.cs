using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml;
using DRP.BF.ResMrg;
using DRP.DAL.DataAccess;
using DRP.Framework;
using DRP.DAL;
using DRP.BF.Glo;
using DRP.Framework.Core;


namespace DRP.BF.Order
{
    /// <summary>
    /// 团队订单预决算
    /// </summary>
    public class OrderBudget_BF
    {
        Order_BF dal = new Order_BF();

        /// <summary>
        /// 获取预决算详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dataStatus">1：预算 2：决算</param>
        /// <returns></returns>
        public DAL.Ord_Budget GetBudget(string orderId, int dataStatus)
        {
            return DAL.Ord_Budget.SingleOrDefault(x => x.OrderID == orderId && x.DataStatus == dataStatus);
        }

        /// <summary>
        /// 预算成本项目
        /// </summary>
        /// <param name="budgetID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderCostItem> GetOrderCost(string budgetID)
        {
            return DAL.Ord_OrderCostItem.Find(x => x.OrderID == budgetID).OrderBy(x => x.OrderIndex).ToList();
        }

        /// <summary>
        /// 预算备注
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_BudgetComment> GetBudgetComment(string orderID)
        {
            return DAL.Ord_BudgetComment.Find(x => x.OrderBudgetID == orderID).ToList();
        }

        /// <summary>
        /// 导游领款（备用金）详情
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DAL.Ord_DrawMoney GetDrawMoney(string orderID)
        {
            return DAL.Ord_DrawMoney.SingleOrDefault(x => x.ID == orderID);
        }

        /// <summary>
        /// 变更订单预决算状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dataStatus">-1：无效 1：预算 2：决算</param>
        /// <returns></returns>
        public void UpdateBudgetStatus(string orderId, int dataStatus)
        {
            if (DAL.Ord_Budget.Exists(x => x.OrderID == orderId))
            {
                new BudgetDAL().UpdateBudgetStatus(orderId, dataStatus);
            }
        }

        /// <summary>
        /// 保存预决算
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="xmlCost"></param>
        /// <param name="xmlComment"></param>
        /// <returns></returns>
        public bool SaveBudget(OrderType orderType, string orderID, int adultNum, int childNum, decimal orderAmt, decimal drawMoney,
            string drawMoneyMethod, string drawMoneyComment, string xmlCost, string xmlComment, int dataStatus, int budgetStatus, string comment, string fileID)
        {
            var isRec = true;
            var user = AuthenticationPage.UserInfo;
            var itemArr = new List<DAL.Ord_OrderCostItem>();
            var orderDAL = new Order_BF();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var e = GetBudget(orderID, dataStatus);
                    var isInsert = e == null;
                    if (e == null)
                    {
                        e = new DAL.Ord_Budget();
                        e.ID = Guid.NewGuid().ToString();
                        e.OrderID = orderID;
                        e.OrgID = user.OrgID;
                        e.DataStatus = dataStatus;
                    }
                    e.Comment = comment;//决算备注

                    #region 订单成本
                    var listCostItem = new OrderUtility().ConverToCostItemEntity(xmlCost);
                    decimal totalCost = 0; //成本总额 
                    listCostItem.ForEach(i =>
                    {
                        var item = DAL.Ord_OrderCostItem.SingleOrDefault(x => x.ID == i.ID);
                        if (item == null)
                        {
                            item = new DAL.Ord_OrderCostItem();
                        }
                        item.ID = i.ID;
                        item.OrderID = e.ID;
                        item.ItemType = i.ItemType;
                        item.ItemName = string.IsNullOrEmpty(i.ItemName) ? "" : (i.ItemName.Replace("请选择", ""));

                        if (i.SupplierID == "undefined")
                        {
                            item.SupplierID = ConvertResourceTableName(i.ItemType, orderID, i.Supplier, orderType);
                        }
                        else
                        {
                            item.SupplierID = i.SupplierID;
                        }
                        
                        item.Supplier = string.IsNullOrEmpty(i.Supplier) ? "" : (i.Supplier.Replace("请选择", "")); ;
                        item.CostAmt = i.CostAmt; 
                        item.Comment = i.Comment;
                        item.OrderIndex = i.OrderIndex;
                        item.OrgID = user.OrgID;
                        item.Save();
                        totalCost += i.CostAmt;

                        itemArr.Add(item);
                    });
                    #endregion

                    #region 订单预决算表
                    e.AdultNum = adultNum;
                    e.ChildNum = childNum;
                    e.OrderAmt = orderAmt;
                    e.OrderCost = totalCost;
                    var profit = orderAmt - totalCost;
                    e.OrderProfit = profit;
                    if (orderAmt != 0)
                        e.ProfitRate = profit / orderAmt * 100;
                    e.Save();
                    #endregion

                    #region 导游领汇款(备用金)
                    if (dataStatus == 1) //当预算时才有备用金
                    {
                        var d = GetDrawMoney(orderID);
                        if (d == null)
                        {
                            d = new DAL.Ord_DrawMoney();
                            d.ID = orderID;
                            d.OrderID = orderID;
                            d.OrgID = user.OrgID;
                            d.DataStatus = 1;//未领取
                        }
                        d.Amount = drawMoney;
                        d.Method = drawMoneyMethod;
                        d.Comment = drawMoneyComment;
                        d.Save();
                    }
                    #endregion

                    #region 冗余数据写入订单表

                    if (orderType == OrderType.QYT) //企业团预算或决算将计算的值冗余写入订单主表
                    {
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
                        var order = dal.GetOrderInfo(orderID);
                        order.AdultNum = adultNum;
                        order.ChildNum = childNum;
                        order.OrderAmt = orderAmt;
                        order.BudgetStatus = budgetStatus;
                        if (budgetStatus == 7)
                            order.OrderStatus = (int)OrderStatus.Completed;
                        else
                            order.OrderStatus = (int)OrderStatus.Confirmed;
                        order.OrderCost = totalCost;
                        order.SupplierName = supplierName;
                        order.ID = orderID;
                        order.UpdateUserName = user.UserName;
                        order.Save();
                    }
                    else //自主班团预算写入扩展表：Ord_OrderExtend
                    {
                        var ordExtend = dal.GetOrderExtend(orderID);
                        if (ordExtend == null)
                        {
                            ordExtend = new DAL.Ord_OrderExtend();
                            ordExtend.ID = orderID;
                            ordExtend.OrderID = orderID;
                            ordExtend.OrgID = user.OrgID;
                            ordExtend.PaidAmt = 0;
                            ordExtend.OrderInvoiceAmt = 0;
                            ordExtend.CostInvoiceAmt = 0;
                        }
                        if (budgetStatus == 7)
                            ordExtend.OrderStatus = (int)OrderStatus.Completed;
                        else
                            ordExtend.OrderStatus = (int)OrderStatus.Confirmed;
                        ordExtend.AdultNum = adultNum;
                        ordExtend.ChildNum = childNum;
                        ordExtend.OrderAmt = orderAmt;
                        ordExtend.OrderCost = totalCost;
                        ordExtend.OrderProfit = e.OrderProfit;
                        ordExtend.ProfitRate = e.ProfitRate;
                        ordExtend.DrawMoneyAmt = drawMoney;
                        ordExtend.BudgetStatus = budgetStatus;
                        ordExtend.Save();
                    }
                    #endregion

                    #region 预算备注
                    if (dataStatus == 1) //预算操作
                    {
                        DAL.Ord_BudgetComment.Delete(x => x.OrderBudgetID == orderID);
                        if (!string.IsNullOrEmpty(xmlComment))
                        {
                            var doc = new XmlDocument();
                            doc.LoadXml(xmlComment);
                            var nodes = doc.SelectNodes("document/data");
                            foreach (XmlNode node in nodes)
                            {
                                var cEntity = new DAL.Ord_BudgetComment();
                                cEntity.ID = Guid.NewGuid().ToString();
                                cEntity.OrderBudgetID = orderID;
                                cEntity.Name = node.GetNodeValue("name");
                                cEntity.Comment = node.GetNodeValue("comment");
                                cEntity.OrgID = user.OrgID;
                                cEntity.Save();
                            }
                        }
                    }
                    #endregion

                    #region 订单附件

                    //删除
                    DAL.Ord_OrderFile.Find(x => x.OrderID == orderID).ToList().ForEach(x =>
                    {
                        DAL.Ord_OrderFile.Delete(y => y.ID == x.ID);
                    });


                    if (!string.IsNullOrEmpty(fileID))
                    {
                        foreach (var f in fileID.Split(','))
                        {
                            var fentity = new DAL.Ord_OrderFile();
                            fentity.ID = Guid.NewGuid().ToString();
                            fentity.OrderID = orderID;
                            fentity.FilleD = f;
                            fentity.OrgID = user.OrgID;
                            fentity.CreateUserID = user.UserID;
                            fentity.CreateUserName = user.UserName;
                            fentity.Save();
                        }
                    }
                    #endregion

                    #region 订单日志
                    var title = isInsert ? "提交预算" : "修改预算";
                    dal.InsertLog(orderID, title);
                    #endregion

                    scope.Complete();
                }

                //更新供应商交易金额 
                itemArr.ForEach(item =>
                {
                    orderDAL.UpdateResourceInfo((ResourceType)item.ItemType, item.SupplierID, user);
                });
            }
            catch (Exception ex)
            {
                isRec = false;
                BizUtility.ExceptionHandler(user, ex, "保存预决算时发生错误");
            }
            return isRec;
        }

        /// <summary>
        /// 获取供应商资料表
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private string ConvertResourceTableName(int itemType, string orderID, string Supplier, OrderType orderType)
        {
            var orderDAL = new Order_BF();
            var user = AuthenticationPage.UserInfo;

            var supplierID = "";
            #region tableName
            switch ((ResourceType)itemType)
            {
                case ResourceType.TravelAgency:
                    #region ScenicTicket
                    DAL.Res_TravelAgency entityTravelAgency = new DAL.Res_TravelAgency();
                    entityTravelAgency.ID = Guid.NewGuid().ToString();
                    if (orderType == OrderType.QYT)
                    {
                        var en = orderDAL.GetOrderInfo(orderID);
                        entityTravelAgency.RouteTypeID = en.RouteTypeID;
                        entityTravelAgency.DestinationID = en.DestinationID;
                    }
                    else
                    {
                        var en = orderDAL.GetTourInfo(orderID);
                        var enRoute = orderDAL.GetRouteInfo(en.RouteID);
                        entityTravelAgency.RouteTypeID = enRoute.RouteTypeID;
                        entityTravelAgency.DestinationID = enRoute.DestinationID;
                    }
                    var rEntity = new BasicInfo_BF().Get(entityTravelAgency.RouteTypeID);
                    entityTravelAgency.RouteType = rEntity == null ? "" : rEntity.Name;
                    var dEntity = new Destination_BF().Get(entityTravelAgency.DestinationID);
                    entityTravelAgency.Destination = dEntity == null ? "" : dEntity.Name;
                    entityTravelAgency.DestinationPath = new Destination_BF().GetDestinationPathID(entityTravelAgency.DestinationID);
                    entityTravelAgency.Name = Supplier;
                    var chTravelAgency = new EcanConvertToCh().convertCh(entityTravelAgency.Name.Substring(0, 1));
                    entityTravelAgency.Spell = chTravelAgency.Length > 0 ? chTravelAgency.Substring(0, 1).ToUpper() : "";
                    entityTravelAgency.Brand = Supplier;
                    entityTravelAgency.OrgID = user.OrgID;
                    entityTravelAgency.DeptID = user.DeptID;
                    entityTravelAgency.IsEnable = true;
                    entityTravelAgency.TradeNum = 0;
                    entityTravelAgency.TradeAdultNum = 0;
                    entityTravelAgency.TradeAmt = 0;
                    entityTravelAgency.TradeChildNum = 0;
                    entityTravelAgency.OrderIndex = 0;
                    entityTravelAgency.CreateDate = DateTime.Now;
                    entityTravelAgency.CreateUserID = user.UserID;
                    entityTravelAgency.CreateUserName = user.UserName;
                    entityTravelAgency.Save();
                    supplierID = entityTravelAgency.ID;
                    #endregion
                    break;
                case ResourceType.ScenicTicket:
                    #region ScenicTicket
                    DAL.Res_ScenicTicket entiytScenicTicket = new DAL.Res_ScenicTicket();
                    entiytScenicTicket.ID = Guid.NewGuid().ToString();
                    if (orderType == OrderType.QYT)
                    {
                        var en = orderDAL.GetOrderInfo(orderID);
                        entiytScenicTicket.RouteTypeID = en.RouteTypeID;
                        entiytScenicTicket.DestinationID = en.DestinationID;
                    }
                    else
                    {
                        var en = orderDAL.GetTourInfo(orderID);
                        var enRoute = orderDAL.GetRouteInfo(en.RouteID);
                        entiytScenicTicket.RouteTypeID = enRoute.RouteTypeID;
                        entiytScenicTicket.DestinationID = enRoute.DestinationID;
                    }
                    entiytScenicTicket.Name = Supplier;
                    var chScenicTicket = new EcanConvertToCh().convertCh(entiytScenicTicket.Name.Substring(0, 1));
                    entiytScenicTicket.Spell = chScenicTicket.Length > 0 ? chScenicTicket.Substring(0, 1).ToUpper() : "";
                    entiytScenicTicket.IsEnable = true;
                    entiytScenicTicket.NormalPrice = "0";
                    entiytScenicTicket.TradeNum = 0;
                    entiytScenicTicket.TradeAdultNum = 0;
                    entiytScenicTicket.TradeAmt = 0;
                    entiytScenicTicket.TradeChildNum = 0;
                    entiytScenicTicket.OrderIndex = 0;
                    entiytScenicTicket.CreateDate = DateTime.Now;
                    entiytScenicTicket.CreateUserID = user.UserID;
                    entiytScenicTicket.CreateUserName = user.UserName;
                    entiytScenicTicket.OrgID = user.OrgID;
                    entiytScenicTicket.DeptID = user.DeptID;
                    entiytScenicTicket.Save();
                    supplierID = entiytScenicTicket.ID;
                    #endregion
                    break;
                case ResourceType.Guide:
                    #region Guide
                    DAL.Res_Guide entiytGuide = new DAL.Res_Guide();
                    entiytGuide.ID = Guid.NewGuid().ToString();
                    if (orderType == OrderType.QYT)
                    {
                        var en = orderDAL.GetOrderInfo(orderID);
                        entiytGuide.DepartureID = en.DestinationID;
                        entiytGuide.DepartureName = en.DestinationName;
                    }
                    else
                    {
                        var en = orderDAL.GetTourInfo(orderID);
                        var enRoute = orderDAL.GetRouteInfo(en.RouteID);
                        entiytGuide.DepartureID = enRoute.DestinationID;
                        entiytGuide.DepartureName = enRoute.Destination;
                    }
                    entiytGuide.Name = Supplier;
                    var chGuide = new EcanConvertToCh().convertCh(entiytGuide.Name.Substring(0, 1));
                    entiytGuide.Spell = chGuide.Length > 0 ? chGuide.Substring(0, 1).ToUpper() : "";
                    entiytGuide.IsLeaderCard = false;
                    entiytGuide.IsEnable = true;
                    entiytGuide.IsIDCard = false;
                    entiytGuide.TradeNum = 0;
                    entiytGuide.TradeAdultNum = 0;
                    entiytGuide.TradeAmt = 0;
                    entiytGuide.TradeChildNum = 0;
                    entiytGuide.OrderIndex = 0;
                    entiytGuide.CreateDate = DateTime.Now;
                    entiytGuide.CreateUserID = user.UserID;
                    entiytGuide.CreateUserName = user.UserName;
                    entiytGuide.OrgID = user.OrgID;
                    entiytGuide.DeptID = user.DeptID;
                    entiytGuide.Sex = "男";
                    entiytGuide.Language ="普通话" ;
                    entiytGuide.GuideLevel = "初级";
                    entiytGuide.Save();
                    supplierID = entiytGuide.ID;
                    #endregion
                    break;
                case ResourceType.Hotel:
                    #region Hotel
                    DAL.Res_Hotel entiytHotel = new DAL.Res_Hotel();
                    entiytHotel.ID = Guid.NewGuid().ToString();
                    if (orderType == OrderType.QYT)
                    {
                        var en = orderDAL.GetOrderInfo(orderID);
                        entiytHotel.RouteType = en.RouteTypeName;
                        entiytHotel.RouteTypeID = en.RouteTypeID;
                        entiytHotel.DestinationID = en.DestinationID;
                        entiytHotel.DestinationPath = en.DestinationPath;
                        entiytHotel.Destination = en.DestinationName;
                    }
                    else
                    {
                        var en = orderDAL.GetTourInfo(orderID);
                        var enRoute = orderDAL.GetRouteInfo(en.RouteID);
                        entiytHotel.RouteType = enRoute.RouteType;
                        entiytHotel.RouteTypeID = enRoute.RouteTypeID;
                        entiytHotel.DestinationID = enRoute.DestinationID;
                        entiytHotel.DestinationPath = enRoute.DestinationPath;
                        entiytHotel.Destination = enRoute.Destination;
                    }
                    entiytHotel.Name = Supplier;
                    var chHotel = new EcanConvertToCh().convertCh(entiytHotel.Name.Substring(0, 1));
                    entiytHotel.Spell = chHotel.Length > 0 ? chHotel.Substring(0, 1).ToUpper() : "";
                    entiytHotel.IsEnable = true;
                    entiytHotel.TradeNum = 0;
                    entiytHotel.TradeAdultNum = 0;
                    entiytHotel.TradeAmt = 0;
                    entiytHotel.TradeChildNum = 0;
                    entiytHotel.OrderIndex = 0;
                    entiytHotel.CreateDate = DateTime.Now;
                    entiytHotel.CreateUserID = user.UserID;
                    entiytHotel.CreateUserName = user.UserName;
                    entiytHotel.OrgID = user.OrgID;
                    entiytHotel.DeptID = user.DeptID;
                    entiytHotel.Save();
                    supplierID = entiytHotel.ID;
                    #endregion
                    break;
                case ResourceType.Motorcade:
                    #region Motorcade
                    DAL.Res_Motorcade entiytMotorcade = new DAL.Res_Motorcade();
                    entiytMotorcade.ID = Guid.NewGuid().ToString();
                    if (orderType == OrderType.QYT)
                    {
                        var en = orderDAL.GetOrderInfo(orderID);
                        entiytMotorcade.Departure = en.Departure;
                        entiytMotorcade.DepartureID = en.DepartureID;
                    }
                    else
                    {
                        var en = orderDAL.GetTourInfo(orderID);
                        var enRoute = orderDAL.GetRouteInfo(en.RouteID);
                        entiytMotorcade.Departure = enRoute.Destination;
                        entiytMotorcade.DepartureID = enRoute.DestinationID;
                    }
                    entiytMotorcade.Name = Supplier;
                    var chMotorcade = new EcanConvertToCh().convertCh(entiytMotorcade.Name.Substring(0, 1));
                    entiytMotorcade.Spell = chMotorcade.Length > 0 ? chMotorcade.Substring(0, 1).ToUpper() : "";
                    entiytMotorcade.IsEnable = true;
                    entiytMotorcade.TradeNum = 0;
                    entiytMotorcade.TradeAdultNum = 0;
                    entiytMotorcade.TradeAmt = 0;
                    entiytMotorcade.TradeChildNum = 0;
                    entiytMotorcade.OrderIndex = 0;
                    entiytMotorcade.CreateDate = DateTime.Now;
                    entiytMotorcade.CreateUserID = user.UserID;
                    entiytMotorcade.CreateUserName = user.UserName;
                    entiytMotorcade.OrgID = user.OrgID;
                    entiytMotorcade.DeptID = user.DeptID;
                    entiytMotorcade.Save();
                    supplierID = entiytMotorcade.ID;
                    #endregion
                    break;
                case ResourceType.VisaAgency:
                    #region VisaAgency
                    DAL.Res_Visa entiytVisa = new DAL.Res_Visa();
                    entiytVisa.ID = Guid.NewGuid().ToString();
                    entiytVisa.Name = Supplier;
                    var chVisaAgency = new EcanConvertToCh().convertCh(entiytVisa.Name.Substring(0, 1));
                    entiytVisa.Spell = chVisaAgency.Length > 0 ? chVisaAgency.Substring(0, 1).ToUpper() : "";
                    entiytVisa.IsEnable = true;
                    entiytVisa.TradeNum = 0;
                    entiytVisa.TradeAdultNum = 0;
                    entiytVisa.TradeAmt = 0;
                    entiytVisa.TradeChildNum = 0;
                    entiytVisa.OrderIndex = 0;
                    entiytVisa.CreateDate = DateTime.Now;
                    entiytVisa.CreateUserID = user.UserID;
                    entiytVisa.CreateUserName = user.UserName;
                    entiytVisa.OrgID = user.OrgID;
                    entiytVisa.DeptID = user.DeptID;
                    entiytVisa.Save();
                    supplierID = entiytVisa.ID;
                    #endregion
                    break;
                case ResourceType.Insurer:
                    #region Insurer
                    DAL.Res_Insurance entiytInsurer = new DAL.Res_Insurance();
                    entiytInsurer.ID = Guid.NewGuid().ToString();
                    entiytInsurer.Name = Supplier;
                    var chInsurer = new EcanConvertToCh().convertCh(entiytInsurer.Name.Substring(0, 1));
                    entiytInsurer.Spell = chInsurer.Length > 0 ? chInsurer.Substring(0, 1).ToUpper() : "";
                    entiytInsurer.IsEnable = true;
                    entiytInsurer.TradeNum = 0;
                    entiytInsurer.TradeAdultNum = 0;
                    entiytInsurer.TradeAmt = 0;
                    entiytInsurer.TradeChildNum = 0;
                    entiytInsurer.OrderIndex = 0;
                    entiytInsurer.CreateDate = DateTime.Now;
                    entiytInsurer.CreateUserID = user.UserID;
                    entiytInsurer.CreateUserName = user.UserName;
                    entiytInsurer.OrgID = user.OrgID;
                    entiytInsurer.DeptID = user.DeptID;
                    entiytInsurer.Save();
                    supplierID = entiytInsurer.ID;
                    #endregion
                    break;
                case ResourceType.Shopping:
                    #region Shopping
                    DAL.Res_Shopping entiytShopping = new DAL.Res_Shopping();
                    entiytShopping.ID = Guid.NewGuid().ToString();
                    if (orderType == OrderType.QYT)
                    {
                        var en = orderDAL.GetOrderInfo(orderID);
                        entiytShopping.RouteType = en.RouteTypeName;
                        entiytShopping.RouteTypeID = en.RouteTypeID;
                        entiytShopping.Destination = en.DestinationName;
                        entiytShopping.DestinationID = en.DestinationID;
                        entiytShopping.DestinationPath = en.DestinationPath;
                    }
                    else
                    {
                        var en = orderDAL.GetTourInfo(orderID);
                        var enRoute = orderDAL.GetRouteInfo(en.RouteID);
                        entiytShopping.RouteType = enRoute.RouteType;
                        entiytShopping.RouteTypeID = enRoute.RouteTypeID;
                        entiytShopping.Destination = enRoute.Destination;
                        entiytShopping.DestinationID = enRoute.DestinationID;
                        entiytShopping.DestinationPath = enRoute.DestinationPath;
                    }
                    entiytShopping.Name = Supplier;
                    var chShopping = new EcanConvertToCh().convertCh(entiytShopping.Name.Substring(0, 1));
                    entiytShopping.Spell = chShopping.Length > 0 ? chShopping.Substring(0, 1).ToUpper() : "";
                    entiytShopping.IsEnable = true;
                    entiytShopping.TradeNum = 0;
                    entiytShopping.TradeAdultNum = 0;
                    entiytShopping.TradeAmt = 0;
                    entiytShopping.TradeChildNum = 0;
                    entiytShopping.OrderIndex = 0;
                    entiytShopping.CreateDate = DateTime.Now;
                    entiytShopping.CreateUserID = user.UserID;
                    entiytShopping.CreateUserName = user.UserName;
                    entiytShopping.OrgID = user.OrgID;
                    entiytShopping.DeptID = user.DeptID;
                    entiytShopping.Save();
                    supplierID = entiytShopping.ID;
                    #endregion
                    break;
                case ResourceType.TicketAgency:
                    #region TicketAgency
                    DAL.Res_TicketAgency entiytTicketAgency = new DAL.Res_TicketAgency();
                    entiytTicketAgency.ID = Guid.NewGuid().ToString();
                    entiytTicketAgency.Name = Supplier;
                    var chTicketAgency = new EcanConvertToCh().convertCh(entiytTicketAgency.Name.Substring(0, 1));
                    entiytTicketAgency.Spell = chTicketAgency.Length > 0 ? chTicketAgency.Substring(0, 1).ToUpper() : "";
                    entiytTicketAgency.IsEnable = true;
                    entiytTicketAgency.TradeNum = 0;
                    entiytTicketAgency.TradeAdultNum = 0;
                    entiytTicketAgency.TradeAmt = 0;
                    entiytTicketAgency.TradeChildNum = 0;
                    entiytTicketAgency.OrderIndex = 0;
                    entiytTicketAgency.CreateDate = DateTime.Now;
                    entiytTicketAgency.CreateUserID = user.UserID;
                    entiytTicketAgency.CreateUserName = user.UserName;
                    entiytTicketAgency.OrgID = user.OrgID;
                    entiytTicketAgency.DeptID = user.DeptID;
                    entiytTicketAgency.Save();
                    supplierID = entiytTicketAgency.ID;
                    #endregion
                    break;
                case ResourceType.OtherRes:
                    #region OtherRes
                    DAL.Res_Other entiytOtherRes = new DAL.Res_Other();
                    entiytOtherRes.ID = Guid.NewGuid().ToString();
                    entiytOtherRes.Name = Supplier;
                    var chOtherRes = new EcanConvertToCh().convertCh(entiytOtherRes.Name.Substring(0, 1));
                    entiytOtherRes.Spell = chOtherRes.Length > 0 ? chOtherRes.Substring(0, 1).ToUpper() : "";
                    entiytOtherRes.IsEnable = true;
                    entiytOtherRes.TradeNum = 0;
                    entiytOtherRes.TradeAdultNum = 0;
                    entiytOtherRes.TradeAmt = 0;
                    entiytOtherRes.TradeChildNum = 0;
                    entiytOtherRes.OrderIndex = 0;
                    entiytOtherRes.CreateDate = DateTime.Now;
                    entiytOtherRes.CreateUserID = user.UserID;
                    entiytOtherRes.CreateUserName = user.UserName;
                    entiytOtherRes.OrgID = user.OrgID;
                    entiytOtherRes.DeptID = user.DeptID;
                    entiytOtherRes.Save();
                    supplierID = entiytOtherRes.ID;
                    #endregion
                    break;
            }
            #endregion
            return supplierID;
        }
    }
}
