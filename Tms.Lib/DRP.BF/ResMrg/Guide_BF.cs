using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.Framework.Core;

namespace DRP.BF.ResMrg
{
    /// <summary>
    /// 导游管理
    /// </summary>
    public class Guide_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 组合查询条件
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
            if (qry.DataStatus != null)
                sb.AppendFormat(" and IsEnable='{0}'", (bool)qry.DataStatus);
            return sb.ToString();
        }

        /// <summary>
        /// 导游查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Res_Guide> QueryData(ResourceCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Res_Guide>("Res_Guide", qry.pageIndex, qry.pageSize, out record, QueryCondition(qry), qry.SortExpress);
        }


        /// <summary>
        /// 查询所有导游
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Res_Guide> QueryData()
        {
            return DAL.Res_Guide.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).OrderBy(x => x.Spell).ToList();
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Res_Guide Get(string keyID)
        {
            return DAL.Res_Guide.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存线路
        /// </summary> 
        public bool Save(string keyID, WebControl pnlControl, string departureName, string fileID)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Res_Guide();
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                    entity.DeptID = user.DeptID;
                    entity.OrgID = user.OrgID;
                }
                entity = new ReflectHelper<DAL.Res_Guide>().AssignEntity(entity, pnlControl);
                entity.DepartureName = departureName;
                entity.Spell = EcanConvertToCh.GetFirstChar(entity.Name);
                entity.ID = keyID;
                entity.Save();

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
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存导游时发生错误");
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
                return new DAL.Res_Guide().MultiDelete(listID);
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除导游时发生错误");
                return false;
            }
        }
    }
}
