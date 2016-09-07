using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.PageBase;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Res.Service
{
    /// <summary>
    /// 其他通用资源管理
    /// </summary>
    public class OtherRes : HandlerBase
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
            qry.Keyword = context.Request["key"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Other_BF().QueryData(qry, out total);
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
                var isOk = new Other_BF().Delete(listID);
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
            DAL.Res_Other entity = new DAL.Res_Other();
            entity.ID = context.Request["KeyID"];
            entity.ID = string.IsNullOrEmpty(entity.ID) ? Guid.NewGuid().ToString() : entity.ID;
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
            var isOk = new Other_BF().Save(entity, list);
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
            return xType == 1 ? "otherresqry" : "otherres";
        }
    }
}