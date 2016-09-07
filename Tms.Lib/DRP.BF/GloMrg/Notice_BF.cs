using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.Framework.Core;

namespace DRP.BF.GloMrg
{
    /// <summary>
    /// 通知公告管理
    /// </summary>
    public class Notice_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string QueryCondition(QueryCriteriaBase qry)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and Subject like '%{0}%'", qry.Keyword);
            // sb.AppendFormat(" and (IsAll='{0}' or DeptID like '%{1}%' or DeptID like '%{2}%')", true, user.DeptID, user.PartDeptID);
            return sb.ToString();
        }

        public DataTable GetNotice(QueryCriteriaBase qry, out int record)
        {
            var fields = "ID,Subject,CreateDate,CreateUserName,DeptName,IsAll,CONVERT(VARCHAR(10),CreateDate,23) cDate";
            return db.GetPagination("Glo_Notice", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress, fields);
        }

        public List<DAL.Glo_Notice> GetTopNotice(int iCount)
        {
            var sql = string.Format("select top {0} ID,Subject,CreateDate,CreateUserName from Glo_Notice where OrgID=@OrgID order by CreateDate desc", iCount);
            return new SubSonic.Query.CodingHorror(sql, AuthenticationPage.UserInfo.OrgID).ExecuteTypedList<DAL.Glo_Notice>();
        }


        public DAL.Glo_Notice Get(string keyID)
        {
            return DAL.Glo_Notice.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存通知公告
        /// </summary> 
        public bool Save(string keyID, WebControl pnlControl, string deptName = "")
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Glo_Notice();
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                    entity.OrgID = user.OrgID;
                }
                entity = new ReflectHelper<DAL.Glo_Notice>().AssignEntity(entity, pnlControl);
                entity.DeptName = deptName;
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存通知公告时发生错误");
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
                return new DAL.Glo_Notice().MultiDelete(listID);
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除通知公告时发生错误");
                return false;
            }
        }


        #region 阅读人记录
        /// <summary>
        /// 登记已阅读人记录
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public bool SignReader(string noticeID)
        {
            try
            {
                var user = AuthenticationPage.UserInfo;
                if (!DAL.Glo_NoticeTrace.Exists(x => x.NoticeID == noticeID && x.UserID == user.UserID))
                {
                    var e = new DAL.Glo_NoticeTrace();
                    e.ID = Guid.NewGuid().ToString();
                    e.NoticeID = noticeID;
                    e.UserID = user.UserID;
                    e.UserName = user.UserName;
                    e.CreateDate = DateTime.Now;
                    e.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 阅读人记录列表
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public List<DAL.Glo_NoticeTrace> GetNoticeTrace(string noticeID)
        {
            return DAL.Glo_NoticeTrace.Find(x => x.NoticeID == noticeID).OrderByDescending(x => x.CreateDate).ToList();
        }
        #endregion
    }
}
