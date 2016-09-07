using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.Fin;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 导游领款管理（财务）
    /// </summary>
    public class DrawMoney : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询
                    QueryData(context);
                    break;
                case 2://修改取款状态
                    SetStatus(context);
                    break;
            }
        }

        private void QueryData(HttpContext context)
        {
            var qry = new DrawMoneyCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.OrderName = context.Request["OrderName"];
            qry.OrderNo = context.Request["OrderNo"];
            var tourDate = new DateScope();
            tourDate.sDate = context.Request["sDate"];
            tourDate.eDate = context.Request["eDate"];
            qry.TourDate = tourDate;
            qry.DataStatus = context.Request["Status"].ToInt();

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new DrawMoney_BF().QueryData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 更新领款状态
        /// </summary>
        /// <param name="context"></param>
        private void SetStatus(HttpContext context)
        {
            var id = context.Request["ID"];
            var status = context.Request["Status"].ToInt();
            var isOk = new DrawMoney_BF().UpdateStatus(id, status);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "findrawmoney";
            }
        }
    }
}