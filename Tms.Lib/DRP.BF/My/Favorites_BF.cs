using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.Framework.Core;

namespace DRP.BF.My
{
    /// <summary>
    /// 用户个人常用链接
    /// </summary>
    public class Favorites_BF
    { 

        /// <summary>
        /// 常用链接
        /// </summary>
        /// <returns></returns>
        public List<DAL.User_Favorite> GetUserFavorite()
        {
            return User_Favorite.Find(x => x.UserID == AuthenticationPage.UserInfo.UserID).OrderBy(x=>x.OrderIndex).ToList();
        }

        /// <summary>
        /// 常用链接
        /// </summary>
        /// <returns></returns>
        public List<DAL.User_Favorite> GetUserFavorite(int iCount)
        {
            var sql = string.Format("select top {0} * from User_Favorites where UserID=@userID", iCount);
            return new SubSonic.Query.CodingHorror(sql, AuthenticationPage.UserInfo.UserID).ExecuteTypedList<DAL.User_Favorite>(); 
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.User_Favorite Get(string keyID)
        {
            return DAL.User_Favorite.SingleOrDefault(x => x.ID == keyID);
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
                    entity = new DAL.User_Favorite();
                    entity.UserID = user.UserID;
                    entity.OrgID = user.OrgID;
                }
                entity = new ReflectHelper<DAL.User_Favorite>().AssignEntity(entity, pnlControl);
                entity.ID = keyID;
                entity.Save();
                 
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "设置链接时发生错误");
            }
            return isSuccess;
        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            DAL.User_Favorite.Delete(x => x.ID == keyID); 
        } 
    }
}
