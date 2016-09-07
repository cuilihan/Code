using DRP.BF.eShop;
using DRP.Framework.Core;
using DRP.Message.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop
{
    public partial class Register : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new eShopConfig().Get(OrgID);
            if (e != null)
            {
                lblCompany.Text = e.TravelName;
                lblLoginUrl.Text = string.Format("<a href='/Login.aspx?appid={0}'>登录</a>", e.OrgID);
            }
        }

        protected override string LocationName
        {
            get
            {
                return "用户注册";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var isOk = new SmsBiz().IsValidCode(txtCode.Text.Trim(), txtMobile.Text.Trim());
            if (isOk)
            {
                var dao = new eShopMemeber();
                if (dao.Exist(txtMobile.Text, OrgID))
                    lblTips.Text = "当前手机号码已经被注册，请找回密码或重置手机号";
                else
                {
                    isOk = dao.RegMemeber(txtMobile.Text.Trim(), OrgID);
                    if (isOk) lblTips.Text = "注册成功，登录密码为：【123456】，请登录后修改";
                    else lblTips.Text = "用户注册失败";
                }
            }
            else
            {
                lblTips.Text = "验证码错误，验证码5分钟内有效";
            }
        }
    }
}