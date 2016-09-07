using DRP.BF;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Message.Core;

namespace DRP.WEB.Module.Om
{
    public partial class SmsAalance : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var Account_ID = ConfigHelper.GetAppSettingValue("Account_ID");
            lblAcct_N.Text = Account_ID;
            var e = new NSmsBiz();
            lblBalance_N.Text = e.GetTemplateSmsBalance();
        }


        protected override string NavigateID
        {
            get
            {
                return "smsbalance";
            }
        }

        /// <summary>
        /// 运营商状态执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStatus_Click(object sender, EventArgs e)
        {
            lblStatus.Text = new NSmsBiz().GetSmsStateReport_N(20);
        }

        protected void btnSmsT_Click(object sender, EventArgs e)
        {
            var user = AuthenticationPage.UserInfo;
            var entity = new MessageEntity();
            entity.KeyID = Guid.NewGuid().ToString();
            entity.SendUserID = user.UserID;
            entity.SendUserName = user.UserName;
            entity.MsgContent = new Random().Next(10000, 99999).ToString();
            entity.OrgID = user.OrgID;
            entity.DataStatus = 0;
            entity.RecMobile = Mobile.Text;
            entity.IsTemplateSms = true;
            entity.SmsType = T_Sms_Type.Validate;

            var r = new NSmsBiz().SendSms(entity);
            JScript.Alert(r.Message);
        }
    }
}