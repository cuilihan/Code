using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Om
{
    public partial class NavGroupEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }
        private void BindData()
        {
            var e = new NavGroup_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

      
        protected override string NavigateID
        {
            get
            {
                return "navgroup";
            }
        }
    }
}