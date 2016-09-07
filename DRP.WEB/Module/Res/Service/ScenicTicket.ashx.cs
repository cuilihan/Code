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
    /// 景点门票
    /// </summary>
    public class ScenicTicket : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://资源查询
                    QueryResource(context);
                    break;
                case 2://保存
                    SaveResource(context);
                    break;
                case 3: //删除
                    DeleteResource(context);
                    break;
                case 4://查询业务联系人
                    QueryBizContact(context);
                    break;
                case 5://平铺方式查询所有景点门票
                    QueryScenicSpotTile(context);
                    break;
            }
        }

        /// <summary>
        /// 查询资源
        /// </summary>
        private void QueryResource(HttpContext context)
        {
            var qry = new ResourceCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.RouteTypeID = context.Request["routeTypeID"];
            qry.DestinationID = context.Request["destinationID"];
            qry.Keyword = context.Request["key"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new ScenicTicket_BF().QueryData(qry, out total);
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
                listID.AddRange(keyID.Split(','));
                var isOk = new ScenicTicket_BF().Delete(listID);
                context.Response.Write(isOk ? "1" : "0");
            }
        }

        #region << 保存资源 >>
        /// <summary>
        /// 保存资源
        /// </summary>
        /// <param name="context"></param>
        private void SaveResource(HttpContext context)
        {
            DAL.Res_ScenicTicket entity = new DAL.Res_ScenicTicket();
            entity.ID = context.Request["KeyID"];
            entity.ID = string.IsNullOrEmpty(entity.ID) ? Guid.NewGuid().ToString() : entity.ID;
            entity.RouteTypeID = context.Request["RouteTypeID"];
            var rEntity = new BasicInfo_BF().Get(entity.RouteTypeID);
            entity.RouteType = rEntity == null ? "" : rEntity.Name;
            entity.DestinationID = context.Request["DestinationID"];
            var dEntity = new Destination_BF().Get(entity.DestinationID);
            entity.Destination = dEntity == null ? "" : dEntity.Name;
            entity.DestinationPath = new Destination_BF().GetDestinationPathID(entity.DestinationID);
            entity.Name = context.Request["Name"];
            entity.Spell = EcanConvertToCh.GetFirstChar(entity.Name);//取第一个文字的拼音首字母  
            entity.TeamPrice = context.Request["TeamPrice"];
            entity.CooperatePrice = context.Request["CooperatePrice"];
            entity.NormalPrice = context.Request["NormalPrice"];
            entity.IsEnable = context.Request["IsEnable"].ToBoolen();
            entity.Contact = context.Request["Contact"];
            entity.Title = context.Request["Title"];
            entity.Mobile = context.Request["Mobile"];
            entity.Phone = context.Request["Phone"];
            entity.Fax = context.Request["Fax"];
            entity.Mail = context.Request["Mail"];
            entity.Addr = context.Request["Addr"];
            entity.Remark = context.Request["Comment"];
            entity.OrderIndex = context.Request["OrderIndex"].ToInt();
            var xmlData = context.Request["Item"];
            var list = new ResourceUtility().ToBizContactList(xmlData, entity.ID);
            var isOk = new ScenicTicket_BF().Save(entity, list);
            context.Response.Write(isOk ? "1" : "0");
        }

        #endregion

        #region << 查询所有地接社地（以平铺方式） >>

        private void QueryScenicSpotTile(HttpContext context)
        {
            var routeTypeID = context.Request["routeTypeID"];
            var list = new ScenicTicket_BF().QueryData(routeTypeID);//地接社
            var destinationList = new Destination_BF().GetDestinationList();//目的地
            destinationList = destinationList.FindAll(x => x.RouteTypeID == routeTypeID
                && x.ParentID == Guid.Empty.ToString());
            var sb = new StringBuilder();
            destinationList.ForEach(x =>
            {

                var coll = list.FindAll(t => t.DestinationPath.Contains(x.ID));
                if (coll.Count > 0) //目的地下有景点门票才显示出来
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
                str = "<tr><td><div class='icon_sad'>无景点门票信息</div></td></tr>";
            context.Response.Write(str);
        }
        #endregion

        /// <summary>
        /// 业务联系人
        /// </summary>
        /// <param name="context"></param>
        private void QueryBizContact(HttpContext context)
        {
            var resourceID = context.Request["fkID"];
            var list = new ResourceUtility().GetBizContact(resourceID);
            var json = ConvertJson.ListToJson(list);
            context.Response.Write(json);
        }


        /// <summary>
        /// 数据服务权限
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override string GetNavigateID(HttpContext context)
        {
            var xType = context.Request["xType"].ToInt();//1：查询 2：管理
            return xType == 1 ? "scenicspotqry" : "scenicspot";
        }

    }
}