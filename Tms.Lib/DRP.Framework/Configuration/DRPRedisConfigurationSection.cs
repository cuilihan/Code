using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DRP.Framework.Configuration;

namespace DRP.Framework.Core
{
    public class DRPRedisConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("server", IsDefaultCollection = true, IsRequired = false)]
        public DRPRedisConfigurationElementCollection Server
        {
            get
            {
                return (DRPRedisConfigurationElementCollection)this["server"];
            }
            set
            {
                this["server"] = value;
            }
        }
    }
}
