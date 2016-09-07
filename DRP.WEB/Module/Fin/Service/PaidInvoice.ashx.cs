using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 付款发票管理
    /// </summary>
    public class PaidInvoice : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://查询
                    QueryData(context);
                    break;
                case 2://删除
                    Delete(context);
                    break;
            }
        }

        private void QueryData(HttpContext context)
        {
            var orderID = context.Request["orderID"];
            var list = new SupplierInvoice_BF().GetInovice(orderID);
            var s = ConvertJson.ListToJson(list);
            context.Response.Write(s);
        }

        private void Delete(HttpContext context)
        {
            var ids = context.Request["id"];
            if (string.IsNullOrEmpty(ids))
            {
                context.Response.Write("0");
                return;
            }
            var isOk = new SupplierInvoice_BF().Delete(ids.Split(','));
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