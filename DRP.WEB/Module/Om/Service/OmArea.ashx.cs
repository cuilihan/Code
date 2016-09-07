using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.Framework;


namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// 行政区域
    /// </summary>
    public class OmArea : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询列表
                    QueryData(context);
                    break;
                case 2://删除
                    Delete(context);
                    break;
                case 3://ComboTree数据
                    CreateComboTree(context);
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var strJson = new OmArea_BF().GetOmAreaTreeJson();
            context.Response.Write(strJson);
        }

        /// <summary>
        /// 删除导航
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                new OmArea_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }

        private void CreateComboTree(HttpContext context)
        {
            var str = new OmArea_BF().GetOmAreaComboTree();
            context.Response.Write(str);
        }

        protected override string NavigateID
        {
            get
            {
                return "omarea";
            }
        }
    }
}