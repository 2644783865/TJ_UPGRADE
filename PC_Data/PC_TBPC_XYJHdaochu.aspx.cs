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
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_XYJHdaochu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT planno, ptcode,pjnm,engnm,depnm,sqrnm,sqrtime,PUR_TUHAO, " +
                                      "marid, marnm, margg, marcz, margb,PUR_MASHAPE,length,width,num, marunit,fznum,marfzunit,rpnum,rpfznum,cgrnm,case WHEN PUR_CSTATE='0' then '否' else '是' end as pur_cstate,PUR_ZYDY,purnote  " +
                                      "FROM  View_TBPC_PURCHASEPLAN_RVW   where 1=1 ";
            if (tb_pihao.Text != "")//批号
            {
                sqltext = sqltext + " and planno like '%" + tb_pihao.Text.Trim() + "%'";
            }
            if (tb_marid.Text != "")//物料编码
            {
                sqltext = sqltext + " and marid like '%" + tb_marid.Text.Trim() + "%'";
            }
            if (txtStartTime.Value != "")//开始时间
            {
                sqltext = sqltext + " and sqrtime>='" + txtStartTime.Value.Trim() + "'";
            }
            if (txtEndTime.Value != "")//结束时间
            {
                string enddate = txtEndTime.Value.ToString() == "" ? "2100-01-01" : txtEndTime.Value.ToString();
                enddate = enddate + " 23:59:59";
                sqltext = sqltext + " and sqrtime<='" + enddate + "'";
            }
            //if (tb_pj.Text != "")//项目
            //{
            //    sqltext = sqltext + " and pjnm like '%" + tb_pj.Text.Trim() + "%'";
            //}
            //if (tb_eng.Text != "")//工程
            //{
            //    sqltext = sqltext + " and engnm like '%" + tb_eng.Text.Trim() + "%'";
            //}
            if (tb_th.Text != "")//图号
            {
                sqltext = sqltext + " and PUR_TUHAO like '%" + tb_th.Text.Trim() + "%'";
            }
            if (tb_shape.Text != "")//物料类型
            {
                sqltext = sqltext + " and PUR_MASHAPE = '" + tb_shape.Text.Trim() + "'";
            }
            if (tb_name.Text != "")//名称
            {
                sqltext = sqltext + " and marnm like '%" + tb_name.Text.Trim() + "%'";
            }
            if (tb_cz.Text != "")//材质
            {
                sqltext = sqltext + " and marcz like '%" + tb_cz.Text.Trim() + "%'";
            }
            if (tb_gg.Text != "")//规格
            {
                sqltext = sqltext + " and margg like '%" + tb_gg.Text.Trim() + "%'";
            }
            if (tb_gb.Text != "")//国标
            {
                sqltext = sqltext + " and margb like '%" + tb_gb.Text.Trim() + "%'";
            }
            if (DropDownList3.SelectedIndex != 0)//申请部门
            {
                sqltext = sqltext + " and depid = '" + DropDownList3.SelectedValue + "'";
            }
            if (tb_sqr.Text != "")
            {
                sqltext = sqltext + " and sqrnm like '%" + tb_sqr.Text.Trim() + "%'";
            }
            if (rad_weidaohuo.Checked)
            {
                sqltext = sqltext + " and prstate != '5'";
            }
            else if (rad_bfdaohuo.Checked)
            {
                sqltext = sqltext + " and prstate = '5'";
            }
            sqltext = sqltext + " order by ptcode asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 10000)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出的数据量超过10000条，请添加导出条件，分多次导出！');", true);
            }
            else
            {
                ExportMSData(dt);
            }
        }
        /// <summary>
        /// 批量导出材料需用计划
        /// </summary>
        public static void ExportMSData(System.Data.DataTable dt)
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("材料需用计划模版1") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            ////根据批号查询数据


            ////设置工作薄名称
            ////string filename = Server.MapPath("采购订单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ////wksheet.Name = filename;

            //////填充数据

            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount+1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = i + 1;
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        dataArray[i, j + 1] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount+1]).Value2 = dataArray;
            //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount+1]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("材料需用计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ////ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            string filename = "材料需用计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("材料需用计划模版1.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                #endregion

                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        #region Region
        //private static void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        //{
        //    try
        //    {
        //        System.IO.FileInfo path = new System.IO.FileInfo(filename);
        //        workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(Type.Missing, Type.Missing, Type.Missing);
        //        m_xlApp.Workbooks.Close();
        //        m_xlApp.Quit();
        //        m_xlApp.Application.Quit();
        //        CloseExeclProcess(m_xlApp);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

        //        wksheet = null;
        //        workbook = null;
        //        m_xlApp = null;

        //        GC.Collect();
        //        System.Web.HttpContext.Current.Response.Clear();
        //        System.Web.HttpContext.Current.Response.Charset = "GB2312";
        //        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpContext.Current.Server.UrlEncode(filename));
        //        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 

        //        System.Web.HttpContext.Current.Response.WriteFile(filename); // 把文件流发送到客户端 
        //        System.Web.HttpContext.Current.Response.Flush();
        //        path.Delete();//删除服务器文件
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //} 
        #endregion

        private static void CloseExeclProcess(Application excelApp)
        {
            #region kill excel process

            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in procs)
            {
                int baseAdd = p.MainModule.BaseAddress.ToInt32();
                //oXL is Excel.ApplicationClass object 
                if (baseAdd == excelApp.Hinstance)
                {
                    p.Kill();
                    break;
                }
            }
            #endregion
        }
        //protected void tb_pj_Textchanged(object sender, EventArgs e)
        //{
        //    string Cname = "";
        //    if (tb_pj.Text.ToString().Contains("|"))
        //    {
        //        Cname = tb_pj.Text.Substring(0, tb_pj.Text.ToString().IndexOf("|"));
        //        tb_pj.Text = Cname.Trim();
        //    }
        //    else if (tb_pj.Text == "")
        //    {

        //    }
        //}
        //protected void tb_eng_Textchanged(object sender, EventArgs e)
        //{
        //    string Cname = "";
        //    if (tb_eng.Text.ToString().Contains("|"))
        //    {
        //        Cname = tb_eng.Text.Substring(0, tb_eng.Text.ToString().IndexOf("|"));
        //        tb_eng.Text = Cname.Trim();
        //    }
        //    else if (tb_eng.Text == "")
        //    {

        //    }
        //}

    }
}
