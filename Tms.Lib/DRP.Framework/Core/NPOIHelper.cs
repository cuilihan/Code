using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DRP.Framework.Core
{
    /// <summary>
    /// NPOI导出、导入Excel
    /// <remarks>不依赖于Office组件</remarks>
    /// </summary>
    public class NPOIHelper
    {
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        private static MemoryStream Export(DataTable dtSource, string strHeaderText, List<ExcelCellFormat> list)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "苏州大途网络科技有限公司";
            workbook.DocumentSummaryInformation = dsi;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "Kimlee"; //填加xls文件作者信息
            si.ApplicationName = "旅管家"; //填加xls文件创建程序信息
            si.LastAuthor = "Kimlee"; //填加xls文件最后保存者信息
            si.Comments = "Kimlee"; //填加xls文件作者信息 
            si.Subject = "旅管家订单管理";
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            #region 表头及样式
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
            headerRow.HeightInPoints = 25;
            headerRow.CreateCell(0).SetCellValue(strHeaderText);
            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 20;
            font.Boldweight = 700;
            headStyle.SetFont(font);
            headerRow.GetCell(0).CellStyle = headStyle;
            sheet.AddMergedRegion(new Region(0, 0, 0, list.Count - 1)); 
            #endregion

            #region 列头及样式
            headerRow = (HSSFRow)sheet.CreateRow(1); 
            headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.Boldweight = 700;
            headStyle.SetFont(font);
            var i = 0;
            list.ForEach(x =>
            {
                headerRow.CreateCell(i).SetCellValue(x.ColumnName);
                headerRow.GetCell(i).CellStyle = headStyle;
                sheet.SetColumnWidth(i, x.CharLength * 256); //设置列宽
                i++;
            });
            #endregion

            int rowIndex = 2;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                i = 0;
                list.ForEach(x =>
                {
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(i);
                    var v = row[x.ColumField].ToString();

                    switch (x.DbType)
                    {
                        case DbType.DateTime:
                            if (string.IsNullOrEmpty(v))
                                newCell.SetCellValue("");
                            else
                            {
                                DateTime dtVal;
                                DateTime.TryParse(v, out dtVal);
                                newCell.SetCellValue(dtVal);
                                newCell.CellStyle = dateStyle;
                            }
                            break;
                        case DbType.Int16:
                            int intVal;
                            Int32.TryParse(v, out intVal);
                            newCell.SetCellValue(intVal);
                            break;
                        case DbType.Int32:
                            int iVal;
                            Int32.TryParse(v, out iVal);
                            newCell.SetCellValue(iVal);
                            break;
                        case DbType.Decimal:
                            double dVal;
                            double.TryParse(v, out dVal);
                            newCell.SetCellValue(dVal);
                            break;
                        case DbType.Double:
                            double dblVal;
                            double.TryParse(v, out dblVal);
                            newCell.SetCellValue(dblVal);
                            break;
                        default:
                            newCell.SetCellValue(v);
                            break;
                    }
                    i++;
                });
                #endregion

                rowIndex++;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }

        /// <summary>
        /// DataTable导出到Excel文件,用于WinForm等导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName, List<ExcelCellFormat> list)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText, list))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }


        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName, List<ExcelCellFormat> list)
        {
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
            "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
            curContext.Response.BinaryWrite(Export(dtSource, strHeaderText, list).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// 读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }

            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
    }
}
