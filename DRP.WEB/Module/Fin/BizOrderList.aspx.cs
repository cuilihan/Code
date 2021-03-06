﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.SysMrg;
using DRP.Framework.Core;
using DRP.BF.Order;
using DRP.Framework;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Fin
{
    public partial class BizOrderList : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        private void BindOrderSource()
        {
            ddlOrderSource.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_SingleBizType);
            ddlOrderSource.DataTextField = "Name";
            ddlOrderSource.DataValueField = "ID";
            ddlOrderSource.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "finorderbiz";
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.EffectiveData);
            if (en != null && en.effectiveData > DateTime.Now)
            {
                var qry = new OrderCriteria();
                qry.OrdType = (OrderType)5;
                qry.OrderName = txtOrderName.Text;
                qry.OrderNo = txtOrderNo.Text;
                qry.CusotmerName = txtCustomer.Text;
                qry.Supplier = txtSupplier.Text;
                qry.DateType = ddlDateType.SelectedValue.ToInt();
                var dateScope = new DateScope();
                dateScope.sDate = sDate.Text;
                dateScope.eDate = eDate.Text;
                qry.QueryDateScope = dateScope;
                qry.OrdStatus = (OrderStatus)ddlOrderStatus.SelectedValue.ToInt();
                qry.DeptID = ddlDept.SelectedItem.Value;
                qry.CreateUserID = ddlCreator.SelectedItem.Value;
                qry.OrderSourceName = ddlOrderSource.SelectedItem.Text;

                var list = new List<NExcelCellFormat>();
                list.Add(new NExcelCellFormat(10, "OrderNo", "编号", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(4, "SourceName", "类型", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(8, "OrderName", "订单名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "tDate", "订单日期", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(10, "SupplierName", "供应商", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "CustomerName", "客户", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "Company", "公司名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(6, "OrderAmt", "应收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "CollectedAmt", "已收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "ToConfirmCollectedAmt", "待确认收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "UnCollectedAmt", "未收款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "OrderCost", "成本", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "Profit", "毛利", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(6, "P", "毛利率", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(8, "CostInvoiceAmt", "供应商发票金额", NExcelCellFormatStyle.Double));
                string[] strs = { "1|待确认", "2|已确认", "3|已完成", "4|已取消" };
                list.Add(new NExcelCellFormat(6, "OrderStatus", "订单状态", NExcelCellFormatStyle.Normal, strs));
                list.Add(new NExcelCellFormat(8, "OrderInvoiceAmt", "开票状态", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(5, "CreateUserName", "提交人", NExcelCellFormatStyle.Normal)); 
                var entity = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.Participant);
                if (entity != null && entity.xType == 5 && entity.xVal == 1)
                {
                    list.Add(new NExcelCellFormat(6, "Participant", "参与人员", NExcelCellFormatStyle.Normal));
                }

                list.Add(new NExcelCellFormat(6, "cDate", "下单日期", NExcelCellFormatStyle.Normal));

                var dt = new Order_BF().QueryOrder_All(qry);
                //查询条件输出
                new NExcelHelper().RptData_NExcel("单项业务订单列表", "", dt, this.Response, this.Request, list, true);
            }
            else
            {
                JScript.Alert("您没有权限！请联系在线客服！");
            }
        }
    }
}