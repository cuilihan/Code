using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.BF.Cache;
using DRP.DAL;
using DRP.Framework.Core;

namespace DRP.BF.Glo
{
    /// <summary>
    /// 出发地管理
    /// </summary>
    public class Departure_BF
    { 
        /// <summary>
        /// 机构出发地集合
        /// </summary>
        /// <returns></returns>
        public List<DAL.Glo_Departure> GetDeparture()
        {
            return DAL.Glo_Departure.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).OrderBy(x => x.OrderIndex).ToList(); 
        }
          

        /// <summary>
        /// 出发地详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Glo_Departure Get(string keyID)
        {
            return DAL.Glo_Departure.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存机构
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, string name,int orderIndex)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Glo_Departure();
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                    entity.OrgID = user.OrgID;
                    entity.DeptID = user.DeptID;
                }
                entity.Name = name;
                entity.OrderIndex = orderIndex;
                entity.ID = keyID;   
                entity.Save(); 
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存出发地时发生错误");
            }
            return isSuccess;
        }
         

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Glo_Departure.Delete(x => x.ID == keyID); 
        }
         
    }
}

