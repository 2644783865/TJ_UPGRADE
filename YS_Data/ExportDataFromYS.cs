using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.YS_Data
{
    public class ExportDataFromYS
    {
        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
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



                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

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

                wksheet = null;
                workbook = null;
                m_xlApp = null;

                GC.Collect();    // 强制垃圾回收 
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpContext.Current.Server.UrlEncode(filename));
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
                //System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                System.Web.HttpContext.Current.Response.WriteFile(filename); // 把文件流发送到客户端 

                System.Web.HttpContext.Current.Response.Flush();
                path.Delete();//删除服务器文件
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 外协入库单导出
        /// </summary>
        public static void Export_OUT_LAB_MAR(string strwhere)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            Worksheet wksheet;

            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("技术外协实际费用明细") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;



            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //导出汇总表
            //获取excel汇总页标题

            string sqltotal = "select WG_CODE AS Code,SupplierName AS Supplier,WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,"
                + "WL_NAME,WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName,GUIGE AS MaterialStandard,"
                + "CAIZHI AS Attribute,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,cast(WG_UPRICE as float) AS UnitPrice,cast(WG_AMOUNT as float) AS Amount,"
                + "WG_PTCODE AS PTC,WG_ORDERID,WG_NOTE AS Comment,WG_COMPANY from View_SM_IN where " + strwhere;
            System.Data.DataTable dtsqltotal = DBCallCommon.GetDTUsingSqlText(sqltotal);


            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            if (dtsqltotal.Rows.Count > 0)
            {
                object[,] data = new object[dtsqltotal.Rows.Count, 14];

                for (int a = 0; a < dtsqltotal.Rows.Count; a++)
                {
                    data[a, 0] = dtsqltotal.Rows[a]["Code"].ToString();
                    data[a, 1] = dtsqltotal.Rows[a]["PTC"].ToString();
                    data[a, 2] = dtsqltotal.Rows[a]["MaterialCode"].ToString();
                    data[a, 3] = dtsqltotal.Rows[a]["MaterialName"].ToString();
                    data[a, 4] = dtsqltotal.Rows[a]["MaterialStandard"].ToString();
                    data[a, 5] = dtsqltotal.Rows[a]["Attribute"].ToString();
                    data[a, 6] = dtsqltotal.Rows[a]["Unit"].ToString();
                    data[a, 7] = dtsqltotal.Rows[a]["RN"].ToString();
                    data[a, 8] = dtsqltotal.Rows[a]["UnitPrice"].ToString();
                    data[a, 9] = dtsqltotal.Rows[a]["Amount"].ToString();
                    data[a, 10] = dtsqltotal.Rows[a]["Warehouse"].ToString();
                    data[a, 11] = dtsqltotal.Rows[a]["WL_NAME"].ToString();
                    data[a, 12] = dtsqltotal.Rows[a]["Comment"].ToString();
                    data[a, 13] = dtsqltotal.Rows[a]["WG_COMPANY"].ToString();
                }

                wksheet.get_Range("A2", wksheet.Cells[dtsqltotal.Rows.Count + 1, 14]).Value2 = data;
                wksheet.get_Range("A2", wksheet.Cells[dtsqltotal.Rows.Count + 2, 14]).Borders.LineStyle = 1;
                wksheet.Cells[dtsqltotal.Rows.Count + 2, 1] = "总计";

                wksheet.get_Range("H" + (dtsqltotal.Rows.Count + 2).ToString(), "H" + (dtsqltotal.Rows.Count + 2).ToString()).Formula = "=SUM(H2:H" + (dtsqltotal.Rows.Count + 1).ToString() + ")";
                wksheet.get_Range("J" + (dtsqltotal.Rows.Count + 2).ToString(), "J" + (dtsqltotal.Rows.Count + 2).ToString()).Formula = "=SUM(J2:J" + (dtsqltotal.Rows.Count + 1).ToString() + ")";
            }

            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromYS.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 生产领料单导出
        /// </summary>
        public static void Export_MAR(string strwhere)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            Worksheet wksheet;

            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("生产领料实际费用明细") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;



            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //导出汇总表
            //获取excel汇总页标题

            string sqltotal = "select id as TrueCode,OutCode AS Code,Dep AS Dep,Warehouse AS Warehouse,Sender AS Sender,"
                + "MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,Standard AS MaterialStandard,"
                + "Length AS Length,Width AS Width,LotNumber AS LotNumber,Unit AS Unit,cast(round(UnitPrice,4) as float) AS UnitPrice,cast(RealNumber as float) AS RN,"
                + "RealSupportNumber,cast(round(Amount,2) as float) AS Amount,Location AS PositionOut,"
                + "PlanMode AS PlanMode,PTC AS PTC,TSAID,ZZBZNM,DetailNote AS Note,OP_BSH AS BSH,OP_PAGENUM,OP_NOTE1 from View_SM_OUT where " + strwhere;
            System.Data.DataTable dtsqltotal = DBCallCommon.GetDTUsingSqlText(sqltotal);


            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            if (dtsqltotal.Rows.Count > 0)
            {
                object[,] data = new object[dtsqltotal.Rows.Count, 26];

                for (int a = 0; a < dtsqltotal.Rows.Count; a++)
                {
                    data[a, 0] = dtsqltotal.Rows[a]["TrueCode"].ToString();
                    data[a, 1] = dtsqltotal.Rows[a]["Dep"].ToString();
                    data[a, 2] = dtsqltotal.Rows[a]["Code"].ToString();
                    data[a, 3] = dtsqltotal.Rows[a]["TSAID"].ToString();
                    data[a, 4] = dtsqltotal.Rows[a]["MaterialCode"].ToString();
                    data[a, 5] = dtsqltotal.Rows[a]["MaterialName"].ToString();
                    data[a, 6] = dtsqltotal.Rows[a]["MaterialStandard"].ToString();
                    data[a, 7] = dtsqltotal.Rows[a]["Attribute"].ToString();
                    data[a, 8] = dtsqltotal.Rows[a]["LotNumber"].ToString();
                    data[a, 9] = dtsqltotal.Rows[a]["Length"].ToString();
                    data[a, 10] = dtsqltotal.Rows[a]["Width"].ToString();
                    data[a, 11] = dtsqltotal.Rows[a]["Unit"].ToString();
                    data[a, 12] = dtsqltotal.Rows[a]["RN"].ToString();
                    data[a, 13] = dtsqltotal.Rows[a]["RealSupportNumber"].ToString();
                    data[a, 14] = dtsqltotal.Rows[a]["UnitPrice"].ToString();
                    data[a, 15] = dtsqltotal.Rows[a]["Amount"].ToString();
                    data[a, 16] = dtsqltotal.Rows[a]["Sender"].ToString();
                    data[a, 17] = dtsqltotal.Rows[a]["Warehouse"].ToString();
                    data[a, 18] = dtsqltotal.Rows[a]["PositionOut"].ToString();
                    data[a, 19] = dtsqltotal.Rows[a]["PTC"].ToString();
                    data[a, 20] = dtsqltotal.Rows[a]["ZZBZNM"].ToString();
                    data[a, 21] = dtsqltotal.Rows[a]["PlanMode"].ToString();
                    data[a, 22] = dtsqltotal.Rows[a]["BSH"].ToString();
                    data[a, 23] = dtsqltotal.Rows[a]["OP_PAGENUM"].ToString();
                    data[a, 24] = dtsqltotal.Rows[a]["Note"].ToString();
                    data[a, 25] = dtsqltotal.Rows[a]["OP_NOTE1"].ToString();

                }

                wksheet.get_Range("A2", wksheet.Cells[dtsqltotal.Rows.Count + 1, 26]).Value2 = data;
                wksheet.get_Range("A2", wksheet.Cells[dtsqltotal.Rows.Count + 2, 26]).Borders.LineStyle = 1;
                wksheet.Cells[dtsqltotal.Rows.Count + 2, 1] = "总计";

                wksheet.get_Range("M" + (dtsqltotal.Rows.Count + 2).ToString(), "M" + (dtsqltotal.Rows.Count + 2).ToString()).Formula = "=SUM(M2:M" + (dtsqltotal.Rows.Count + 1).ToString() + ")";
                wksheet.get_Range("N" + (dtsqltotal.Rows.Count + 2).ToString(), "N" + (dtsqltotal.Rows.Count + 2).ToString()).Formula = "=SUM(N2:N" + (dtsqltotal.Rows.Count + 1).ToString() + ")";
                wksheet.get_Range("P" + (dtsqltotal.Rows.Count + 2).ToString(), "P" + (dtsqltotal.Rows.Count + 2).ToString()).Formula = "=SUM(P2:P" + (dtsqltotal.Rows.Count + 1).ToString() + ")";
            }

            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromYS.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 生产报量单导出
        /// </summary>
        public static void Export_LABOR(string strwhere)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            Worksheet wksheet;

            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("生产报量明细") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;



            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //导出汇总表
            //获取excel汇总页标题

            string sqltotal = "select PS_Project,PS_Eng,PS_ENGID,PS_PIHAO,PS_XUHAO,PS_TUHAO,PS_ZONGXU,PS_NAME,PS_ZXNAME,PS_GUIGE,PS_CAIZI,PS_NUMBER,PS_DANZHONG,"
                + "PS_ZONGZHONG,PS_DJ,PS_JE,PS_MAOPI,PS_STATE,PS_KU,PS_GONGYI,PS_BLSTATE,PS_BLDATE,PS_BZ from TBMP_STATISTICS where " + strwhere;
            System.Data.DataTable dtsqltotal = DBCallCommon.GetDTUsingSqlText(sqltotal);


            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            if (dtsqltotal.Rows.Count > 0)
            {
                object[,] data = new object[dtsqltotal.Rows.Count, 21];

                for (int a = 0; a < dtsqltotal.Rows.Count; a++)
                {
                    data[a, 0] = dtsqltotal.Rows[a]["PS_PIHAO"].ToString();
                    data[a, 1] = dtsqltotal.Rows[a]["PS_XUHAO"].ToString();
                    data[a, 2] = dtsqltotal.Rows[a]["PS_TUHAO"].ToString();
                    data[a, 3] = dtsqltotal.Rows[a]["PS_ZONGXU"].ToString();
                    data[a, 4] = dtsqltotal.Rows[a]["PS_Project"].ToString();
                    data[a, 5] = dtsqltotal.Rows[a]["PS_Eng"].ToString();
                    data[a, 6] = dtsqltotal.Rows[a]["PS_ENGID"].ToString();
                    data[a, 7] = dtsqltotal.Rows[a]["PS_NAME"].ToString();
                    data[a, 8] = dtsqltotal.Rows[a]["PS_ZXNAME"].ToString();
                    data[a, 9] = dtsqltotal.Rows[a]["PS_GUIGE"].ToString();
                    data[a, 10] = dtsqltotal.Rows[a]["PS_CAIZI"].ToString();
                    data[a, 11] = dtsqltotal.Rows[a]["PS_NUMBER"].ToString();
                    data[a, 12] = dtsqltotal.Rows[a]["PS_DANZHONG"].ToString();
                    data[a, 13] = dtsqltotal.Rows[a]["PS_ZONGZHONG"].ToString();
                    data[a, 14] = dtsqltotal.Rows[a]["PS_DJ"].ToString();
                    data[a, 15] = dtsqltotal.Rows[a]["PS_JE"].ToString();
                    data[a, 16] = dtsqltotal.Rows[a]["PS_MAOPI"].ToString();
                    data[a, 17] = dtsqltotal.Rows[a]["PS_STATE"].ToString();
                    data[a, 18] = dtsqltotal.Rows[a]["PS_KU"].ToString();
                    data[a, 19] = dtsqltotal.Rows[a]["PS_GONGYI"].ToString();
                    data[a, 20] = dtsqltotal.Rows[a]["PS_BZ"].ToString();

                }

                wksheet.get_Range("A2", wksheet.Cells[dtsqltotal.Rows.Count + 1, 21]).Value2 = data;
                wksheet.get_Range("A2", wksheet.Cells[dtsqltotal.Rows.Count + 2, 21]).Borders.LineStyle = 1;
                wksheet.Cells[dtsqltotal.Rows.Count + 2, 1] = "总计";

                wksheet.get_Range("N" + (dtsqltotal.Rows.Count + 2).ToString(), "N" + (dtsqltotal.Rows.Count + 2).ToString()).Formula = "=SUM(N2:N" + (dtsqltotal.Rows.Count + 1).ToString() + ")";
                wksheet.get_Range("P" + (dtsqltotal.Rows.Count + 2).ToString(), "P" + (dtsqltotal.Rows.Count + 2).ToString()).Formula = "=SUM(P2:P" + (dtsqltotal.Rows.Count + 1).ToString() + ")";
            }

            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromYS.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 预算明细模版下载
        /// </summary>
        public static void Export_template_download(string type)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            string sqltext = "";
            string sql_dt = "";
            string[] sql_where = { "'01.11'", "'01.08'" };
            string Template_name = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            if (type == "2")
            {
                sqltext = "select YS_Product_Code,YS_Product_Name from TBBD_Product_type where YS_Product_Tag='2' and YS_Product_FatherCode!='Root' order by YS_Product_Code";
                Template_name = "预算明细导入模版（生产部用）";

            }
            else if (type == "1")
            {
                sqltext = "select YS_Product_Code,YS_Product_Name from TBBD_Product_type where YS_Product_Tag='1' and YS_Product_FatherCode=";
                Template_name = "预算明细导入模版（市场部用）";
            }


            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath(Template_name) + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt);
            Worksheet wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            for (int i = 1; i < 3; i++)
            {
                wksheet = (Worksheet)workbook.Sheets.get_Item(i);
                if (type == "1")
                {
                    sql_dt = sqltext + sql_where[i - 1];
                }
                else if (type == "2")
                {
                    sql_dt = sqltext;
                }
                dt = DBCallCommon.GetDTUsingSqlText(sql_dt);
                if (dt.Rows.Count > 0)
                {
                    object[,] data = new object[dt.Rows.Count, 2];

                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        data[a, 0] = dt.Rows[a]["YS_Product_Code"].ToString();
                        data[a, 1] = dt.Rows[a]["YS_Product_Name"].ToString();
                    }

                    wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, 2]).Value2 = data;
                    wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, 8]).Borders.LineStyle = 1;
                    wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, 8]).RowHeight = 20;
                    if (type == "1")
                    {
                        if (i == 1)   //外购件
                        {
                            wksheet.get_Range("F2", "F" + (dt.Rows.Count + 1).ToString()).Formula = "=C2*D2";
                            wksheet.get_Range("G2", "G" + (dt.Rows.Count + 1).ToString()).Formula = "=F2/1.17";
                        }

                        else if (i == 2)    //加工件
                        {
                            wksheet.get_Range("E2", "E" + (dt.Rows.Count + 1).ToString()).Formula = "=C2*D2";
                            wksheet.get_Range("F2", "F" + (dt.Rows.Count + 1).ToString()).Formula = "=E2/1.17";
                        }
                    }
                    else if (type == "2")
                    {
                        wksheet.get_Range("E2", "E" + (dt.Rows.Count + 1).ToString()).Formula = "=C2*D2";
                    }

                }
            }

            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromYS.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
    }
}