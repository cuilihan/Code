using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.TmsApi.Core
{
    /// <summary>
    /// 接口异常消息
    /// </summary>
    public class ApiException
    {
        /// <summary>
        /// 未实现DataType方法
        /// </summary>
        /// <returns></returns>
        public static string NonFindDataType()
        {
            return ApiHelper.ErrorMessage(-98, "未实现DataType的方法");
        }

        /// <summary>
        /// 参数值XmlData为空
        /// </summary>
        /// <returns></returns>
        public static string XmlDataEmpty()
        {
            return ApiHelper.ErrorMessage(-1, "参数值XmlData为空");
        }

        /// <summary>
        /// 服务TaskGuid不存在或服务不可用
        /// </summary>
        /// <returns></returns>
        public static string TaskGuidEmpty()
        {
            return ApiHelper.ErrorMessage(-2, "服务TaskGuid不存在或服务不可用");
        }

        /// <summary>
        /// 未找到根节点Document
        /// </summary>
        /// <returns></returns>
        public static string RootDocumentError()
        {
            return ApiHelper.ErrorMessage(-4, "未找到根节点Document或XML格式错误");
        }

        /// <summary>
        /// DataType为空值
        /// </summary>
        /// <returns></returns>
        public static string DataTypeEmpty()
        {
            return ApiHelper.ErrorMessage(-3, "DataType为空值或未实现此方法");
        }

        /// <summary>
        /// 系统错误执行过程异常
        /// </summary>
        /// <returns></returns>
        public static string SysOperateError()
        {
            return ApiHelper.ErrorMessage(-99, " 系统错误执行过程异常");
        }

    }
}
