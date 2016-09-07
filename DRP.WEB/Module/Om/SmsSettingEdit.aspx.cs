using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Om
{
    public partial class SmsSettingEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OmSms_BF().Get(KeyID);
            LoadData(e, pnlControl);
        }

        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new OmSms_BF().Save(KeyID, pnlControl, Request["orgID"]);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }
    }
}