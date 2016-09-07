using DRP.BF.PageBase;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.Utility;
using DRP.BF.OmMrg;
using DRP.BF;
using System.Web.UI;

namespace DRP.WEB.Service
{
    /// <summary>
    /// UploadFile 的摘要说明
    /// </summary>
    public class UploadFile : HandlerBase
    {

        protected override void DRPProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://通用的文件上传
                    var en = new OrgSetting_BF().Get(AuthenticationPage.UserInfo.OrgID, OrgSettingType.UploadFile);
                    if (en != null && en.xType == 6 && en.xVal == 1)
                    {
                        UploadFileAction(context);
                    }
                    else
                    {
                        context.Response.Write("1");
                    }
                    break;
            }
        }

        /// <summary>
        /// 上传成功输出JSON数据结构
        /// </summary>
        /// <param name="context"></param>
        private void UploadFileAction(HttpContext context)
        {
            var file = context.Request.Files["Filedata"];
            var fileSize = "0KB";
            if (file != null)
            {
                #region 文件大小
                var fSize = file.ContentLength;
                decimal __size = fSize / 1024;
                if (__size < 1024)
                    fileSize = __size.ToString("f0") + "KB";
                else
                {
                    __size /= 1024;
                    if (__size < 1024) fileSize = __size.ToString("f0") + "MB";
                    else fileSize = __size.ToString("f0") + "GB";
                }
                #endregion

                #region 存储位置

                var path = DRP.Framework.DRPResolveUrl.ResolveUrl("~/Files/");
                var date = DateTime.Today.ToString("yyyyMM");
                path = path + date + "/";
                string uploadPath = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                #endregion

                //保存文件 
                var extName = Path.GetExtension(file.FileName);
                var rename = Guid.NewGuid().ToString() + extName;
                var attachment = new DRP.DAL.Glo_File();
                attachment.FileName = file.FileName;
                attachment.FileType = extName;
                attachment.FileSize = fileSize;
                attachment.CreateDate = DateTime.Now;
                attachment.ID = Guid.NewGuid().ToString();
                attachment.FilePath = ConfigHelper.GetAppSettingValue("DomainUrl") + path + rename;
                var c = "";
                if (new Attachment_BF().Save(attachment, out c))
                {
                    file.SaveAs(uploadPath + "\\" + rename);
                    var json = "\"FileName\":\"{0}\",\"FileSize\":\"{1}\",\"FileType\":\"{2}\",\"CreateDate\":\"{3}\",\"CreateUserName\":\"{4}\",\"ID\":\"{5}\",\"isOk\":\"{6}\"";
                    json = string.Format(json, attachment.FileName, fileSize, extName, attachment.CreateDate, c, attachment.ID,false);
                    context.Response.Write("{" + json + "}");
                }
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}