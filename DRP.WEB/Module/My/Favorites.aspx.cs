using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;

namespace DRP.WEB.Module.My
{
    public partial class Favorites : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}