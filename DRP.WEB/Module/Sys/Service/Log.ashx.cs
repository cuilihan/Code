using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Sys.Service
{
    /// <summary>
    /// 日志查询
    /// </summary>
    public class Log : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询日志
                    QueryLog(context);
                    break;
                case 2://删除日志
                    DeleteLog(context);
                    break; 
            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        private void QueryLog(HttpContext context)
        {
            var qry = new LogCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.sDate = context.Request["sDate"];
            qry.sDate = context.Request["eDate"];
            qry.LogerLv=context.Request["lv"];
            qry.Keyword=context.Request["key"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "LogDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new Log_BF().GetSysLog(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 日志删除
        /// </summary>
        /// <param name="context"></param>
        private void DeleteLog(HttpContext context)
        { 
            var ids=context.Request["ids"];
            if (string.IsNullOrEmpty(ids))
                context.Response.Write("");
            else
            {
                var list = new List<string>();
                foreach (var s in ids.Split(','))
                {
                    list.Add(string.Format("'{0}'",s));
                }
                var isOk = new Log_BF().DeleteLog(list);
                context.Response.Write(isOk ? "0" : "1");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "syslog";
            }
        }
    }
}