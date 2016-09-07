using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.Message.Core
{
    /// <summary>
    /// 消息查询基类
    /// </summary>
    public class MessageQueryBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortExpress { get; set; }

        public string MessageTitle { get; set; }

        public string SendUserID { get; set; }

        public string sDate { get; set; }

        public string eDate { get; set; }

        public string Mobile { get; set; }

        public string OrgID { get; set; }
    }
}
