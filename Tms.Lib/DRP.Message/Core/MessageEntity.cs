using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.Message.Core
{
    /// <summary>
    /// 消息实体
    /// </summary>
    [Serializable]
    public class MessageEntity
    {
        public string KeyID { get; set; }

        public string RecUserID { get; set; }

        public string RecUserName { get; set; }

        public string SendUserID { get; set; }

        public string SendUserName { get; set; }

        public string MsgTitle { get; set; }

        public string MsgContent { get; set; }

        public string OrgID { get; set; }

        public int DataStatus { get; set; }

        public string RecMobile { get; set; }
        public string[] MassMobile { get; set; }

        public string Target { get; set; }

        public string URL { get; set; }

        /// <summary>
        /// 是否以模板形式发手机短信
        /// </summary>
        public bool IsTemplateSms { get; set; }

        /// <summary>
        /// 模板短信类型
        /// </summary>
        public T_Sms_Type SmsType { get; set; }
    }
}
