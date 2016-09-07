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
    public partial class OrgAdmin : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var org = new OrgInfo_BF().Get(KeyID);
            if (org == null)
            {
                btnSave.Visible = false;
                return;
            }
            OrgName.Text = org.Name;

            var e = new OmUser_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null) 
            {
                UserPwd.Text = Security.DecrypByRijndael(e.UserPwd);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        { 
            var isOk = new OrgInfo_BF().SaveOrgAdminUser(KeyID, pnlContainer);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}