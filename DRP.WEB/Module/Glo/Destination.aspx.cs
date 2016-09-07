using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;

namespace DRP.WEB.Module.Glo
{
    public partial class Destination : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindRouteType();
        }

        private void BindRouteType()
        {
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            var sb = new StringBuilder();
            var idx = 0;
            list.ForEach(x =>
            {
                sb.AppendFormat("<li id='{0}' class='{1}'>{2}</li>", x.ID, idx++ == 0 ? "at" : "not", x.Name);
            });
            lblRouteType.Text = sb.ToString();
        }

        protected override string NavigateID
        {
            get
            {
                return "destination";
            }
        }
    }
}