using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.SysMrg;
using DRP.Framework;
using DRP.Framework.Core;
using System.Text;
using System.Data;
using DRP.BF.OmMrg;

namespace DRP.WEB.Service
{
    /// <summary>
    /// 通用数据服务
    /// <remarks>不做权限验证</remarks>
    /// </summary>
    public class CommonData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://根据部门查询用户
                    QueryDeptUser(context);
                    break;
                case 2://在线帮助
                    LoadOnlineHelp(context);
                    break;
                case 3://查询机构的充值记录
                    QueryOrgPaidItem(context);
                    break;
            }
        }

        /// <summary>
        /// 查询数据用户
        /// </summary>
        /// <param name="deptID"></param>
        private void QueryDeptUser(HttpContext context)
        {
            var deptID = context.Request["deptID"];
            var list = new User_BF().GetSysUser(deptID);
            context.Response.Write(ConvertJson.ListToJson(list));
        }

        /// <summary>
        /// 在线帮助
        /// </summary>
        /// <param name="context"></param>
        private void LoadOnlineHelp(HttpContext context)
        {
            var pageID = context.Request["pageID"];
            if (string.IsNullOrEmpty(pageID)) return;

            TmsHelp.WebServiceInfo doc = new TmsHelp.WebServiceInfo();
            doc.Url = ConfigHelper.GetAppSettingValue("TMS_Help_Host");
            var dt = doc.GetHelpInfobyPageID(pageID);
            if (dt.Rows.Count == 0) return;
            DataRow row = dt.Rows[0];
            var sb = new StringBuilder();
            sb.AppendFormat("<div class='help-title'>{0}</div>", row["Subject"].ToString());
            sb.AppendFormat("<div class='help-content'>{0}</div>", row["nContent"].ToString());
            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 机构的购买记录
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrgPaidItem(HttpContext context)
        {
            var id = Guid.Parse(context.Request["id"]);
            var list = new OrgInfo_BF().GetReceiptItem(id);
            context.Response.Write(ConvertJson.ListToJson(list));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}