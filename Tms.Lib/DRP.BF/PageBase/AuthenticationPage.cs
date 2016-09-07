using System;
using DRP.Framework.Core;
using DRP.Framework.ViewState;
using System.Collections.Generic;
using System.Web;
using DRP.Framework;
using DRP.DAL;
using DRP.BF.SysMrg;

namespace DRP.BF
{
    /// <summary>
    /// 用户身份验证类
    /// </summary>
    public class AuthenticationPage : ViewStateCompressPage
    {
        private string __navigateID = "";
        /// <summary>
        /// 页面ID
        /// </summary>
        protected virtual string NavigateID { get { return __navigateID; } }

        protected override void OnPreLoad(EventArgs e)
        {
            try
            {
                new Permisstion_BF().AccessAuthority(NavigateID);
                base.OnPreLoad(e);
            }
            catch (Exception ex)
            {
                Permisstion_BF.UrlReturn(ex.Message);
                return;
            }
        }

        #region << 登录用户信息 >>

        private static Dictionary<string, UserInfo> __userCollection = new Dictionary<string, UserInfo>();

        /// <summary>
        /// 切换当前登录用户的使用角色（仅限用于用户具有多个角色的情况）
        /// </summary>
        /// <param name="role"></param>
        public static void SetUserCurrentRole(DAL.Sys_RoleInfo role)
        {
            var user = UserInfo;
            user.CurrentRole = role;
            user.UserDataPermission = new Permission_BF().QueryDataPermission(role == null ? "" : role.ID);
            RemoveCacheKey(user.UserID);
            __userCollection.Add(user.UserID, user);
        }

        /// <summary>
        /// 当前登录用户的身份信息
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                var userID = Page.User.Identity.Name;
                UserInfo user = null;
                if (__userCollection.ContainsKey(userID))
                    user = __userCollection[userID];
                if (user != null) return user;

                var dal = new UserInfoDAL();
                var entity = dal.Get(userID);
                if (entity != null)
                {
                    var userRoles = dal.GetUserRoles(userID);
                    var currentRole = userRoles.Count > 0 ? userRoles[0] : null;
                    user = new UserInfo()
                    {
                        UserID = entity.UserID,
                        UserName = entity.UserName,
                        UserAcct = entity.UserAcct,
                        DeptID = entity.DeptID,
                        DeptName = entity.DeptName,
                        PartDeptID = entity.PartDeptID,
                        PartDeptName = entity.PartDeptName,
                        Mobile = entity.Mobile,
                        OrgID = entity.OrgID,
                        OrgName = entity.OrgName,
                        NavGroupID = entity.NavGroupID,
                        ProName = entity.ProName,
                        LoginUserType = (UserType)entity.UserType,
                        Roles = userRoles,
                        CurrentRole = currentRole,
                        UserDataPermission = (UserType)entity.UserType == UserType.AdminUser ? DataPermission.All :
                        new Permission_BF().QueryDataPermission(currentRole == null ? "" : currentRole.ID),
                        RouteTypePermission = entity.UserType == (int)UserType.BizUser
                        ? new Permission_BF().QueryOrderPermission(entity.UserID, entity.OrgID)
                        : new List<string>()
                    };
                    __userCollection.Add(userID, user);
                }
                return user;
            }
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static UserInfo UserInfo
        {
            get
            {
                return new AuthenticationPage().CurrentUser;
            }
        }

        /// <summary>
        /// 清除登录身份的状态
        /// </summary>
        /// <param name="acctID"></param>
        public static void RemoveCacheKey(string userID)
        {
            if (__userCollection != null && __userCollection.ContainsKey(userID))
            {
                __userCollection.Remove(userID);
            }
        }
        #endregion
    }
}
