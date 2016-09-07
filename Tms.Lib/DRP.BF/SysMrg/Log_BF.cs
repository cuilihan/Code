using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 日志查询条件
    /// </summary>
    public class LogCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 日志查询区间
        /// </summary>
        public string sDate { get; set; }

        /// <summary>
        /// 日志查询区间
        /// </summary>
        public string eDate { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public string LogerLv { get; set; }
    }

    /// <summary>
    /// 日志查询类
    /// </summary>
    public class Log_BF
    {
        DRPDB db = new DRPDB();

        public Sys_Log Get(string keyID)
        {
            return Sys_Log.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 系统用户列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Sys_Log> GetSysLog(LogCriteria qry, out int record)
        {
            var strWhere = string.Format("1=1 and OrgID='{0}'", AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(qry.sDate))
                strWhere += string.Format(" and LogDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                strWhere += string.Format(" and LogDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1));
            if (!string.IsNullOrEmpty(qry.LogerLv))
                strWhere += string.Format(" and LogType like '%{0}%'", qry.LogerLv);
            return db.GetPaginationList<DAL.Sys_Log>("Sys_Log", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="listID"></param>
        public bool DeleteLog(List<string> listID)
        {
            return new Sys_Log().MultiDelete(listID);
        }
    }
}
