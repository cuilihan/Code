using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DRP.Framework.Configuration
{
    public class DRPRedisConfigurationElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DRPRedisConfigurationElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new DRPRedisConfigurationElement(elementName);
        }

        protected new DRPRedisConfigurationElement BaseGet(int index)
        {
            return (DRPRedisConfigurationElement)base.BaseGet(index);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DRPRedisConfigurationElement)element).Name;
        }
    }
}
