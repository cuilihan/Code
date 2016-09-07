using System.Configuration;

namespace DRP.Framework.Core
{
    public class DRPApiConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("services", IsDefaultCollection = true, IsRequired = false)]
        public DRPApiConfigurationElementCollection Services
        {
            get
            {
                return (DRPApiConfigurationElementCollection)this["services"]; 
            }
            set
            {
                this["services"] = value;
            }
        }
    }
}
