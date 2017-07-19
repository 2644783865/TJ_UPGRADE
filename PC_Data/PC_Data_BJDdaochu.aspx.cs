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
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Data_BJDdaochu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpage();
            }
        }

        private void initpage()
        {
            string sqlText = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_POSITION!='0504' and ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqlText, dataText, dataValue);
            drp_stu.SelectedIndex = 0;
        }


        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT   picno, zdrnm,supplierresnm, pjnm, engnm, ptcode, marid, marnm, margg, marcz, margb,case when margb='' then PIC_TUHAO else '' end as PIC_TUHAO, length, width, marnum, " +
                               "marunit, marfznum, marfzunit, price, detamount, detailnote ,supplieranm,qoutefstsa, qoutescdsa, qoutelstsa, supplierbnm,qoutefstsb, qoutescdsb, qoutelstsb," +
                                "suppliercnm,qoutefstsc, qoutescdsc,qoutelstsc, supplierdnm,qoutefstsd, qoutescdsd, qoutelstsd, supplierenm,qoutefstse, qoutescdse, qoutelstse, supplierfnm,qoutefstsf, qoutescdsf, qoutelstsf,PIC_MAP,PIC_CHILDENGNAME " +
                                "from View_TBPC_IQRCMPPRICE_RVW  where  1=1 ";

            if (tb_orderno.Text != "")
            {
                sqltext = sqltext + " and picno like '%" + tb_orderno.Text.Trim() + "%'";
            }
            if (TextBox1.Text != "")
            {
                sqltext = sqltext + " and supplierresnm like '%" + TextBox1.Text + "%'";
            }
            if (txtStartTime.Value != "")
            {
                sqltext = sqltext + " and irqdata>='" + txtStartTime.Value + "'";
            }
            if (txtEndTime.Value != "")
            {
                string enddate = txtEndTime.Value.ToString() == "" ? "2100-01-01" : txtEndTime.Value.ToString();
                enddate = enddate + " 23:59:59";
                sqltext = sqltext + " and irqdata<='" + enddate + "'";
            }
            if (tb_pj.Text != "")
            {
                sqltext = sqltext + " and pjnm like '%" + tb_pj.Text.Trim() + "%'";
            }
            if (tb_eng.Text != "")
            {
                sqltext = sqltext + " and engnm like '%" + tb_eng.Text.Trim() + "%'";
            }
            if (tb_th.Text != "")
            {
                sqltext = sqltext + " and PIC_TUHAO like '%" + tb_th.Text.Trim() + "%'";
            }
            if (drp_stu.SelectedIndex != 0)
            {
                sqltext = sqltext + " and zdrid = '" + drp_stu.SelectedValue + "'";
            }
            if (tb_name.Text != "")
            {
                sqltext = sqltext + " and marnm like '%" + tb_name.Text.Trim() + "%'";
            }
            if (tb_cz.Text != "")
            {
                sqltext = sqltext + " and marcz like '%" + tb_cz.Text.Trim() + "%'";
            }
            if (tb_gg.Text != "")
            {
                sqltext = sqltext + " and margg like '%" + tb_gg.Text.Trim() + "%'";
            }
            if (tb_gb.Text != "")
            {
                sqltext = sqltext + " and margb like '%" + tb_gb.Text.Trim() + "%'";
            }
            if (rad_weitijiao.Checked)
            {
                sqltext = sqltext + " and totalstate='0'";
            }
            if (rad_shz.Checked)
            {
                sqltext = sqltext + " and totalstate='1' or totalstate='3'";
            }
            if (rad_bh.Checked)
            {
                sqltext = sqltext + " and totalstate='2'";
            }
            if (rad_tg.Checked)
            {
                sqltext = sqltext + " and totalstate='4'";
            }
            sqltext = sqltext + " order by picno desc,ptcode asc";
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
        /// 采购比价单
        /// </summary>
        public static void ExportMSData(System.Data.DataTable dt)
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购比价单明细") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            ////根据批号查询数据


            ////设置工作薄名称


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
            //string filename = System.Web.HttpContext.Current.Server.MapPath("采购比价单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

            string filename = "采购比价单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购比价单明细.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 3);

                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["picno"].ToString());//比价单号
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PIC_CHILDENGNAME"].ToString());//部件名称
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["PIC_MAP"].ToString());//部件图号
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["PIC_TUHAO"].ToString());//图号
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["marnm"].ToString());//名称
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["width"].ToString());//宽度
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["marnum"].ToString());//数量
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["marfznum"].ToString());//辅助数量
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["supplierresnm"].ToString());//供应商
                    //row.CreateCell(4).SetCellValue( dt.Rows[i]["pjnm"].ToString());//项目名称
                    //row.CreateCell(5).SetCellValue( dt.Rows[i]["engnm"].ToString());//工程名称

                    //row.CreateCell(7).SetCellValue( dt.Rows[i]["marid"].ToString());//物料编码

                    row.CreateCell(17).SetCellValue(dt.Rows[i]["price"].ToString());//单价
                    row.CreateCell(18).SetCellValue(dt.Rows[i]["detamount"].ToString());//金额
                    row.CreateCell(19).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(20).SetCellValue(dt.Rows[i]["supplieranm"].ToString());//供应商1
                    row.CreateCell(21).SetCellValue(dt.Rows[i]["qoutefstsa"].ToString());//供应商1
                    row.CreateCell(22).SetCellValue(dt.Rows[i]["qoutescdsa"].ToString());//供应商1
                    row.CreateCell(23).SetCellValue(dt.Rows[i]["qoutelstsa"].ToString());//供应商1
                    row.CreateCell(24).SetCellValue(dt.Rows[i]["supplierbnm"].ToString());//供应商2
                    row.CreateCell(25).SetCellValue(dt.Rows[i]["qoutefstsb"].ToString());//供应商2
                    row.CreateCell(26).SetCellValue(dt.Rows[i]["qoutescdsb"].ToString());//供应商2
                    row.CreateCell(27).SetCellValue(dt.Rows[i]["qoutelstsb"].ToString());//供应商2
                    row.CreateCell(28).SetCellValue(dt.Rows[i]["suppliercnm"].ToString());//供应商3
                    row.CreateCell(29).SetCellValue(dt.Rows[i]["qoutefstsc"].ToString());//供应商3
                    row.CreateCell(30).SetCellValue(dt.Rows[i]["qoutescdsc"].ToString());//供应商3
                    row.CreateCell(31).SetCellValue(dt.Rows[i]["qoutelstsc"].ToString());//供应商3
                    row.CreateCell(32).SetCellValue(dt.Rows[i]["supplierdnm"].ToString());//供应商4
                    row.CreateCell(33).SetCellValue(dt.Rows[i]["qoutefstsd"].ToString());//供应商4
                    row.CreateCell(34).SetCellValue(dt.Rows[i]["qoutescdsd"].ToString());//供应商4
                    row.CreateCell(35).SetCellValue(dt.Rows[i]["qoutelstsd"].ToString());//供应商4
                    row.CreateCell(36).SetCellValue(dt.Rows[i]["supplierenm"].ToString());//供应商5
                    row.CreateCell(37).SetCellValue(dt.Rows[i]["qoutefstse"].ToString());//供应商5
                    row.CreateCell(38).SetCellValue(dt.Rows[i]["qoutescdse"].ToString());//供应商5
                    row.CreateCell(39).SetCellValue(dt.Rows[i]["qoutelstse"].ToString());//供应商5
                    row.CreateCell(40).SetCellValue(dt.Rows[i]["supplierfnm"].ToString());//供应商6
                    row.CreateCell(41).SetCellValue(dt.Rows[i]["qoutefstsf"].ToString());//供应商6
                    row.CreateCell(42).SetCellValue(dt.Rows[i]["qoutescdsf"].ToString());//供应商6
                    row.CreateCell(43).SetCellValue(dt.Rows[i]["qoutelstsf"].ToString());//供应商6

                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);

                    for (int j = 0; j < 44; j++)
                    {
                        row.Cells[j].CellStyle = cells;
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
        private static void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {
                System.IO.FileInfo path = new System.IO.FileInfo(filename);
                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();
                CloseExeclProcess(m_xlApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;

                GC.Collect();
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Charset = "GB2312";
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpContext.Current.Server.UrlEncode(filename));
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 

                System.Web.HttpContext.Current.Response.WriteFile(filename); // 把文件流发送到客户端 
                System.Web.HttpContext.Current.Response.Flush();
                path.Delete();//删除服务器文件
            }
            catch (Exception e)
            {
                throw e;
            }
        }
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

        protected void TextBox1_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (TextBox1.Text.ToString().Contains("|"))
            {
                Cname = TextBox1.Text.Substring(0, TextBox1.Text.ToString().IndexOf("|"));
                TextBox1.Text = Cname.Trim();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写供应商！');", true);
            }
        }
        protected void tb_pj_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_pj.Text.ToString().Contains("|"))
            {
                Cname = tb_pj.Text.Substring(0, tb_pj.Text.ToString().IndexOf("|"));
                tb_pj.Text = Cname.Trim();
            }
            else if (tb_pj.Text == "")
            {

            }
        }
        protected void tb_eng_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_eng.Text.ToString().Contains("|"))
            {
                Cname = tb_eng.Text.Substring(0, tb_eng.Text.ToString().IndexOf("|"));
                tb_eng.Text = Cname.Trim();
            }
            else if (tb_eng.Text == "")
            {

            }
        }
    }
}
