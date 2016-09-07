using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;
using DRP.Framework;

namespace DRP.BF.Init
{
    /// <summary>
    /// 初始化设置
    /// </summary>
    public class Init_BF
    {
        /// <summary>
        /// 当部门为空时，可以初始化
        /// </summary>
        /// <returns></returns>
        public bool IsNeededInit()
        {
            var isOk = true;
            var orgID = AuthenticationPage.UserInfo.OrgID;
            var dept= DAL.Sys_Department.Exists(x => x.OrgID == orgID);
            if (dept) //有部门数据
            {
                var user = DAL.Sys_UserInfo.Exists(x => x.OrgID == orgID); //有员工数据
                if (user)
                {
                    var role = DAL.Sys_RoleInfo.Exists(x => x.OrgID == orgID);//有角色
                    if (role)
                    {
                        var p = DAL.Sys_Permission.Exists(x => x.OrgID == orgID);//有权限分配
                        if (p)
                        {
                            isOk = false;
                        }
                    }
                }
            }
            return isOk;
        }

        /// <summary>
        /// 保存部门
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public bool SaveOrg(string companyName, string deptName)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                DAL.Sys_Department.Delete(x => x.OrgID == user.OrgID);
                var pDept = new DAL.Sys_Department();
                pDept.ID = Guid.NewGuid().ToString();
                pDept.Name = companyName;
                pDept.ParentID = Guid.Empty.ToString();
                pDept.OrderIndex = 1;
                pDept.DataStatus = 1;
                pDept.CreateUserID = user.UserID;
                pDept.CreateUserName = user.UserName;
                pDept.CreateDate = DateTime.Now;
                pDept.OrgID = user.OrgID;
                pDept.Save();

                if (!string.IsNullOrEmpty(deptName))
                {
                    var idx = 1;
                    var arr = deptName.Split(',');
                    foreach (var s in arr)
                    {
                        var entity = new Sys_Department();
                        entity.ID = Guid.NewGuid().ToString();
                        entity.Name = s;
                        entity.ParentID = pDept.ID;
                        entity.OrderIndex = idx++;
                        entity.DataStatus = 1;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CreateDate = DateTime.Now;
                        entity.OrgID = user.OrgID;
                        entity.Save();
                    }
                } 
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "初始化部门信息时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool SaveRole(string roleName)
        {
            var user = AuthenticationPage.UserInfo;
            var isSuccess = true;
            try
            {
                if (string.IsNullOrEmpty(roleName)) return false;
                DAL.Sys_RoleInfo.Delete(x => x.OrgID == user.OrgID);

                foreach (var r in roleName.Split(','))
                {
                    var entity = new Sys_RoleInfo();
                    entity.ID = Guid.NewGuid().ToString();
                    entity.RoleName = r;
                    entity.RoleMember = "";
                    entity.OrgID = user.OrgID;
                    entity.Comment = "";
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserName = user.UserName;
                    entity.CreateUserID = user.UserID;
                    entity.Save();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "初始化角色信息时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 初始化系统参数配置
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public bool SaveBasicInfo(string strData)
        {
            if (string.IsNullOrEmpty(strData)) return false;
            var user = AuthenticationPage.UserInfo;
            var isSuccess = true;
            try
            {
                var arr = strData.Split('@');//不同类型的参数
                if (arr.Length == 0) return false;
                var dict = new Dictionary<string, List<string>>();
                foreach (var p in arr)
                {
                    if (string.IsNullOrEmpty(p)) continue;
                    var arrData = p.Split(',');
                    if (arrData.Length == 0) continue;
                    var list = new List<string>();
                    var key = "";
                    foreach (var s in arrData)
                    {
                        if (string.IsNullOrEmpty(s)) continue;
                        var m_Arr = s.Split('-');
                        if (m_Arr.Length == 2)
                        {
                            if (string.IsNullOrEmpty(key))
                                key = m_Arr[0];
                            var val = m_Arr[1];
                            list.Add(val);
                        }
                    }
                    dict.Add(key, list);
                }
                foreach (KeyValuePair<string, List<string>> kv in dict)
                {
                    var basicType = kv.Key.ToInt();
                    Glo_BasicInfo.Delete(x => x.OrgID == user.OrgID && x.BasicType == basicType);
                    var idx = 0;
                    kv.Value.ForEach(x =>
                    {
                        var entity = new Glo_BasicInfo();
                        entity.ID = Guid.NewGuid().ToString();
                        entity.Name = x;
                        entity.BasicType = basicType;
                        entity.OrderIndex = idx++;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CreateDate = DateTime.Now;
                        entity.OrgID = user.OrgID;
                        entity.DeptID = user.DeptID;
                        entity.Save();
                    });
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "初始化参数信息时发生错误");
            }
            return isSuccess;
        }
    }
}
