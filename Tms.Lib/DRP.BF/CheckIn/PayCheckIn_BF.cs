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
    /// 非订单支出登记查询条件
    /// </summary>
    public class PayCheckInCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 支出类型
        /// </summary>
        public string PayTypeID { get; set; }

        /// <summary>
        /// 支出日期
        /// </summary>
        public DateScope PayDate { get; set; }

        /// <summary>
        /// 支出部门ID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string Operator { get; set; }
    }

    /// <summary>
    /// 支出登记
    /// </summary>
    public class PayCheckIn_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string QueryCondition(PayCheckInCriteria qry)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'", AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(qry.PayTypeID))
                sb.AppendFormat(" and PayTypeID='{0}'", qry.PayTypeID);
            if (!string.IsNullOrEmpty(qry.DeptID))
                sb.AppendFormat(" and DeptID='{0}'", qry.DeptID);
            if (qry.PayDate != null)
            {
                if (!string.IsNullOrEmpty(qry.PayDate.sDate))
                    sb.AppendFormat(" and PayDate>='{0}'", qry.PayDate.sDate);
                if (!string.IsNullOrEmpty(qry.PayDate.eDate))
                    sb.AppendFormat(" and PayDate<'{0}'", Convert.ToDateTime(qry.PayDate.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(qry.Operator))
                sb.AppendFormat(" and Operator like '%{0}%'", qry.Operator);
            return sb.ToString();
        }

        /// <summary>
        /// 支出查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Fin_PayCheckIn> QueryData(PayCheckInCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Fin_PayCheckIn>("Fin_PayCheckIn", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 支出详情
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public Fin_PayCheckIn Get(string keyID)
        {
            return DAL.Fin_PayCheckIn.SingleOrDefault(x => x.ID == keyID);
        }


        /// <summary>
        /// 保存支出
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl, string deptName, string payType)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Fin_PayCheckIn();
                    entity.OrgID = user.OrgID;
                    entity.CreateUserID = user.UserID;
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                }
                entity = new ReflectHelper<DAL.Fin_PayCheckIn>().AssignEntity(entity, pnlControl);
                entity.DeptName = deptName;
                entity.PayType = payType;
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "非订单支出登记时发生错误");
            }
            return isSuccess;
        }

        public bool Delete(List<string> listID)
        {
            return new DAL.Fin_PayCheckIn().MultiDelete(listID);
        }

        #region 统计报表

        /// <summary>
        /// 按支出类型统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable PayDataStatisticByPayType(string sDate, string eDate)
        {
            return new PayCheckInDAL().PayCheckInStatisticByPayType(sDate, eDate, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 按支出部门统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable PayDataStatisticByDept(string sDate, string eDate)
        {
            return new PayCheckInDAL().PayCheckInStatisticByDept(sDate, eDate, AuthenticationPage.UserInfo.OrgID);
        }


        /// <summary>
        /// 按月份统计非订单支登记金额
        /// </summary>
        /// <param name="year"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataTable PayCheckInStatisticByMonth(int year, string deptID)
        {
            return new PayCheckInDAL().PayCheckInStatisticByMonth(year, deptID, AuthenticationPage.UserInfo.OrgID);
        }
        #endregion
    }
}
