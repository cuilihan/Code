using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.BF.Cache;
using DRP.DAL.DataAccess;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 数据权限类型
    /// </summary>
    public enum DataPermission
    {
        /// <summary>
        /// 私有权限
        /// </summary>
        Private = 0,
        /// <summary>
        /// 部门权限
        /// </summary>
        Dept,
        /// <summary>
        /// 所有
        /// </summary>
        All
    }

    /// <summary>
    /// 权限管理
    /// </summary>
    public partial class Permission_BF
    {
        #region 保存权限

        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="dataPermission">数据权限 0：私有 1：部门 2：所有</param>
        /// <param name="orderPermission">订单权限 按线路类型划分</param>
        /// <param name="crmPermission">客户管理操作权限</param>
        /// <param name="navIDs">功能导航菜单ID，以“，”分隔</param>
        /// <returns></returns>
        public bool SavePermission(string roleID, int dataPermission, string orderPermission, string navIDs, int crmPermission, int dataModulePermission)
        {
            if (string.IsNullOrEmpty(navIDs)) return false;
            var isOk = true;
            var user = AuthenticationPage.UserInfo;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region << 功能菜单权限 >>
                    DAL.Sys_Permission.Delete(x => x.RoleID == roleID && x.OrgID == user.OrgID);
                    foreach (var nid in navIDs.Split(','))
                    {
                        var e = new DAL.Sys_Permission();
                        e.ID = Guid.NewGuid().ToString();
                        e.RoleID = roleID;
                        e.NavID = nid;
                        e.Permission = 0; //暂时未启用Button的操作权限
                        e.OrgID = user.OrgID;
                        e.Save();
                    }
                    #endregion

                    #region << 数据权限 >>
                    DAL.Sys_DataPermission.Delete(x => x.RoleID == roleID && x.OrgID == user.OrgID);
                    var dataPermissionEntity = new DAL.Sys_DataPermission();
                    dataPermissionEntity.ID = Guid.NewGuid().ToString();
                    dataPermissionEntity.RoleID = roleID;
                    dataPermissionEntity.Permission = dataPermission;
                    dataPermissionEntity.OrgID = user.OrgID;
                    dataPermissionEntity.Save();
                    #endregion

                    #region << 客户管理操作权限 >>
                    DAL.Sys_CrmPermission.Delete(x => x.RoleID == roleID && x.OrgID == user.OrgID && x.BtnPermission != 9);
                    if (crmPermission > 0)
                    {
                        var crmPermissionEntity = new DAL.Sys_CrmPermission();
                        crmPermissionEntity.ID = Guid.NewGuid().ToString();
                        crmPermissionEntity.RoleID = roleID;
                        crmPermissionEntity.BtnPermission = crmPermission;
                        crmPermissionEntity.OrgID = user.OrgID;
                        crmPermissionEntity.Save();
                    }
                    #endregion

                    #region << 订单权限 >>
                    DAL.Sys_OrderPermission.Delete(x => x.RoleID == roleID && x.OrgID == user.OrgID);
                    if (!string.IsNullOrEmpty(orderPermission))
                    {
                        foreach (var s in orderPermission.Split(','))
                        {
                            var oEntity = new DAL.Sys_OrderPermission();
                            oEntity.ID = Guid.NewGuid().ToString();
                            oEntity.RoleID = roleID;
                            oEntity.RouteTypeID = s;
                            oEntity.OrgID = user.OrgID;
                            oEntity.Save();
                        }
                    }
                    #endregion

                    #region << 数据模块显示权限 >>
                    DAL.Sys_CrmPermission.Delete(x => x.RoleID == roleID && x.OrgID == user.OrgID && x.BtnPermission == 9);
                    if (dataModulePermission > 0)
                    {
                        var dataModulePermissionEntity = new DAL.Sys_CrmPermission();
                        dataModulePermissionEntity.ID = Guid.NewGuid().ToString();
                        dataModulePermissionEntity.RoleID = roleID;
                        dataModulePermissionEntity.BtnPermission = dataModulePermission;
                        dataModulePermissionEntity.OrgID = user.OrgID;
                        dataModulePermissionEntity.Save();
                    }
                    #endregion

                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存权限时发生错误");
                isOk = false;
            }
            finally
            {
                if (isOk)
                {
                    //重新缓存机构的导航权限
                    var key = "Sys_Permision_Key_" + user.OrgID;
                    BizCacheHelper.SysPermissionCache.Remove(key);
                    BizUtility.WriteLog(user, "设置用户权限");
                }
            }
            return isOk;
        }
        #endregion

        #region 权限查询
        /// <summary>
        /// 角色数据权限(默认为私有权限）
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public DataPermission QueryDataPermission(string roleId)
        {
            if (string.IsNullOrEmpty(roleId)) return DataPermission.Private;
            var e = DAL.Sys_DataPermission.SingleOrDefault(x => x.RoleID == roleId);
            return e == null ? DataPermission.Private : (DataPermission)e.Permission;
        }

        /// <summary>
        /// (按线路类型）查询订单权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<string> QueryOrderPermission(string userID, string orgID)
        {
            return new PermissionDAL().UserRouteTypePermission(userID, orgID);
        }

        /// <summary>
        /// 角色订单权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>线路类型ID集合[RouteTypeID]</returns>
        public List<string> QueryOrderPermission(string roleId)
        {
            var coll = DAL.Sys_OrderPermission.Find(x => x.RoleID == roleId && x.OrgID == AuthenticationPage.UserInfo.OrgID);
            var list = new List<string>();
            foreach (var e in coll)
                list.Add(e.RouteTypeID);
            return list;
        }

        /// <summary>
        /// 角色的导航菜单权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns>菜单ID集合[NavigateID]</returns>
        public List<DAL.Sys_Permission> QueryPermission(string roleID)
        {
            var coll = DAL.Sys_Permission.Find(x => x.RoleID == roleID && x.OrgID == AuthenticationPage.UserInfo.OrgID);
            return coll.ToList();
        }

        /// <summary>
        /// 客户操作Button权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DAL.Sys_CrmPermission QueryCrmPermission(string roleID)
        {
            return DAL.Sys_CrmPermission.SingleOrDefault(x => x.RoleID == roleID && x.BtnPermission != 9);
        }

        /// <summary>
        /// 数据模块显示权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DAL.Sys_CrmPermission QueryDataModulePermission(string roleID)
        {
            return DAL.Sys_CrmPermission.SingleOrDefault(x => x.RoleID == roleID && x.BtnPermission == 9);
        }

        /// <summary>
        /// 操作Button是否有权限
        /// </summary>
        /// <remarks>多角色取其并集</remarks>
        /// <param name="buttonVal"></param>
        /// <returns></returns>
        public bool HasButtonPermission(int buttonVal)
        {
            var hasPermissioin = false;
            var roles = AuthenticationPage.UserInfo.Roles;
            roles.ForEach(x =>
            {
                if (hasPermissioin) return;
                var p = new Permission_BF().QueryCrmPermission(x.ID);
                if (p == null) { return; }//默认不具有所有权限
                hasPermissioin = (p.BtnPermission & buttonVal) == buttonVal;
            });
            return hasPermissioin;
        }

        /// <summary>
        /// 数据模块显示是否有权限
        /// </summary>
        /// <remarks>多角色取其并集</remarks>
        /// <param name="buttonVal"></param>
        /// <returns></returns>
        public bool DataModulePermission(int buttonVal)
        {
            var dataModulePermission = false;
            var roles = AuthenticationPage.UserInfo.Roles;
            if (!AuthenticationPage.UserInfo.UserAcct.Contains("admin"))
            {
                roles.ForEach(x =>
                {
                    if (dataModulePermission) return;
                    var p = new Permission_BF().QueryDataModulePermission(x.ID);
                    if (p == null) { return; }//默认不具有所有权限
                    dataModulePermission = (p.BtnPermission & buttonVal) == buttonVal;
                });
            }
            else
                dataModulePermission = true;
            return dataModulePermission;
        }
        #endregion
    }
}
