using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;

namespace DRP.WEB.Module.Om
{
    public partial class Receipt : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OrgInfo_BF().GetReceipt((Guid)KeyID.ToGuid());
            if (e == null)
            {
                Receiver.Text = AuthenticationPage.UserInfo.UserName;
                ReceiveDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            { 
                LoadData(e, pnlContainer);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var grantDate = new DateScope();
            grantDate.sDate = sDate.Text;
            grantDate.eDate = eDate.Text;
            var isOk = new OrgInfo_BF().SaveReceipt(Guid.Parse(KeyID), Request["orgID"], PaidAmt.Text.ToDecimal(),UserCount.Text.ToInt(), grantDate,
                Receiver.Text, DateTime.Parse(ReceiveDate.Text), Comment.Text);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }


    }
}