using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.CheckIn;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;


namespace DRP.WEB.Module.CheckIn.Service
{
    /// <summary>
    /// 非订单支出登记
    /// </summary>
    public class PaySign : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询
                    QueryData(context);
                    break;
                case 2://删除
                    Delete(context);
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var qry = new PayCheckInCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.Operator = context.Request["Opeator"];
            var dateScope = new DateScope();
            dateScope.sDate = context.Request["sDate"];
            dateScope.eDate = context.Request["eDate"];
            qry.PayDate = dateScope;
            qry.PayTypeID = context.Request["TypeID"];

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new PayCheckIn_BF().QueryData(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                var list = new List<string>();
                foreach (var s in keyID.Split(','))
                    list.Add(string.Format("'{0}'", s));
                var isOk = new PayCheckIn_BF().Delete(list);
                context.Response.Write(isOk ? "1" : "0");
            }
        }


        protected override string NavigateID
        {
            get
            {
                return "finpayablesign";
            }
        }
        
    }
}