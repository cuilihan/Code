using DRP.BF.eShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop
{
    public partial class AboutUs : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new eShopConfig().Get(OrgID);
            if (e != null)
            {
                if (!string.IsNullOrEmpty(e.Logo))
                {
                    lblAboutUs.Text = e.AboutUs;
                } 
            }
        }

        protected override string LocationName
        {
            get
            {
                return "关于我们";
            }
        }
    }
}