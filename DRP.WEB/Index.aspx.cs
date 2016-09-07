using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.GloMrg;
using DRP.BF.OmMrg;
using DRP.BF.SysMrg;
using DRP.Cached;
using DRP.Framework.Core;

namespace DRP.WEB
{
    public partial class Index : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetUserRole();
                BindData();
                LoadOnLineService();
            }
        }

        /// <summary>
        /// 指定用户角色
        /// </summary>
        private void SetUserRole()
        {
            var roleID = Request["id"];
            if (!string.IsNullOrEmpty(roleID))
            {
                var role = new Role_BF().Get(roleID);
                AuthenticationPage.SetUserCurrentRole(role);
            }
            var roles = AuthenticationPage.UserInfo.Roles;
            if (roles.Count > 1)
            {
                var sb = new StringBuilder();
                roles.ForEach(x =>
                {
                    sb.AppendFormat("<a href='/Index.aspx?id={0}'>{1}</a>", x.ID, x.RoleName);
                });
                lblRoles.Text = sb.ToString();
            }
            else
                lblRoles.Text = "【无】";
        }

        private void BindData()
        {
            var user = AuthenticationPage.UserInfo;
            lblLoginUserName.Text = user.UserName;
            var roleName = user.CurrentRole == null ? "" : user.CurrentRole.RoleName;
            roleName = string.IsNullOrEmpty(roleName) ? "无" : roleName;
            lblCurrentRole.Text = "【" + roleName + "】"; ;
            lblRoleName.Text = "【" + (roleName.Length > 4 ? roleName.Substring(0, 4) : roleName) + "】";
            lblOrgProName.Text = string.IsNullOrEmpty(user.ProName) ? ConfigHelper.GetAppSettingValue("OrgProductName") : user.ProName;
            lblSysDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + "&nbsp;&nbsp;" + DateTime.Today.ToString("ddd");
            try
            {
                BindNavigate();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Error.aspx?msg=" + DRP.Framework.Core.Security.Base64Encrypt(ex.Message));
            }

            var theme = CookieHelper.GetCookieValue("DRP_TMS_THEMES_" + UserInfo.UserID);
            var skin = "蓝色";
            if (!string.IsNullOrEmpty(theme))
            {
                switch (theme.ToUpper())
                {
                    case "GRAY":
                        skin = "灰色";
                        break;
                    case "BLUE":
                        skin = "蓝色";
                        break;
                }
            }
            lblCurSkin.Text = skin;
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }


        private void BindNavigate()
        {
            var list = new Navigate_BF().GetLoginUserTopNavigate();
            var sb = new StringBuilder();
            list.ForEach(x =>
            {
                sb.AppendFormat("<li><a class=\"{0}\" id='{1}' pageID='{4}' href='{3}' target='{5}'>{2}</a></li>",
                    x.NavCls, x.ID, x.NavName, string.IsNullOrEmpty(x.NavUrl) ? "javascript:;" : x.NavUrl, x.PageID,
                    x.PageID == "tmssite" ? "_blank" : "frmBench");
            });
            lblNavigate.Text = sb.ToString();
        }

        #region 更换皮肤

        protected void btnBlue_Click(object sender, EventArgs e)
        {
            SetThemesToCookie("BLUE");
        }

        protected void btnGray_Click(object sender, EventArgs e)
        {
            SetThemesToCookie("GRAY");
        }

        private void SetThemesToCookie(string themes)
        {
            CookieHelper.AddCookie("DRP_TMS_THEMES_" + AuthenticationPage.UserInfo.UserID, themes);
            Response.Redirect("/");
        }
        #endregion

        #region QQ客服
        private void LoadOnLineService()
        {
            var list = new QQ_BF().GetData();
            var sb = new StringBuilder();
            list.ForEach(x =>
            {
                var s = "<a target=\"_blank\" title='{1}' href=\"http://wpa.qq.com/msgrd?v=3&uin={0}&site=qq&menu=yes\"><img border=\"0\" src=\"http://wpa.qq.com/pa?p=2:{0}:4\" alt=\"{1}\" />{1}</a>";
                sb.AppendFormat(s, x.QQ, x.Name);
            });
            lblQQ.Text = sb.ToString();
        }
        #endregion
    }
}