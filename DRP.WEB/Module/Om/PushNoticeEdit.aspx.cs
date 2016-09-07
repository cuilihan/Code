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
    public partial class PushNoticeEdit : Pagebase
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
                return "ompushnotice";
            }
        }

        private void BindData()
        {
            var e = new PushNotice_BF().Get(Guid.Parse(KeyID));
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new PushNotice_BF().Save(Guid.Parse(KeyID), pnlContainer);
            JScript.CloseTabPanel(isOk ? "保存成功" : "保存失败", "编辑消息");
        }
    }
}