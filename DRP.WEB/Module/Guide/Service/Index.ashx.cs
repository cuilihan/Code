using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.BF.RptMrg;
using DRP.Framework;
using DRP.BF.GloMrg;

namespace DRP.WEB.Module.Guide.Service
{
    /// <summary>
    /// 首页加载的数据
    /// </summary>
    public class Index : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://订单统计
                    OrderStatistic(context);
                    break;
                case 2://系统更新日志
                    SysUpdateLog(context);
                    break;
            }
        }

        /// <summary>
        /// 订单成交量分析
        /// </summary>
        /// <param name="context"></param>
        private void OrderStatistic(HttpContext context)
        {
            var year = DateTime.Today.Year;
            var orderType = context.Request["orderType"].ToInt();

            var dal = new RptUtility_BF();
            var dt = dal.OrderStatistic(year, orderType);
            var coll = new List<string>();
            var list = new List<string>();
            list.Add("一月");
            list.Add("二月");
            list.Add("三月");
            list.Add("四月");
            list.Add("五月");
            list.Add("六月");
            list.Add("七月");
            list.Add("八月");
            list.Add("九月");
            list.Add("十月");
            list.Add("十一月");
            list.Add("十二月");
            var i = 1;
            list.ForEach(x =>
            {
                var strJson = "\"Month\":\"{0}\",\"OrderAmt\":\"{1}\",\"Profit\":\"{2}\"";
                var a = FindDataTableValue(dt, "M", i.ToString(), "OrderAmt");
                var p = FindDataTableValue(dt, "M", i.ToString(), "Profit");
                var json = string.Format(strJson, x, a, p);
                coll.Add("{" + json + "}");
                i++;
            });
            var s = "[" + string.Join(",", coll) + "]";
            context.Response.Write(s);
        }

        /// <summary>
        /// 系统更新日志
        /// </summary>
        /// <param name="context"></param>
        private void SysUpdateLog(HttpContext context)
        {
            var list = new UpdateLog_BF().GetUpdateLog();
            var arrYear = new List<int>();//年份集合
            var outJson = new List<string>();
            foreach (var e in list)
            {
                if (!arrYear.Exists(x => x.Equals(e.CreateDate.Year)))
                    arrYear.Add(e.CreateDate.Year);
            }
            foreach (var y in arrYear)
            {
                var coll = list.FindAll(x => x.CreateDate.Year == y);
                var arrJson = new List<string>();
                foreach (var a in coll)
                {
                    var tDate = a.CreateDate;
                    var strDate = "";
                    if (tDate != null)
                    {
                        if (!string.IsNullOrEmpty(tDate.ToString()))
                            strDate = Convert.ToDateTime(tDate).ToString("yyyy-MM-dd");
                    }
                    var summary = a.Summary;
                    if (!string.IsNullOrEmpty(summary))
                        summary = summary.Replace("\r\n", "<br/>").Replace("\n", "<br/>").Replace("\r", "<br/>");
                    var json = "\"date\":\"" + a.CreateDate.ToString("MM.dd") + "\",\"xType\":\"" + a.xType + "\",\"creator\":\"" + a.CreateUserName + "\",\"createdate\":\"" + a.CreateDate.ToString("yyyy-MM-dd") + "\",\"Comment\":\"" + summary + "\",\"CreateDate\":\"" + strDate + "\"";
                    arrJson.Add("{" + json + "}");
                }
                var strJson = "\"year\":\"" + y + "\",\"info\":[" + string.Join(",", arrJson) + "]";
                outJson.Add("{" + strJson + "}");
            }
            var s = "[" + string.Join(",", outJson) + "]";
            context.Response.Write(s);
        }

        #region << 辅助方法 >>
        /// <summary>
        /// 查询表格数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="col">列名</param>
        /// <param name="key">列名对应的值</param>
        /// <param name="field">待显示的列</param>
        /// <returns></returns>
        private string FindDataTableValue(DataTable dt, string col, string key, string field)
        {
            var rows = dt.Select(string.Format("{0}='{1}'", col, key));
            if (rows.Length > 0) return rows[0][field].ToString();
            else return "";
        }
        #endregion

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}