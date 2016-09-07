using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.PageBase;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// GloPushNotice 的摘要说明
    /// </summary>
    public class GloPushNotice : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://查询数据
                    QueryData(context);
                    break;
                case 2://删除消息
                    Delete(context);
                    break;
            }
        }

        /// <summary>
        /// 查询数据
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
            var list = new PushNotice_BF().GetData(qry, out total);
            var json = ConvertJson.ListToJson(list);
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
                new PushNotice_BF().Delete(Guid.Parse(keyID));
                context.Response.Write("1");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "ompushnotice";
            }
        }
    }
}