using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 短信管理
    /// </summary>
    public class OmSms_BF
    {
        /// <summary>
        /// 短信平台充值记录
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<DAL.Sms_Platform> GetSmsPlatform(string orgID)
        {
            return DAL.Sms_Platform.Find(x => x.OrgID == orgID).OrderByDescending(x => x.CreateDate).ToList();
        }

        public DAL.Sms_Platform Get(string keyID)
        {
            return DAL.Sms_Platform.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存机构
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl, string orgID)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Sms_Platform();
                    entity.ID = Guid.NewGuid().ToString();
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                    entity.OrgID = orgID;
                }
                entity = new ReflectHelper<DAL.Sms_Platform>().AssignEntity(entity, pnlControl);

                entity.Save();

                //更新主表记录
                var org = new OrgInfo_BF().Get(orgID);
                org.SmsCount = org.SmsCount == null ? entity.SmsCount : ((int)org.SmsCount + entity.SmsCount);
                org.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "设置机构短信时发生错误");
            }
            return isSuccess;
        }
    }
}
