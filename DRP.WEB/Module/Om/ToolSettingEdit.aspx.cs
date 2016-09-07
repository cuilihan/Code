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
    public partial class ToolSettingEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }
        private void BindData()
        {
            var e = new ToolSetting_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new ToolSetting_BF().Save(KeyID, pnlContainer);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "omtool";
            }
        }
    }
}