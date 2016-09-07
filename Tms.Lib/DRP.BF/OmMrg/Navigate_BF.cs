using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;
using DRP.Cached;
using System.Web.UI.WebControls;
using DRP.Framework.Core;
using DRP.Log;
using DRP.BF.Cache;
using DRP.DAL.DataAccess;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class Navigate_BF
    {
        /// <summary>
        /// 导航菜单集合
        /// </summary>
        /// <returns></returns>
        public List<Om_Navigate> GetNaviate()
        {
            return Om_Navigate.All().OrderBy(x => x.OrderIndex).ToList();
        }

        /// <summary>
        /// 登录用户一级导航菜单
        /// </summary>
        /// <returns></returns>
        public List<Om_Navigate> GetLoginUserTopNavigate()
        {
            var list = GetLoginUserNavigate();
            return list = list.FindAll(x => x.ParentID == Guid.Empty.ToString());
        }

        /// <summary>
        /// 登录用户具有有权限的导航菜单
        /// </summary>
        /// <returns></returns>
        public List<Om_Navigate> GetLoginUserNavigate()
        {
            var user = AuthenticationPage.UserInfo;
            var list = new NavGroup_BF().GetNavigateByNavGrouID();//机构所有菜单的集合

            if (user.LoginUserType == UserType.BizUser) //子系统的管理员不设权限，业务用户再进行权限过滤
            {
                if (user.CurrentRole == null)
                {
                    throw new Exception("当前用户未设置访问权限");
                }
                list = new Permisstion_BF().NavigatePermissionFilter(list, user.CurrentRole.ID);//登录用户具有的权限过滤
            }
            return list.OrderBy(x => x.OrderIndex).ToList();
        }

        #region 机构的导航菜单
        /// <summary>
        /// 机构的导航菜单treegrid数据源(json)
        /// </summary>
        /// <returns></returns>
        public string OrgNavigateTreeJson()
        {
            var list = new NavGroup_BF().GetNavigateByNavGrouID();
            return ConvertJson.ListToJson(list);
        }
        #endregion

        #region 生成TreeGrid的Json数据

        /// <summary>
        /// 导航菜单JSON数据结构
        /// </summary>
        /// <returns></returns>
        public string NavigateTreeJson()
        {
            var list = GetNaviate();
            var s = CreateTreeNode(list, Guid.Empty.ToString());
            return "[" + s + "]";
        }

        /// <summary>
        /// 递归生成树节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        private string CreateTreeNode(List<DAL.Om_Navigate> list, string pId)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pId);
            var T = "\"ID\":\"{0}\",\"ParentID\":\"{1}\",\"NavName\":\"{2}\",\"PageID\":\"{3}\",\"NavUrl\":\"{4}\",\"NavCls\":\"{5}\",\"IsVisual\":\"{6}\",\"OrderIndex\":\"{7}\"";
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = T + ",\"children\":[{8}]";
                    var node = CreateTreeNode(list, e.ID);
                    str = string.Format(str, e.ID, e.ParentID, e.NavName, e.PageID, e.NavUrl, e.NavCls, e.IsVisual, e.OrderIndex, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    arr.Add("{" + string.Format(T, e.ID, e.ParentID, e.NavName, e.PageID, e.NavUrl, e.NavCls, e.IsVisual, e.OrderIndex) + "}");
                }
            }
            return string.Join(",", arr);
        }

        #endregion


        /// <summary>
        /// 导航菜单
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Om_Navigate Get(string keyID)
        {
            return DAL.Om_Navigate.SingleOrDefault(x => x.ID == keyID);
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
                    entity = new DAL.Om_Navigate();
                    entity.CreateDate = DateTime.Now;
                    entity.ParentID = string.IsNullOrEmpty(parentID) ? Guid.Empty.ToString() : parentID;
                }
                entity = new ReflectHelper<DAL.Om_Navigate>().AssignEntity(entity, pnlControl);
                entity.ID = keyID;
                entity.CreateUserName = user.UserName;
                entity.Save();

                //导航菜单更新时，所有的导航组缓存须重建
                var listGroup = new NavGroup_BF().GetNavGroup();
                listGroup.ForEach(x =>
                {
                    new NavGroup_BF().RemoveNavGroupCache(x.ID);
                });
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存导航组时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 删除导航菜单
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            try
            {
                Om_Navigate.Delete(x => x.ID == keyID);

                //导航菜单删除时，所有的导航组缓存须重建
                var listGroup = new NavGroup_BF().GetNavGroup();
                listGroup.ForEach(x =>
                {
                    new NavGroup_BF().RemoveNavGroupCache(x.ID);
                });
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除菜单时发生错误");
            }
        }
    }
}
