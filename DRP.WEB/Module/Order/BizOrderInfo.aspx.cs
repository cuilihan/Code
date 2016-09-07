using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.OmMrg;
using System.IO;
using System.Text;

namespace DRP.WEB.Module.Order
{
    public partial class BizOrderInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                BindFile(KeyID);
                var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (en != null && en.xType == 5 && en.xVal == 1)
                {
                    Part.Style.Value = "margin: 8px 0px 20px 0px;";
                }
            }
        }

        private void BindData()
        {
            var setting = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(setting.logo))
                img.Src = setting.logo;

            var e = new Order_BF().GetOrderInfo(KeyID);

            lnkToWord.NavigateUrl = "BizToWord.aspx?id=" + e.ID;
            LoadData(e, pnlWraper);
            if (e != null)
            {
                var profit = e.OrderAmt - e.OrderCost;
                lblProfit.Text = profit.ToString();
                if (e.OrderAmt != 0)
                {
                    var rate = profit / e.OrderAmt;
                    lblProfitRate.Text = (rate * 100).ToString("f2")+"%";
                }
                BindOrderCustomer(e.ID);
                BindOrderCost(e.ID);
            }
            UserName.Text = AuthenticationPage.UserInfo.UserName;
            printDate.Text = DateTime.Now.ToString();

            Page.Title = e.OrderName + "-订单详情";
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

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

        protected void rptCost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        private void BindFile(string orderID)
        {
            rptFile.DataSource = new Order_BF().GetOrderFile(orderID);
            rptFile.DataBind();
        }
    }
}