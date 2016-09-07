using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DRP.DAL.DataAccess;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 机构用户统计
    /// </summary>
    public class OmUserRpt_BF
    {
        /// <summary>
        /// 机构数据统计
        /// </summary>
        /// <returns></returns>
        public DataTable OrgStatistic()
        {
            return new OmRptDAL().OrgStatistic();
        }
    }
}
