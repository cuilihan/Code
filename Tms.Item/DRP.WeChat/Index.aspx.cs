using DRP.BF;
using DRP.WeChat.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WeChat
{
    public partial class Index : WeChatBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            lblUserName.Text = AuthenticationPage.UserInfo.UserName;
        }
    }
}