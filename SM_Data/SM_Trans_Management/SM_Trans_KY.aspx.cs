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
    public partial class SM_Trans_KY : BasicPage
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
            GetKY();
        }
        protected void GetYear()
        {
            string sql = "SELECT DISTINCT KY_YEAR  FROM TBTM_KY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear.DataTextField = "KY_YEAR";
            DropDownListYear.DataValueField = "KY_YEAR";
            DropDownListYear.DataSource = dt;
            DropDownListYear.DataBind();
        }

        protected void GetKY()
        {
            if (DropDownListYear.SelectedValue.ToString() != "")
            {
                // ((Label)RepeaterKY.FindControl("LabelYear7")).Text = DropDownListYear.SelectedValue.ToString(); 
            }
            string sql = "SELECT KY_ID AS KYID,KY_PROJECT AS KYPROJECT,KY_GOODNAME AS KYGOODNAME,KY_NUM AS KYNUM," +
                "KY_BZXS AS KYBZXS,KY_TJ AS KYTJ,KY_ZL AS KYZL,KY_YF AS KYYF,KY_YSGS AS KYYSGS," +
                "KY_TRANSDATE AS KYTRANSDATE,KY_FYR AS KYFYR,KY_BZ AS KYBZ,KY_YFJSQK AS KYYFJSQK FROM TBTM_KY " +
                "WHERE KY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterKY.DataSource = dt;
            RepeaterKY.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel7.Visible = true;
            }
            else
            {
                Panel7.Visible = false;
            }
        }

        protected void RepeaterKY_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterKY.Controls)
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
            GetKY();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_KYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterKY.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterKY.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_KY WHERE KY_ID='" + ((System.Web.UI.WebControls.Label)RepeaterKY.Items[i].FindControl("LabelID")).Text + "'";
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
            string sql = "SELECT KY_PROJECT,KY_GOODNAME,KY_NUM,KY_BZXS,KY_TJ," +
                "KY_ZL,KY_YF,KY_YSGS,KY_TRANSDATE,KY_FYR,KY_BZ,KY_YFJSQK " +
                "FROM TBTM_KY WHERE KY_YEAR='" + DropDownListYear.SelectedValue + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            
            //设置表头
            #region
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear.SelectedValue + "货物空运发运台账";
            Range range;
            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 13]);
            range.MergeCells = true;
            ac.Cells[2, 1] = "序号";
            ac.Cells[2, 2] = "项目名称";
            ac.Cells[2, 3] = "货物名称";
            ac.Cells[2, 4] = "件数";
            ac.Cells[2, 5] = "包装形式";
            ac.Cells[2, 6] = "体积（m3）";
            ac.Cells[2, 7] = "重量（KG）";
            ac.Cells[2, 8] = "运费（元）";
            ac.Cells[2, 9] = "运输公司";
            ac.Cells[2, 10] = "发运日期";
            ac.Cells[2, 11] = "发运人";
            ac.Cells[2, 12] = "备注";
            ac.Cells[2, 13] = "运费结算情况";

            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[2, 13]);
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
            ac.get_Range("A3", ac.Cells[rowCount + 2, colCount]).Value2 = dataArray;
            ac.get_Range("A3", ac.Cells[rowCount + 2, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "KY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
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
