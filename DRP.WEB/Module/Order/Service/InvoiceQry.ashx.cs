using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Order.Service
{
    /// <summary>
    /// 订单发票查询
    /// </summary>
    public class InvoiceQry : HandlerBase
    { 
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://发票查询 
                    InvoiceQuery(context);
                    break;
            }
        }


        private void InvoiceQuery(HttpContext context)
        {
            var qry = new InvoiceCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.InvoiceName = context.Request["InvoiceName"];
            qry.InvoiceNo = context.Request["InvoiceNo"];
            qry.Status = (InvoiceStatus)context.Request["Status"].ToInt(); 
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.QueryDateScope = dateScope; 
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new OrderInvoice_BF().QueryInvoice(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }


        protected override string NavigateID
        {
            get
            {
                return "salesinvoiceqry";
            }
        }
    }
}