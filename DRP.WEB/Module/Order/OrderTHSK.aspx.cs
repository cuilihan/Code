using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Order
{
    public partial class OrderTHSK : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFile(KeyID);
                BindParameter();
                BindData();
                var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (en != null && en.xType == 5 && en.xVal == 1)
                {
                    Participant.Style.Value = "";
                }
            }
        }

        private void BindParameter()
        {
            SourceID.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_OrderSource);
            SourceID.DataTextField = "Name";
            SourceID.DataValueField = "ID";
            SourceID.DataBind();
        }

        private void BindData()
        {
            var dal = new Order_BF();
            var e = dal.GetOrderInfo(KeyID);
            
            LoadData(e, pnlContainer);
            if (e != null)
            {
                Employee.Value = e.Participant;
                Dept.Value = e.DeptName;
                hfDept.Value = e.PartDeptID;
                hfEmployee.Value = e.ParticipantID;
                hfRouteTypeID.Value = e.RouteTypeID;
                hfDestinationID.Value = e.DestinationID;
                SourceID.SelectedValue = e.SourceID;
                OrderStatus.Checked = e.OrderStatus != 1;

                rptCustomer.DataSource = dal.GetOrderCustomer(e.ID);
                rptCustomer.DataBind();

                rptCost.DataSource = dal.GetOrderCost(e.ID);
                rptCost.DataBind();
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
                return "salesordersk";
            }
        }

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblSex = e.Item.FindControl("lblSex") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                lblSex.Text = "<select style='width:50px; height:26px;'>";
                if (DataBinder.Eval(e.Item.DataItem, "Sex").Equals("男"))
                    lblSex.Text += "<option value=\"男\" selected='selected'>男</option><option value=\"女\">女</option>";
                else
                    lblSex.Text += "<option value=\"男\" >男</option><option selected='selected' value=\"女\">女</option>";
                lblSex.Text += "</select>";
            }
        }

        protected void rptCost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblSupplier = e.Item.FindControl("lblSupplier") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                var sb = new StringBuilder();
                sb.Append("<select name='supplier' style='width:240px; height:26px;'>");
                sb.Append("<option value=''>请选择</option>");
                var itemType = DataBinder.Eval(e.Item.DataItem, "ItemType").ToString().ToInt();
                var dt = new ResourceUtility().QueryResource(itemType);
                var supplierID = DataBinder.Eval(e.Item.DataItem, "SupplierID").ToString();
                foreach (DataRow row in dt.Rows)
                {
                    var id = row["ID"].ToString();
                    var spell = row["Spell"].ToString();
                    var name = row["Name"].ToString();
                    var opt = "<option value='" + id + "'>" + spell + "-" + name + "</option>";
                    if (id == supplierID)
                        opt = "<option selected='selected' value='" + id + "'>" + spell + "-" + name + "</option>";
                    sb.Append(opt);
                }
                sb.Append("</select>");
                lblSupplier.Text = sb.ToString();

            }
        }
    }
}