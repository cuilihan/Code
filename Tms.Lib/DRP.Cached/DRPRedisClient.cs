using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DRP.Framework.Core;
using ServiceStack.Redis;

namespace DRP.Cached
{
    /// <summary>
    /// 缓存服务器客户端管理
    /// </summary>
    public class DRPRedisClient
    { 
        private DRPRedisClient()
        {
        }

        private static PooledRedisClientManager _RedisClientManager;
        /// <summary>
        /// RedisClient链接池管理 
        /// </summary>
        private static PooledRedisClientManager RedisClientManager
        {
            get
            {
                if (null == _RedisClientManager)
                {
                    _RedisClientManager = CreateManagerPool();
                }
                return _RedisClientManager;
            }
        }

        /// <summary>
        /// 创建连接池
        /// </summary>
        /// <returns></returns>
        private static PooledRedisClientManager CreateManagerPool()
        {
            var list = new DRPRedisServer().GetServer();
            List<string> Hosts = new List<string>();
            list.ForEach(x =>
            {
                Hosts.Add(x.ServerIP + ":" + x.ServerPort);
            });
            return CreateManager(Hosts.ToArray(), Hosts.ToArray());
        }

        /// <summary>
        /// 客户端链接池
        /// </summary>
        /// <param name="readWriteHosts">“写”链接池链接数</param>
        /// <param name="readOnlyHosts">“读”链接池链接数</param>
        /// <returns></returns>
        private static PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            //支持读写分离，均衡负载
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 5000,
                MaxReadPoolSize = 5000,
                AutoStart = true,
            });
        }
        
        /// <summary>
        /// 缓存服务器客户端
        /// </summary>
        /// <param name="serverKey"></param>
        /// <returns></returns>
        public static IRedisClient CreateInstance()
        {
            return RedisClientManager.GetClient();
        }
    }
}
