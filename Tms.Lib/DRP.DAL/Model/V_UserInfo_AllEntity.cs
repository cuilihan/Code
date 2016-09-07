using System;
using System.Collections.Generic;

namespace DRP.DAL
{
    /// <summary> 
    /// V_UserInfo_All实体类
    /// 创建人：系统自动生成
    /// 日期：2014-10-20
    /// </summary>
    [Serializable]
    public class V_UserInfo_AllEntity
    {
         /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// OrgID
        /// </summary>
        public string OrgID { get; set; }

        /// <summary>
        /// OrgName
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 导航组ID
        /// </summary>
        public string NavGroupID { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }

        /// <summary>
        /// UserAcct
        /// </summary>
        public string UserAcct { get; set; }

        /// <summary>
        /// UserPwd
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// DataStatus
        /// </summary>
        public int DataStatus { get; set; }

        /// <summary>
        /// DeptID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// DeptName
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// PartDeptID
        /// </summary>
        public string PartDeptID { get; set; }

        /// <summary>
        /// PartDeptName
        /// </summary>
        public string PartDeptName { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
   
    }
}
