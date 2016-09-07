using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.eShop
{
    /// <summary>
    /// 微站配置数据
    /// </summary>
    public class eShopConfig
    {
        /// <summary>
        /// 微站配置数据
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DAL.Sn_BasicInfo Get(string orgID)
        {
            return DAL.Sn_BasicInfo.SingleOrDefault(x => x.OrgID == orgID);
        }

        /// <summary>
        /// 广告
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<DAL.Sn_AdSlide> GetAd(string orgID)
        {
            return DAL.Sn_AdSlide.Find(x => x.OrgID == orgID).ToList();
        }
    }
}
