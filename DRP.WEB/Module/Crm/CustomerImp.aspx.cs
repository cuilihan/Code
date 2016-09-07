using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Crm;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Crm
{
    public partial class CustomerImport : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string NavigateID
        {
            get
            {
                return "customer";
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            var path = Server.MapPath("/Files/Temp/");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            var fileName = UpLoadFile.UpLoad(FileCustomer, path);
            if (string.IsNullOrEmpty(fileName))
            {
                JScript.Alert("上传客户资料文件时发生错误");
                return;
            }
            else
            {
                var iCount = new Customer_BF().Import(path + fileName);
                if (iCount == -1)
                {
                    JScript.Alert("导入客户资料时发生错误");
                }
                else
                {
                    JScript.Alert("导入完成，成功导入" + iCount + "条客户资料");
                }
            }
        }
    }
}