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
    /// 供应商已付款明细
    /// </summary>
    public class PaidItem : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://供应商付款明细
                    SupplierPaidItem(context);
                    break;
                case 2://删除付款
                    SupplierDelete(context);
                    break;
            }
        }

        private void SupplierPaidItem(HttpContext context)
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
            sortName = string.IsNullOrEmpty(sortName) ? "PayDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Asc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new OrderPayable_BF().QueryPaidItemData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        private void SupplierDelete(HttpContext context)
        {
            var id = context.Request["id"];
            var isOk = new OrderPayable_BF().PaidDelete(id);
            context.Response.Write(isOk ? "1" : "0");
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