using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Message.Core;

namespace DRP.Message
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        SysMessage,
        /// <summary>
        /// 手机短信消息
        /// </summary>
        SmsMessage
    }

    /// <summary>
    /// 消息管理
    /// </summary>
    public class MessageHelper
    {
        /// <summary>
        /// 创建消息实体接口
        /// </summary>
        /// <param name="xType"></param>
        /// <returns></returns>
        public IMessage CreateInstance(MessageType xType)
        {
            IMessage e = null;
            if (xType == MessageType.SysMessage)
            {
                e = new MessageBiz();
            }
            else
            {
                e = new SmsBiz();
            }
            return e;
        }
    }
}
