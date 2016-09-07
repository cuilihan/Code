using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.Cached;
using DRP.DAL;
using DRP.Framework.Core;
using DRP.Log;
using DRP.BF.Cache;
using DRP.DAL.DataAccess;
using System.Transactions;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 导航组管理
    /// </summary>
    public class NavGroup_BF
    {

        #region << 按导航组菜单菜单集合 >>

        private const string NavGroupKey = "Om_NavGroup_Key";

        /// <summary>
        /// 获取导航组具有的菜单集合
        /// </summary>
        /// <param name="navGroupID"></param>
        /// <returns></returns>
        public List<DAL.Om_Navigate> GetNavigateByNavGrouID(string navGroupID)
        {
            var key = NavGroupKey + "_" + navGroupID;
            var list = BizCacheHelper.NavGroupNavigateCache.Get(key);
            if (list == null)
            {
                list = new OmNavGroupDAL().GetNavigateByNavGroupID(navGroupID);
                BizCacheHelper.NavGroupNavigateCache.Insert(key, list);
            }
            return list;
        }

        /// <summary>
        /// 删除导航组缓存
        /// </summary>
        internal void RemoveNavGroupCache(string navGroupID)
        {
            var key = NavGroupKey + "_" + navGroupID;
            BizCacheHelper.NavGroupNavigateCache.Remove(key);
        }

        /// <summary>
        /// 获取登录用户具有的菜单集合
        /// </summary>
        /// <param name="navGroupID"></param>
        /// <returns></returns>
        public List<DAL.Om_Navigate> GetNavigateByNavGrouID()
        {
            var navGroupID = AuthenticationPage.UserInfo.NavGroupID;
            return GetNavigateByNavGrouID(navGroupID);
        }
        #endregion

        /// <summary>
        /// 导航组集合
        /// </summary>
        /// <returns></returns>
        public List<Om_NavGroup> GetNavGroup()
        {
            return Om_NavGroup.All().OrderBy(x => x.OrderIndex).ToList();
        }

        /// <summary>
        /// 导航组详情
        /// </summary>
        /// <param name="keyId"></param>
        /// <returns></returns>
        public Om_NavGroup Get(string keyId)
        {
            return Om_NavGroup.SingleOrDefault(x => x.ID == keyId);
        }

        /// <summary>
        /// 保存导航组
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(DAL.Om_NavGroup e, string navIDs)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 导航组对应的菜单

                    DAL.Om_NavGroupRelation.Delete(x => x.GroupID == e.ID);

                    if (!string.IsNullOrEmpty(navIDs))
                    {
                        var arr = navIDs.Split(',');
                        foreach (var id in arr)
                        {
                            var t = new DAL.Om_NavGroupRelation();
                            t.ID = Guid.NewGuid().ToString();
                            t.NavID = id;
                            t.GroupID = e.ID;
                            t.Save();
                        }
                    }
                    #endregion

                    var entity = Get(e.ID);
                    if (entity == null)
                    {
                        entity = new DAL.Om_NavGroup();
                        entity.CreateUserName = user.UserName;
                        entity.CreateDate = DateTime.Now;
                    }
                    entity.NavGroup = e.NavGroup;
                    entity.OrderIndex = e.OrderIndex;
                    entity.Comment = e.Comment;
                    entity.ID = e.ID;
                    entity.Save();

                    scope.Complete();

                    //导航组重新设置功能时，须重新加载导航组对应的功能菜单
                    RemoveNavGroupCache(e.ID);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存导航组时发生错误");
            }
            return isSuccess;
        }


        /// <summary>
        /// 删除导航组
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Om_NavGroup.Delete(x => x.ID == keyID);
            RemoveNavGroupCache(keyID);
        }

        /// <summary>
        /// 查询导航组菜单（用于导航组功能菜单分配）
        /// </summary>
        /// <param name="navGroupID"></param>
        /// <returns>Json数组</returns>
        public string QueryNavGroupItems(string navGroupID)
        {
            if (string.IsNullOrEmpty(navGroupID))
            {
                var list = new Navigate_BF().GetNaviate();
                return ConvertJson.ListToJson(list);
            }
            else
            {
                var dt = new OmNavGroupDAL().QueryNavigateByGroupID(navGroupID);
                return ConvertJson.ToJson(dt);
            }
        }
    }
}
