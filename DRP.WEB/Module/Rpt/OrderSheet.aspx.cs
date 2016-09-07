using DRP.BF;
using DRP.BF.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;
using DRP.BF.RptMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Rpt
{
    public partial class OrderSheet : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string NavigateID
        {
            get
            {
                return "rptordersheet";
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            var qry = new OrderCriteria();
            qry.OrderName = txtOrderName.Text;
            qry.OrderNo = txtOrderNo.Text;
            qry.OrdType = (OrderType)ddlOrderType.SelectedValue.ToInt();
            qry.CusotmerName = txtCustomer.Text;
            qry.Supplier = txtSupplier.Text;
            qry.DateType = 1;//出团日期
            var dateScope = new DateScope();
            dateScope.sDate = sDate.Text;
            dateScope.eDate = eDate.Text;
            qry.QueryDateScope = dateScope;
            if (this.CanceledOrder.Checked)
            {
                qry.OrdStatus = (OrderStatus)4;
            }
            var isOk = new RptUtility_BF().OrderSheetExport(qry);
            if (!isOk) JScript.Alert("导出数据失败");
        }
    }
}