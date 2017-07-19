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
    public partial class SM_Trans_GNFY : BasicPage
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
            GetEngeering();
            GetGNFY();

        }

        protected void GetYear()
        {
            string sql = "SELECT DISTINCT GNFY_YEAR  FROM TBTM_GNFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear.DataTextField = "GNFY_YEAR";
            DropDownListYear.DataValueField = "GNFY_YEAR";
            DropDownListYear.DataSource = dt;
            DropDownListYear.DataBind();
        }

        protected void GetGNFY()
        {
            string sql = "";

            if (DropDownListproject.SelectedValue != "-请选择-")
            {
                if (DropDownListengeering.SelectedValue.Trim() != "")
                {
                    sql = "SELECT GNFY_ID,GNFY_PJNAME,GNFY_ENGNAME,GNFY_HTBH,GNFY_HTJE,GNFY_ZW,GNFY_ZV," +
                    "GNFY_RZB,GNFY_DJTIME,GNFY_ZL1,GNFY_TJ1,GNFY_ZL2,GNFY_TJ2,GNFY_CC,GNFY_GBZL,GNFY_LJE,GNFY_BZ,GNFY_YEAR FROM TBTM_GNFY " +
                        //"GNFY_LLZTJ AS GNFYLLZTJ,GNFY_LLZZL AS GNFYLLZZL,GNFY_LLCC AS GNFYLLCC,GNFY_GBFDJZL AS GNFYGBFDJZL,GNFY_GBDJZL AS GNFYGBDJZL," +
                        //"GNFY_ZZJSJE AS GNFYZZJSJE,GNFY_ZCJSZL AS GNFYZCJSZL,GNFY_ZCJSJE AS GNFYZCJSJE,GNFY_BZ AS GNFYBZ FROM TBTM_GNFY " +
                    "WHERE GNFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and GNFY_PJNAME='" + DropDownListproject.SelectedValue + "' and GNFY_ENGNAME='" + DropDownListengeering.SelectedValue + "' ";
                }
                else
                {
                    sql = "SELECT GNFY_ID,GNFY_PJNAME,GNFY_ENGNAME,GNFY_HTBH,GNFY_HTJE,GNFY_ZW,GNFY_ZV," +
                    "GNFY_RZB,GNFY_DJTIME,GNFY_ZL1,GNFY_TJ1,GNFY_ZL2,GNFY_TJ2,GNFY_CC,GNFY_GBZL,GNFY_LJE,GNFY_BZ,GNFY_YEAR FROM TBTM_GNFY " +
                        //"GNFY_LLZTJ AS GNFYLLZTJ,GNFY_LLZZL AS GNFYLLZZL,GNFY_LLCC AS GNFYLLCC,GNFY_GBFDJZL AS GNFYGBFDJZL,GNFY_GBDJZL AS GNFYGBDJZL," +
                        //"GNFY_ZZJSJE AS GNFYZZJSJE,GNFY_ZCJSZL AS GNFYZCJSZL,GNFY_ZCJSJE AS GNFYZCJSJE,GNFY_BZ AS GNFYBZ FROM TBTM_GNFY " +
                    "WHERE GNFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and GNFY_PJNAME='" + DropDownListproject.SelectedValue + "'  ";

                }
            }
            else
            {
                sql = "SELECT GNFY_ID,GNFY_PJNAME,GNFY_ENGNAME,GNFY_HTBH,GNFY_HTJE,GNFY_ZW,GNFY_ZV," +
                    "GNFY_RZB,GNFY_DJTIME,GNFY_ZL1,GNFY_TJ1,GNFY_ZL2,GNFY_TJ2,GNFY_CC,GNFY_GBZL,GNFY_LJE,GNFY_BZ,GNFY_YEAR FROM TBTM_GNFY " +
                    //"GNFY_LLZTJ AS GNFYLLZTJ,GNFY_LLZZL AS GNFYLLZZL,GNFY_LLCC AS GNFYLLCC,GNFY_GBFDJZL AS GNFYGBFDJZL,GNFY_GBDJZL AS GNFYGBDJZL," +
                    //"GNFY_ZZJSJE AS GNFYZZJSJE,GNFY_ZCJSZL AS GNFYZCJSZL,GNFY_ZCJSJE AS GNFYZCJSJE,GNFY_BZ AS GNFYBZ FROM TBTM_GNFY " +
                    "WHERE GNFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "'";
            }
            
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterGNFY.DataSource = dt;
            RepeaterGNFY.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel4.Visible = true;
            }
            else
            {
                Panel4.Visible = false;
            }
        }


        protected void GetProject()
        {
            string sql = "SELECT DISTINCT GNFY_PJNAME  FROM TBTM_GNFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListproject.DataTextField = "GNFY_PJNAME";
            DropDownListproject.DataValueField = "GNFY_PJNAME";
            DropDownListproject.DataSource = dt;
            DropDownListproject.DataBind();
            DropDownListproject.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            DropDownListproject.SelectedIndex = 0;
        }

        protected void GetEngeering()
        {
            string sql = "SELECT DISTINCT GNFY_ENGNAME  FROM TBTM_GNFY where GNFY_PJNAME='" + DropDownListproject.SelectedValue+ "' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListengeering.DataTextField = "GNFY_ENGNAME";
            DropDownListengeering.DataValueField = "GNFY_ENGNAME";
            DropDownListengeering.DataSource = dt;
            DropDownListengeering.DataBind();
            DropDownListengeering.Items.Insert(0, new ListItem(" ", " "));
            DropDownListengeering.SelectedIndex = 0;
        }


        protected void RepeaterGNFY_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterGNFY.Controls)
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
            GetGNFY();
        }

        protected void DropDownListPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEngeering();
            GetGNFY();
        }

        protected void DropDownListENG_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetGNFY();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_GNFYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterGNFY.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterGNFY.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_GNFY WHERE GNFY_ID='" + ((System.Web.UI.WebControls.Label)RepeaterGNFY.Items[i].FindControl("LabelID")).Text + "'";
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
            string sql = "SELECT GNFY_PJNAME,GNFY_ENGNAME,GNFY_HTBH,GNFY_HTJE,GNFY_ZW,GNFY_ZV," +
                "GNFY_RZB,GNFY_DJTIME,GNFY_ZL1,GNFY_TJ1,GNFY_ZL2,GNFY_TJ2,GNFY_CC,GNFY_GBZL,GNFY_LJE,GNFY_BZ FROM TBTM_GNFY WHERE GNFY_YEAR='" + DropDownListYear.SelectedValue + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Application m_xlApp = new Application();

            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("国内设备发运台账模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            m_xlApp.Visible = false;//Excel不显示
            m_xlApp.DisplayAlerts = false;//关闭提示，采用默认方式执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wksheet.Cells[i + 5, 1] = Convert.ToString(i + 1);//序号

                wksheet.Cells[i + 5, 2] = "'" + dt.Rows[i]["GNFY_PJNAME"].ToString();//项目名称

                wksheet.Cells[i + 5, 3] = "'" + dt.Rows[i]["GNFY_ENGNAME"].ToString();//工程名称

                wksheet.Cells[i + 5, 4] = "'" + dt.Rows[i]["GNFY_HTBH"].ToString();//合同编号

                wksheet.Cells[i + 5, 5] = "'" + dt.Rows[i]["GNFY_HTJE"].ToString();//合同金额

                wksheet.Cells[i + 5, 6] = "'" + dt.Rows[i]["GNFY_ZW"].ToString();//总重量

                wksheet.Cells[i + 5, 7] = "'" + dt.Rows[i]["GNFY_ZV"].ToString();//总体积

                wksheet.Cells[i + 5, 8] = "'" + dt.Rows[i]["GNFY_RZB"].ToString();//容重比

                wksheet.Cells[i + 5, 9] = "'" + dt.Rows[i]["GNFY_DJTIME"].ToString();//登记日期

                wksheet.Cells[i + 5, 10] = "'" + dt.Rows[i]["GNFY_ZL1"].ToString();//重量

                wksheet.Cells[i + 5, 11] = "'" + dt.Rows[i]["GNFY_TJ1"].ToString();//体积

                wksheet.Cells[i + 5, 12] = "'" + dt.Rows[i]["GNFY_ZL2"].ToString();//重量


                wksheet.Cells[i + 5, 13] = "'" + dt.Rows[i]["GNFY_TJ2"].ToString();//体积

                wksheet.Cells[i + 5, 14] = "'" + dt.Rows[i]["GNFY_CC"].ToString();//车次

                wksheet.Cells[i + 5, 15] = "'" + dt.Rows[i]["GNFY_GBZL"].ToString();//过磅重量

                wksheet.Cells[i + 5, 16] = "'" + dt.Rows[i]["GNFY_LJE"].ToString();//最终结算金额

                wksheet.Cells[i + 5, 17] = "'" + dt.Rows[i]["GNFY_BZ"].ToString();//备注


                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 17]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 17]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 17]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            }
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "国内设备发运" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
       
        
            //    ApplicationClass ac = new ApplicationClass();
            //    ac.Visible = true;     // Excel不显示  
            //    Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            //    Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            //    ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //    // 设置单元格格式  
            //    //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).NumberFormat = "@";
            //    //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).Font.Size = 12;

            //    //设置表头
            //    ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear.SelectedValue + "国内设备发运台账";
            //    Range range;
            //    range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 17]);
            //    range.MergeCells = true;

            //    ac.Cells[2, 1] = "序号";
            //    range = wksheet.get_Range(wksheet.Cells[2, 1], wksheet.Cells[4, 1]);
            //    range.MergeCells = true;
            //    ac.Cells[2, 2] = "项目名称";
            //    range = wksheet.get_Range(wksheet.Cells[2, 2], wksheet.Cells[4, 2]);
            //    range.MergeCells = true;
            //    ac.Cells[2, 3] = "工程名称";
            //    range = wksheet.get_Range(wksheet.Cells[2, 3], wksheet.Cells[4, 3]);
            //    range.MergeCells = true;
            //    ac.Cells[2, 4] = "运输合同";
            //    range = wksheet.get_Range(wksheet.Cells[2, 4], wksheet.Cells[2, 7]);
            //    range.MergeCells = true;
            //    ac.Cells[3, 4] = "合同编号";
            //    range = wksheet.get_Range(wksheet.Cells[3, 4], wksheet.Cells[4, 4]);
            //    range.MergeCells = true;
            //    ac.Cells[3, 5] = "合同金额";
            //    range = wksheet.get_Range(wksheet.Cells[3, 5], wksheet.Cells[4, 5]);
            //    range.MergeCells = true;
            //    ac.Cells[3, 6] = "总重量（T)";
            //    range = wksheet.get_Range(wksheet.Cells[3, 6], wksheet.Cells[4, 6]);
            //    range.MergeCells = true;
            //    ac.Cells[3, 7] = "总体积（m3)";
            //    range = wksheet.get_Range(wksheet.Cells[3, 7], wksheet.Cells[4, 7]);
            //    range.MergeCells = true;
            //    ac.Cells[2, 8] = "容重比（m3/T）";
            //    range = wksheet.get_Range(wksheet.Cells[2, 8], wksheet.Cells[4, 8]);
            //    range.MergeCells = true;
            //    ac.Cells[2, 9] = "实际发运量";
            //    range = wksheet.get_Range(wksheet.Cells[2, 9], wksheet.Cells[2, 14]);
            //    range.MergeCells = true;
            //    ac.Cells[3, 9] = "登记日期";
            //    range = wksheet.get_Range(wksheet.Cells[3, 9], wksheet.Cells[4, 9]);
            //    range.MergeCells = true;
            //    ac.Cells[3, 10] = "本期发运量";
            //    range = wksheet.get_Range(wksheet.Cells[3, 10], wksheet.Cells[3, 11]);
            //    range.MergeCells = true;
            //    ac.Cells[4, 10] = "重量（T）";

            //    ac.Cells[4, 11] = "体积（m3）";
            //    ac.Cells[3, 12] = "本年度累计发运量";
            //    range = wksheet.get_Range(wksheet.Cells[3, 12], wksheet.Cells[3, 13]);
            //    range.MergeCells = true;
            //    ac.Cells[4, 12] = "重量（T）";
            //    ac.Cells[4, 13] = "体积（m3）";
            //    ac.Cells[3, 14] = "车次";
            //    range = wksheet.get_Range(wksheet.Cells[3, 14], wksheet.Cells[4, 14]);
            //    range.MergeCells = true;

            //    ac.Cells[2, 15] = "过磅重量（T）";
            //    range = wksheet.get_Range(wksheet.Cells[2, 15], wksheet.Cells[4, 15]);
            //    range.MergeCells = true;
            //    ac.Cells[2, 16] = "最终结算金额（元）";
            //    range = wksheet.get_Range(wksheet.Cells[2, 16], wksheet.Cells[4, 16]);
            //    range.MergeCells = true;
            //    ac.Cells[2, 17] = "备注";
            //    range = wksheet.get_Range(wksheet.Cells[2, 17], wksheet.Cells[4, 17]);
            //    range.MergeCells = true;

            //    range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[4, 17]);
            //    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            //    //用数组填充表格
            //    int rowCount = dt.Rows.Count;

            //    int colCount = dt.Columns.Count;

            //    object[,] dataArray = new object[rowCount, colCount + 1];

            //    for (int i = 0; i < rowCount; i++)
            //    {
            //        dataArray[i, 0] = (i + 1).ToString();
            //        for (int j = 0; j < colCount; j++)
            //        {

            //            dataArray[i, j + 1] = dt.Rows[i][j];

            //        }

            //    }
            //    ac.get_Range("A5", ac.Cells[rowCount + 4, colCount + 1]).Value2 = dataArray;
            //    ac.get_Range("A5", ac.Cells[rowCount + 4, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //    wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //    string filename = Server.MapPath("/SM_Data/ExportFile/" + "GNFY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //    wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //    wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            //    ac.Quit();
        }
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
            //    //关闭Excel进程
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
