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
    public partial class PaidInvoiceEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new SupplierInvoice_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var dal = new SupplierInvoice_BF();
            var isOk = dal.Save(KeyID,Request["orderID"], pnlContainer);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
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