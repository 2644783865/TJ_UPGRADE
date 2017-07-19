using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ZCZJ_DPF
{
    public class exportCommanmethod
    {
        /// <summary>
        /// 批量导出
        /// </summary>
        /// <param name="sqltext">查询语句</param>
        /// <param name="filename">文件名称(.xls)</param>
        /// <param name="filestandard">模板名称(.xls)</param>
        /// <param name="ifwidth">是否自动列宽</param>
        /// <param name="ifmyownstyle">是否使用自动样式</param>
        /// <param name="ifxuhao">是否需要序号列</param>
        public static void exporteasy(string sqltext, string filename, string filestandard, bool ifwidth, bool ifmyownstyle, bool ifxuhao)
        {
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath(filestandard)))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);

                //个性样式
                NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                font1.FontName = "仿宋";//字体
                font1.FontHeightInPoints = 11;//字号

                ICellStyle cells = wk.CreateCellStyle();
                cells.SetFont(font1);
                cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    if (ifxuhao)
                    {
                        ICell cell0 = row.CreateCell(0);
                        cell0.SetCellValue(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string str = dt.Rows[i][j].ToString();
                            row.CreateCell(j + 1).SetCellValue(str);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string str = dt.Rows[i][j].ToString();
                            row.CreateCell(j).SetCellValue(str);
                        }
                    }
                    if (ifmyownstyle & ifxuhao)
                    {
                        for (int j = 0; j < dt.Columns.Count + 1; j++)
                        {
                            row.Cells[j].CellStyle = cells;
                        }
                    }
                    else if (ifmyownstyle & !ifxuhao)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row.Cells[j].CellStyle = cells;
                        }
                    }
                }

                if (ifwidth & ifxuhao)
                {
                    for (int r = 0; r <= dt.Columns.Count; r++)
                    {
                        sheet1.AutoSizeColumn(r);
                    }
                }
                else if (ifwidth == true & ifxuhao == false)
                {
                    for (int r = 0; r < dt.Columns.Count; r++)
                    {
                        sheet1.AutoSizeColumn(r);
                    }
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 批量导出
        /// </summary>
        /// <param name="sqltext">查询语句</param>
        /// <param name="filename">文件名称(.xls)</param>
        /// <param name="filestandard">模板名称(.xls)</param>
        /// <param name="row_hang">从第几行开始写入(.xls)</param>
        /// <param name="ifwidth">是否自动列宽</param>
        /// <param name="ifmyownstyle">是否使用自动样式</param>
        /// <param name="ifxuhao">是否需要序号列</param>
        public static void exporteasy(string sqltext, string filename, string filestandard, int row_hang, bool ifwidth, bool ifmyownstyle, bool ifxuhao)
        {
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath(filestandard)))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);

                //个性样式
                NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                font1.FontName = "仿宋";//字体
                font1.FontHeightInPoints = 11;//字号

                ICellStyle cells = wk.CreateCellStyle();
                cells.SetFont(font1);
                cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + row_hang);
                    if (ifxuhao)
                    {
                        ICell cell0 = row.CreateCell(0);
                        cell0.SetCellValue(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string str = dt.Rows[i][j].ToString();
                            row.CreateCell(j + 1).SetCellValue(str);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string str = dt.Rows[i][j].ToString();
                            row.CreateCell(j).SetCellValue(str);
                        }
                    }
                    if (ifmyownstyle & ifxuhao)
                    {
                        for (int j = 0; j < dt.Columns.Count + 1; j++)
                        {
                            row.Cells[j].CellStyle = cells;
                        }
                    }
                    else if (ifmyownstyle & !ifxuhao)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row.Cells[j].CellStyle = cells;
                        }
                    }
                }

                if (ifwidth & ifxuhao)
                {
                    for (int r = 0; r <= dt.Columns.Count; r++)
                    {
                        sheet1.AutoSizeColumn(r);
                    }
                }
                else if (ifwidth == true & ifxuhao == false)
                {
                    for (int r = 0; r < dt.Columns.Count; r++)
                    {
                        sheet1.AutoSizeColumn(r);
                    }
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
