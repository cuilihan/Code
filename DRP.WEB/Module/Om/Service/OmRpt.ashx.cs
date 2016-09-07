using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.PageBase;
using DRP.BF.OmMrg;
using System.Text;
using System.Data;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// OmRpt 的摘要说明
    /// </summary>
    public class OmRpt : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://指定日期开通机构明细
                    QuantityRpt(context);
                    break;
                case 2://生成日历
                    CreateDay(context);
                    break;
                case 3://按月度统计数量
                    OpenRateStatistic(context);
                    break;
                case 4://机构订单量统计
                    OrgOrderQuantityStatistic(context);
                    break;
                case 5: //统计订单的应收款与人数
                    OrderReceivableStatisitic(context);
                    break;
                case 6://针对机构收取服务费用统计
                    OrgReceiptStatistic(context);
                    break;
            }
        }

        #region 机构开通统计

        /// <summary>
        /// 指定日期开通机构明细
        /// </summary>
        /// <param name="context"></param>
        private void QuantityRpt(HttpContext context)
        {
            var y = context.Request["y"].ToInt();
            var m = context.Request["m"].ToInt();
            var d = context.Request["d"].ToInt();
            var sDate = new DateTime(y, m, d);
            var list = new OmRpt_BF().QuantityRpt(sDate.ToString("yyyy-MM-dd"));
            var sb = new StringBuilder();
            var idx = 1;
            list.ForEach(x =>
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", idx++);
                sb.AppendFormat("<td>{0}</td>", x.Name);
                sb.AppendFormat("<td>{0}</td>", x.OrgContact);
                sb.AppendFormat("<td>{0}</td>", x.ContactPhone);
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", x.CreateDate.ToString("yyyy-MM-dd"));
                sb.AppendFormat("<td style='text-align:center;'>{0}~{1}</td>", x.OpenDate.ToString("yyyy-MM-dd"), x.ExpiryDate.ToString("yyyy-MM-dd"));
                sb.AppendFormat("<td>{0}</td>", x.ProDomain);
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", x.NavGroup);
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", x.CreateUserName);
                sb.Append("</tr>");
            });
            var str = sb.ToString();
            str = string.IsNullOrEmpty(str) ? "<tr><td colspan='10'>无</td></tr>" : str;
            context.Response.Write(str);
        }

        /// <summary>
        /// 生成日历
        /// </summary>
        /// <param name="context"></param>
        private void CreateDay(HttpContext context)
        {
            var y = context.Request["Y"].ToInt();
            var m = context.Request["M"].ToInt();
            var sDate = new DateTime(y, m, 1);
            var eDate = sDate.AddMonths(1);
            var ts = new TimeSpan();
            ts = eDate - sDate;
            var sb = new StringBuilder();
            var cDay = DateTime.Now.Day;
            for (var i = 1; i <= ts.TotalDays; i++)
            {
                var cls = i == cDay ? "c_day_on" : "";
                sb.AppendFormat("<a d='{0}' class='{1}'>{0}</a>", i, cls);
            }
            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 按月度统计开通机构数量
        /// </summary>
        /// <param name="context"></param>
        private void OpenRateStatistic(HttpContext context)
        {
            var year = context.Request["y"].ToInt();
            var dt = new OmRpt_BF().OrgOpenQuantityStatistic(year);
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
                var strJson = "\"M\":\"{0}\",\"Num\":\"{1}\"";
                var row = dt.Select(string.Format("M='{0}'", i++));
                var n = row.Length == 0 ? 0 : (row[0]["iCount"].ToString().ToInt());
                var json = string.Format(strJson, x, n);
                coll.Add("{" + json + "}");
            });
            var s = "[" + string.Join(",", coll) + "]";
            context.Response.Write(s);
        }

        #endregion

        #region 机构的订单量统计

        /// <summary>
        /// 机构订单量统计
        /// </summary>
        /// <param name="context"></param>
        private void OrgOrderQuantityStatistic(HttpContext context)
        {
            var orgName = HttpUtility.HtmlDecode(context.Request["name"]);
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var dt = new OmRpt_BF().OrgOrderQuantityStatistic(orgName, sDate, eDate);
            var sb = new StringBuilder();
            if (dt.Rows.Count == 0)
                sb.Append("<tr><td>未查询到数据</td></tr>");
            else
            {
                var nLen = dt.Rows.Count;
                var halfLen = nLen / 2;
                if (nLen % 2 != 0)
                    halfLen += 1;
                for (var i = 0; i < halfLen; i++)
                {
                    sb.Append("<tr>");
                    #region 最多订单的一半企业
                    sb.AppendFormat("<td style='text-align:center;'>{0}</td>", (i + 1));
                    sb.AppendFormat("<td>【{0}】{1}</td>", dt.Rows[i]["AreaName"].ToString(), dt.Rows[i]["Name"].ToString());
                    sb.AppendFormat("<td style='text-align:center;'>{0}【{1}】</td>", dt.Rows[i]["OrgContact"].ToString(), dt.Rows[i]["ContactPhone"].ToString());
                    sb.AppendFormat("<td style='text-align:center;'>{0}</td>", Convert.ToDateTime(dt.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd"));
                    sb.AppendFormat("<td style='text-align:center;'>{0}</td>", Convert.ToDateTime(dt.Rows[i]["ExpiryDate"].ToString()).ToString("yyyy-MM-dd"));
                    sb.AppendFormat("<td style='text-align:center;'>{0}</td>", dt.Rows[i]["iCount"].ToString());

                    #endregion

                    #region 最少订单的一半企业
                    if (halfLen + 1 + i > nLen)
                    {
                        sb.AppendFormat("<td style='text-align:center;'>{0}</td>", (halfLen + i + 1));
                        sb.Append("<td></td>");
                        sb.Append("<td style='text-align:center;'></td>");
                        sb.Append("<td style='text-align:center;'></td>");
                        sb.Append("<td style='text-align:center;'></td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td style='text-align:center;'>{0}</td>", (halfLen + i + 1));
                        sb.AppendFormat("<td>【{0}】{1}</td>", dt.Rows[halfLen + i]["AreaName"].ToString(), dt.Rows[halfLen + i]["Name"].ToString());
                        sb.AppendFormat("<td style='text-align:center;'>{0}【{1}】</td>",
                            dt.Rows[halfLen + i]["OrgContact"].ToString(), dt.Rows[halfLen + i]["ContactPhone"].ToString());
                        sb.AppendFormat("<td style='text-align:center;'>{0}</td>", Convert.ToDateTime(dt.Rows[halfLen + i]["CreateDate"].ToString()).ToString("yyyy-MM-dd"));
                        sb.AppendFormat("<td style='text-align:center;'>{0}</td>", Convert.ToDateTime(dt.Rows[halfLen + i]["ExpiryDate"].ToString()).ToString("yyyy-MM-dd"));
                        sb.AppendFormat("<td style='text-align:center;'>{0}</td>", dt.Rows[halfLen + i]["iCount"].ToString());

                    }
                    #endregion
                    sb.Append("</tr>");
                }
            }
            context.Response.Write(sb.ToString());
        }
        #endregion

        #region 订单的应收款与人数

        /// <summary>
        /// 订单应收款与人数的统计
        /// </summary>
        /// <param name="context"></param>
        private void OrderReceivableStatisitic(HttpContext context)
        {
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var dt = new OmRpt_BF().OrderReceivableStatistic(sDate, eDate);
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
            var coll = new List<string>();
            list.ForEach(x =>
            {
                var strJson = "\"Month\":\"{0}\",\"VisitorNum\":\"{1}\",\"OrderAmt\":\"{2}\",\"OrderNum\":\"{3}\"";
                var vNum = FindDataTableValue(dt, "M", i.ToString(), "VisitorNum");
                var orderAmt = FindDataTableValue(dt, "M", i.ToString(), "OrderAmt");
                var orderNum = FindDataTableValue(dt, "M", i.ToString(), "OrderNum");
                var json = string.Format(strJson, x, vNum, orderAmt, orderNum);
                coll.Add("{" + json + "}");
                i++;
            });
            var s = "[" + string.Join(",", coll) + "]";
            context.Response.Write(s);
        }

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

        #region 针对机构收取服务费用统计
        /// <summary>
        /// 收款统计
        /// </summary>
        /// <param name="context"></param>
        private void OrgReceiptStatistic(HttpContext context)
        {
            var year = context.Request["y"].ToInt();
            var dt = new OmRpt_BF().OrgReceiptAmoutStatistic(year);
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
                var strJson = "\"M\":\"{0}\",\"Num\":\"{1}\"";
                var row = dt.Select(string.Format("M='{0}'", i++));
                var n = row.Length == 0 ? 0 : (row[0]["Amt"].ToString().ToInt());
                var json = string.Format(strJson, x, n);
                coll.Add("{" + json + "}");
            });
            var s = "[" + string.Join(",", coll) + "]";
            context.Response.Write(s);
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