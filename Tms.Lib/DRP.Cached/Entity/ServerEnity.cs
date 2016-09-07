using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.Cached
{
    public class ServerEnity
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 服务器唯一识别码，调用缓存服务器的关健字
        /// </summary>
        public string ServerKey { get; set; }

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 服务器端口号
        /// </summary>
        public int ServerPort { get; set; }
    }
}
