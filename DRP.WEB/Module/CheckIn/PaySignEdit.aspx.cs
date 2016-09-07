using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.CheckIn;
using DRP.BF.Glo;
using DRP.BF.SysMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.CheckIn
{
    public partial class PaySignEdit :Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindParameter();
                BindData();
            }
        }

        private void BindParameter()
        {
            DeptID.DataSource = new Dept_BF().GetDepartment();
            DeptID.DataTextField = "Name";
            DeptID.DataValueField = "ID";
            DeptID.DataBind();

            PayTypeID.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.CheckIn_PayableSign);
            PayTypeID.DataTextField = "Name";
            PayTypeID.DataValueField = "ID";
            PayTypeID.DataBind();

            PayMethod.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Fin_CollectedType);
            PayMethod.DataTextField = PayMethod.DataValueField = "Name";
            PayMethod.DataBind();
        }

        private void BindData()
        {
            Operator.Text = AuthenticationPage.UserInfo.UserName;
            var e = new PayCheckIn_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new PayCheckIn_BF().Save(KeyID, pnlContainer, DeptID.SelectedItem.Text,PayTypeID.SelectedItem.Text);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "finpayablesign";
            }
        }
    }
}