using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.BF.SysMrg;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 系统监控数据
    /// </summary>
    public class OmScan_BF
    {
        /// <summary>
        /// 记录用户的登录次数
        /// </summary>
        public static void LoginTrace(string userID, string orgID)
        {
            try
            {
                var user = new User_BF().Get(userID);
                var org = new OrgInfo_BF().Get(orgID);
                if (user != null && org != null)
                {
                    var e = new DAL.Om_UserOnLine();
                    e.ID = Guid.NewGuid().ToString();
                    e.OrgID = org.ID;
                    e.OrgName = org.Name;
                    e.UserID = user.ID;
                    e.UserName = user.Name;
                    e.CreateDate = DateTime.Now;
                    e.Save();
                }
            }
            catch { }
        }
    }
}
