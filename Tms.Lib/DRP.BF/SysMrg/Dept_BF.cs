using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class Dept_BF
    {
        /// <summary>
        /// 部门集合
        /// </summary>
        /// <returns></returns>
        public List<Sys_Department> GetDepartment()
        {
            return Sys_Department.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).OrderBy(x => x.OrderIndex).ToList();
        } 
     
        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Sys_Department Get(string keyID)
        {
            return DAL.Sys_Department.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存导航菜单
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, string name, int orderIndex, int dataStatus, string parentID = "")
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Sys_Department();
                    entity.CreateDate = DateTime.Now;
                    entity.OrgID = user.OrgID;
                    entity.CreateUserName = user.UserName;
                    entity.CreateUserID = user.UserID;
                    entity.ParentID = string.IsNullOrEmpty(parentID) ? Guid.Empty.ToString() : parentID;
                }
                entity.Name = name;
                entity.OrderIndex = orderIndex;
                entity.DataStatus = dataStatus;
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "保存部门信息时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 是否存在人员信息
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public bool HasUser(string deptID)
        {
            return DAL.Sys_UserInfo.Exists(x => x.DeptID == deptID || x.PartDeptID == deptID);
        }

        /// <summary>
        /// 是否存在子部门
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public bool HasChild(string deptID)
        {
            return DAL.Sys_Department.Exists(x => x.ParentID == deptID);
        }

        /// <summary>
        /// 删除导航菜单
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Sys_Department.Delete(x => x.ID == keyID);
        }

        #region 生成TreeGrid的Json数据

        /// <summary>
        /// 导航菜单JSON数据结构
        /// </summary>
        /// <returns></returns>
        public string DeptTreeJson()
        {
            var list = GetDepartment();
            var s = CreateTreeNode(list, Guid.Empty.ToString());
            return "[" + s + "]";
        }

        /// <summary>
        /// 递归生成树节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        private string CreateTreeNode(List<DAL.Sys_Department> list, string pId)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pId);
            var T = "\"ID\":\"{0}\",\"ParentID\":\"{1}\",\"Name\":\"{2}\",\"DataStatus\":\"{3}\"";
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = T + ",\"children\":[{4}]";
                    var node = CreateTreeNode(list, e.ID);
                    str = string.Format(str, e.ID, e.ParentID, e.Name, e.DataStatus, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    arr.Add("{" + string.Format(T, e.ID, e.ParentID, e.Name, e.DataStatus) + "}");
                }
            }
            return string.Join(",", arr);
        }

        #endregion
        
        #region 生成 ComboTree 的Json数据
        /// <summary>
        /// 生成ComboTree的Json数据
        /// </summary>
        /// <param name="isFilterDisabled">是否过滤禁用的部门</param>
        /// <returns></returns>
        public string GetSysDeptComboTree(bool isFilterDisabled=false)
        {
            var list = GetDepartment();
            if (isFilterDisabled)
                list = list.FindAll(x => x.DataStatus == 1);
            var s = CreateComboTreeNode(list, Guid.Empty.ToString());
            return "[" + s + "]";
        }

        private string CreateComboTreeNode(List<DAL.Sys_Department> list, string pid)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pid);
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = "\"id\":\"{0}\",\"text\":\"{1}\",\"state\":\"closed\",\"children\":[{2}]";
                    var node = CreateComboTreeNode(list, e.ID);
                    str = string.Format(str, e.ID, e.Name, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    var s = "\"id\":\"{0}\",\"text\":\"{1}\"";
                    arr.Add("{" + string.Format(s, e.ID, e.Name) + "}");
                }
            }
            return string.Join(",", arr);
        }
        #endregion

    }
}
