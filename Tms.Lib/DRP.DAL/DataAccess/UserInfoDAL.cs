using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic.Query;

namespace DRP.DAL
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public partial class UserInfoDAL
    {
        /// <summary>
        /// 用户具体信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public V_UserInfo_AllEntity Get(string userID)
        {
            V_UserInfo_AllEntity e = null;
            var sql = "Select UserID,OrgID,NavGroupID,OrgName,UserName,UserAcct,UserPwd,DataStatus,DeptID,DeptName,ProName,PartDeptID,PartDeptName,Mobile,UserType From [V_UserInfo_All] WHERE 1=1 and UserID=@UserID";
            using (var rdr = new CodingHorror(sql, userID).ExecuteReader())
            {
                if (rdr.Read())
                {
                    e = new V_UserInfo_AllEntity();
                    e.UserID = rdr["UserID"].ToString();
                    e.OrgID = rdr["OrgID"].ToString();
                    e.OrgName = rdr["OrgName"].ToString();
                    e.NavGroupID = rdr["NavGroupID"].ToString();
                    e.UserName = rdr["UserName"].ToString();
                    e.UserAcct = rdr["UserAcct"].ToString();
                    e.UserPwd = rdr["UserPwd"].ToString();
                    e.DataStatus = Convert.ToInt16(rdr["DataStatus"].ToString());
                    e.DeptID = rdr["DeptID"].ToString();
                    e.DeptName = rdr["DeptName"].ToString();
                    e.PartDeptID = rdr["PartDeptID"].ToString();
                    e.PartDeptName = rdr["PartDeptName"].ToString();
                    e.Mobile = rdr["Mobile"].ToString();
                    e.ProName = rdr["ProName"].ToString();
                    var userType = rdr["UserType"].ToString();
                    int uType = 0;
                    Int32.TryParse(userType, out uType);
                    e.UserType = uType;
                }
            }
            return e;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userAcct"></param>
        /// <param name="userPwd"></param>
        /// <param name="orgID"></param> 
        public V_UserInfo_AllEntity Get(string userAcct, string userPwd, string orgID)
        {
            V_UserInfo_AllEntity e = null;
            var sql = "Select UserID,OrgID,NavGroupID,OrgName,UserName,UserAcct,UserPwd,DataStatus,DeptID,DeptName,ProName,PartDeptID,PartDeptName,Mobile,UserType From [V_UserInfo_All] WHERE 1=1 and UserAcct=@UserAcct and UserPwd=@UserPwd and OrgID=@OrgID and DataStatus=@DataStatus";
            using (var rdr = new CodingHorror(sql, userAcct, userPwd, orgID, 1).ExecuteReader())
            {
                if (rdr.Read())
                {
                    e = new V_UserInfo_AllEntity();
                    e.UserID = rdr["UserID"].ToString();
                    e.OrgID = rdr["OrgID"].ToString();
                    e.OrgName = rdr["OrgName"].ToString();
                    e.NavGroupID = rdr["NavGroupID"].ToString();
                    e.UserName = rdr["UserName"].ToString();
                    e.UserAcct = rdr["UserAcct"].ToString();
                    e.UserPwd = rdr["UserPwd"].ToString();
                    e.DataStatus = Convert.ToInt16(rdr["DataStatus"].ToString());
                    e.DeptID = rdr["DeptID"].ToString();
                    e.DeptName = rdr["DeptName"].ToString();
                    e.PartDeptID = rdr["PartDeptID"].ToString();
                    e.PartDeptName = rdr["PartDeptName"].ToString();
                    e.Mobile = rdr["Mobile"].ToString();
                    e.ProName = rdr["ProName"].ToString();
                    var userType = rdr["UserType"].ToString();
                    int uType = 0;
                    Int32.TryParse(userType, out uType);
                    e.UserType = uType;
                }
            }
            return e;
        }


        /// <summary>
        /// 用户是否存在(已启用状态的用户)
        /// </summary>
        /// <param name="userAcct"></param>
        /// <param name="userPwd"></param>
        /// <param name="orgID"></param>
        /// <returns>用户ID</returns>
        public string IsExitUser(string userAcct, string userPwd, string orgID)
        {
            var sql = "Select UserID From [V_UserInfo_All] WHERE 1=1 and UserAcct=@UserAcct and UserPwd=@UserPwd and OrgID=@OrgID and DataStatus=@DataStatus";
            using (var rdr = new CodingHorror(sql, userAcct, userPwd, orgID, 1).ExecuteReader())
            {
                if (rdr.Read())
                    return rdr["UserID"].ToString();
                else
                    return "";
            }
        }


        /// <summary>
        /// 用户的角色列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<DAL.Sys_RoleInfo> GetUserRoles(string userID)
        {
            var sql = "SELECT b.ID,b.RoleName FROM Sys_RoleMember a INNER JOIN Sys_RoleInfo b ON a.RoleID=b.ID WHERE a.UserID=@userID";
            return new SubSonic.Query.CodingHorror(sql, userID).ExecuteTypedList<DAL.Sys_RoleInfo>();
        }
    }
}
