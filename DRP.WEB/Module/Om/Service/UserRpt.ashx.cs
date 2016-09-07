using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using DRP.BF.OmMrg;
using DRP.BF.PageBase;
using DRP.Framework;

namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// 机构用户统计
    /// </summary>
    public class UserRpt : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            UserStatistic(context);
        }

        private void UserStatistic(HttpContext context)
        {
            var sb = new StringBuilder();
            var dt = new OmUserRpt_BF().OrgStatistic();
            var tUserNum = 0;
            var tOrgNum = 0;
            var idx = 1;
            foreach (DataRow row in dt.Rows)
            {
                var AreaName = row["AreaName"].ToString();
                var orgCount = row["iCount"].ToString().ToInt();
                var userCount = row["UserNum"].ToString().ToInt();
                sb.Append("<tr>");
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", idx++);
                sb.AppendFormat("<td>{0}</td>", AreaName);
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", orgCount);
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", userCount);
                sb.Append("</tr>");
                tOrgNum += orgCount;
                tUserNum += userCount;
            }
            sb.Append("<tr>");
            sb.Append("<td colspan='2' style='font-weight:bold; text-align:center;'>合计</t>");
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", tOrgNum);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", tUserNum);
            sb.Append("</tr>");
            context.Response.Write(sb.ToString());
        }

        protected override string NavigateID
        {
            get
            {
                return "omuserrpt";
            }
        }
    }
}