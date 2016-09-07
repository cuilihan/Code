using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class Role_BF
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public List<DAL.Sys_RoleInfo> GetRoleInfo()
        {
            return DAL.Sys_RoleInfo.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).ToList();
        }

        /// <summary>
        /// 角色详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Sys_RoleInfo Get(string keyID)
        {
            return DAL.Sys_RoleInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存角色及成员
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="comment"></param>
        /// <param name="userIDs">多个成员格式以“，”分隔：UserID+"|"+UserName+","+UserID+"|"+UserName </param>
        /// <returns></returns>
        public bool SaveRole(string roleID, string roleName, string comment, string userIDs)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                DAL.Sys_RoleInfo e = null;
                var orgId = user.OrgID;
                if (!string.IsNullOrEmpty(roleID))
                    e = Get(roleID);
                else
                    roleID = Guid.NewGuid().ToString();

                if (e == null)
                {
                    e = new DAL.Sys_RoleInfo();
                    e.CreateDate = DateTime.Now;
                    e.CreateUserID = user.UserID;
                    e.CreateUserName = user.UserName;
                    e.OrgID = orgId;
                }
                e.RoleName = roleName;
                e.Comment = comment;
                e.ID = roleID;
                var listUserName = new List<string>();
                using (TransactionScope scope = new TransactionScope())
                {
                    DAL.Sys_RoleMember.Delete(x => x.RoleID == roleID && x.OrgID == orgId);
                    if (!string.IsNullOrEmpty(userIDs))
                    {
                        var arrIDs = userIDs.Split(',');
                        foreach (var uid in arrIDs)
                        {
                            var arrUser = uid.Split('|');
                            var u = new DAL.Sys_RoleMember();
                            u.ID = Guid.NewGuid().ToString();
                            u.RoleID = roleID;
                            u.UserID = arrUser[0];
                            u.UserName = arrUser[1];
                            u.OrgID = orgId;
                            u.Save();

                            listUserName.Add(u.UserName);
                        }
                    }
                    e.RoleMember = string.Join(",", listUserName);//在主表中显示所有的角色成员
                    e.Save();
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存角色及成员时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public void DeleteRole(string roleID)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var orgId = user.OrgID;
                    //删除角色
                    DAL.Sys_RoleInfo.Delete(x => x.ID == roleID && x.OrgID == orgId);

                    //删除角色成员
                    DAL.Sys_RoleMember.Delete(x => x.RoleID == roleID && x.OrgID == orgId);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "删除角色时发生错误");
            }
        }

        /// <summary>
        /// 查询角色成员
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<DAL.Sys_RoleMember> GetRoleMember(string roleID)
        {
            return DAL.Sys_RoleMember.Find(x => x.RoleID == roleID).ToList();
        }
    }
}
