using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.ProMrg;
using DRP.BF.Quotation;

namespace DRP.WEB.Module.T
{
    public partial class RouteEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindParameter();
                BindData();
            }
        }
 

        private void BindParameter()
        {
            var dal = new BasicInfo_BF();

            RouteType.DataSource = dal.GetBasicInfo(BasicType.Pro_RouteType);
            RouteType.DataTextField = "Name";
            RouteType.DataValueField = "ID";
            RouteType.DataBind();

            //Stay.DataSource = dal.GetBasicInfo(BasicType.Pro_QuotationStay);
            //Stay.DataValueField = Stay.DataTextField = "Name";
            //Stay.DataBind();

            //Dinner.DataSource = dal.GetBasicInfo(BasicType.Pro_QuotationDinner);
            //Dinner.DataValueField = Dinner.DataTextField = "Name";
            //Dinner.DataBind();
        }

        private void BindData()
        {
            var e = new Quotation_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                RouteType.SelectedValue = e.RouteTypeID;
                BindSchedule();
                BindServiceItem();
            } 
        }
         

        #region << 线路行程 >>

        private void BindSchedule()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                rptSchedule.DataSource = new Quotation_BF().GetRouteSchedule(KeyID);
                rptSchedule.DataBind();
            }
        }

        protected void rptSchedule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblDayNum") as Literal;
                lblNo.Text = DataBinder.Eval(e.Item.DataItem, "DayNum").ToString();
            }
        }

        #endregion

        protected override string NavigateID
        {
            get
            {
                return "routetemplate";
            }
        }


        #region << 服务标准 >>
        protected void rptItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblItemName = e.Item.FindControl("lblItemName") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                var itemName = DataBinder.Eval(e.Item.DataItem, "ItemName").ToString();
                var sb = new StringBuilder();
                sb.Append("<select class='textbox' style='width:140px;height:26px;'>");
                sb.Append(AddListItem("门票",itemName));
                sb.Append(AddListItem("住宿", itemName));
                sb.Append(AddListItem("用餐", itemName));
                sb.Append(AddListItem("全陪", itemName));
                sb.Append(AddListItem("地陪", itemName));
                sb.Append(AddListItem("交通(飞机)", itemName));
                sb.Append(AddListItem("交通(船)", itemName));
                sb.Append(AddListItem("交通(火车)", itemName));
                sb.Append(AddListItem("交通(汽车)", itemName));
                sb.Append(AddListItem("交通(接送)", itemName));
                sb.Append(AddListItem("保险(责任险)", itemName)); 
                sb.Append(AddListItem("保险(意外险)", itemName));
                sb.Append(AddListItem("保险(航空险)", itemName));
                sb.Append(AddListItem("其他", itemName));  
                sb.Append("</select>");
                lblItemName.Text = sb.ToString();
            }
        }

        private void BindServiceItem()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                rptItem.DataSource = new Quotation_BF().GetQuotationCostItem(KeyID);
                rptItem.DataBind();
            }
        }

        private string AddListItem(string name,string matchName)
        {
            if (!name.Equals(matchName))
                return string.Format("<option value='{0}'>{0}</option>", name);
            else
                return string.Format("<option selected='selected' value='{0}'>{0}</option>",name);
        }

        #endregion
    }
}