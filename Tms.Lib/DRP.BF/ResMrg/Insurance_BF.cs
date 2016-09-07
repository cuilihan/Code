using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.DAL;

namespace DRP.BF.ResMrg
{
    /// <summary>
    /// 保险公司管理
    /// </summary>
    public class Insurance_BF
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
            return sb.ToString();
        }

        /// <summary>
        /// 保险机构查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Res_Insurance> QueryData(ResourceCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Res_Insurance>("Res_Insurance", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 保险机构详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Res_Insurance Get(string keyID)
        {
            return DAL.Res_Insurance.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存签证机构
        /// </summary> 
        public bool Save(DAL.Res_Insurance entity, List<DAL.Res_BizContact> list)
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

                    #region << 地接社主表 >>
                    var t = Get(entity.ID);
                    if (t == null)
                    {
                        t = new Res_Insurance();
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
                    t.Spell = entity.Spell;
                    t.Contact = entity.Contact;
                    t.Mobile = entity.Mobile;
                    t.Phone = entity.Phone;
                    t.Mail = entity.Mail;
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
                BizUtility.ExceptionHandler(user, ex, "保存保险机构时发生错误");
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
                        DAL.Res_Insurance.Delete(t => t.ID == x);
                    });
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除保险机构时发生错误");
                return false;
            }
        }
    }
}
