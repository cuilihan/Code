using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.PageBase;
using DRP.BF.CheckAccount;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 导游报账结算
    /// </summary>
    public class BalanceSettlement : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询报账结算单
                    QueryData(context);
                    break;
            }
        }

        /// <summary>
        /// 查询报账结算单
        /// </summary>
        /// <param name="context"></param>
        private void QueryData(HttpContext context)
        {
            var qry = new OrderBalanceSettlementCriterial();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderNo = context.Request["OrderNo"];
            qry.OrderName = context.Request["OrderName"];
            qry.GuideMobile = context.Request["Mobile"];
            qry.GuideName = context.Request["Name"]; 
            qry.DataStatus = context.Request["Status"].ToInt();

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new OrderBalanceSettlement_BF().QueryData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        protected override string NavigateID
        {
            get
            {
                return "balancesettlement";
            }
        }
    }
}