using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using DRP.Framework.Core;
using DRP.Framework;
using DRP.BF.Glo;
using DRP.BF.Cache;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 参数实体
    /// </summary>
    internal class ParameterEntity
    {
        /// <summary>
        /// 参数类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数项目列表
        /// </summary>
        public List<string> Items { get; set; }
    }

    /// <summary>
    /// 线路类型
    /// </summary>
    internal enum RouteType
    {
        ShortRoute, LongRoute, Foreign
    }

    /// <summary>
    /// 目的地实体
    /// </summary>
    internal class DestinationEtity
    {
        public string Name { get; set; }

        public List<string> Children { get; set; }
    }

    /// <summary>
    /// 机构数据初始化
    /// </summary>
    public class OrgInit_BF
    {
        /// <summary>
        /// 参数实体
        /// </summary>
        /// <returns></returns>
        private List<ParameterEntity> GetParameterList(string folder)
        {
            var list = new List<ParameterEntity>();
            var resFile = folder + "InitData.xml";
            var doc = new XmlDocument();
            doc.Load(resFile);
            var nodes = doc.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var coll = new List<string>();
                var e = new ParameterEntity();
                e.Name = node.Attributes["name"].InnerText;
                foreach (XmlNode n in node.ChildNodes)
                    coll.Add(n.InnerText);
                e.Items = coll;
                list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// 初始化机构数据，清空原数据且不可恢复
        /// </summary>
        /// <param name="orgID">机构ID</param>
        /// <returns></returns>
        public bool InitData(string orgID)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var folder = ConfigHelper.GetAppSettingValue("InitDataPath");
                if (string.IsNullOrEmpty(folder))
                {
                    BizUtility.ExceptionHandler(user, new Exception("未设置初始化文件目录"));
                    return false;
                }
                var list = GetParameterList(folder);

                #region 保存配置文件中的初始数据
                list.ForEach(x =>
                {
                    switch (x.Name)
                    {
                        case "RouteType"://线路类型
                            SaveBasicInfoData(BasicType.Pro_RouteType, orgID, x.Items, user);
                            break;
                        case "Destination"://目的地
                            SaveDestination(orgID, folder + "Destination.xml");
                            break;
                        case "CollectedType"://收款方式
                            SaveBasicInfoData(BasicType.Fin_CollectedType, orgID, x.Items, user);
                            break;
                        case "OrderSource"://订单来源
                            SaveBasicInfoData(BasicType.Ord_OrderSource, orgID, x.Items, user);
                            break;
                        case "InvoiceItem"://开票项目
                            SaveBasicInfoData(BasicType.Fin_InvoiceItem, orgID, x.Items, user);
                            break;
                        case "InvoiceFetchType"://发票领取方式
                            SaveBasicInfoData(BasicType.Fin_InvoiceFetchType, orgID, x.Items, user);
                            break;
                        case "DrawMoneyType"://导游领款方式
                            SaveBasicInfoData(BasicType.Ord_DrawMoneyMethod, orgID, x.Items, user);
                            break;
                        case "NonOrderIncomeType"://非订单收入类型
                            SaveBasicInfoData(BasicType.CheckIn_IncomeSign, orgID, x.Items, user);
                            break;
                        case "NonOrderPayType"://非订单支出类型
                            SaveBasicInfoData(BasicType.CheckIn_PayableSign, orgID, x.Items, user);
                            break;
                        case "BizType"://单项业务类型
                            SaveBasicInfoData(BasicType.Ord_SingleBizType, orgID, x.Items, user);
                            break;
                        case "CustomerType"://客户类型
                            SaveBasicInfoData(BasicType.Crm_CustomerType, orgID, x.Items, user);
                            break;
                        case "CustomerTraceType"://客户销售线索类型
                            SaveBasicInfoData(BasicType.Crm_SalesTraceType, orgID, x.Items, user);
                            break;
                        case "GuideLevel"://导游等级
                            SaveBasicInfoData(BasicType.Res_GuideGrade, orgID, x.Items, user);
                            break;
                        case "HotelStarLv"://酒店星级
                            SaveBasicInfoData(BasicType.Res_HotelStar, orgID, x.Items, user);
                            break;
                        case "MotorcadeScale"://车队规模
                            SaveBasicInfoData(BasicType.Res_MotorcadeScale, orgID, x.Items, user);
                            break;
                        case "CredentialType"://客户证件类型
                            SaveBasicInfoData(BasicType.Crm_CredentialType, orgID, x.Items, user);
                            break;
                    }
                });
                #endregion

                #region 保存出发地
                SaveDeparture(orgID);
                #endregion

                BizUtility.WriteLog(user, "初始化系统参数完成");
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "初始化系统参数时发生错误");
                return false;
            }
        }

        #region 保存初始化数据
        /// <summary>
        /// 通用参数BasicInfo表
        /// </summary>
        /// <param name="xType"></param>
        /// <param name="orgID"></param>
        /// <param name="items"></param>
        private void SaveBasicInfoData(BasicType xType, string orgID, List<string> items, UserInfo user)
        {
            DAL.Glo_BasicInfo.Delete(x => x.OrgID == orgID && x.BasicType == (int)xType);

            var idx = 1;
            items.ForEach(x =>
            {
                var e = new DAL.Glo_BasicInfo();
                e.ID = Guid.NewGuid().ToString();
                e.Name = x;
                e.BasicType = (int)xType;
                e.OrderIndex = idx++;
                e.CreateUserID = user.UserID;
                e.CreateUserName = user.UserName;
                e.CreateDate = DateTime.Now;
                e.OrgID = orgID;
                e.DeptID = user.DeptID;
                e.Save();
            });
        }

        /// <summary>
        /// 保存目的地
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="fileName"></param>
        private void SaveDestination(string orgID, string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);
            var user = AuthenticationPage.UserInfo;
           // var sList = ConvertDestinationEntity(doc, RouteType.ShortRoute);
            var cList = ConvertDestinationEntity(doc, RouteType.LongRoute);
            var fList = ConvertDestinationEntity(doc, RouteType.Foreign);
            DAL.Glo_Destination.Delete(x => x.OrgID == orgID);
           // SaveDestination(sList, RouteType.ShortRoute, orgID, user);
            SaveDestination(cList, RouteType.LongRoute, orgID, user);
            SaveDestination(fList, RouteType.Foreign, orgID, user);

            //加新加载缓存
            var key = "Glo_Destination_Key_" + orgID;
            BizCacheHelper.GloDestinaionCache.Remove(key);
        }

        /// <summary>
        /// 保存目的地
        /// </summary>
        /// <param name="list"></param>
        /// <param name="xType"></param>
        /// <param name="orgID"></param>
        /// <param name="user"></param>
        private void SaveDestination(List<DestinationEtity> list, RouteType xType, string orgID, UserInfo user)
        {
            var rName = "短线";
            switch (xType)
            {
                case RouteType.ShortRoute: rName = "短线"; break;
                case RouteType.LongRoute: rName = "长线"; break;
                case RouteType.Foreign: rName = "出境"; break;
            }
            var entity = DAL.Glo_BasicInfo.SingleOrDefault(x => x.Name.Contains(rName) && x.OrgID == orgID);
            if (entity == null) return;
            var idx = 0;
            list.ForEach(x =>
            {
                var e = new DAL.Glo_Destination();
                e.ID = Guid.NewGuid().ToString();
                e.Name = x.Name;
                e.RouteTypeID = entity.ID;
                e.ParentID = Guid.Empty.ToString();
                e.OrderIndex = idx++;
                e.OrgID = orgID;
                e.CreateDate = DateTime.Now;
                e.DeptID = user.DeptID;
                e.CreateUserID = user.UserID;
                e.CreateUserName = user.UserName;
                e.Save();
                //子目录
                x.Children.ForEach(t =>
                {
                    var d = new DAL.Glo_Destination();
                    d.ID = Guid.NewGuid().ToString();
                    d.Name = t;
                    d.RouteTypeID = entity.ID;
                    d.ParentID = e.ID;
                    d.OrderIndex = idx++;
                    d.OrgID = orgID;
                    d.CreateDate = DateTime.Now;
                    d.DeptID = user.DeptID;
                    d.CreateUserID = user.UserID;
                    d.CreateUserName = user.UserName;
                    d.Save();
                });
            });
        }

        /// <summary>
        /// 线路类型数据
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xType"></param>
        /// <returns></returns>
        private List<DestinationEtity> ConvertDestinationEntity(XmlDocument doc, RouteType xType)
        {
            var list = new List<DestinationEtity>();
            var path = "lv1";
            switch (xType)
            {
                case RouteType.ShortRoute: path = "lv1"; break;
                case RouteType.LongRoute: path = "lv2"; break;
                case RouteType.Foreign: path = "lv3"; break;
            }
            var xmlNode = doc.SelectSingleNode("document/" + path);
            if (xmlNode == null) return list;
            var nodes = xmlNode.SelectNodes("data");
            foreach (XmlNode node in nodes)
            {
                var e = new DestinationEtity();
                e.Name = node.Attributes["name"].InnerText;
                var items = new List<string>();
                foreach (XmlNode item in node.ChildNodes)
                {
                    items.Add(item.InnerText);
                }
                e.Children = items;
                list.Add(e);
            }
            return list;
        }
        #endregion

        #region 保存出发地
        private void SaveDeparture(string orgID)
        {
            var org = DAL.Om_OrgInfo.SingleOrDefault(x => x.ID == orgID);
            if (org != null)
            {
                DAL.Glo_Departure.Delete(x => x.OrgID == org.ID);
                var e = new DAL.Glo_Departure();
                e.ID = Guid.NewGuid().ToString();
                e.Name = org.AreaName;
                e.OrgID = orgID;
                e.DeptID = org.ID;
                e.OrderIndex = 1;
                e.CreateDate = DateTime.Now;
                e.CreateUserID = org.CreateUserID;
                e.CreateUserName = org.CreateUserName;
                e.Save();
            }
        }
        #endregion
    }
}
