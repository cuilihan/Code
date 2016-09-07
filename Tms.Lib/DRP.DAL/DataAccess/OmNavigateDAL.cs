using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 子系统的导航菜单（按机构划分配出来的菜单）
    /// </summary>
    public class OmNavigate
    {
        /// <summary>
        /// 机构具体的导航菜单集合
        /// </summary>
        /// <param name="navGroupID"></param>
        /// <returns></returns>
        public List<DAL.Om_Navigate> GetOrgNavigate(string navGroupID)
        {
            var sql = "SELECT b.ID,b.PageID,b.ParentID,b.NavName,b.NavUrl,b.NavCls,b.NavIcon,b.OrderIndex,b.OrderIndex FROM dbo.Om_NavGroupRelation a INNER JOIN dbo.Om_Navigate b ON a.NavID=b.ID WHERE a.GroupID=@GroupID and IsVisual=@IsVisual order by b.OrderIndex asc";
            return new SubSonic.Query.CodingHorror(sql, navGroupID, true).ExecuteTypedList<DAL.Om_Navigate>();
        }
    }
}
