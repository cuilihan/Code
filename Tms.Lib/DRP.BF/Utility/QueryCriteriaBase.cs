using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF
{

    /// <summary>
    /// 日期区间，用于日期区间选择
    /// </summary>
    public class DateScope
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public string sDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string eDate { get; set; }
    }

    /// <summary>
    /// 查询条件基类
    /// </summary>
    public class QueryCriteriaBase
    {
        /// <summary>
        /// 页面索引
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string SortExpress { get; set; }

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 查询日期区间
        /// </summary>
        public DateScope QueryDateScope { get; set; }

        /// <summary>
        /// 状态 -1：待审核  0：禁用 1：启用
        /// </summary>
        public int DataStatus { get; set; }
    }
}
