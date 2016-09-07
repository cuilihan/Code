using DRP.BF;
using DRP.BF.GloMrg;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Om
{
    public partial class UpdateLogEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }
        private void BindData()
        {
            var e = new UpdateLog_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e == null)
            {
                CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                CreateUserName.Text = AuthenticationPage.UserInfo.UserName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new UpdateLog_BF().Save(KeyID, pnlContainer);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "updatelog";
            }
        }
    }
}