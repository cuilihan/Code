using DRP.BF.TmsApi;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using DRP.Framework;
using DRP.BF.TmsApi.Core;

namespace DRP.Api
{
    /// <summary>
    /// TMS对外开发接口服务基类
    /// </summary>
    public class TmsServiceBase : System.Web.Services.WebService
    {
        #region 权限验证
 
        /// <summary>
        /// 数据安全验证
        /// </summary>
        /// <param name="taskGuid"></param>
        /// <param name="xmlData"></param>
        /// <param name="isValid">验证是否通过</param>
        /// <returns>通过验证时为dataType,否则为错误消息</returns>
        protected string AccessAuthority(string taskGuid, string xmlData, out bool isValid)
        {
            isValid = false;
            if (string.IsNullOrEmpty(xmlData))
            {
               return ApiException.XmlDataEmpty();
            } 
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlData);
                var root = doc.SelectSingleNode("Document");
                if (root == null)
                {
                    return ApiException.RootDocumentError(); 
                }
                var dataType = root.Attributes["DataType"].InnerText;
                if (string.IsNullOrEmpty(dataType))
                {
                    return ApiException.DataTypeEmpty(); 
                }
                else
                {
                    isValid = true;
                    return dataType;
                }
            }
            catch (Exception ex)
            {
                return ApiException.SysOperateError(); 
            }
        }

        #endregion

    }
}