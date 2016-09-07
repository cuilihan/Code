using DRP.DataService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DRP.Framework;
using DRP.DAL;
using DRP.BF;

namespace DRP.DataService
{
    public class OrderXmlToEntity
    {
        /// <summary>
        /// Xml转化订单实体
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        internal BTBOrderEntity ConvertXmlToEntity(string xmlData, string otaName)
        {
            var t = new BTBOrderEntity();
            var xml = new XmlDocument();
            xml.LoadXml(xmlData);
            var node = xml.SelectSingleNode("OrderDetail");
            t.ID = Guid.Parse(node.GetNodeValue("Guid"));
            t.BTBSerial = node.GetNodeValue("Serial");
            t.BTBCode = node.GetNodeValue("Code");
            t.RouteName = node.GetNodeValue("Name");
            var createGuid = node.GetNodeValue("CreateGuid");
            var user = GetCreateUserInfo(createGuid);//创建用户
            var dest = GetDestinationID(node);//目的地            
            t.AreaID = dest == null ? Guid.Empty : dest.AreaID;
            t.AreaPath = dest == null ? "" : dest.AreaPath;
            t.AreaPathID = dest == null ? "" : dest.AreaPathId;
            var totalNum = node.GetNodeValue("ManCount").ToInt();//总人数
            t.ChildNum = node.GetNodeValue("ChildManCount").ToInt();
            t.AdultNum = totalNum - t.ChildNum;
            t.AdultAmt = 0;
            t.ChildAmt = 0;
            t.AdjustAmt = AdjustAmt(node);
            t.Receivable = node.GetNodeValue("AmountSales").ToDecimal();
            t.TourDate = (DateTime)node.GetNodeValue("DepartureDate").ToDate();
            t.TourDays = node.GetNodeValue("JourneyDays").ToInt();
            t.Comment = node.GetNodeValue("BuyerRemark");
            t.CreateDate = DateTime.Now;
            t.Creator = user == null ? node.GetNodeValue("CreateAccount") : user.Name;
            t.CreatorID = user == null ? (Guid)createGuid.ToGuid() : Guid.Parse(user.ID);
            t.DeptId = user == null ? Guid.Empty : Guid.Parse(user.DeptID);
            t.Modifier = t.Creator;
            t.ModifyDate = DateTime.Now;
            t.DataStatus = 1;//默认为已提交
            #region 自主班订单私有属性
            t.Schedule = "";
            t.Standard = "";
            t.Venue = "";
            t.CollectionTime = "";
            t.JAmount = 0;
            t.SAmount = 0;
            t.Period = 0;
            #endregion
            t.IsSync = true;
            t.SyncDate = DateTime.Now;
            t.OTAOrderStatus = OrderStatus(node.GetNodeValue("OrderStatus").ToInt());
            t.OTAName = otaName;
            t.OrderCost = node.GetNodeValue("AmountSettlement").ToDecimal();
            t.CustomerList = OrderCustomer(xml, t.ID);
            t.OrderType = 1;//默认同行散客 
            t.OrderCategory = Guid.Parse("c1e6fcfa-1eee-429f-beb7-f99055fa6a8d");//订单来源：默认为“其他” 
            t.RouteType = Guid.Empty;//线路类型：取目的地最顶级数据
            if (dest != null && !string.IsNullOrEmpty(dest.AreaPathId))
            {
                var _id = dest.AreaPathId.Split(',').Last();
                if (!string.IsNullOrEmpty(_id)) t.RouteType = Guid.Parse(_id);
            }

            return t;
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private string OrderStatus(int state)
        {
            var s = "";
            switch (state)
            {
                case 1: s = "待审核"; break;
                case 2: s = "报名确认"; break;
                case 4: s = "出票确认"; break;
                case 5: s = "回团确认"; break;
                case 8: s = "已退团"; break;
                case 9: s = "已取消"; break;
            }
            return s;
        }

        /// <summary>
        /// 调整金额
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private decimal AdjustAmt(XmlNode node)
        {
            var nodes = node.SelectSingleNode("Adjustments/OrderAdjustment");
            if (nodes == null)
            {
                return 0;
            }
            decimal amt = 0;
            foreach (XmlNode n in nodes)
                amt += n.GetNodeValue("Amount").ToDecimal();

            return amt;
        }

        /// <summary>
        /// 将OTA中的创建用户转化为本地系统的用户信息
        /// </summary>
        /// <param name="OTAuserId"></param>
        /// <returns></returns>
        private DAL.Sys_UserInfo GetCreateUserInfo(string OTAuserId)
        {
            var e = DAL.OTA_UserInfo.SingleOrDefault(x => x.OTAUID == OTAuserId);
            if (e == null) return null;
            return DAL.Sys_UserInfo.SingleOrDefault(x => x.ID == e.UID.ToString());
        }

        /// <summary>
        /// 游客列表
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private List<BTBOrderCustomer> OrderCustomer(XmlDocument doc, Guid orderId)
        {
            var list = new List<BTBOrderCustomer>();
            var nodes = doc.SelectNodes("OrderDetail/PassengerInfo/Passengers/Passenger");
            var idx = 0;
            foreach (XmlNode node in nodes)
            {
                var e = new BTBOrderCustomer();
                e.ID = Guid.NewGuid();
                e.OrderID = orderId;
                e.CustomerID = Guid.Parse(node.GetNodeValue("Guid"));
                e.cName = node.GetNodeValue("Name");
                e.cSex = node.GetNodeValue("Sex");
                e.cPhone = node.GetNodeValue("Phone");
                e.IDCard = node.GetNodeValue("IDNumber");
                e.pSort = idx++;
                e.Comment = node.GetNodeValue("Remark");

                list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// 递归查询本地目的地
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Destination GetDestinationID(XmlNode node)
        {
            var n = node.SelectSingleNode("Destinations/Destination/SubDestinationGuid");
            if (n == null) return null;
            var e = DAL.OTA_AreaSetting.SingleOrDefault(x => x.OTAareaID == n.InnerText);
            if (e == null) return null;
            var dest = DAL.Om_Area.SingleOrDefault(x => x.ID == e.AreaID.ToString());
            if (dest == null) return null;
            var entity = new Destination();
            entity.AreaID = Guid.Parse(dest.ID);
            var _id = dest.ParentID;
            var _pColl = new List<string>();
            var _idColl = new List<string>();
            _pColl.Add(dest.AreaName);
            _idColl.Add(dest.ID.ToString());
            while (true)
            {
                var _t = DAL.Om_Area.SingleOrDefault(x => x.ID == _id);
                if (_t == null)
                    break;
                else
                {
                    if (!string.IsNullOrEmpty(_id) && _id != null)
                    {
                        _pColl.Add(_t.AreaName);
                        _idColl.Add(_id.ToString());
                    }
                    _id = _t.ParentID;
                }
            }
            var _arrId = _idColl.ToArray();
            Array.Reverse(_arrId);
            var _arrName = _pColl.ToArray();
            Array.Reverse(_arrName);
            entity.AreaPath = string.Join(",", _arrName);
            entity.AreaPathId = string.Join(",", _arrId);
            return entity;
        }

        /// <summary>
        /// 全路径
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private string FindFullPath(Guid parentID)
        {
            var list = new List<string>();
            list.Add(parentID.ToString());
            while (true)
            {
                var e = DAL.Om_Area.SingleOrDefault(x => x.ID == parentID.ToString());
                if (e == null)
                    break;
                else
                {
                    parentID = Guid.Parse(e.ParentID);
                    if (parentID != Guid.Empty && parentID != null)
                        list.Add(parentID.ToString());
                }
            }
            return string.Join(",", list);
        }

    }

    public class Destination
    {
        public Guid AreaID { get; set; }

        public string AreaPath { get; set; }

        public string AreaPathId { get; set; }
    }
}
