using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF;
using DRP.BF.PageBase;
using DRP.BF.OmMrg;
using DRP.Framework.Core;
using ThoughtWorks.QRCode.Codec;
using System.IO;


namespace DRP.WEB.Module.Om.Service
{
    /// <summary>
    /// 机构信息管理
    /// </summary>
    public class OrgInfo : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询数据
                    QueryData(context);
                    break;
                case 2: //删除 
                    Delete(context);
                    break;
                case 3://创建二维码
                    CreateQRCode(context);
                    break;
                case 4://机构数据初始化
                    OrgInitData(context);
                    break;
                case 5://开通快捷游机票预订系统接口（同步数据）
                    OpenKjYouNet(context);
                    break;
                case 6: //收款明细
                    QueryReceiptItem(context);
                    break;
                case 7://删除收款
                    DeleteReceiptItem(context);
                    break;
            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        private void QueryData(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.DataStatus = context.Request["xType"].ToInt();
            qry.Keyword = context.Request["key"];

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new OrgInfo_BF().GetOrgInfo(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 收款明细
        /// </summary>
        /// <param name="context"></param>
        private void QueryReceiptItem(HttpContext context)
        {
            var list = new OrgInfo_BF().GetReceiptItem((Guid)context.Request["orgID"].ToGuid());
            context.Response.Write(ConvertJson.ListToJson(list));
        }

        /// <summary>
        /// 删除收款明细
        /// </summary>
        /// <param name="context"></param>
        private void DeleteReceiptItem(HttpContext context)
        {
            var keyID = context.Request["id"].ToGuid();
            var isOk = new OrgInfo_BF().DeleteReceipt((Guid)keyID);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                new OrgInfo_BF().Delete(keyID);
                context.Response.Write("1");
            }
        }

        /// <summary>
        /// 创建机构二维码
        /// </summary>
        /// <param name="context"></param>
        private void CreateQRCode(HttpContext context)
        {
            try
            {
                var id = context.Request["ID"];
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                var data = string.Format(ConfigHelper.GetAppSettingValue("WechatDomain"), id);
                var path = HttpContext.Current.Server.MapPath("/Files/Wechat/");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var fileName = path + id + ".jpg";
                var image = qrCodeEncoder.Encode(data);
                image.Save(fileName);

                new OrgInfo_BF().UpdateQRCode(id, data);
                context.Response.Write(1);
            }
            catch
            {
                context.Response.Write(0);
            }
        }

        /// <summary>
        /// 机构数据初始化
        /// </summary>
        /// <param name="context"></param>
        private void OrgInitData(HttpContext context)
        {
            var orgID = context.Request["id"];
            var isOk = new OrgInit_BF().InitData(orgID);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 开通快捷游机票预订系统接口（同步数据）
        /// </summary>
        /// <param name="context"></param>
        private void OpenKjYouNet(HttpContext context)
        {
            var id = context.Request["id"];
            var isOpen = context.Request["isOpen"].Equals("1");//是否已开通
            var e = new OrgInfo_BF().Get(id);
            var isOk = new AirTicketOrderService().SyncOrgInfo(e, !isOpen);
            context.Response.Write(isOk ? "1" : "");
        }

        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }
    }
}