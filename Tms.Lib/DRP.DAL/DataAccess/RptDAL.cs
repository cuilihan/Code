using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 统计报表
    /// </summary>
    public class RptDAL
    {
        #region 订单收支明细

        /// <summary>
        /// 订单收支明细
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable OrderSheetItem(string condition)
        {
            var sql = string.Format("select * from [Rpt_OrderSheet] where {0} order by tourdate desc", condition);
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        #endregion
    }
}
