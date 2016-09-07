using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml;
using DRP.BF.ResMrg;
using DRP.DAL;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.BF.Fin
{
    /// <summary>
    /// 订单应付款查询条件
    /// </summary>
    public class PayableCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 供应商类型
        /// </summary>
        public ResourceType SupplierType { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 应付款金额
        /// </summary>
        public decimal PayableAmt { get; set; }
    }

    /// <summary>
    /// 供应商付款明细项目
    /// </summary>
    public class PayableItemCriterial : QueryCriteriaBase
    {
        public string SupplierID { get; set; }

        public string sDate { get; set; }

        public string eDate { get; set; }

        public string OrderName { get; set; }

        public string UserName { get; set; }

        public string OrderNo { get; set; }

        public string DeptID { get; set; }
    }

    /// <summary>
    /// 订单应付款
    /// </summary>
    public class OrderPayable_BF
    {
        DRPDB db = new DRPDB();

        #region 付款查询

        /// <summary>
        /// 供应商付款查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string QueryCondition(PayableCriteria qry)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);

            if (!string.IsNullOrEmpty(qry.SupplierName))
                sb.AppendFormat(" and Name like '%{0}%'", qry.SupplierName);
            if (qry.SupplierType != ResourceType.All)
                sb.AppendFormat(" and xType='{0}'", (int)qry.SupplierType);
            if (qry.PayableAmt > 0)
                sb.AppendFormat(" and CostAmt='{0}'", qry.PayableAmt);
            return sb.ToString();
        }

        public bool PaidDelete(string id)
        {
            try
            {
                DRP.DAL.Fin_OrderPayable.Delete(x => x.ID == id);
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }

        /// <summary>
        /// 供应商付款汇总
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryPayableStatisticData(PayableCriteria qry, out int record)
        {
            record = 0;
            var strWhere = QueryCondition(qry);
            var dt = db.GetPagination("V_Fin_PayableStatistic", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
            return dt;
        }

        /// <summary>
        /// 供应商待付款明细
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryPayableItemData(PayableItemCriterial qry, out int record)
        {
            record = 0;
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}' and SupplierID='{1}' and UnPayAmt <> 0", user.OrgID, qry.SupplierID);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.UserName) && qry.UserName != "所有")
                sb.AppendFormat(" and CreateUserName like '%{0}%'", qry.UserName);
            if (!string.IsNullOrEmpty(qry.OrderNo))
                sb.AppendFormat(" and OrderNo like '%{0}%'", qry.OrderNo);
            if (!string.IsNullOrEmpty(qry.DeptID))
                sb.AppendFormat(" and DeptID = '{0}'", qry.DeptID);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and TourDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            var strWhere = sb.ToString();
            return db.GetPagination("V_Fin_SupplierPayableItem", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        /// <summary>
        /// 供应商已付款明细
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryPaidItemData(PayableItemCriterial qry, out int record)
        {
            record = 0;
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}' and SupplierID='{1}'", user.OrgID, qry.SupplierID);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and PayDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and PayDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            var strWhere = sb.ToString();
            return db.GetPagination("V_Fin_SupplierPaidItem", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        /// <summary>
        /// 供应商所有付款明细
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QuerySupplierPayableItem(PayableItemCriterial qry, out int record)
        {
            record = 0;
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}' and SupplierID='{1}'", user.OrgID, qry.SupplierID);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and TourDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            var strWhere = sb.ToString();
            return db.GetPagination("V_Fin_SupplierPayableItem", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }
        #endregion

        #region 付款操作
        /// <summary>
        /// 保存付款信息
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public bool SavePayable(string supplierID, string supplierName, string xmlData)
        {
            if (string.IsNullOrEmpty(xmlData))
                return false;
            var user = AuthenticationPage.UserInfo;
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlData);
                var nodes = doc.SelectNodes("document/data");
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (XmlNode node in nodes)
                    {
                        #region 付款主表
                        var pAmt = node.GetNodeValue("PayAmt").ToDecimal();
                        if (pAmt == 0) continue;

                        var e = new DAL.Fin_OrderPayable();
                        e.ID = Guid.NewGuid().ToString();
                        e.OrderCostItemID = node.GetNodeValue("OrderCostID");
                        e.SupplierID = supplierID;
                        e.SupplierName = supplierName;
                        e.Amount = node.GetNodeValue("PayAmt").ToDecimal();
                        e.PayDate = Convert.ToDateTime(node.GetNodeValue("PayDate"));
                        e.Comment = node.GetNodeValue("Comment");
                        e.CreateDate = DateTime.Now;
                        e.CreateUserID = user.UserID;
                        e.CreateUserName = user.UserName;
                        e.OrgID = user.OrgID;
                        e.OrderID = node.GetNodeValue("OrderID");
                        e.DataStatus = 1;
                        e.DeptID = user.DeptID;
                        e.Save();

                        #endregion
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存付款时发生错误");
                return false;
            }
        }
        #endregion

        public DataTable QueryOrder_All(PayableItemCriterial qry)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}' and SupplierID='{1}' and UnPayAmt <> 0", user.OrgID, qry.SupplierID);
            if (!string.IsNullOrEmpty(qry.OrderName))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.OrderName);
            if (!string.IsNullOrEmpty(qry.UserName) && qry.UserName != "所有")
                sb.AppendFormat(" and CreateUserName like '%{0}%'", qry.UserName);
            if (!string.IsNullOrEmpty(qry.OrderNo))
                sb.AppendFormat(" and OrderNo like '%{0}%'", qry.OrderNo);
            if (!string.IsNullOrEmpty(qry.DeptID))
                sb.AppendFormat(" and DeptID = '{0}'", qry.DeptID);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and TourDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            var strWhere = sb.ToString();
            var sql = string.Format("select * from V_Fin_SupplierPayableItem where {0}", strWhere);
            
            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }
    }
}
