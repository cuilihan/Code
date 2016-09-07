using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 用户权限管理
    /// </summary>
    public class PermissionDAL
    {
        /// <summary>
        /// 用户订单（按线路类型）查询权限
        /// </summary>
        /// <returns></returns>
        public List<string> UserRouteTypePermission(string userID, string orgID)
        {
            var list = new List<string>();
            var sql = "SELECT DISTINCT RouteTypeID FROM Sys_OrderPermission a INNER JOIN Sys_RoleMember b ON a.RoleID=b.RoleID WHERE UserID=@UserID AND b.OrgID=@OrgID";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, userID, orgID).ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader["RouteTypeID"].ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 用户订单（按线路类型）查询权限名称
        /// </summary>
        /// <returns></returns>
        public List<string> UserRouteTypeNamePermission(string userID)
        {
            var list = new List<string>();
            var sql = " SELECT DISTINCT c.Name RouteTypeName FROM Sys_OrderPermission a INNER JOIN Sys_RoleMember b ON a.RoleID=b.RoleID INNER JOIN dbo.Glo_BasicInfo c ON a.RouteTypeID=c.ID WHERE UserID=@UserID";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, userID).ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader["RouteTypeName"].ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 查询用户权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int UserDataPermission(string userID)
        {
            var sql = "select MAX(p.Permission) permission from Sys_DataPermission p where exists (select RoleID from Sys_RoleMember where UserID=@UserID and RoleID=p.RoleID)";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, userID).ExecuteReader())
            {
                if (reader.Read())
                {
                    return Convert.ToInt16(reader["permission"].ToString());
                }
                else
                    return 0;
            }
        }
    }
}
