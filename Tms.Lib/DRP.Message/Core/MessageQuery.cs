using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.Message.Core
{
    public enum MessageStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        All=0,
        /// <summary>
        /// 未读
        /// </summary>
        UnRead=1,
        /// <summary>
        /// 已读
        /// </summary>
        Readed=2
    }

    public class MessageQuery:MessageQueryBase
    {
        public string RecUserID { get; set; }

        /// <summary>
        /// 阅读状态  
        /// </summary>
        public MessageStatus DataStatus { get; set; }
    }
}
