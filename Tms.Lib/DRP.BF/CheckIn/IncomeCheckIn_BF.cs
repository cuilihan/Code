using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.Framework.Core;

namespace DRP.BF.CheckIn
{
    /// <summary>
    /// 非订单收入登记查询条件
    /// </summary>
    public class IncomeCheckInCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 收入类型
        /// </summary>
        public string IncomeTypeID { get; set; }

        /// <summary>
        /// 收入日期
        /// </summary>
        public DateScope IncomeDate { get; set; }

        /// <summary>
        /// 收入部门ID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string Operator { get; set; }
    }

    /// <summary>
    /// 收入登记
    /// </summary>
    public class IncomeCheckIn_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string QueryCondition(IncomeCheckInCriteria qry)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'",AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(qry.IncomeTypeID))
                sb.AppendFormat(" and IncomeTypeID='{0}'",qry.IncomeTypeID);
            if (!string.IsNullOrEmpty(qry.DeptID))
                sb.AppendFormat(" and DeptID='{0}'",qry.DeptID);
            if (qry.IncomeDate != null)
            {
                if (!string.IsNullOrEmpty(qry.IncomeDate.sDate))
                    sb.AppendFormat(" and IncomeDate>='{0}'",qry.IncomeDate.sDate);
                if (!string.IsNullOrEmpty(qry.IncomeDate.eDate))
                    sb.AppendFormat(" and IncomeDate<'{0}'",Convert.ToDateTime(qry.IncomeDate.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(qry.Operator))
                sb.AppendFormat(" and Operator like '%{0}%'",qry.Operator);
            return sb.ToString();
        }

        /// <summary>
        /// 收入查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Fin_IncomeCheckIn> QueryData(IncomeCheckInCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Fin_IncomeCheckIn>("Fin_IncomeCheckIn", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 收入详情
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public Fin_IncomeCheckIn Get(string keyID)
        {
            return DAL.Fin_IncomeCheckIn.SingleOrDefault(x => x.ID==keyID);
        }


        /// <summary>
        /// 保存收入
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl, string deptName,string incomeType)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Fin_IncomeCheckIn();
                    entity.OrgID = user.OrgID;
                    entity.CreateUserID = user.UserID;
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                }
                entity = new ReflectHelper<DAL.Fin_IncomeCheckIn>().AssignEntity(entity, pnlControl);
                entity.DeptName = deptName;
                entity.IncomeType = incomeType;
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "非订单收入登记时发生错误");
            }
            return isSuccess;
        }

        public bool Delete(List<string> listID)
        {
            return new DAL.Fin_IncomeCheckIn().MultiDelete(listID);
        }

        #region 统计报表

        /// <summary>
        /// 按收入类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable IncomeDataStatisticByIncomeType(string sDate, string eDate)
        {
            return new IncomeCheckInDAL().IncomeCheckInStatisticByIncomeType(sDate, eDate, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 按收入部门统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable IncomeDataStatisticByDept(string sDate, string eDate)
        {
            return new IncomeCheckInDAL().IncomeCheckInStatisticByDept(sDate, eDate, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 按月份统计非订单收入登记金额
        /// </summary>
        /// <param name="year"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataTable IncomeCheckInStatisticByMonth(int year, string deptID)
        {
            return new IncomeCheckInDAL().IncomeCheckInStatisticByMonth(year, deptID, AuthenticationPage.UserInfo.OrgID);
        }

        #endregion
    }
}
