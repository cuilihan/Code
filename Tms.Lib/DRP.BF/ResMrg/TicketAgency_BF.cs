using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.DAL;
using DRP.DAL.DataAccess;

namespace DRP.BF.ResMrg
{
    /// <summary>
    /// 票务机构
    /// </summary>
    public class TicketAgency_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 组合资源查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string QueryCondition(ResourceCriteria qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and Name like '%{0}%'", qry.Keyword);
            if (!string.IsNullOrEmpty(qry.TicketType))
                sb.AppendFormat(" and TicketType like '%{0}%'",qry.TicketType);
            return sb.ToString();
        }

        /// <summary>
        /// 机票机构
        /// </summary>
        /// <returns></returns>
        public List<DAL.Res_TicketAgency> GetTicketAgency()
        {
            return new ResourceDAL().QueryResource<DAL.Res_TicketAgency>("Res_TicketAgency", AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 酒店查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Res_TicketAgency> QueryData(ResourceCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Res_TicketAgency>("Res_TicketAgency", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 票务机构详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Res_TicketAgency Get(string keyID)
        {
            return DAL.Res_TicketAgency.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存票务机构
        /// </summary> 
        public bool Save(DAL.Res_TicketAgency entity, List<DAL.Res_BizContact> list)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region << 业务联系人 >>
                    new ResourceUtility().DeleteBizContact(entity.ID);

                    list.ForEach(x =>
                    {
                        x.Save();
                    });
                    #endregion

                    #region << 主表 >>
                    var t = Get(entity.ID);
                    if (t == null)
                    {
                        t = new Res_TicketAgency();
                        t.OrgID = user.OrgID;
                        t.DeptID = user.DeptID;
                        t.CreateUserID = user.UserID;
                        t.CreateUserName = user.UserName;
                        t.CreateDate = DateTime.Now;
                        t.TradeAdultNum = 0;
                        t.TradeChildNum = 0;
                        t.TradeAmt = 0;
                        t.TradeNum = 0;
                    }

                    t.Name = entity.Name;
                    t.TicketType = entity.TicketType;
                    t.Spell = entity.Spell;
                    t.Contact = entity.Contact; 
                    t.Mobile = entity.Mobile;
                    t.Phone = entity.Phone; 
                    t.Mail = entity.Mail;
                    t.QQ = entity.QQ;
                    t.Addr = entity.Addr;
                    t.Remark = entity.Remark;
                    t.IsEnable = entity.IsEnable; 
                    t.OrderIndex = entity.OrderIndex;
                    t.ID = entity.ID;
                    t.Save();
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存票务机构时发生错误");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="routeID"></param>
        /// <returns></returns>
        public bool Delete(List<string> listID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    listID.ForEach(x =>
                    {
                        new ResourceUtility().DeleteBizContact(x);
                        DAL.Res_TicketAgency.Delete(t => t.ID == x);
                    });
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除票务机构时发生错误");
                return false;
            }
        }
    }
}
