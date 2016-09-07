using System;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace DRP.Framework.Core
{
    public class ReflectHelper<T> where T : class
    {
        #region 绑定查询条件
        /// <summary>
        /// 查询条件,返回Where后的条件
        /// </summary>
        /// <remarks>支持控件：TextBox,DropdownList,RadioButtonList,HiddenField</remarks>
        /// <param name="control"></param>
        /// <returns></returns>
        public static string AssignQueryCondition(Control control)
        {
            var sb = new StringBuilder("1=1");
            var coll = control.Controls;
            System.Threading.Tasks.Parallel.For(0, coll.Count, i =>
            {
                var ctrl = coll[i];
                var _type = ctrl.GetType();
                if (_type == typeof(TextBox))
                {
                    var _txt = ctrl as TextBox;
                    if (!_txt.ID.Contains("txt") && !string.IsNullOrEmpty(_txt.Text))
                    {
                        sb.AppendFormat(" and {0} like '%{1}%'", _txt.ID, _txt.Text);
                    }
                }
                if (_type == typeof(DropDownList))
                {
                    var _ddl = ctrl as DropDownList;
                    if (_ddl.SelectedIndex >= 0 && !string.IsNullOrEmpty(_ddl.SelectedValue) && !_ddl.ID.Contains("ddl"))
                    {
                        sb.AppendFormat(" and {0}='{1}'", _ddl.ID, _ddl.SelectedValue.Trim());
                    }
                }
                if (_type == typeof(HiddenField))
                {
                    var _hide = ctrl as HiddenField;
                    if (!string.IsNullOrEmpty(_hide.Value))
                    {
                        sb.AppendFormat(" and {0} like '%{1}%'", _hide.ID, _hide.Value);
                    }
                }
                if (_type == typeof(RadioButtonList))
                {
                    var _rdList = ctrl as RadioButtonList;
                    if (_rdList.SelectedIndex >= 0 && !string.IsNullOrEmpty(_rdList.SelectedValue))
                    {
                        sb.AppendFormat(" and {0}={1}", _rdList.ID, _rdList.SelectedValue);
                    }
                }
            });
            return sb.ToString();
        }

        #endregion

        #region 绑定实体

        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        public T AssignEntity(T obj, Control control)
        {
            PropertyInfo[] pArr = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pArr)
            {
                if (pi.Name == "Columns") continue;            
                var v = GetValueFromControl(pi.Name, control);
                if (string.IsNullOrEmpty(v))
                {
                    //if (pi.PropertyType == typeof(string))
                    //{
                    //    v = " ";
                    //}
                    //else
                        continue;
                }
                SetProperty(pi.Name, obj, v, pi.PropertyType);
            }
            return obj;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="target"></param>
        /// <param name="value"></param>
        internal void SetProperty(string propertyName, T target, dynamic value, Type propertyType)
        {
            Type type = target.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                    value = new Guid(value.ToString());
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                    value = Convert.ToDateTime(value);
                else if (propertyType == typeof(Int32) || propertyType == typeof(Int32?))
                    value = Convert.ToInt32(value);
                else if (propertyType == typeof(bool) || propertyType == typeof(Boolean?))
                    value = value.ToString().Length == 1 ? (value.ToString().Equals("1") ? true : false) : Convert.ToBoolean(value);
                else if (propertyType == typeof(byte) || propertyType == typeof(byte?))
                    value = Convert.ToByte(value);
                else if (propertyType == typeof(double) || propertyType == typeof(double?))
                    value = Convert.ToDouble(value);
                else if (propertyType == typeof(float) || propertyType == typeof(float?))
                    value = Convert.ToDouble(value);
                else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                    value = Convert.ToDecimal(value);
                propertyInfo.SetValue(target, value, null);
            }
            else
                propertyInfo.SetValue(target, null, null);
        }

        /// <summary>
        /// 从控件中获取数据
        /// </summary>
        /// <param name="ctrlName"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        private string GetValueFromControl(string ctrlName, Control control)
        {
            if (control.ID == null) return string.Empty;
            var _control = control.FindControl(ctrlName);

            if (_control == null) return string.Empty;
            var _ctrlType = _control.GetType();
            if (_ctrlType == typeof(TextBox))
                return ((TextBox)_control).Text;
            if (_ctrlType == typeof(Label))
                return ((Label)_control).Text;
            if (_ctrlType == typeof(HiddenField))
                return ((HiddenField)_control).Value;
            if (_ctrlType == typeof(RadioButton))
                return ((RadioButton)_control).Text;
            if (_ctrlType == typeof(RadioButtonList))
                return ((RadioButtonList)_control).SelectedValue;
            if (_ctrlType == typeof(DropDownList))
                return ((DropDownList)_control).SelectedValue;
            if (_ctrlType == typeof(CheckBoxList))
            {
                var list = new List<string>();
                var chkList = (CheckBoxList)_control;
                foreach (ListItem item in chkList.Items)
                {
                    if (item.Selected)
                        list.Add(item.Value);
                }
                return string.Join(",", list.ToArray());
            }
            if (_ctrlType == typeof(CheckBox))
                return ((CheckBox)_control).Checked ? "1" : "0";
            return string.Empty;
        }
        #endregion

        #region 绑定控件

        /// <summary>
        /// 将实例属性绑定到控件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="control"></param>
        public void BindDataToControl(T obj, Control control)
        {
            PropertyInfo[] pArr = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pArr)
            {
                if (pi.Name == "Columns") continue;
                var v = GetProperty(pi.Name, obj);
                SetValueToControl(pi.Name, v, control);
            }
        }

        /// <summary>
        /// 从实体中解析出字段值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="target"></param>
        /// <param name="value"></param>
        private string GetProperty(string propertyName, object target)
        {
            Type type = target.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            //  propertyInfo.PropertyType
            var obj = propertyInfo.GetValue(target, null);
            if (obj != null)
            {
                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    return Convert.ToDateTime(obj.ToString()).ToString("yyyy-MM-dd");
                }
                else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
                {
                    if (obj.ToString() == "True")
                        return "1";
                    else
                        return "0";
                }
                return obj.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 绑定控件
        /// </summary>
        /// <param name="ctrlName"></param>
        /// <param name="value"></param>
        /// <param name="control"></param>
        private void SetValueToControl(string ctrlName, string value, Control control)
        {
            if (control.ID == null) return;

            var _control = control.FindControl(ctrlName);
            if (_control == null) return;

            var _ctrlType = _control.GetType();

            if (_ctrlType == typeof(TextBox))
                ((TextBox)_control).Text = value;
            if (_ctrlType == typeof(RadioButtonList))
                ((RadioButtonList)_control).SelectedValue = value;
            if (_ctrlType == typeof(DropDownList))
                ((DropDownList)_control).SelectedValue = value;
            if (_ctrlType == typeof(CheckBox))
                ((CheckBox)_control).Checked = value.Length == 1 ? (value.Equals("1")) : Convert.ToBoolean(value);
            if (_ctrlType == typeof(CheckBoxList))
            {
                var chkList = _control as CheckBoxList;
                foreach (ListItem item in chkList.Items)
                {
                    if (value.Contains(item.Value))
                        item.Selected = true;
                }
            }
            if (_ctrlType == typeof(Label))
                ((Label)_control).Text = value;
            if (_ctrlType == typeof(Literal))
                ((Literal)_control).Text = value;
            if (_ctrlType == typeof(HiddenField))
                ((HiddenField)_control).Value = value;
        }

        #endregion 
    }
}
