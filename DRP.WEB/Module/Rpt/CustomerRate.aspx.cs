using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB.Module.Rpt
{
    public partial class CustomerRate : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitDate();
        }

        private void InitDate()
        {
            var y = DateTime.Today.Year;
            for (int i = 0; i < 10; i++)
            {
                ddlYear.Items.Add(new ListItem(y.ToString() + "年", y.ToString()));
                y -= 1;
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "rptcustomerrate";
            }
        }
    }
}