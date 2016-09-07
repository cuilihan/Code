using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.SysMrg;

namespace DRP.WEB.Module.Sys
{
    public partial class Permission : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRole();
                BindRouteType();
            }
        }

        private void BindRole()
        {
            rdRole.DataSource = new Role_BF().GetRoleInfo();
            rdRole.DataTextField = "RoleName";
            rdRole.DataValueField = "ID";
            rdRole.DataBind();
        }

        private void BindRouteType()
        {
            chkOrderPermission.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            chkOrderPermission.DataTextField = "Name";
            chkOrderPermission.DataValueField = "ID";
            chkOrderPermission.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "syspermission";
            }
        }
    }
}