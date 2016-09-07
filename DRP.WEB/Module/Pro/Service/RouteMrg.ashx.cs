using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.BF.ProMrg;
using DRP.DAL;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Pro.Service
{
    /// <summary>
    /// 线路管理
    /// </summary>
    public class RouteMrg : HandlerBase
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
                case 4://目的地选择
                    Destination(context);
                    break;
            }
        }


        /// <summary>
        /// 查询线路列表
        /// </summary>
        private void QueryRouteInfo(HttpContext context)
        {
            var qry = new RouteCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.RouteType = context.Request["routeTypeID"];
            qry.RouteName = context.Request["RouteName"];
            qry.RouteNo = context.Request["RouteNo"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new RouteInfo_BF().QueryData(qry, out total);
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
            var isOk = new RouteInfo_BF().Delete(routeID);
            context.Response.Write(isOk ? "1" : "0");
        }

        #region << 保存线路 >>
        /// <summary>
        /// 保存线路
        /// </summary>
        /// <param name="context"></param>
        private void SaveRouteInfo(HttpContext context)
        {
            DAL.Pro_RouteInfo entity = new DAL.Pro_RouteInfo();
            entity.ID = context.Request["ID"];
            entity.ID = string.IsNullOrEmpty(entity.ID) ? Guid.NewGuid().ToString() : entity.ID;
            entity.RouteName = context.Request["RouteName"];
            entity.RouteNo = context.Request["RouteNo"];
            entity.ScheduleDays = context.Request["Days"].ToInt();
            entity.RouteType = context.Request["RouteType"];
            entity.RouteTypeID = context.Request["RouteTypeID"];
            entity.DestinationID = context.Request["DestinationID"];
            entity.Destination = new Destination_BF().Get(entity.DestinationID).Name;
            entity.DestinationPath = new Destination_BF().GetDestinationPathID(entity.DestinationID);
            entity.Feature = context.Request["Feature"];
            entity.PriceInclude = context.Request["PriceInclude"];
            entity.PriceNonIncude = context.Request["PriceNoneInclude"];
            entity.SelfItem = context.Request["SelfItem"];
            entity.Remind = context.Request["Remind"];
            entity.Shopping = context.Request["Shopping"];
            entity.Comment = context.Request["Comment"];
            entity.RouteSource = context.Request["RouteSource"];
            entity.RouteSourceID = context.Request["RouteSourceID"];

            var xmlData = context.Request["Schedule"];
            var list = ToRouteScheduleList(xmlData);
            var isOk = new RouteInfo_BF().Save(entity, list);
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
                e.Traffic = node.GetNodeValue("traffic");
                e.ID = Guid.NewGuid().ToString();
                if (string.IsNullOrEmpty(e.Title) && string.IsNullOrEmpty(e.Schedule))
                    continue;
                list.Add(e);
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 目的地
        /// </summary>
        /// <param name="context"></param>
        private void Destination(HttpContext context)
        {
            var routeTypeID = context.Request["routeTypeID"];
            var json = new Destination_BF().GetDestinationComboTree(routeTypeID);
            context.Response.Write(json);
        }

        protected override string NavigateID
        {
            get
            {
                return "routeinfo";
            }
        }
    }
}