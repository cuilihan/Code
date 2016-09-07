using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Order
{
    public partial class TourOrderInfo : Pagebase
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

            var e = new TourInfo_BF().Get(KeyID);
            lnkToWord.NavigateUrl = "TourToWord.aspx?id=" + e.ID;
            LoadData(e, pnlWraper);
            if (e != null)
            {
                Page.Title = e.TourName + "-订单详情";
                var route = new RouteInfo_BF().Get(e.RouteID);
                var tourExtend = new Order_BF().GetOrderExtend(e.ID);
                if (route != null)
                {
                    Destination.Text = route.Destination;
                    PriceInclude.Text = route.PriceInclude;
                    PriceNonIncude.Text = route.PriceNonIncude;
                }
                if (tourExtend != null)
                {
                    AdultNum.Text = tourExtend.AdultNum.ToString();
                    ChildNum.Text = tourExtend.ChildNum.ToString();
                    OrderAmt.Text = tourExtend.OrderAmt.ToString();
                    OrderCost.Text = tourExtend.OrderCost.ToString();
                    OrderProfit.Text = tourExtend.OrderProfit.ToString();
                    if (tourExtend.OrderAmt != 0)
                    {
                        ProfitRate.Text = ((tourExtend.OrderProfit / tourExtend.OrderAmt) * 100).ToString("f2");
                    }
                }

                #region 导游
                var guideList = new Order_BF().GetOrderGuide(e.ID);
                var data = new List<string>();
                guideList.ForEach(x =>
                {
                    data.Add(x.GuideName + "(" + x.Mobile + ")");
                });
                GuideName.Text = string.Join(",", data);
                #endregion

                #region 行程
                rptData.DataSource = new RouteInfo_BF().GetRouteSchedule(route.ID);
                rptData.DataBind();
                #endregion

                UserName.Text = AuthenticationPage.UserInfo.UserName;
                printDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss");

                #region 预算备注
                var list = new OrderBudget_BF().GetBudgetComment(e.ID);
                if (list.Count > 0)
                {
                    var sb = new StringBuilder();
                    sb.Append("<div style=\"margin: 20px 0px 10px 0px; font-weight: bold;\">");
                    sb.Append("预算备注");
                    sb.Append("</div>");
                    sb.Append("<table class=\"tblPrint\">");
                    sb.Append("<tr>");
                    sb.Append("<th style='width:60px;'>名称</th>");
                    sb.Append("<th>备注</th>");
                    sb.Append("</tr>");
                    list.ForEach(x =>
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", x.Name);
                        sb.AppendFormat("<td>{0}</td>", x.Comment);
                        sb.Append("</tr>");
                    });
                    sb.Append("</table>");
                    lblComment.Text = sb.ToString();
                }
                #endregion

                #region 订单日志
                rptLog.DataSource = new Order_BF().GetOrderLog(e.ID);
                rptLog.DataBind();
                #endregion
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                return "salesorderzzb";
            }
        }
    }
}