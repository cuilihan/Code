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
    public partial class BizRpt : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var y = DateTime.Now.Year;
            for (int i = 0; i < 10; i++)
            {
                ddlYear.Items.Add(new ListItem((y - i).ToString() + "年", (y - i).ToString()));
            }
            ddlDept.DataSource = new Dept_BF().GetDepartment();
            ddlDept.DataValueField = "ID";
            ddlDept.DataTextField = "Name";
            ddlDept.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "rptmultiple";
            }
        }
    }
}