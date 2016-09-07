using DRP.BF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Om
{
    public partial class OmReceivable : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                eDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "omreceivablerpt";
            }
        }
    }
}