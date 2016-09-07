using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.SysMrg;

namespace DRP.WEB.Module.Sys
{
    public partial class RoleEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new Role_BF().Get(KeyID);
            if (e != null)
            {
                RoleName.Text = e.RoleName;
                Comment.Text = e.Comment;
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "sysrole";
            }
        }
    }
}