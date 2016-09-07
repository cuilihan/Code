using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.TmsApi
{
    /// <summary>
    /// 服务实体
    /// </summary>
    [Serializable]
    public class TaskEntity
    {
        /// <summary>
        /// 服务唯一识别码
        /// </summary>
        public string TaskGuid { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 服务类库
        /// </summary>
        public string Type { get; set; }
    }
}
