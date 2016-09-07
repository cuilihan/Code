using DRP.BF.PageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.BF.mSite;
using System.IO;

namespace DRP.WEB.Module.mSite.Service
{
    /// <summary>
    /// mSiet 的摘要说明
    /// </summary>
    public class mSiet : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            switch (context.Request["action"].ToInt())
            {
                case 1://保存
                    Save(context);
                    break;
                case 2://上传Logo
                    UploadFile(context);
                    break;
            }
        }

        private void Save(HttpContext context)
        {
            var e = new DAL.Sn_BasicInfo();
            e.IsShowRoute = context.Request["IsShowRoute"].ToInt() == 1;
            e.LinkUrl = context.Request["Domain"];
            e.TravelName = context.Request["TravelName"];
            e.HotLine = context.Request["Phone"];
            e.Logo = context.Request["LogoUrl"];
            e.AboutUs = context.Request["AboutUs"]; 
            var adData = context.Request["AdData"];
            var isOk = new mSite_BF().SaveSetting(e,  adData);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="context"></param>
        private void UploadFile(HttpContext context)
        {
            var file = context.Request.Files["Filedata"];
            if (file != null)
            {
                #region 存储位置
                var path = "/Files/eShop/" + DateTime.Today.ToString("yyyyMM");
                var vPath = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(vPath)) Directory.CreateDirectory(vPath);
                #endregion

                var fileName =Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var url = ConfigHelper.GetAppSettingValue("DomainUrl") + path + "/" + fileName;
                file.SaveAs(vPath + "\\" + fileName);
                context.Response.Write(url);
            }
        }

    

        protected override string NavigateID
        {
            get
            {
                return "microsite";
            }
        }

    }
}