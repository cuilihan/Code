using System.Web.UI.WebControls;
namespace DRP.DAL
{
    public sealed class DataBindHelper
    {
        public static void BindData(object dataSource, ListControl control, string textField, string valueField, ListItem addCustomerItem, int insertIndex)
        {
            control.DataSource = dataSource;
            control.DataTextField = textField;
            control.DataValueField = valueField;
            control.DataBind();
            if (addCustomerItem != null)
                control.Items.Insert(insertIndex, addCustomerItem);
        }

        public static void BindData(object dataSource, ListControl control, string textField, string valueField, ListItem addCustomerItem)
        {
            BindData(dataSource, control, textField, valueField, addCustomerItem, 0);
        }

        public static void BindData(object dataSource, ListControl control, string textField, string valueField)
        {
            BindData(dataSource, control, textField, valueField, null, 0);
        }
    }
}
