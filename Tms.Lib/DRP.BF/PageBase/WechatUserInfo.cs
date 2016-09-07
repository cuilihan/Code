using DRP.BF;
using DRP.BF.SysMrg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRP.BF
{
    /// <summary>
    /// 微信登录用户信息
    /// </summary>
    [Serializable]
    public class WechatUserInfo
    {
        /// <summary>
        ///用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgID { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

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