using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.DAL;

namespace DRP.BF.ResMrg
{
    /// <summary>
    /// 景点门票
    /// </summary>
    public class ScenicTicket_BF
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
            if (!string.IsNullOrEmpty(qry.RouteTypeID))
                sb.AppendFormat(" and RouteTypeID='{0}'", qry.RouteTypeID);
            if (!string.IsNullOrEmpty(qry.DestinationID))
                sb.AppendFormat(" and DestinationPath like '%{0}%'", qry.DestinationID);
            return sb.ToString();
        }

        /// <summary>
        /// 景点门票查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Res_ScenicTicket> QueryData(ResourceCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Res_ScenicTicket>("Res_ScenicTicket", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 按线路类型查询所有景点门票
        /// </summary>
        /// <param name="routeTypeID"></param>
        /// <returns></returns>
        public List<DAL.Res_ScenicTicket> QueryData(string routeTypeID)
        {
            var user = AuthenticationPage.UserInfo;
            return DAL.Res_ScenicTicket.Find(x => x.OrgID == user.OrgID && x.RouteTypeID == routeTypeID).OrderBy(x => x.Spell).ToList();
        }

        /// <summary>
        /// 景点门票详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Res_ScenicTicket Get(string keyID)
        {
            return DAL.Res_ScenicTicket.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存酒店
        /// </summary> 
        public bool Save(DAL.Res_ScenicTicket entity, List<DAL.Res_BizContact> list)
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
                        t = new Res_ScenicTicket();
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
                    t.NormalPrice = entity.NormalPrice;
                    t.TeamPrice = entity.TeamPrice;
                    t.CooperatePrice = entity.CooperatePrice;
                    t.RouteTypeID = entity.RouteTypeID;
                    t.RouteType = entity.RouteType;
                    t.Destination = entity.Destination;
                    t.DestinationID = entity.DestinationID;
                    t.DestinationPath = entity.DestinationPath;
                    t.OrderIndex = entity.OrderIndex;
                    t.ID = entity.ID;
                    t.Save();
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存景点门票时发生错误");
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
                        DAL.Res_ScenicTicket.Delete(t => t.ID == x);
                    });
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除景点门票时发生错误");
                return false;
            }
        }
    }
}
