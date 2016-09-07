using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework;

namespace DRP.WEB.Module.Sys.Service
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class Department : HandlerBase
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

        protected override string NavigateID
        {
            get
            {
                return "sysdept";
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var strJson = new Dept_BF().DeptTreeJson();
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
                var dal = new Dept_BF();
                if (dal.HasUser(keyID))
                {
                    context.Response.Write("2");
                }
                if (dal.HasChild(keyID))
                {
                    context.Response.Write("3");
                }
                else
                {
                    dal.Delete(keyID);
                    context.Response.Write("1");
                }
            }
        }

    }
}