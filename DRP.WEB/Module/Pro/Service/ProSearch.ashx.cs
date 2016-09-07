using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.DAL;
using DRP.BF.Glo;
using System.Text;
using DRP.BF.ProMrg;
using System.Data;
using DRP.BF;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Pro.Service
{
    /// <summary>
    /// 自主班产品搜索（预订）
    /// </summary>
    public class ProSearch : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://产品目的地查询
                    QueryProductDestination(context);
                    break;
                case 2://团次列表查询
                    SearchTour(context);
                    break;
                case 3://查看价格详情
                    QueryTourPricePolicy(context);
                    break;
            }
        }


        #region << 自主班产品目的地查询（预订导航） >>

        /// <summary>
        /// 自主班产品目的地查询（预订导航）
        /// </summary>
        /// <param name="context"></param>
        private void QueryProductDestination(HttpContext context)
        {
            var routeTypeID = context.Request["routeTypeID"];
            var destinationList = new Destination_BF().GetDestinationList().FindAll(x => x.RouteTypeID == routeTypeID);//目的地
            var productList = new Product_BF().GetDestinationTourNumber();

            var coll = destinationList.FindAll(x => x.ParentID == Guid.Empty.ToString());
            var sb = new StringBuilder();
            coll.ForEach(x =>
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td class='rowlabel'><span class='icon_destination'>{0}</span></td>", FormatLink(productList, x.ID, x.Name, routeTypeID));
                sb.Append("<td>");
                var data = destinationList.FindAll(t => t.ParentID == x.ID);
                data.ForEach(a =>
                {
                    sb.AppendFormat(FormatLink(productList, a.ID, a.Name, routeTypeID));
                });
                sb.Append("</td>");
                sb.Append("</tr>");
            });
            var str = sb.ToString();
            if (string.IsNullOrEmpty(str))
                str = "<tr><td><div class='icon_sad'>无目的地信息</div></td></tr>";
            context.Response.Write(str);
        }

        /// <summary>
        /// 目的地是具有的团次数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="destinationID"></param>
        private string FormatLink(DataTable dt, string destinationID, string destinationName, string routeTypeID)
        {
            var iCount = 0;
            var rows = dt.Select(string.Format("DestinationPath like '%{0}%'", destinationID));
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                    iCount += rows[i]["TourNum"].ToString().ToInt();
                return string.Format("<a href='ProList.aspx?rid={3}&did={0}'>{1}({2})</a>", destinationID, destinationName, iCount, routeTypeID);
            }
            else
                return "<a style='color:#ccc;'>" + destinationName + "(0)</a>";
        }

        #endregion

        #region << 团次列表查询 >>

        /// <summary>
        /// 团次列表查询
        /// </summary> 
        private void SearchTour(HttpContext context)
        {
            var qry = new TourCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.RouteTypeID = context.Request["rid"];
            qry.DestinationID = context.Request["did"];
            qry.TourName = context.Request["key"];
            qry.EffectiveDays = !(context.Request["EffectiveDays"] == "true");
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.TourDateScope = dateScope;
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Product_BF().QueryTour(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }
        #endregion

        #region << 团次的价格策略 >>
        private void QueryTourPricePolicy(HttpContext context)
        {
            var tourID = context.Request["TourID"];
            var list = new TourInfo_BF().GetTourPrice(tourID);
            var sb = new StringBuilder();
            foreach (var e in list)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", e.Name);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", e.SalePrice);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", e.Rebate);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", e.RoomRate);
                sb.Append("</tr>");
            }
            context.Response.Write(sb.ToString());
        }
        #endregion

        protected override string NavigateID
        {
            get
            {
                return "productbook";
            }
        }
    }
}