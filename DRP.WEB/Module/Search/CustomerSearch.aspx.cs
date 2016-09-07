using DRP.BF;
using DRP.BF.Glo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Search
{
    public partial class CustomerSearch : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Name.Text = HttpUtility.HtmlDecode(Request["key"]);
                BindData();
            }
        }

        private void BindData()
        {
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Crm_CustomerType);
            var sb = new StringBuilder();
            list.ForEach(x =>
            {
                sb.AppendFormat("<a id='{0}'>{0}</a>", x.Name);
            });
            lblItem.Text = sb.ToString();
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