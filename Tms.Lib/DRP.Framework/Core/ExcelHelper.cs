using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.in2bits.MyXls;
using System.Web;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace DRP.Framework.Core
{
    /// <summary>
    /// 导出Excel单元格属性
    /// </summary>
    [Serializable]
    public class ExcelCellFormat
    {
        /// <summary>
        /// 字符宽度
        /// </summary>
        public int CharLength { get; set; }

        /// <summary>
        /// 字段Filed
        /// </summary>
        public string ColumField { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType { get; set; }

        public ExcelCellFormat() { }

        public ExcelCellFormat(ushort length, string colField, string colName,DbType dbType=DbType.String)
        {
            CharLength = length;
            ColumField = colField;
            ColumnName = colName;
            DbType = dbType;
        }
    }

    /// <summary>
    /// Excel导出导入
    /// 引用类库<see cref="org.in2bits.MyXls.dll"/>
    /// </summary>
    public class ExcelHelper
    {
        #region 导出
        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dtSource">导出文件的数据源</param>
        /// <param name="dict">列名-别名</param>
        /// <returns></returns>
        /// <remarks>当Dict为null时，导出的excel抬头即为列名</remarks>
        public void ExportToExcel(string fileName, DataTable dtSource, List<ExcelCellFormat> list = null)
        {
            XlsDocument doc = new XlsDocument();
            doc.FileName = HttpUtility.UrlEncode(fileName) + ".xls";
            Worksheet sheet = doc.Workbook.Worksheets.Add(fileName);
            Cells cells = sheet.Cells;
            int rowIndex = 1;
            int cellIndex = 1;

            #region 标题
            XF xfDataHead = doc.NewXF();
            xfDataHead.HorizontalAlignment = HorizontalAlignments.Centered;
            xfDataHead.VerticalAlignment = VerticalAlignments.Centered;
            xfDataHead.Font.FontName = "宋体";
            xfDataHead.Font.Bold = true;
            xfDataHead.UseBorder = true;
            xfDataHead.BottomLineStyle = 1;
            xfDataHead.BottomLineColor = Colors.Black;
            xfDataHead.TopLineStyle = 1;
            xfDataHead.TopLineColor = Colors.Black;
            xfDataHead.LeftLineStyle = 1;
            xfDataHead.LeftLineColor = Colors.Black;
            xfDataHead.RightLineStyle = 1;
            xfDataHead.RightLineColor = Colors.Black;
            if (list != null)
            {
                ushort idx = 0;
                foreach (var e in list)
                {
                    //单元格宽度调整
                    ColumnInfo col = new ColumnInfo(doc, sheet);
                    col.Width = (ushort)(e.CharLength * 256);
                    col.ColumnIndexStart = idx;
                    col.ColumnIndexEnd = idx;
                    sheet.AddColumnInfo(col);
                    idx++;
                    //标题
                    cells.Add(1, cellIndex++, e.ColumnName, xfDataHead);
                }
            }
            else
            {
                DataColumnCollection collection = dtSource.Columns;
                foreach (DataColumn col in dtSource.Columns)
                {
                    cells.Add(1, cellIndex++, col.ColumnName, xfDataHead);
                }
            }
            #endregion

            #region 数据列表
            XF xfData = doc.NewXF();
            xfData.Font.FontName = "宋体";
            xfData.UseBorder = true;
            xfData.BottomLineStyle = 1;
            xfData.BottomLineColor = Colors.Black;
            xfData.TopLineStyle = 1;
            xfData.TopLineColor = Colors.Black;
            xfData.LeftLineStyle = 1;
            xfData.LeftLineColor = Colors.Black;
            xfData.RightLineStyle = 1;
            xfData.RightLineColor = Colors.Black;
            foreach (DataRow row in dtSource.Rows)
            {
                rowIndex++;
                cellIndex = 1;
                if (list != null)
                {
                    foreach (var e in list)
                    {
                        cells.Add(rowIndex, cellIndex++, row[e.ColumField].ToString(), xfData);
                    }
                }
                else
                {
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        cells.Add(rowIndex, cellIndex++, row[col].ToString(), xfData);
                    }
                }
            }
            #endregion

            doc.Send();
        }



        /// <summary>
        /// 导出Excel文件(存在头和尾)
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dtSource">导出文件的数据源</param>
        /// <param name="dict">列名-别名</param>
        /// <returns></returns>
        /// <remarks>当Dict为null时，导出的excel抬头即为列名</remarks>
        public void ExportToExcelMade(string fileName, DataTable dtSource, string header, string footer, Dictionary<string, string> dict = null)
        {
            XlsDocument doc = new XlsDocument();
            doc.FileName = HttpUtility.UrlEncode(fileName) + ".xls";
            Worksheet sheet = doc.Workbook.Worksheets.Add(fileName);
            Cells cells = sheet.Cells;
            int rowIndex = 2;
            int cellIndex = 1;

            #region 标题

            if (!string.IsNullOrEmpty(header))
            {
                cells.Add(1, 3, header);
            }
            else
            {
                cells.Add(1, 3, fileName);
            }

            if (dict != null)
            {
                foreach (KeyValuePair<string, string> key in dict)
                {
                    cells.Add(2, cellIndex++, key.Value);
                }
            }
            else
            {
                DataColumnCollection collection = dtSource.Columns;
                foreach (DataColumn col in dtSource.Columns)
                {
                    cells.Add(2, cellIndex++, col.ColumnName);
                }
            }
            #endregion

            #region 数据列表
            foreach (DataRow row in dtSource.Rows)
            {
                rowIndex++;
                cellIndex = 1;
                if (dict != null)
                {
                    foreach (KeyValuePair<string, string> key in dict)
                    {
                        cells.Add(rowIndex, cellIndex++, row[key.Key].ToString());
                    }
                }
                else
                {
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        cells.Add(rowIndex, cellIndex++, row[col].ToString());
                    }
                }
            }
            #endregion


            if (!string.IsNullOrEmpty(footer))
            {
                cells.Add(rowIndex + 3, 3, footer);
            }

            doc.Send();
        }

        #endregion

        #region 导入

        private string ExcelConnectionString(string fileName)
        {
            // return "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + fileName;
            return "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + fileName + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
        }


        /// <summary>
        /// 获取Excel第一个工作表名
        /// </summary>
        /// <param name="excelFileName">物理绝对路径</param>
        /// <returns></returns>
        public string GetExcelSheetName(string excelFileName)
        {
            string tableName = null;
            if (File.Exists(excelFileName))
            {
                using (OleDbConnection conn = new OleDbConnection(ExcelConnectionString(excelFileName)))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    tableName = dt.Rows[0][2].ToString().Trim();
                    conn.Close();
                }
            }
            return tableName;
        }


        /// <summary>
        /// 获取Excel工作表名
        /// </summary>
        /// <param name="excelFileName">物理绝对路径</param>
        /// <returns></returns>
        public List<string> GetExcelSheetNameArray(string excelFileName)
        {
            var list = new List<string>();
            if (File.Exists(excelFileName))
            {
                using (OleDbConnection conn = new OleDbConnection(ExcelConnectionString(excelFileName)))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(row[2].ToString().Trim());
                    }
                    conn.Close();
                }
            }
            return list;
        }


        /// <summary>
        /// 将导入的Excel文件转化为DataTable
        /// </summary>
        /// <param name="excelFileName"></param>
        /// <returns></returns>
        public DataTable GetExcelDataAsTable(string excelFileName)
        {
            using (OleDbConnection conn = new OleDbConnection(ExcelConnectionString(excelFileName)))
            {
                string sql = string.Format("select * from [{0}]", GetExcelSheetName(excelFileName));
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ExcelTable");
                conn.Close();
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 将导入的Excel文件转化为DataTable
        /// </summary>
        /// <param name="excelFileName"></param>
        /// <returns></returns>
        public DataTable GetExcelDataAsTable(string fileName, string tblName)
        {
            using (OleDbConnection conn = new OleDbConnection(ExcelConnectionString(fileName)))
            {
                string sql = string.Format("select * from [{0}]", tblName);
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ExcelTable");
                conn.Close();
                return ds.Tables[0];
            }
        }
        #endregion
    }
}
