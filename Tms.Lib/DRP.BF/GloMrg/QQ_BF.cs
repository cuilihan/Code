using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.BF.Cache;
using DRP.DAL;

namespace DRP.BF.GloMrg
{
    /// <summary>
    /// 在线QQ
    /// </summary>
    public class QQ_BF
    {
        private const string CacheKey = "TMS_QQ_Key";

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Glo_QQ Get(string keyID)
        {
            return DAL.Glo_QQ.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 在线QQ客服列表
        /// </summary>
        /// <returns></returns>
        public List<DAL.Glo_QQ> GetData()
        {
            var list = BizCacheHelper.GloQQCache.Get(CacheKey);
            if (list == null)
            {
                list = DAL.Glo_QQ.All().OrderBy(x => x.OrderIndex).ToList();
                BizCacheHelper.GloQQCache.Insert(CacheKey, list);
            }
            return list;
        }

        /// <summary>
        /// 保存
        /// </summary> 
        /// <returns></returns>
        public bool Save(string keyID, string qq, string name,int index, string comment)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var e = Get(keyID);
                if (e == null)
                {
                    e = new DAL.Glo_QQ();
                    e.CreateDate = DateTime.Now;
                }
                e.Name = name;
                e.QQ = qq;
                e.Comment = comment;
                e.OrderIndex = index;
                e.ID = keyID;
                e.Save();

                BizCacheHelper.GloQQCache.Remove(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存QQ时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public bool Delete(List<string> listID)
        {
            var isOk = new Glo_QQ().MultiDelete(listID);
            if (isOk)
                BizCacheHelper.GloQQCache.Remove(CacheKey);
            return isOk;
        }
    }
}
