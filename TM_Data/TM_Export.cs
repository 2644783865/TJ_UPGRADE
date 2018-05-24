using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ZCZJ_DPF.TM_Data
{
    public class ExportTMDataFromDB
    {
        /// <summary>
        /// 导出原始数据
        /// </summary>
        /// <param name="prdno"></param>
        /// <param name="viewtabel"></param>
        //public static void ExportOrgData(string prdno, string viewtable, string orderColum, string strwhere)
        //{
        //    Object Opt = System.Type.Missing;
        //    Application m_xlApp = new Application();
        //    Workbooks workbooks = m_xlApp.Workbooks;
        //    Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        //    Worksheet wksheet;
        //    workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("副本原始数据表") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;
        //    //Microsoft.Office.Interop.Excel.Style st=workbook.Styles.Add("PropertyBorder", Type.Missing);

        //    m_xlApp.Visible = false;     // Excel不显示  
        //    m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

        //    wksheet = (Worksheet)workbook.Sheets.get_Item(1);
        //    System.Data.DataTable dt = AfterTrimData(prdno, viewtable, orderColum, strwhere);
        //    string name = "";
        //    if (dt.Rows.Count > 0)
        //    {
        //        name = prdno;
        //    }
        //    int length = name.Length;
        //    if (length > 31)
        //    {
        //        length = 31;
        //    }
        //    wksheet.Name = name.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-').Substring(0, length);//工作表名称 //替换 ： / ? * [ 或 ]，最长31


        //    string sqltext_bt = "select TSA_ENGNAME+'('+TSA_ID+')' AS TSA_ENGNAME,TSA_PJNAME+'('+TSA_PJID+')' AS TSA_PJNAME from View_TM_TaskAssign where TSA_ID='" + prdno + "'";
        //    System.Data.DataTable dt_bt = DBCallCommon.GetDTUsingSqlText(sqltext_bt);

        //    //////// 填充数据
        //    ////////项目
        //    wksheet.Cells[3, 3] = dt_bt.Rows[0]["TSA_PJNAME"].ToString();
        //    ////////工程
        //    wksheet.Cells[3, 23] = dt_bt.Rows[0]["TSA_ENGNAME"].ToString();


        //    //详细数据
        //    int rowCount = dt.Rows.Count;

        //    int colCount = dt.Columns.Count;

        //    object[,] dataArray = new object[rowCount, colCount];

        //    for (int i = 0; i < rowCount; i++)
        //    {

        //        for (int j = 0; j < colCount; j++)
        //        {

        //            dataArray[i, j] = dt.Rows[i][j];
        //        }
        //    }

        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Value2 = dataArray;
        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Borders.LineStyle = 1;

        //    //设置列宽
        //    ///////wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
        //    string filename = System.Web.HttpContext.Current.Server.MapPath("原始数据" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
        //    ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        //}
        /// <summary>
        /// 导出制作明细表
        /// </summary>
        /// <param name="lotnum">制作明细批号、为空时表明为全部</param>
        public static void ExportMSData(string lotnum, string taskid)
        {
            string revtable = "View_TM_MSFORALLRVW";
            string table = "View_TM_MKDETAIL";
            if (lotnum.Contains("BG"))
            {
                revtable = "View_TM_MSCHANGERVW";
                table = "View_TM_MSCHANGE";
            }
            string filename = "产品明细表" + lotnum + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("产品明细表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                ISheet sheet1 = wk.GetSheetAt(1);
                //根据批号查询数据
                string sql_select_data = "";

                sql_select_data = "select MS_TUHAO,MS_ZONGXU,MS_NAME+isnull(MS_GUIGE,''),MS_CAIZHI,MS_UNUM,MS_NUM,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_TECHUNIT,MS_YONGLIANG,MS_MATOTALWGHT,MS_LENGTH,MS_WIDTH,MS_NOTE,MS_XIALIAO,MS_PROCESS,isnull(MS_KU,'') as MS_KU,MS_ALLBEIZHU from " + table + " where MS_PID='" + lotnum + "' and MS_ENGID='" + taskid + "'  order by  dbo.f_formatstr(MS_NEWINDEX, '.')";

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_data);

                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }

                string basic_sql = "select MS_PJID, MS_ENGID,MS_PJNAME, MS_ENGNAME,MS_MAP from " + revtable + " where MS_ID='" + lotnum + "'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(basic_sql);
                IRow row2 = sheet0.GetRow(2);
                row2.GetCell(1).SetCellValue(dt1.Rows[0]["MS_PJNAME"].ToString());
                row2.GetCell(3).SetCellValue(dt1.Rows[0]["MS_PJID"].ToString());
                row2.GetCell(5).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                IRow row3 = sheet0.GetRow(3);
                row3.GetCell(1).SetCellValue(dt1.Rows[0]["MS_ENGID"].ToString());
                IRow row4 = sheet0.GetRow(4);
                row4.GetCell(1).SetCellValue(dt1.Rows[0]["MS_MAP"].ToString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 2);
                    for (int j = 0; j < 16; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    string[] pross = new string[10];
                    for (int j = 0; j < dt.Rows[i][16].ToString().Split('-').Length; j++)
                    {
                        pross[j] = dt.Rows[i][16].ToString().Split('-')[j];
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        row.CreateCell(j + 16).SetCellValue(pross[j]);
                    }
                    for (int j = 17; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 9).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }


                sheet0.ForceFormulaRecalculation = true;
                sheet1.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }



        /// <summary>
        /// 导出所有制作明细表
        /// </summary>
        /// <param name="lotnum">制作明细批号、为空时表明为全部</param>
        public static void ExportMSAllData(string taskid)
        {

            string filename = "产品明细表汇总" + taskid + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("产品明细表汇总.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(1);

                string sql_select_data = "";

                sql_select_data = "select * from (select MS_PID,MS_TUHAO,MS_ZONGXU,MS_NAME+isnull(MS_GUIGE,'') as MS_NAME,MS_CAIZHI,MS_UNUM,MS_NUM,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_TECHUNIT,MS_YONGLIANG,MS_MATOTALWGHT,MS_LENGTH,MS_WIDTH,MS_NOTE,MS_XIALIAO,MS_PROCESS,isnull(MS_KU,'') as MS_KU,MS_ALLBEIZHU from View_TM_MSCHANGE where MS_ENGID='" + taskid + "' union all select MS_PID,MS_TUHAO,MS_ZONGXU,MS_NAME+isnull(MS_GUIGE,'') as MS_NAME ,MS_CAIZHI,MS_UNUM,MS_NUM,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_TECHUNIT,MS_YONGLIANG,MS_MATOTALWGHT,MS_LENGTH,MS_WIDTH,MS_NOTE,MS_XIALIAO,MS_PROCESS,isnull(MS_KU,'') as MS_KU,MS_ALLBEIZHU from View_TM_MKDETAIL where MS_ENGID='" + taskid + "')a  order by MS_PID,MS_ZONGXU";

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_data);

                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 2);
                    for (int j = 0; j < 17; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    string[] pross = new string[10];
                    for (int j = 0; j < dt.Rows[i][17].ToString().Split('-').Length; j++)
                    {
                        pross[j] = dt.Rows[i][17].ToString().Split('-')[j];
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        row.CreateCell(j + 17).SetCellValue(pross[j]);
                    }
                    for (int j = 18; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 9).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }


                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }



        /// <summary>
        /// 导出制作明细表（可导出多批）
        /// </summary>
        /// <param name="lotnum"></param>
        //public static void ExportCheckedMSData(string array_lotnum, string taskid)
        //{



        //    //根据批号查询数据
        //    string sql_select_data = "";
        //    string[] tasksplit = taskid.Split('-');

        //    string sql_gettable = "select [dbo].[TM_GetViewMsNameByTaskID]('" + taskid + "')";
        //    string mstable = DBCallCommon.GetDTUsingSqlText(sql_gettable).Rows[0][0].ToString();

        //    sql_select_data = "select ISNULL(MS_MSXUHAO,'') AS MS_MSXUHAO,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_BOXCODE as MS_ENNAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,(CASE WHEN MS_KU='库' THEN CAST(MS_TLWGHT AS VARCHAR) ELSE '' END) AS MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_PROCESS,isnull(MS_KU,'') as MS_KU,MS_BOXCODE,(CASE WHEN MS_GB IS NULL OR MS_GB='' THEN MS_NOTE ELSE MS_GB+'；'+MS_NOTE END) AS MS_NOTE,MS_PID from " + mstable + " where MS_PID in(" + array_lotnum + ") and MS_ENGID='" + taskid + "' and MS_NEWINDEX like '%.%' order by  MS_PID,dbo.f_formatstr(MS_NEWINDEX, '.')";


        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_data);

        //    if (dt.Rows.Count == 0)
        //    {
        //        System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
        //        return;
        //    }

        //    //设置工作薄名称
        //    wksheet.Name = "明细表";

        //    string sql_fp = "select Top 1 '" + taskid.Split('-')[0] + "' AS MS_ENGID,MS_PJNAME+'('+MS_PJID+')' AS MS_PJNAME,MS_ENGNAME  from View_TM_MSFORALLRVW where MS_ENGID='" + taskid + "'";
        //    System.Data.DataTable dt_fp = DBCallCommon.GetDTUsingSqlText(sql_fp);
        //    Worksheet wksheet_fp = (Worksheet)workbook.Sheets.get_Item(1);
        //    wksheet_fp.Cells[7, 3] = dt_fp.Rows[0]["MS_ENGID"].ToString();

        //    wksheet_fp.Cells[7, 5] = dt_fp.Rows[0]["MS_PJNAME"].ToString();

        //    wksheet_fp.Cells[7, 9] = dt_fp.Rows[0]["MS_ENGNAME"].ToString();

        //    int rowCount = dt.Rows.Count;

        //    int colCount = dt.Columns.Count;

        //    object[,] dataArray = new object[rowCount, colCount];

        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        for (int j = 0; j < colCount; j++)
        //        {
        //            dataArray[i, j] = dt.Rows[i][j];
        //        }
        //    }

        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Value2 = dataArray;
        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Borders.LineStyle = 1;
        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 6, colCount]).Font.Name = "宋体";
        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 6, colCount]).Font.Size = 8;
        //    //***********列汇总

        //    int row = rowCount + 5;
        //    wksheet.Cells[row + 1, 2] = "TOTAL:";
        //    string formula1 = "=SUM(J6:J" + row.ToString() + ")";
        //    Range rg1 = wksheet.get_Range("J" + (row + 1).ToString(), System.Type.Missing);
        //    rg1.Formula = formula1;
        //    rg1.Calculate();//小计

        //    string filename = System.Web.HttpContext.Current.Server.MapPath("制作明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
        //    ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        //}
        /// <summary>
        /// 导出材料计划表
        /// </summary>
        /// <param name="lotnum">批号，为空时表明为全部</param>
        public static void ExportMPData(string lotnum, string taskid, string matype)
        {
            //根据批号获取基本数据
            string sql_select_basic = "";

            sql_select_basic = "select TSA_PJID,CM_PROJ,TSA_ENGNAME,MP_ENGID,MP_ADATE from View_TM_MPFORALLRVW where MP_ID in ('" + lotnum + "')";
            System.Data.DataTable dt_sql_select_basic = DBCallCommon.GetDTUsingSqlText(sql_select_basic);
            if (dt_sql_select_basic.Rows.Count == 0)
            {

                System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                return;
            }
            string filename = "材料汇总表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("材料汇总表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);
                ////填充数据
                IRow row2 = sheet.GetRow(2);
                row2.GetCell(2).SetCellValue(dt_sql_select_basic.Rows[0]["CM_PROJ"].ToString());
                row2.GetCell(5).SetCellValue(dt_sql_select_basic.Rows[0]["TSA_PJID"].ToString());
                row2.GetCell(8).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                IRow row3 = sheet.GetRow(3);
                row3.GetCell(2).SetCellValue(dt_sql_select_basic.Rows[0]["MP_ENGID"].ToString());


                string sql_select = "select '-' as MP_TUHAO,MP_NAME+isnull(MP_GUIGE,''),MP_CAIZHI , MP_TECHUNIT,'' as MP_UYONGLIANG,SUM(CAST(MP_YONGLIANG as float)) AS MP_YONGLIANG,SUM(MP_WEIGHT) AS MP_WEIGHT,MP_MASHAPE, MP_ALLBEIZHU from View_TM_MPPLAN where MP_PID in ('" + lotnum + "') and ((MP_FIXEDSIZE='N' and MP_MASHAPE='板') or MP_MASHAPE='型' or MP_MASHAPE='圆' or MP_MASHAPE='非' ) group by MP_MARID,CONVERTRATE,MP_NAME ,MP_GUIGE,MP_CAIZHI , MP_STANDARD, MP_KEYCOMS, MP_TECHUNIT, MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_ALLBEIZHU,MP_MASHAPE,MP_TRACKNUM ";
                sql_select += "union all select  MP_TUHAO,MP_NAME+isnull(MP_GUIGE,''),MP_CAIZHI , MP_TECHUNIT,'' as MP_UYONGLIANG,SUM(CAST(MP_YONGLIANG as float)) AS MP_YONGLIANG,SUM(MP_WEIGHT) AS MP_WEIGHT,MP_MASHAPE, MP_ALLBEIZHU from View_TM_MPPLAN where MP_PID in ('" + lotnum + "') and ( MP_MASHAPE='采' or MP_MASHAPE='采购成品' or MP_MASHAPE='锻' or MP_MASHAPE='铸' ) group by MP_TUHAO, MP_MARID,CONVERTRATE,MP_NAME ,MP_GUIGE,MP_CAIZHI , MP_STANDARD, MP_KEYCOMS, MP_TECHUNIT, MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_ALLBEIZHU,MP_MASHAPE,MP_TRACKNUM ";
                DataTable sql_select_dt = DBCallCommon.GetDTUsingSqlText(sql_select);
                for (int i = 0; i < sql_select_dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 6);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < sql_select_dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(sql_select_dt.Rows[i][j].ToString());
                    }
                }

                ISheet sheet1 = wk.GetSheetAt(1);
                sql_select = "select '-' as MP_TUHAO,MP_NAME+isnull(MP_GUIGE,''),MP_CAIZHI , MP_TECHUNIT,SUM(CAST(MP_YONGLIANG as float)) AS MP_YONGLIANG,SUM(MP_WEIGHT) AS MP_WEIGHT,MP_MASHAPE,MP_LENGTH,MP_WIDTH from View_TM_MPPLAN where MP_PID in ('" + lotnum + "') and (MP_FIXEDSIZE='Y' and MP_MASHAPE='板')  group by MP_MARID,CONVERTRATE,MP_NAME,MP_GUIGE,MP_CAIZHI,MP_LENGTH,MP_WIDTH, MP_STANDARD, MP_KEYCOMS, MP_TECHUNIT , MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT, MP_ALLBEIZHU,MP_MASHAPE,MP_TRACKNUM";
                sql_select_dt = DBCallCommon.GetDTUsingSqlText(sql_select);
                for (int i = 0; i < sql_select_dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 6);

                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < sql_select_dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(sql_select_dt.Rows[i][j].ToString());
                    }
                }
                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }


            //SqlConnection sqlConn = new SqlConnection();
            //SqlCommand sqlCmd = new SqlCommand();
            //sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            //DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "TM_MP_COLLECT");
            //DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", "View_TM_MPPLAN", SqlDbType.VarChar, 1000);
            //DBCallCommon.AddParameterToStoredProc(sqlCmd, "@PiHao", lotnum, SqlDbType.VarChar, 1000);
            //DBCallCommon.AddParameterToStoredProc(sqlCmd, "@WhereCon", "", SqlDbType.VarChar, 1000);
            //材料明细数据
            //sql_select_basic = "select '-' as MP_TUHAO,MP_NAME+isnull(MP_GUIGE,''),MP_CAIZHI,MP_TECHUNIT,'',SUM(CAST(MP_YONGLIANG as float)) AS MP_YONGLIANG,  SUM(MP_WEIGHT) AS MP_WEIGHT,MP_MASHAPE,MP_ALLBEIZHU from View_TM_MPPLAN where MP_PID='" + lotnum + "' and  MP_FIXEDSIZE='Y' and MP_MASHAPE='板' group by MP_MARID,CONVERTRATE,MP_NAME,MP_GUIGE,MP_CAIZHI,MP_LENGTH,MP_WIDTH, MP_STANDARD, MP_KEYCOMS, MP_TECHUNIT , MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT, MP_ALLBEIZHU,MP_MASHAPE,MP_TRACKNUM";
            //sql_select_basic += " union all select '-' as MP_TUHAO,MP_NAME+isnull(MP_GUIGE,''),MP_CAIZHI,MP_TECHUNIT,'',SUM(CAST(MP_YONGLIANG as float)) AS MP_YONGLIANG,  SUM(MP_WEIGHT) AS MP_WEIGHT,MP_MASHAPE,MP_ALLBEIZHU from View_TM_MPPLAN where MP_PID='" + lotnum + "' and (MP_MASHAPE<>'板' or MP_FIXEDSIZE<>'Y')  group by MP_MARID,CONVERTRATE,MP_NAME ,MP_GUIGE,MP_CAIZHI , MP_STANDARD, MP_KEYCOMS, MP_TECHUNIT, MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_MASHAPE,MP_TRACKNUM,MP_ALLBEIZHU";
            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_basic);
            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = i + 1;
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        dataArray[i, j + 1] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A7", wksheet.Cells[rowCount + 7, colCount]).Value2 = dataArray;
            //wksheet.get_Range("A7", wksheet.Cells[rowCount + 7, colCount]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("/TM_Data/ExportFile/" + "材料计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

        }



        /// <summary>
        /// 导出采购外协表
        /// </summary>
        /// <param name="lotnum"></param>
        /// <param name="taskid"></param>
        //public static void ExportOutData(string lotnum, string taskid)
        //{
        //    Object Opt = System.Type.Missing;
        //    Application m_xlApp = new Application();
        //    Workbooks workbooks = m_xlApp.Workbooks;
        //    Workbook workbook;
        //    Worksheet wksheet;
        //    workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("委外联系单") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

        //    m_xlApp.Visible = false;     // Excel不显示  
        //    m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

        //    wksheet = (Worksheet)workbook.Sheets.get_Item(1);

        //    //设置工作薄名称
        //    if (lotnum != "")
        //    {
        //        string name = lotnum.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-');
        //        if (name.Length > 31)
        //        {
        //            wksheet.Name = name.Substring(0, 31);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
        //        }
        //        else
        //        {
        //            wksheet.Name = name;
        //        }
        //    }
        //    else
        //    {
        //        wksheet.Name = "采购外协 全部";
        //    }

        //    //根据批号查询数据
        //    string sql_select_basicdata = "select OST_ENGID,OST_PJNAME,OST_ENGNAME,OST_PJID,OST_MDATE,OST_ADATE from View_TM_OUTSOURCETOTAL where OST_OUTSOURCENO='" + lotnum + "'";
        //    System.Data.DataTable dt_basicdata = DBCallCommon.GetDTUsingSqlText(sql_select_basicdata);
        //    if (dt_basicdata.Rows.Count == 0)
        //    {
        //        CloseExeclProcess(m_xlApp);
        //        System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
        //        return;
        //    }

        //    ////填充数据

        //    #region 基本数据
        //    //生产制号
        //    wksheet.Cells[3, 3] = taskid.Split('-')[0];
        //    Range rghz0 = wksheet.get_Range(wksheet.Cells[3, 3], wksheet.Cells[3, 4]);
        //    rghz0.MergeCells = true;
        //    rghz0.HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //    // 项目名称：       
        //    wksheet.Cells[3, 6] = dt_basicdata.Rows[0]["OST_PJNAME"].ToString() + "(" + dt_basicdata.Rows[0]["OST_PJID"].ToString() + ")";
        //    Range rghz1 = wksheet.get_Range(wksheet.Cells[3, 6], wksheet.Cells[3, 8]);
        //    rghz1.MergeCells = true;
        //    rghz1.HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //    //工程名称
        //    wksheet.Cells[3, 11] = dt_basicdata.Rows[0]["OST_ENGNAME"].ToString() + "(" + dt_basicdata.Rows[0]["OST_ENGID"].ToString() + ")";
        //    Range rghz2 = wksheet.get_Range(wksheet.Cells[3, 11], wksheet.Cells[3, 13]);
        //    rghz2.MergeCells = true;
        //    rghz2.HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //    //批准/日期:
        //    wksheet.Cells[4, 3] = dt_basicdata.Rows[0]["OST_ADATE"].ToString().Split(' ')[0];
        //    Range rghz3 = wksheet.get_Range(wksheet.Cells[4, 3], wksheet.Cells[4, 4]);
        //    rghz3.MergeCells = true;
        //    rghz3.HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //    //编制/日期：
        //    wksheet.Cells[4, 6] = dt_basicdata.Rows[0]["OST_MDATE"].ToString().Split(' ')[0];
        //    Range rghz4 = wksheet.get_Range(wksheet.Cells[4, 6], wksheet.Cells[4, 8]);
        //    rghz4.MergeCells = true;
        //    rghz4.HorizontalAlignment = XlHAlign.xlHAlignLeft;

        //    //计划批号：	
        //    wksheet.Cells[4, 11] = lotnum;
        //    Range rghz5 = wksheet.get_Range(wksheet.Cells[4, 11], wksheet.Cells[4, 13]);
        //    rghz5.MergeCells = true;
        //    rghz5.HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //    #endregion

        //    #region 详细数据
        //    string sql_select_detail = "";
        //    if (lotnum.Contains(".JSB WXBG/") || lotnum.Contains(".JSB WXQX/"))
        //    {
        //        sql_select_detail = "select ROW_NUMBER() OVER (ORDER BY OSL_TRACKNUM) AS RowNum,OSL_NAME,OSL_MARID,OSL_BIAOSHINO,OSL_GUIGE,OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,CASE WHEN OSL_REQUEST<>'' THEN OSL_REQUEST+';'+OSL_NOTE ELSE OSL_NOTE END ,OSL_REQDATE,OSL_DELSITE,OSL_TRACKNUM from View_TM_OUTSCHANGE where OSL_OUTSOURCENO='" + lotnum + "' ORDER BY OSL_TRACKNUM";
        //    }
        //    else
        //    {
        //        sql_select_detail = "select ROW_NUMBER() OVER (ORDER BY OSL_TRACKNUM) AS RowNum,OSL_NAME,OSL_MARID,OSL_BIAOSHINO,OSL_GUIGE,OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,CASE WHEN OSL_REQUEST<>'' THEN OSL_REQUEST+';'+OSL_NOTE ELSE OSL_NOTE END,OSL_REQDATE,OSL_DELSITE,OSL_TRACKNUM from View_TM_OUTSOURCELIST where OSL_OUTSOURCENO='" + lotnum + "'  ORDER BY OSL_TRACKNUM";
        //    }

        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_detail);

        //    int rowCount = dt.Rows.Count;

        //    int colCount = dt.Columns.Count;

        //    object[,] dataArray = new object[rowCount, colCount];

        //    for (int i = 0; i < rowCount; i++)
        //    {

        //        for (int j = 0; j < colCount; j++)
        //        {

        //            dataArray[i, j] = dt.Rows[i][j];

        //        }
        //    }

        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Value2 = dataArray;
        //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Borders.LineStyle = 1;

        //    #endregion

        //    ////设置列宽
        //    wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
        //    string filename = System.Web.HttpContext.Current.Server.MapPath("外协计划表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
        //    ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        //}
        //private static void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet) //输出Excel文件并退出
        //{
        //    try
        //    {
        //        workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(Type.Missing, Type.Missing, Type.Missing);
        //        m_xlApp.Workbooks.Close();
        //        m_xlApp.Quit();
        //        m_xlApp.Application.Quit();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);
        //        wksheet = null;
        //        workbook = null;
        //        m_xlApp = null;
        //        GC.Collect();
        //        //下载
        //        System.IO.FileInfo path = new System.IO.FileInfo(filename);
        //        //同步，异步都支持
        //        HttpResponse contextResponse = HttpContext.Current.Response;
        //        contextResponse.Redirect(string.Format("~/TM_Data/ExportFile/{0}", path.Name), false);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        /// <summary>
        /// 关闭Excel进程
        /// </summary>
        /// <param name="excelApp"></param>
        //private static void CloseExeclProcess(Application excelApp)
        //{
        //    #region kill excel process

        //    System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
        //    foreach (System.Diagnostics.Process p in procs)
        //    {
        //        int baseAdd = p.MainModule.BaseAddress.ToInt32();
        //        //oXL is Excel.ApplicationClass object 
        //        if (baseAdd == excelApp.Hinstance)
        //        {
        //            p.Kill();
        //            break;
        //        }
        //    }
        //    #endregion
        //}
        /// <summary>
        /// 返回经过处理后的数据表(原始数据)
        /// </summary>
        /// <param name="prdno"></param>
        /// <param name="viewtablename"></param>
        /// <returns></returns>
        private static System.Data.DataTable AfterTrimData(string prdno, string viewtablename, string exporttype, string strwhere)
        {
            System.Data.DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_ExportTrimData]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@ViewTableName", viewtablename, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TaskID", prdno, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@ExportType", exporttype, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrWhere", strwhere, SqlDbType.Text, 1000);
                sqlConn.Open();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        /// <summary>
        /// 导出某一生产制号下所有制作明细
        /// </summary>
        /// <param name="taskid"></param>
        public static void ExportAllMsData(string taskid)
        {
            //DeleteFiles();
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("制作明细表（JS-001)") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(2);

            ////根据生产制号查询数据
            //string sql_select_data = "";
            //sql_select_data = "select ISNULL(MS_MSXUHAO,'') AS MS_MSXUHAO,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_BOXCODE as MS_ENNAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,(CASE WHEN MS_KU='库' THEN CAST(MS_TLWGHT AS VARCHAR) ELSE '' END) AS MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_PROCESS,isnull(MS_KU,'') as MS_KU,MS_BOXCODE,(CASE WHEN MS_GB IS NULL OR MS_GB='' THEN MS_NOTE ELSE MS_GB+'；'+MS_NOTE END) AS MS_NOTE from MSVIEW where MS_ENGID like '" + taskid + "-%' and MS_STATUS='0' and MS_STATERV='8' and MS_NEWINDEX like '%.%' order by dbo.f_formatstr(MS_NEWINDEX, '.')";

            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_data);
            //if (dt.Rows.Count == 0)
            //{
            //    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
            //    return;
            //}
            ////设置工作薄名称
            //string name = taskid.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-');
            //if (name.Length > 31)
            //{
            //    wksheet.Name = name.Substring(0, 31);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
            //}
            //else
            //{
            //    wksheet.Name = name;
            //}
            //string sql_fp = "select Top 1 '" + taskid.Split('-')[0] + "' AS MS_ENGID,MS_PJNAME+'('+MS_PJID+')' AS MS_PJNAME,MS_ENGNAME  from View_TM_MSFORALLRVW where MS_ENGID LIKE '" + taskid + "-%'";
            //System.Data.DataTable dt_fp = DBCallCommon.GetDTUsingSqlText(sql_fp);
            //Worksheet wksheet_fp = (Worksheet)workbook.Sheets.get_Item(1);
            //wksheet_fp.Cells[7, 3] = dt_fp.Rows[0]["MS_ENGID"].ToString();

            //wksheet_fp.Cells[7, 5] = dt_fp.Rows[0]["MS_PJNAME"].ToString();

            //wksheet_fp.Cells[7, 9] = dt_fp.Rows[0]["MS_ENGNAME"].ToString();

            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount];

            //for (int i = 0; i < rowCount; i++)
            //{

            //    for (int j = 0; j < colCount; j++)
            //    {

            //        dataArray[i, j] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Value2 = dataArray;
            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Borders.LineStyle = 1;
            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 6, colCount]).Font.Name = "宋体";
            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 6, colCount]).Font.Size = 8;
            ////***********列汇总
            //int row = rowCount + 5;
            //wksheet.Cells[row + 1, 2] = "TOTAL:";
            //string formula1 = "=SUM(J6:J" + row.ToString() + ")";
            //Range rg1 = wksheet.get_Range("J" + (row + 1).ToString(), System.Type.Missing);
            //rg1.Formula = formula1;
            //rg1.Calculate();//小计

            //string filename = System.Web.HttpContext.Current.Server.MapPath("制作明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 从原始数据中导出制作明细
        /// </summary>
        /// <param name="sqltext"></param>
        public static void ExportMsFromOrg(string taskid)
        {
            string filename = "产品明细表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("产品明细表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                ISheet sheet1 = wk.GetSheetAt(1);
                //根据批号查询数据
                string sql_select_data = "";

                sql_select_data = "select BM_TUHAO,BM_ZONGXU,BM_CHANAME+isnull(BM_MAGUIGE,''),BM_MAQUALITY,BM_SINGNUMBER,BM_PNUMBER,BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_MASHAPE,BM_TECHUNIT,BM_YONGLIANG,BM_MATOTALWGHT,BM_MALENGTH,BM_MAWIDTH,BM_NOTE,BM_XIALIAO,BM_PROCESS,isnull(BM_KU,'') as BM_KU,BM_ALLBEIZHU from View_TM_DQO where  BM_ENGID='" + taskid + "'  order by  dbo.f_formatstr(BM_ZONGXU, '.')";

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_data);

                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }

                string sqlText = "select TSA_ID,CM_PROJ,TSA_ENGNAME,TSA_PJID from View_TM_TaskAssign where TSA_ID='" + taskid + "'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlText);
                IRow row2 = sheet0.GetRow(2);
                row2.GetCell(1).SetCellValue(dt1.Rows[0]["CM_PROJ"].ToString());
                row2.GetCell(3).SetCellValue(dt1.Rows[0]["TSA_PJID"].ToString());

                IRow row3 = sheet0.GetRow(3);
                row3.GetCell(1).SetCellValue(dt1.Rows[0]["TSA_ID"].ToString());
                IRow row4 = sheet0.GetRow(4);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 2);
                    for (int j = 0; j < 16; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    string[] pross = new string[10];
                    for (int j = 0; j < dt.Rows[i][16].ToString().Split('-').Length; j++)
                    {
                        pross[j] = dt.Rows[i][16].ToString().Split('-')[j];
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        row.CreateCell(j + 16).SetCellValue(pross[j]);
                    }
                    for (int j = 17; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 9).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }


                sheet0.ForceFormulaRecalculation = true;
                sheet1.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }









        //Object Opt = System.Type.Missing;
        //Application m_xlApp = new Application();
        //Workbooks workbooks = m_xlApp.Workbooks;
        //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        //Worksheet wksheet;
        //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath( "产品明细表") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

        //m_xlApp.Visible = false;     // Excel不显示  
        //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

        //wksheet = (Worksheet)workbook.Sheets.get_Item(2);

        //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

        ////设置工作薄名称
        //string name = taskid.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-');
        //if (name.Length > 31)
        //{
        //    wksheet.Name = name.Substring(0, 31);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
        //}
        //else
        //{
        //    wksheet.Name = name;
        //}

        //string sql_fp = "select a.TSA_ENGNAME,a.TSA_ID,a.TSA_NUMBER,TSA_MAP,b.CM_PROJ,b.TSA_PJID,b.CM_COMP from TBCM_BASIC as a left join dbo.View_CM_TSAJOINPROJ as b on a.TSA_ID=b.TSA_ID where CM_ID='" + partId + "'";
        //System.Data.DataTable dt_fp = DBCallCommon.GetDTUsingSqlText(sql_fp);
        //Worksheet wksheet_fp = (Worksheet)workbook.Sheets.get_Item(1);
        //wksheet_fp.Cells[3, 2] = dt_fp.Rows[0]["CM_COMP"].ToString();
        //wksheet_fp.Cells[3, 4] = dt_fp.Rows[0]["TSA_PJID"].ToString();
        //wksheet_fp.Cells[3, 6] = dt_fp.Rows[0]["TSA_ENGNAME"].ToString();
        //wksheet_fp.Cells[4, 2] = taskid;
        //wksheet_fp.Cells[4, 4] = dt_fp.Rows[0]["TSA_ENGNAME"].ToString();
        //wksheet_fp.Cells[4, 6] = dt_fp.Rows[0]["TSA_NUMBER"].ToString();
        //wksheet_fp.Cells[5, 2] = dt_fp.Rows[0]["TSA_MAP"].ToString();
        //int rowCount = dt.Rows.Count;

        //int colCount = dt.Columns.Count;

        //object[,] dataArray = new object[rowCount, colCount + 7];

        //for (int i = 0; i < rowCount; i++)
        //{

        //    for (int j = 0; j < 16; j++)
        //    {

        //        dataArray[i, j] = dt.Rows[i][j];
        //    }
        //    string[] pross = new string[8];
        //    for (int j = 0; j < dt.Rows[i][16].ToString().Split('-').Length; j++)
        //    {
        //        pross[j] = dt.Rows[i][16].ToString().Split('-')[j];
        //    }
        //    for (int j = 0; j < 8; j++)
        //    {
        //        dataArray[i, j + 16] = pross[j];
        //    }
        //    for (int j = 17; j < colCount; j++)
        //    {
        //        dataArray[i, j + 7] = dt.Rows[i][j];
        //    }

        //}

        //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount + 7]).Value2 = dataArray;
        //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount + 7]).Borders.LineStyle = 1;
        //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount + 7]).Font.Name = "宋体";
        //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount + 7]).Font.Size = 8;

        //string filename = System.Web.HttpContext.Current.Server.MapPath("/TM_Data/ExportFile/" + "产品明细表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
        //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);


        /// <summary>
        /// 导出某一生产制号下材料计划
        /// </summary>
        /// <param name="exporttype"></param>
        /// <param name="taskid"></param>
        public static void ExportAllMpData(System.Data.DataTable dt, string proj, string engid, string engname, string matype)
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //if (matype != "定尺板")//定尺计划
            //{
            //    workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("材料需用计划表（JS-003-01)") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;
            //}
            //else //非定尺计划
            //{
            //    workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("定尺板材料需用计划表（JS-003-01)") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;
            //}

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            ////设置工作薄名称
            //string name = (engid + " 材料计划").Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-');
            //if (name.Length > 31)
            //{
            //    wksheet.Name = name.Substring(0, 31);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
            //}
            //else
            //{
            //    wksheet.Name = name;
            //}

            ////基本信息填充
            ////生产制号：    项目名称： 	工程名称： 	计划日期：    	计划编号： 
            //string str_basic = "";
            //str_basic = "生产制号：" + engid.Split('-')[0] + "";
            //str_basic += "  项目名称：" + proj;
            //str_basic += "  工程名称：" + engname;

            //if (matype == "定尺板")
            //{
            //    wksheet.Cells[3, 1] = str_basic;
            //    Range rghz = wksheet.get_Range(wksheet.Cells[3, 1], wksheet.Cells[3, 18]);
            //    rghz.MergeCells = true;
            //    rghz.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //}
            //else
            //{
            //    wksheet.Cells[3, 1] = str_basic;
            //    Range rghz = wksheet.get_Range(wksheet.Cells[3, 1], wksheet.Cells[3, 13]);
            //    rghz.MergeCells = true;
            //    rghz.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //}

            ////材料明细数据
            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = i + 1;
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        dataArray[i, j + 1] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount + 1]).Value2 = dataArray;
            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount - 1]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("材料计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 导出某一生产制号下所有外协计划
        /// </summary>
        /// <param name="exporttype"></param>
        /// <param name="taskid"></param>
        public static void ExportAllOutData(string exporttype, string taskid)
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("委外联系单") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            ////设置工作薄名称
            //string name = taskid.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-');
            //if (name.Length > 31)
            //{
            //    wksheet.Name = name.Substring(0, 31);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
            //}
            //else
            //{
            //    wksheet.Name = name;
            //}

            ////根据批号查询数据
            //string sql_select_basicdata = "select OST_ENGID,OST_PJNAME,OST_ENGNAME,OST_PJID,OST_MDATE,OST_ADATE from View_TM_OUTSOURCETOTAL where OST_ENGID like '" + taskid + "-%'" + ExportOutType(exporttype);
            //System.Data.DataTable dt_basicdata = DBCallCommon.GetDTUsingSqlText(sql_select_basicdata);
            //if (dt_basicdata.Rows.Count == 0)
            //{
            //    CloseExeclProcess(m_xlApp);
            //    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
            //    return;
            //}

            //////填充数据

            //#region 基本数据
            ////生产制号
            //wksheet.Cells[3, 3] = taskid.Split('-')[0];
            //Range rghz0 = wksheet.get_Range(wksheet.Cells[3, 3], wksheet.Cells[3, 4]);
            //rghz0.MergeCells = true;
            //rghz0.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //// 项目名称：       
            //wksheet.Cells[3, 6] = dt_basicdata.Rows[0]["OST_PJNAME"].ToString() + "(" + dt_basicdata.Rows[0]["OST_PJID"].ToString() + ")";
            //Range rghz1 = wksheet.get_Range(wksheet.Cells[3, 6], wksheet.Cells[3, 8]);
            //rghz1.MergeCells = true;
            //rghz1.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ////工程名称
            //wksheet.Cells[3, 11] = dt_basicdata.Rows[0]["OST_ENGNAME"].ToString() + "(" + dt_basicdata.Rows[0]["OST_ENGID"].ToString() + ")";
            //Range rghz2 = wksheet.get_Range(wksheet.Cells[3, 11], wksheet.Cells[3, 13]);
            //rghz2.MergeCells = true;
            //rghz2.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //////////////////////////////批准/日期:
            ////////////////////////////wksheet.Cells[4, 3] = dt_basicdata.Rows[0]["OST_ADATE"].ToString().Split(' ')[0];
            ////////////////////////////Range rghz3 = wksheet.get_Range(wksheet.Cells[4, 3], wksheet.Cells[4, 4]);
            ////////////////////////////rghz3.MergeCells = true;
            ////////////////////////////rghz3.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //////////////////////////////编制/日期：
            ////////////////////////////wksheet.Cells[4, 6] = dt_basicdata.Rows[0]["OST_MDATE"].ToString().Split(' ')[0];
            ////////////////////////////Range rghz4 = wksheet.get_Range(wksheet.Cells[4, 6], wksheet.Cells[4, 8]);
            ////////////////////////////rghz4.MergeCells = true;
            ////////////////////////////rghz4.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //////////////////////////////计划批号：	
            ////////////////////////////wksheet.Cells[4, 11] = lotnum;
            ////////////////////////////Range rghz5 = wksheet.get_Range(wksheet.Cells[4, 11], wksheet.Cells[4, 13]);
            ////////////////////////////rghz5.MergeCells = true;
            ////////////////////////////rghz5.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //#endregion

            //#region 详细数据
            //string sql_select_detail = "select ROW_NUMBER() OVER (ORDER BY OSL_TRACKNUM) AS RowNum,OSL_NAME,OSL_MARID,OSL_BIAOSHINO,OSL_GUIGE,OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,CASE WHEN OSL_REQUEST<>'' THEN OSL_REQUEST+';'+OSL_NOTE ELSE OSL_NOTE END,OSL_REQDATE,OSL_DELSITE,OSL_TRACKNUM from View_TM_OUTSOURCELIST where OSL_ENGID like '" + taskid + "-%' and OSL_STATUS='0' and OST_STATE='8' ";
            //sql_select_detail += ExportOutType(exporttype);
            //sql_select_detail += " order by OSL_TRACKNUM";

            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_detail);

            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount];

            //for (int i = 0; i < rowCount; i++)
            //{

            //    for (int j = 0; j < colCount; j++)
            //    {

            //        dataArray[i, j] = dt.Rows[i][j];

            //    }
            //}

            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Value2 = dataArray;
            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Borders.LineStyle = 1;

            //#endregion

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("外协计划表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }



        /// <summary>
        /// 去掉GridView列中相邻的重复项
        /// </summary>
        /// <param name="gridviewname"></param>
        /// <param name="columnindex"></param>
        public static void AbandonRepeatColumnCells(GridView gridviewname, int columnindex)
        {
            if (gridviewname.Rows.Count > 0)
            {
                int currentstarandrow;
                int nextstarandrow = 1;
                while (nextstarandrow < gridviewname.Rows.Count)
                {
                    currentstarandrow = nextstarandrow - 1;
                    string breakfirstfor = "0";
                    for (int i = currentstarandrow; i < gridviewname.Rows.Count; i++)
                    {
                        for (int j = i + 1; j < gridviewname.Rows.Count; j++)
                        {
                            nextstarandrow++;
                            if (gridviewname.Rows[currentstarandrow].Cells[columnindex].Text == gridviewname.Rows[j].Cells[columnindex].Text)
                            {
                                gridviewname.Rows[j].Cells[columnindex].Text = "";
                            }
                            else
                            {
                                breakfirstfor = "1";
                                break;
                            }
                        }

                        if (breakfirstfor == "1")
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 工程量管理模块，导出生产制号
        /// </summary>
        /// <param name="sqltext"></param>
        public static void ExportTaskID(string sqltext)
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("生产制号") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = i + 1;
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        dataArray[i, j + 1] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A2", wksheet.Cells[rowCount + 1, colCount + 1]).Value2 = dataArray;
            //wksheet.get_Range("A2", wksheet.Cells[rowCount + 1, colCount + 1]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("生产制号" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 正常明细调整时导出调整项下的全部数据
        /// </summary>
        /// <param name="sqltext"></param>
        public static void ExportNormalMSAdjust(string sqltext, string pjname, string taskid)
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("制作明细表(含是否体现)") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //wksheet.Cells[3, 6] = pjname;
            //Range rghz = wksheet.get_Range(wksheet.Cells[3, 6], wksheet.Cells[3, 8]);
            //rghz.MergeCells = true;
            //rghz.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //wksheet.Cells[3, 11] = taskid;
            //Range rghz1 = wksheet.get_Range(wksheet.Cells[3, 11], wksheet.Cells[3, 14]);
            //rghz1.MergeCells = true;
            //rghz1.HorizontalAlignment = XlHAlign.xlHAlignLeft;


            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        dataArray[i, j] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Value2 = dataArray;
            //wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("制作明细(包含不体现记录)" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 导出涂装方案
        /// </summary>
        /// <param name="sqltext"></param>
        public static void ExportPSData(string ps_lotnum)
        {


            string filename = "涂装细化方案.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("涂装细化方案.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);

                string sqlText = "select CM_PROJ,PS_PJID,TSA_ENGNAME,PS_PAINTBRAND,PS_ID,PS_SUBMITNM,PS_REVIEWANM,PS_REVIEWBNM from VIEW_TM_PAINTSCHEME where PS_ID='" + ps_lotnum + "'";

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }

                IRow row1 = sheet.GetRow(1);
                //18变为19
                row1.GetCell(19).SetCellValue(dt.Rows[0]["PS_ID"].ToString());

                IRow row3 = sheet.GetRow(3);
                //16变为17
                row3.GetCell(17).SetCellValue(dt.Rows[0]["PS_SUBMITNM"].ToString());

                IRow row4 = sheet.GetRow(4);
               //16变为17
                row4.GetCell(17).SetCellValue(dt.Rows[0]["PS_REVIEWANM"].ToString());

                IRow row5 = sheet.GetRow(5);
                //16变为17
                row5.GetCell(17).SetCellValue(dt.Rows[0]["PS_REVIEWBNM"].ToString());

                IRow row6 = sheet.GetRow(6);
                //2变为3，7变为8，,1变为13，16变为17
                row6.GetCell(3).SetCellValue(dt.Rows[0]["CM_PROJ"].ToString());
                row6.GetCell(8).SetCellValue(dt.Rows[0]["PS_PJID"].ToString());
                row6.GetCell(13).SetCellValue(dt.Rows[0]["TSA_ENGNAME"].ToString());
                row6.GetCell(17).SetCellValue(dt.Rows[0]["PS_PAINTBRAND"].ToString());

                sqlText = "select PS_ENGID,PS_NAME,PS_TUHAO,PS_LEVEL,PS_MIANJI,PS_BOTSHAPE,PS_BOTHOUDU,PS_BOTYONGLIANG,PS_BOTXISHIJI,PS_MIDSHAPE,PS_MIDHOUDU,PS_MIDYONGLIANG,PS_MIDXISHIJI,PS_TOPSHAPE,PS_TOPHOUDU,PS_TOPYONGLIANG,PS_TOPXISHIJI,PS_TOPCOLOR,PS_TOPCOLORLABEL,PS_TOTALHOUDU,PS_BEIZHU from View_TM_PAINTSCHEMEDETAIL where PS_PID='" + ps_lotnum + "'";
                dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                
                    IRow row = sheet.CreateRow(i + 9);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }


                }
                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }


            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("涂装细化方案") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            ////基本信息
            //string sql_basic = "select [PS_PAINTBRAND],[PS_ENGNAME],[PS_PJNAME],[PS_PJID],[PS_ENGID],[PS_SUBMITTM],[PS_SUBMITNM],[PS_REVIEWANM],[PS_REVIEWBNM],[PS_REVIEWCNM]  from VIEW_TM_PAINTSCHEME where PS_ID='" + ps_lotnum + "'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_basic);
            //if (dr.HasRows)
            //{
            //    dr.Read();
            //    wksheet.Cells[6, 1] = "  项目编号:" + dr["PS_PJID"].ToString() + "    项目名称:" + dr["PS_PJNAME"].ToString() + "    工程名称:" + dr["PS_ENGNAME"].ToString() + "(" + dr["PS_ENGID"].ToString().Substring(0, dr["PS_ENGID"].ToString().LastIndexOf('-')) + ")" + "    编制日期:" + dr["PS_SUBMITTM"].ToString().Substring(0, dr["PS_SUBMITTM"].ToString().LastIndexOf(' '));
            //    wksheet.Cells[3, 12] = dr["PS_SUBMITNM"].ToString();
            //    wksheet.Cells[4, 12] = dr["PS_REVIEWANM"].ToString();
            //    wksheet.Cells[5, 12] = dr["PS_REVIEWCNM"].ToString() != "" ? dr["PS_REVIEWCNM"].ToString() : dr["PS_REVIEWBNM"].ToString();
            //    wksheet.Cells[7, 4] = "油漆品牌:" + dr["PS_PAINTBRAND"].ToString();
            //    dr.Close();
            //}
            //Range rghz1 = wksheet.get_Range(wksheet.Cells[6, 1], wksheet.Cells[6, 12]);
            //rghz1.MergeCells = true;
            //rghz1.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //Range rghz2 = wksheet.get_Range(wksheet.Cells[7, 4], wksheet.Cells[7, 11]);
            //rghz2.MergeCells = true;
            //rghz2.HorizontalAlignment = XlHAlign.xlHAlignLeft;



            ////详细信息
            //string sqltext = "select [PS_NAME],[PS_LEVEL],[PS_PRIMER],[PS_PRIMERH],[PS_MIDPRIMER],[PS_MIDPRIMERH],[PS_TOPCOAT],[PS_TOPCOATH],[PS_COLOR],[PS_COLORLABEL],[PS_PAINTTOTAL] from [TBPM_PAINTSCHEMELIST] where [PS_PID]='" + ps_lotnum + "'";
            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = i + 1;
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        dataArray[i, j + 1] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A9", wksheet.Cells[rowCount + 8, colCount + 1]).Value2 = dataArray;
            //wksheet.get_Range("A9", wksheet.Cells[rowCount + 8, colCount + 1]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("涂装方案" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 导出生产制号
        /// </summary>
        /// <param name="sqltext"></param>
        public static void ExportTaskIDContainsOld(string sqltext)
        {
            //    Object Opt = System.Type.Missing;
            //    Application m_xlApp = new Application();
            //    Workbooks workbooks = m_xlApp.Workbooks;
            //    Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //    Worksheet wksheet;
            //    workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("技术员任务-导出生产制号") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //    m_xlApp.Visible = false;     // Excel不显示  
            //    m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //    wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //    int rowCount = dt.Rows.Count;

            //    int colCount = dt.Columns.Count;

            //    object[,] dataArray = new object[rowCount, colCount + 1];

            //    for (int i = 0; i < rowCount; i++)
            //    {
            //        dataArray[i, 0] = i + 1;
            //        for (int j = 0; j < colCount; j++)
            //        {
            //            dataArray[i, j + 1] = dt.Rows[i][j];
            //        }
            //    }

            //    wksheet.get_Range("A2", wksheet.Cells[rowCount + 1, colCount + 1]).Value2 = dataArray;
            //    wksheet.get_Range("A2", wksheet.Cells[rowCount + 1, colCount + 1]).Borders.LineStyle = 1;

            //    ////设置列宽
            //    wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //    string filename = System.Web.HttpContext.Current.Server.MapPath("生产制号" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //    ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 导出角色权限
        /// </summary>
        /// <param name="sqltext"></param>
        public static void ExportRole()
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("角色权限") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //string sqltext = "select R_ID,R_NAME,FatherName,FatherID,NAME,PAGE_ID,IDPATH,PAGELEVEL from View_Role_Detail order by R_XH";
            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //int rowCount = dt.Rows.Count;
            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount + 1];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = i + 1;
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        dataArray[i, j + 1] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A2", wksheet.Cells[rowCount + 1, colCount + 1]).Value2 = dataArray;
            //wksheet.get_Range("A2", wksheet.Cells[rowCount + 1, colCount + 1]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("角色权限" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 导出物料信息
        /// </summary>
        /// <param name="sqltext"></param>
        public static void ExportMaterial(string sqltext)
        {
            string filename = "物料信息.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("../Basic_Data/物料信息.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                int rowCount = dt.Rows.Count;
                int colCount = dt.Columns.Count;

                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }


                for (int i = 0; i < rowCount; i++)
                {
                    IRow row = sheet.CreateRow(i + 2);
                    for (int j = 0; j < colCount; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }


                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }

        }
        // 导出厂商审批
        public static void ExportCusupinfo_Review(string sqltext)
        {
            string filename = "厂商审批信息.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("../Basic_Data/厂商审批信息.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                int rowCount = dt.Rows.Count;
                int colCount = dt.Columns.Count;

                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }


                for (int i = 0; i < rowCount; i++)
                {
                    IRow row = sheet.CreateRow(i + 2);
                    for (int j = 0; j < colCount; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }


                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 采购申请导出
        /// </summary>
        /// <param name="martype"></param>
        /// <param name="mpno"></param>
        public static void ExportPurPlan(string martype, string mpno)
        {
            //    Object Opt = System.Type.Missing;
            //    Application m_xlApp = new Application();
            //    Workbooks workbooks = m_xlApp.Workbooks;
            //    Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //    Worksheet wksheet;

            //    switch (martype)
            //    {
            //        case "定尺板":
            //            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("~/TM_Data/定尺板材料需用计划表（JS-003-01)") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt);
            //            break;
            //        case "协A":
            //            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("~/TM_Data/委外联系单") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt);
            //            break;
            //        case "协B":
            //            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("~/TM_Data/委外联系单") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt);
            //            break;
            //        default:
            //            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("~/TM_Data/材料需用计划表（JS-003-01)") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt);
            //            break;
            //    }

            //    m_xlApp.Visible = false;     // Excel不显示  
            //    m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //    wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //    string sqltext;

            //    switch (martype)
            //    {
            //        case "定尺板":
            //            sqltext = "SELECT [PCODE],[PJID],[PJNM],[ENGID],[ENGNM],[TJDATE],[MARID],[MARNM] ,'δ=' as A ,[MARGG],'×' as B,[LENGTH],'×' as C,[WIDTH],[MARCZ],CASE WHEN [FZUNIT] IS NULL OR [FZUNIT]='' THEN [UNIT] ELSE [UNIT]+'/'+[FZUNIT] END, [NUM],[FZNUM], '' AS TZJCC,'' AS CLLB,'' CFCD,'' AS HJYX, [DETAILNOTE],[PTCODE] from [View_OTHER_PLAN_RVW] where [PCODE]='" + mpno + "'";
            //            break;
            //        case "协A":
            //            sqltext = "SELECT [PCODE],[PJID],[PJNM],[ENGID],[ENGNM],[TJDATE],[MARNM],[MARID],[MP_TUHAO],[MARGG],[MARCZ],'' AS UNITWGHT,[NUM],CASE WHEN [FZUNIT]='吨' or [FZUNIT]='T' or [FZUNIT]='t' THEN [FZNUM]*1000 ELSE [FZNUM] END, '采购部',[DETAILNOTE],[TIMERQ],'',[PTCODE] from [View_OTHER_PLAN_RVW] where [PCODE]='" + mpno + "'";
            //            break;
            //        case "协B":
            //            sqltext = "SELECT [PCODE],[PJID],[PJNM],[ENGID],[ENGNM],[TJDATE],[MARNM],[MARID],[MP_TUHAO],[MARGG],[MARCZ],'' AS UNITWGHT,[NUM],CASE WHEN [FZUNIT]='吨' or [FZUNIT]='T' or [FZUNIT]='t' THEN [FZNUM]*1000 ELSE [FZNUM] END, '采购部',[DETAILNOTE],[TIMERQ],'',[PTCODE] from [View_OTHER_PLAN_RVW] where [PCODE]='" + mpno + "'";
            //            break;
            //        default:
            //            sqltext = "SELECT [PCODE],[PJID],[PJNM],[ENGID],[ENGNM],[TJDATE],[MARID],[MP_TUHAO],[MARNM],[MARGG],[MARCZ],CASE WHEN [FZUNIT] IS NULL OR [FZUNIT]='' THEN [UNIT] ELSE [UNIT]+'/'+[FZUNIT] END,[NUM],[FZNUM],'' AS  CLLB,'' AS YT,[TIMERQ],'' AS  HBYQ,[DETAILNOTE],[PTCODE] from [View_OTHER_PLAN_RVW] where [PCODE]='" + mpno + "'";
            //            break;
            //    }

            //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //    int rowCount = dt.Rows.Count;

            //    int colCount = dt.Columns.Count - 6;

            //    object[,] dataArray = new object[rowCount, colCount + 1];

            //    for (int i = 0; i < rowCount; i++)
            //    {
            //        dataArray[i, 0] = i + 1;
            //        for (int j = 0; j < colCount; j++)
            //        {
            //            dataArray[i, j + 1] = dt.Rows[i][j + 6];
            //        }
            //    }

            //    string str_basic = "";
            //    str_basic = "生产制号：" + dt.Rows[0]["ENGID"].ToString().Split('-')[0] + "  ";
            //    str_basic += "项目名称：" + dt.Rows[0]["PJNM"].ToString() + "(" + dt.Rows[0]["PJID"] + ")" + "  ";
            //    str_basic += "工程名称：" + dt.Rows[0]["ENGNM"].ToString() + "  ";
            //    str_basic += "计划日期：" + dt.Rows[0]["TJDATE"].ToString().Split(' ')[0] + "  ";
            //    str_basic += "计划编号：" + mpno;


            //    switch (martype)
            //    {
            //        case "定尺板":
            //            wksheet.Cells[3, 1] = str_basic;
            //            Range rghz = wksheet.get_Range(wksheet.Cells[3, 1], wksheet.Cells[3, 13]);
            //            rghz.MergeCells = true;
            //            rghz.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //            break;
            //        case "协A":
            //            //生产制号
            //            wksheet.Cells[3, 3] = dt.Rows[0]["ENGID"].ToString();
            //            Range rghz0 = wksheet.get_Range(wksheet.Cells[3, 3], wksheet.Cells[3, 4]);
            //            rghz0.MergeCells = true;
            //            rghz0.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            // 项目名称：       
            //            wksheet.Cells[3, 6] = dt.Rows[0]["PJNM"].ToString() + "(" + dt.Rows[0]["PJID"].ToString() + ")";
            //            Range rghz1 = wksheet.get_Range(wksheet.Cells[3, 6], wksheet.Cells[3, 8]);
            //            rghz1.MergeCells = true;
            //            rghz1.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            //工程名称
            //            wksheet.Cells[3, 11] = dt.Rows[0]["ENGNM"].ToString() + "(" + dt.Rows[0]["ENGID"].ToString() + ")";
            //            Range rghz2 = wksheet.get_Range(wksheet.Cells[3, 11], wksheet.Cells[3, 13]);
            //            rghz2.MergeCells = true;
            //            rghz2.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            //批准/日期:
            //            wksheet.Cells[4, 3] = "";
            //            Range rghz3 = wksheet.get_Range(wksheet.Cells[4, 3], wksheet.Cells[4, 4]);
            //            rghz3.MergeCells = true;
            //            rghz3.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            //编制/日期：
            //            wksheet.Cells[4, 6] = dt.Rows[0]["TJDATE"].ToString().Split(' ')[0];
            //            Range rghz4 = wksheet.get_Range(wksheet.Cells[4, 6], wksheet.Cells[4, 8]);
            //            rghz4.MergeCells = true;
            //            rghz4.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //            //计划批号：	
            //            wksheet.Cells[4, 11] = mpno;
            //            Range rghz5 = wksheet.get_Range(wksheet.Cells[4, 11], wksheet.Cells[4, 13]);
            //            rghz5.MergeCells = true;
            //            rghz5.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //            break;
            //        case "协B":
            //            //生产制号
            //            wksheet.Cells[3, 3] = dt.Rows[0]["ENGID"].ToString();
            //            Range rghz00 = wksheet.get_Range(wksheet.Cells[3, 3], wksheet.Cells[3, 4]);
            //            rghz00.MergeCells = true;
            //            rghz00.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            // 项目名称：       
            //            wksheet.Cells[3, 6] = dt.Rows[0]["PJNM"].ToString() + "(" + dt.Rows[0]["PJID"].ToString() + ")";
            //            Range rghz11 = wksheet.get_Range(wksheet.Cells[3, 6], wksheet.Cells[3, 8]);
            //            rghz11.MergeCells = true;
            //            rghz11.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            //工程名称
            //            wksheet.Cells[3, 11] = dt.Rows[0]["ENGNM"].ToString() + "(" + dt.Rows[0]["ENGID"].ToString() + ")";
            //            Range rghz22 = wksheet.get_Range(wksheet.Cells[3, 11], wksheet.Cells[3, 13]);
            //            rghz22.MergeCells = true;
            //            rghz22.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            //批准/日期:
            //            wksheet.Cells[4, 3] = "";
            //            Range rghz33 = wksheet.get_Range(wksheet.Cells[4, 3], wksheet.Cells[4, 4]);
            //            rghz33.MergeCells = true;
            //            rghz33.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            //编制/日期：
            //            wksheet.Cells[4, 6] = dt.Rows[0]["TJDATE"].ToString().Split(' ')[0];
            //            Range rghz44 = wksheet.get_Range(wksheet.Cells[4, 6], wksheet.Cells[4, 8]);
            //            rghz44.MergeCells = true;
            //            rghz44.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //            //计划批号：	
            //            wksheet.Cells[4, 11] = mpno;
            //            Range rghz55 = wksheet.get_Range(wksheet.Cells[4, 11], wksheet.Cells[4, 13]);
            //            rghz55.MergeCells = true;
            //            rghz55.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            break;
            //        default:
            //            wksheet.Cells[3, 1] = str_basic;
            //            Range rghzd = wksheet.get_Range(wksheet.Cells[3, 1], wksheet.Cells[3, 18]);
            //            rghzd.MergeCells = true;
            //            rghzd.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //            break;
            //    }

            //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount + 1]).Value2 = dataArray;
            //    wksheet.get_Range("A6", wksheet.Cells[rowCount + 5, colCount + 1]).Borders.LineStyle = 1;

            //    ////设置列宽
            //    wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //    string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //    ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            //}

            //public static void DeleteFiles()
            //{
            //    string path = System.Web.HttpContext.Current.Server.MapPath("/TM_Data/ExportFile");
            //    if (!Directory.Exists(path))//创建文件夹
            //    {
            //        Directory.CreateDirectory(path);
            //    }

            //    foreach (string fileName in Directory.GetFiles(path))
            //    {
            //        string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            //        System.IO.File.Delete(path + "\\" + newName);//删除文件下储存的文件
            //    }
        }
    }
}
