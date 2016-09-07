using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.Quotation
{
    /// <summary>
    /// 报价模板设置
    /// </summary>
    public class QuotationSetting_BF
    {
        public DAL.Pro_QuotationSetting Get(string keyID)
        {
            return DAL.Pro_QuotationSetting.SingleOrDefault(x => x.ID == keyID);
        }

        public bool Save(string template)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {              
                var e = Get(user.OrgID);
                if (e == null)
                {
                    e = new DAL.Pro_QuotationSetting();
                    e.CreateDate = DateTime.Now;
                    e.OrgID = user.OrgID;
                    e.CreateUserName = user.UserName;
                }
                e.Template = template;
                e.ID = user.OrgID;
                e.Save();

                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存报价模板设置时发生错误");
                return false;
            }
        }
    }
}
