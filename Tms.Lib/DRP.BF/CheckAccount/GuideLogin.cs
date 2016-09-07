using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL.DataAccess;

namespace DRP.BF.CheckAccount
{
    /// <summary>
    /// 导游报账登录
    /// </summary>
    public class GuideLogin
    {
        /// <summary>
        /// 导游报账登录
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pwd"></param>
        /// <param name="orgId"></param>
        /// <returns>0：登录失败 1：只有一个报账订单  2：多个报账订单</returns>
        public int Login(string userID, string pwd, string orgId)
        {
            return new GuideCheckAccountDAL().Login(userID, pwd, orgId);
        }
    }
}
