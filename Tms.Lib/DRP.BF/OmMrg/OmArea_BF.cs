using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.Framework.Core;
using DRP.Log;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 行政区域管理
    /// </summary>
    public class OmArea_BF
    {
        private const string NavigateKey = "Om_Area";

        /// <summary>
        /// 行政区域集合
        /// </summary>
        /// <returns></returns>
        public List<DAL.Om_Area> GetOmArea()
        {
            return Om_Area.All().OrderBy(x => x.OrderIndex).ToList();
        }

        /// <summary>
        /// 区域
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Om_Area Get(string keyID)
        {
            return DAL.Om_Area.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存导航菜单
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl, string parentID = "")
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Om_Area();
                    entity.CreateDate = DateTime.Now;
                    entity.ParentID = string.IsNullOrEmpty(parentID) ? Guid.Empty.ToString() : parentID;
                }
                entity = new ReflectHelper<DAL.Om_Area>().AssignEntity(entity, pnlControl);
                entity.ID = keyID;
                entity.CreateUserName = user.UserName;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存导航组时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Om_Area.Delete(x => x.ID == keyID);
        }

        /// <summary>
        /// 获取区域的完整路径名称
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        private string GetAreaPath(List<DAL.Om_Area> list, string areaID)
        {
            var arr = new List<string>();
            var e = list.Find(x => x.ID == areaID);
            if (e == null)
                return "";
            else
            {
                arr.Add(e.AreaName);
                arr.Add(GetAreaPath(list, e.ParentID));
            }
            return string.Join(",", arr);
        }

        /// <summary>
        /// 获取区域的完整路径名称
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public string GetAreaPath(string areaID)
        {
            var list = GetOmArea();
            return GetAreaPath(list, areaID); 
        }

        #region 生成TreeGrid的Json数据

        /// <summary>
        /// 导航菜单JSON数据结构
        /// </summary>
        /// <returns></returns>
        public string GetOmAreaTreeJson()
        {
            var list = GetOmArea();
            var s = CreateTreeNode(list, Guid.Empty.ToString());
            return "[" + s + "]";
        }

        /// <summary>
        /// 递归生成树节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        private string CreateTreeNode(List<DAL.Om_Area> list, string pId)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pId);
            var T = "\"ID\":\"{0}\",\"ParentID\":\"{1}\",\"AreaName\":\"{2}\",\"OrderIndex\":\"{3}\"";
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = T + ",\"children\":[{4}]";
                    var node = CreateTreeNode(list, e.ID);
                    str = string.Format(str, e.ID, e.ParentID, e.AreaName, e.OrderIndex, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    arr.Add("{" + string.Format(T, e.ID, e.ParentID, e.AreaName, e.OrderIndex) + "}");
                }
            }
            return string.Join(",", arr);
        }

        #endregion

        #region 生成 ComboTree 的Json数据
        /// <summary>
        /// 生成ComboTree的Json数据
        /// </summary>
        /// <returns></returns>
        public string GetOmAreaComboTree()
        {
            var list = GetOmArea();
            var s = CreateComboTreeNode(list, Guid.Empty.ToString());
            return "[" + s + "]";
        }

        private string CreateComboTreeNode(List<DAL.Om_Area> list, string pid)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pid);
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = "\"id\":\"{0}\",\"text\":\"{1}\",\"state\":\"closed\",\"children\":[{2}]";
                    var node = CreateComboTreeNode(list, e.ID);
                    str = string.Format(str, e.ID, e.AreaName, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    var s = "\"id\":\"{0}\",\"text\":\"{1}\"";
                    arr.Add("{" + string.Format(s, e.ID, e.AreaName) + "}");
                }
            }
            return string.Join(",", arr);
        }
        #endregion
    }
}
