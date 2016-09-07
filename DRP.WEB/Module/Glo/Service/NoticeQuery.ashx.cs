using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.GloMrg;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.WEB.Module.Glo.Service
{
    /// <summary>
    ///通知公告查询
    /// </summary>
    public class NoticeQuery : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            QueryData(context);
        }

        /// <summary>
        /// 查询资源
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.Keyword = context.Request["key"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Notice_BF().GetNotice(qry, out total);
            var json = ConvertJson.ToJson(dt);
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