using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;

namespace DRP.BF.WeChat
{
    public class WeChatIndex_BF
    {
        #region << 首页数据统计 >>
        /// <summary>
        /// 根据各家不同的获取数据
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DAL.Rpt_BizStatistic GetEntity()
        {
            var orgID = AuthenticationPage.UserInfo.OrgID;
            return Rpt_BizStatistic.SingleOrDefault(x => x.OrgID == orgID);
        }
        #endregion
    }
}
