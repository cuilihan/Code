using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.Message.Core;
using DRP.BF.eShop;

namespace DRP.eShop.Service
{
    /// <summary>
    /// Reg 的摘要说明
    /// </summary>
    public class Reg : mHandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://请求系统发验证短信
                    SendValidCodeSms(context);
                    break;
            }
        }

        private void SendValidCodeSms(HttpContext context)
        {
            var mobile = context.Request["m"];
            if (string.IsNullOrEmpty(mobile) || mobile.Length != 11)
                context.Response.Write("手机号码为空");
            else
            {
                var message = new MessageEntity();
                message.SendUserID = Guid.Empty.ToString();
                message.SendUserName = "系统";
                message.RecMobile = mobile;
                message.OrgID = context.Request["appid"];
                message.DataStatus = 1;
                message.IsTemplateSms = true;
                message.SmsType = T_Sms_Type.Validate;
                message.MsgContent = new Random().Next(100000, 999999).ToString();
                var ret = new SmsBiz().SendSms(message);
                if (ret.Code.Equals("504")) context.Response.Write("1");
                else context.Response.Write(ret.Message);
            }
        }
    }
}