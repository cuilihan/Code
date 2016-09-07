using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.CrmMrg;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Crm.Service
{
    /// <summary>
    /// 客户销售线索
    /// </summary>
    public class CustomerVisit : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
             var action=context.Request["action"].ToInt();
             switch (action)
             {
                 case 1:
                     CustomerTraceQuery(context);
                     break;
             }
        }

        /// <summary>
        /// 客户销售线索查询
        /// </summary>
        /// <param name="context"></param>
        private void CustomerTraceQuery(HttpContext context)
        { 
            var customerID=context.Request["customerID"];

            var qry = new QueryCriteriaBase();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.Keyword = customerID;
            
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new CustomerTrace_BF().QueryData(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

    }
}