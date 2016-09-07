using DRP.DAL;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 全局推送消息
    /// </summary>
    public class PushNotice_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Glo_PushNotice> GetData(QueryCriteriaBase qry, out int record)
        {
            var fields = "ID,LinkUrl,sDate,eDate,CreateDate,Creator";
            return db.GetPaginationList<DAL.Glo_PushNotice>("Glo_PushNotice", qry.pageIndex, qry.pageSize, out record, "1=1", qry.SortExpress, fields);
        }

        /// <summary>
        /// 推送消息详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Glo_PushNotice Get(Guid keyID)
        {
            return DAL.Glo_PushNotice.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 最后发布的一条消息
        /// </summary>
        /// <returns></returns>
        public DAL.Glo_PushNotice GetLastNotice()
        {
            var sql = "SELECT Top 1 * FROM Glo_PushNotice WHERE GETDATE()>=sDate AND GETDATE()<(DATEADD(DAY,1,eDate)) ORDER BY CreateDate DESC";
            var list = new SubSonic.Query.CodingHorror(sql).ExecuteTypedList<DAL.Glo_PushNotice>();
            if (list.Count > 0) return list.First();
            else return null;
        }

        /// <summary>
        /// 保存推送消息
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(Guid keyID, WebControl pnlControl)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Glo_PushNotice();
                    entity.Creator = user.UserName;
                    entity.CreateDate = DateTime.Now;
                }
                entity = new ReflectHelper<DAL.Glo_PushNotice>().AssignEntity(entity, pnlControl);
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存推送消息时发生错误");
            }
            return isSuccess;
        }

        public void Delete(Guid keyID)
        {
            DAL.Glo_PushNotice.Delete(x => x.ID == keyID);
        }
    }
}
