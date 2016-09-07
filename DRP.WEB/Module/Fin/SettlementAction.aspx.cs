using DRP.BF;
using DRP.BF.CheckAccount;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Fin
{
    public partial class SettlementAction : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OrderBalanceSettlement_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                if (e.SettlementType.IndexOf("报销") > -1)
                    pnlBank.Visible = true;
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "balancesettlement";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new OrderBalanceSettlement_BF().UpdateStatus(KeyID);
            JScript.CloseDialogWin(isOk ? "结算成功" : "结算失败");
        }
    }
}