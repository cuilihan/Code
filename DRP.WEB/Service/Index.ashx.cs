using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Service
{
    /// <summary>
    /// 首页加载的数据
    /// </summary>
    public class Index : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://左面导航菜单
                    QueryNavigate(context);
                    break;
                case 2://左侧zTree菜单
                    QueryNavigateTree(context);
                    break;
                case 3://全局推送消息
                    QueryPushNotice(context);
                    break;
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "index";
            }
        }

        /// <summary>
        /// 左侧导航菜单(手风琴效果)
        /// </summary>
        /// <param name="pNavID"></param>
        /// <param name="context"></param>
        private void QueryNavigate(HttpContext context)
        {
            string pNavID = context.Request["id"];
            var list = new Navigate_BF().GetLoginUserNavigate();
            var coll = list.FindAll(x => x.ParentID == pNavID);
            var arrData = new List<string>();
            coll.ForEach(x =>
            {
                var data = list.FindAll(t => t.ParentID == x.ID);
                var arrJson = new List<string>();
                data.ForEach(a =>
                {
                    var json = "\"NavID\":\"{0}\",\"NavName\":\"{1}\",\"NavUrl\":\"{2}\",\"NavCls\":\"{3}\",\"PageID\":\"{4}\"";
                    json = string.Format(json, a.ID, a.NavName, a.NavUrl, a.NavCls, a.PageID);
                    arrJson.Add("{" + json + "}");
                });
                var strJson = "\"NavID\":\"{0}\",\"NavName\":\"{1}\",\"NavUrl\":\"{2}\",\"NavCls\":\"{3}\",\"PageID\":\"{4}\",\"NavData\":{5}";
                var s = string.Format(strJson, x.ID, x.NavName, x.NavUrl, x.NavCls, x.PageID, "[" + string.Join(",", arrJson) + "]");
                arrData.Add("{" + s + "}");
            });
            context.Response.Write("[" + string.Join(",", arrData) + "]");
        }

        /// <summary>
        /// 左侧导航菜单(zTree)
        /// </summary>
        /// <param name="pNavID"></param>
        /// <param name="context"></param>
        private void QueryNavigateTree(HttpContext context)
        {
            string pNavID = context.Request["id"];
            var list = new Navigate_BF().GetLoginUserNavigate();
            var coll = list.FindAll(x => x.ParentID == pNavID);
            var arrData = coll;
            coll.ForEach(x =>
            {
                arrData.AddRange(list.FindAll(t => t.ParentID == x.ID));
                var arrJson = new List<string>();
            });
            var strJson = ConvertJson.ListToJson(arrData);
            context.Response.Write(strJson);
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="context"></param>
        private void QueryPushNotice(HttpContext context)
        {
            var e = new PushNotice_BF().GetLastNotice();
            if (e != null) context.Response.Write(e.nContent);
            else context.Response.Write("");
        }
    }
}