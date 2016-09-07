using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.DAL;

namespace DRP.Message.Core
{
    /// <summary>
    /// 系统消息管理类
    /// </summary>
    public class MessageBiz : IMessage
    {
        /// <summary>
        /// 系统消息列表
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<DAL.Glo_Message> GetUserMessage(MessageQuery qry, out int totalRecord)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and RecUserID='{0}'", qry.RecUserID);
            if (!string.IsNullOrEmpty(qry.MessageTitle))
                sb.AppendFormat(" and MsgTitle like '%{0}%'", qry.MessageTitle);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and CrateDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            if (qry.DataStatus != MessageStatus.All)
                sb.AppendFormat(" and DataStatus={0}", (int)qry.DataStatus);
            return new DRPDB().GetPaginationList<DAL.Glo_Message>("Glo_Message", qry.PageIndex, qry.PageSize, out totalRecord, sb.ToString(), qry.SortExpress);
        }

        /// <summary>
        /// 设置消息状态
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public void SetMessageStatus(string keyID, MessageStatus status = MessageStatus.Readed)
        {
            var e = DAL.Glo_Message.SingleOrDefault(x => x.ID == keyID);
            e.DataStatus = (int)status;
            e.Save();
        }

        /// <summary>
        /// 前N条未读消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public List<DAL.Glo_Message> GetTopMessage(string userID, int iCount)
        {
            var sql = string.Format("SELECT TOP {0} ID,MsgTitle,Target,URL FROM Glo_Message WHERE RecUserID=@userID and DataStatus=1 Order By CreateDate Desc", iCount);
            return new SubSonic.Query.CodingHorror(sql, userID).ExecuteTypedList<DAL.Glo_Message>();
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="idArr"></param>
        /// <param name="status"></param>
        public void SetStatus(List<string> idArr, MessageStatus status)
        {
            var sql = "Update Glo_Message Set DataStatus={0} where ID in ({1})";
            sql = string.Format(sql, (int)status, string.Join(",", idArr));
            new SubSonic.Query.CodingHorror(sql).Execute();
        }

        #region IMessage 成员

        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Send(MessageEntity entity)
        {
            try
            {
                var e = new DAL.Glo_Message();
                e.ID = entity.KeyID;
                e.OrgID = entity.OrgID;
                e.RecUserID = entity.RecUserID;
                e.RecUserName = entity.RecUserName;
                e.SendUserID = entity.SendUserID;
                e.SendUserName = entity.SendUserName;
                e.MsgContent = entity.MsgContent;
                e.MsgTitle = entity.MsgTitle;
                e.CreateDate = DateTime.Now;
                e.DataStatus = entity.DataStatus;
                e.Target = entity.Target;
                e.URL = entity.URL;
                e.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
