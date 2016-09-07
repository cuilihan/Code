using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Crm;
using DRP.BF.CrmMrg;
using DRP.BF.Glo;

namespace DRP.WEB.Module.Crm
{
    public partial class CustomerEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustomerType();
                BindData();
            }
        }

        private void BindData()
        {
            var e = new Customer_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                if (e.Mobile == "")
                {
                    haveMobile.Checked = true;
                }
                Comment.Text = e.Remark;
                BindCertificate(e.ID);
            }
            BindTrace();
        }



        private void BindCustomerType()
        {
            CustomerType.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Crm_CustomerType);
            CustomerType.DataTextField = CustomerType.DataValueField = "Name";
            CustomerType.DataBind();
        }

        /// <summary>
        /// 绑定客户的证件类型
        /// </summary>
        /// <param name="customerID"></param>
        private void BindCertificate(string customerID)
        { 
            rptCertificate.DataSource = new Customer_BF().GetCertificate(customerID);
            rptCertificate.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "customer";
            }
        }

        /// <summary>
        /// 绑定客户销售线索记录
        /// </summary>
        private void BindTrace()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                rptTrace.DataSource = new CustomerTrace_BF().GetCusotmerTrace(KeyID);
                rptTrace.DataBind();
            }
        }

        /// <summary>
        /// 绑定销售类型
        /// </summary>
        private string BindItemType(string val)
        {
            var sb = new StringBuilder();
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Crm_SalesTraceType);
            sb.Append("<select style='width:80px;height:26px;'>");
            sb.Append("<option value=''>请选择</option>");
            list.ForEach(x =>
            {
                var opt = "<option value='" + x.Name + "'>" + x.Name + "</option>";
                if (x.Name == val)
                    opt = "<option value='" + x.Name + "' selected='selected'>" + x.Name + "</option>";
                sb.Append(opt);
            });
            sb.Append("</select>");
            return sb.ToString();
        }

        private string ItemType(string val)
        {
            var obj = ViewState["ItemType"];
            if (obj == null)
            {
                var s = BindItemType(val);
                ViewState["ItemType"] = obj;
                return s;
            }
            else
                return obj.ToString();
        }

        protected void rptTrace_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblItemType = e.Item.FindControl("lblItemType") as Literal;
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                var lblTradeDate = e.Item.FindControl("lblTradeDate") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                lblItemType.Text = ItemType(DataBinder.Eval(e.Item.DataItem, "ItemType").ToString());
                var d = DataBinder.Eval(e.Item.DataItem, "TradeDate");
                if (d != null)
                {
                    if (!string.IsNullOrEmpty(d.ToString()))
                        d = Convert.ToDateTime(d).ToString("yyyy-MM-dd");
                }
                lblTradeDate.Text = string.Format("<input type=\"text\" class=\"textbox\" style=\"width: 90px;height:26px;\"  onclick='WdatePicker()' value='{0}' />", d);
            }
        }
    }
}