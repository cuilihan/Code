using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.Framework.Core;
using DRP.BF.SysAdmin;
using DRP.BF.ResMrg;

namespace DRP.WEB.Module.Sys
{
    public partial class OTAEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDDL();
            }
        }

        private void BindDDL()
        {
            hfID.Value = KeyID.ToString();
            ddlSupplier.DataSource = new TravelAgency_BF().QueryOTAData();
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "ID";
            ddlSupplier.DataBind();
            ddlSupplier.SelectedValue = "8b0cea3d-cd8d-4de8-b0be-6a292bcf745e";

            var dal = new OTASetting_BF();
            var order = dal.GetDetail(Guid.Parse(KeyID));
            if (order == null) return;
            ddlSupplier.SelectedValue = order.OTAID.ToString();
            LoadData(order, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfOTAisHave.Value != "1")
            {
                var id = Request["id"];
                var OTAID = Guid.Parse(ddlSupplier.SelectedValue);
                var OTAName = ddlSupplier.SelectedItem.Text;
                var syncType = isSynchronize.Checked == true ? 1 : 0;
                var pwd = this.AcctPwd.Text;
                var isOk = new OTASetting_BF().SaveOTASetting(id, pnlContainer, OTAID, syncType, OTAName, pwd);
                JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
            }
            else
            {
                JScript.Alert("名称重复，保存失败！");
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