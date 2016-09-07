using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;

namespace DRP.WEB.Module.Pro
{
    public partial class ProNav : Pagebase
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            sDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            eDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            var sb = new StringBuilder();
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            var idx = 0;
            list.ForEach(x =>
            {
                sb.AppendFormat("<li class=\"{0}\" id='{1}'>{2}</li>", idx++ == 0 ? "at" : "not", x.ID, x.Name);
            }); 
            if (sb.Length > 0)
                lblRouteType.Text = sb.ToString();
            else
                lblRouteType.Text = "<li class=\"at\">未设置线路类型</li>";
        }


        protected override string NavigateID
        {
            get
            {
                return "productbook";
            }
        }
    }
}