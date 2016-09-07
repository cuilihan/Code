using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.GloMrg;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.WEB.Module.Om
{
    public partial class QQEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new QQ_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            var isOk = new QQ_BF().Save(KeyID,QQ.Text, Name.Text, OrderIndex.Text.ToInt(),Comment.Text);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "onlineservice";
            }
        }
    }
}