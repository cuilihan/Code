using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace DRP.Framework.Core
{
    public class ConvertJson
    {
        #region 私有方法
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            if (type == typeof(Guid))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                if (!string.IsNullOrEmpty(str))
                    str = Convert.ToDateTime(str).ToString("yyyy-MM-dd");
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        #endregion

        #region List转换成Json
        /// <summary>
        /// List转换成Json
        /// </summary>
        public static string ListToJson<T>(IList<T> list)
        {
            if (list.Count == 0) return "[]";
            object obj = list[0];
            return ListToJson<T>(list, "rows");
        }

        /// <summary>
        /// List转换成Json 
        /// </summary>
        public static string ListToJson<T>(IList<T> list, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            //  if (string.IsNullOrEmpty(jsonName)) jsonName = list[0].GetType().Name;
            // Json.Append("{\"" + jsonName + "\":[");
            Json.Append("[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        var pName = pi[j].Name.ToString();
                        if (pName.Contains("Columns")) continue;
                        var pValue = pi[j].GetValue(list[i], null);
                        var v = "\"\"";
                        if (pValue != null)
                        {
                            Type type = pValue.GetType();
                            v = StringFormat(pValue.ToString(), type);
                            if (!string.IsNullOrEmpty(v))
                                v = HtmlHeler.NoHTML(v);
                        }
                        Json.Append("\"" + pName + "\":" + v);

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            var s = Json.ToString();
            return s == "[]" ? "" : s;
        }
        #endregion

        #region 对象转换为Json
        /// <summary> 
        /// 对象转换为Json 
        /// </summary> 
        /// <param name="jsonObject">对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }
        #endregion

        #region 对象集合转换Json
        /// <summary> 
        /// 对象集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += ToJson(item) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }
        #endregion

        #region 普通集合转换Json
        /// <summary> 
        /// 普通集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToArrayString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }
        #endregion

        #region  DataSet转换为Json
        /// <summary> 
        /// DataSet转换为Json 
        /// </summary> 
        /// <param name="dataSet">DataSet对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }
        #endregion

        #region Datatable转换为Json
        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return "[]";
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString().Trim();
                    if (!string.IsNullOrEmpty(strValue))
                    {
                        strValue = strValue.Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'");
                        strValue = strValue.Replace("	", " ");
                        strValue = HtmlHeler.NoHTML(strValue);
                    }
                    jsonString.Append("\"" + strKey + "\":");
                    Type type = dt.Columns[j].DataType;
                    if (type == typeof(DateTime))
                    {
                        if (!string.IsNullOrEmpty(strValue))
                            strValue = Convert.ToDateTime(strValue).ToString("yyyy-MM-dd");
                    }
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + strValue + "\",");
                    }
                    else
                    {
                        jsonString.Append("\"" + strValue + "\"");
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        /// <summary>
        /// DataTable转换为Json 
        /// </summary>
        public static string ToJson(DataTable dt, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName)) jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion

        #region DataReader转换为Json
        /// <summary> 
        /// DataReader转换为Json 
        /// </summary> 
        /// <param name="dataReader">DataReader对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DbDataReader dataReader)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            while (dataReader.Read())
            {
                jsonString.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    Type type = dataReader.GetFieldType(i);
                    string strKey = dataReader.GetName(i);
                    string strValue = dataReader[i].ToString();
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (i < dataReader.FieldCount - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            dataReader.Close();
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #endregion

        #region Json转化为对象
        /// <summary>
        /// JSON字符串转化为序列化对象
        /// </summary>
        /// <param name="jsonText">JSON字符串</param>
        /// <param name="entity">序列化对象</param>
        /// <returns>对象实体</returns>
        public static List<T> AssignEntityFromJsonText<T>(string jsonText) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                JavaScriptSerializer s = new JavaScriptSerializer();
                object[] obj = (object[])s.DeserializeObject(jsonText);
                PropertyInfo[] pArr = typeof(T).GetProperties();
                ReflectHelper<T> reflect = new ReflectHelper<T>();
                foreach (var o in obj)
                {
                    T entity = new T();
                    Dictionary<string, object> dict = (Dictionary<string, object>)o;
                    foreach (PropertyInfo pi in pArr)
                    {
                        var v = GetValueFormDictionary(dict, pi.Name);
                        if (string.IsNullOrEmpty(v)) continue;
                        reflect.SetProperty(pi.Name, entity, v, pi.PropertyType);
                    }
                    list.Add(entity);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string GetValueFormDictionary(Dictionary<string, object> dict, string key)
        {
            foreach (KeyValuePair<string, object> kp in dict)
            {
                if (kp.Key.ToLower() == key.ToLower())
                    return kp.Value.ToString();
            }
            return string.Empty;
        }
        #endregion
    }
}
