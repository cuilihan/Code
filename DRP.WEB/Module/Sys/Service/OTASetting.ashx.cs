using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.BF.SysAdmin;

namespace DRP.WEB.Module.Sys.Service
{
    /// <summary>
    /// OTASetting 的摘要说明
    /// </summary>
    public class OTASetting : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询数据
                    Query(context);
                    break;
                case 2://删除数据
                    Delete(context);
                    break;
                case 3://验证数据
                    Checking(context);
                    break;
            }
        }
        private void Checking(HttpContext context)
        {
            string json = "";
            int result = 0;
            string OTA = context.Request["OTA"];
            string id = context.Request["id"];

            var list = DRP.DAL.OTA_Setting.Find(x => x.OTA == OTA && x.ID != Guid.Parse(id)).ToList();
            result = list.Count();
            if (result == 0)
            {
                json = "{\"verify\":\"0\"}";
                context.Response.Write(json);
                return;
            }
            else
            {
                json = "{\"verify\":\"1\"}";
                context.Response.Write(json);
                return;
            }
        }

        private void Delete(HttpContext context)
        {
            var id = Guid.Parse(context.Request["id"]);
            var isOk = new OTASetting_BF().Delete(id);
            context.Response.Write(isOk ? "1" : "0");
        }

        private void Query(HttpContext context)
        {
            var qry = new OTAQuery();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            qry.Supplier = context.Request["Supplier"];

            var total = 0;
            var d = new OTASetting_BF().OTAQuery(qry, out total);
            var json = ConvertJson.ToJson(d);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}