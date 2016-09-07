using DRP.BF;
using DRP.BF.OmMrg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.My
{
    public partial class SendSms : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Mobile.Text = Request["m"];
            if (!IsPostBack)
                SetSmsCount();
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

        private void SetSmsCount()
        {
            var org = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            var n = org.SmsCount - org.SendSmsCount;
            lblSmsCount.Text = org == null ? "0" : (n < 0 ? "0" : n.ToString());
        }
    }
}