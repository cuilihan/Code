using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DRP.DataService;
using DRP.DataService.Entity;
using DRP.Framework.Core;
using Newtonsoft.Json;

namespace Tms.WebApi.Controllers
{
    public class OrderSyncController : Controller
    {

        [HttpPost]
        [ValidateInput(false)]
        public string ReceiveOrderAsParameter(string orderType, string orderXml, string appID, string expires, string sign)
        {

            var e = new BTBDataEntity();
            e.OrderType = orderType;
            e.OrderXml = orderXml;
            e.AppId = appID;
            e.Expires = DateTime.Parse(expires);
            e.Sign = sign;
            return AcceptOrderData(e);
        }

        /// <summary>
        /// 接收同步订单数据
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private string AcceptOrderData(BTBDataEntity e)
        {
            if (!CheckSign(e)) return ParseToXml(false, "签名验证错误");

            var isOk = new DataSyncHelper().SyncData(e);
            if (isOk) return ParseToXml(true, "订单处理完成");
            else return ParseToXml(false, "订单处理异常");
        }

        private string ParseToXml(bool isRet, string message, string exceptionType = "")
        {
            var sb = new StringBuilder();
            sb.Append("<PushReturn>");
            sb.Append("<Success>" + isRet.ToString() + "</Success>");
            sb.Append("<Error>");
            sb.Append("<Code>" + (isRet ? "1" : "0") + "</Code>");
            sb.Append("<Message><![CDATA[" + message + "]]></Message>");
            sb.Append("<ExceptionType>" + exceptionType + "</ExceptionType>");
            sb.Append("</Error>");
            sb.Append("</PushReturn>");
            return sb.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="value"></param>
        private bool CheckSign(BTBDataEntity e)
        {
            var key = ConfigHelper.GetAppSettingValue("RequestKey");
            string sign = Oct.Framework.Core.Security.Encypt.MD5(e.OrderType + e.OrderXml + e.AppId + e.Expires + key);
            //return e.Sign.Equals(sign);
            return true;
        }
    }
}