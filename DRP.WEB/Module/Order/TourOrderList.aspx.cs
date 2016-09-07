using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.BF.Order;
using DRP.Framework.Core;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module
{
    public partial class TourOrderList : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.EffectiveData);
            if (en != null && en.effectiveData > DateTime.Now)
            {
                var qry = new OrderCriteria();
                qry.OrdType = OrderType.ZZBT;
                qry.OrderName = txtOrderName.Text;
                qry.OrderNo = txtOrderNo.Text;
                qry.DateType = 2;
                var dateScope = new DateScope();
                dateScope.sDate = sDate.Text;
                dateScope.eDate = eDate.Text;
                qry.QueryDateScope = dateScope;
                qry.RouteTypeID = Request.Form["RouteTypeID"];
                qry.DestinationID = Request.Form["DestinationID"];
                qry.OrdStatus = OrderStatus.All;

                var list = new List<NExcelCellFormat>();
                list.Add(new NExcelCellFormat(10, "OrderNo", "编号", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(4, "DestinationName", "目的地", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(15, "OrderName", "订单名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "tDate", "出团日期", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "PlanNum", "计划人数", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "VisitorNum", "报名人数", NExcelCellFormatStyle.Normal));
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
                list.Add(new NExcelCellFormat(8, "OrderInvoiceAmt", "开票状态", NExcelCellFormatStyle.Normal));
                string[] BudgetStatusStrs = { "1|未预算", "4|已预算", "5|已决算", "7|决算确认" };
                list.Add(new NExcelCellFormat(6, "BudgetStatus", "预决算状态", NExcelCellFormatStyle.Normal, BudgetStatusStrs));
                list.Add(new NExcelCellFormat(6, "GuideName", "安排导游", NExcelCellFormatStyle.Normal));
                string[] IsCheckAccountStrs = { "True|已报账", "False|" };
                list.Add(new NExcelCellFormat(6, "IsCheckAccount", "报账状态", NExcelCellFormatStyle.Normal, IsCheckAccountStrs));
                string[] IsCloseCollectedStrs = { "1|已结清", "0|" };
                list.Add(new NExcelCellFormat(6, "IsCloseCollected", "余款结清", NExcelCellFormatStyle.Normal, IsCloseCollectedStrs));

                var dt = new Order_BF().QueryOrder_All(qry);
                //查询条件输出
                new NExcelHelper().RptData_NExcel("自主班团订单列表", "", dt, this.Response, this.Request, list, true);
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
                return "salesorderzzb";
            }
        }
    }
}