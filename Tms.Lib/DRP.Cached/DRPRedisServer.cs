using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Framework.Configuration;
using System.Configuration;
using DRP.Framework.Core;

namespace DRP.Cached
{
    /// <summary>
    /// 缓存服务器列表
    /// </summary>
    public class DRPRedisServer
    {
        /// <summary>
        /// 获取可以使用的缓存服务器
        /// </summary>
        /// <returns></returns>
        public List<ServerEnity> GetServer()
        {
            var list = new List<ServerEnity>();
            DRPRedisConfigurationSection items = (DRPRedisConfigurationSection)ConfigurationManager.GetSection("RedisServer");
            if (items != null && items.Server != null)
            {
                foreach (DRPRedisConfigurationElement item in items.Server)
                {
                    var e = new ServerEnity();
                    e.ServerName = item.Name;
                    e.ServerKey = item.ServerKey;
                    e.ServerIP = item.IP;
                    e.ServerPort = item.Port;
                    list.Add(e);
                }
            }
            return list;
        }
    }
}
