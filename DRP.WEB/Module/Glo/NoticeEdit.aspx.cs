using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.GloMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Glo
{
    public partial class NoticeEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        protected override string NavigateID
        {
            get
            {
                return "glonotice";
            }
        }

        private void BindData()
        {
            var e = new Notice_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new Notice_BF().Save(KeyID, pnlContainer);
            JScript.CloseTabPanel(isOk ? "保存成功" : "保存失败", "通知公告维护");
        }
    }
}