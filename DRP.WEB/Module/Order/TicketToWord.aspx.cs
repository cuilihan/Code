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
    public partial class TicketToWord : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();

                var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (en != null && en.xType == 5 && en.xVal == 1)
                {
                    pa.Style.Value = "";
                }
            }
        }

        private void BindData()
        {
            var dal = new TicketOrder_BF();
            var e = dal.Get(KeyID);
           
            LoadData(e, pnlWraper);
            if (e != null)
            {
                OrderProfit.Text = (e.OrderAmt - e.OrderCost).ToString();
                var r = (e.OrderAmt - e.OrderCost) / (e.OrderAmt == 0 ? 1 : e.OrderAmt) * 100;
                ProfitRate.Text = r.ToString("f2");

                OrderCollectedAmt.Text = (e.CollectedAmt + e.ToConfirmCollectedAmt).ToString("f2");
                OrderUnCollected.Text = (e.OrderAmt - e.CollectedAmt - e.ToConfirmCollectedAmt).ToString("f2");

                //客户信息
                rptCustomer.DataSource = new Order_BF().GetOrderCustomer(e.ID);
                rptCustomer.DataBind();

                BindFlightInfo(e);

                rptCost.DataSource = new Order_BF().GetTHSKOrderCost(e.ID);
                rptCost.DataBind();

                Page.Title = e.OrderName + "-机票订单";
            }
            ToWordAction(Page.Title);
        }

        /// <summary>
        /// 绑定航班信息
        /// </summary>
        /// <param name="e"></param>
        private void BindFlightInfo(DAL.Ord_TicketOrder e)
        {
            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<td style=\"text-align: center; font-weight: bold;\">去程</td>");
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.ToFlightLeg);
            sb.AppendFormat("<td>{0}</td>", e.ToFlightInfo);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.ToAirport);
            sb.AppendFormat("<td>{0}</td>", e.ToAirLine);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.ToCabin);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.ToTicketPrice);
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td style=\"text-align: center; font-weight: bold;\">回程</td>");
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.FromFlightLeg);
            sb.AppendFormat("<td>{0}</td>", e.FromFlightInfo);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.FromAirport);
            sb.AppendFormat("<td>{0}</td>", e.FromAirLine);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.FromCabin);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e.FromTicketPrice);
            sb.Append("</tr>");

            lblFlight.Text = sb.ToString();
        }

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
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
                return "anonymous";
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