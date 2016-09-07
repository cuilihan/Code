using DRP.BF.eShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.Framework.Core;
using System.IO;

namespace DRP.eShop.Module.My.Service
{
    /// <summary>
    /// Photo 的摘要说明
    /// </summary>
    public class Photo : mHandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1: //更新会员头像（上传）
                    UploadPhoto(context);
                    break;
                case 2://更新系统内的头像
                    UpdateSysPhoto(context);
                    break;
            }
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="context"></param>
        private void UploadPhoto(HttpContext context)
        {
            var file = context.Request.Files["Filedata"];
            if (file != null)
            {
                #region 存储位置
                var path = "/Files/Img/" + DateTime.Today.ToString("yyyyMM");
                var vPath = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(vPath)) Directory.CreateDirectory(vPath);
                #endregion

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var url = path + "/" + fileName;
                file.SaveAs(vPath + "\\" + fileName);
                var isOk = new eShopMemeber().UpdatePhoto(url);
                if (isOk) context.Response.Write(url);
                else
                    context.Response.Write("-1");
            }
        }

        private void UpdateSysPhoto(HttpContext context)
        {
            var photo = context.Request["Src"];
            var isOk = new eShopMemeber().UpdatePhoto(photo);
            context.Response.Write(isOk ? "1" : "-1"); 
        }
    }
}