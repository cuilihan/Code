using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.BF.Quotation;
using DRP.DAL;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.T.Service
{
    /// <summary>
    /// 线路模板管理
    /// </summary>
    public class RouteTemplate : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询线路
                    QueryRouteInfo(context);
                    break;
                case 2://保存线路
                    SaveRouteInfo(context);
                    break;
                case 3://删除线路
                    DeleteRoute(context);
                    break;
                case 4://目的地zTree
                    Destination(context);
                    break;
                case 5://选择目的地Combotree
                    DestinationTree(context);
                    break;
            }
        }


        /// <summary>
        /// 查询线路列表
        /// </summary>
        private void QueryRouteInfo(HttpContext context)
        { 
            var qry = new QuotationCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.DestinationID = context.Request["DestinationID"];
            qry.RouteName = context.Request["RouteName"];
            qry.RouteNo = context.Request["RouteNo"];
            qry.Days = context.Request["Days"].ToInt();
            qry.ViewSpot = context.Request["ViewSpot"];
            var strAvgPrice = context.Request["AvgPrice"];
            if (!string.IsNullOrEmpty(strAvgPrice))
            {
                var arr = strAvgPrice.Split(',');
                if (arr.Length == 2)
                {
                    qry.MinAvgAmount = arr[0].ToInt();
                    qry.MaxAvgAmount = arr[1].ToInt();
                }
            }
            var strVisitNum = context.Request["VisitorNum"];
            if (!string.IsNullOrEmpty(strVisitNum))
            {
                var arr = strVisitNum.Split(',');
                if (arr.Length == 2)
                {
                    qry.MinVisitNum = arr[0].ToInt();
                    qry.MaxVisitNum = arr[1].ToInt();
                }
            }
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Quotation_BF().QueryData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 删除线路
        /// </summary>
        /// <param name="context"></param>
        private void DeleteRoute(HttpContext context)
        {
            var routeID = context.Request["id"];
            var isOk = new Quotation_BF().Delete(routeID);
            context.Response.Write(isOk ? "1" : "0");
        }

        #region << 保存线路 >>
        /// <summary>
        /// 保存线路
        /// </summary>
        /// <param name="context"></param>
        private void SaveRouteInfo(HttpContext context)
        {
            DAL.Pro_Quotation entity = new DAL.Pro_Quotation();
            entity.ID = context.Request["ID"];
            entity.ID = string.IsNullOrEmpty(entity.ID) ? Guid.NewGuid().ToString() : entity.ID;
            entity.RouteName = context.Request["RouteName"];
            entity.RouteNo = context.Request["RouteNo"];
            entity.Days = context.Request["Days"].ToInt();
            entity.RouteType = context.Request["RouteType"];
            entity.RouteTypeID = context.Request["RouteTypeID"];
            entity.DestinationID = context.Request["DestinationID"];
            entity.Destination = new Destination_BF().Get(entity.DestinationID).Name;
            entity.DestinationPath = new Destination_BF().GetDestinationPathID(entity.DestinationID);
            entity.VisitorNum = context.Request["VisitorNum"].ToInt();
            entity.Stay = context.Request["Stay"];
            entity.Dinner = context.Request["Dinner"];
            entity.ViewSpot = context.Request["ViewSpot"];
            entity.Feature = context.Request["Feature"];
            entity.SelfItem = context.Request["SelfItem"];
            entity.Notes = context.Request["Notes"];
            entity.Comment = context.Request["Comment"];
            entity.Cost = context.Request["Cost"].ToDecimal();
            entity.AvgPrice = context.Request["AvgPrice"].ToDecimal();
            entity.Profit = context.Request["Profit"].ToDecimal();
            entity.ChildPrice = context.Request["ChildPrice"].ToDecimal();
            entity.ChildCost = context.Request["ChildCost"].ToDecimal();
            entity.Remark = context.Request["Remark"];

            var xmlData = context.Request["Schedule"];
            var xmlItem = context.Request["Items"];
            var listSchedule = ToRouteScheduleList(xmlData);
            var listItem = ToQuotatinItem(xmlItem);
            var isOk = new Quotation_BF().Save(entity, listSchedule, listItem);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 线路行程由Xml格式转化为实体列表
        /// </summary>
        /// <param name="xmlData"></param>
        private List<DAL.Pro_RouteSchedule> ToRouteScheduleList(string xmlData)
        {
            var list = new List<DAL.Pro_RouteSchedule>();
            if (string.IsNullOrEmpty(xmlData))
                return list;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var e = new Pro_RouteSchedule();
                e.DayNum = node.GetNodeValue("daynum").ToInt();
                e.Title = node.GetNodeValue("title");
                e.Schedule = node.GetNodeValue("schedule").Trim();
                e.Stay = node.GetNodeValue("stay");
                e.Dinner = node.GetNodeValue("dinner");
                e.ID = Guid.NewGuid().ToString();
                if (string.IsNullOrEmpty(e.Title) && string.IsNullOrEmpty(e.Schedule))
                    continue;
                list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// 服务标准由Xml格式转化为实体列表
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        private List<DAL.Pro_QuotationCostItem> ToQuotatinItem(string xmlData)
        {
            var list = new List<DAL.Pro_QuotationCostItem>();
            if (string.IsNullOrEmpty(xmlData))
                return list;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            var idx = 0;
            foreach (XmlNode node in nodes)
            {
                var e = new Pro_QuotationCostItem();
                e.ID = Guid.NewGuid().ToString();
                e.ItemName = node.GetNodeValue("itemName");
                e.ItemRemark = node.GetNodeValue("itemRemark");
                e.ItemPrice = node.GetNodeValue("itemPrice").ToDecimal();
                e.ItemNum = node.GetNodeValue("itemNum").ToInt();
                e.ItemSum = e.ItemPrice * e.ItemNum;
                e.OrderIndex = idx++;
                if (string.IsNullOrEmpty(e.ItemName) && e.ItemSum == 0)
                    continue;
                list.Add(e);
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 线路类型及目的地(zTree)
        /// </summary>
        /// <param name="context"></param>
        private void Destination(HttpContext context)
        {
            var routeTypeList = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            var destinationList = new Destination_BF().GetDestinationList();
            var list = new List<string>();
            routeTypeList.ForEach(x =>
            {
                var s = string.Format("id:\"{0}\", pId: \"{1}\", name: \"{2}\"", x.ID, "0", x.Name);
                list.Add("{" + s + "}");
            });
            destinationList.ForEach(x =>
            {
                var s = string.Format("id:\"{0}\", pId: \"{1}\", name: \"{2}\"", x.ID, x.ParentID == Guid.Empty.ToString() ? x.RouteTypeID : x.ParentID, x.Name);
                list.Add("{" + s + "}");
            });
            var json = string.Join(",", list);
            context.Response.Write("[" + json + "]");
        }


        /// <summary>
        /// 目的地Combotree
        /// </summary>
        /// <param name="context"></param>
        private void DestinationTree(HttpContext context)
        {
            var routeTypeID = context.Request["routeTypeID"];
            var json = new Destination_BF().GetDestinationComboTree(routeTypeID);
            context.Response.Write(json);
        }

        protected override string NavigateID
        {
            get
            {
                return "routetemplate";
            }
        }
    }
}