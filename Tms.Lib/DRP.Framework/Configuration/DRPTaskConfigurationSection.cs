using System.Configuration;

namespace DRP.Framework.Core
{
    public class DRPTaskConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("services", IsDefaultCollection = true, IsRequired = false)]
        public DRPTaskConfigurationElementCollection Services
        {
            get
            {
                return (DRPTaskConfigurationElementCollection)this["services"]; 
            }
            set
            {
                this["services"] = value;
            }
        }
    }
}
