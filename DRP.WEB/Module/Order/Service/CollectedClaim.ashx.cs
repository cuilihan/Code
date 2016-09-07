using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Fin;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Order.Service
{
    /// <summary>
    /// 收款认领
    /// </summary>
    public class CollectedClaim : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询
                    QueryData(context);
                    break;
                case 2://收款认领
                    break;
            }
        }


        /// <summary>
        /// 收款认领查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryData(HttpContext context)
        {
            var qry = new CollectionItemCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.BankName = context.Request["Bank"];
            qry.sDate = context.Request["sDate"];
            qry.eDate = context.Request["eDate"];
            qry.MaxIncome = context.Request["MaxAmt"].ToDecimal();
            qry.MinIncome = context.Request["MinAmt"].ToDecimal();
            qry.FromBank = context.Request["FromBank"];
            qry.FromAcct = context.Request["FromAcct"];
            var status = context.Request["Status"].ToInt();
            if (status == 0)
                qry.DataStatus = CollectedStatus.NoneClaim;
            else
                qry.DataStatus = (CollectedStatus)status;

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TradeDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new CollectedItem_BF().QueryData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }


        protected override string NavigateID
        {
            get
            {
                return "ordcollectedclaim";
            }
        }
    }
}