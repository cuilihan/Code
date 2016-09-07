using System.Configuration;

namespace DRP.Framework.Core
{
    public class DRPApiConfigurationElementCollection : ConfigurationElementCollection
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
            return new DRPApiConfigurationElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new DRPApiConfigurationElement(elementName);
        }

        protected new DRPApiConfigurationElement BaseGet(int index)
        {
            return (DRPApiConfigurationElement)base.BaseGet(index);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DRPApiConfigurationElement)element).Name;
        }
    }
}
