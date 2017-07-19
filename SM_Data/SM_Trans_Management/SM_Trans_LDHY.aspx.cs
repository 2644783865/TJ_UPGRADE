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
    public partial class SM_Trans_LDHY : BasicPage
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

            GetLDHY();
        }

        protected void GetYear()
        {
            string sql = "SELECT DISTINCT LDHY_YEAR  FROM TBTM_LDHY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear.DataTextField = "LDHY_YEAR";
            DropDownListYear.DataValueField = "LDHY_YEAR";
            DropDownListYear.DataSource = dt;
            DropDownListYear.DataBind();
        }

        protected void GetProject()
        {
            string sql = "SELECT DISTINCT LDHY_PROJECT  FROM TBTM_LDHY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListproject.DataTextField = "LDHY_PROJECT";
            DropDownListproject.DataValueField = "LDHY_PROJECT";
            DropDownListproject.DataSource = dt;
            DropDownListproject.DataBind();
            DropDownListproject.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            DropDownListproject.SelectedIndex = 0;
        }

        protected void GetEngeering()
        {
            string sql = "SELECT DISTINCT LDHY_GOODNAME  FROM TBTM_LDHY where LDHY_PROJECT='" + DropDownListproject.SelectedValue + "' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListengeering.DataTextField = "LDHY_GOODNAME";
            DropDownListengeering.DataValueField = "LDHY_GOODNAME";
            DropDownListengeering.DataSource = dt;
            DropDownListengeering.DataBind();
            DropDownListengeering.Items.Insert(0, new ListItem(" ", " "));
            DropDownListengeering.SelectedIndex = 0;
        }



        protected void GetLDHY()
        {
            string sql = "";

            if (DropDownListproject.SelectedValue != "-请选择-")
            {
                if (DropDownListengeering.SelectedValue.Trim() != "")
                {
                    sql = "SELECT LDHY_ID AS LDHYID,LDHY_PROJECT AS LDHYPROJECT,LDHY_GOODNAME AS LDHYGOODNAME,LDHY_NUM AS LDHYNUM," +
                 "LDHY_BZXS AS LDHYBZXS,LDHY_TJ AS LDHYTJ,LDHY_ZL AS LDHYZL,LDHY_YFYF AS LDHYYFYF," +
                 "LDHY_YSYF AS LDHYYSYF,LDHY_YSFS AS LDHYYSFS,LDHY_TRANSDATE AS LDHYTRANSDATE,LDHY_CZR AS LDHYCZR,LDHY_BZ AS LDHYBZ,LDHY_YFJSQK AS LDHYYFJSQK FROM TBTM_LDHY " +
                 "WHERE LDHY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and LDHY_PROJECT='" + DropDownListproject.SelectedValue + "' and LDHY_GOODNAME='" + DropDownListengeering.SelectedValue + "'";
                
                }
                else
                {
                    sql = "SELECT LDHY_ID AS LDHYID,LDHY_PROJECT AS LDHYPROJECT,LDHY_GOODNAME AS LDHYGOODNAME,LDHY_NUM AS LDHYNUM," +
                    "LDHY_BZXS AS LDHYBZXS,LDHY_TJ AS LDHYTJ,LDHY_ZL AS LDHYZL,LDHY_YFYF AS LDHYYFYF," +
                    "LDHY_YSYF AS LDHYYSYF,LDHY_YSFS AS LDHYYSFS,LDHY_TRANSDATE AS LDHYTRANSDATE,LDHY_CZR AS LDHYCZR,LDHY_BZ AS LDHYBZ,LDHY_YFJSQK AS LDHYYFJSQK FROM TBTM_LDHY " +
                     "WHERE LDHY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and LDHY_PROJECT='" + DropDownListproject.SelectedValue + "'";

                }
            }
            else
            {
                sql = "SELECT LDHY_ID AS LDHYID,LDHY_PROJECT AS LDHYPROJECT,LDHY_GOODNAME AS LDHYGOODNAME,LDHY_NUM AS LDHYNUM," +
                "LDHY_BZXS AS LDHYBZXS,LDHY_TJ AS LDHYTJ,LDHY_ZL AS LDHYZL,LDHY_YFYF AS LDHYYFYF," +
                "LDHY_YSYF AS LDHYYSYF,LDHY_YSFS AS LDHYYSFS,LDHY_TRANSDATE AS LDHYTRANSDATE,LDHY_CZR AS LDHYCZR,LDHY_BZ AS LDHYBZ,LDHY_YFJSQK AS LDHYYFJSQK FROM TBTM_LDHY " +
                "WHERE LDHY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "'";
            }
                                    
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterLDHY.DataSource = dt;
            RepeaterLDHY.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel8.Visible = true;
            }
            else
            {
                Panel8.Visible = false;
            }
        }

        protected void RepeaterLDHY_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterLDHY.Controls)
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
            GetLDHY();
        }

        protected void DropDownListPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEngeering();
            GetLDHY();
        }

        protected void DropDownListENG_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetLDHY();
        }


        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_LDHYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterLDHY.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterLDHY.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_LDHY WHERE LDHY_ID='" + ((System.Web.UI.WebControls.Label)RepeaterLDHY.Items[i].FindControl("LabelID")).Text + "'";
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
            string sql = "SELECT LDHY_PROJECT,LDHY_GOODNAME,LDHY_NUM,LDHY_BZXS,LDHY_TJ," +
                "LDHY_ZL,LDHY_YFYF,LDHY_YSYF,LDHY_YSFS,LDHY_TRANSDATE,LDHY_CZR,LDHY_BZ,LDHY_YFJSQK " +
                "FROM TBTM_LDHY WHERE LDHY_YEAR='" + DropDownListYear.SelectedValue + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            // 设置单元格格式  
            //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).NumberFormat = "@";
            //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).Font.Size = 12;

            //设置表头
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear.SelectedValue + "零担货运记录台账";
            Range range;
            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 14]);
            range.MergeCells = true;
            ac.Cells[2, 1] = "序号";
            ac.Cells[2, 2] = "项目名称";
            ac.Cells[2, 3] = "货物名称";
            ac.Cells[2, 4] = "件数";
            ac.Cells[2, 5] = "包装形式";
            ac.Cells[2, 6] = "体积（m3）";
            ac.Cells[2, 7] = "重量（KG）";
            ac.Cells[2, 8] = "应付运费（元）";
            ac.Cells[2, 9] = "应收运费（元）";
            ac.Cells[2, 10] = "运输方式";
            ac.Cells[2, 11] = "发运日期";
            ac.Cells[2, 12] = "操作人";
            ac.Cells[2, 13] = "备注";
            ac.Cells[2, 14] = "运费结算情况";

            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[2, 14]);
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

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

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "LDHY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
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
