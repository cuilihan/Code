using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.BF.WeChat;
using DRP.DAL;

namespace DRP.WeChat.Service
{
    /// <summary>
    /// WeChatHandler 的摘要说明
    /// </summary>
    public class WeChatHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1:
                    Rpt_BizStatistic(context);
                    break;
                case 2: //图片压缩测试
                    CompressTest(context);
                    break;
            }

        }

        private void Rpt_BizStatistic(HttpContext context)
        {
            var entity = new WeChatIndex_BF().GetEntity();
            if (entity != null)
            {
                var list = new List<DAL.Rpt_BizStatistic>();
                list.Add(entity);
                context.Response.Write(ConvertJson.ListToJson(list));
            }
        }

        private void CompressTest(HttpContext context)
        {
            var base64=context.Request["base64"];
            var mime = context.Request["type"];
            var img= DRP.Framework.Core.Security.Base64Decrypt(base64);
            context.Response.Write(img);
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