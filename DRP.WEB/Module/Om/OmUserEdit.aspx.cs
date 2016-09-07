using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Om
{
    public partial class OmUserEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OmUser_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e == null)
            {
                OrgName.Text = ConfigHelper.GetAppSettingValue("OrgName");
            }
            else
            {
                AcctPwd.Value = Security.DecrypByRijndael(e.UserPwd);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["id"]))//新增时判断是否存在相同的账号
            {
                if (new OmUser_BF().ExistAcct(UserAcct.Text.Trim()))
                {
                    JScript.Alert("账号已存在");
                    return;
                }
            }
            var p = string.IsNullOrEmpty(txtAcctPwd.Text) ? AcctPwd.Value : txtAcctPwd.Text;
            var isOk = new OmUser_BF().SaveOmUser(KeyID, pnlContainer, p);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "omuser";
            }
        }
    }
}