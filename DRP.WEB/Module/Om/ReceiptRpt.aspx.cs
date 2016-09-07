using DRP.BF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Om
{
    public partial class ReceiptRpt : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var y = DateTime.Now.Year;
                for (var i = y - 9; i <= y; i++)
                {
                    Year.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
                }
                Year.SelectedValue = y.ToString();
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "orgreceipt";
            }
        }
    }
}