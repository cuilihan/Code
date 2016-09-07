using DRP.BF.eShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop.Module.My
{
    public partial class InfoEdit : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new eShopMemeber().Get(base.CurrentUser.UserID);
            if (e == null) btnSave.Visible = false;
            else
            {
                Mobile.Text = e.Mobile;
                UserName.Text = e.UserName;
                NickName.Text = e.NickName;
                IDNo.Text = e.IDNo;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Mobile.Text) || Mobile.Text.Length != 11)
            {
                lblTips.Text = "手机号格式错误";
                return;
            }
            var entity = new DAL.Sn_Memeber();
            entity.ID = base.CurrentUser.UserID;
            entity.UserName = UserName.Text.Trim();
            entity.NickName = NickName.Text.Trim();
            entity.Mobile = Mobile.Text.Trim();
            entity.IDNo = IDNo.Text.Trim();
            var isOK = new eShopMemeber().UpdateInfo(entity);
            if (isOK) lblTips.Text = "资料更新成功";
            else lblTips.Text = "资料更新失败";
        }

        protected override string LocationName
        {
            get
            {
                return "修改资料";
            }
        }
    }
}