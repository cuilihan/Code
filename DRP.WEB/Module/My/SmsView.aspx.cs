using DRP.BF;
using DRP.Message.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.My
{
    public partial class SmsView : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e=new SmsBiz().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected override string NavigateID
        {
            get
            {
                return "mysms";
            }
        }
    }
}