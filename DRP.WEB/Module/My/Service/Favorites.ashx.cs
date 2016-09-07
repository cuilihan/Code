using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.My;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.My.Service
{
    /// <summary>
    /// 常用链接
    /// </summary>
    public class Favorites : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询数据
                    QueryData(context);
                    break;
                case 2: //删除 
                    Delete(context);
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var list = new Favorites_BF().GetUserFavorite();
            var json = ConvertJson.ListToJson(list);
            context.Response.Write(json);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                new Favorites_BF().Delete(keyID);
                context.Response.Write("1");
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