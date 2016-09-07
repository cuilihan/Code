using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF
{
    /// <summary>
    /// 消息实体
    /// </summary>
    public struct MessageType
    {
        /// <summary>
        /// 代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 接口错误处理
    /// </summary>
    public class TmsApiMessage
    {
        private MessageType mEntity = new MessageType();

        public TmsApiMessage(int code, string message)
        {
            mEntity.Code = code;
            mEntity.Message = message;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<document code=\"{0}\">", mEntity.Code);
            sb.Append("<data>");
            sb.AppendFormat("<code>{0}</code>", mEntity.Code);
            sb.AppendFormat("<message>{0}</message>", mEntity.Message);
            sb.Append("</data>");
            sb.Append("</document>");
            return sb.ToString();
        }
    }
}
