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
    public partial class OrderBiz : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindParameter();
                BindFile(KeyID);
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
            SourceID.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_SingleBizType);
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

                OrderStatus.Checked = e.OrderStatus != 1;

                rptCustomer.DataSource = dal.GetOrderCustomer(e.ID);
                rptCustomer.DataBind();

                rptCost.DataSource = dal.GetOrderCost(e.ID);
                rptCost.DataBind();
            }           
        }

        protected override string NavigateID
        {
            get
            {
                return "salesbizorder";
            }
        }

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblSex = e.Item.FindControl("lblSex") as Literal;
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
                if(id==supplierID)
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