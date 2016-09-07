using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.BF.eShop
{
    /// <summary>
    /// 微网站会员管理
    /// </summary>
    public class eShopMemeber
    {
        #region 注册会员

        /// <summary>
        /// 手机号是否被注册
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public bool Exist(string mobile, string orgID)
        {
            return DAL.Sn_Memeber.Exists(x => x.Mobile == mobile && x.OrgID == orgID);
        }

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool RegMemeber(string mobile, string orgID)
        {
            try
            {
                var e = new DAL.Sn_Memeber();
                e.ID = Guid.NewGuid().ToString();
                e.OrgID = orgID;
                e.UserPwd = Security.EncrypByRijndael("123456");
                e.UserAcct = mobile;
                e.Mobile = mobile;
                e.CreateDate = DateTime.Now;
                e.LastActiveDate = DateTime.Now;
                e.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 会员登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pwd"></param>
        /// <param name="orgID"></param>
        /// <returns>null：登录失败</returns>
        public DAL.Sn_Memeber Login(string mobile, string pwd, string orgID)
        {
            pwd = Security.EncrypByRijndael(pwd);
            return DAL.Sn_Memeber.SingleOrDefault(x => x.OrgID == orgID && x.Mobile == mobile && x.UserPwd == pwd);
        }

        /// <summary>
        /// 登录用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DAL.Sn_Memeber Get(string userID)
        {
            return DAL.Sn_Memeber.SingleOrDefault(x => x.ID == userID);
        }

        #endregion

        #region 会员管理

        /// <summary>
        /// 更新资料
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool UpdateInfo(DAL.Sn_Memeber e)
        {
            try
            {
                var entity = Get(e.ID);
                entity.UserName = e.UserName;
                entity.NickName = e.NickName;
                entity.IDNo = e.IDNo;
                entity.Mobile = e.Mobile;
                entity.ID = e.ID;
                entity.Save();

                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool UpdatePhoto(string photo)
        {
            try
            {
                var entity = Get(mPageBase.UserInfo.UserID);
                if (entity == null) return false;
                entity.Photo = photo;
                entity.Save();
                
                mPageBase.RemoveUserKey(entity.ID);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
