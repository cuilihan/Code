using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Xml;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.DAL.Model;
using DRP.Framework;
using DRP.Message;
using DRP.Message.Core;

namespace DRP.BF.CheckAccount
{
    /// <summary>
    /// 导游报账
    /// </summary>
    public class CheckAccount_BF
    {
        DRPDB db = new DRPDB();

        #region 报账数据查询

        /// <summary>
        /// 查询报账的订单
        /// </summary>
        /// <param name="guideID">导游手机号 对应Ord_OrderGuide</param>
        /// <param name="flag">0:本月  1：一月以前</param>
        /// <returns></returns>
        public List<V_Ord_CheckAccountEntity> GetOrderCheckAccount(int flag)
        {
            var list = new List<V_Ord_CheckAccountEntity>();
            var userData = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(userData)) return list;
            var arr = userData.Split('@');
            if (arr.Length == 0) return list;
            return new GuideCheckAccountDAL().GetOrderCheckAccount(arr[0], arr[1], arr[2], flag);
        }

        /// <summary>
        /// 导游领款金额
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public decimal GuideDrawMoney(string orderID)
        {
            var list = DAL.Ord_DrawMoney.Find(x => x.OrderID == orderID).ToList();
            var e = list.Count == 0 ? null : list.First();
            return e == null ? 0 : e.Amount;
        }

        public DAL.Ord_OrderGuide GetOrderGuide(string keyID)
        {
            return DAL.Ord_OrderGuide.SingleOrDefault(x => x.ID == keyID);
        }

        #endregion

        #region 保存报账数据
        /// <summary>
        /// 导游报账数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public bool Save(DAL.Ord_OrderBalance e, string xmlData)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 团队安排的导游报账状态变更
                    var orderGuideEntity = GetOrderGuide(e.OrderGuideID);
                    orderGuideEntity.IsOver = 1;
                    orderGuideEntity.OrderBalanceID = e.ID;
                    orderGuideEntity.Save();
                    #endregion

                    var isInsert = false;

                    #region 报账主表
                    var entity = GetOrderBalance(e.ID);
                    if (entity == null)
                    {
                        isInsert = true;
                        entity = new Ord_OrderBalance();
                        entity.ID = e.ID;
                        entity.OrderID = e.OrderID;
                        entity.OrderType = e.OrderType;
                        entity.OrderGuideID = e.OrderGuideID;
                        entity.GuideName = e.GuideName;
                        entity.GuideMobile = e.GuideMobile;
                        entity.CreateDate = DateTime.Now;
                        entity.OrgID = orderGuideEntity.OrgID;
                    }
                    entity.AdultNum = e.AdultNum;
                    entity.ChildNum = e.ChildNum;
                    entity.YLTK = e.YLTK;
                    entity.YLTK_Remark = e.YLTK_Remark;
                    entity.XSTK = e.XSTK;
                    entity.XSTK_Remark = e.XSTK_Remark;
                    entity.YHZZ = e.YHZZ;
                    entity.YHZZ_Remark = e.YHZZ_Remark;
                    entity.QTSR = e.QTSR;
                    entity.QTSR_Remark = e.QTSR_Remark;
                    entity.DataStatus = e.DataStatus;

                    entity.Save();
                    #endregion

                    #region 报账项目表
                    DAL.Ord_OrderBalanceItem.Delete(x => x.OrderBalanceID == e.ID);
                    DAL.Ord_OrderBalanceItemDatum.Delete(x => x.OrderBalanceID == e.ID);

                    var doc = new XmlDocument();
                    doc.LoadXml(xmlData);
                    var nodes = doc.SelectNodes("document/data");
                    var idx = 1;
                    var c = 1;
                    var dict = new Dictionary<int, string>();
                    foreach (XmlNode node in nodes)
                    {
                        var orderBalanceItemID = Guid.NewGuid().ToString();
                        var itemType = node.Attributes["type"].InnerText.ToInt();
                        var itemName = node.Attributes["name"].InnerText;
                        if (!dict.ContainsKey(itemType))
                        {
                            dict.Add(itemType, orderBalanceItemID);

                            var item = new DAL.Ord_OrderBalanceItem();
                            item.ID = orderBalanceItemID;
                            item.OrderBalanceID = entity.ID;
                            item.ItemName = itemName;
                            item.ItemType = itemType;
                            item.pSort = idx++;
                            item.OrgID = entity.OrgID;
                            item.Save();
                        }

                        var itemEntity = new DAL.Ord_OrderBalanceItemDatum();
                        itemEntity.ID = Guid.NewGuid().ToString();
                        itemEntity.OrderBalanceID = entity.ID;
                        itemEntity.ItemID = dict[itemType];
                        itemEntity.ItemName = node.GetNodeValue("sName");
                        itemEntity.ItemPrice = node.GetNodeValue("Price").ToDecimal();
                        itemEntity.ItemNum = node.GetNodeValue("Num").ToInt();
                        itemEntity.ItemAmt = node.GetNodeValue("ItemAmt").ToDecimal();
                        itemEntity.IsSign = node.GetNodeValue("isSign").Equals("1");
                        itemEntity.IsInvoice = node.GetNodeValue("isInvoice").Equals("1");
                        itemEntity.pSort = c++;
                        itemEntity.OrgID = entity.OrgID;

                        itemEntity.Save();
                    }
                    #endregion

                    //消息实体
                    var m_Entity = new MessageEntity();
                    m_Entity.KeyID = Guid.NewGuid().ToString();
                    m_Entity.SendUserID = e.ID;
                    m_Entity.SendUserName = e.GuideName;
                    m_Entity.MsgTitle = isInsert ? "导游【" + e.GuideName + "】提交报账单" : "导游【" + e.GuideName + "】修改报账单";
                    m_Entity.OrgID = entity.OrgID;
                    m_Entity.URL = "/Module/CheckAccount/ViewBill.aspx?id=" + e.OrderGuideID + "&xType=" + entity.OrderType + "&orderBalanceID=" + entity.ID + "&mID=" + m_Entity.KeyID;
                    m_Entity.DataStatus = (int)MessageStatus.UnRead;
                    m_Entity.Target = "_blank";

                    #region 更新报账状态
                    if (e.OrderType == (int)OrderType.QYT)
                    {
                        var oEntity = new Order_BF().GetOrderInfo(e.OrderID);
                        oEntity.IsCheckAccount = true;//已报账
                        oEntity.Save();

                        m_Entity.RecUserID = oEntity.CreateUserID;
                        m_Entity.RecUserName = oEntity.CreateUserName;
                    }
                    else
                    {
                        var oEntity = new Order_BF().GetOrderExtend(e.OrderID);
                        if (oEntity != null)
                        {
                            oEntity.IsCheckAccount = true;
                            oEntity.Save();

                            var tour = new TourInfo_BF().Get(e.OrderID);
                            if (tour != null)
                            {
                                m_Entity.RecUserID = tour.CreateUserID;
                                m_Entity.RecUserName = tour.CreateUserName;
                            }
                        }
                    }
                    #endregion

                    #region 发消息提醒
                    new MessageHelper().CreateInstance(DRP.Message.MessageType.SysMessage).Send(m_Entity);
                    #endregion

                    scope.Complete();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 已报账数据查询

        /// <summary>
        /// 导游报账的游客人数
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderVisitorNum GetOrderVisitorNum(string orderID)
        {
            return new GuideCheckAccountDAL().OrderCheckAccountVisitorNum(orderID);
        }

        /// <summary>
        /// 已报账数据主表
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Ord_OrderBalance GetOrderBalance(string keyID)
        {
            return DAL.Ord_OrderBalance.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 报账项目
        /// </summary>
        /// <param name="orderBalanceID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderBalanceItem> GetOrderBalanceItem(string orderBalanceID)
        {
            return DAL.Ord_OrderBalanceItem.Find(x => x.OrderBalanceID == orderBalanceID).OrderBy(x => x.pSort).ToList();
        }

        /// <summary>
        /// 项目明细
        /// </summary>
        /// <param name="orderBalaceID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderBalanceItemDatum> GetOrderBalanceItemData(string orderBalaceID)
        {
            return DAL.Ord_OrderBalanceItemDatum.Find(x => x.OrderBalanceID == orderBalaceID).OrderBy(x => x.pSort).ToList();
        }

        /// <summary>
        /// 报账单成本总额
        /// </summary>
        /// <param name="orderBalanceID"></param>
        /// <returns></returns>
        public Decimal OrderBalanceCostAmt(string orderBalanceID)
        {
            return new GuideCheckAccountDAL().OrderCheckAccountCostAmount(orderBalanceID);
        }

        #endregion

        #region 报账单确认
        /// <summary>
        /// 报账单确认
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public bool CheckAccountConfirm(string keyID)
        {
            try
            {
                var e = GetOrderGuide(keyID);
                e.IsOver = 2;
                e.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
