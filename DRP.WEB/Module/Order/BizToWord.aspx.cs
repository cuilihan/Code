using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.Order;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Order
{
    public partial class BizToWord : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (en != null && en.xType == 5 && en.xVal == 1)
                {
                    Part.Style.Value = "margin: 8px 0px 20px 0px;";
                }
            }
        }

        private void BindData()
        {
            var e = new Order_BF().GetOrderInfo(KeyID);
            LoadData(e, pnlWraper);
            if (e != null)
            {
                var profit = e.OrderAmt - e.OrderCost;
                lblProfit.Text = profit.ToString();
                if (e.OrderAmt != 0)
                {
                    var rate = profit / e.OrderAmt;
                    lblProfitRate.Text = (rate * 100).ToString("f2") + "%";
                }
                BindOrderCustomer(e.ID);
                BindOrderCost(e.ID);
            }

            Page.Title = e.OrderName + "-订单详情";
            ToWordAction(Page.Title);
        }

        private void BindOrderCustomer(string orderID)
        {
            rptData.DataSource = new Order_BF().GetOrderCustomer(orderID);
            rptData.DataBind();
        }

        private void BindOrderCost(string orderID)
        {
            rptCost.DataSource = new Order_BF().GetOrderCost(orderID);
            rptCost.DataBind();
        }

        protected void rptCost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        private void ToWordAction(string title)
        {
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + title + DateTime.Now.ToString("yyyyMMdd") + ".doc");
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/ms-word";
            this.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            this.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();
        }
    }
}