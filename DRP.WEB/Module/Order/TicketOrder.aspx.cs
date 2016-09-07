using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.Order;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;

namespace DRP.WEB.Module.Order
{
    public partial class TicketOrder : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetCollectedSignStatus();

            var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
            if (en != null && en.xType == 5 && en.xVal == 1)
            {
                hidePart.Value = "1";
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "ticketorder";
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.EffectiveData);
            if (en != null && en.effectiveData > DateTime.Now)
            {
                var qry = new TicketOrderCriteria();
                qry.OrderName = txtOrderName.Text;
                qry.PNR = txtPNR.Text;
                qry.sDate = sDate.Text;
                qry.eDate = eDate.Text;
                qry.Contact = txtContact.Text;
                qry.Supplier = txtSupplier.Text;
                qry.FlightLeg = txtFlightInfo.Text;
                qry.OrdStatus = (OrderStatus)ddlOrderStatus.SelectedItem.Value.ToInt();
                qry.csDate = cSData.Text;
                qry.ceDate = cEDate.Text;

                var list = new List<NExcelCellFormat>();
                list.Add(new NExcelCellFormat(6, "PNR", "PNR编号", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(10, "OrderName", "订单名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "ToFlightLeg", "行程", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(15, "ToFlightInfo", "航班信息", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "TourDate", "航班日期", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(2, "AdultNum", "人数", NExcelCellFormatStyle.Int));
                list.Add(new NExcelCellFormat(12, "SupplierName", "供应商", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "OrderAmt", "应收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "CollectedAmt", "已收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "UnCollectedAmt", "未收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "OrderCost", "成本", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "Profit", "毛利", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "P", "毛利率", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(8, "CostInvoiceAmt", "供应商发票金额", NExcelCellFormatStyle.Double));
                string[] strs = { "1|待确认", "2|已确认", "3|已完成", "4|已取消" };
                list.Add(new NExcelCellFormat(5, "OrderStatus", "订单状态", NExcelCellFormatStyle.Normal, strs));
                list.Add(new NExcelCellFormat(8, "OrderInvoiceAmt", "开票状态", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "Contact", "订票联系人", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "ContactPhone", "联系电话", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(4, "CreateUserName", "提交人", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "Company", "公司名称", NExcelCellFormatStyle.Normal));

                var entity = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (entity != null && entity.xType == 5 && entity.xVal == 1)
                {
                    list.Add(new NExcelCellFormat(6, "Participant", "参与人员", NExcelCellFormatStyle.Normal));
                }

                list.Add(new NExcelCellFormat(10, "CreateDate", "下单日期", NExcelCellFormatStyle.Normal));

                var dt = new TicketOrder_BF().QueryOrder_All(qry);
                //查询条件输出
                new NExcelHelper().RptData_NExcel("机票订单列表", "", dt, this.Response, this.Request, list, true);
            }
            else
            {
                JScript.Alert("您没有权限！请联系在线客服！");
            }
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
    }
}