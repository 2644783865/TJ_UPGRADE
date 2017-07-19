using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ZCZJ_DPF;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseStorage_Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }
        }

        private string GetCondition()
        {
            string condition = "";

            string sql = "select StrCondition from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='0'";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (dt.Rows.Count > 0)
            {
                condition = dt.Rows[0]["StrCondition"].ToString();
            }
            return condition.Replace("^", "'");
        }


        protected void Confirm_Click(object sender, EventArgs e)
        {
            string sql = "";

            string condition = GetCondition();

            string tableName = "View_SM_Storage";
            //type为1时不是即时库存
           if(Request.QueryString["type"]=="1")
           {
              tableName = "View_SM_StorageTemp";

              if (condition != "")
              {
                  condition += " AND " + " OperState='Query" + Session["UserID"].ToString() + "'";
              }
              else
              {
                  condition += " OperState='Query" + Session["UserID"].ToString() + "'";
              }
           }
           
            if (CheckBoxListStyle.SelectedValue=="0")
            {
                //条目明细
                if (condition != "")
                {
                    sql = "SELECT MaterialCode,MaterialName,Attribute,Standard,GB,LotNumber,Length,Width,Unit,Number as Num,SupportNumber,Fixed,PTC,Warehouse,Location,CGMODE,Note FROM " + tableName + " WHERE " + condition + " order by MaterialCode ";
                }
                else
                {
                    sql = "SELECT MaterialCode,MaterialName,Attribute,Standard,GB,LotNumber,Length,Width,Unit,Number as Num,SupportNumber,Fixed,PTC,Warehouse,Location,CGMODE,Note FROM " + tableName + " order by MaterialCode";
                }

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                ExportDataItem(dt);

            }
            else if (CheckBoxListStyle.SelectedValue=="1")
            {
                //汇总1,按物料编码汇总,MaterialCode,MaterialName,Standard,GB,Attribute,Unit

                //标准件
                if (condition != "")
                {
                    sql = "SELECT MaterialCode,MaterialName,Attribute,Standard,GB,Unit,cast(SUM(Number) as float) AS Num,cast(SUM(SupportNumber) as float) AS SNum FROM " + tableName + " WHERE " + condition + " GROUP BY MaterialCode,MaterialName,Standard,GB,Attribute,Unit order by MaterialCode";
                }
                else
                {
                    sql = "SELECT MaterialCode,MaterialName,Attribute,Standard,GB,Unit,cast(SUM(Number) as float) AS Num,cast(SUM(SupportNumber) as float) AS SNum FROM " + tableName + " GROUP BY MaterialCode,MaterialName,Standard,GB,Attribute,Unit order by MaterialCode";
                }
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                ExportDataSummaryOne(dt);
 
            }
            else if (CheckBoxListStyle.SelectedValue == "2")
            {

                //钢材汇总

                //按物料编码，长，宽,MaterialCode,MaterialName,Standard,Unit,NumberInAccount,Length,Width
                if (condition != "")
                {
                    sql = "SELECT MaterialCode,MaterialName,Attribute,Standard,GB,Length,Width,Unit,cast(SUM(Number) as float) AS SumNum,cast(SUM(SupportNumber) as float) AS SNum FROM " + tableName + " WHERE " + condition + " GROUP BY MaterialCode,MaterialName,Standard,GB,Attribute,Unit,Length,Width order by MaterialCode";
                }
                else
                {
                    sql = "SELECT MaterialCode,MaterialName,Attribute,Standard,GB,Length,Width,Unit,cast(SUM(Number) as float) AS SumNum,cast(SUM(SupportNumber) as float) AS SNum FROM " + tableName + " GROUP BY MaterialCode,MaterialName,Standard,GB,Attribute,Unit,Length,Width order by MaterialCode";

                }
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                ExportDataSummaryTwo(dt);

            }
            else if (CheckBoxListStyle.SelectedValue == "3")
            {
                //名称汇总
                if (condition != "")
                {
                    sql = "SELECT MaterialName AS 物料名称,Unit AS 单位,cast(SUM(Number) as float)  AS 数量 FROM " + tableName + " WHERE " + condition + " GROUP BY MaterialName,Unit order by MaterialName";
                }
                else
                {
                    sql = "SELECT MaterialName AS 物料名称,Unit AS 单位,cast(SUM(Number) as float)  AS 数量 FROM " + tableName + " GROUP BY MaterialName,Unit order by MaterialName";
                }

                ExportExcel(sql);
            }
        }

        private void ExportExcel(string strsql)
        {

            #region
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).NumberFormatLocal = "@";

            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = dt.Rows[i][j];


                }
            }

            //设置表头，excel的行从1开始Microsoft.Office.Interop.Excel.Workbook 这个东西下面的Worksheets，Worksheets.Columns等集合的索引全是从1开始

            //设置表头
            wksheet.Cells[1, 1] = "序号";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                wksheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;

                if (dt.Columns[i].ColumnName == "数量")
                {
                    wksheet.get_Range(getExcelColumnLabel(i + 2) + "1", wksheet.Cells[1 + rowCount, i + 2]).NumberFormatLocal = "G/通用格式";
                }
            }


            //设置表体
            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;


            //设置公式
            Range rg = wksheet.get_Range("D" + (rowCount + 2).ToString(), System.Type.Missing);
            rg.Formula = "=SUM(D2:D" + (dt.Rows.Count + 1).ToString() + ")";

            rg.Calculate();


            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "库存导出" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            if (File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }

            string script = String.Format("EndDownload()");

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "EndDownload();", true);
            #endregion

        }



        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "库存明细.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("即时库存明细表模版.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 4);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < objdt.Columns.Count; j++)
                    {
                        string str = objdt.Rows[i][j].ToString();
                        row.CreateCell(j+1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= objdt.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
            #region
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;

            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("即时库存明细表模版") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //int rowCount = objdt.Rows.Count;

            //int colCount = objdt.Columns.Count;

            //string sumcol = string.Empty;

            //for (int i = 0; i < objdt.Columns.Count; i++)
            //{
            //    if (objdt.Columns[i].ColumnName == "Num")
            //    {
            //        int col = i + 1;
            //        sumcol = getExcelColumnLabel(col+1);
            //        break;
            //    }
            //}

            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 1]).NumberFormatLocal = "@";
            
            //object[,] dataArray = new object[rowCount, colCount+1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0]=(object)(i+1);

            //    for (int j = 0; j < colCount; j++)
            //    {
                    
            //        dataArray[i, j+1] =objdt.Rows[i][j];

            //    }
            //}

            ////get_Range第一个参数只能是A5

            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount+1]).Value2 = dataArray;

            ////设置公式
            //Range rg = wksheet.get_Range(sumcol + (5 + rowCount).ToString(), System.Type.Missing); //获取公式列

            ////rg.Formula = "=SUM(" + wksheet.Cells[5, sumcol + 1].ToString() + ":" + wksheet.Cells[4 + rowCount, sumcol + 1].ToString() + ")";
            
            ////rg.Formula = "=SUM(K5:K"+(4 + rowCount).ToString() + ")";

            //rg.Formula = "=SUM(" + sumcol + "5:" + sumcol + (4 + rowCount).ToString() + ")";

            //rg.Calculate();

            //rg.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            ////设置列宽
            ////wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/SM_Data/ExportFile/" + "即时库存条目" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            #endregion

        }

        //将EXCEL列数转换为列字母
        private string getExcelColumnLabel(int index)
        {
            String rs = "";

            do
            {
                index--;
                rs = ((char)(index % 26 + (int)'A')) + rs;
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return rs;
        }

        private void ExportDataSummaryOne(System.Data.DataTable objdt)
        {
            string filename = "库存明细汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("即时库存明细表模版汇总1.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 4);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < objdt.Columns.Count; j++)
                    {
                        string str = objdt.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= objdt.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }

            #region
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("即时库存明细表模版汇总1") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            

            //int rowCount = objdt.Rows.Count;

            //int colCount = objdt.Columns.Count;

            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ////wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).NumberFormatLocal = "@";

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = (object)(i + 1);

            //    for (int j = 0; j < colCount; j++)
            //    {

            //        dataArray[i, j + 1] = objdt.Rows[i][j];

            //    }
            //}

            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount+1]).Value2 = dataArray;
            
            ////设置列宽
            ////wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/SM_Data/ExportFile/" + "即时库存条目" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            #endregion
        }

        private void ExportDataSummaryTwo(System.Data.DataTable objdt)
        {
            string filename = "库存明细.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("即时库存明细表模版汇总2.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 4);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < objdt.Columns.Count; j++)
                    {
                        string str = objdt.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= objdt.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }




            #region
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("即时库存明细表模版汇总2") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //int rowCount = objdt.Rows.Count;

            //int colCount = objdt.Columns.Count;

            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
           
            ////wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount + 2]).NumberFormatLocal = "@";

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = (object)(i + 1);

            //    for (int j = 0; j < colCount; j++)
            //    {

            //        dataArray[i, j + 1] = objdt.Rows[i][j];

            //    }
            //}

            //wksheet.get_Range("A5", wksheet.Cells[4 + rowCount, colCount+1]).Value2 = dataArray;
          

            ////设置列宽
            ////wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/SM_Data/ExportFile/" + "即时库存条目" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            #endregion
        }


        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();


                #region kill excel process

                System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");

                foreach (System.Diagnostics.Process p in procs)
                {
                    int baseAdd = p.MainModule.BaseAddress.ToInt32();
                    //oXL is Excel.ApplicationClass object 
                    if (baseAdd == m_xlApp.Hinstance)
                    {
                        p.Kill();
                        break;
                    }
                }

                #endregion


                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                //下载

                //System.IO.FileInfo path = new System.IO.FileInfo(filename);

                //////同步，异步都支持
                //HttpResponse contextResponse = HttpContext.Current.Response;
                //contextResponse.Redirect(string.Format("~/SM_Data/ExportFile/{0}", path.Name), false);

                DownloadFile.Send(Context, filename);


                //contextResponse.End();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", String.Format("window.open('{0}');", Server.UrlEncode(filename)), true); 

                string script = String.Format("EndDownload()");

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "EndDownload();", true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
