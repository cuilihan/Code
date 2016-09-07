using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.BF.Cache;
using DRP.DAL;
using DRP.BF.SysMrg;

namespace DRP.BF.Glo
{
    /// <summary>
    /// 目的地集合
    /// </summary>
    public class Destination_BF
    {
        private const string DestinationCacheKey = "Glo_Destination_Key";

        /// <summary>
        /// 机构目的地集合
        /// </summary>
        /// <returns></returns>
        public List<DAL.Glo_Destination> GetDestinationList()
        {
            var user = AuthenticationPage.UserInfo;
            var key = DestinationCacheKey + "_" + user.OrgID;
            var list = BizCacheHelper.GloDestinaionCache.Get(key);
            if (list == null)
            {
                list = DAL.Glo_Destination.Find(x => x.OrgID == user.OrgID).OrderBy(x => x.OrderIndex).ToList();
                BizCacheHelper.GloDestinaionCache.Insert(key, list);
            }
            return list;
        }

        /// <summary>
        /// 目的地详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Glo_Destination Get(string keyID)
        {
            return DAL.Glo_Destination.SingleOrDefault(x => x.ID == keyID);
        }

        public bool Save(string keyID, string name, string routeTypeID, int orderIndex, string parentID = "")
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Glo_Destination();
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                    entity.CreateUserName = user.UserName;
                    entity.DeptID = user.DeptID;
                    entity.OrgID = user.OrgID;
                    entity.ParentID = string.IsNullOrEmpty(parentID) ? Guid.Empty.ToString() : parentID;
                }
                entity.Name = name;
                entity.RouteTypeID = routeTypeID;
                entity.OrderIndex = orderIndex;
                entity.ID = keyID;
                entity.Save();

                //加新加载缓存
                var key = DestinationCacheKey + "_" + user.OrgID;
                BizCacheHelper.GloDestinaionCache.Remove(key);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存目的地时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Glo_Destination.Delete(x => x.ID == keyID);
            //加新加载缓存
            var key = DestinationCacheKey + "_" + AuthenticationPage.UserInfo.OrgID;
            BizCacheHelper.GloDestinaionCache.Remove(key);
        }

        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public bool HasChildNode(string keyID)
        {
            return DAL.Glo_Destination.Exists(x => x.ParentID == keyID);
        }


        /// <summary>
        /// 获取目的地区域的完整路径名称
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        private string GetDestinationPath(List<DAL.Glo_Destination> list, string destinationID)
        {
            var arr = new List<string>();
            var e = list.Find(x => x.ID == destinationID);
            if (e == null)
                return "";
            else
            {
                arr.Add(e.Name);
                arr.Add(GetDestinationPath(list, e.ParentID));
            }
            return string.Join(",", arr);
        }


        /// <summary>
        /// 获取区域的完整路径名称
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public string GetDestinationPath(string destinationID)
        {
            var list = GetDestinationList();
            return GetDestinationPath(list, destinationID);
        }


        /// <summary>
        /// 获取目的地区域的完整路径ID
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        private string GetDestinationPathID(List<DAL.Glo_Destination> list, string destinationID)
        {
            var arr = new List<string>();
            var e = list.Find(x => x.ID == destinationID);
            if (e == null)
                return "";
            else
            {
                arr.Add(e.ID);
                arr.Add(GetDestinationPathID(list, e.ParentID));
            }
            return string.Join(",", arr);
        }


        /// <summary>
        /// 获取区域的完整路径ID
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public string GetDestinationPathID(string destinationID)
        {
            var list = GetDestinationList();
            return GetDestinationPathID(list, destinationID);
        }

        #region 生成TreeGrid的Json数据

        /// <summary>
        /// 导航菜单JSON数据结构
        /// </summary>
        /// <returns></returns>
        public string GetDestinationTreeJson(string routeTypeID)
        {
            var list = GetDestinationList();
            list = list.FindAll(x => x.RouteTypeID == routeTypeID);
            var s = CreateTreeNode(list, Guid.Empty.ToString());
            return "[" + s + "]";
        }

        /// <summary>
        /// 递归生成树节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        private string CreateTreeNode(List<DAL.Glo_Destination> list, string pId)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pId);
            var T = "\"ID\":\"{0}\",\"ParentID\":\"{1}\",\"Name\":\"{2}\",\"OrderIndex\":\"{3}\"";
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = T + ",\"children\":[{4}]";
                    var node = CreateTreeNode(list, e.ID);
                    str = string.Format(str, e.ID, e.ParentID, e.Name, e.OrderIndex, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    arr.Add("{" + string.Format(T, e.ID, e.ParentID, e.Name, e.OrderIndex) + "}");
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
        public string GetDestinationComboTree(string routeTypeID)
        {
            var list = GetDestinationList();
            list = list.FindAll(x => x.RouteTypeID == routeTypeID);
            var s = CreateComboTreeNode(list, Guid.Empty.ToString());
            return "[" + s + "]";
        }

        private string CreateComboTreeNode(List<DAL.Glo_Destination> list, string pid)
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
