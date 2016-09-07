using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Crm;
using DRP.BF.CrmMrg;
using DRP.BF.Glo;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Crm
{
    public partial class TraceEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItemType();
                BindData();
            }
        }

        private void BindItemType()
        {
            ItemType.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Crm_SalesTraceType);
            ItemType.DataTextField = ItemType.DataValueField = "Name";
            ItemType.DataBind();
        }

        private void BindData()
        {
            var e = new CustomerTrace_BF().Get(KeyID);
            if (e != null)
            {
                LoadData(e, pnlContainer);
            }
            else
            {
                var customer = new Customer_BF().Get(Request["customerID"]);
                if (customer != null)
                    Contact.Text = customer.Name;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new CustomerTrace_BF().Save(KeyID, Request["customerID"], pnlContainer);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        /// <summary>
        /// 允许匿名访问
        /// </summary>
        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}