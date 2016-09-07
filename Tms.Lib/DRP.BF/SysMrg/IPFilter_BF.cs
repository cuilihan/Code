using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Cached;
using DRP.DAL;
using DRP.BF.Cache;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 客户端IP过滤
    /// </summary>
    public class IPFilter_BF
    {

        private const string IPFilter = "Sys_IPFilter";

        /// <summary>
        /// 获取IP集合
        /// </summary>
        /// <returns></returns>
        private List<Sys_IPFilter> GetIpItems()
        {
            var list = BizCacheHelper.IpFilterCache.Get(IPFilter); 
            if (list == null)
            {
                list = Sys_IPFilter.All().ToList();
                BizCacheHelper.IpFilterCache.Insert(IPFilter, list); 
            }
            return list;
        }

        /// <summary>
        /// 获取机构IP集合
        /// </summary>
        /// <returns></returns>
        public List<Sys_IPFilter> GetOrgIPItem()
        {
            var list = GetIpItems();
            if (list != null && list.Count > 0)
                return list.FindAll(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).ToList();
            return list;
        }
    }
}
