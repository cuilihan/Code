using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;

namespace DRP.BF.eShop
{
    /// <summary>
    /// 权限设置
    /// </summary>
    public class mAuthentication
    {
        /// <summary>
        /// 当前用户是否登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                var e = mUserInfo;
                if (e == null) return false;
                return string.IsNullOrEmpty(e.Mobile);
            }
        }

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static mUserEntity mUserInfo
        {
            get
            {
                var e = new mUserEntity();

                return e;
            }
        }
    }
}
