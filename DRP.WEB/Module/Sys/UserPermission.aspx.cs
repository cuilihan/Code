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
    public partial class UserPermission : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var list = new User_BF().GetUserRoles(KeyID);
            if (list != null)
            {
                var arr = new List<string>();
                foreach (var e in list)
                    arr.Add(e.RoleName);
                RoleName.Text = string.Join(",", arr);
            }
        }


        protected override string NavigateID
        {
            get
            {
                return "sysuser";
            }
        }
    }
}