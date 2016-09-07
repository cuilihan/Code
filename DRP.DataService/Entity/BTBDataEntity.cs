using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRP.DataService.Entity
{
    /// <summary>
    /// BTB系统推送的数据结构
    /// </summary>
    public class BTBDataEntity
    {
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 详情订单结构
        /// </summary>
        public string OrderXml { get; set; }

        /// <summary>
        /// 3.0提供的Key
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 订单所属公司的主账号
        /// </summary>
        public string MastAcct { get; set; }
    }
}
