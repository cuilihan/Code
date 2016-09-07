using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Res.Service
{
    /// <summary>
    /// 通用资源辅助类
    /// </summary>
    public class Resource : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://线路类型
                    BindRouteType(context);
                    break;
                case 2: //按线路类型查询目的地(combotree)
                    BindDestination(context);
                    break;
                case 3: //根据资源类型查询供应商
                    QueryResource(context);
                    break;
                case 4://交易订单
                    TradeOrder(context);
                    break;
            }
        }

        /// <summary>
        /// 线路类型
        /// </summary>
        /// <param name="context"></param>
        private void BindRouteType(HttpContext context)
        {
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            var coll = new List<string>();
            list.ForEach(x =>
            {
                var json = string.Format("\"id\":\"{0}\",\"text\":\"{1}\"", x.ID, x.Name);
                coll.Add("{" + json + "}");
            });
            var str = "[" + string.Join(",", coll) + "]";
            context.Response.Write(str);
        }

        /// <summary>
        /// 目的地
        /// </summary>
        /// <param name="context"></param>
        private void BindDestination(HttpContext context)
        {
            var routeTypeID = context.Request["routeTypeID"];
            var json = new Destination_BF().GetDestinationComboTree(routeTypeID);
            context.Response.Write(json);
        }

        #region << 根据资源类型查询供应商：应用于订单管理功能中 >>
        /// <summary>
        /// 根据资源类型查询供应商
        /// </summary>
        /// <param name="context"></param>
        private void QueryResource(HttpContext context)
        {
            var itemType = context.Request["itemType"].ToInt();
            var dt = new ResourceUtility().QueryResource(itemType);
            context.Response.Write(ConvertJson.ToJson(dt));
        }
        #endregion


        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="context"></param>
        private void TradeOrder(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.Keyword = context.Request["key"];
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var resouceID = context.Request["resID"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new ResourceUtility().TradeOrder(qry, sDate, eDate, resouceID, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }



        /// <summary>
        /// 允许匿名访问
        /// </summary>
        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}