using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Trans_Manage : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initial();
            }

            CheckUser(ControlFinder);
        }

        protected void Initial()
        {
            TabContainer1.ActiveTab = Tab2;
            GetYear2();
            GetCNJG();         
            GetYear4();
            GetGNFY();
            GetYear5();
            GetYZZT();
            //GetYear6();
            //GetDDYS();
            GetYear7();
            GetKY();
            GetYear8();
            GetLDHY();
            GetYear9();
            GetJZXFY();
            GetYear10();
            GetJZXFYMX();

            if (Request.QueryString["TAB"] != null)
            {
                switch (Request.QueryString["TAB"].ToString())
                {
                    //case "1": TabContainer1.ActiveTab = Tab1; break;
                    case "2": TabContainer1.ActiveTab = Tab2; break;                
                    case "4": TabContainer1.ActiveTab = Tab4; break;
                    case "5": TabContainer1.ActiveTab = Tab5; break;
                    //case "6": TabContainer1.ActiveTab = Tab6; break;
                    case "7": TabContainer1.ActiveTab = Tab7; break;
                    case "8": TabContainer1.ActiveTab = Tab8; break;
                    case "9": TabContainer1.ActiveTab = Tab9; break;
                    case "10": TabContainer1.ActiveTab = Tab10; break;
                    default: break;
                }
            }

          
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            //if (TabContainer1.ActiveTab == Tab1)
            //{
            //}
            if (TabContainer1.ActiveTab == Tab2)
            {
                GetYear2();
                GetCNJG();
            }
           
            if (TabContainer1.ActiveTab == Tab4)
            {
                GetYear4();
                GetGNFY();
            }
            if (TabContainer1.ActiveTab == Tab5)
            {

            }
            //if (TabContainer1.ActiveTab == Tab6)
            //{
            //    GetYear6();
            //    GetDDYS();
            //}
            if (TabContainer1.ActiveTab == Tab7)
            {
                GetYear7();
                GetKY();
            }
            if (TabContainer1.ActiveTab == Tab8)
            {
                GetYear8();
                GetLDHY();
            }
            if (TabContainer1.ActiveTab == Tab9)
            {
                GetYear9();
                GetJZXFY();
            }
            if (TabContainer1.ActiveTab == Tab10)
            {
                GetYear10();
                GetJZXFYMX();
            }

            CheckUser(ControlFinder);
        }


        protected void GetYear2()
        {
            string sql = "SELECT DISTINCT CNJG_YEAR  FROM TBTM_CNJG";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear2.DataTextField = "CNJG_YEAR";
            DropDownListYear2.DataValueField = "CNJG_YEAR";
            DropDownListYear2.DataSource = dt;
            DropDownListYear2.DataBind();
        }

        protected void GetCNJG()
        {
             string sql = "SELECT CNJG_ID AS CNJGID,CNJG_CC AS CNJGCC,CNJG_MDG AS CNJGMDG,CNJG_RZB AS CNJGRZB,CNJG_HTH AS CNJGHTH," +
                "CNJG_LLSTARTDATE AS CNJGLLSTARTDATE,CNJG_LLENDDATE AS CNJGLLENDDATE," +
                "CNJG_LLFDJZL AS CNJGLLFDJZL,CNJG_LLDJZL AS CNJGLLDJZL,CNJG_LLZTJ AS CNJGLLZTJ,CNJG_LLZZL AS CNJGLLZZL," +
                "CNJG_LLCC AS CNJGLLCC,CNJG_GBFDJZL AS CNJGGBFDJZL,CNJG_GBDJZL AS CNJGGBDJZL,CNJG_GBJE AS CNJGGBJE," +
                "CNJG_ZCJSZL AS CNJGZCJSZL,CNJG_ZCJSJE AS CNJGZCJSJE,CNJG_BZ AS CNJGBZ FROM TBTM_CNJG " +
                "WHERE CNJG_YEAR='" + DropDownListYear2.SelectedValue.ToString() + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterCNJG.DataSource = dt;
            RepeaterCNJG.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
            }
        }

        protected void RepeaterCNJG_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterCNJG.Controls)
            {
                if (item.ItemType == ListItemType.Header)
                {
                    ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear2")).Text = DropDownListYear2.SelectedValue;
                    break;
                }
            } 
        }

        protected void DropDownListYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCNJG();
        }

        protected void Add2_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_CNJGEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete2_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterCNJG.Items.Count; i++)
            { 
                if(((System.Web.UI.WebControls.CheckBox)RepeaterCNJG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_CNJG WHERE CNJG_ID='" + ((System.Web.UI.WebControls.Label)RepeaterCNJG.Items[i].FindControl("LabelID")).Text + "'";
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count > 0)
            {
                DBCallCommon.ExecuteTrans(sqllist);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=2");
        }

        protected void Export2_Click(object sender, EventArgs e)
        {
            string sql = "SELECT CNJG_CC,CNJG_MDG,CNJG_RZB,CNJG_HTH, " +
                "CNJG_LLSTARTDATE,CNJG_LLENDDATE,CNJG_LLFDJZL,CNJG_LLDJZL," +
                "CNJG_LLZTJ,CNJG_LLZZL,CNJG_LLCC,CNJG_GBFDJZL,CNJG_GBDJZL," +
                "CNJG_GBJE,CNJG_ZCJSZL,CNJG_ZCJSJE,CNJG_BZ " +
                "FROM TBTM_CNJG WHERE CNJG_YEAR='" + DropDownListYear2.SelectedValue + "'";
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
            ac.Cells[1, 1] = "中材重机储运部"+DropDownListYear2.SelectedValue + "年集港发运台账";
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

            int rowIndex = 4;   // 行  
            int colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 3).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    colIndex++;
                }
                rowIndex++;
            }
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "CNJG" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.End();        
        }
                     

        //protected void GetSHC()
        //{
        //    if (DropDownListYear3.SelectedValue.ToString() != "")
        //    {
        //        // ((Label)RepeaterCNJG.FindControl("LabelYear3")).Text = DropDownListYear3.SelectedValue.ToString(); 
        //    }
        //    string sql = "SELECT SHC_ID AS SHCID,SHC_PROJECT AS SHCPROJECT,SHC_FYXS AS SHCFYXS,SHC_FYPC AS SHCFYPC,SHC_HWMS AS SHCHWMS," +
        //        "SHC_JS AS SHCJS,SHC_TJ AS SHCTJ," +
        //        "SHC_MZ AS SHCMZ,SHC_ZYG AS SHCZYG,SHC_JGWB AS SHCJGWB,SHC_ZC AS SHCZC," +
        //        "SHC_HY AS SHCHY,SHC_CM AS SHCCM," +
        //        "SHC_BZ AS SHCBZ FROM TBTM_SHC " +
        //        "WHERE SHC_YEAR='" + DropDownListYear3.SelectedValue.ToString() + "'";
        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    RepeaterSHC.DataSource = dt;
        //    RepeaterSHC.DataBind();
        //    if (dt.Rows.Count == 0)
        //    {
        //        Panel3.Visible = true;
        //    }
        //    else
        //    {
        //        Panel3.Visible = false;
        //    }
        //}
        
        //protected void RepeaterSHC_ItemDataBound(object sender, EventArgs e)
        //{
        //    foreach (RepeaterItem item in RepeaterSHC.Controls)
        //    {
        //        if (item.ItemType == ListItemType.Header)
        //        {
        //            ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear3")).Text = DropDownListYear3.SelectedValue;
        //            break;
        //        }
        //    }
        //}

        //protected void DropDownListYear3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetSHC();
        //}

        //protected void Add3_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("SM_Trans_SHCEdit.aspx?FLAG=NEW&&ID=NEW");
        //}

       
        protected void GetYear4()
        {
            string sql = "SELECT DISTINCT GNFY_YEAR  FROM TBTM_GNFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear4.DataTextField = "GNFY_YEAR";
            DropDownListYear4.DataValueField = "GNFY_YEAR";
            DropDownListYear4.DataSource = dt;
            DropDownListYear4.DataBind();
        }

        protected void GetGNFY()
        {
            if (DropDownListYear4.SelectedValue.ToString() != "")
            {
                // ((Label)RepeaterGNFY.FindControl("LabelYear4")).Text = DropDownListYear4.SelectedValue.ToString(); 
            }
            string sql = "SELECT GNFY_ID AS GNFYID,GNFY_PROJECT AS GNFYPROJECT,GNFY_HTBH AS GNFYHTBH,GNFY_HTJE AS GNFYHTJE,GNFY_RZB AS GNFYRZB," +
                "GNFY_LLSTARTDATE AS GNFYLLSTARTDATE,GNFY_LLENDDATE AS GNFYLLENDDATE,GNFY_LLFDJZL AS GNFYLLFDJZL,GNFY_LLDJZL AS GNFYLLDJZL," +
                "GNFY_LLZTJ AS GNFYLLZTJ,GNFY_LLZZL AS GNFYLLZZL,GNFY_LLCC AS GNFYLLCC,GNFY_GBFDJZL AS GNFYGBFDJZL,GNFY_GBDJZL AS GNFYGBDJZL," +
                "GNFY_ZZJSJE AS GNFYZZJSJE,GNFY_ZCJSZL AS GNFYZCJSZL,GNFY_ZCJSJE AS GNFYZCJSJE,GNFY_BZ AS GNFYBZ FROM TBTM_GNFY " +
                "WHERE GNFY_YEAR='" + DropDownListYear4.SelectedValue.ToString() + "'";
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

        protected void RepeaterGNFY_ItemDataBound(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterGNFY.Controls)
            {
                if (item.ItemType == ListItemType.Header)
                {
                    ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear4")).Text = DropDownListYear4.SelectedValue;
                    break;
                }
            }
        }

        protected void DropDownListYear4_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGNFY();
        }

        protected void Add4_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_GNFYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete4_Click(object sender, EventArgs e)
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
            Response.Redirect("SM_Trans_Manage.aspx?TAB=4");
        }

        protected void Export4_Click(object sender, EventArgs e)
        {
            string sql = "SELECT GNFY_PROJECT,GNFY_HTBH,GNFY_HTJE,GNFY_RZB,GNFY_LLSTARTDATE," +
                "GNFY_LLENDDATE,GNFY_LLFDJZL,GNFY_LLDJZL,GNFY_LLZTJ,GNFY_LLZZL,GNFY_LLCC," +
                "GNFY_GBFDJZL,GNFY_GBDJZL,GNFY_ZZJSJE,GNFY_ZCJSZL,GNFY_ZCJSJE,GNFY_BZ " +
                "FROM TBTM_GNFY WHERE GNFY_YEAR='" + DropDownListYear4.SelectedValue + "'";
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
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear2.SelectedValue + "年国内设备发运台账";
            Range range;
            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 18]);
            range.MergeCells = true;
            ac.Cells[2, 1] = "序号";
            range = wksheet.get_Range(wksheet.Cells[2, 1], wksheet.Cells[3, 1]);
            range.MergeCells = true;
            ac.Cells[2, 2] = "项目名称";
            range = wksheet.get_Range(wksheet.Cells[2, 2], wksheet.Cells[3, 2]);
            range.MergeCells = true;
            ac.Cells[2, 3] = "运输合同";
            range = wksheet.get_Range(wksheet.Cells[2, 3], wksheet.Cells[2, 4]);
            range.MergeCells = true;
            ac.Cells[3, 3] = "合同编号";
            ac.Cells[3, 4] = "合同金额";
            ac.Cells[2, 5] = "理论容重比（立方米/吨）";
            range = wksheet.get_Range(wksheet.Cells[2, 5], wksheet.Cells[3, 5]);
            range.MergeCells = true;
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
            range = wksheet.get_Range(wksheet.Cells[2, 13], wksheet.Cells[2, 14]);
            range.MergeCells = true;
            ac.Cells[3, 13] = "非大件重量（T）";
            ac.Cells[3, 14] = "大件重量（T）";
            ac.Cells[2, 15] = "最终结算金额";
            range = wksheet.get_Range(wksheet.Cells[2, 15], wksheet.Cells[3, 15]);
            range.MergeCells = true;
            ac.Cells[2, 16] = "中材建设（吨）";
            range = wksheet.get_Range(wksheet.Cells[2, 16], wksheet.Cells[3, 16]);
            range.MergeCells = true;
            ac.Cells[2, 17] = "中材建设部分金额";
            range = wksheet.get_Range(wksheet.Cells[2, 17], wksheet.Cells[3, 17]);
            range.MergeCells = true;
            ac.Cells[2, 18] = "备注";
            range = wksheet.get_Range(wksheet.Cells[2, 18], wksheet.Cells[3, 18]);
            range.MergeCells = true;

            int rowIndex = 4;   // 行  
            int colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 3).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    colIndex++;
                }
                rowIndex++;
            }
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "GNFY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.End(); 
        }


        protected void GetYear5()
        {
            string sql = "SELECT DISTINCT KHZT_YEAR FROM TBTM_KHZT";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear5.DataTextField = "KHZT_YEAR";
            DropDownListYear5.DataValueField = "KHZT_YEAR";
            DropDownListYear5.DataSource = dt;
            DropDownListYear5.DataBind();
        }

        protected void GetYZZT()
        {
            if (DropDownListYear5.SelectedValue.ToString() != "")
            {
                // ((Label)RepeaterDDYS.FindControl("LabelYear5")).Text = DropDownListYear6.SelectedValue.ToString(); 
            }
            string sql = "SELECT KHZT_ID,KHZT_DATE,KHZT_NAME,KHZT_NUM,KHZT_LFM,KHZT_BZ FROM TBTM_KHZT " +
                "WHERE KHZT_YEAR='" + DropDownListYear5.SelectedValue.ToString() + "'";
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
                    ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear5")).Text = DropDownListYear5.SelectedValue;
                    break;
                }
            }
        }

        protected void DropDownListYear5_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetYZZT();
        }

        protected void Add5_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_KHZTEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete5_Click(object sender, EventArgs e)
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
            Response.Redirect("SM_Trans_Manage.aspx?TAB=5");
        }

        protected void Export5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT KHZT_DATE,KHZT_NAME,KHZT_NUM,KHZT_LFM,KHZT_BZ" +
                         " FROM TBTM_KHZT WHERE KHZT_YEAR='" + DropDownListYear5.SelectedValue + "'";
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
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear5.SelectedValue + "业主自提运输台账";
            Range range;
            range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 6]);
            range.MergeCells = true;
            ac.Cells[2, 1] = "序号";
            range = wksheet.get_Range(wksheet.Cells[2, 1], wksheet.Cells[3, 1]);
            range.MergeCells = true;
            ac.Cells[2, 2] = "日期";
            range = wksheet.get_Range(wksheet.Cells[2, 2], wksheet.Cells[3, 2]);
            range.MergeCells = true;
            ac.Cells[2, 3] = "货物名称";
            range = wksheet.get_Range(wksheet.Cells[2, 3], wksheet.Cells[3, 3]);
            range.MergeCells = true;
            ac.Cells[2, 4] = "数量（吨）";
            range = wksheet.get_Range(wksheet.Cells[2, 4], wksheet.Cells[3, 4]);
            range.MergeCells = true;
            ac.Cells[2, 5] = "体积（立方米）";
            range = wksheet.get_Range(wksheet.Cells[2, 5], wksheet.Cells[3, 5]);
            range.MergeCells = true;
            ac.Cells[2, 6] = "备注（立方米）";
            range = wksheet.get_Range(wksheet.Cells[2, 6], wksheet.Cells[3, 6]);
            range.MergeCells = true;   
   

            int rowIndex = 4;   // 行  
            int colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 3).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    colIndex++;
                }
                rowIndex++;
            }
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "KHZY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.End();
        }
        


        //protected void GetYear6()
        //{
        //    string sql = "SELECT DISTINCT DDYS_YEAR  FROM TBTM_DDYS";
        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    DropDownListYear6.DataTextField = "DDYS_YEAR";
        //    DropDownListYear6.DataValueField = "DDYS_YEAR";
        //    DropDownListYear6.DataSource = dt;
        //    DropDownListYear6.DataBind();
        //}

        //protected void GetDDYS()
        //{
        //    if (DropDownListYear6.SelectedValue.ToString() != "")
        //    {
        //        // ((Label)RepeaterDDYS.FindControl("LabelYear6")).Text = DropDownListYear6.SelectedValue.ToString(); 
        //    }
        //    string sql = "SELECT DDYS_ID AS DDYSID,DDYS_TRANSDATE AS DDYSTRANSDATE,DDYS_GOODNAME AS DDYSGOODNAME,DDYS_UNIT AS DDYSUNIT,DDYS_NUM AS DDYSNUM," +
        //        "DDYS_JSDJ AS DDYSJSDJ,DDYS_JSZJ AS DDYSJSZJ,DDYS_CH AS DDYSCH,DDYS_QYD AS DDYSQYD," +
        //        "DDYS_MDD AS DDYSMDD,DDYS_BZ AS DDYSBZ FROM TBTM_DDYS " +
        //        "WHERE DDYS_YEAR='" + DropDownListYear6.SelectedValue.ToString() + "'";
        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    RepeaterDDYS.DataSource = dt;
        //    RepeaterDDYS.DataBind();
        //    if (dt.Rows.Count == 0)
        //    {
        //        Panel6.Visible = true;
        //    }
        //    else
        //    {
        //        Panel6.Visible = false;
        //    }
        //}

        //protected void RepeaterDDYS_ItemDataBound(object sender, EventArgs e)
        //{
        //    foreach (RepeaterItem item in RepeaterDDYS.Controls)
        //    {
        //        if (item.ItemType == ListItemType.Header)
        //        {
        //            ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear6")).Text = DropDownListYear6.SelectedValue;
        //            break;
        //        }
        //    }
        //}

        //protected void DropDownListYear6_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetDDYS();
        //}

        //protected void Add6_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("SM_Trans_DDYSEdit.aspx?FLAG=NEW&&ID=NEW");
        //}

        //protected void Delete6_Click(object sender, EventArgs e)
        //{
        //    string sql = "";
        //    List<string> sqllist = new List<string>();
        //    for (int i = 0; i < RepeaterDDYS.Items.Count; i++)
        //    {
        //        if (((System.Web.UI.WebControls.CheckBox)RepeaterDDYS.Items[i].FindControl("CheckBox1")).Checked == true)
        //        {
        //            sql = "DELETE FROM TBTM_DDYS WHERE DDYS_ID='" + ((System.Web.UI.WebControls.Label)RepeaterDDYS.Items[i].FindControl("LabelID")).Text + "'";
        //            sqllist.Add(sql);
        //        }
        //    }
        //    if (sqllist.Count > 0)
        //    {
        //        DBCallCommon.ExecuteTrans(sqllist);
        //    }
        //    Response.Redirect("SM_Trans_Manage.aspx?TAB=6");
        //}

        //protected void Export6_Click(object sender, EventArgs e)
        //{
        //    string sql = "SELECT DDYS_TRANSDATE,DDYS_GOODNAME,DDYS_UNIT,DDYS_NUM," +
        //        "DDYS_JSDJ,DDYS_JSZJ,DDYS_CH,DDYS_QYD,DDYS_MDD,DDYS_BZ " +
        //            "FROM TBTM_DDYS WHERE DDYS_YEAR='" + DropDownListYear6.SelectedValue + "'";
        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

        //    ApplicationClass ac = new ApplicationClass();
        //    ac.Visible = false;     // Excel不显示  
        //    Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
        //    Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

        //    ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

        //    // 设置单元格格式  
        //    //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).NumberFormat = "@";
        //    //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).Font.Size = 12;

        //    //设置表头
        //    ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear2.SelectedValue + "年度倒短运输发运统计";
        //    Range range;
        //    range = wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, 11]);
        //    range.MergeCells = true;
        //    ac.Cells[2, 1] = "序号";
        //    range = wksheet.get_Range(wksheet.Cells[2, 1], wksheet.Cells[3, 1]);
        //    range.MergeCells = true;
        //    ac.Cells[2, 2] = "日期";
        //    range = wksheet.get_Range(wksheet.Cells[2, 2], wksheet.Cells[3, 2]);
        //    range.MergeCells = true;
        //    ac.Cells[2, 3] = "货物名称";
        //    range = wksheet.get_Range(wksheet.Cells[2, 3], wksheet.Cells[3, 3]);
        //    range.MergeCells = true;
        //    ac.Cells[2, 4] = "单位";
        //    range = wksheet.get_Range(wksheet.Cells[2, 4], wksheet.Cells[3, 4]);
        //    range.MergeCells = true;
        //    ac.Cells[2, 5] = "数量";
        //    range = wksheet.get_Range(wksheet.Cells[2, 5], wksheet.Cells[3, 5]);
        //    range.MergeCells = true;
        //    ac.Cells[2, 6] = "结算金额（元）";
        //    range = wksheet.get_Range(wksheet.Cells[2, 6], wksheet.Cells[2, 7]);
        //    range.MergeCells = true;
        //    ac.Cells[3, 6] = "单价";
        //    ac.Cells[3, 7] = "总价";
        //    ac.Cells[2, 8] = "运输信息";
        //    range = wksheet.get_Range(wksheet.Cells[2, 8], wksheet.Cells[2, 10]);
        //    range.MergeCells = true;
        //    ac.Cells[3, 8] = "车号";
        //    ac.Cells[3, 9] = "启运地";
        //    ac.Cells[3, 10] = "目的地";            
        //    ac.Cells[2, 11] = "备注";
        //    range = wksheet.get_Range(wksheet.Cells[2, 11], wksheet.Cells[3, 11]);
        //    range.MergeCells = true;

        //    int rowIndex = 4;   // 行  
        //    int colIndex = 2;   // 列 
        //    // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        ac.Cells[rowIndex, 1] = "'" + (rowIndex - 3).ToString();
        //        colIndex = 2;
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
        //            {
        //                try
        //                {
        //                    // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
        //                    ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
        //                }
        //                catch (System.FormatException)
        //                {
        //                    ac.Cells[rowIndex, colIndex] = "";
        //                }
        //            }
        //            else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
        //            {
        //                ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
        //            }
        //            else
        //            {
        //                ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
        //            }
        //            wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //            colIndex++;
        //        }
        //        rowIndex++;
        //    }
        //    string filename = Server.MapPath("/SM_Data/ExportFile/" + "DDYS" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
        //    wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
        //    ac.Quit();
        //    wkbook = null;
        //    ac = null;
        //    GC.Collect();    // 强制垃圾回收  
        //    //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
        //    Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
        //    Response.WriteFile(filename); // 把文件流发送到客户端 
        //    Response.End();         
        //}


        protected void GetYear7()
        {
            string sql = "SELECT DISTINCT KY_YEAR  FROM TBTM_KY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear7.DataTextField = "KY_YEAR";
            DropDownListYear7.DataValueField = "KY_YEAR";
            DropDownListYear7.DataSource = dt;
            DropDownListYear7.DataBind();
        }

        protected void GetKY()
        {
            if (DropDownListYear7.SelectedValue.ToString() != "")
            {
                // ((Label)RepeaterKY.FindControl("LabelYear7")).Text = DropDownListYear7.SelectedValue.ToString(); 
            }
            string sql = "SELECT KY_ID AS KYID,KY_PROJECT AS KYPROJECT,KY_GOODNAME AS KYGOODNAME,KY_NUM AS KYNUM," +
                "KY_BZXS AS KYBZXS,KY_TJ AS KYTJ,KY_ZL AS KYZL,KY_YF AS KYYF,KY_YSGS AS KYYSGS," +
                "KY_TRANSDATE AS KYTRANSDATE,KY_FYR AS KYFYR,KY_BZ AS KYBZ,KY_YFJSQK AS KYYFJSQK FROM TBTM_KY " +
                "WHERE KY_YEAR='" + DropDownListYear7.SelectedValue.ToString() + "'";
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
                    ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear7")).Text = DropDownListYear7.SelectedValue;
                    break;
                }
            }
        }

        protected void DropDownListYear7_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetKY();
        }

        protected void Add7_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_KYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete7_Click(object sender, EventArgs e)
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
            Response.Redirect("SM_Trans_Manage.aspx?TAB=7");
        }

        protected void Export7_Click(object sender, EventArgs e)
        {
            string sql = "SELECT KY_PROJECT,KY_GOODNAME,KY_NUM,KY_BZXS,KY_TJ," +
                "KY_ZL,KY_YF,KY_YSGS,KY_TRANSDATE,KY_FYR,KY_BZ,KY_YFJSQK " +
                "FROM TBTM_KY WHERE KY_YEAR='" + DropDownListYear7.SelectedValue + "'";
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
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear7.SelectedValue + "年货物空运发运台账";
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

            int rowIndex = 3;   // 行  
            int colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 2).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    colIndex++;
                }
                rowIndex++;
            }
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "KY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.End(); 
        }

        protected void GetYear8()
        {
            string sql = "SELECT DISTINCT LDHY_YEAR  FROM TBTM_LDHY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear8.DataTextField = "LDHY_YEAR";
            DropDownListYear8.DataValueField = "LDHY_YEAR";
            DropDownListYear8.DataSource = dt;
            DropDownListYear8.DataBind();
        }

        protected void GetLDHY()
        {
            if (DropDownListYear8.SelectedValue.ToString() != "")
            {
                // ((Label)RepeaterLDHY.FindControl("LabelYear8")).Text = DropDownListYear8.SelectedValue.ToString(); 
            }
            string sql = "SELECT LDHY_ID AS LDHYID,LDHY_PROJECT AS LDHYPROJECT,LDHY_GOODNAME AS LDHYGOODNAME,LDHY_NUM AS LDHYNUM," +
                "LDHY_BZXS AS LDHYBZXS,LDHY_TJ AS LDHYTJ,LDHY_ZL AS LDHYZL,LDHY_YFYF AS LDHYYFYF," +
                "LDHY_YSYF AS LDHYYSYF,LDHY_YSFS AS LDHYYSFS,LDHY_TRANSDATE AS LDHYTRANSDATE,LDHY_CZR AS LDHYCZR,LDHY_BZ AS LDHYBZ,LDHY_YFJSQK AS LDHYYFJSQK FROM TBTM_LDHY " +
                "WHERE LDHY_YEAR='" + DropDownListYear8.SelectedValue.ToString() + "'";
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
                    ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear8")).Text = DropDownListYear8.SelectedValue;
                    break;
                }
            }
        }

        protected void DropDownListYear8_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLDHY();
        }

        protected void Add8_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_LDHYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete8_Click(object sender, EventArgs e)
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
            Response.Redirect("SM_Trans_Manage.aspx?TAB=8");
        }

        protected void Export8_Click(object sender, EventArgs e)
        {
            string sql = "SELECT LDHY_PROJECT,LDHY_GOODNAME,LDHY_NUM,LDHY_BZXS,LDHY_TJ," +
                "LDHY_ZL,LDHY_YFYF,LDHY_YSYF,LDHY_YSFS,LDHY_TRANSDATE,LDHY_CZR,LDHY_BZ,LDHY_YFJSQK " +
                "FROM TBTM_LDHY WHERE LDHY_YEAR='" + DropDownListYear8.SelectedValue + "'";
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
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear7.SelectedValue + "年零担货运记录台账";
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

            int rowIndex = 3;   // 行  
            int colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 2).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    colIndex++;
                }
                rowIndex++;
            }
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "LDHY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.End(); 
        }

        
        protected void GetYear9()
        {
            string sql = "SELECT DISTINCT JZXFY_YEAR  FROM TBTM_JZXFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear9.DataTextField = "JZXFY_YEAR";
            DropDownListYear9.DataValueField = "JZXFY_YEAR";
            DropDownListYear9.DataSource = dt;
            DropDownListYear9.DataBind();
        }

        protected void GetJZXFY()
        {
            if (DropDownListYear9.SelectedValue.ToString() != "")
            {
                // ((Label)RepeaterJZXFY.FindControl("LabelYear9")).Text = DropDownListYear9.SelectedValue.ToString(); 
            }
            string sql = "SELECT JZXFY_ID AS JZXFYID,JZXFY_PROJECT AS JZXFYPROJECT,JZXFY_FYPC AS JZXFYFYPC,JZXFY_TRANSDATE AS JZXFYTRANSDATE," +
                "JZXFY_HWMS AS JZXFYHWMS,JZXFY_XS AS JZXFYXS,JZXFY_LFM AS JZXFYLFM,JZXFY_HZ AS JZXFYHZ," +
                "JZXFY_RZB AS JZXFYRZB,JZXFY_TJZXL AS JZXFYTJZXL,JZXFY_ZLZXL AS JZXFYZLZXL,JZXFY_XXJXS AS JZXFYXXJXS,JZXFY_ZXSYCL AS JZXFYZXSYCL " +
                " FROM TBTM_JZXFY " +
                "WHERE JZXFY_YEAR='" + DropDownListYear9.SelectedValue.ToString() + "'";
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
                    ((System.Web.UI.WebControls.Label)item.FindControl("LabelYear9")).Text = DropDownListYear9.SelectedValue;
                    break;
                }
            }
        }

        protected void DropDownListYear9_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetJZXFY();
        }

        protected void Add9_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_JZXFYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Delete9_Click(object sender, EventArgs e)
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
            Response.Redirect("SM_Trans_Manage.aspx?TAB=9");
        }

        protected void Export9_Click(object sender, EventArgs e)
        {
            string sql = "SELECT JZXFY_PROJECT,JZXFY_FYPC,JZXFY_TRANSDATE,JZXFY_HWMS,JZXFY_XS," +
                "JZXFY_LFM,JZXFY_HZ,JZXFY_RZB,JZXFY_TJZXL,JZXFY_ZLZXL,JZXFY_XXJXS,JZXFY_ZXSYCL " +
                "FROM TBTM_JZXFY WHERE JZXFY_YEAR='" + DropDownListYear9.SelectedValue + "' ORDER BY JZXFY_TRANSDATE";
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
            ac.Cells[1, 1] = "中材重机储运部" + DropDownListYear9.SelectedValue + "年度集装箱发运统计";
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

            int rowIndex = 4;   // 行  
            int colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 3).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    colIndex++;
                }
                rowIndex++;
            }

            sql = "SELECT a.JZXFY_PROJECT,a.JZXFY_FYPC,a.JZXFY_TRANSDATE,b.JZXFYDETAIL_GOODNAME,b.JZXFYDETAIL_SCZH," +
                "b.JZXFYDETAIL_HWZL FROM TBTM_JZXFY a INNER JOIN TBTM_JZXFYDETAIL b ON a.JZXFY_ID=b.JZXFYDETAIL_JZXFYID " +
                "WHERE a.JZXFY_YEAR='" + DropDownListYear9.SelectedValue + "' ORDER BY a.JZXFY_TRANSDATE";
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

            rowIndex = 3;   // 行  
            colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 2).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    colIndex++;
                }
                rowIndex++;
            }
           
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "JZXFY" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.End(); 
        }

        protected void GetYear10()
        {
            //集装箱发运明细按照集装箱发运总信息查询
            string sql = "SELECT DISTINCT JZXFY_YEAR  FROM TBTM_JZXFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear10.DataTextField = "JZXFY_YEAR";
            DropDownListYear10.DataValueField = "JZXFY_YEAR";
            DropDownListYear10.DataSource = dt;
            DropDownListYear10.DataBind();
        }

        protected void GetJZXFYMX()
        {
            if (DropDownListYear10.SelectedValue.ToString() != "")
            {
                // ((Label)RepeaterJZXFYMX.FindControl("LabelYear10")).Text = DropDownListYear10.SelectedValue.ToString(); 
            }
            string sql = "SELECT a.JZXFYDETAIL_ID AS JZXFYMXID,b.JZXFY_PROJECT AS JZXFYPROJECT,b.JZXFY_FYPC AS JZXFYFYPC,b.JZXFY_TRANSDATE AS JZXFYTRANSDATE," +
                "a.JZXFYDETAIL_GOODNAME AS JZXFYMXGOODNAME,a.JZXFYDETAIL_SCZH AS JZXFYMXSCZH,a.JZXFYDETAIL_HWZL AS JZXFYMXHWZL " +
                "FROM TBTM_JZXFYDETAIL a INNER JOIN TBTM_JZXFY b ON a.JZXFYDETAIL_JZXFYID=b.JZXFY_ID " +
                "WHERE b.JZXFY_YEAR='" + DropDownListYear10.SelectedValue.ToString() + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterJZXFYMX.DataSource = dt;
            RepeaterJZXFYMX.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel10.Visible = true;
            }
            else
            {
                Panel10.Visible = false;
            }
        }

        protected void RepeaterJZXFYMX_ItemDataBound(object sender, EventArgs e)
        {

        }

        protected void DropDownListYear10_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetJZXFYMX();
        }

        protected void Delete10_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterJZXFYMX.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterJZXFYMX.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_JZXFYDETAIL WHERE JZXFYDETAIL_ID='" + ((System.Web.UI.WebControls.Label)RepeaterJZXFYMX.Items[i].FindControl("LabelID")).Text + "'";
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count > 0)
            {
                DBCallCommon.ExecuteTrans(sqllist);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=10");
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

        } 

    }
}

