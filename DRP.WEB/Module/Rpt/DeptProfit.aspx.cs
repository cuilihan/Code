using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB.Module.Rpt
{
    public partial class DeptProfit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                eDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "rptdeptprofit";
            }
        }
    }
}