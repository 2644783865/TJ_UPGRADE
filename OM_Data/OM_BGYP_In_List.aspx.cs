using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYP_In_List : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUser(ControlFinder);
        }




        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_export_Click(object sender, EventArgs e)
        {

            string sqltext = ""; ;

            sqltext += "select * from View_OM_BGYP_IN order by MakeTime desc ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }



        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "办公用品入库" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("办公用品入库.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));

                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["rkcode"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["maId"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["name"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["canshu"].ToString());

                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["num"].ToString());
                    row.CreateCell(6).SetCellValue("" + objdt.Rows[i]["unit"].ToString());
                    row.CreateCell(7).SetCellValue("" + objdt.Rows[i]["uprice"].ToString());
                    row.CreateCell(8).SetCellValue("" + objdt.Rows[i]["price"].ToString());
                    row.CreateCell(9).SetCellValue("" + objdt.Rows[i]["MakerNM"].ToString());
                    row.CreateCell(10).SetCellValue("" + objdt.Rows[i]["MakeTime"].ToString());
                    row.CreateCell(11).SetCellValue("" + objdt.Rows[i]["note"].ToString());
                }

                for (int i = 0; i <= objdt.Columns.Count; i++)
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
    }
}
