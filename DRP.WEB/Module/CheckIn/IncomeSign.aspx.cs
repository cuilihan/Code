using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.SysMrg;

namespace DRP.WEB.Module.CheckIn
{
    public partial class IncomeSign : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindParameter();
        }

        private void BindParameter()
        {
            ddlDeptID.DataSource = new Dept_BF().GetDepartment();
            ddlDeptID.DataTextField = "Name";
            ddlDeptID.DataValueField = "ID";
            ddlDeptID.DataBind();

            ddlType.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.CheckIn_IncomeSign);
            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "ID";
            ddlType.DataBind(); 
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