using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DRP.BF.eShop
{
    /// <summary>
    /// 微网站数据服务接口层基类DRP.eShop
    /// </summary>
    public class mHandlerBase : IHttpHandler
    {
        /// <summary>
        /// 一般处理程序的业务应用入口
        /// </summary>
        /// <param name="context"></param>
        protected virtual void DRPProcessRequest(HttpContext context)
        {
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        { 
            DRPProcessRequest(context); 
        }
    }
}
