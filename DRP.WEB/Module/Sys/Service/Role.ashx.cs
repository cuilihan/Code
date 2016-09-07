using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Sys.Service
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class Role : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询角色列表
                    QueryData(context);
                    break;
                case 2://部门Tree
                    CreateDeptComboTreeJson(context);
                    break;
                case 3://查询部门用户
                    QueryDeptUser(context);
                    break;
                case 4://保存角色及成员
                    SaveRole(context);
                    break;
                case 5://删除角色
                    DeleteRole(context);
                    break;
                case 6://查询角色成员
                    QueryRoleMember(context);
                    break;
            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var list = new Role_BF().GetRoleInfo();
            var json = ConvertJson.ListToJson(list);
            context.Response.Write(json);
        }

        /// <summary>
        /// 查询角色成员
        /// </summary>
        /// <param name="context"></param>
        private void QueryRoleMember(HttpContext context)
        {
            var list = new Role_BF().GetRoleMember(context.Request["roleID"]);
            context.Response.Write(ConvertJson.ListToJson(list));
        }

        /// <summary>
        /// 下拉选择部门
        /// </summary>
        /// <param name="context"></param>
        private void CreateDeptComboTreeJson(HttpContext context)
        {
            var str = new Dept_BF().GetSysDeptComboTree();
            context.Response.Write(str);
        }

        /// <summary>
        /// 查询部门用户
        /// </summary>
        /// <param name="deptID"></param>
        private void QueryDeptUser(HttpContext context)
        {
            var deptID = context.Request["deptID"];
            var list = new User_BF().GetSysUser(deptID);
            context.Response.Write(ConvertJson.ListToJson(list));
        }

        /// <summary>
        /// 保存角色及成员
        /// </summary>
        /// <param name="context"></param>
        private void SaveRole(HttpContext context)
        {
            var roleID = context.Request["RoleID"];
            var roleName = context.Request["RoleName"];
            var comment = context.Request["Comment"];
            var userIDs = context.Request["UserID"];
            var isOk = new Role_BF().SaveRole(roleID, roleName, comment, userIDs);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="context"></param>
        private void DeleteRole(HttpContext context)
        {
            var roleID = context.Request["id"];
            if (string.IsNullOrEmpty(roleID))
                context.Response.Write("0");
            else
            {
                new Role_BF().DeleteRole(roleID);
                context.Response.Write("1");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "sysrole";
            }
        }
    }
}