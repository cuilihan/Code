using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.Message.Core;

namespace DRP.WEB.Module.My.Service
{
    /// <summary>
    /// Message 的摘要说明
    /// </summary>
    public class Message : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://查询系统消息
                    QuerySysMessage(context);
                    break;
                case 2://设置系统消息状态
                    SetStatatus(context);
                    break;
                case 3://查询手机短信
                    QueryUserSms(context);
                    break;
                case 4://发短消息
                    SendSms(context);
                    break;
            }
        }

        /// <summary>
        /// 系统消息
        /// </summary>
        /// <param name="context"></param>
        private void QuerySysMessage(HttpContext context)
        {
            var qry = new MessageQuery();
            qry.PageSize = context.Request["rows"].ToInt();
            qry.PageIndex = context.Request["page"].ToInt();
            qry.DataStatus = (MessageStatus)context.Request["status"].ToInt();
            qry.sDate = context.Request["sDate"];
            qry.eDate = context.Request["eDate"];
            qry.RecUserID = DRP.BF.AuthenticationPage.UserInfo.UserID;
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new MessageBiz().GetUserMessage(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        private void SetStatatus(HttpContext context)
        {
            var ids = context.Request["id"];
            if (string.IsNullOrEmpty(ids)) return;
            var list = new List<string>();
            foreach (var s in ids.Split(','))
                list.Add(string.Format("'{0}'", s));
            new MessageBiz().SetStatus(list, MessageStatus.Readed);
            context.Response.Write(1);
        }

        /// <summary>
        /// 用户手机消息
        /// </summary>
        /// <param name="context"></param>
        private void QueryUserSms(HttpContext context)
        {
            var qry = new MessageQueryBase();
            qry.PageSize = context.Request["rows"].ToInt();
            qry.PageIndex = context.Request["page"].ToInt();
            qry.sDate = context.Request["sDate"];
            qry.eDate = context.Request["eDate"];
            qry.MessageTitle = context.Request["key"];
            qry.SendUserID = DRP.BF.AuthenticationPage.UserInfo.UserID;
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new SmsBiz().GetUserMessage(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="context"></param>
        private void SendSms(HttpContext context)
        {
            var mobile = context.Request["Mobile"];
            var content = context.Request["Content"];
            if (string.IsNullOrEmpty(mobile))
            {
                context.Response.Write("手机号为空");
                return;
            }
            mobile = mobile.Replace("，", ",").Replace(" ", ",").Replace("|", ",");
            var user = DRP.BF.AuthenticationPage.UserInfo;
            var e = new MessageEntity();
            e.SendUserID = user.UserID;
            e.SendUserName = user.UserName;
            e.MsgContent = content;
            e.OrgID = user.OrgID;
            e.RecMobile = mobile;
            e.IsTemplateSms = false;
            var ret = new NSmsBiz().SendSms(e);
            if (ret.Code == "0") //发送成功
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write(ret.Message);
            }
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