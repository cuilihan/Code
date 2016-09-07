using DRP.BF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRP.WeChat.Utility
{
    public class WeChatBase : Pagebase
    {
        protected override bool IsWechat
        {
            get
            {
                return true;
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "wechat";
            }
        }
    }
}