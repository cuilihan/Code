using DRP.BF;
using DRP.BF.Order;
using DRP.BF.ResMrg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;
using System.Data;

namespace DRP.WEB.Module.Order
{
    public partial class TicketOrderEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                BindFile(KeyID);
            }
        }

        private void BindData()
        {
            var dal = new TicketOrder_BF();
            var e = dal.Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                Employee.Value = e.Participant;
                Dept.Value = e.DeptName;
                hfDept.Value = e.PartDeptID;
                hfEmployee.Value = e.ParticipantID;

                //客户信息
                rptCustomer.DataSource = new Order_BF().GetOrderCustomer(e.ID);
                rptCustomer.DataBind();

                //成本
                rptCost.DataSource =new Order_BF().GetOrderCost(e.ID);
                rptCost.DataBind();

                BindFlightInfo(e);
            }
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
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 90px; height: 26px;\" class=\"textbox\" value='{0}' /></td>",e.ToFlightLeg);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 96%; height: 26px;\" class=\"textbox\" value='{0}' /></td>",e.ToFlightInfo);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 80px; height: 26px;\" class=\"textbox\"  value='{0}' /></td>",e.ToAirport);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 130px; height: 26px;\" class=\"textbox\" value='{0}' /></td>",e.ToAirLine);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 70px; height: 26px;\" class=\"textbox\" value='{0}' /></td>",e.ToCabin);
            sb.AppendFormat("<td><input type=\"text\" name='txtPrice' style=\"width: 60px; height: 26px;\" class=\"textbox\" value='{0}' /></td>",e.ToTicketPrice);
            sb.AppendFormat("<td style='text-align:center;'><a href='javascript:;' onclick=\"c.fnDeleteFlight(this)\">删除</a></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td style=\"text-align: center; font-weight: bold;\">回程</td>");
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 90px; height: 26px;\" class=\"textbox\" value='{0}' /></td>", e.FromFlightLeg);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 96%; height: 26px;\" class=\"textbox\" value='{0}' /></td>", e.FromFlightInfo);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 80px; height: 26px;\" class=\"textbox\"  value='{0}' /></td>", e.FromAirport);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 130px; height: 26px;\" class=\"textbox\" value='{0}' /></td>", e.FromAirLine);
            sb.AppendFormat("<td><input type=\"text\" style=\"width: 70px; height: 26px;\" class=\"textbox\" value='{0}' /></td>", e.FromCabin);
            sb.AppendFormat("<td><input type=\"text\" name='txtPrice' style=\"width: 60px; height: 26px;\" class=\"textbox\" value='{0}' /></td>", e.FromTicketPrice);
            sb.AppendFormat("<td style='text-align:center;'><a href='javascript:;' onclick=\"c.fnDeleteFlight(this)\">删除</a></td>");
            sb.Append("</tr>");

            lblFlight.Text = sb.ToString();
        }

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblSex = e.Item.FindControl("lblSex") as Literal;
                var lblIDType = e.Item.FindControl("lblIDType") as Literal;

                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                var sex = DataBinder.Eval(e.Item.DataItem, "Sex").ToString();
                var s = "<select style='width:50px; height:26px;'>";
                if (sex.Equals("男"))
                {
                    s += "<option value=\"男\" selected='selected'>男</option><option value=\"女\">女</option>";
                }
                else
                    s += "<option value=\"男\" >男</option><option value=\"女\" selected='selected'>女</option>";
                s += "</select>";
                lblSex.Text = s;

                var sb = new StringBuilder();
                var t = TicketType.SelectedValue.Trim() == "国内" ? "0" : "1";
                var m_v = DataBinder.Eval(e.Item.DataItem, "IDType").ToString();
                sb.Append("<select style='width:120px;height:26px;' onchange=\"c.fnGetCertificate('" + t + "',this)\">");
                if (t == "0")
                    sb.Append(AppendOption("身份证", m_v));
                else
                {
                    sb.Append("<option value=''>请选择</option>");
                    sb.Append(AppendOption("护照", m_v));
                    sb.Append(AppendOption("台胞证", m_v));
                    sb.Append(AppendOption("回乡证", m_v));
                    sb.Append(AppendOption("港澳通行证", m_v));
                    sb.Append(AppendOption("海员证", m_v));
                    sb.Append(AppendOption("大陆往来台湾通行证", m_v));
                    sb.Append(AppendOption("其他", m_v));
                }
                sb.Append("</select>");
                lblIDType.Text = sb.ToString();
            }
        }

        private string AppendOption(string v, string m_v)
        {
            if (v == m_v)
                return string.Format("<option value='{0}' selected='selected'>{0}</option>", v);
            else
                return string.Format("<option value='{0}'>{0}</option>", v);
        }

        protected override string NavigateID
        {
            get
            {
                return "ticketorder";
            }
        }

        protected void rptCost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();

                var lblSupplier = e.Item.FindControl("lblSupplier") as Literal;
                var itemType = DataBinder.Eval(e.Item.DataItem, "ItemType").ToString().ToInt();
                var supplierID = DataBinder.Eval(e.Item.DataItem, "SupplierID").ToString();
                var dt = new ResourceUtility().QueryResource(itemType);
                lblSupplier.Text = BindResource(dt, supplierID);
            }
        }

        private string BindResource(DataTable dt, string supplierID)
        {
            var sb = new StringBuilder();
            sb.Append("<select name='supplier' style='width:240px; height:26px;'>");
            sb.Append("<option value=''>请选择</option>");
            foreach (DataRow row in dt.Rows)
            {
                var id = row["ID"].ToString();
                var spell = row["Spell"].ToString();
                var name = row["Name"].ToString();
                var opt = "<option value='" + id + "'>" + spell + "-" + name + "</option>";
                if (id == supplierID)
                    opt = "<option value='" + id + "' selected='selected'>" + spell + "-" + name + "</option>";
                sb.Append(opt);
            }
            sb.Append("</select>");
            return sb.ToString();
        }

        private void BindFile(string orderID)
        {
            rptFile.DataSource = new Order_BF().GetOrderFile(orderID);
            rptFile.DataBind();
        }
    }
}