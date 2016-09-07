using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin
{
    public partial class CreateInvoice : Pagebase
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
            if (e != null)
            {
                btnSave.Visible = e.InvoiceStatus == (int)InvoiceStatus.Submit;
                InvoiceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new OrderInvoice_BF().CreateInvoice(KeyID, InvoiceNo.Text, InvoiceDate.Text, AuditRemark.Text);
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