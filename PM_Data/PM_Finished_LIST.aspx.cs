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
using System.Text;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Finished_LIST : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> list = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region 导出
        protected void btndaochu_click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select a.*,b.*,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state,c.CM_PROJ from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU left join View_CM_TSAJOINPROJ as c on a.BM_ENGID=c.TSA_ID where dbo.Splitnum(BM_ZONGXU,'.')=0 and BM_PJID not like 'JSB.%' and  BM_MARID='' ";
            if (hidRWH.Value != "")
            {
                sqltext += " and BM_ENGID LIKE '%" + hidRWH.Value + "%'";
            }
            if (hidTH.Value != "")
            {
                sqltext += " and BM_TUHAO LIKE '%" + hidTH.Value + "%'";
            }
            if (hidMC.Value != "")
            {
                sqltext += " and BM_CHANAME LIKE '%" + hidMC.Value + "%'";
            }
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "成品库存查询" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("成品库存查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + dt.Rows[i]["BM_ZONGXU"].ToString());
                    row.CreateCell(2).SetCellValue("" + dt.Rows[i]["BM_ENGID"].ToString());
                    row.CreateCell(3).SetCellValue("" + dt.Rows[i]["CM_PROJ"].ToString());
                    row.CreateCell(4).SetCellValue("" + dt.Rows[i]["BM_TUHAO"].ToString());
                    row.CreateCell(5).SetCellValue("" + dt.Rows[i]["BM_CHANAME"].ToString());
                    row.CreateCell(6).SetCellValue("" + dt.Rows[i]["bm_tuunitwght"].ToString());
                    row.CreateCell(7).SetCellValue("" + dt.Rows[i]["bm_singnumber"].ToString());
                    row.CreateCell(8).SetCellValue("" + dt.Rows[i]["bm_ku"].ToString());
                    row.CreateCell(9).SetCellValue("" + dt.Rows[i]["kc_rknum"].ToString());
                    row.CreateCell(10).SetCellValue("" + dt.Rows[i]["kc_cknum"].ToString());
                    row.CreateCell(11).SetCellValue("" + dt.Rows[i]["kc_kcnum"].ToString());
                    row.CreateCell(12).SetCellValue("" + dt.Rows[i]["bm_number"].ToString());
                }
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
        #endregion

    }
}
