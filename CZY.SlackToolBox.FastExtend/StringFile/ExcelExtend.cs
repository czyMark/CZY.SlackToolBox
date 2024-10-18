using OfficeOpenXml; 
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend.StringFile
{ 
    public static class ExcelExtend
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tables"></param>
        public static void ExportExcel(this string filePath, List<DataTable> tables)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                if (tables == null || tables.Count == 0)
                {
                    package.Workbook.Worksheets.Add("Sheet1");
                }
                foreach (var table in tables)
                {
                    CreateSheet(package, table);
                }

                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    package.SaveAs(stream);
                }
            }
        }

        /// <summary>
        /// 根据DataTable创建Sheet
        /// </summary>
        /// <param name="package"></param>
        /// <param name="table"></param>
        /// <returns></returns>

        private static ExcelWorksheet CreateSheet(ExcelPackage package, DataTable table)
        {
            string sheetName = string.IsNullOrEmpty(table.TableName) ? "Sheet1" : table.TableName;
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add(sheetName);

            int columnCount = table.Columns.Count;
            //添加标题
            for (int i = 0; i < columnCount; i++)
            {
                sheet.Cells[1, i + 1].Value = table.Columns[i].Caption;
            }
            for (int row = 0; row < table.Rows.Count; row++)
            {
                for (int col = 0; col < columnCount; col++)
                {
                    sheet.Cells[row + 2, col + 1].Value = table.Rows[row][col];
                }
            }

            return sheet;
        }


        /// <summary>
        /// 将Excel中的内容导入到DataTable
        /// </summary>
        /// <param name="filePath">要导入的文件</param>
        /// <returns>表格的集合</returns>
        public static List<DataTable> ImportExcel(this string filePath, string sheetName = "")
        {
            List<DataTable> tables = new List<DataTable>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    for (int i = 1; i <= package.Workbook.Worksheets.Count; ++i)
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[i];
                        if (string.IsNullOrWhiteSpace(sheetName))
                        {
                            DataTable table = CreateTable(sheet);
                            if (table != null)
                                tables.Add(table);
                        }
                        else
                        {
                            if (sheet.Name == sheetName)
                            {
                                DataTable table = CreateTable(sheet);
                                if (table != null)
                                    tables.Add(table);
                                break;
                            }
                        }
                    }

                    fs.Close();
                    //fs = null;
                    package.Dispose();
                }
            }

            return tables;
        }

        /// <summary>
        /// 根据Sheet创建DataTable
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private static DataTable CreateTable(ExcelWorksheet sheet)
        {
            DataTable table = new DataTable();
            SetDataTableColumns(table, sheet);
            if (table.Columns == null || table.Columns.Count == 0)
                return null;
            string sheetName = sheet.Name;
            table.TableName = sheetName;

            for (int m = sheet.Dimension.Start.Row + 1, n = sheet.Dimension.End.Row; m <= n; m++)
            {
                DataRow row = table.NewRow();
                for (int j = sheet.Dimension.Start.Column, k = sheet.Dimension.End.Column; j <= k; j++)
                {
                    string columnName = $"{sheetName}_{j}";
                    row[columnName] = sheet.Cells[m, j].Value;
                }
                if (IsRowValid(row))
                {
                    table.Rows.Add(row);
                }

            }

            return table;

        }

        /// <summary>
        /// 判断是否全为空的行
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static bool IsRowValid(DataRow row)
        {
            int colunmCount = row.Table.Columns.Count;
            for (int i = 0; i < colunmCount; i++)
            {
                if (row[i] != null && !string.IsNullOrWhiteSpace(row[i].ToString()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 设置表格的列
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sheet"></param>
        private static void SetDataTableColumns(DataTable table, ExcelWorksheet sheet)
        {
            string sheetName = sheet.Name;
            if (sheet.Dimension == null)
                return;
            int endColumn = sheet.Dimension.End.Column;
            int statrtRow = sheet.Dimension.Start.Row;
            for (int i = sheet.Dimension.Start.Column; i <= endColumn; i++)
            {
                object val = sheet.Cells[statrtRow, i].Value;
                if (val != null)
                {
                    var col = table.Columns.Add($"{sheetName}_{i}");
                    col.Caption = val.ToString();
                }
            }
        }
    }
}
