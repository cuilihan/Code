using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.SysMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.My
{
    public partial class MyPasswrod : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lblUserName.Text = AuthenticationPage.UserInfo.UserName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOriPwd.Text))
                return;
            var user = AuthenticationPage.UserInfo;
            var oriPwd = "";
            if (user.LoginUserType == UserType.BizUser)
            {
                var u = new User_BF().Get(user.UserID);
                oriPwd = u.AcctPwd;
            }
            else
            {
                var u = new OmUser_BF().Get(user.UserID);
                oriPwd = u.UserPwd;
            } 
            if (oriPwd != Security.EncrypByRijndael(txtOriPwd.Text.Trim()))
            {
                JScript.Alert("原密码错误");
                return;
            }
            else
            {
               var isOk= new User_BF().UpdatePwd(txtNewPwd.Text);
               JScript.Alert(isOk ? "密码更新成功" : "密码更新失败");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "mypassword";
            }
        }
    }
}