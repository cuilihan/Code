using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.Framework.Core;
using DRP.Log;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 运维用户管理
    /// </summary>
    public class OmUser_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 运维用户列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Om_UserInfo> GetOmUser(QueryCriteriaBase qry, out int record)
        {
            var strWhere = "1=1";
            if (!string.IsNullOrEmpty(qry.Keyword))
                strWhere += string.Format(" and (OrgName like '%{0}%' or UserName like '%{0}%')", qry.Keyword);
            return db.GetPaginationList<DAL.Om_UserInfo>("Om_UserInfo", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        /// <summary>
        /// 运维人员详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Om_UserInfo Get(string keyID)
        {
            return DAL.Om_UserInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存运维人员
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool SaveOmUser(string keyID, WebControl pnlControl, string pwd)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Om_UserInfo();
                    entity.OrgID = Guid.Empty.ToString();//运维公司默认为空的Guid
                    entity.OrgName = ConfigHelper.GetAppSettingValue("OrgName");
                    entity.CreateName = AuthenticationPage.UserInfo.UserName;
                    entity.CreateDate = DateTime.Now;
                }
                entity = new ReflectHelper<DAL.Om_UserInfo>().AssignEntity(entity, pnlControl);
                entity.UserPwd = Security.EncrypByRijndael(pwd);
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存导航组时发生错误");
            }
            return isSuccess;
        }

        private object obj = new object();

        /// <summary>
        /// 账号是否存在
        /// </summary>
        /// <param name="userAcct"></param>
        /// <returns></returns>
        public bool ExistAcct(string userAcct)
        {
            lock (obj)
            {
                return DAL.Om_UserInfo.Exists(x => x.UserAcct == userAcct);
            }
        }

        /// <summary>
        /// 删除运维用户
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Om_UserInfo.Delete(x => x.ID == keyID);
        }
    }
}
