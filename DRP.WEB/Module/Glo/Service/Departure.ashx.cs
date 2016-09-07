using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Glo.Service
{
    /// <summary>
    /// 出发地服务
    /// </summary>
    public class Departure : HandlerBase
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
            var list = new Departure_BF().GetDeparture();
            var json = ConvertJson.ListToJson(list);
            context.Response.Write(json);
        }

        /// <summary>
        /// 删除导航组
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                new Departure_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "departure";
            }
        }
    }
}