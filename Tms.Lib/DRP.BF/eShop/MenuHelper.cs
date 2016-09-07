using DRP.BF.Glo;
using DRP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.eShop
{
    /// <summary>
    /// 微网站菜单 
    /// </summary>
    public class MenuHelper
    {
        /// <summary>
        /// 微网站菜单集合
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<DAL.Sn_MenuItem> GetMenuItem(string orgID)
        {
            return DAL.Sn_MenuItem.Find(x => x.OrgID == orgID && x.CtrlChecked == 1).OrderBy(x => x.SortIndex).ToList();
        }

        /// <summary>
        /// 线路类型
        /// </summary>
        /// <param name="basicType"></param>
        /// <returns></returns>
        public List<Glo_BasicInfo> GetRouteType(string orgID)
        {
            return DAL.Glo_BasicInfo.Find(x => x.BasicType == (int)BasicType.Pro_RouteType && x.OrgID == orgID)
                .OrderBy(x => x.OrderIndex).ToList();
        }
    }
}
