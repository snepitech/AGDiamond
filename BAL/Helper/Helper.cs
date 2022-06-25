//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using SqlOps = ConfigLib.SqlServer.OperationSql;
using Val = ConfigLib.Validation.BOValidation;

namespace BAL
{
    public static class Helper
    {
        private static DataSet _DS = new DataSet();
        public static DataTable ObjectToData(object o)
        {
            DataTable dt = new DataTable("OutputData");
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            o.GetType().GetProperties().ToList().ForEach(f =>
            {
                try
                {
                    dt.Columns.Add(f.Name, f.PropertyType);
                }
                catch { }
            });
            return dt;
        }
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static List<T> ConvertDataTableList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo prop in temp.GetProperties())
                {
                    if (prop.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)

                        prop.SetValue(obj, ChangeType(dr[column.ColumnName], prop.PropertyType), null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            if (t.Name == "Guid")
                return Guid.Parse(Convert.ToString(value));
            else
                return Convert.ChangeType(value, t);
        }

        /// <summary>
        /// Converts a List to DataTable
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="data">List</param>
        /// <returns>DataTable</returns>
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }


        //public static dynamic DataTableToList(DataTable dt, int? login_type = null, TableToListParameter t = null)
        //{
        //    if (t == null)
        //    {
        //        t = new TableToListParameter();
        //    }
        //  //  var serializer = new JavaScriptSerializer();
        //   // serializer.MaxJsonLength = Int32.MaxValue;
        //    var result = ConvertDataTabletoList(dt, login_type, t).ToArray();
        //    return result;
        //}


        public static DataTable ConvertToDataTable<T>(List<dynamic> items)
        {

            DataTable dtDataTable = new DataTable();
            if (items.Count == 0) return dtDataTable;

            for (int i = 0; i < items.Count(); i++)
            {
                ((IEnumerable)items[i]).Cast<dynamic>().Select(p => p.Key).ToList().ForEach(col => { if (!dtDataTable.Columns.Contains(col)) { dtDataTable.Columns.Add(col); } });
            }

            ((IEnumerable)items).Cast<dynamic>().ToList().
                ForEach(data =>
                {
                    DataRow dr = dtDataTable.NewRow();
                    ((IEnumerable)data).Cast<dynamic>().ToList().ForEach(Col => { dr[Col.Key] = Col.Value; });
                    dtDataTable.Rows.Add(dr);
                });

            return dtDataTable;

        }


        public static DataTable ConvertToDataTable(List<object> items)
        {
            DataTable dt = new DataTable();
            int i = 0;
            foreach (var item in items)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                PropertyInfo[] props = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in props)
                {
                    if (!dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName.ToLower()).Contains(prop.Name.ToLower()))
                    {
                        var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                        dt.Columns.Add(prop.Name, type);
                    }
                    if (prop.GetValue(item, null) != null)
                    {
                        dt.Rows[i][prop.Name] = prop.GetValue(item, null);
                    }
                }
                i++; ;
            }
            return dt;
        }

        public static List<Dictionary<string, string>> NListData(Dictionary<string, List<string>> list, List<Dictionary<string, string>> AllCombination, Dictionary<string, string> innercombination, int keyIndex)
        {
            // Reset All Data when Comes to first Index after all inner
            if (keyIndex == 0)
            {
                innercombination.Keys.ToList().ForEach(x => innercombination[x] = string.Empty);
            }

            var currentKey = list.Keys.ElementAt(keyIndex);
            var currentValues = list[currentKey];
            for (int i = 0; i < currentValues.Count; i++)
            {
                // Object of Dictionary to Add in List 
                Dictionary<string, string> addDetail = new Dictionary<string, string>();
                foreach (var item in innercombination)
                {
                    addDetail.Add(item.Key, item.Value);
                }

                // Set Value on given Key of Dictionary
                addDetail[currentKey] = currentValues[i];

                // Add If last Key has Value as it is Recursive Method
                if (addDetail.ElementAt(addDetail.Keys.Count - 1).Value != string.Empty)
                {
                    AllCombination.Add(addDetail);
                }

                // Call same Method but with next Key Index 
                List<Dictionary<string, string>> subList = new List<Dictionary<string, string>>();
                if (list.Count > keyIndex + 1)
                {
                    subList = NListData(list, AllCombination, addDetail, keyIndex + 1);
                }

            }
            return AllCombination.Distinct().ToList();
        }

        public class TableToListParameter
        {
            public bool convertErrorColumn { get; set; } = false;
            public bool removeIDs { get; set; } = false;
            public string columnString { get; set; }

        }

        public class PropertyCopier<TParent, TChild> where TParent : class
                                            where TChild : class
        {
            public static void Copy(TParent parent, TChild child)
            {
                var parentProperties = parent.GetType().GetProperties();
                var childProperties = child.GetType().GetProperties();

                foreach (var parentProperty in parentProperties)
                {
                    foreach (var childProperty in childProperties)
                    {
                        if (parentProperty.Name != "id" && parentProperty.Name != "auto_id")
                        {
                            if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                childProperty.SetValue(child, parentProperty.GetValue(parent));
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name] is DateTime)
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }
        private static string ConvertToDateString(object date)
        {
            if (date == null)
                return string.Empty;

            return Val.DispDate(Convert.ToDateTime(date));
        }

        private static string ConvertToString(object value)
        {
            return Convert.ToString(Convert.ToString(value));
        }

        private static int ConvertToInt(object value)
        {
            return Convert.ToInt32(value ?? 0);
        }

        private static long ConvertToLong(object value)
        {
            return Convert.ToInt64(value ?? 0);
        }

        private static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(value ?? 0);
        }

        private static DateTime convertToDateTime(object date)
        {
            return Convert.ToDateTime(date);
        }
        public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            foreach (var columnName in columnNames)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
            }
        }

        public static int getMaxSrNO(string tablename, string columnname, string extraCondition = "")
        {
            int srno = 0;
            SqlOps.DataSetClear(_DS);
            SqlOps.Clear();
            SqlOps.AddParams("table_name", tablename);
            SqlOps.AddParams("column_name", columnname);
            if (extraCondition != "")
                SqlOps.AddParams("extra_condition", extraCondition);
            SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, tablename, Functions.fnMaxsrno, SqlOps.GetParams(), "");

            if (Val.IsEmptyDataSet(_DS) && Val.DataTableIsEmpty(_DS.Tables[tablename]))
            {
                srno = Convert.ToInt32(Convert.ToString(_DS.Tables[tablename].Rows[0][0]));
            }
            return srno;
        }
        public static string getMaxReturnNo(string tablename, string columnname, string key, string WhereCondition = "")
        {
            string return_no = null;
            SqlOps.DataSetClear(_DS);
            SqlOps.Clear();
            SqlOps.AddParams("table_name", tablename);
            SqlOps.AddParams("column_name", columnname);
            if (key != "")
                SqlOps.AddParams("key", key);
            SqlOps.AddParams("WhereCondition", WhereCondition);
            SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, tablename, Functions.fnMaxReturnno, SqlOps.GetParams(), "");

            if (Val.IsEmptyDataSet(_DS) && Val.DataTableIsEmpty(_DS.Tables[tablename]))
            {
                return_no = Convert.ToString(_DS.Tables[tablename].Rows[0][0]);
            }
            return return_no;
        }
        public static bool deleteImages(string path)
        {
            try
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~" + path)) && File.Exists(HttpContext.Current.Server.MapPath("~" + path.Replace("Thumb", "Large"))))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~" + path));
                    File.Delete(HttpContext.Current.Server.MapPath("~" + path.Replace("Large", "Thumb")));
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex) { return false; }
        }

        public static dynamic HasProperty(dynamic obj, string name)
        {
            Type objType = obj.GetType();
            JToken j = ((JObject)obj).GetValue(name);
            if (objType == typeof(JObject))
            {
                if (j != null)
                    return j;
            }
            if (objType.GetProperty(name) != null)
                return objType.GetProperty(name).GetValue(obj);
            else
                return string.Empty;
        }

        public static List<dynamic> ConvertDataTabletoList(System.Data.DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            rows = dt.AsEnumerable()
                 .Select(
                       r => r.Table.Columns.Cast<DataColumn>()
                       .Select(c => new KeyValuePair<string, object>
                           (
                           c.ColumnName,
                               r[c.Ordinal]
                               )
                      ).ToDictionary(z => z.Key, z => z.Value)
                       ).ToList();

            serializer.MaxJsonLength = Int32.MaxValue;
            return rows.ToList<dynamic>();
        }

        public static void CreateDirectoryIfNotExist(string Path)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        public static void DeleteLastinFolder(string folderPath = "", int count = 250)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                folderPath = Constant.ExcelPath;
            }
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (dir.GetFiles().Length > count)
            {
                foreach (var fi in new DirectoryInfo(folderPath).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(count))
                {
                    fi.Delete();
                }
            }

        }

        public static bool ValidateLotCarat(int lot_ref_id, decimal carat, Guid? customer_id = null)
        {
            bool return_no = false;
            SqlOps.DataSetClear(_DS);
            SqlOps.Clear();
            SqlOps.AddParams("lot_ref_id", lot_ref_id);
            SqlOps.AddParams("carat", carat);
            SqlOps.AddParams("customer_id", customer_id == Guid.Empty ? null : customer_id);
            string message = SqlOps.ExFunction(SqlOps.EnumServer.NewWorkSoft, Functions.fnValidateLotCarat, SqlOps.GetParams(), "@Exists");
            return Convert.ToBoolean(message);
        }

        public static DataTable MergeTables(DataTable t1, DataTable t2)
        {
            if (t1 == null || t2 == null) throw new ArgumentNullException("t1 or t2", "Both tables must not be null");

            if (t1.Rows.Count > t2.Rows.Count)
            {
                for (int i = t2.Rows.Count; i < t1.Rows.Count; i++)
                {
                    DataRow _BlankRow = t2.NewRow(); t2.Rows.Add(_BlankRow);
                }
            }
            else if (t2.Rows.Count > t1.Rows.Count)
            {
                for (int i = t1.Rows.Count; i < t2.Rows.Count; i++)
                {
                    DataRow _BlankRow = t1.NewRow(); t1.Rows.Add(_BlankRow);
                }
            }
            DataTable t3 = t1.Clone();  // first add columns from table1
            foreach (DataColumn col in t2.Columns)
            {
                string newColumnName = col.ColumnName;
                int colNum = 1;
                while (t3.Columns.Contains(newColumnName))
                {
                    newColumnName = string.Format("{0}_{1}", col.ColumnName, ++colNum);
                }
                t3.Columns.Add(newColumnName, col.DataType);
            }
            var mergedRows = t1.AsEnumerable().Zip(t2.AsEnumerable(), (r1, r2) => r1.ItemArray.Concat(r2.ItemArray).ToArray());
            foreach (object[] rowFields in mergedRows)
                t3.Rows.Add(rowFields);
            return t3;
        }

        public static void ConvertHTMLtoPDF(string html)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
            }
        }
    }

}