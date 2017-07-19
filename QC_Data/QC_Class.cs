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
using System.Data.SqlClient;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.QC_Data
{
    public class QC_Class
    {
        //导出不合格品通知单信息
        public static void ExportDataItem(System.Data.DataTable dt)
        {

            string filename = "不合格品台账.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("不合格品台账.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 3);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        try
                        {
                            row.GetCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        }
                        catch (Exception)
                        {

                            row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        }
                        
                    }
                }


                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
