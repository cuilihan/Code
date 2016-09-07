using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// 导航组管理
    /// </summary>
    public class NavGroup : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询导航组
                    QueryData(context);
                    break;
                case 2://删除导航组
                    Delete(context);
                    break;
                case 3://查询导航组功能菜单
                    NavGroupMenu(context);
                    break;
                case 4://保存导航组
                    SaveNavGroup(context);
                    break;
            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var list = new NavGroup_BF().GetNavGroup();
            var json = ConvertJson.ListToJson(list);
            context.Response.Write(json);
        }

        /// <summary>
        /// 删除导航组
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                new NavGroup_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }

        /// <summary>
        /// 导航组功能
        /// </summary>
        /// <param name="context"></param>
        private void NavGroupMenu(HttpContext context)
        {
            var navGroupID = context.Request["nid"];
            var json = new NavGroup_BF().QueryNavGroupItems(navGroupID);
            context.Response.Write(json);
        }

        /// <summary>
        /// 保存导航组
        /// </summary>
        /// <param name="context"></param>
        private void SaveNavGroup(HttpContext context)
        { 
            var e = new DAL.Om_NavGroup();
            e.ID = string.IsNullOrEmpty(context.Request["NavGroupID"]) ? Guid.NewGuid().ToString() : context.Request["NavGroupID"];
            e.NavGroup = context.Request["NavGroup"];
            e.Comment = context.Request["Comment"];
            e.OrderIndex = context.Request["OrderIndex"].ToInt();
            var navIDs = context.Request["NavigateID"];
            var isOk=new NavGroup_BF().Save(e, navIDs);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "omgroup";
            }
        }
    }
}