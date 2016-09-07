using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Res.Service
{
    /// <summary>
    /// 购物店
    /// </summary>
    public class Shopping : HandlerBase
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
            var dt = new Shopping_BF().QueryData(qry, out total);
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
                var isOk = new Shopping_BF().Delete(listID);
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
            DAL.Res_Shopping entity = new DAL.Res_Shopping();
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
            entity.IsEnable = context.Request["IsEnable"].ToBoolen();
            entity.Contact = context.Request["Contact"]; 
            entity.Mobile = context.Request["Mobile"];
            entity.Phone = context.Request["Phone"]; 
            entity.Mail = context.Request["Mail"];
            entity.Addr = context.Request["Addr"];
            entity.Remark = context.Request["Comment"];
            entity.OrderIndex = context.Request["OrderIndex"].ToInt();
            var xmlData = context.Request["Item"];
            var list = new ResourceUtility().ToBizContactList(xmlData, entity.ID);
            var isOk = new Shopping_BF().Save(entity, list);
            context.Response.Write(isOk ? "1" : "0");
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
            return xType == 1 ? "shoppingqry" : "shopping";
        }
         
    }
}