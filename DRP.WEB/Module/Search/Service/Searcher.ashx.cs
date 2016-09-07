using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.BF.ResMrg;
using DRP.BF.Search;
using DRP.BF;
using DRP.BF.Order;

namespace DRP.WEB.Module.Search.Service
{
    /// <summary>
    /// Searcher 的摘要说明
    /// </summary>
    public class Searcher : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request["action"].ToInt())
            {
                case 1://资源查询
                    QueryResource(context);
                    break;
                case 2://订单查询
                    QueryOrder(context);
                    break;
                case 3://客户查询
                    QueryCustomer(context);
                    break;
            }
        }

        /// <summary>
        /// 资源查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryResource(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            var key = context.Request["Key"];
            if (!string.IsNullOrEmpty(key))
                key = HttpUtility.HtmlDecode(key);
            qry.Keyword = key;
            var itemType = (ResourceType)context.Request["itemType"].ToInt();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt(); 
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Search_BF().SearchResource(qry,itemType, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s); 
        }

        /// <summary>
        /// 客户查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryCustomer(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            var key = context.Request["Key"];
            var itemType = context.Request["id"];
            if (!string.IsNullOrEmpty(key))
                key = HttpUtility.HtmlDecode(key);
            qry.Keyword = key;
            if (!string.IsNullOrEmpty(itemType))
                itemType = HttpUtility.HtmlDecode(itemType);

            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new Search_BF().SearchCustomer(qry, itemType, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrder(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            var key = context.Request["Key"];
            var orderType = context.Request["itemType"].ToInt();
            if (!string.IsNullOrEmpty(key))
                key = HttpUtility.HtmlDecode(key);
            var tourDate = context.Request["tourDate"];
            qry.Keyword = key; 
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new Search_BF().QueryOrder(qry,tourDate,(OrderType)orderType, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}