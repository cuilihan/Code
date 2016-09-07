using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DRP.DAL;

namespace DRP.BF.ProMrg
{
    /// <summary>
    /// 团次产品
    /// </summary>
    public class Product_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 目的地具有的团次数
        /// </summary>
        /// <returns></returns>
        public DataTable GetDestinationTourNumber()
        {
            return db.DRP_Pro_GetDestinationTour(AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 查询团次
        /// </summary> 
        /// <returns></returns>
        public DataTable QueryTour(TourCriteria qry, out int record)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (!string.IsNullOrEmpty(qry.TourName))
                sb.AppendFormat(" and TourName like '%{0}%'", qry.TourName);
            if (!string.IsNullOrEmpty(qry.RouteTypeID))
                sb.AppendFormat(" and RouteTypeID='{0}'", qry.RouteTypeID);
            if (!string.IsNullOrEmpty(qry.DestinationID))
                sb.AppendFormat(" and DestinationPath like '%{0}%'", qry.DestinationID);
            if (qry.TourDateScope != null)
            {
                if (!string.IsNullOrEmpty(qry.TourDateScope.sDate))
                {
                    sb.AppendFormat(" and TourDate>='{0}'", qry.TourDateScope.sDate);
                }
                if (!string.IsNullOrEmpty(qry.TourDateScope.eDate))
                {
                    sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.TourDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
            }
            if (qry.EffectiveDays)
                sb.Append(" and EffectiveDays>=0");

            return db.GetPagination("V_TourInfo", qry.pageIndex, qry.pageSize, out record, sb.ToString(), qry.SortExpress);
        }
    }
}
