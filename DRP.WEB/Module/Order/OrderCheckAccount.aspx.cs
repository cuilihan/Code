using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.Framework;

namespace DRP.WEB.Module.Order
{
    public partial class OrderCheckAccount : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            rptData.DataSource = new OrderGuide_BF().GetOrderGuide(KeyID);
            rptData.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                var xType = Request["xType"].ToInt();
                return xType == (int)OrderType.QYT ? "salesorderqy" : "salesorderzzb";
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblStatus = e.Item.FindControl("lblStatus") as Literal;
                var lblAction = e.Item.FindControl("lblAction") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                var strIsOver = DataBinder.Eval(e.Item.DataItem, "IsOver");
                var isOver =strIsOver==null?0:strIsOver.ToString().ToInt();
                var id = DataBinder.Eval(e.Item.DataItem, "ID").ToString();
                var strBalanceID= DataBinder.Eval(e.Item.DataItem, "OrderBalanceID");
                var orderBalanceID = strBalanceID == null ? "0" : strBalanceID.ToString();
                var lnkView = string.Format("<a href='/Module/CheckAccount/ViewBill.aspx?id={0}&xType={1}&orderBalanceID={2}' target='_blank'>查看</a>",
                    id, Request["xType"], orderBalanceID);
                var lnkEdit = string.Format("<a href='/Module/CheckAccount/Index.aspx?id={0}&orderBalanceID={1}&xType={2}&t=1' target='_blank'>修改</a>",
                    id, orderBalanceID, Request["xType"]);
                var btn = string.Format("<a href='javascript:;' onclick=\"fnConfirmed('{0}')\">确认</a>", id);
                switch (isOver)
                {
                    case 0://
                        lblStatus.Text = "未报账";
                        break;
                    case 1:
                        lblStatus.Text = "已报账";
                        lblAction.Text = lnkEdit + " | " + lnkView + " | " + btn;
                        break;
                    case 2:
                        lblStatus.Text = "已确认";
                        lblAction.Text = lnkView;
                        break;
                }
            }
        }
    }
}