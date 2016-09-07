using DRP.BF;
using DRP.BF.mSite;
using DRP.BF.OmMrg;
using DRP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.mSite
{
    public partial class LogoSetting : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var setting = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            if (setting != null)
            {
                if (!string.IsNullOrEmpty(setting.logo))
                    imgLogo.Src = setting.logo;
                LogoUrl.Value = setting.logo;
            }
        }



        protected override string NavigateID
        {
            get
            {
                return "setSysInfo";
            }
        }
    }
}