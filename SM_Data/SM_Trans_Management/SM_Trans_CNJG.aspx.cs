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
    public partial class SM_Trans_CNJG : BasicPage
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
            GetCNJG();
        }

        protected void GetYear()
        {
            string sql = "SELECT DISTINCT CNJG_YEAR  FROM TBTM_CNJG";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear.DataTextField = "CNJG_YEAR";
            DropDownListYear.DataValueField = "CNJG_YEAR";
            DropDownListYear.DataSource = dt;
            DropDownListYear.DataBind();

            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString()+"月";
                if (k < 10)
                {
                    j = "0" + k.ToString()+"月";
                }
                DropDownMonth.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            DropDownMonth.Items.Insert(0, new ListItem(" ", " "));
        }

        protected void GetCNJG()
        {
            string date1 = DropDownListYear.SelectedValue.ToString() + DropDownMonth.SelectedValue.ToString();
           
            string sql = "SELECT CNJG_ID AS CNJGID,CNJG_CC AS CNJGCC,CNJG_MDG AS CNJGMDG,CNJG_RZB AS CNJGRZB,CNJG_HTH AS CNJGHTH," +
               "CNJG_LLSTARTDATE AS CNJGLLSTARTDATE,CNJG_LLENDDATE AS CNJGLLENDDATE," +
               "CNJG_LLFDJZL AS CNJGLLFDJZL,CNJG_LLDJZL AS CNJGLLDJZL,CNJG_LLZTJ AS CNJGLLZTJ,CNJG_LLZZL AS CNJGLLZZL," +
               "CNJG_LLCC AS CNJGLLCC,CNJG_GBFDJZL AS CNJGGBFDJZL,CNJG_GBDJZL AS CNJGGBDJZL,CNJG_GBJE AS CNJGGBJE," +
               "CNJG_ZCJSZL AS CNJGZCJSZL,CNJG_ZCJSJE AS CNJGZCJSJE,CNJG_BZ AS CNJGBZ FROM TBTM_CNJG " +
               "WHERE  CNJG_LLSTARTDATE like '" + date1.TrimEnd() + "%' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterCNJG.DataSource = dt;
            RepeaterCNJG.DataBind();
            if (dt.Rows.Count == 0)
            {
                nodatapal.Visible = true;
            }
            else
            {
                nodatapal.Visible = false;
            }
        }

        protected void RepeaterCNJG_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterCNJG.Controls)
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
            GetCNJG();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_CNJGEdit.aspx?FLAG=NEW&&ID=NEW");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "js", "setTimeout(\"Add_Click()\",1000);", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "setTimeout(\"Add_Click()\",1000);", true);
            //this.ClientScript.RegisterStartupScript(GetType(), "js", "Add_Click();", true);
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterCNJG.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterCNJG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_CNJG WHERE CNJG_ID='" + ((System.Web.UI.WebControls.Label)RepeaterCNJG.Items[i].FindControl("LabelID")).Text + "'";
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
            string sql = "SELECT CNJG_CC,CNJG_MDG,CNJG_RZB,CNJG_HTH, " +
                "CNJG_LLSTARTDATE,CNJG_LLENDDATE,CNJG_LLFDJZL,CNJG_LLDJZL," +
                "CNJG_LLZTJ,CNJG_LLZZL,CNJG_LLCC,CNJG_GBFDJZL,CNJG_GBDJZL," +
                "CNJG_GBJE,CNJG_ZCJSZL,CNJG_ZCJSJE,CNJG_BZ " +
                "FROM TBTM_CNJG WHERE CNJG_YEAR='" + DropDownListYear.SelectedValue + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //设置表头
            #region  
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear.SelectedValue + "集港发运台账";
            Range range;
            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 18]);
            range.MergeCells = true;
            ac.Cells[2, 1] = "序号";
            range = wksheet.get_Range(wksheet.Cells[2, 1], wksheet.Cells[3, 1]);
            range.MergeCells = true;
            ac.Cells[2, 2] = "船次/批次";
            range = wksheet.get_Range(wksheet.Cells[2, 2], wksheet.Cells[3, 2]);
            range.MergeCells = true;
            ac.Cells[2, 3] = "目的港";
            range = wksheet.get_Range(wksheet.Cells[2, 3], wksheet.Cells[3, 3]);
            range.MergeCells = true;
            ac.Cells[3, 4] = "理论容重比（立方米/吨）";
            ac.Cells[2, 5] = "合同";
            ac.Cells[3, 5] = "合同编号";
            ac.Cells[2, 6] = "理论";
            range = wksheet.get_Range(wksheet.Cells[2, 6], wksheet.Cells[2, 12]);
            range.MergeCells = true;
            ac.Cells[3, 6] = "开始日期";
            ac.Cells[3, 7] = "结束日期";
            ac.Cells[3, 8] = "非大件重量（T）";
            ac.Cells[3, 9] = "大件重量（T）";
            ac.Cells[3, 10] = "总体积（m3）";
            ac.Cells[3, 11] = "总重量（T）";
            ac.Cells[3, 12] = "车次";
            ac.Cells[2, 13] = "过磅结算";
            range = wksheet.get_Range(wksheet.Cells[2, 13], wksheet.Cells[2, 15]);
            range.MergeCells = true;
            ac.Cells[3, 13] = "非大件重量（T）";
            ac.Cells[3, 14] = "大件重量（T)";
            ac.Cells[3, 15] = "金额（元）";
            ac.Cells[2, 16] = "中材建设（吨）";
            range = wksheet.get_Range(wksheet.Cells[2, 16], wksheet.Cells[3, 16]);
            range.MergeCells = true;
            ac.Cells[2, 17] = "中材建设部分金额（元）";
            range = wksheet.get_Range(wksheet.Cells[2, 17], wksheet.Cells[3, 17]);
            range.MergeCells = true;
            ac.Cells[2, 18] = "备注";
            range = wksheet.get_Range(wksheet.Cells[2, 18], wksheet.Cells[3, 18]);
            range.MergeCells = true;

            range = wksheet.get_Range(wksheet.Cells[1,1],wksheet.Cells[3,18]);
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            #endregion

            #region  填充数据

            //int rowIndex = 4;   // 行  
            //int colIndex = 2;   // 列 
            //// 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    ac.Cells[rowIndex, 1] = "'" + (rowIndex - 3).ToString();
            //    colIndex = 2;
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
            //        {
            //            try
            //            {
            //                // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
            //                ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
            //            }
            //            catch (System.FormatException)
            //            {
            //                ac.Cells[rowIndex, colIndex] = "";
            //            }
            //        }
            //        else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
            //        {
            //            ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
            //        }
            //        else
            //        {
            //            ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
            //        }
            //        wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //        colIndex++;
            //    }
            //    rowIndex++;
            //}

            #endregion


            //用数组填充表格
            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            object[,] dataArray = new object[rowCount, colCount+1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (i + 1).ToString();
                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j+1] = dt.Rows[i][j];

                }

            }
            ac.get_Range("A4", ac.Cells[rowCount+3, colCount]).Value2 = dataArray;
            ac.get_Range("A4", ac.Cells[rowCount + 3, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "CNJG" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
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
            //Response.End();
            Response.Flush();
   
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        } 
    }
}
