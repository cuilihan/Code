using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.PageBase;
using DRP.Framework.Core;
using DRP.BF.GloMrg;
using DRP.BF;

namespace DRP.WEB.Module.Glo.Service
{
    /// <summary>
    /// 通知公告管理
    /// </summary>
    public class NoticeMrg : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1: //查询
                    QueryData(context);
                    break;
                case 2://删除
                    Delete(context);
                    break;
            }
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                var listID = new List<string>();
                foreach (var id in keyID.Split(','))
                    listID.Add(string.Format("'{0}'", id));
                var isOk = new Notice_BF().Delete(listID);
                context.Response.Write(isOk ? "1" : "0");
            }
        }


        protected override string NavigateID
        {
            get
            {
                return "glonotice";
            }
        }
    }
}