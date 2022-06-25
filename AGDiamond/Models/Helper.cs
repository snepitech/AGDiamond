using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTable = System.Data.DataTable;

namespace AGDiamond
{
    public class Helper
    {
        public static DataTable GetDataTableFromExcel(string path, int startRow = 1, bool addRowNumber = false, bool hasHeader = true, bool includeBlank = false)
        {
            try
            {
                DataTable tbl = new DataTable();
                if (Path.GetExtension(Path.GetFileName(path)).ToLower() == ".csv")
                {
                    FileInfo fileInfo = new FileInfo(path);

                    string connectionString = "Provider=Microsoft.Jet.OleDb.4.0;" +
                              "Data Source=" + fileInfo.DirectoryName + ";" +
                              "Extended Properties='text;HDR=YES;'";

                    using (OleDbConnection connection =
                        new OleDbConnection(connectionString))
                    {
                        // Open the connection 
                        connection.Open();

                        // Set up the adapter and query the table.
                        string sqlStatement = "SELECT * FROM [" + fileInfo.Name + "]";
                        using (OleDbDataAdapter adapter =
                            new OleDbDataAdapter(sqlStatement, connection))
                        {
                            tbl = new DataTable();
                            adapter.Fill(tbl);
                        }
                    }

                    //Add row_num for unique records
                    if (addRowNumber == true)
                    {
                        tbl.Columns.Add("ROWNUM", typeof(int));
                    }
                    tbl.Columns.Add("ROWNUMBER", typeof(int));
                    int row_num = 0;

                    for (int rowNum = 0; rowNum < tbl.Rows.Count; rowNum++)
                    {
                        tbl.Rows[rowNum]["ROWNUM"] = ++row_num;
                    }
                    for (int rowNum = 0; rowNum < tbl.Rows.Count; rowNum++)
                    {
                        tbl.Rows[rowNum]["ROWNUMBER"] = ++row_num;
                    }
                }
                else if (Path.GetExtension(Path.GetFileName(path)).ToLower() == ".xlsx")
                {
                    using (var pck = new OfficeOpenXml.ExcelPackage())
                    {
                        using (var stream = File.OpenRead(path))
                        {
                            pck.Load(stream);
                        }
                        var ws = pck.Workbook.Worksheets.First();
                        if (ws.Cells.Count() == 0)
                            return tbl;
                        //includeBlank : True - Include Blank Row and Column Or Not
                        if (includeBlank)
                        {
                            for (int i = 1; i <= ws.Dimension.End.Column; i++)
                            {
                                tbl.Columns.Add(hasHeader && ws.Cells[1, i] != null ? ws.Cells[1, i].Text : string.Format("Column {0}", ws.Cells[1, i].Start.Column));
                            }
                        }
                        else
                        {
                            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            {
                                tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                            }
                        }

                        int row_num = 0;
                        if (addRowNumber == true)
                        {
                            tbl.Columns.Add("ROWNUM", typeof(int));
                        }
                        tbl.Columns.Add("ROWNUMBER", typeof(string));

                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            DataRow row = tbl.Rows.Add();

                            int countBlank = 0;
                            if (addRowNumber == true)
                            {
                                row["ROWNUM"] = rowNum - (hasHeader ? 1 : 0);
                            }

                            //includeBlank : True - Include Blank Row and Column Or Not
                            if (includeBlank)
                            {
                                for (int colNum = 1; colNum <= ws.Dimension.End.Column; colNum++)
                                {
                                    if (!string.IsNullOrEmpty(ws.Cells[rowNum, colNum].Text.Trim()))
                                    {
                                        row[ws.Cells[rowNum, colNum].Start.Column - 1] = ws.Cells[rowNum, colNum].Text;
                                        countBlank += 1;
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                                    foreach (var cell in wsRow)
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(cell.Text.Trim()))
                                            {
                                                row[cell.Start.Column - 1] = cell.Text;
                                                countBlank += 1;
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            if (countBlank == 0 && !includeBlank)
                            {
                                tbl.Rows.Remove(row);
                            }
                        }

                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            tbl.Rows[i]["ROWNUMBER"] = i + 2;
                        }
                    }
                }
                else if (Path.GetExtension(Path.GetFileName(path)).ToLower() == ".xls")
                {
                    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                    FileInfo fileInfo = new FileInfo(path);
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        DataTable dtExcel = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                        string sqlStatement = "SELECT * FROM [" + dtExcel.Rows[0]["table_name"].ToString() + "]";
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlStatement, conn))
                        {
                            DataSet ds = new DataSet();
                            dataAdapter.Fill(ds);
                            tbl = ds.Tables[0];
                        }
                        conn.Close();
                    }

                    //Add row_num for unique records
                    if (addRowNumber == true)
                    {
                        tbl.Columns.Add("ROWNUM", typeof(int));
                        int row_num = 0;

                        for (int rowNum = 0; rowNum < tbl.Rows.Count; rowNum++)
                        {
                            tbl.Rows[rowNum]["ROWNUM"] = ++row_num;
                        }
                    }

                }
                return tbl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool deleteImages(string path)
        {
            try
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~" + path)) && File.Exists(HttpContext.Current.Server.MapPath("~" + path.Replace("Thumb", "Large"))))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~" + path));
                    File.Delete(HttpContext.Current.Server.MapPath("~" + path.Replace("Thumb", "Large")));
                    return true;
                }
                else
                    return false;
            }
            catch (Exception) { return false; }
        }

        public static int LineNumber(Exception ex)
        {
            int linenum = 0;

            try
            {
                linenum = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(":line") + 5));
            }
            catch (Exception)
            {
                // ignored
            }

            return linenum;
        }

        public static void DeleteLastinFolder(string folderPath = "", int count = 250)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                folderPath = BAL.Constant.ExcelPath;
            }

            foreach (var fi in new DirectoryInfo(folderPath).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(count))
            {
                fi.Delete();
            }

        }

        public static JsonResult JsonMax(object data)
        {
            return new JsonResult
            {
                Data = data,
                MaxJsonLength = 214748364
            };
        }

    }
}