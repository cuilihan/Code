using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// OmUser 的摘要说明
    /// </summary>
    public class OmUser : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询用户
                    QueryUserInfo(context);
                    break;
                case 2://删除用户
                    Delete(context);
                    break;
            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        private void QueryUserInfo(HttpContext context)
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
            var list = new OmUser_BF().GetOmUser(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s); 
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                new OmUser_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "omuser";
            }
        }
    }
}