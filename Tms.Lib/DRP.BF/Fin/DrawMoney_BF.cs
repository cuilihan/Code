using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DRP.DAL;
using DRP.DAL.DataAccess;

namespace DRP.BF.Fin
{
    /// <summary>
    /// 导游领款查询条件
    /// </summary>
    public class DrawMoneyCriteria : QueryCriteriaBase
    {
        public string OrderNo { get; set; }

        public string OrderName { get; set; }

        public DateScope TourDate { get; set; }

        public int DataStatus { get; set; }
    }


    /// <summary>
    /// 导游领款管理
    /// </summary>
    public class DrawMoney_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 导游领款查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string QueryCondition(DrawMoneyCriteria qry)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (qry.TourDate != null)
            {
                if (!string.IsNullOrEmpty(qry.TourDate.sDate))
                    sb.AppendFormat(" and TourDate>='{0}'", qry.TourDate.sDate);
                if (!string.IsNullOrEmpty(qry.TourDate.eDate))
                    sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.TourDate.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            }
            if (qry.DataStatus > 0)
                sb.AppendFormat(" and DataStatus='{0}'", (int)qry.DataStatus);
            if (!string.IsNullOrEmpty(qry.OrderNo))
                sb.AppendFormat(" and OrderNo like '%{0}%'", qry.OrderNo);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);          
            return sb.ToString();
        }

        /// <summary>
        /// 导游领款查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryData(DrawMoneyCriteria qry, out int record)
        {
            record = 0;
            var strWhere = QueryCondition(qry);
            var dt = db.GetPagination("V_Fin_DrawMoney", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
            return dt;
        }

        /// <summary>
        /// 更新导游领款状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(string id, int status)
        {
            try
            {
                new DrawMoneyDAL().UpdateStatus(id, status);
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "更新导游领款状态时发生错误");
                return false;
            }
        }
    }
}
