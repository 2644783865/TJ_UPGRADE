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
    public partial class SM_Trans_JZXFY : BasicPage
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

            GetJZXFY();
        }

        protected void GetYear()
        {
            string sql = "SELECT DISTINCT JZXFY_YEAR  FROM TBTM_JZXFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear.DataTextField = "JZXFY_YEAR";
            DropDownListYear.DataValueField = "JZXFY_YEAR";
            DropDownListYear.DataSource = dt;
            DropDownListYear.DataBind();
        }

        protected void GetProject()
        {
            string sql = "SELECT DISTINCT JZXFY_PROJECT  FROM TBTM_JZXFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListproject.DataTextField = "JZXFY_PROJECT";
            DropDownListproject.DataValueField = "JZXFY_PROJECT";
            DropDownListproject.DataSource = dt;
            DropDownListproject.DataBind();
            DropDownListproject.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            DropDownListproject.SelectedIndex = 0;
        }


        protected void GetJZXFY()
        {
            string sql = "";

            if (DropDownListproject.SelectedValue != "-请选择-")
            {               
                    sql = "SELECT JZXFY_ID AS JZXFYID,JZXFY_PROJECT AS JZXFYPROJECT,JZXFY_FYPC AS JZXFYFYPC,JZXFY_TRANSDATE AS JZXFYTRANSDATE," +
                                  "JZXFY_HWMS AS JZXFYHWMS,JZXFY_XS AS JZXFYXS,JZXFY_LFM AS JZXFYLFM,JZXFY_HZ AS JZXFYHZ," +
                                  "JZXFY_RZB AS JZXFYRZB,JZXFY_TJZXL AS JZXFYTJZXL,JZXFY_ZLZXL AS JZXFYZLZXL,JZXFY_XXJXS AS JZXFYXXJXS,JZXFY_ZXSYCL AS JZXFYZXSYCL " +
                                  " FROM TBTM_JZXFY " +
                                  "WHERE JZXFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and JZXFY_PROJECT='" + DropDownListproject.SelectedValue + "'";
               
            }
            else
            {
                sql = "SELECT JZXFY_ID AS JZXFYID,JZXFY_PROJECT AS JZXFYPROJECT,JZXFY_FYPC AS JZXFYFYPC,JZXFY_TRANSDATE AS JZXFYTRANSDATE," +
                        "JZXFY_HWMS AS JZXFYHWMS,JZXFY_XS AS JZXFYXS,JZXFY_LFM AS JZXFYLFM,JZXFY_HZ AS JZXFYHZ," +
                        "JZXFY_RZB AS JZXFYRZB,JZXFY_TJZXL AS JZXFYTJZXL,JZXFY_ZLZXL AS JZXFYZLZXL,JZXFY_XXJXS AS JZXFYXXJXS,JZXFY_ZXSYCL AS JZXFYZXSYCL " +
                       " FROM TBTM_JZXFY " +
                       "WHERE JZXFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "'";
            }
                        
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterJZXFY.DataSource = dt;
            RepeaterJZXFY.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel9.Visible = true;
            }
            else
            {
                Panel9.Visible = false;
            }
        }

        protected void RepeaterJZXFY_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterJZXFY.Controls)
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
            GetJZXFY();
        }

        protected void DropDownListPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetJZXFY();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_JZXFYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            string sqldetail = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterJZXFY.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterJZXFY.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_JZXFY WHERE JZXFY_ID='" + ((System.Web.UI.WebControls.Label)RepeaterJZXFY.Items[i].FindControl("LabelID")).Text + "'";
                    sqllist.Add(sql);
                    sqldetail = "DELETE FROM TBTM_JZXFYDETAIL WHERE JZXFYDETAIL_JZXFYID='" + ((System.Web.UI.WebControls.Label)RepeaterJZXFY.Items[i].FindControl("LabelID")).Text + "'";
                    sqllist.Add(sqldetail);
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
            string sql = "SELECT JZXFY_PROJECT,JZXFY_FYPC,JZXFY_TRANSDATE,JZXFY_HWMS,JZXFY_XS," +
                "JZXFY_LFM,JZXFY_HZ,JZXFY_RZB,JZXFY_TJZXL,JZXFY_ZLZXL,JZXFY_XXJXS,JZXFY_ZXSYCL " +
                "FROM TBTM_JZXFY WHERE JZXFY_YEAR='" + DropDownListYear.SelectedValue + "' ORDER BY JZXFY_TRANSDATE";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  


            //设置表头
            #region
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear.SelectedValue + "度集装箱发运统计";
            Range range;
            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 13]);
            range.MergeCells = true;
            ac.Cells[2, 1] = "序号";
            range = wksheet.get_Range(wksheet.Cells[2, 1], wksheet.Cells[3, 1]);
            range.MergeCells = true;
            ac.Cells[2, 2] = "项目名称";
            range = wksheet.get_Range(wksheet.Cells[2, 2], wksheet.Cells[3, 2]);
            range.MergeCells = true;
            ac.Cells[2, 3] = "发运批次";
            range = wksheet.get_Range(wksheet.Cells[2, 3], wksheet.Cells[3, 3]);
            range.MergeCells = true;
            ac.Cells[2, 4] = "发运日期";
            range = wksheet.get_Range(wksheet.Cells[2, 4], wksheet.Cells[3, 4]);
            range.MergeCells = true;
            ac.Cells[2, 5] = "货物描述";
            range = wksheet.get_Range(wksheet.Cells[2, 5], wksheet.Cells[3, 5]);
            range.MergeCells = true;
            ac.Cells[2, 6] = "装货量";
            range = wksheet.get_Range(wksheet.Cells[2, 6], wksheet.Cells[2, 8]);
            range.MergeCells = true;
            ac.Cells[3, 6] = "箱数";
            ac.Cells[3, 7] = "立方米";
            ac.Cells[3, 8] = "货重（T）";
            ac.Cells[2, 9] = "容重比";
            range = wksheet.get_Range(wksheet.Cells[2, 9], wksheet.Cells[3, 9]);
            range.MergeCells = true;
            ac.Cells[2, 10] = "体积装箱率";
            range = wksheet.get_Range(wksheet.Cells[2, 10], wksheet.Cells[3, 10]);
            range.MergeCells = true;
            ac.Cells[2, 11] = "重量装箱率";
            range = wksheet.get_Range(wksheet.Cells[2, 11], wksheet.Cells[3, 11]);
            range.MergeCells = true;
            ac.Cells[2, 12] = "箱型及箱数";
            range = wksheet.get_Range(wksheet.Cells[2, 12], wksheet.Cells[3, 12]);
            range.MergeCells = true;
            ac.Cells[2, 13] = "装箱所用材料";
            range = wksheet.get_Range(wksheet.Cells[2, 13], wksheet.Cells[3, 13]);
            range.MergeCells = true;

            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[3, 13]);
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            #endregion

            //用数组填充表格
            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (i + 1).ToString();
                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = dt.Rows[i][j];

                }

            }
            ac.get_Range("A4", ac.Cells[rowCount + 3, colCount]).Value2 = dataArray;
            ac.get_Range("A4", ac.Cells[rowCount + 3, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应


            sql = "SELECT a.JZXFY_PROJECT,a.JZXFY_FYPC,a.JZXFY_TRANSDATE,b.JZXFYDETAIL_GOODNAME,b.JZXFYDETAIL_SCZH," +
                "b.JZXFYDETAIL_HWZL FROM TBTM_JZXFY a INNER JOIN TBTM_JZXFYDETAIL b ON a.JZXFY_ID=b.JZXFYDETAIL_JZXFYID " +
                "WHERE a.JZXFY_YEAR='" + DropDownListYear.SelectedValue + "' ORDER BY a.JZXFY_TRANSDATE";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            wkbook.Sheets.Add(Type.Missing, wksheet, 1, Type.Missing);
            wksheet = (Worksheet)wksheet.Next;

            ac.Cells[1, 1] = "集装箱发运明细";
            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 7]);
            range.MergeCells = true;
            ac.Cells[2, 1] = "序号";
            ac.Cells[2, 2] = "项目名称";
            ac.Cells[2, 3] = "发运批次";
            ac.Cells[2, 4] = "发运日期";
            ac.Cells[2, 5] = "货物名称";
            ac.Cells[2, 6] = "生产制号";
            ac.Cells[2, 7] = "货物重量";

            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[2, 7]);
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            //用数组填充表格
            int rowCount_MX = dt.Rows.Count;

            int colCount_MX = dt.Columns.Count;

            object[,] dataArray_MX = new object[rowCount_MX, rowCount_MX + 1];

            for (int i = 0; i < rowCount_MX; i++)
            {
                dataArray_MX[i, 0] = (i + 1).ToString();
                for (int j = 0; j < colCount_MX; j++)
                {

                    dataArray_MX[i, j + 1] = dt.Rows[i][j];

                }

            }
            ac.get_Range("A3", ac.Cells[rowCount + 2, colCount]).Value2 = dataArray_MX;
            ac.get_Range("A3", ac.Cells[rowCount + 2, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "JZXFY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();

            //关闭Excel进程
            #region kill excel process

            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in procs)
            {
                int baseAdd = p.MainModule.BaseAddress.ToInt32();
                //oXL is Excel.ApplicationClass object 
                if (baseAdd == ac.Hinstance)
                {
                    p.Kill();
                    break;
                }
            }
            #endregion

            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.Flush();
        }
    }
}
