using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BAL
{
    public static class DyanamicJson
    {
        public static string sort_field = "", sort_group = "";

        public static DataTable dynamic_dt(DataTable dt_grid, string Report_title, string groupBy = "", Boolean isEvalPerfom = true)
        {
            DataTable dt_dyna = new DataTable();
            try
            {

                string eval = "none";
                string align = "Center";
                string width = "700";
                if (dt_grid.Rows.Count > 0)
                {
                    dt_dyna.Columns.Add("column_name");
                    dt_dyna.Columns.Add("id");
                    dt_dyna.Columns.Add("col_header");
                    dt_dyna.Columns.Add("width");
                    dt_dyna.Columns.Add("groupby");
                    dt_dyna.Columns.Add("groupindex");
                    dt_dyna.Columns.Add("eval");
                    dt_dyna.Columns.Add("align");
                    int k = 0;
                    string[] columnNames = (from dc in dt_grid.Columns.Cast<DataColumn>()
                                            select dc.ColumnName).ToArray();
                    string[] grouping = groupBy == "" ? null : groupBy.Split(',');

                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        try
                        {
                            eval = "none";
                            align = "Center";
                            width = "1000";
                            if (columnNames[i].ToString().ToUpper().Contains("ROWNUM"))
                            {
                                width = "0";


                            }
                           else if (columnNames[i].ToString().ToUpper().Contains("PIECE"))
                            {

                                width = "650";
                                if (isEvalPerfom)
                                    eval = "sum";
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("CARAT"))
                            {
                                align = "Center";
                                width = "600";
                                if (isEvalPerfom)
                                    eval = "sum";
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("WEIGHT"))
                            {
                                align = "Right";
                                width = "900";
                                if (isEvalPerfom)
                                    eval = "sum";
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("DATE"))
                                width = "1200";

                            else if (columnNames[i].ToString().ToUpper().Contains("ROUGH"))
                                width = "1000";
                            else if (columnNames[i].ToString().ToUpper().Contains("LOT"))
                                width = "750";
                            else if (columnNames[i].ToString().ToUpper().Contains("PACKET"))
                                width = "1000";
                            else if (columnNames[i].ToString().ToUpper().Contains("TYPE"))
                            {
                                align = "center";
                                width = "1200";
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("NAME") || columnNames[i].ToString().ToUpper().Contains("ISSUE TO"))
                            {
                                if (columnNames[i].ToString().ToUpper().Contains("PARTY") || columnNames[i].ToString().ToUpper().Contains("SALES"))
                                {
                                    align = "center";
                                    width = "3000";
                                }
                                if (columnNames[i].ToString().ToUpper().Contains("ISSUE TO"))
                                {
                                    align = "Left";
                                    width = "2000";
                                }
                                else
                                {
                                    align = "Left";
                                    width = "1500";
                                }
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("PRICE"))
                            {
                                align = "Right";
                                width = "1000";
                                if (isEvalPerfom)
                                    eval = "sum";
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("VALUE"))
                            {
                                align = "Right";
                                width = "1000";
                                if (isEvalPerfom)
                                    eval = "sum";
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("PROCESS"))
                                width = "1500";
                            else if (columnNames[i].ToString().ToUpper().Contains("COLOR") || columnNames[i].ToString().ToUpper().Contains("CUT") || columnNames[i].ToString().ToUpper().Contains("POLISH") || columnNames[i].ToString().ToUpper().Contains("SYMMETRY") || columnNames[i].ToString().ToUpper().Contains("Memo") || columnNames[i].ToString().ToUpper().Contains("INVOICE"))

                                width = "750";

                            else if (columnNames[i].ToString().ToUpper().Contains("SHADE") || columnNames[i].ToString().ToUpper().Contains("BOXNAME"))
                                width = "750";
                            else if (columnNames[i].ToString().ToUpper().Contains("GRADE"))
                                width = "1050";
                            else if (columnNames[i].ToString().ToUpper().Contains("SHAPE"))
                                width = "850";
                            else if (columnNames[i].ToString().ToUpper().Contains("SIEVES"))
                                width = "700";
                            else if (columnNames[i].ToString().ToUpper().Contains("DAYS"))
                                width = "500";
                            else if (columnNames[i].ToString().ToUpper().Contains("REF"))
                                width = "1200";
                            else if (columnNames[i].ToString().ToUpper().Contains("CLIENT STATED") || columnNames[i].ToString().ToUpper().Contains("CONTROL NO") || columnNames[i].ToString().ToUpper().Contains("SERVICE"))
                                width = "1400";
                            else if (columnNames[i].ToString().ToUpper().Contains("CLIENT STATED CARAT WEIGHT") || columnNames[i].ToString().ToUpper().Contains("MEASUREMENT"))
                                width = "1600";
                            else
                                width = "800";

                            if (columnNames[i].ToString().ToUpper().Contains("ROUGHS"))
                            {
                                align = "Center";
                                width = "1200";
                                if (isEvalPerfom)
                                    eval = "sum";
                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("PACKETS") || columnNames[i].ToString().ToUpper().Contains("FINISH"))
                            {
                                align = "Center";
                                width = "1300";

                            }
                            else if (columnNames[i].ToString().ToUpper().Contains("JANGADS"))
                            {
                                align = "Center";
                                width = "1200";
                                if (isEvalPerfom)
                                    eval = "sum";
                            }
                            if (columnNames[i].ToString().ToUpper().Contains("%"))
                            {
                                align = "Right";
                                width = "900";
                                if (isEvalPerfom)
                                    eval = "average";
                            }

                            DataRow dr = dt_dyna.NewRow();

                            dr[0] = columnNames[i].ToString();
                            dr[1] = (i + 1).ToString();
                            dr[2] = columnNames[i].ToString();
                            dr[3] = width;

                            if (groupBy != "" && grouping.Length > 0)
                            {
                                for (int g = 0; g < grouping.Length; g++)
                                {

                                    if (i == Convert.ToInt32(grouping[g].ToString()))
                                    {
                                        dr[4] = "True";
                                        dr[3] = 0;
                                        dr[5] = (g + 1).ToString();
                                        if (dr[0].ToString().ToUpper().Contains("Date"))
                                        {
                                            sort_group = dr[5].ToString();
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        dr[4] = "False";
                                        dr[5] = "0";
                                    }
                                }

                            }
                            else
                            {
                                dr[4] = "False";
                                dr[5] = "0";
                            }
                            dr[6] = eval;

                            dr[7] = align;
                            dt_dyna.Rows.Add(dr);

                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }
                return dt_dyna;
            }
            catch (Exception)
            {
            }
            return dt_dyna;
        }

        public static String DyanamicJsonFromTable(DataTable dt_detail, string ReportFormat, string Report_Title, Boolean isEvalPerfom = true)
        {
            try
            {
                DataTable dt_jsontable = dynamic_dt(dt_detail.Copy(), Report_Title, string.Empty, isEvalPerfom);

                TableDesign tblData = new TableDesign();

                tblData.type = "Query";
                tblData.tableORquery = "";
                tblData.Title = Report_Title;
                tblData.reportPage = ReportFormat;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt_jsontable, Newtonsoft.Json.Formatting.Indented);
                tblData.FieldData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FieldData>>(json);

                //Details Design
                Details det = new Details();
                det.font = "Cambria";
                det.size = isEvalPerfom ? 10 : 8;
                det.color = "0,0,0";

                tblData.Details = det;

                //Page Header
                PageHeader pagehead = new PageHeader();
                pagehead.HeaderText = Report_Title;
                pagehead.font = "Cambria";
                pagehead.size = 12;
                pagehead.color = "0,0,0";
                pagehead.top = 50;
                pagehead.left = 25;


                pagehead.width = ReportFormat == "Portrait" ? 11100 : 16200;
                pagehead.align = "Center";
                tblData.PageHeader = pagehead;

                //Group Footer LineStyle
                GroupFooter gf = new GroupFooter();
                gf.LineStyle = "Dotted";
                gf.color = "0,0,0";
                tblData.GroupFooter = gf;

                string rptjson = Newtonsoft.Json.JsonConvert.SerializeObject(tblData);
                return rptjson;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public class Detail_report
        {
            public string Type { get; set; }
            public ReportHeader ReportHeader { get; set; }
            public PageFooter PageFooter { get; set; }
        }

        //Report Header
        public class ReportHeader
        {
            public string font { get; set; }
            public string color { get; set; }
            public CompanyLogo CompanyLogo { get; set; }
            public CompanyHeader CompanyHeader { get; set; }
            public CompanyAddress CompanyAddress { get; set; }
            public CompanyContact CompanyContact { get; set; }
            public PrintDateTop PrintDateTop { get; set; }
        }

        public class CompanyLogo
        {
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
        }

        public class CompanyHeader
        {
            public string txtHead { get; set; }
            public int size { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
        }

        public class CompanyAddress
        {
            public string txtaddress { get; set; }
            public int size { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
        }
        public class CompanyContact
        {
            public string txtContact { get; set; }
            public int size { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
        }
        public class PrintDateTop
        {
            public bool show { get; set; }
            public int size { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
            public DateTime date { get; set; }
        }

        //Page Footer
        public class PageFooter
        {
            public string font { get; set; }
            public string color { get; set; }
            public PageFooterText PageFooterText { get; set; }
            public PageNumber PageNumber { get; set; }
            public PrintDateBot PrintDateBot { get; set; }
        }

        public class PageFooterText
        {
            public bool Show { get; set; }
            public string Text { get; set; }
            public int size { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
        }

        public class PageNumber
        {
            public bool Show { get; set; }
            public string Type { get; set; }
            public int size { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
        }

        public class PrintDateBot
        {
            public bool Show { get; set; }
            public int size { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
        }

        //Table Design Classes
        public class TableDesign
        {
            public string Title { get; set; }
            public string type { get; set; }
            public string tableORquery { get; set; }
            public string reportPage { get; set; }
            public List<FieldData> FieldData { get; set; }
            public PageHeader PageHeader { get; set; }
            public Details Details { get; set; }
            public GroupFooter GroupFooter { get; set; }
        }

        public class FieldData
        {
            public string column_name { get; set; }
            public int id { get; set; }
            public string col_header { get; set; }
            public int width { get; set; }
            public bool groupby { get; set; }
            public int groupindex { get; set; }
            public string eval { get; set; }
            public string align { get; set; }
        }

        public class PageHeader
        {
            public string HeaderText { get; set; }
            public string font { get; set; }
            public int size { get; set; }
            public string color { get; set; }
            public int top { get; set; }
            public int left { get; set; }
            public int width { get; set; }
            public string align { get; set; }
        }

        public class Details
        {
            public string font { get; set; }
            public int size { get; set; }
            public string color { get; set; }
        }

        public class GroupFooter
        {
            public string LineStyle { get; set; }
            public string color { get; set; }
        }
    }
}
