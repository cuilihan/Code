using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Sys.Service
{
    /// <summary>
    /// 系统用户管理
    /// </summary>
    public class User : HandlerBase
    {
        User_BF dal = new User_BF();

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询用户
                    QueryUserInfo(context);
                    break;
                case 2://删除用户
                    Delete(context);
                    break;
                case 3://选择部门Json
                    CreateDeptComboTreeJson(context);
                    break;
                case 4://菜单功能 
                    QueryNavigate(context);
                    break;
                case 5://数据权限
                    QueryDataPermission(context);
                    break;
                case 6://订单查询权限
                    QueryOrderPermissiion(context);
                    break;
                case 7://OTA用户数据初始化
                    OTAInitUser(context);
                    break;
                case 8://OTA绑定用户数据查询
                    QueryOTAUserInfo(context);
                    break;
                case 9: //OTA用户数据           
                    context.Response.Write(OTAUserInfoQueryData(context));
                    break;
                case 10://OTA用户数据查询
                    OTAQueryUser(context);
                    break;
                case 11://OTA用户数据解绑
                    OTAUserDelete(context);
                    break;
                case 12://OTA用户数据绑定
                    OTAUserSave(context);
                    break;
            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        private void QueryUserInfo(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.Keyword = context.Request["key"];

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new User_BF().GetSysUser(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                new User_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }

        /// <summary>
        /// 下拉选择部门
        /// </summary>
        /// <param name="context"></param>
        private void CreateDeptComboTreeJson(HttpContext context)
        {
            var str=new Dept_BF().GetSysDeptComboTree(true);
            context.Response.Write(str);
        }

        #region 权限查询

        /// <summary>
        /// 导航权限查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryNavigate(HttpContext context)
        {
            var userID = context.Request["uid"];
            var dt=new User_BF().QueryUserNavPermission(userID);
            context.Response.Write(ConvertJson.ToJson(dt));
        }

        /// <summary>
        /// 数据权限查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryDataPermission(HttpContext context)
        {
            var userId =context.Request["uid"];
            var p = new User_BF().QueryUserDataPermission(userId);
            var v = "私有数据权限";
            switch (p)
            {
                case DataPermission.Dept:
                    v = "部门数据权限";
                    break;
                case DataPermission.All:
                    v = "所有数据权限";
                    break;
            }
            context.Response.Write(v);
        }

        /// <summary>
        /// 订单权限查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrderPermissiion(HttpContext context)
        {
            var userId = context.Request["uid"];
            var v= new User_BF().QueryUserOrderPermission(userId);
            context.Response.Write(v);
        }

        #endregion

        /// <summary>
        /// OTA初始化
        /// </summary>
        /// <param name="context"></param>
        private void OTAInitUser(HttpContext context)
        {
            var isok = dal.InitUserData(Guid.Parse(context.Request["OTAID"]));

            context.Response.Write(isok ? "1" : "0");

        }

        /// <summary>
        /// 查询用户
        /// </summary>
        private void QueryOTAUserInfo(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.Keyword = context.Request["key"];

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new User_BF().GetOTASysUser(qry);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        private string OTAUserInfoQueryData(HttpContext context)
        {
            var id = context.Request["id"];
            var list = new User_BF().GetOTABindUserInfo(id);
            var json = ConvertJson.ListToJson<OTAUserAccount>(list);
            return json;
        }

        private void OTAQueryUser(HttpContext context)
        {
            var listGet = new DRP.BF.DataSync.OctHelper().QueryPackageBuyerStoreUserList();
            var list = listGet[0];
            context.Response.Write(ConvertJson.ListToJson(listGet));
        }

        public void OTAUserDelete(HttpContext context)
        {
            var isok = dal.OTAUserDelete(Guid.Parse(context.Request["relId"]));

            context.Response.Write(isok ? "1" : "0");
        }

        public void OTAUserSave(HttpContext context)
        {
            var uid = context.Request["uid"];
            var otdid = context.Request["otdid"];
            var otauid = context.Request["otauid"];
            var name = context.Request["name"];
            var otaName = context.Request["otaName"];

            var isok = dal.OTAUserSave(uid, otdid, otauid, name, otaName);

            context.Response.Write(isok ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "sysuser";
            }
        }
    }
}