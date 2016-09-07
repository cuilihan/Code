using DRP.Cached;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;

namespace DRP.BF.TmsApi
{
    /// <summary>
    /// 接口通用服务
    /// </summary>
    public class ApiHelper
    { 
        #region << 转换服务 >>

        /// <summary>
        /// 列表转化为XML文档字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public string ListToDocument<T>(List<T> list)
        {
            var sb = new StringBuilder();
            sb.Append("<document code=\"1\">");
            for (var i = 0; i < list.Count; i++)
            {
                sb.Append("<data>");
                T obj = Activator.CreateInstance<T>();
                PropertyInfo[] pi = obj.GetType().GetProperties();
                for (int j = 0; j < pi.Length; j++)
                {
                    var pName = pi[j].Name.ToString();
                    if (pName.Contains("Columns")) continue;
                    var pValue = pi[j].GetValue(list[i], null);
                    sb.AppendFormat("<{0}>{1}</{0}>", pName, pValue);
                }
                sb.Append("</data>");
            }
            sb.Append("</document>");
            return sb.ToString();
        }

        /// <summary>
        /// DataTable转换为XmlDocument
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DataTableToDocument(DataTable dt)
        {
            var sb = new StringBuilder();
            sb.Append("<document code=\"1\">");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<data>");
                foreach (DataColumn col in dt.Columns)
                {
                    var pName = col.ColumnName;
                    if (pName.Contains("Columns")) continue;
                    var pValue = row[col].ToString();
                    sb.AppendFormat("<{0}>{1}</{0}>", pName, pValue);
                }
                sb.Append("</data>");
            }
            sb.Append("</document>");
            return sb.ToString();
        }

        /// <summary>
        /// 操作成功返回XML文档格式消息
        /// </summary>
        /// <returns></returns>
        public static string SuccessMessage(string message)
        {
            var sb = new StringBuilder();
            sb.Append("<document code=\"1\">");
            sb.AppendFormat("<data>{0}</data>", message);
            sb.Append("</document>");
            return sb.ToString();
        }

        /// <summary>
        /// 操作失败返回XML文档格式消息
        /// </summary>
        /// <returns></returns>
        public static string ErrorMessage(int code, string message)
        {
            return new TmsApiMessage(code, message).ToString();
        }
        #endregion

    }
}
