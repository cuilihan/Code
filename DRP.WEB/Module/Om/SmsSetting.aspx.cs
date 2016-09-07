using DRP.BF;
using DRP.BF.OmMrg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Om
{
    public partial class SmsSetting : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OrgInfo_BF().Get(Request["OrgID"]);
            if (e != null)
            {
                lblOrgInfo.Text = e.Name;
                lblSmsCount.Text = string.IsNullOrEmpty(e.SmsCount.ToString()) ? "0" : e.SmsCount.ToString();
                lblSmsSendCount.Text = e.SendSmsCount == null ? "0" : e.SendSmsCount.ToString();
                var a = (e.SmsCount == null ? 0 : (int)e.SmsCount) - (e.SendSmsCount == null ? 0 : (int)e.SendSmsCount);
                lblDeltSms.Text = a.ToString();

                var list = new OmSms_BF().GetSmsPlatform(e.ID);
                rptData.DataSource = list;
                rptData.DataBind();

                var total = 0;
                list.ForEach(x => 
                {
                    total += x.Amount;
                });
                lblAmount.Text = total.ToString();
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }
    }
}