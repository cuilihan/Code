using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Fin;
using DRP.BF.PageBase;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 供应商付款
    /// </summary>
    public class Payable : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://供应商应付款汇总
                    QueryPayableStatisticData(context);
                    break;
                case 2://查询供应商的待付款订单
                    QueryPayableItemOrder(context);
                    break;
                case 3://保存付款
                    SavePayable(context);
                    break;
            }
        }


        /// <summary>
        /// 供应商付款总额
        /// </summary>
        /// <param name="context"></param>
        private void QueryPayableStatisticData(HttpContext context)
        {
            var qry = new PayableCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.SupplierName = context.Request["Name"];
            qry.PayableAmt = context.Request["PayAmt"].ToDecimal();
            qry.SupplierType = (ResourceType)context.Request["xType"].ToInt();

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "Spell" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Asc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new OrderPayable_BF().QueryPayableStatisticData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 查询供应商待付款的订单
        /// </summary>
        /// <param name="context"></param>
        private void QueryPayableItemOrder(HttpContext context)
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
            qry.UserName = context.Request["UserName"];
            qry.OrderNo = context.Request["OrderNo"];
            qry.DeptID = context.Request["DeptID"];

            var total = 0;
            var dt = new OrderPayable_BF().QueryPayableItemData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 保存付款信息
        /// </summary>
        /// <param name="context"></param>
        private void SavePayable(HttpContext context)
        {
            var supplierID = context.Request["SupplierID"];
            var supplierName = context.Request["SupplierName"];
            var xmlData = context.Request["XmlData"];
            var isOk = new OrderPayable_BF().SavePayable(supplierID, supplierName, xmlData);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "finpayable";
            }
        }
    }
}