using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.DAL.Model
{
    /// <summary>
    /// 导游报账数据
    /// </summary>
    public class GuideCheckAccountEntity
    {
        /// <summary>
        /// 报账项目金额
        /// </summary>
        public decimal ItemAmt { get; set; }

        /// <summary>
        /// 报账项目名称
        /// </summary>
        public string ItemName { get; set; }
    }
}
