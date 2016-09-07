using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.PageBase;
using DRP.Framework;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// 在线用户统计
    /// </summary>
    public class OnLineRpt : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://用户登录次数
                    UserLoginStatistic(context);
                    break;
            }
        }

        private void UserLoginStatistic(HttpContext context)
        {
            
        }



        protected override string NavigateID
        {
            get
            {
                return "omonline";
            }
        }
    }
}