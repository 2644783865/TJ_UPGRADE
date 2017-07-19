using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.SM_Data.SM_Trans_Management
{
    public partial class SM_Trans_KHZT : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                databind();
            }
            CheckUser(ControlFinder);
        }

        private void databind()
        {
            GetYear();
            GetProject();
            GetProject();

            GetYZZT();
        }

        protected void GetYear()
        {
            string sql = "SELECT DISTINCT KHZT_YEAR FROM TBTM_KHZT";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear.DataTextField = "KHZT_YEAR";
            DropDownListYear.DataValueField = "KHZT_YEAR";
            DropDownListYear.DataSource = dt;
            DropDownListYear.DataBind();
        }

        protected void GetProject()
        {
            string sql = "SELECT DISTINCT KHZT_PJNAME  FROM TBTM_KHZT";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListproject.DataTextField = "KHZT_PJNAME";
            DropDownListproject.DataValueField = "KHZT_PJNAME";
            DropDownListproject.DataSource = dt;
            DropDownListproject.DataBind();
            DropDownListproject.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            DropDownListproject.SelectedIndex = 0;
        }

        protected void GetEngeering()
        {
            string sql = "SELECT DISTINCT KHZT_ENGNAME  FROM TBTM_KHZT where KHZT_PJNAME='" + DropDownListproject.SelectedValue + "' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListengeering.DataTextField = "KHZT_ENGNAME";
            DropDownListengeering.DataValueField = "KHZT_ENGNAME";
            DropDownListengeering.DataSource = dt;
            DropDownListengeering.DataBind();
            DropDownListengeering.Items.Insert(0, new ListItem(" ", " "));
            DropDownListengeering.SelectedIndex = 0;
        }

        protected void GetYZZT()
        {
            string sql = "";

            if (DropDownListproject.SelectedValue != "-请选择-")
            {
                if (DropDownListengeering.SelectedValue.Trim() != "")
                {
                    sql = "SELECT KHZT_ID,KHZT_PJNAME,KHZT_ENGNAME,KHZT_ZW,KHZT_ZV,KHZT_RZB,KHZT_DJTIME,KHZT_ZL1,KHZT_TJ1,KHZT_ZL2,KHZT_TJ2,KHZT_BZ FROM TBTM_KHZT " +
                                "WHERE KHZT_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and KHZT_PJNAME='" + DropDownListproject.SelectedValue + "' and KHZT_ENGNAME='" + DropDownListengeering.SelectedValue + "'";
                }
                else
                {
                    sql = "SELECT KHZT_ID,KHZT_PJNAME,KHZT_ENGNAME,KHZT_ZW,KHZT_ZV,KHZT_RZB,KHZT_DJTIME,KHZT_ZL1,KHZT_TJ1,KHZT_ZL2,KHZT_TJ2,KHZT_BZ FROM TBTM_KHZT " +
                                "WHERE KHZT_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and KHZT_PJNAME='" + DropDownListproject.SelectedValue + "' ";
                }
            }
   
            else
            {
                sql = "SELECT KHZT_ID,KHZT_PJNAME,KHZT_ENGNAME,KHZT_ZW,KHZT_ZV,KHZT_RZB,KHZT_DJTIME,KHZT_ZL1,KHZT_TJ1,KHZT_ZL2,KHZT_TJ2,KHZT_BZ FROM TBTM_KHZT " +
                                "WHERE KHZT_YEAR='" + DropDownListYear.SelectedValue.ToString() + "'";
            }
                        
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterYZZT.DataSource = dt;
            RepeaterYZZT.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel5.Visible = true;
            }
            else
            {
                Panel5.Visible = false;
            }
        }

        protected void RepeaterYZZT_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterYZZT.Controls)
            {
                if (item.ItemType == ListItemType.Header)
                {
                    ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear")).Text = DropDownListYear.SelectedValue;
                    break;
                }
            }
        }

        protected void DropDownListYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetYZZT();
        }

        protected void DropDownListPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEngeering();
            GetYZZT();
        }

        protected void DropDownListENG_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetYZZT();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_KHZTEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterYZZT.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterYZZT.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_KHZT WHERE KHZT_ID='" + ((System.Web.UI.WebControls.Label)RepeaterYZZT.Items[i].FindControl("LabelID")).Text + "'";
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count > 0)
            {
                DBCallCommon.ExecuteTrans(sqllist);
            }
            databind();
        }
        
        protected void Export_Click(object sender, EventArgs e)
        {
            string sql = "SELECT KHZT_PJNAME,KHZT_ENGNAME,KHZT_ZW,KHZT_ZV,KHZT_RZB,KHZT_DJTIME,KHZT_ZL1,KHZT_TJ1,KHZT_ZL2,KHZT_TJ2,KHZT_BZ FROM TBTM_KHZT " +
                "WHERE KHZT_YEAR='" + DropDownListYear.SelectedValue + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        
            Application m_xlApp = new Application();

            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("客户自提设备发运台账模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            m_xlApp.Visible = false;//Excel不显示
            m_xlApp.DisplayAlerts = false;//关闭提示，采用默认方式执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
          

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wksheet.Cells[i + 5, 1] = Convert.ToString(i + 1);//序号

                wksheet.Cells[i + 5, 2] = "'" + dt.Rows[i]["KHZT_PJNAME"].ToString();//项目名称

                wksheet.Cells[i + 5, 3] = "'" + dt.Rows[i]["KHZT_ENGNAME"].ToString();//工程名称

                wksheet.Cells[i + 5, 4] = "'" + dt.Rows[i]["KHZT_ZW"].ToString();//总重量

                wksheet.Cells[i + 5, 5] = "'" + dt.Rows[i]["KHZT_ZV"].ToString();//总体积

                wksheet.Cells[i + 5, 6] = "'" + dt.Rows[i]["KHZT_RZB"].ToString();//容重比

                wksheet.Cells[i + 5, 7] = "'" + dt.Rows[i]["KHZT_DJTIME"].ToString();//登记日期

                wksheet.Cells[i + 5, 8] = "'" + dt.Rows[i]["KHZT_ZL1"].ToString();//重量

                wksheet.Cells[i +5, 9] = "'" + dt.Rows[i]["KHZT_TJ1"].ToString();//体积

                wksheet.Cells[i + 5, 10] = "'" + dt.Rows[i]["KHZT_ZL2"].ToString();//重量

                wksheet.Cells[i + 5, 11] = "'" + dt.Rows[i]["KHZT_TJ2"].ToString();//体积

                wksheet.Cells[i + 5, 12] = "'" + dt.Rows[i]["KHZT_BZ"].ToString();//备注

                

                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5,12]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 12]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 12]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            }
             //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "客户自提设备发运" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        
            //ApplicationClass ac = new ApplicationClass();
            //ac.Visible = false;     // Excel不显示  
            //Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            //Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            //ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            
            ////设置表头
            //ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear.SelectedValue + "业主自提运输台账";
            //Range range;
            //range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 12]);
            //range.MergeCells = true;
            //ac.Cells[2, 1] = "序号";
            //range = wksheet.get_Range(wksheet.Cells[2, 1], wksheet.Cells[4, 1]);
            //range.MergeCells = true;
            //ac.Cells[2, 2] = "项目名称";
            //range = wksheet.get_Range(wksheet.Cells[2, 2], wksheet.Cells[4, 2]);
            //range.MergeCells = true;
            //ac.Cells[2, 3] = "工程名称";
            //range = wksheet.get_Range(wksheet.Cells[2, 3], wksheet.Cells[4, 3]);
            //range.MergeCells = true;
            //ac.Cells[2, 4] = "总重量（T）";
            //range = wksheet.get_Range(wksheet.Cells[2, 4], wksheet.Cells[4, 4]);
            //range.MergeCells = true;
            //ac.Cells[2, 5] = "总体积（m3）";
            //range = wksheet.get_Range(wksheet.Cells[2, 5], wksheet.Cells[4, 5]);
            //range.MergeCells = true;
            //ac.Cells[2, 6] = "容重比";
            //range = wksheet.get_Range(wksheet.Cells[2, 6], wksheet.Cells[4, 6]);
            //range.MergeCells = true;
            //ac.Cells[2, 7] = "实际发运量";
            //range = wksheet.get_Range(wksheet.Cells[2, 7], wksheet.Cells[2, 11]);
            //range.MergeCells = true;
            //ac.Cells[3, 7] = "登记日期";
            //range = wksheet.get_Range(wksheet.Cells[3, 7], wksheet.Cells[4, 7]);
            //range.MergeCells = true;
            //ac.Cells[3, 8] = "本期发运量";
            //range = wksheet.get_Range(wksheet.Cells[3, 8], wksheet.Cells[3, 9]);
            //range.MergeCells = true;
            //ac.Cells[4, 8] = "重量（T）";
            //ac.Cells[4, 9] = "体积（m3）";
            //ac.Cells[3, 10] = "本年度累计发运量";
            //range = wksheet.get_Range(wksheet.Cells[3, 10], wksheet.Cells[3, 11]);
            //range.MergeCells = true;
            //ac.Cells[4, 10] = "重量（T）";
            //ac.Cells[4, 11] = "体积（m3）";
            //ac.Cells[2, 12] = "备注";
            //range = wksheet.get_Range(wksheet.Cells[2, 12], wksheet.Cells[4, 12]);
            //range.MergeCells = true;

            //range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[4, 12]);
            //range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            ////用数组填充表格
            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = (i + 1).ToString();
            //    for (int j = 0; j < colCount; j++)
            //    {

            //        dataArray[i, j + 1] = dt.Rows[i][j];

            //    }

            //}
            //ac.get_Range("A5", ac.Cells[rowCount + 4, colCount+1]).Value2 = dataArray;
            //ac.get_Range("A5", ac.Cells[rowCount + 4, colCount+1]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/SM_Data/ExportFile/" + "KHZY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            //ac.Quit();
        }

            //关闭Excel进程
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();
                 //下载

                System.IO.FileInfo path = new System.IO.FileInfo(filename);

                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/SM_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //    #region kill excel process

        //    System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
        //    foreach (System.Diagnostics.Process p in procs)
        //    {
        //        int baseAdd = p.MainModule.BaseAddress.ToInt32();
        //        //oXL is Excel.ApplicationClass object 
        //        if (baseAdd == ac.Hinstance)
        //        {
        //            p.Kill();
        //            break;
        //        }
        //    }
        //    #endregion

        //    wkbook = null;
        //    ac = null;
        //    GC.Collect();    // 强制垃圾回收  
        //    //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
        //    Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
        //    Response.WriteFile(filename); // 把文件流发送到客户端 
        //    Response.Flush();
        //}

    }
}
