using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DRP.Framework.Configuration
{
    /// <summary>
    /// 分步式缓存Redis配置项
    /// </summary>
    public class DRPRedisConfigurationElement : ConfigurationElement
    {
        public DRPRedisConfigurationElement() : base() { }

        public DRPRedisConfigurationElement(string name)
            : base()
        {
            Name = name;
        }

        /// <summary>
        /// 缓存服务器名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// 缓存服务器IP
        /// </summary>
        [ConfigurationProperty("ip", IsRequired = true)]
        public string IP
        {
            get
            {
                return (string)this["ip"];
            }
            set
            {
                this["ip"] = value;
            }
        }

        /// <summary>
        /// 缓存服务器唯一识别值
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true)]
        public string ServerKey
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        /// <summary>
        /// 缓存服务器端口
        /// </summary>
        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get
            {
                return Convert.ToInt16(this["port"]);
            }
            set
            {
                this["port"] = value;
            }
        }
    }
}
