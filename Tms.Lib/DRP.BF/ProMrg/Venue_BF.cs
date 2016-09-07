using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.Framework.Core;

namespace DRP.BF.ProMrg
{
    /// <summary>
    /// 集合地点设置
    /// </summary>
    public class Venue_BF
    {
        /// <summary>
        /// 集合地点列表
        /// </summary>
        /// <returns></returns>
        public List<DAL.Pro_Venue> GetVenue(string routeTypeID)
        {
            if (string.IsNullOrEmpty(routeTypeID))
                return DAL.Pro_Venue.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).OrderBy(x => x.OrderIndex).ToList();
            else
                return DAL.Pro_Venue.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID
                    && (x.RouteTypeID.Contains(routeTypeID)))
                    .OrderBy(x => x.OrderIndex).ToList();
        }


        /// <summary>
        /// 集合地点设置
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Pro_Venue Get(string keyID)
        {
            return DAL.Pro_Venue.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存集合地点
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl, string departureName, string routeTypeName)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Pro_Venue();
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                    entity.OrgID = user.OrgID;
                    entity.DeptID = user.DeptID;
                }
                entity = new ReflectHelper<DAL.Pro_Venue>().AssignEntity(entity, pnlControl);
                entity.Departure = departureName;
                entity.RouteType = routeTypeName;
                entity.ID = keyID;

                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存集合地点时发生错误");
            }
            return isSuccess;
        }


        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(List<string> listIDs)
        {
            new Pro_Venue().MultiDelete(listIDs);
        }
    }
}
