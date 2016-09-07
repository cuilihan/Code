using DRP.BF.eShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop.Module.My
{
    public partial class MyTravel : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string LocationName
        {
            get
            {
                return "我的旅行";
            } 
        }
    }
}