using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.SysMrg;
using DRP.BF.OmMrg;
using DRP.BF.Fin;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin
{
    public partial class PayableAction : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDept();
            }
        }

        private void BindDept()
        {
            ddlDept.DataSource = new Dept_BF().GetDepartment();
            ddlDept.DataValueField = "ID";
            ddlDept.DataTextField = "Name";
            ddlDept.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.EffectiveData);
            if (en != null && en.effectiveData > DateTime.Now)
            {
                var qry = new PayableItemCriterial();
                qry.OrderName = txtName.Text;
                qry.sDate = sDate.Text;
                qry.eDate = eDate.Text;
                qry.UserName = ddlCreator.Text;
                qry.OrderNo = txtOrderNo.Text;
                qry.DeptID = ddlDept.SelectedValue;
                qry.SupplierID = Request["id"];

                var list = new List<NExcelCellFormat>();
                list.Add(new NExcelCellFormat(10, "OrderName", "订单名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(10, "OrderNo", "订单编号", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(10, "TourDate", "出团日期", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(10, "CustomerName", "客户名称", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(10, "CostAmt", "应付款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(10, "PaidAmt", "已付款", NExcelCellFormatStyle.Double));
                list.Add(new NExcelCellFormat(10, "Name", "部门", NExcelCellFormatStyle.Normal));
                list.Add(new NExcelCellFormat(10, "CreateUserName", "操作人", NExcelCellFormatStyle.Normal));

                var dt = new OrderPayable_BF().QueryOrder_All(qry);
                //查询条件输出
                new NewExcelHelper().RptData_NExcel("散客订单列表", "", dt, this.Response, this.Request, list, true);
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "finpayable";
            }
        }
    }
}