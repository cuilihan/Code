using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.BF.Cache;
using DRP.Framework.Core;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 工具栏目设置
    /// </summary>
    public class ToolSetting_BF
    {
        private const string DRP_OM_Tools = "Om_PushTools";

        /// <summary>
        /// 工具集合
        /// </summary>
        /// <returns></returns>
        public List<DAL.Om_Tool> GetOmTools()
        {
            var list = BizCacheHelper.OrgToolsCache.Get(DRP_OM_Tools);
            if (list == null)
            {
                list= DAL.Om_Tool.All().OrderBy(x => x.OrderIndex).ToList();
                BizCacheHelper.OrgToolsCache.Insert(DRP_OM_Tools, list);  
            }
            return list;
        }
          
        /// <summary>
        /// 工具详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Om_Tool Get(string keyID)
        {
            return DAL.Om_Tool.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存机构
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
                    entity = new DAL.Om_Tool(); 
                }
                entity = new ReflectHelper<DAL.Om_Tool>().AssignEntity(entity, pnlControl);
                entity.ID = keyID;
                entity.Save();

                BizCacheHelper.OrgToolsCache.Remove(DRP_OM_Tools);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存工具栏目时发生错误");
            }
            return isSuccess;
        }

      

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            DAL.Om_Tool.Delete(x => x.ID == keyID);
            BizCacheHelper.OrgToolsCache.Remove(DRP_OM_Tools);
        } 
    }
}
