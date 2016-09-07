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
    public partial class IncomeSignEdit : Pagebase
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

            IncomeTypeID.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.CheckIn_IncomeSign);
            IncomeTypeID.DataTextField = "Name";
            IncomeTypeID.DataValueField = "ID";
            IncomeTypeID.DataBind();

            IncomeMethod.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Fin_CollectedType);
            IncomeMethod.DataTextField = IncomeMethod.DataValueField = "Name";
            IncomeMethod.DataBind();
        }

        private void BindData()
        {
            Operator.Text = AuthenticationPage.UserInfo.UserName;
            var e = new IncomeCheckIn_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new IncomeCheckIn_BF().Save(KeyID, pnlContainer, DeptID.SelectedItem.Text, IncomeTypeID.SelectedItem.Text);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        protected override string NavigateID
        {
            get
            {
                return "finincomesign";
            }
        }
    }
}