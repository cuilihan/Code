using DRP.BF.Cache;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DRP.BF.GloMrg
{
    public class UpdateLog_BF
    {
        private List<DAL.Glo_UpdateLog> GetLog()
        {
            return DAL.Glo_UpdateLog.All().OrderByDescending(x => x.CreateDate).ToList();
        }

        private const string Om_UpateLog_Key = "Om_UpateLog_Key";

        /// <summary>
        /// 更新日志
        /// </summary>
        /// <returns></returns>
        public List<DAL.Glo_UpdateLog> GetUpdateLog()
        {
            var list = BizCacheHelper.UpdateLog.Get(Om_UpateLog_Key);
            if (list == null)
            {
                list = GetLog();
                BizCacheHelper.UpdateLog.Insert(Om_UpateLog_Key, list);
            }
            return list;
        }
         
         

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Glo_UpdateLog Get(string keyID)
        {
            return DAL.Glo_UpdateLog.SingleOrDefault(x => x.ID == keyID);
        }



        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Glo_UpdateLog();
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now; 
                }
                entity = new ReflectHelper<DAL.Glo_UpdateLog>().AssignEntity(entity, pnlControl); 
                entity.ID = keyID; 
                entity.Save();

                BizCacheHelper.UpdateLog.Remove(Om_UpateLog_Key); //缓存
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存更新日志时发生错误");
            }
            return isSuccess;
        }


        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            DAL.Glo_UpdateLog.Delete(x => x.ID == keyID); 
        }

    }
}
