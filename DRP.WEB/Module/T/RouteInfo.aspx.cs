using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.Quotation;
using DRP.BF.SysMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.T
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
            var dal = new Quotation_BF();
            var e = dal.Get(KeyID);
            LoadData(e, pnlWraper);
            if (e != null)
            {
                tourVisitorNum.InnerText = e.VisitorNum.ToString();
                tourTotalAmount.InnerText = (e.Cost + e.Profit).ToString();
                Page.Title = e.RouteName;

                if (!string.IsNullOrEmpty(e.Stay))
                    lblStandard.Text = "住宿标准：" + e.Stay;
                if (!string.IsNullOrEmpty(e.Dinner))
                {
                    if (string.IsNullOrEmpty(lblStandard.Text))
                        lblStandard.Text = "用餐标准：" + e.Dinner;
                    else
                        lblStandard.Text += "<span style='padding-left:2em;'>用餐标准</span>：" + e.Dinner;
                }

                rptData.DataSource = dal.GetRouteSchedule(KeyID);
                rptData.DataBind();

                rptItem.DataSource = dal.GetQuotationCostItem(KeyID);
                rptItem.DataBind();

                #region << 线路相关信息 >>
                var sb = new StringBuilder();
                if (!string.IsNullOrEmpty(e.Feature))
                {
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<div class='itemName'>行程特色：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.Feature);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    lblFeature.Text = sb.ToString();
                }

                sb.Clear();
                if (!string.IsNullOrEmpty(e.Notes))
                {
                    sb.Append("<div class='itemName'>特别提醒：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.Notes);
                }

                if (!string.IsNullOrEmpty(e.SelfItem))
                {
                    sb.Append("<div class='itemName'>自费项目说明：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.SelfItem);
                }

                if (!string.IsNullOrEmpty(e.Comment))
                {
                    sb.Append("<div class='itemName'>备注：</div>");
                    sb.AppendFormat("<div>{0}</div>", e.Comment);
                }
                var strItems = sb.ToString();
                sb.Clear();
                if (!string.IsNullOrEmpty(strItems))
                {
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append(strItems);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    lblItems.Text = sb.ToString();
                }
                #endregion
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

        protected void rptItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "routetemplate";
            }
        }

        /// <summary>
        /// 导出PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnToPDF_Click(object sender, EventArgs e)
        {
            var path = Server.MapPath("/Module/T/Files/" + KeyID + ".pdf");
            var file = RouteName.Text + ".pdf";
            //if (!File.Exists(path))
            //{
            var isOk = new Quotation_BF().CreateQuotationPDFFile(KeyID, path);
            if (isOk)
            {
                DownFile.DownLoad(file, path);
            }
            else
            {
                JScript.Alert("生成PDF文件失败");
            }
            //}
            //else {
            //    DownFile.DownLoad(file, path);
            //}
        }
    }
}