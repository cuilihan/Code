using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.BF.Fin;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 订单收款管理
    /// </summary>
    public class OrderCollected : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://订单收款明细查询
                    QueryOrderCollected(context);
                    break;
            }
        }

        private void QueryOrderCollected(HttpContext context)
        {
            var orderID = context.Request["orderID"];
            var list = new OrderCollected_BF().GetOrderCollectedList(orderID);
            var strJson = ConvertJson.ListToJson(list);
            context.Response.Write(strJson);
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