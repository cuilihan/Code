using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public class OmNavGroupDAL
    {
        /// <summary>
        /// 根据导航组ID查询导航菜单
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public DataTable QueryNavigateByGroupID(string groupID)
        { 
            var sql = "SELECT a.ID,a.ParentID,a.NavName,b.NavID FROM Om_Navigate a LEFT JOIN (SELECT NavID FROM Om_NavGroupRelation WHERE GroupID=@GroupID) as b ON a.ID = b.NavID";
            sql = string.Format(sql, groupID);
            return new SubSonic.Query.CodingHorror(sql, groupID).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 查询导航组具有的菜单
        /// </summary>
        /// <param name="navGroupID"></param>
        /// <returns></returns>
        public List<DAL.Om_Navigate> GetNavigateByNavGroupID(string navGroupID)
        {
            var sql = "SELECT b.* FROM dbo.Om_NavGroupRelation a INNER JOIN dbo.Om_Navigate b ON a.NavID=b.ID WHERE a.GroupID=@navGroupID order by b.OrderIndex asc";
            return new SubSonic.Query.CodingHorror(sql, navGroupID).ExecuteTypedList<DAL.Om_Navigate>();
        }
    }
}
