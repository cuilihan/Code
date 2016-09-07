using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.Framework;

namespace DRP.WEB.Module.Glo.Service
{
    /// <summary>
    /// 目的地服务
    /// </summary>
    public class Destinaion : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询列表
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
            var routeTypeID = context.Request["routeTypeID"];
            var strJson = new Destination_BF().GetDestinationTreeJson(routeTypeID);
            context.Response.Write(strJson);
        }

        /// <summary>
        /// 删除导航
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                var d = new Destination_BF();
                if (d.HasChildNode(keyID))
                    context.Response.Write("-1");
                else
                {
                    new Destination_BF().Delete(keyID);
                    context.Response.Write("1");
                }
            }
        }
         

        protected override string NavigateID
        {
            get
            {
                return "destination";
            }
        }
    }
}