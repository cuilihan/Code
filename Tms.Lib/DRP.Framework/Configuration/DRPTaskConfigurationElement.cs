using System;
using System.Configuration;

namespace DRP.Framework.Core
{
    public class DRPTaskConfigurationElement : ConfigurationElement
    {
        public DRPTaskConfigurationElement() : base() { }

        public DRPTaskConfigurationElement(string name)
            : base()
        {
            Name = name;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey=true)]
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
        /// 任务执行类
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
                this["type"] = value;
            }
        }


        /// <summary>
        /// 任务执行类
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
        /// 任务执行频率
        /// </summary>
        [ConfigurationProperty("frequency", IsRequired = true)]
        public string Frequencyype
        {
            get
            {
                return (string)this["frequency"];
            }
            set
            {
                this["frequency"] = value;
            }
        }
    }
}
