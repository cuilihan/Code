using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.BF.ResMrg
{
    /// <summary>
    /// 供应商资源类型
    /// </summary>
    public enum ResourceType
    {
        All = 0,
        /// <summary>
        /// 地接社
        /// </summary>
        TravelAgency = 1,
        /// <summary>
        /// 景点门票
        /// </summary>
        ScenicTicket = 2,
        /// <summary>
        /// 导游
        /// </summary>
        Guide = 3,
        /// <summary>
        /// 酒店
        /// </summary>
        Hotel = 4,
        /// <summary>
        /// 车队 
        /// </summary>
        Motorcade = 5,
        /// <summary>
        /// 签证机构
        /// </summary>
        VisaAgency = 6,
        /// <summary>
        /// 保险公司
        /// </summary>
        Insurer = 7,
        /// <summary>
        /// 购物店
        /// </summary>
        Shopping = 8,
        /// <summary>
        /// 票务机构
        /// </summary>
        TicketAgency = 9,
        /// <summary>
        /// 其他供应商资源
        /// </summary>
        OtherRes = 10
    }

    /// <summary>
    /// 资源查询条件
    /// </summary>
    public class ResourceCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 线路类型
        /// </summary>
        public string RouteTypeID { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string DestinationID { get; set; }

        /// <summary>
        /// 出发地
        /// </summary>
        public string DepartureID { get; set; }

        /// <summary>
        /// 售票类型（票务机构查询专用）
        /// </summary>
        public string TicketType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool? DataStatus { get; set; }
    }

    /// <summary>
    /// 资源管理通用类
    /// </summary>
    public class ResourceUtility
    {
        #region 资源业务联系人

        /// <summary>
        /// 资源业务联系人
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public List<DAL.Res_BizContact> GetBizContact(string resourceID)
        {
            return DAL.Res_BizContact.Find(x => x.FKID == resourceID).ToList();
        }

        /// <summary>
        /// 删除业务联系人
        /// </summary>
        /// <param name="resourceID"></param>
        public void DeleteBizContact(string resourceID)
        {
            DAL.Res_BizContact.Delete(x => x.FKID == resourceID);
        }

        /// <summary>
        /// 将XML字符串转化为业务联系人列表
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public List<DAL.Res_BizContact> ToBizContactList(string xmlData, string resourceID)
        {
            var list = new List<DAL.Res_BizContact>();
            if (string.IsNullOrEmpty(xmlData))
                return list;

            var user = AuthenticationPage.UserInfo;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var e = new Res_BizContact();
                e.ID = Guid.NewGuid().ToString();
                e.FKID = resourceID;
                e.Name = node.GetNodeValue("Name");
                e.Phone = node.GetNodeValue("Phone");
                e.Fax = node.GetNodeValue("Fax");
                e.Remark = node.GetNodeValue("Comment");
                e.CreateUserID = user.UserID;
                e.CreateUserName = user.UserName;
                e.OrgID = user.OrgID;
                e.CreateDate = DateTime.Now;
                list.Add(e);
            }

            return list;
        }

        #endregion

        #region << 根据资源类型查询供应商：应用于订单管理功能中 >>

        /// <summary>
        /// 获取供应商资料表名
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private string ConvertResourceTableName(int itemType)
        {
            var tblName = "";
            #region tableName
            switch ((ResourceType)itemType)
            {
                case ResourceType.TravelAgency:
                    tblName = "Res_TravelAgency";
                    break;
                case ResourceType.ScenicTicket:
                    tblName = "Res_ScenicTicket";
                    break;
                case ResourceType.Guide:
                    tblName = "Res_Guide";
                    break;
                case ResourceType.Hotel:
                    tblName = "Res_Hotel";
                    break;
                case ResourceType.Motorcade:
                    tblName = "Res_Motorcade";
                    break;
                case ResourceType.VisaAgency:
                    tblName = "Res_Visa";
                    break;
                case ResourceType.Insurer:
                    tblName = "Res_Insurance";
                    break;
                case ResourceType.Shopping:
                    tblName = "Res_Shopping";
                    break;
                case ResourceType.TicketAgency:
                    tblName = "Res_TicketAgency";
                    break;
                case ResourceType.OtherRes:
                    tblName = "Res_Other";
                    break;
            }
            #endregion
            return tblName;
        }

        /// <summary>
        /// 根据资源类型查询供应商
        /// </summary>
        /// <param name="context"></param>
        public DataTable QueryResource(int itemType)
        {
            DataTable dt = new DataTable();
            var tblName = ConvertResourceTableName(itemType);
            if (string.IsNullOrEmpty(tblName))
            {
                return dt;
            }
            else
            {
                return new ResourceDAL().QueryResource(tblName, AuthenticationPage.UserInfo.OrgID);
            }
        }

        /// <summary>
        /// 更新供应商的合作信息
        /// </summary>
        /// <param name="xType"></param>
        /// <param name="supplierID"></param>
        /// <param name="costAmt"></param>
        /// <param name="user"></param>
        internal void UpdateTraceInfo(ResourceType xType, string supplierID)
        {
            var tblName = ConvertResourceTableName((int)xType);
            if (!string.IsNullOrEmpty(tblName))
            {
                new ResourceDAL().UpdateResourceTradeInfo(tblName, supplierID);
            }
        }
        #endregion

        #region << 订单查询 >>
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable TradeOrder(QueryCriteriaBase qry, string sDate, string eDate, string resourceID, out int record)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}' and SupplierID='{1}'", user.OrgID, resourceID);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.Keyword);
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and TourDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            return new DRPDB().GetPagination("V_Res_TradeOrder", qry.pageIndex, qry.pageSize, out record, sb.ToString(), qry.SortExpress);
        }

        #endregion
    }
}
