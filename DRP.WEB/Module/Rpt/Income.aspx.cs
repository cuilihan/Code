using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.SysMrg;

namespace DRP.WEB.Module.Rpt
{
    public partial class Income : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                BindDept();
            }
        }

        private void BindDept()
        {
            ddlDept.DataSource = new Dept_BF().GetDepartment();
            ddlDept.DataValueField = "ID";
            ddlDept.DataTextField = "Name";
            ddlDept.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "rptorderincome";
            }
        }
    }
}