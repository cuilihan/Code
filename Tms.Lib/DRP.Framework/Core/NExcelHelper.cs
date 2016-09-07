using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.IO;
using System.Web;
using NPOI.SS.Util;
using DRP.Framework;

namespace DRP.Framework.Core
{

    public enum NExcelCellFormatStyle
    {
        Center,
        Double,
        Date,
        Int,
        Normal
    }

    /// <summary>
    /// 导出Excel单元格属性
    /// </summary>
    [Serializable]
    public class NExcelCellFormat
    {
        /// <summary>
        /// 字符宽度
        /// </summary>
        public ushort CharLength { get; set; }

        /// <summary>
        /// 字段Filed
        /// </summary>
        public string ColumField { get; set; }


        public NExcelCellFormatStyle ColumStyle { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        ///  string[] strs = { "0|新建", "1|已汇", "2|已报账", "3|已结清" };
        /// </summary>
        public string[] ColumEnm { get; set; }

        /// <summary>
        /// Col1 -Col2+ Col3
        /// </summary>
        public string[] CalculateFunc { get; set; }

        public NExcelCellFormat() { }

        public NExcelCellFormat(ushort length, string colField, string colName, NExcelCellFormatStyle colStyle, string[] Enm = null, string[] __CalculateFunc = null)
        {
            CharLength = length;
            ColumField = colField;
            ColumnName = colName;
            ColumStyle = colStyle;
            ColumEnm = Enm;
            CalculateFunc = __CalculateFunc;
        }
    }


    public class NExcelHelper
    {
        #region << 通用方法封装 >>

        /// <summary>
        /// 导出的方法，收入明细表,付款明细报表
        /// </summary>
        /// <param name="__fileName">导出的名称程序自动加上时间结尾</param>
        /// <param name="__title">导出标题如果存在需要显示筛选条件</param>
        /// <param name="dtSource">数据</param>
        /// <param name="rp">页面调用输入this.Response</param>
        /// <param name="rq">页面调用输入this.Request</param>
        /// <param name="list">表头</param>
        /// <param name="Istotal">是否输出合计，输出合计是根据表头是否为double和int</param>
        public void RptData_NExcel(string __fileName, string __title, DataTable dtSource, HttpResponse rp, HttpRequest rq, List<NExcelCellFormat> list = null, bool Istotal = false)
        {
            #region << Excel工作文档 >>
            HSSFWorkbook wk = new HSSFWorkbook();
            var title = __fileName + DateTime.Now.ToString("yyyyMMddss");
            ISheet sheet = wk.CreateSheet(title);

            // 通用字体
            IFont fontCell = wk.CreateFont();
            fontCell.FontName = "宋体";

            // 加粗字体
            IFont fontBold = wk.CreateFont();
            fontBold.FontName = "宋体";
            fontBold.Boldweight = (short)FontBoldWeight.BOLD;
            #endregion

            #region << 设置表格头 >>
            // 表格头样式
            ICellStyle styleHeader = wk.CreateCellStyle();
            styleHeader.Alignment = HorizontalAlignment.CENTER;
            styleHeader.VerticalAlignment = VerticalAlignment.CENTER;
            styleHeader.SetFont(fontBold);
            styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            int columnIndex = 0;
            int rowIndex = 1;
            int headerIndex = 0;

            //如果存在标题行需要创建
            if (!string.IsNullOrEmpty(__title))
            {
                ICellStyle styleTitle = wk.CreateCellStyle();
                styleTitle.Alignment = HorizontalAlignment.LEFT;
                styleTitle.VerticalAlignment = VerticalAlignment.CENTER;
                styleTitle.SetFont(fontBold);
                styleTitle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTitle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTitle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTitle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                // 创建头行
                IRow rowTitle = sheet.CreateRow(0);
                ICell cellTitle = rowTitle.CreateCell(0);
                cellTitle.SetCellValue(__title);
                cellTitle.CellStyle = styleTitle;
                SetCellRangeAddress(sheet, 0, 0, 0, list.Count - 1);
                rowIndex = 2;
                headerIndex = 1;

            }


            // 创建头行
            IRow rowHeader = sheet.CreateRow(headerIndex);
            ICell cellHeader;
            if (list != null)
            {
                foreach (var e in list)
                {
                    cellHeader = rowHeader.CreateCell(columnIndex);
                    cellHeader.SetCellValue(e.ColumnName);
                    cellHeader.CellStyle = styleHeader;
                    sheet.SetColumnWidth(columnIndex, e.CharLength * 2 * 256);
                    columnIndex++;
                }
            }
            #endregion

            #region << 样式 >>


            // 通用样式
            ICellStyle styleCell = wk.CreateCellStyle();
            // 居中样式
            ICellStyle styleCenter = wk.CreateCellStyle();
            // 小数样式
            ICellStyle styleDouble = wk.CreateCellStyle();
            // 整数样式
            ICellStyle styleInt = wk.CreateCellStyle();
            // 日期样式
            ICellStyle styleDate = wk.CreateCellStyle();
            #endregion
            var totalUnCollectedAmt = 0.0;
            var totalProfit = 0.0;
            var totalP = 0.0;
            var totalOrderInvoiceAmt = 0.0;
            #region << 数据列表 >>
            foreach (DataRow row in dtSource.Rows)
            {

                // 创建一行
                IRow rowData = sheet.CreateRow(rowIndex);

                columnIndex = 0;
                ICell cell = rowData.CreateCell(columnIndex);
                foreach (var e in list)
                {
                    #region << 设置各个单元格数据 >>

                    string v = string.Empty;
                    if (e.ColumEnm != null && e.ColumEnm.Length > 0)
                    {
                        foreach (var enms in e.ColumEnm)
                        {
                            var ne = enms.Split('|');
                            v = row[e.ColumField].ToString();
                            if (v.Equals(ne[0]))
                            {
                                v = ne[1];
                                break;
                            }
                        }
                    }
                    else
                    {
                        switch (e.ColumField)
                        {
                            case "UnCollectedAmt":
                                var sum = row["OrderAmt"].ToString().ToDouble() - row["CollectedAmt"].ToString().ToDouble() - row["ToConfirmCollectedAmt"].ToString().ToDouble();
                                totalUnCollectedAmt += sum;
                                v = sum.ToString();
                                break;
                            case "Profit":
                                sum = row["OrderAmt"].ToString().ToDouble() - row["OrderCost"].ToString().ToDouble();
                                totalProfit += sum;
                                v = sum.ToString();
                                break;
                            case "P":
                                var oAmt = row["OrderAmt"].ToString().ToDouble();
                                var oCost = row["OrderCost"].ToString().ToDouble();
                                var profit = oAmt - oCost;
                                if (oAmt == 0)
                                {
                                    v = "";
                                }
                                else
                                {
                                    var rate = profit / oAmt * 100;
                                    v = rate.ToString();
                                }
                                break;
                            case "OrderInvoiceAmt":
                                var s = "未开";
                                var invoiceAmt = row["OrderInvoiceAmt"].ToString().ToDouble();
                                var orderAmt = row["orderAmt"].ToString().ToDouble();
                                    if (invoiceAmt == 0) s = "未开";
                                    else {
                                        var amt = orderAmt - invoiceAmt;
                                        if (amt == 0) s = "已开发票";
                                        else {
                                            if (amt > 0) s = "部分开票";
                                            else s = "超额开票";
                                        }
                                    }
                                    v = s;
                                break;
                            default:
                                v = row[e.ColumField].ToString();
                                break;
                        }
                    }

                    SetDataCellAdd(wk, fontCell, fontBold, rowData, cell, columnIndex, v, e.ColumStyle, styleCenter, styleCell, styleDouble, styleInt, styleDate);
                    columnIndex++;
                    #endregion
                }
                rowIndex++;
            }
            #endregion

            #region << 合计 >>
            if (Istotal)
            {
                // 统计行标题样式
                ICellStyle styleTotal = wk.CreateCellStyle();
                styleTotal.Alignment = HorizontalAlignment.CENTER;
                styleTotal.VerticalAlignment = VerticalAlignment.CENTER;
                styleTotal.SetFont(fontBold);
                styleTotal.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotal.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotal.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotal.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

                // 统计行数值样式
                ICellStyle styleTotalDouble = wk.CreateCellStyle();
                styleTotalDouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                styleTotalDouble.Alignment = HorizontalAlignment.RIGHT;
                styleTotalDouble.VerticalAlignment = VerticalAlignment.CENTER;
                styleTotalDouble.SetFont(fontBold);
                styleTotalDouble.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotalDouble.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotalDouble.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotalDouble.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

                // 创建一行
                IRow rowTotal = sheet.CreateRow(rowIndex);
                int cellIndex = 0;
                ICell cellTotal = rowTotal.CreateCell(0);

                var i = 0;
                foreach (var e in list)
                {
                    decimal oAmt = 0;
                    decimal oCost = 0;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        oAmt += row["OrderAmt"].ToString().ToDecimal();
                        oCost += row["OrderCost"].ToString().ToDecimal();
                    }

                    if (i == 0)
                    {
                        SetCellAdd(rowTotal, cellTotal, cellIndex++, "合计", styleTotal);
                    }
                    else if (e.ColumStyle == NExcelCellFormatStyle.Double || e.ColumStyle == NExcelCellFormatStyle.Int)
                    {
                        decimal total = 0;
                        
                        foreach (DataRow row in dtSource.Rows)
                        {
                            switch (e.ColumField)
                            {
                                case "UnCollectedAmt":
                                    total = totalUnCollectedAmt.ToString("0.00").ToDecimal();
                                    break;
                                case "Profit":
                                    total = totalProfit.ToString("0.00").ToDecimal();
                                    break;
                                case "P":
                                    if (oAmt==0)
                                    {
                                        total = 0;
                                    }
                                    else
                                    {
                                        var profit = oAmt - oCost;
                                        var rate = profit / oAmt * 100;
                                        total = rate.ToString("0.00").ToDecimal();
                                    }
                                    break;
                                default:
                                    total += row[e.ColumField].ToString().ToDecimal();
                                    break;
                            }
                        }

                        SetCellAdd(rowTotal, cellTotal, cellIndex++, total.ToString(), styleTotal);
                    }
                    else
                        SetCellAdd(rowTotal, cellTotal, cellIndex++, "", styleTotal);
                    i++;
                }
            }
            #endregion

            #region << 输出 >>
            MemoryStream ms = new MemoryStream();
            try
            {
                wk.Write(ms);

                string fileName = title + ".xls";

                rq.ContentEncoding = Encoding.GetEncoding("gb2312");
                if (rq.UserAgent.ToLower().IndexOf("msie") > -1)
                {
                    fileName = HttpUtility.UrlPathEncode(fileName);
                }
                if (rq.UserAgent.ToLower().IndexOf("firefox") > -1)
                {
                    rp.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else
                {
                    rp.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                }
                rp.ContentType = "application/ms-excel";
                rp.BinaryWrite(ms.ToArray());
                wk = null;
                ms.Close();
                ms.Dispose();
            }
            catch
            {
                if (wk != null)
                {
                    wk = null;
                }
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
            }
            #endregion
        }

        /// <summary>
        /// 数据单元格赋值
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="cell">列</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="CellStyle">列样式</param>
        /// <param name="v">列值</param>
        public static void SetDataCellAdd(HSSFWorkbook wk, IFont fontCell,
            IFont fontBold, IRow row, ICell cell, int cellIndex,
            string v, NExcelCellFormatStyle CellStyle, ICellStyle styleCenter, ICellStyle styleCell,
            ICellStyle styleDouble, ICellStyle styleInt, ICellStyle styleDate)
        {
            #region << 单元格样式 >>



            styleCell.VerticalAlignment = VerticalAlignment.CENTER;
            styleCell.SetFont(fontCell);
            styleCell.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCell.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCell.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCell.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleCenter.Alignment = HorizontalAlignment.CENTER;
            styleCenter.VerticalAlignment = VerticalAlignment.CENTER;
            styleCenter.SetFont(fontCell);
            styleCenter.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCenter.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleDouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
            styleDouble.Alignment = HorizontalAlignment.RIGHT;
            styleDouble.VerticalAlignment = VerticalAlignment.CENTER;
            styleDouble.SetFont(fontCell);
            styleDouble.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDouble.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDouble.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDouble.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleInt.DataFormat = HSSFDataFormat.GetBuiltinFormat("0");
            styleInt.Alignment = HorizontalAlignment.RIGHT;
            styleInt.VerticalAlignment = VerticalAlignment.CENTER;
            styleInt.SetFont(fontCell);
            styleInt.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleInt.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleInt.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleInt.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleDate.DataFormat = wk.CreateDataFormat().GetFormat("yyyy/MM/dd");
            styleDate.Alignment = HorizontalAlignment.CENTER;
            styleDate.VerticalAlignment = VerticalAlignment.CENTER;
            styleDate.SetFont(fontCell);
            styleDate.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDate.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDate.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDate.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            #endregion

            cell = row.CreateCell(cellIndex);
            switch (CellStyle)
            {
                case NExcelCellFormatStyle.Center:
                    cell.SetCellValue(v);
                    cell.CellStyle = styleCenter;
                    break;
                case NExcelCellFormatStyle.Double:
                    cell.CellStyle = styleDouble;
                    cell.SetCellValue(v.ToDouble());
                    break;
                case NExcelCellFormatStyle.Date:
                    cell.CellStyle = styleDate;//日期
                    cell.SetCellValue(v);
                    break;
                case NExcelCellFormatStyle.Int:
                    cell.CellStyle = styleInt;//整数
                    cell.SetCellValue(v.ToInt());
                    break;
                default:
                    cell.CellStyle = styleCell;//通用
                    cell.SetCellValue(v);
                    break;
            }

        }
        /// <summary>
        /// 单元格赋值
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="cell">列</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="CellStyle">列样式</param>
        /// <param name="v">列值</param>
        public static void SetCellAdd(IRow row, ICell cell, int cellIndex, string v, ICellStyle CellStyle)
        {
            cell = row.CreateCell(cellIndex);
            cell.SetCellValue(v);
            cell.CellStyle = CellStyle;
        }


        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }
        #endregion
    }





    public class NewExcelHelper
    {
        #region << 通用方法封装 >>

        /// <summary>
        /// 导出的方法，收入明细表,付款明细报表
        /// </summary>
        /// <param name="__fileName">导出的名称程序自动加上时间结尾</param>
        /// <param name="__title">导出标题如果存在需要显示筛选条件</param>
        /// <param name="dtSource">数据</param>
        /// <param name="rp">页面调用输入this.Response</param>
        /// <param name="rq">页面调用输入this.Request</param>
        /// <param name="list">表头</param>
        /// <param name="Istotal">是否输出合计，输出合计是根据表头是否为double和int</param>
        public void RptData_NExcel(string __fileName, string __title, DataTable dtSource, HttpResponse rp, HttpRequest rq, List<NExcelCellFormat> list = null, bool Istotal = false)
        {
            #region << Excel工作文档 >>
            HSSFWorkbook wk = new HSSFWorkbook();
            var title = __fileName + DateTime.Now.ToString("yyyyMMddss");
            ISheet sheet = wk.CreateSheet(title);

            // 通用字体
            IFont fontCell = wk.CreateFont();
            fontCell.FontName = "宋体";

            // 加粗字体
            IFont fontBold = wk.CreateFont();
            fontBold.FontName = "宋体";
            fontBold.Boldweight = (short)FontBoldWeight.BOLD;
            #endregion

            #region << 设置表格头 >>
            // 表格头样式
            ICellStyle styleHeader = wk.CreateCellStyle();
            styleHeader.Alignment = HorizontalAlignment.CENTER;
            styleHeader.VerticalAlignment = VerticalAlignment.CENTER;
            styleHeader.SetFont(fontBold);
            styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            int columnIndex = 0;
            int rowIndex = 1;
            int headerIndex = 0;

            //如果存在标题行需要创建
            if (!string.IsNullOrEmpty(__title))
            {
                ICellStyle styleTitle = wk.CreateCellStyle();
                styleTitle.Alignment = HorizontalAlignment.LEFT;
                styleTitle.VerticalAlignment = VerticalAlignment.CENTER;
                styleTitle.SetFont(fontBold);
                styleTitle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTitle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTitle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTitle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                // 创建头行
                IRow rowTitle = sheet.CreateRow(0);
                ICell cellTitle = rowTitle.CreateCell(0);
                cellTitle.SetCellValue(__title);
                cellTitle.CellStyle = styleTitle;
                SetCellRangeAddress(sheet, 0, 0, 0, list.Count - 1);
                rowIndex = 2;
                headerIndex = 1;

            }


            // 创建头行
            IRow rowHeader = sheet.CreateRow(headerIndex);
            ICell cellHeader;
            if (list != null)
            {
                foreach (var e in list)
                {
                    cellHeader = rowHeader.CreateCell(columnIndex);
                    cellHeader.SetCellValue(e.ColumnName);
                    cellHeader.CellStyle = styleHeader;
                    sheet.SetColumnWidth(columnIndex, e.CharLength * 2 * 256);
                    columnIndex++;
                }
            }
            #endregion

            #region << 样式 >>


            // 通用样式
            ICellStyle styleCell = wk.CreateCellStyle();
            // 居中样式
            ICellStyle styleCenter = wk.CreateCellStyle();
            // 小数样式
            ICellStyle styleDouble = wk.CreateCellStyle();
            // 整数样式
            ICellStyle styleInt = wk.CreateCellStyle();
            // 日期样式
            ICellStyle styleDate = wk.CreateCellStyle();
            #endregion
            var totalUnCollectedAmt = 0.0;
            var totalProfit = 0.0;
            var totalP = 0.0;
            var totalOrderInvoiceAmt = 0.0;
            #region << 数据列表 >>
            foreach (DataRow row in dtSource.Rows)
            {

                // 创建一行
                IRow rowData = sheet.CreateRow(rowIndex);

                columnIndex = 0;
                ICell cell = rowData.CreateCell(columnIndex);
                foreach (var e in list)
                {
                    #region << 设置各个单元格数据 >>

                    string v = string.Empty;
                    if (e.ColumEnm != null && e.ColumEnm.Length > 0)
                    {
                        foreach (var enms in e.ColumEnm)
                        {
                            var ne = enms.Split('|');
                            v = row[e.ColumField].ToString();
                            if (v.Equals(ne[0]))
                            {
                                v = ne[1];
                                break;
                            }
                        }
                    }
                    else
                    {
                        switch (e.ColumField)
                        {
                            case "UnCollectedAmt":
                                var sum = row["OrderAmt"].ToString().ToDouble() - row["CollectedAmt"].ToString().ToDouble() - row["ToConfirmCollectedAmt"].ToString().ToDouble();
                                totalUnCollectedAmt += sum;
                                v = sum.ToString();
                                break;
                            case "Profit":
                                sum = row["OrderAmt"].ToString().ToDouble() - row["OrderCost"].ToString().ToDouble();
                                totalProfit += sum;
                                v = sum.ToString();
                                break;
                            case "P":
                                var oAmt = row["OrderAmt"].ToString().ToDouble();
                                var oCost = row["OrderCost"].ToString().ToDouble();
                                var profit = oAmt - oCost;
                                if (oAmt == 0)
                                {
                                    v = "";
                                }
                                else
                                {
                                    var rate = profit / oAmt * 100;
                                    v = rate.ToString();
                                }
                                break;
                            case "OrderInvoiceAmt":
                                var s = "未开";
                                var invoiceAmt = row["OrderInvoiceAmt"].ToString().ToDouble();
                                var orderAmt = row["orderAmt"].ToString().ToDouble();
                                if (invoiceAmt == 0) s = "未开";
                                else
                                {
                                    var amt = orderAmt - invoiceAmt;
                                    if (amt == 0) s = "已开发票";
                                    else
                                    {
                                        if (amt > 0) s = "部分开票";
                                        else s = "超额开票";
                                    }
                                }
                                v = s;
                                break;
                            default:
                                v = row[e.ColumField].ToString();
                                break;
                        }
                    }

                    SetDataCellAdd(wk, fontCell, fontBold, rowData, cell, columnIndex, v, e.ColumStyle, styleCenter, styleCell, styleDouble, styleInt, styleDate);
                    columnIndex++;
                    #endregion
                }
                rowIndex++;
            }
            #endregion

            #region << 合计 >>
            if (Istotal)
            {
                // 统计行标题样式
                ICellStyle styleTotal = wk.CreateCellStyle();
                styleTotal.Alignment = HorizontalAlignment.CENTER;
                styleTotal.VerticalAlignment = VerticalAlignment.CENTER;
                styleTotal.SetFont(fontBold);
                styleTotal.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotal.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotal.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotal.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

                // 统计行数值样式
                ICellStyle styleTotalDouble = wk.CreateCellStyle();
                styleTotalDouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                styleTotalDouble.Alignment = HorizontalAlignment.RIGHT;
                styleTotalDouble.VerticalAlignment = VerticalAlignment.CENTER;
                styleTotalDouble.SetFont(fontBold);
                styleTotalDouble.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotalDouble.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotalDouble.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                styleTotalDouble.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

                // 创建一行
                IRow rowTotal = sheet.CreateRow(rowIndex);
                ICell cellTotal = rowTotal.CreateCell(0);


            }
            #endregion

            #region << 输出 >>
            MemoryStream ms = new MemoryStream();
            try
            {
                wk.Write(ms);

                string fileName = title + ".xls";

                rq.ContentEncoding = Encoding.GetEncoding("gb2312");
                if (rq.UserAgent.ToLower().IndexOf("msie") > -1)
                {
                    fileName = HttpUtility.UrlPathEncode(fileName);
                }
                if (rq.UserAgent.ToLower().IndexOf("firefox") > -1)
                {
                    rp.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else
                {
                    rp.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                }
                rp.ContentType = "application/ms-excel";
                rp.BinaryWrite(ms.ToArray());
                wk = null;
                ms.Close();
                ms.Dispose();
            }
            catch
            {
                if (wk != null)
                {
                    wk = null;
                }
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
            }
            #endregion
        }

        /// <summary>
        /// 数据单元格赋值
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="cell">列</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="CellStyle">列样式</param>
        /// <param name="v">列值</param>
        public static void SetDataCellAdd(HSSFWorkbook wk, IFont fontCell,
            IFont fontBold, IRow row, ICell cell, int cellIndex,
            string v, NExcelCellFormatStyle CellStyle, ICellStyle styleCenter, ICellStyle styleCell,
            ICellStyle styleDouble, ICellStyle styleInt, ICellStyle styleDate)
        {
            #region << 单元格样式 >>



            styleCell.VerticalAlignment = VerticalAlignment.CENTER;
            styleCell.SetFont(fontCell);
            styleCell.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCell.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCell.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCell.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleCenter.Alignment = HorizontalAlignment.CENTER;
            styleCenter.VerticalAlignment = VerticalAlignment.CENTER;
            styleCenter.SetFont(fontCell);
            styleCenter.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleCenter.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleDouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
            styleDouble.Alignment = HorizontalAlignment.RIGHT;
            styleDouble.VerticalAlignment = VerticalAlignment.CENTER;
            styleDouble.SetFont(fontCell);
            styleDouble.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDouble.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDouble.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDouble.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleInt.DataFormat = HSSFDataFormat.GetBuiltinFormat("0");
            styleInt.Alignment = HorizontalAlignment.RIGHT;
            styleInt.VerticalAlignment = VerticalAlignment.CENTER;
            styleInt.SetFont(fontCell);
            styleInt.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleInt.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleInt.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleInt.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            styleDate.DataFormat = wk.CreateDataFormat().GetFormat("yyyy/MM/dd");
            styleDate.Alignment = HorizontalAlignment.CENTER;
            styleDate.VerticalAlignment = VerticalAlignment.CENTER;
            styleDate.SetFont(fontCell);
            styleDate.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDate.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDate.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            styleDate.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;

            #endregion

            cell = row.CreateCell(cellIndex);
            switch (CellStyle)
            {
                case NExcelCellFormatStyle.Center:
                    cell.SetCellValue(v);
                    cell.CellStyle = styleCenter;
                    break;
                case NExcelCellFormatStyle.Double:
                    cell.CellStyle = styleDouble;
                    cell.SetCellValue(v.ToDouble());
                    break;
                case NExcelCellFormatStyle.Date:
                    cell.CellStyle = styleDate;//日期
                    cell.SetCellValue(v);
                    break;
                case NExcelCellFormatStyle.Int:
                    cell.CellStyle = styleInt;//整数
                    cell.SetCellValue(v.ToInt());
                    break;
                default:
                    cell.CellStyle = styleCell;//通用
                    cell.SetCellValue(v);
                    break;
            }

        }
        /// <summary>
        /// 单元格赋值
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="cell">列</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="CellStyle">列样式</param>
        /// <param name="v">列值</param>
        public static void SetCellAdd(IRow row, ICell cell, int cellIndex, string v, ICellStyle CellStyle)
        {
            cell = row.CreateCell(cellIndex);
            cell.SetCellValue(v);
            cell.CellStyle = CellStyle;
        }


        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }
        #endregion
    }
}
