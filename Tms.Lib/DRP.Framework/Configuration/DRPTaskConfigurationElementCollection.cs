using System.Configuration;

namespace DRP.Framework.Core
{
    public class DRPTaskConfigurationElementCollection : ConfigurationElementCollection
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
            return new DRPTaskConfigurationElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new DRPTaskConfigurationElement(elementName);
        }

        protected new DRPTaskConfigurationElement BaseGet(int index)
        {
            return (DRPTaskConfigurationElement)base.BaseGet(index);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DRPTaskConfigurationElement)element).Name;
        }
    }
}
