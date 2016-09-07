using DRP.BF;
using DRP.BF.CheckAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Fin
{
    public partial class SettlementBill : Pagebase
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
                DataStatus.Text = e.DataStatus == 1 ? "未结算" : "已结算";
                if (e.SettlementType.IndexOf("报销") > -1)
                    pnlBank.Visible = true;
            }
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