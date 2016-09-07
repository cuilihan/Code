using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.Crm;
using DRP.Framework.Core;
using System.Text;
using DRP.DAL;
using DRP.BF;

namespace DRP.WeChat.Module.Crm.Service
{
    /// <summary>
    /// CustomerHandler 的摘要说明
    /// </summary>
    public class CustomerHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1:
                    QueryCustomerInfo(context);
                    break;
            }
        }


        /// <summary>
        /// 查询客户
        /// </summary>
        private void QueryCustomerInfo(HttpContext context)
        {
            var qry = new CustomCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new Customer_BF().QueryData(qry, out total);
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