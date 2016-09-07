using DRP.BF;
using DRP.BF.mSite;
using DRP.BF.OmMrg;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.mSite
{
    public partial class SiteSetting : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var user = AuthenticationPage.UserInfo;
            var org = new OrgInfo_BF().Get(user.OrgID);
            if (org == null) return;
            Domain.Value = ConfigHelper.GetAppSettingValue("TMS_MicroSite_Domain");
            OrgID.Value = org.ID.ToLower().ToString();
            lblDomain.Text = Domain.Value + "Index.aspx?appid=" + org.ID.ToLower();

            var e = new OrgSetting_BF().Get(org.ID, OrgSettingType.OpenWechat);
            lblStatus.Text = e == null ? "禁用" : e.xVal == 1 ? "启用" : "禁用";
            btnSave.Visible = lblStatus.Text != "禁用";
            TravelName.Text = string.IsNullOrEmpty(org.Brand) ? org.Name : org.Brand;
            if (lblStatus.Text == "禁用") lblTips.Text = "请致电“旅管家”运维客服部开通";

            #region 微站配置数据

            var setting = new mSite_BF().GetBasicInfo();
            if (setting != null)
            {
                ShowRoute.SelectedValue = setting.IsShowRoute ? "1" : "0";
                lblDomain.Text = setting.LinkUrl;
                TravelName.Text = setting.TravelName;
                Phone.Text = setting.HotLine;
                if (!string.IsNullOrEmpty(setting.Logo))
                    imgLogo.Src = setting.Logo;
                AboutUs.Text = setting.AboutUs;
                LogoUrl.Value = setting.Logo;
            }
            #endregion

            #region 广告图片
            var adData = new mSite_BF().GetAdSlide();
            var sb = new StringBuilder();
            adData.ForEach(x =>
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td><img src='{0}' style='width:100px; height:60px;' /><input type='hidden' value='{0}' /></td>", x.ImgSrc);
                sb.AppendFormat("<td><input type='text' style='height:26px;width:90%;' class='textbox' value='{0}' /></td>", x.LinkUrl);
                sb.AppendFormat("<td style='text-align:center;'><a href='javascript:;' onclick=\"a.fnDeletePic(this)\">删除</a></td>");
                sb.Append("</tr>");
            });
            lblAdInfo.Text = sb.ToString();
            #endregion
        }



        protected override string NavigateID
        {
            get
            {
                return "microsite";
            }
        }
    }
}