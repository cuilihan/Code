using DRP.BF.eShop;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop
{
    public partial class eShop : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var orgID = Request["appid"];//机构ID
            if (string.IsNullOrEmpty(orgID))
                UrlReturn("缺少必要参数：【AppID】");
            else
            {
                lnkHome.NavigateUrl = "/Index.aspx?appid=" + orgID;
                var e = new eShopConfig().Get(orgID);
                if (e != null)
                {
                    if (string.IsNullOrEmpty(lblName.Text))
                        lblName.Text = e.TravelName;
                    Page.Title = e.TravelName;
                } 
            }
        }

        /// <summary>
        /// 跳转错误页面
        /// </summary>
        /// <param name="msg"></param>
        private void UrlReturn(string msg = "")
        {
            var url = "/Error.aspx";
            if (!string.IsNullOrEmpty(msg))
                url += "?msg=" + Security.Base64Encrypt(msg);
            Response.Redirect(url);
        }
    }
}