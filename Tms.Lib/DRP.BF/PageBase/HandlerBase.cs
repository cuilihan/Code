using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DRP.BF.PageBase
{
    /// <summary>
    /// 一般处理程序应用基类
    /// </summary>
    public class HandlerBase : IHttpHandler
    {
        private string __navigate = "";
        /// <summary>
        /// 请求页面对应的功能ID
        /// </summary>
        protected virtual string NavigateID { get { return __navigate; } }

        /// <summary>
        /// 一般处理程序的业务应用入口
        /// </summary>
        /// <param name="context"></param>
        protected virtual void DRPProcessRequest(HttpContext context)
        {
        }

        /// <summary>
        /// 请求页面对应的功能ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string GetNavigateID(HttpContext context)
        {
            return "";
        }


        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            var navID = NavigateID;
            if (string.IsNullOrEmpty(navID))
                navID = GetNavigateID(context);
            try
            {
                new Permisstion_BF().AccessAuthority(navID);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("权限拒绝"))
                    context.Response.Write("-100");
            }
            DRPProcessRequest(context);
        }

        #endregion
    }
}
