using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework;

namespace DRP.WEB.Module.Sys.Service
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class Permission : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询导航菜单
                    QueryNavigateData(context);
                    break;
                case 2://保存权限
                    SavePermission(context);
                    break;
                case 3://查询角色的权限
                    QueryPermission(context);
                    break;
            }
        }

        /// <summary>
        /// 查询导航菜单
        /// </summary>
        private void QueryNavigateData(HttpContext context)
        {
            var strJson = new Navigate_BF().OrgNavigateTreeJson();
            context.Response.Write(strJson);
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="context"></param>
        private void SavePermission(HttpContext context)
        {
            var roleId = context.Request["RoleID"];
            var dataPermission = context.Request["DataPermissoin"];
            var orderPermission = context.Request["OrderPermission"];
            var navPermission = context.Request["NavPermission"];
            var crmPermission = context.Request["CrmPermission"];
            var dataModulePermission = context.Request["DataModulePermission"];
            var isOk = new Permission_BF().SavePermission(roleId, dataPermission.ToInt(), orderPermission, navPermission, crmPermission.ToInt(), dataModulePermission.ToInt());
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void QueryPermission(HttpContext context)
        {
            var dal = new Permission_BF();
            var roleId = context.Request["roleID"];
            var dataPermission = dal.QueryDataPermission(roleId);
            var orderPermission = dal.QueryOrderPermission(roleId);
            var navPermission = dal.QueryPermission(roleId);
            var crmPermission = dal.QueryCrmPermission(roleId);
            var dataModulePermission = dal.QueryDataModulePermission(roleId);

            var _crmPermissionArr = new List<string>();
            if (crmPermission != null)
            {
                var val = crmPermission.BtnPermission;
                var v = val & 1;
                if (v == 1) _crmPermissionArr.Add("1");
                v = val & 2;
                if (v == 2) _crmPermissionArr.Add("2");
                v = val & 4;
                if (v == 4) _crmPermissionArr.Add("4");
            }
            var arrNavPermission = new List<string>();
            foreach (var e in navPermission)
            {
                var json = string.Format("\"NavID\":\"{0}\"", e.NavID);
                arrNavPermission.Add("{" + json + "}");
            }
            var __BtnPermission = -1;
            if(dataModulePermission!=null){
                __BtnPermission = dataModulePermission.BtnPermission;
            }

            var jsonData = string.Format("\"DataPermission\":\"{0}\",\"CrmPermission\":\"{1}\",\"OrderPermission\":\"{2}\",\"NavPermission\":[{3}],\"DataModulePermission\":\"{4}\"",
                (int)dataPermission, string.Join(",", _crmPermissionArr), string.Join(",", orderPermission), string.Join(",", arrNavPermission), __BtnPermission);
            context.Response.Write("{" + jsonData + "}");
        }

        protected override string NavigateID
        {
            get
            {
                return "syspermission";
            }
        }
    }
}