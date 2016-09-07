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
    public partial class TeamToWord : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            var dal = new Order_BF();
            var e = dal.GetOrderInfo(KeyID);

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

                Page.Title = e.OrderName + "-订单详情";
            }
            ToWordAction(Page.Title);
        }


        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected override string NavigateID
        {
            get
            {
                return "salesorderqy";
            }
        }
    }
}