using System;
using System.Collections.Generic;
using DRP.BF.SysMrg;

namespace DRP.BF
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    { 
        /// <summary>
        /// 业务用户
        /// </summary>
        BizUser=0,
        /// <summary>
        /// 管理员用户
        /// </summary>
        AdminUser = 1
    }

    /// <summary>
    /// 登录用户信息
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// 用户唯一标识ID
        /// </summary>
        public string UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 登录用户姓名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 登录用户手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        public string UserAcct
        {
            get;
            set;
        }         

        /// <summary>
        /// 用户所属的部门ID
        /// </summary>
        public string DeptID
        {
            get;
            set;
        }

        /// <summary>
        /// 兼管部门ID
        /// </summary>
        public string PartDeptID
        { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            get;
            set;
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }

        /// <summary>
        /// 兼管部门名称
        /// </summary>
        public string PartDeptName { get; set; }

        /// <summary>
        /// 用户具有的角色
        /// </summary>
        public List<DAL.Sys_RoleInfo> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// 当前登录使用的角色
        /// </summary>
        public DAL.Sys_RoleInfo CurrentRole { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgID { get; set; }

        /// <summary>
        /// 机构所属的导航组
        /// </summary>
        public string NavGroupID { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 登录用户类型
        /// </summary>
        public UserType LoginUserType { get; set; }

        /// <summary>
        /// 登录用户的数据权限
        /// </summary>
        public DataPermission UserDataPermission
        {
            get;
            set;
        }

        /// <summary>
        /// 订单线路查询权限
        /// </summary>
        public List<string> RouteTypePermission
        { get; set; }
    }
}
