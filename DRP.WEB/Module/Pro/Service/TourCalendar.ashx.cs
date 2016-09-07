using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DRP.BF.PageBase;

namespace DRP.WEB.Module.Pro.Service
{
    /// <summary>
    /// 开班日历
    /// </summary>
    public class TourCalendar : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            DrawCalendar(context);
        }


        protected override string NavigateID
        {
            get
            {
                return "routeinfo";
            }
        }

        #region << 创建日历 >>

        /// <summary>
        /// 团次日历
        /// </summary>
        /// <param name="context"></param>
        private void DrawCalendar(HttpContext context)
        {
            var sDate = context.Request["st"];
            var defaultDate = DateTime.Now;
            if (!string.IsNullOrEmpty(sDate))
                defaultDate = Convert.ToDateTime(sDate);
            var nextDate = defaultDate.AddMonths(1);
            var sCalendar = DrawCalendar(defaultDate.Year, defaultDate.Month);
            var eCalendar = DrawCalendar(nextDate.Year, nextDate.Month, true);
            var sb = new StringBuilder();

            sb.Append("<table class='tblCalendar' cellpadding='1' cellspacing='1'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.AppendFormat("<th class='preMonth'><a href='javascript:;' onclick=\"t.fnGoToCalendar('{0}')\">上一月</a></th>",
                defaultDate.AddMonths(-1).ToString("yyyy-MM-dd"));
            sb.AppendFormat("<th>{0} ~ {1}</th>", defaultDate.ToString("yyyy年MM月"), nextDate.ToString("yyyy年MM月"));
            sb.AppendFormat("<th class='nextMonth'><a href='javascript:;' onclick=\"t.fnGoToCalendar('{0}')\">下一月</a></th>",
                defaultDate.AddMonths(2).ToString("yyyy-MM-dd"));
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.AppendFormat("<td colspan='3'>{0}</td>", sCalendar + eCalendar);
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");

            context.Response.Write(sb.ToString());
        }

        #region 单元格
        private string ConvertToChs(int Num)
        {
            switch (Num)
            {
                case 0:
                    return "<span>日</span>";
                case 1:
                    return "一";
                case 2:
                    return "二";
                case 3:
                    return "三";
                case 4:
                    return "四";
                case 5:
                    return "五";
                case 6:
                    return "<span>六</span>";
                default:
                    return "";
            }
        }

        private string GetCell(DateTime dt)
        {
            var td = "<td tag='{0}'>{1}</td>";
            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
            {
                if (dt == DateTime.Today)
                    td = string.Format(td, dt.ToString("yyyy-MM-dd"), "<span class='weekend today'>" + dt.ToString("dd") + "</span>");
                else
                    td = string.Format(td, dt.ToString("yyyy-MM-dd"), "<span class='weekend'>" + dt.ToString("dd") + "</span>");
            }
            else
            {
                if (dt == DateTime.Today)
                    td = string.Format(td, dt.ToString("yyyy-MM-dd"), "<span class='today'>" + dt.ToString("dd") + "</span>");
                else
                    td = string.Format(td, dt.ToString("yyyy-MM-dd"), dt.ToString("dd"));
            }
            return td;
        }
        #endregion

        /// <summary>
        /// HTML日历
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="Width">日历宽度</param>
        /// <returns>HtmlTable</returns>
        private string DrawCalendar(int year, int month, bool isShowborder = false)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<table style='border-left:{0}' cellspacing='0' cellpadding='0'>", isShowborder ? "1px solid #9CB8E7" : "0px");

            #region 表头
            sb.Append("<tr>");
            for (int i = 0; i < 7; i++)
            {
                var td = "<td>{0}</td>";
                sb.AppendFormat(td, ConvertToChs(i));
            }
            sb.Append("</tr>");
            #endregion

            DateTime dt = Convert.ToDateTime(year.ToString() + "-" + month.ToString() + "-01");//每月一号
            int preDay = Convert.ToInt32(dt.DayOfWeek);//每月一号是周几

            #region 每月一号前填充"-"
            sb.Append("<tr>");
            for (int i = 0; i < preDay; i++)
            {
                sb.Append("<td>-</td>");
            }
            #endregion

            #region 表体
            int dayOfMonth = int.Parse(dt.AddMonths(1).AddDays(-1).ToString("dd")) + preDay;
            int temp = preDay;
            while (temp < dayOfMonth)
            {

                if (temp % 7 == 0 && temp > 0)
                {
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append(GetCell(dt));
                    dt = dt.AddDays(1);
                }
                else
                {
                    sb.Append(GetCell(dt));
                    dt = dt.AddDays(1);
                }
                temp++;
            }

            dt = Convert.ToDateTime(year.ToString() + "-" + month.ToString() + "-01");//每月一号		
            int tp = Convert.ToInt32(dt.AddMonths(1).AddDays(-1).DayOfWeek);//每月月终是周几，填充空白
            for (int i = tp + 1; i < 7; i++)
            {
                sb.Append("<td>-</td>");
            }
            sb.Append("</tr>");
            sb.Append("</table>");
            #endregion

            return sb.ToString();
        }
        #endregion
    }
}