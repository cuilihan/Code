using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.My
{
    public partial class MyInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            if (e != null)
            {
                OrgName.Text = e.Name;
                OrgDomain.Text = e.ProDomain;
                DateLimit.Text = e.OpenDate.ToString("yyyy-MM-dd") + " / " + e.ExpiryDate.ToString("yyyy-MM-dd");
                if (e.DataStatus == -1)
                {
                    DataStatus.Text = "未审核";
                }
                else
                {
                    var ts = new TimeSpan();
                    ts = e.OpenDate - DateTime.Today;
                    if (ts.Days > 0) DataStatus.Text = "尚未启用";
                    else
                    {
                        ts = e.ExpiryDate - DateTime.Today;
                        if (ts.Days < 0)
                            DataStatus.Text = "已过期";
                        else
                            DataStatus.Text = "正常";
                    }
                }
                UserCount.Text = e.MaxUserCount.ToString();
                lblCompany.Text = string.Format("<a href='http://www.58datu.com' target='_blank'>{0}</a>", ConfigHelper.GetAppSettingValue("OrgName"));
                var a = (e.SmsCount - e.SendSmsCount);
                SmsCount.Text = a > 0 ? a.ToString() : "0";
                TotalAmt.Text = e.ReceiptAmt.ToString();

                OrgID.Value = e.ID;
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "index";
            }
        }
    }
}