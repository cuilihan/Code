using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.UI.WebControls;
using DRP.BF.Crm;
using DRP.DAL;
using DRP.Framework.Core;

namespace DRP.BF.CrmMrg
{
    /// <summary>
    /// 客户销售线索
    /// </summary>
    public class CustomerTrace_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 销售线索详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Crm_VisitTrace Get(string keyID)
        {
            return DAL.Crm_VisitTrace.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 客户销售线索列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Crm_VisitTrace> QueryData(QueryCriteriaBase qry, out int record)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and CustomerID='{0}'", qry.Keyword); 
            return db.GetPaginationList<DAL.Crm_VisitTrace>("Crm_VisitTrace", qry.pageIndex, qry.pageSize, out record, sb.ToString(), qry.SortExpress);
        }

        /// <summary>
        /// 保存销售线索
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, string customerID, WebControl pnlControl)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var entity = Get(keyID);
                    if (entity == null)
                    {
                        entity = new DAL.Crm_VisitTrace();
                        entity.OrgID = user.OrgID;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CreateDate = DateTime.Now;
                        entity.CustomerID = customerID;
                    }
                    entity = new ReflectHelper<DAL.Crm_VisitTrace>().AssignEntity(entity, pnlControl);
                    entity.ID = keyID;
                    entity.Save();
                    //更新客户主表的沟通次数
                    var customer = new Customer_BF().Get(customerID);
                    if (customer != null)
                    {
                        customer.CommunicateNum = customer.CommunicateNum + 1;
                        customer.ID = customerID;
                        customer.Save();
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存机构管理员时发生错误");
            }
            return isSuccess;
        }


        /// <summary>
        /// 客户销售销售线索
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<DAL.Crm_VisitTrace> GetCusotmerTrace(string customerID)
        {
            return DAL.Crm_VisitTrace.Find(x => x.CustomerID == customerID).OrderByDescending(x => x.CreateDate).ToList();
        }

        /// <summary>
        /// 删除销售线索
        /// </summary>
        /// <param name="traceID"></param>
        public void DeleteTrace(string traceID)
        {
            var e = DAL.Crm_VisitTrace.SingleOrDefault(x => x.ID == traceID);
            if (e != null)
            {
                var customerID = e.CustomerID;
                e.Delete();
                //更新销售线索次数
                var sql = "Update Crm_Customer Set CommunicateNum=CommunicateNum-1 where ID=@ID";
                new SubSonic.Query.CodingHorror(sql, customerID).Execute();
            }
        }
    }
}
