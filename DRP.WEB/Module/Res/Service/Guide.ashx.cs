using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Res.Service
{
    /// <summary>
    /// 导游管理
    /// </summary>
    public class Guide : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询
                    QueryResource(context);
                    break;
                case 2://删除
                    DeleteResource(context);
                    break;
                case 3://以平铺模式显示所有导游
                    QueryGuide(context);
                    break;
            }
        }

        /// <summary>
        /// 查询导游
        /// </summary>
        private void QueryResource(HttpContext context)
        {
            var qry = new ResourceCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.DepartureID = context.Request["DepartureID"];
            qry.Keyword = context.Request["key"];
            var status = context.Request["Status"].ToInt();
            if (status != 0)
                qry.DataStatus = status.ToString().ToBoolen();
            else
                qry.DataStatus = null;
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Guide_BF().QueryData(qry, out total);
            var json = ConvertJson.ListToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private void DeleteResource(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                var listID = new List<string>();
                foreach (var id in keyID.Split(','))
                    listID.Add(string.Format("'{0}'", id));
                var isOk = new Guide_BF().Delete(listID);
                context.Response.Write(isOk ? "1" : "0");
            }
        }

        /// <summary>
        /// 以简洁模式查询所有导游
        /// </summary>
        /// <param name="context"></param>
        private void QueryGuide(HttpContext context)
        {
            var departureData = new Departure_BF().GetDeparture();
            var guideData = new Guide_BF().QueryData();
            var sb = new StringBuilder();
            departureData.ForEach(x =>
            {

                var coll = guideData.FindAll(t => t.DepartureID == x.ID);
                if (coll.Count > 0) //目的地下有酒店才显示出来
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td class='rowlabel'><span class='icon_destination'>{0}</span></td>", x.Name);
                    sb.Append("<td>");
                    coll.ForEach(a =>
                    {
                        sb.AppendFormat("<a href='javascript:;' onclick=\"t.fnInfo('{0}')\">{1}</a>", a.ID, a.Name);
                    });
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
            });
            var str = sb.ToString();
            if (string.IsNullOrEmpty(str))
                str = "<tr><td><div class='icon_sad'>无导游信息</div></td></tr>";
            str = str.Replace("@", "#");
            str = guideData.Count + "@" + str;
            context.Response.Write(str);
        }

        /// <summary>
        /// 数据服务权限
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override string GetNavigateID(HttpContext context)
        {
            var xType = context.Request["xType"].ToInt();//1：查询 2：管理
            var pageID = "";
            switch (xType)
            {
                case 1:
                    pageID = "guideqry";
                    break;
                case 2:
                    pageID = "guide";
                    break;
                case 3://选择导游
                    pageID = "anonymous";
                    break;
            }
            return pageID;
        }
    }
}