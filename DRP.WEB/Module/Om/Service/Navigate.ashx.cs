using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework.Core;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// 导航菜单管理
    /// </summary>
    public class Navigate : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询导航菜单
                    QueryData(context);
                    break;
                case 2://删除导航菜单
                    Delete(context);
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var strJson = new Navigate_BF().NavigateTreeJson(); 
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
                new Navigate_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "omnavigate";
            }
        }
    }
}