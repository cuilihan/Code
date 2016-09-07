using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DRP.BF.ProMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.BF.Order
{
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 同行散客订单
        /// </summary>
        THSK = 1,
        /// <summary>
        /// 自主班散客
        /// </summary>
        ZZBSK = 2,
        /// <summary>
        /// 企业团（整团）
        /// </summary>
        QYT = 3,
        /// <summary>
        /// 自主班团
        /// </summary>
        ZZBT = 4,
        /// <summary>
        /// 单项业务
        /// </summary>
        DXYW = 5,
        /// <summary>
        /// 机票订单
        /// </summary>
        AirTicket = 6
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 待确认
        /// </summary>
        ToConfirm = 1,
        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed = 2,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 3,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 4,
        /// <summary>
        /// 未取消订单（用于查询）
        /// </summary>
        NonCancelOrder
    }

    /// <summary>
    /// 预决算状态（团队订单）
    /// </summary>
    public enum OrderBudgetStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 未做预决算
        /// </summary>
        NoBudget = 1,
        /// <summary>
        /// 已预算
        /// </summary>
        BudgetDone = 2,
        /// <summary>
        /// 预算审核（暂时保留，简化流程)
        /// </summary>
        BudgetAudit = 3,
        /// <summary>
        /// 预算确认
        /// </summary>
        BudgetConfirm = 4,
        /// <summary>
        /// 已决算
        /// </summary>
        FinalDone = 5,
        /// <summary>
        /// 决算审核（暂时保留，简化流程）
        /// </summary>
        FinalAudit = 6,
        /// <summary>
        /// 决算确认
        /// </summary>
        FinalConfirm = 7
    }

    /// <summary>
    /// 订单查询条件
    /// </summary>
    public class    OrderCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType OrdType { get; set; }

        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 客户（公司）名称
        /// </summary>
        public string CusotmerName { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 1：出团日期 2：录入日期
        /// </summary>
        public int DateType { get; set; }

        /// <summary>
        /// 日期区间
        /// </summary>
        public DateScope QryDate { get; set; }

        /// <summary>
        /// 线路类型ID
        /// </summary>
        public string RouteTypeID { get; set; }

        /// <summary>
        /// 目的地ID
        /// </summary>
        public string DestinationID { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus OrdStatus { get; set; }

        /// <summary>
        /// 团队订单预决算状态
        /// </summary>
        public OrderBudgetStatus BudgetStatus { get; set; }

        /// <summary>
        /// 创建部门ID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreateUserID { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public string OrderSourceID { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public string OrderSourceName { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmt { get; set; }


        private bool _OrderClaimQry = false;
        /// <summary>
        /// 是否是订单收款认领查询（专用于收款认领）
        /// </summary>
        public bool OrderClaimQry
        {
            get
            {
                return _OrderClaimQry;
            }
            set
            {
                _OrderClaimQry = value;
            }
        }

        /// <summary>
        /// 参与人员部门ID
        /// </summary>
        public string PartDeptID { get; set; }

        /// <summary>
        /// 参与人员ID
        /// </summary>
        public string ParticipantID { get; set; }
        /// <summary>
        /// 参与人员
        /// </summary>
        public string Participant { get; set; }

        /// <summary>
        /// 参与人员勾选状态
        /// </summary>
        public string PartStatus { get; set; }

        /// <summary>
        /// 客户公司名称
        /// </summary>
        public string Company { get; set; }

        ///<summary>
        /// 提交人
        /// </summary>
        public string CreateUserName { get; set; }

        ///<summary>
        /// 最后操作人
        /// </summary>
        public string UpdateUserName { get; set; }

        /// <summary>
        /// 未付款区间
        /// </summary>
        public string sUnCollectedAmt { get; set; }

        public string eUnCollectedAmt { get; set; }
    }


    /// <summary>
    /// 订单通用类
    /// </summary>
    public class OrderUtility
    {
        /// <summary>
        /// 转化订单客户实体
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderCustomer> ConverToCustomerEntity(string xmlData)
        {
            var list = new List<DAL.Ord_OrderCustomer>();
            if (string.IsNullOrEmpty(xmlData))
                return list;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            var idx = 0;
            foreach (XmlNode node in nodes)
            {
                var e = new DAL.Ord_OrderCustomer();
                e.ID = Guid.NewGuid().ToString();
                var cid = node.GetNodeValue("CustomerID");
                e.CustomerID = string.IsNullOrEmpty(cid) ? Guid.NewGuid().ToString() : cid;
                e.Name = node.GetNodeValue("Name");
                e.Sex = node.GetNodeValue("Sex");
                e.Mobile = node.GetNodeValue("Mobile");
                e.IDNo = node.GetNodeValue("IDNo");
                var cardType = node.GetNodeValue("CardType");
                e.IDType = string.IsNullOrEmpty(cardType) ? "身份证" : cardType;
                e.Company = node.GetNodeValue("Company");
                e.Comment = node.GetNodeValue("Comment");
                e.IsLeader = node.GetNodeValue("IsLeader").ToBoolen();
                e.OrderIndex = idx++;
                if (string.IsNullOrEmpty(e.Name) && string.IsNullOrEmpty(e.Mobile)
                    && string.IsNullOrEmpty(e.IDNo) && string.IsNullOrEmpty(e.Comment))
                {
                    continue;
                }
                else
                    list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// 转化订单成本项目实体
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderCostItem> ConverToCostItemEntity(string xmlData)
        {
            var list = new List<DAL.Ord_OrderCostItem>();
            if (string.IsNullOrEmpty(xmlData))
                return list;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            var idx = 0;
            foreach (XmlNode node in nodes)
            {
                var keyID = node.GetNodeValue("ID");
                var e = new DAL.Ord_OrderCostItem();
                e.ID = string.IsNullOrEmpty(keyID) ? Guid.NewGuid().ToString() : keyID;
                e.ItemType = node.GetNodeValue("ItemType").ToInt();
                e.ItemName = node.GetNodeValue("ItemName");
                e.SupplierID = node.GetNodeValue("SupplierID");
                e.Supplier = node.GetNodeValue("Supplier");
                e.CostAmt = node.GetNodeValue("CostAmt").ToDecimal();
                e.Comment = node.GetNodeValue("Comment");
                e.OrderIndex = idx++;
                if (string.IsNullOrEmpty(e.SupplierID) && e.CostAmt == 0
                    && string.IsNullOrEmpty(e.Comment))
                {
                    continue;
                }
                else
                    list.Add(e);
            }
            return list;

        }

        /// <summary>
        /// 转化订单价格策略实体
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderPrice> ConverToOrderPriceEntity(string xmlData)
        {
            var list = new List<DAL.Ord_OrderPrice>();
            if (string.IsNullOrEmpty(xmlData))
                return list;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            var dal = new TourInfo_BF();
            foreach (XmlNode node in nodes)
            {
                var tourPriceID = node.GetNodeValue("TourPriceID");
                var tEntity = dal.GetTourPriceDetail(tourPriceID);
                var keyID = node.GetNodeValue("ID");
                var e = new DAL.Ord_OrderPrice();
                e.ID = string.IsNullOrEmpty(keyID) ? Guid.NewGuid().ToString() : keyID;
                e.TourPriceID = tourPriceID;
                e.Name = tEntity.Name;
                e.SalePrice = tEntity.SalePrice;
                e.Rebate = tEntity.Rebate;
                e.VisitorNum = node.GetNodeValue("VisitorNum").ToInt();
                e.RoomRate = node.GetNodeValue("RoomRate").ToInt();
                e.IsSeat = tEntity.IsSeat;
                e.IsChild = tEntity.IsChild;
                e.InsuranceAmt = node.GetNodeValue("InsuranceAmt").ToDecimal();
                e.InsuranceCost = node.GetNodeValue("InsuranceCost").ToDecimal();
                list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// 删除订单成本
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public void DeleteOrderCostItem(string keyID)
        {
            DAL.Ord_OrderCostItem.Delete(x => x.ID == keyID);
        }
    }
}
