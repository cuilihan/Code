using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.BF.OmMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Order
{
    public partial class TeamOrderList : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetCollectedSignStatus();
                BindOrderSource();
                BindDept();

                var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (en != null && en.xType == 5 && en.xVal == 1)
                {
                    hidePart.Value = "1";
                    sPart.Style.Value = "";
                }
            }
        }

        private void BindDept()
        {
            ddlDept.DataSource = new Dept_BF().GetDepartment();
            ddlDept.DataValueField = "ID";
            ddlDept.DataTextField = "Name";
            ddlDept.DataBind();
        }

        /// <summary>
        /// 设置收款登记功能的功能
        /// </summary>
        private void SetCollectedSignStatus()
        {
            var e = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.OrderCollectedSign);
            if (e != null && e.xVal == 1) btnCollectedSign.Visible = true;
            else btnCollectedSign.Visible = false;
        }

        private void BindOrderSource()
        {
            ddlOrderSource.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_OrderSource);
            ddlOrderSource.DataTextField = "Name";
            ddlOrderSource.DataValueField = "ID";
            ddlOrderSource.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.EffectiveData);
            if (en != null && en.effectiveData > DateTime.Now)
            {
                var qry = new OrderCriteria();
                qry.OrdType = (OrderType)3;
                qry.OrderName = txtOrderName.Text;
                qry.OrderNo = txtOrderNo.Text;
                qry.CusotmerName = txtCustomer.Text;
                qry.Supplier = txtSupplier.Text;
                qry.DateType = ddlDateType.SelectedValue.ToInt();
                var dateScope = new DateScope();
                dateScope.sDate = sDate.Text;
                dateScope.eDate = eDate.Text;
                qry.QueryDateScope = dateScope;
                qry.RouteTypeID = Request.Form["RouteTypeID"];
                qry.DestinationID = Request.Form["DestinationID"];
                qry.OrdStatus = (OrderStatus)ddlOrderStatus.SelectedValue.ToInt();
                qry.DeptID = this.hideDept.Value;
                qry.CreateUserID = this.hideCreator.Value;
                qry.OrderSourceName = this.hideOrderSource.Value;
                if (this.CanceledOrder.Checked)
                {
                    qry.OrdStatus = (OrderStatus)4;
                }

                var list = new List<NExcelCellFormat>();
                list.Add(new NExcelCellFormat(10, "OrderNo", "编号", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(8, "OrderName", "订单名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "tDate", "出团日期", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "CustomerName", "客户名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "Company", "公司名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(2, "TourDays", "天数", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(2, "AdultNum", "成人", NExcelCellFormatStyle.Int));
                list.Add(new NExcelCellFormat(2, "ChildNum", "儿童", NExcelCellFormatStyle.Int));
                list.Add(new NExcelCellFormat(6, "OrderAmt", "应收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "CollectedAmt", "已收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "UnCollectedAmt", "未收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "OrderCost", "成本", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "Profit", "毛利", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "P", "毛利率", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(8, "CostInvoiceAmt", "供应商发票金额", NExcelCellFormatStyle.Double));
                string[] OrderStatusStrs = { "1|待确认", "2|已确认", "3|已完成", "4|已取消" };
                list.Add(new NExcelCellFormat(6, "OrderStatus", "订单状态", NExcelCellFormatStyle.Normal, OrderStatusStrs));
                list.Add(new NExcelCellFormat(8, "OrderInvoiceAmt", "开票状态", NExcelCellFormatStyle.Normal));
                string[] BudgetStatusStrs = { "1|未预算", "4|已预算", "5|已决算", "7|决算确认" };
                list.Add(new NExcelCellFormat(6, "BudgetStatus", "预决算状态", NExcelCellFormatStyle.Normal, BudgetStatusStrs));
                list.Add(new NExcelCellFormat(6, "CreateUserName", "提交人", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "SourceName", "订单来源", NExcelCellFormatStyle.Normal));   
                var entity = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (entity != null && entity.xType == 5 && entity.xVal == 1)
                {
                    list.Add(new NExcelCellFormat(6, "Participant", "参与人员", NExcelCellFormatStyle.Normal));
                }

                list.Add(new NExcelCellFormat(6, "GuideName", "安排导游", NExcelCellFormatStyle.Normal));
                string[] IsCheckAccountStrs = { "True|已报账", "False|" };
                list.Add(new NExcelCellFormat(6, "IsCheckAccount", "报账状态", NExcelCellFormatStyle.Normal, IsCheckAccountStrs));
                string[] IsCloseCollectedStrs = { "1|已结清", "0|" };
                list.Add(new NExcelCellFormat(6, "IsCloseCollected", "余款结清", NExcelCellFormatStyle.Normal, IsCloseCollectedStrs));

                var dt = new Order_BF().QueryOrder_All(qry);
                //查询条件输出
                new NExcelHelper().RptData_NExcel("企业团订单列表", "", dt, this.Response, this.Request, list, true);
            }
            else
            {
                JScript.Alert("您没有权限！请联系在线客服！");
            }
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