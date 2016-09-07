using DRP.BF;
using DRP.BF.CheckAccount;
using DRP.BF.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;
using DRP.BF.ResMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Order
{
    public partial class OrderCheckAcctBill : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OrderGuide_BF().Get(KeyID);
            if (e != null)
            {
                var t = new CheckAccount_BF().GetOrderBalance(e.OrderBalanceID);
                lblGuide.Text = e.GuideName;
                lblMobile.Text = e.Mobile;
                lblDrawMoney.Text = (t.YLTK == null ? 0 : (decimal)t.YLTK).ToString();
                lblCheckAmt.Text = ((t.XSTK == null ? 0 : (decimal)t.XSTK) + (t.QTSR == null ? 0 : (decimal)t.QTSR)).ToString();
                var totalIncome = lblDrawMoney.Text.ToDecimal() + lblCheckAmt.Text.ToDecimal();
                var totalCost = new CheckAccount_BF().OrderBalanceCostAmt(e.OrderBalanceID);
                var delt = totalIncome - totalCost;
                lblCostAmt.Text = totalCost.ToString();
                var str = "报账单确认";
                if (delt > 0) str = "余款入账";
                if (delt < 0)
                {
                    str = "垫资报销";
                    delt *= -1;
                    tblData.Visible = true;

                    //绑定导游的银行账号
                    var guide = new Guide_BF().Get(e.GuideID);
                    if (guide != null)
                    {
                        txtBankAcct.Text = guide.BankAcct;
                        txtBankName.Text = guide.BankName;
                    }
                }
                lblBalanceType.Text = str;
                lblAmtText.Text = str + "金额";
                lblBalanceAmt.Text = delt.ToString();
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new OrderBalanceSettlement_BF().OrderBalanceConfirm(KeyID, txtBankName.Text, txtBankAcct.Text, lblCostAmt.Text.ToDecimal(), txtComment.Text);
            JScript.CloseDialogWin(isOk ? "确认成功" : "确认失败");
        }
    }
}