using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Message.Core;

namespace DRP.Message
{
    public interface IMessage
    {
        bool Send(MessageEntity entity);
    }
}
