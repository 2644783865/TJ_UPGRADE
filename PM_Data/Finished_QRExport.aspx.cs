using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class Finished_QRExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_zongxulist.Text = "";
            }
        }


        //任务号验证
        public void sfczbhgx(object sender, EventArgs e)
        {
            string tsaid = "";
            string resulttsaid = "";
            List<string> afiid = new List<string>();
            string resultafiid = "";
            string resultvalid = "";
            if (txt_tsa.Text == null || txt_tsa.Text == "")
            {
                resultvalid = txt_tsa.Text.ToString().Trim();
            }
            else
            {
                string sfczgrwh = "SELECT distinct TSA_ID  as Expr1 from  View_CM_Task  WHERE  CM_SPSTATUS='2' AND TSA_ID ='" + txt_tsa.Text.Trim() + "'";
                DataTable dtsfczgrwh = DBCallCommon.GetDTUsingSqlText(sfczgrwh);
                if (dtsfczgrwh.Rows.Count > 0)
                {
                    tsaid = txt_tsa.Text;
                    string sqlquality = "select distinct AFI_ID,PTC,AFI_ENDDATE,AFI_QCMANNM from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC like '%" + tsaid + "%'and RESULT not  in ('合格','让步接收') order by AFI_ENDDATE desc";
                    DataTable dtquality = DBCallCommon.GetDTUsingSqlText(sqlquality);
                    if (dtquality.Rows.Count > 0)
                    {
                        string sqlselect = "";
                        DataTable dtselect;
                        DataView dvselect;
                        for (int i = 0; i < dtquality.Rows.Count; i++)
                        {
                            sqlselect = "select ISAGAIN,AFI_ID,PTC,isnull(RESULT,'')RESULT,AFI_ENDRESLUT,AFI_ENDDATE,AFI_QCMANNM from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC='" + dtquality.Rows[i]["PTC"] + "'and isnull(AFI_ENDDATE,'') =isnull((select top 1 AFI_ENDDATE from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC='" + dtquality.Rows[i]["PTC"] + "' order by AFI_ENDDATE desc),'') order by ISAGAIN desc";
                            dtselect = DBCallCommon.GetDTUsingSqlText(sqlselect);
                            if (dtselect.Rows[0]["RESULT"].ToString() != "合格" && dtselect.Rows[0]["RESULT"].ToString() != "让步接收")
                            {
                                resulttsaid = tsaid;
                                dvselect = dtselect.DefaultView;
                                dvselect.RowFilter = "RESULT not in ('合格','让步接收')";
                                for (int j = 0; j < dvselect.Count; j++)
                                {
                                    if (!afiid.Contains(dvselect[j]["AFI_ID"].ToString().PadLeft(5, '0')))
                                        afiid.Add(dvselect[j]["AFI_ID"].ToString().PadLeft(5, '0'));
                                }
                            }
                        }
                        for (int k = 0; k < afiid.Count; k++)
                        {
                            resultafiid += afiid[k] + "/";
                        }
                        resultvalid = resulttsaid != "" ? "任务号【" + resulttsaid + "】下存在未经质检或检验不合格的报检子项,质检编号为【" + resultafiid.TrimEnd('/') + "】,仍需提交请点击【提交】按钮" : "";
                    }
                }
                else
                {
                    tsaid = txt_tsa.Text;
                    resultvalid = "任务号【" + tsaid + "】不存在";
                }
            }
            bhgts.Text = resultvalid;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string zongxulist = "";
            if (txt_zongxulist.Text.Trim() != "")
            {
                zongxulist = txt_zongxulist.Text.Trim().Replace(";", "','");
                zongxulist = "'" + zongxulist + "'";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要导出的数据！');", true);
                return;
            }
            sqltext = "select BM_ZONGXU,BM_TUHAO,BM_CHANAME,BM_TUUNITWGHT,BM_KU from dbo.View_TM_DQO where BM_ZONGXU in(" + zongxulist + ") and BM_ENGID='" + txt_tsa.Text.Trim() + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "成品二维码信息" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("成品二维码导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.HeightInPoints = 14;//行高
                    row.CreateCell(0).SetCellValue(txt_tsa.Text.Trim() + dt.Rows[i]["BM_ZONGXU"].ToString());//二维码信息
                    row.CreateCell(1).SetCellValue(txt_tsa.Text.Trim());//任务号
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["BM_ZONGXU"].ToString());//总序
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["BM_TUHAO"].ToString());//图号
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["BM_CHANAME"].ToString());//名称
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["BM_TUUNITWGHT"].ToString());//单重
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["BM_KU"].ToString());//库
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 6; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
