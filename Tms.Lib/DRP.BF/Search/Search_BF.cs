using DRP.BF.Order;
using DRP.BF.ResMrg;
using DRP.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.BF.Search
{

    /// <summary>
    /// 查询
    /// </summary>
    public class Search_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 资源查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="resType"></param>
        /// <returns></returns>
        public DataTable SearchResource(QueryCriteriaBase qry, ResourceType resType, out int totalRows)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and Name like '%{0}%'", qry.Keyword);
            if (resType != ResourceType.All)
                sb.AppendFormat(" and xType={0}", (int)resType);
            return db.GetPagination("V_Res_Resource", qry.pageIndex, qry.pageSize, out totalRows, sb.ToString(), qry.SortExpress);
        }

        /// <summary>
        /// 组合客户查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string CustomerQueryCondition(QueryCriteriaBase qry, string itemType)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);

            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion

            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and (Name like '%{0}%' or Mobile like '%{0}%')", qry.Keyword);

            if (!string.IsNullOrEmpty(itemType))
                sb.AppendFormat(" and CustomerType like '%{0}%'", itemType);
            return sb.ToString();
        }

        /// <summary>
        /// 客户列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Crm_Customer> SearchCustomer(QueryCriteriaBase qry, string itemType, out int record)
        {
            return db.GetPaginationList<DAL.Crm_Customer>("Crm_Customer", qry.pageIndex, qry.pageSize, out record, CustomerQueryCondition(qry, itemType), qry.SortExpress);
        }

        #region << 订单查询 >>

        /// <summary>
        /// 组合客户查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string RouteQueryCondition(QueryCriteriaBase qry, string tourDate, OrderType ordType)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);

            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    if (!string.IsNullOrEmpty(user.PartDeptID))
                        sb.AppendFormat(" and (DeptID='{0}' or DeptID='{1}')", user.DeptID, user.PartDeptID);
                    else
                        sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion

            #region 线路类型权限
            if (user.RouteTypePermission.Count > 0 && ordType != OrderType.DXYW)
            {
                if (user.RouteTypePermission.Count == 1)
                    sb.AppendFormat(" and RouteTypeID='{0}'", user.RouteTypePermission[0]);
                else
                {
                    var rArr = new List<string>();
                    user.RouteTypePermission.ForEach(p =>
                    {
                        rArr.Add(string.Format("'{0}'", p));
                    });
                    sb.AppendFormat(" and RouteTypeID in ({0})", string.Join(",", rArr));
                }
            }
            #endregion

            if (ordType != OrderType.All)
                sb.AppendFormat(" and OrderType={0}", (int)ordType);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and (OrderName like '%{0}%' or OrderNo like '%{0}%')", qry.Keyword);
            if (!string.IsNullOrEmpty(tourDate))
            {
                sb.AppendFormat(" and TourDate='{0}'", tourDate);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderInfo> QueryOrder(QueryCriteriaBase qry, string tourDate, OrderType ordType, out int record)
        {
            return db.GetPaginationList<DAL.Ord_OrderInfo>("Ord_OrderInfo", qry.pageIndex, qry.pageSize, out record, RouteQueryCondition(qry, tourDate, ordType), qry.SortExpress);
        }

        #endregion
    }
}
