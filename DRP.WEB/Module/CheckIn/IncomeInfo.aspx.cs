using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.CheckIn;

namespace DRP.WEB.Module.CheckIn
{
    public partial class IncomeInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                BindData();
            }
        }
         

        private void BindData()
        { 
            var e = new IncomeCheckIn_BF().Get(KeyID);
            LoadData(e, pnlContainer);
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