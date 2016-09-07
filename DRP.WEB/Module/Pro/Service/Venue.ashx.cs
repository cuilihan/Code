using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.PageBase;
using DRP.BF.ProMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Pro.Service
{
    /// <summary>
    /// 集合地点设置
    /// </summary>
    public class Venue : HandlerBase
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

        private void QueryData(HttpContext context)
        {
            var routeTypeID = context.Request["routeTypeID"];
            var list = new Venue_BF().GetVenue(routeTypeID);

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
            var list = new List<string>();
            foreach (var id in ids.Split(','))
            {
                list.Add(string.Format("'{0}'", id));
            }
            new Venue_BF().Delete(list);
            context.Response.Write("1");
        }

        protected override string NavigateID
        {
            get
            {
                return "venue";
            }
        }
    }
}