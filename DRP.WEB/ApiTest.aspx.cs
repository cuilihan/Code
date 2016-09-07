using DRP.BF;
using DRP.BF.DataSync;
using DRP.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB
{
    public partial class ApiTest : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TextBox1.Text = new OctConfig().OctAcct;
            TextBox2.Text = new OctConfig().OctPwd;
            TextBox3.Text = new OctConfig().OctAppId;
            TextBox4.Text = new OctConfig().OctAppKey;
            TextBox5.Text = FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now + new OctConfig().OctAppKey, "md5").ToLower();
            TextBox6.Text = DateTime.Now.ToString();
            TextBox7.Text = new OctConfig().OctServiceUrl;

        }


        /// <summary>
        /// 查询供应商订单信息
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="siteID">接口更新去掉</param>
        /// <returns></returns>
        private void GetData()
        {
            var instance = new OctConfig();
            var expires = DateTime.Now;
            var type = TextBox9.Text;
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(type + expires + TextBox4.Text
 , "md5").ToLower();

            var url = string.Concat(TextBox7.Text, DropDownList1.SelectedValue);
            string parameters = string.Format("appid={0}&expires={1}&sign={2}", TextBox3.Text, expires, sign);
            parameters += TextBox8.Text;

            try
            {
                this.lblData.Text = url + "" + parameters;
                var strXmlData = new HttpHelper().PostRequest(url, parameters);

                strXmlData = strXmlData.Replace("xmlns=\"http://tempuri.org/\"", "");
                strXmlData = strXmlData.Replace("<!--[CDATA[", "").Replace("]]-->", "");
                resData.InnerText = strXmlData;
            }
            catch (Exception ex)
            {
                resData.InnerHtml = ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetData();
        }

        protected void btnSyncOrder_Click(object sender, EventArgs e)
        {
            var o = new { OrderType = 1, OrderXml = XmlData.Text, AppId = TextBox3.Text, Expires = DateTime.Now.ToString(), Sign = TextBox5.Text, MastAcct = TextBox1.Text };
            var s = new DRP.Framework.Net.HttpHelper().PostAsync(ApiHost.Text, o);
            resData.InnerText = "执行完成：" + s;
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}