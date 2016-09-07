using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public class WechatUser_BF
    {
        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pwd"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public V_UserInfo_AllEntity GetUserInfo(string userID, string pwd, string orgID)
        {
            return new UserInfoDAL().Get(userID,pwd,orgID); 
        }
    }
}
