using System;
using System.Configuration;

namespace DRP.Framework.Core
{
    public class DRPApiConfigurationElement : ConfigurationElement
    {
        public DRPApiConfigurationElement() : base() { }

        public DRPApiConfigurationElement(string name)
            : base()
        {
            Name = name;
        }

        /// <summary>
        /// 平台名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
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
        /// 平台GUID
        /// </summary>
        [ConfigurationProperty("taskid", IsRequired = true)]
        public string TaskGuide
        {
            get
            {
                return (string)this["taskid"];
            }
            set
            {
                this["taskid"] = value;
            }
        }


        /// <summary>
        /// 是否启用
        /// </summary>
        [ConfigurationProperty("enabled", IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return Convert.ToBoolean(this["enabled"]);
            }
            set
            {
                this["enabled"] = value;
            }
        }

        /// <summary>
        /// 服务类库
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["enabled"] = value;
            }
        } 
    }
}
