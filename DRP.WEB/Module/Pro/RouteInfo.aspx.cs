using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.ProMrg;
using Novacode;
using DRP.BF.Quotation;
using DRP.BF.SysMrg;
using System.IO;
using DRP.Framework.Core;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Pro
{
    public partial class RouteInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var setting = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(setting.logo))
                img.Src = setting.logo;

            var e = new RouteInfo_BF().Get(KeyID);

            LoadData(e, pnlWraper);
            if (e != null)
            {
                Page.Title = e.RouteName;
                rptData.DataSource = new RouteInfo_BF().GetRouteSchedule(KeyID);
                rptData.DataBind();

                var sb=new StringBuilder();
                if(!string.IsNullOrEmpty(e.Remind))
                    sb.AppendFormat("<div class='itemName'>特别提醒：</div><div>{0}</div>",e.Remind);
                 if(!string.IsNullOrEmpty(e.Feature))
                    sb.AppendFormat("<div class='itemName'>行程特色：</div><div>{0}</div>",e.Feature);
                 lblRouteBaseInfo.Text = sb.ToString();
                
                sb.Clear();

                if (!string.IsNullOrEmpty(e.PriceInclude))
                {
                    sb.Append("<div style='font-size:13px; font-weight:bold;font-family: 微软雅黑; margin:5px 0px;'>费用包含：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.PriceInclude);
                }
                if (!string.IsNullOrEmpty(e.PriceNonIncude))
                {
                    sb.Append("<div style='font-size:13px; font-weight:bold;font-family: 微软雅黑; margin:5px 0px;'>费用不含：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.PriceNonIncude);
                }
                lblPriceInfo.Text = sb.ToString();

                sb.Clear();
                if (!string.IsNullOrEmpty(e.SelfItem))
                {
                    sb.Append("<div class='itemName'>自费项目说明：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.SelfItem);
                }
                if (!string.IsNullOrEmpty(e.Shopping))
                {
                    sb.Append("<div class='itemName'>购物说明：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.Shopping);
                }
                if (!string.IsNullOrEmpty(e.Comment))
                {
                    sb.Append("<div class='itemName'>备注：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.Comment);
                }

                lblItems.Text = sb.ToString();
            }

            #region 页脚
            var user = AuthenticationPage.UserInfo;
            var footer = new QuotationSetting_BF().Get(user.OrgID);
            if (footer != null)
            {
                var str = footer.Template;
                if (!string.IsNullOrEmpty(str))
                {
                    if (user.LoginUserType == UserType.AdminUser)
                    {
                        str = str.Replace("@Dept", user.DeptName).Replace("@UserName", user.UserName).Replace("@UserMobile", user.Mobile).Replace("@UserEmail", "");
                    }
                    else
                    {
                        var u = new User_BF().Get(user.UserID);
                        if (u != null)
                        {
                            str = str.Replace("@Dept", u.DeptName).Replace("@UserName", u.Name).Replace("@UserMobile", u.Mobile).Replace("@UserEmail", u.Email);
                        }
                    }
                    lblFooterInfo.Text = str;
                }
            }
            #endregion
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

        protected void btnToWord_Click(object sender, EventArgs e)
        {
            var vPath = Server.MapPath("/Module/Pro/RouteFile/");
            new RouteInfo_BF().ToWord(vPath, KeyID);           
        }
    }
}