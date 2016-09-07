using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.Framework.Core;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Om
{
    public partial class OrgInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var qry = new QueryCriteriaBase();
            qry.Keyword = txtKey.Value;

            var list = new List<NExcelCellFormat>();
            list.Add(new NExcelCellFormat(6, "AreaName", "区域", NExcelCellFormatStyle.Normal));
            list.Add(new NExcelCellFormat(10, "Name", "公司名称", NExcelCellFormatStyle.Normal));
            list.Add(new NExcelCellFormat(10, "ProDomain", "发布网址", NExcelCellFormatStyle.Normal));
            list.Add(new NExcelCellFormat(10, "OpenDate", "开通日期", NExcelCellFormatStyle.Normal));
            list.Add(new NExcelCellFormat(10, "ExpiryDate", "截止日期", NExcelCellFormatStyle.Normal));
            list.Add(new NExcelCellFormat(10, "OrgContact", "注册人", NExcelCellFormatStyle.Normal));
            list.Add(new NExcelCellFormat(10, "ContactPhone", "联系方式", NExcelCellFormatStyle.Normal));

            var dt = new OrgInfo_BF().QueryOrder_All(qry);
            //查询条件输出
            new NewExcelHelper().RptData_NExcel("机构信息表", "", dt, this.Response, this.Request, list, true);
        }
    }
}