using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.DAL;

namespace DRP.BF.ResMrg
{
    /// <summary>
    /// 车队规模
    /// </summary>
    public class Motorcade_BF
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
            if (!string.IsNullOrEmpty(qry.DepartureID))
                sb.AppendFormat(" and DepartureID='{0}'", qry.DepartureID); 
            return sb.ToString();
        }

        /// <summary>
        /// 车队查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Res_Motorcade> QueryData(ResourceCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Res_Motorcade>("Res_Motorcade", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 酒店详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Res_Motorcade Get(string keyID)
        {
            return DAL.Res_Motorcade.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存酒店
        /// </summary> 
        public bool Save(DAL.Res_Motorcade entity, List<DAL.Res_BizContact> list, string fileID)
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
                        t = new Res_Motorcade();
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
                    t.Title = entity.Title;
                    t.Mobile = entity.Mobile;
                    t.Phone = entity.Phone;
                    t.Fax = entity.Fax;
                    t.Mail = entity.Mail;
                    t.QQ = entity.QQ;
                    t.Addr = entity.Addr;
                    t.Remark = entity.Remark;
                    t.IsEnable = entity.IsEnable;
                    t.Scale = entity.Scale;
                    t.DepartureID = entity.DepartureID;
                    t.Departure = entity.Departure; 
                    t.OrderIndex = entity.OrderIndex;
                    t.ID = entity.ID;
                    t.Save();
                    #endregion

                    #region 订单附件

                    //删除
                    DAL.Ord_OrderFile.Find(x => x.OrderID == entity.ID).ToList().ForEach(x =>
                    {
                        DAL.Ord_OrderFile.Delete(y => y.ID == x.ID);
                    });


                    if (!string.IsNullOrEmpty(fileID))
                    {
                        foreach (var f in fileID.Split(','))
                        {
                            var fentity = new DAL.Ord_OrderFile();
                            fentity.ID = Guid.NewGuid().ToString();
                            fentity.OrderID = entity.ID;
                            fentity.FilleD = f;
                            fentity.OrgID = user.OrgID;
                            fentity.CreateUserID = user.UserID;
                            fentity.CreateUserName = user.UserName;
                            fentity.Save();
                        }
                    }
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存车队时发生错误");
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
                        DAL.Res_Motorcade.Delete(t => t.ID == x);
                    });
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除车队时发生错误");
                return false;
            }
        }
    }
}
