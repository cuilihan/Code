using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Order
{
    public partial class TeamOrderInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFile(KeyID);
                BindData();
            }

            var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
            if (en != null && en.xType == 5 && en.xVal == 1)
            {
                Part.Style.Value = "";
            }
        }

        private void BindData()
        {
            var setting = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(setting.logo))
                img.Src = setting.logo;

            var dal = new Order_BF();
            var e = dal.GetOrderInfo(KeyID);
            lnkToWord.NavigateUrl = "TeamToWord.aspx?id=" + e.ID;

            LoadData(e, pnlWraper);
            if (e != null)
            {
                OrderProfit.Text = (e.OrderAmt - e.OrderCost).ToString();
                var r = (e.OrderAmt - e.OrderCost) / (e.OrderAmt == 0 ? 1 : e.OrderAmt) * 100;
                ProfitRate.Text = r.ToString("f2");

                OrderCollectedAmt.Text = (e.CollectedAmt + e.ToConfirmCollectedAmt).ToString("f2");
                OrderUnCollected.Text = (e.OrderAmt - e.CollectedAmt - e.ToConfirmCollectedAmt).ToString("f2");

                rptCustomer.DataSource = dal.GetOrderCustomer(e.ID);
                rptCustomer.DataBind();

                rptLog.DataSource = dal.GetOrderLog(e.ID);
                rptLog.DataBind();

                Page.Title = e.OrderName + "-订单详情";
            }
            PrintDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            UserName.Text = AuthenticationPage.UserInfo.UserName;
        }


        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected override string NavigateID
        {
            get
            {
                return "salesorderqy";
            }
        }
    }
}