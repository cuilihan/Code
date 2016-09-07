using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 机构扩展设置类型
    /// </summary>
    public enum OrgSettingType
    {
        /// <summary>
        /// 自主班订单审核
        /// </summary>
        OrderAudit = 1,
        /// <summary>
        /// 开通微网站
        /// </summary>
        OpenWechat=2,
        /// <summary>
        /// 订单收款登记后财务状态自动确认
        /// </summary>
        OrderCollectedSign = 3,
        /// <summary>
        /// 订单导出使用日期
        /// </summary>
        EffectiveData = 4,
        /// <summary>
        /// 开通参与人员
        /// </summary>
        Participant = 5,
        /// <summary>
        /// 开通参与人员
        /// </summary>
        UploadFile = 6
    }

    /// <summary>
    /// 机构扩展设置
    /// </summary>
    public class OrgSetting_BF
    {
        public DAL.Om_OrgSetting Get(string orgID, OrgSettingType xType)
        {
            return DAL.Om_OrgSetting.SingleOrDefault(x => x.OrgID == orgID && x.xType == (int)xType);
        }

        public List<DAL.Om_OrgSetting> Get(string orgID)
        {
            return DAL.Om_OrgSetting.Find(x => x.OrgID == orgID).ToList();
        }
         
        public struct SettingType
        {
            public OrgSettingType xType { get; set; }

            public int xVal { get; set; }

            public DateTime effectiveData { get; set; }
        }

        public bool Save(string orgID, List<SettingType> list)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    list.ForEach(entity =>
                    {
                        DAL.Om_OrgSetting.Delete(x => x.OrgID == orgID && x.xType == (int)entity.xType);

                        var e = new DAL.Om_OrgSetting();
                        e.ID = Guid.NewGuid().ToString();
                        e.OrgID = orgID;
                        e.xType = (int)entity.xType;
                        e.xVal = entity.xVal;
                        e.CreateDate = DateTime.Now;
                        e.CreateUserName = user.UserName;
                        e.effectiveData = entity.effectiveData;
                        
                        e.Save();
                    });

                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "机构扩展设置时发生错误");
                return false;
            }
        }
    }
}
