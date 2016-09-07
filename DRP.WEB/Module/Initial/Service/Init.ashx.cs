using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Init;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework;

namespace DRP.WEB.Module.Initial.Service
{
    /// <summary>
    /// 系统初始化
    /// </summary>
    public class Init : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://部门
                    SaveData(context);
                    break;
                case 2://角色
                    SaveRole(context);
                    break;
                case 3://参数
                    SaveParam(context);
                    break;
            }
        }

        private void SaveData(HttpContext context)
        {
            var orgName=context.Request["orgName"];
            var deptName = context.Request["deptName"];
            if (string.IsNullOrEmpty(orgName))
                return; 
            var isOk = new Init_BF().SaveOrg(orgName,deptName); 
            context.Response.Write(isOk ? "1" : "0");
        }

        private void SaveRole(HttpContext context)
        {
            var roleName = context.Request["roleName"];
            var isOk = new Init_BF().SaveRole(roleName);
            context.Response.Write(isOk ? "1" : "0");

        }

        /// <summary>
        /// 保存系统参数配置
        /// </summary>
        /// <param name="context"></param>
        private void SaveParam(HttpContext context)
        {
            var data=context.Request["data"];
            var isOk = new Init_BF().SaveBasicInfo(data);
            context.Response.Write(isOk ? "1":"0");
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