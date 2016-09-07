using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Fin;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin
{
    public partial class CollectItemEdit : Pagebase
    {
        CollectedItem_BF dal = new CollectedItem_BF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = dal.Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = dal.Save(KeyID, pnlContainer);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "fincollected";
            }
        }
    }
}