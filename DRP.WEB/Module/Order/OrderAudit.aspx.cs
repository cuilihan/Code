using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.SysMrg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Order
{
    public partial class OrderAudit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
            }
        }

     

        protected override string NavigateID
        {
            get
            {
                return "zzborderaudit";
            }
        }
    }
}