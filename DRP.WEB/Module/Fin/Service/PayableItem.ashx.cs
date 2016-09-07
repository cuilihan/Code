using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Fin;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 供应商应付款明细查询
    /// </summary>
    public class PayableItem : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://供应商应付款明细查询
                    QueryPayableItem(context);
                    break;
            }
        }

        /// <summary>
        /// 查询供应商所有应付款的明细
        /// </summary>
        /// <param name="context"></param>
        private void QueryPayableItem(HttpContext context)
        {
            var qry = new PayableItemCriterial();
            qry.SupplierID = context.Request["id"];
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderName = context.Request["Name"];
            qry.sDate = context.Request["sDate"];
            qry.eDate = context.Request["eDate"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Asc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new OrderPayable_BF().QuerySupplierPayableItem(qry, out total);
            var json = ConvertJson.ToJson(dt);
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