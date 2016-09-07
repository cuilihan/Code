using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.WEB.Module.Fin
{
    public partial class InvoiceOp : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OrderInvoice_BF().GetInvoice(KeyID);
            LoadData(e, pnlContainer);
            var action = Request["action"].ToInt();
            lblAction.Text = action == 1 ? "退回申请原因" : "发票作废原因";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var action = Request["action"].ToInt();
            var status = action == 1 ? InvoiceStatus.Deny : InvoiceStatus.Canceled;
            var isOk = new OrderInvoice_BF().InvoiceOperate(KeyID, status, AuditRemark.Text);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "fininvoice";
            }
        }
    }
}