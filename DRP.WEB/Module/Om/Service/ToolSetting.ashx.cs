using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// 推送工具
    /// </summary>
    public class ToolSetting : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询数据
                    QueryData(context);
                    break;
                case 2: //删除 
                    Delete(context);
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var list = new ToolSetting_BF().GetOmTools();
            var json = ConvertJson.ListToJson(list); 
            context.Response.Write(json);
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
                new ToolSetting_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }


        protected override string NavigateID
        {
            get
            {
                return "omtool";
            }
        }
    }
}