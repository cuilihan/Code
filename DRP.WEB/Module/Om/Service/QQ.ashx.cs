using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.GloMrg;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Glo.Service
{
    /// <summary>
    /// QQ 的摘要说明
    /// </summary>
    public class QQ : HandlerBase
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
            var list = new QQ_BF().GetData();
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
            var isOk = new QQ_BF().Delete(list);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "onlineservice";
            }
        }
    }
}