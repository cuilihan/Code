using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Crm;
using DRP.BF.Glo;
using DRP.BF.SysMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Crm
{
    public partial class Customer : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetButtonPermission();
                BindCustomerType();
                BindDept();
            }
        }

        private void BindDept()
        {
            ddlDeptID.DataSource = new Dept_BF().GetDepartment();
            ddlDeptID.DataTextField = "Name";
            ddlDeptID.DataValueField = "ID";
            ddlDeptID.DataBind();
        }

        private void BindCustomerType()
        {
            ddlCustomerType.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Crm_CustomerType);
            ddlCustomerType.DataTextField = "Name";
            ddlCustomerType.DataValueField = "Name";
            ddlCustomerType.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "customer";
            }
        }

        private void SetButtonPermission()
        {
            var p = new Permission_BF();
          //  btnImp.Visible = p.HasButtonPermission(1);
            btnExport.Visible = p.HasButtonPermission(2);
            btnDel.Visible = p.HasButtonPermission(4);
        }

        /// <summary>
        /// 导出客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            var qry = new CustomCriteria();
            qry.Name = txtName.Text;
            qry.DeptID = ddlDeptID.SelectedValue;
            qry.CreatorID = ddlUserID.SelectedValue;

            var isOk = new Customer_BF().Export(qry);
            if (isOk)
            {
                JScript.Alert("导出客户资料失败");
            }
        }
    }
}