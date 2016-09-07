using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Crm;

namespace DRP.WEB.Module.Crm
{
    public partial class CustomerInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new Customer_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
                BindCertificate(e.ID);
        }

        private void BindCertificate(string customerID)
        {
           rptCert.DataSource= new Customer_BF().GetCertificate(customerID);
           rptCert.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                return "customer";
            }
        }
    }
}