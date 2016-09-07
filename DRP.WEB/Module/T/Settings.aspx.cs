using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Quotation;
using DRP.Framework.Core;

namespace DRP.WEB.Module.T
{
    public partial class Setting : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new QuotationSetting_BF().Get(AuthenticationPage.UserInfo.OrgID);
            if (e == null)
                SetDefaultValue();
            else
                txtTemplate.Text = e.Template;
        }

        private void SetDefaultValue()
        {
            var s = "<p align=\"right\"><strong><span style=\"font-size:18px;\">苏州大途网络科技有限公司</span></strong></p><p align=\"right\">@Dept @UserName</p><p align=\"right\">手机号： @UserMobile &nbsp;邮件：@UserEmail</p><p align=\"right\">客服电话：400-6787-0067</p><p align=\"right\">网站：www.58datu.com</p>";
            txtTemplate.Text = s;
        }

        protected override string NavigateID
        {
            get
            {
                return "templateparas";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new QuotationSetting_BF().Save(txtTemplate.Text);
            JScript.Alert(isOk ? "设置成功" : "设置失败");
        }
    }
}