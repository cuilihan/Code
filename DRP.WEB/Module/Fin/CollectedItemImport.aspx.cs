using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Fin;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin
{
    public partial class CollectedItemImport : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string NavigateID
        {
            get
            {
                return "fincollected";
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            var rec = ImpMakeCollection();
            switch (rec)
            {
                case -1:
                    JScript.Alert("上传文件失败");
                    break;
                case -2:
                    JScript.Alert("导入资料时发生错误");
                    break;
                default:
                    JScript.ScriptStartUp("fnFinish('" + rec + "');");
                    break;
            }
        }

        #region 导入银行收款

        /// <summary>
        /// 导入银行收款
        /// </summary>
        /// <param name="context"></param>
        private int ImpMakeCollection()
        {
            var path = DRP.Framework.DRPResolveUrl.ResolveUrl("~/Files/Temp/");
            string vPath = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(vPath))
            {
                Directory.CreateDirectory(vPath);
            }
            var fileName = UpLoadFile.UpLoad(FileCollected, vPath);
            if (string.IsNullOrEmpty(fileName))
            {
                return -1;
            }
            fileName = vPath + fileName;
            ExcelHelper exl = new ExcelHelper();
            CollectedItem_BF dal = new CollectedItem_BF();

            var iCount = 0;
            try
            {
                var list = exl.GetExcelSheetNameArray(fileName);
                foreach (var s in list)
                {
                    var dt = exl.GetExcelDataAsTable(fileName, s);
                    iCount += dal.ImportData(dt, s);
                }
                return iCount;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "导入银行到账资料发生错误");
                return -2;
            }
        }

        #endregion
    }
}