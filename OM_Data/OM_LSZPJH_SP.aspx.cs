using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.Util;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LSZPJH_SP : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindrpt();
                //MergeCells();
            }
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
            //MergeCells();
        }

        private void bindrpt()
        {
            pager_org.TableName = "OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID";
            pager_org.PrimaryKey = "JH_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "JH_HZSJ,JH_ZPBM";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptZPJH, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private string StrWhere()
        {
            string sql = "SPZT<>'0' and SPLX='LSZPJH' ";
            if (rblRW.SelectedValue == "1")
            {
                sql += " and ((SPR1 like '%" + username + "%' and SPR1_JL ='' and SPZT='1') or (SPR2 like '%" + username + "%' and SPR2_JL ='' and SPZT='1y') or (SPR3 like '%" + username + "%' and SPR3_JL ='' and SPZT='2y'))";
            }
            if (ddlSPZT.SelectedValue == "1")
            {
                sql += " and SPZT='0'";
            }
            else if (ddlSPZT.SelectedValue == "2")
            {
                sql += " and SPZT='1'";
            }
            else if (ddlSPZT.SelectedValue == "3")
            {
                sql += " and SPZT in ('1y','2y','3y')";
            }
            else if (ddlSPZT.SelectedValue == "4")
            {
                sql += " and SPZT='y'";
            }
            else if (ddlSPZT.SelectedValue == "5")
            {
                sql += " and SPZT='n'";
            }
            if (txtDH.Text.Trim() != "")
            {
                sql += " and substring(JH_HZSJ,1,8) like'%" + txtDH.Text.Trim() + "%'";
            }
            if (txtBM.Text.Trim() != "")
            {
                sql += " and JH_ZPBM like'%" + txtBM.Text.Trim() + "%'";
            }
            if (txtGW.Text.Trim() != "")
            {
                sql += " and JH_GWMC like'%" + txtGW.Text.Trim() + "%'";
            }
            return sql;
        }
        #endregion

        private void MergeCells()//合并单元格
        {
            //for (int i = rptZPJH.Items.Count - 1; i > 0; i--)
            //{
            //    HtmlTableCell tdJH_HZBH = (HtmlTableCell)rptZPJH.Items[i].FindControl("tdJH_HZBH");
            //    HtmlTableCell tdJH_ZPBM = (HtmlTableCell)rptZPJH.Items[i].FindControl("tdJH_ZPBM");
            //    HtmlTableCell tdJH_GWMC = (HtmlTableCell)rptZPJH.Items[i].FindControl("tdJH_GWMC");

            //    HtmlTableCell tdJH_HZBH1 = (HtmlTableCell)rptZPJH.Items[i - 1].FindControl("tdJH_HZBH");
            //    HtmlTableCell tdJH_ZPBM1 = (HtmlTableCell)rptZPJH.Items[i - 1].FindControl("tdJH_ZPBM");
            //    HtmlTableCell tdJH_GWMC1 = (HtmlTableCell)rptZPJH.Items[i - 1].FindControl("tdJH_GWMC");

            //    tdJH_HZBH.RowSpan = (tdJH_HZBH.RowSpan == -1) ? 1 : tdJH_HZBH.RowSpan;
            //    tdJH_ZPBM.RowSpan = (tdJH_ZPBM.RowSpan == -1) ? 1 : tdJH_ZPBM.RowSpan;
            //    tdJH_GWMC.RowSpan = (tdJH_GWMC.RowSpan == -1) ? 1 : tdJH_GWMC.RowSpan;
            //    tdJH_HZBH1.RowSpan = (tdJH_HZBH1.RowSpan == -1) ? 1 : tdJH_HZBH1.RowSpan;
            //    tdJH_ZPBM1.RowSpan = (tdJH_ZPBM1.RowSpan == -1) ? 1 : tdJH_ZPBM1.RowSpan;
            //    tdJH_GWMC1.RowSpan = (tdJH_GWMC1.RowSpan == -1) ? 1 : tdJH_GWMC1.RowSpan;

            //    if (tdJH_HZBH.InnerText == tdJH_HZBH1.InnerText)
            //    {
            //        tdJH_HZBH.Visible = false;
            //        tdJH_HZBH1.RowSpan += tdJH_HZBH.RowSpan;
            //    }

            //    if (tdJH_ZPBM.InnerText == tdJH_ZPBM1.InnerText)
            //    {
            //        tdJH_ZPBM.Visible = false;
            //        tdJH_ZPBM1.RowSpan += tdJH_ZPBM.RowSpan;
            //    }
            //    if (tdJH_GWMC.InnerText == tdJH_GWMC1.InnerText)
            //    {
            //        tdJH_GWMC.Visible = false;
            //        tdJH_GWMC1.RowSpan += tdJH_GWMC.RowSpan;
            //    }
            //}
        }

        protected void Query(object sender, EventArgs e)//查询事件
        {
            bindrpt();
            //MergeCells();
        }

        private class asd
        {
            public static string bh;
            public static string hzsj;
        }

        protected void rptZPJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                asd.bh = string.Empty;
                asd.hzsj = string.Empty;
                return;
            }
            HyperLink hplCheck = (HyperLink)e.Item.FindControl("hplCheck");
            hplCheck.Visible = false;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            if (drv["SPZT"].ToString() == "1")
            {
                if (username == drv["SPR1"].ToString())
                {
                    hplCheck.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "1y")
            {
                if (username == drv["SPR2"].ToString())
                {
                    hplCheck.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "2y")
            {
                if (username == drv["SPR3"].ToString())
                {
                    hplCheck.Visible = true;
                }
            }

            //去重复显示
            if (drv["JH_HZSJ"].ToString() == asd.hzsj)
            {
                hplCheck.Visible = false;
            }
            else
            {
                asd.hzsj = drv["JH_HZSJ"].ToString();
            }
            if (drv["JH_HZBH"].ToString() == asd.bh)
            {
                e.Item.FindControl("JH_HZBH").Visible = false;
            }
            else
            {
                asd.bh = drv["JH_HZBH"].ToString();
            }
        }
        #region 导出
        protected void btnDaoChu_onserverclick(object sender, EventArgs e)
        {
            for (int i = 0, length = rptZPJH.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptZPJH.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string JH_HZBH = ((Label)rptZPJH.Items[i].FindControl("JH_HZBH")).Text;
                    string sql = "select * from OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID where JH_HZBH='" + JH_HZBH + "' order by JH_ZPBM,JH_GWMCID";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ExportDataItem(dt);
                    }
                    else
                    {
                        Response.Write("<script>alert('请勾选一个单据，再打印！！！')</script>");
                        return;
                    }
                    break;
                }
            }
        }

        private void ExportDataItem(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            string filename = "招聘计划--" + dr["JH_HZBH"].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("招聘计划导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    IRow row = sheet0.CreateRow(i + 4);
                    row.HeightInPoints = 20;
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["JH_HZBH"].ToString());
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["JH_ZPBM"].ToString());
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["JH_GWMC"].ToString());
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["JH_XQLY"].ToString());
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["JH_ZPFS"].ToString());
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["JH_ZPRS"].ToString());
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["JH_ZPGW"].ToString());
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["JH_ZPZY"].ToString());
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["JH_ZPYX"].ToString());
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["JH_ZPXL"].ToString());
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["JH_ZPXB"].ToString());
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["JH_ZPNL"].ToString());
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["JH_ZPYQ"].ToString());
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["JH_QTYQ"].ToString());
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["JH_XWDGSJ"].ToString());
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["JH_NGZDD"].ToString());
                    row.CreateCell(17).SetCellValue(dt.Rows[i]["JH_QT"].ToString());

                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 9;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 17; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                int num = 0;
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    num += Convert.ToInt32(dt.Rows[i]["JH_ZPRS"].ToString());
                }

                IRow rowhz = sheet0.CreateRow(dt.Rows.Count + 4);
                for (int i = 0; i <= 17; i++)
                {
                    rowhz.CreateCell(i);
                }
                rowhz.GetCell(0).SetCellValue("人数合计");
                rowhz.GetCell(6).SetCellValue(num);
                rowhz.HeightInPoints = 20;
                NPOI.SS.UserModel.IFont font2 = wk.CreateFont();
                font2.FontName = "仿宋";//字体
                font2.FontHeightInPoints = 9;//字号
                ICellStyle cells2 = wk.CreateCellStyle();
                cells2.SetFont(font2);
                cells2.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cells2.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cells2.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cells2.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                cells2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                cells2.VerticalAlignment = VerticalAlignment.CENTER;
                for (int i = 0; i <= 17; i++)
                {
                    rowhz.Cells[i].CellStyle = cells2;
                }
                CellRangeAddress range1 = new CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 0, 5);
                sheet0.AddMergedRegion(range1);
                CellRangeAddress range2 = new CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 7, 17);
                sheet0.AddMergedRegion(range2);

                IRow rowqz1 = sheet0.CreateRow(dt.Rows.Count + 6);
                for (int i = 0; i <= 17; i++)
                {
                    rowqz1.CreateCell(i);
                }
                rowqz1.GetCell(0).SetCellValue("编制负责人：");
                rowqz1.GetCell(2).SetCellValue(dr["ZDR"].ToString());
                rowqz1.GetCell(5).SetCellValue("申报单位/部门主管：");
                rowqz1.GetCell(7).SetCellValue(dr["SPR1"].ToString());
                rowqz1.GetCell(8).SetCellValue(dr["SPR1_JL"].ToString() == "y" ? "同意" : dr["SPR1_JL"].ToString() == "n" ? "不同意" : "");
                rowqz1.GetCell(12).SetCellValue("集团公司主管领导：");
                rowqz1.GetCell(14).SetCellValue(dr["SPR2"].ToString());
                rowqz1.GetCell(15).SetCellValue(dr["SPR2_JL"].ToString() == "y" ? "同意" : dr["SPR2_JL"].ToString() == "n" ? "不同意" : "");
                for (int i = 0; i <= 17; i++)
                {
                    rowqz1.Cells[i].CellStyle = cells2;
                }
                CellRangeAddress range3 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 0, 1);
                sheet0.AddMergedRegion(range3);
                //CellRangeAddress range4 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 3, 4);
                //sheet0.AddMergedRegion(range4);
                CellRangeAddress range5 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 5, 6);
                sheet0.AddMergedRegion(range5);
                //CellRangeAddress range6 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 9, 11);
                //sheet0.AddMergedRegion(range6);
                CellRangeAddress range7 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 12, 13);
                sheet0.AddMergedRegion(range7);

                IRow rowqz2 = sheet0.CreateRow(dt.Rows.Count + 7);
                for (int i = 0; i <= 17; i++)
                {
                    rowqz2.CreateCell(i);
                }
                rowqz2.GetCell(1).SetCellValue("日期：");
                rowqz2.GetCell(2).SetCellValue(dr["ZDR_SJ"].ToString());
                rowqz2.GetCell(6).SetCellValue("日期：");
                rowqz2.GetCell(7).SetCellValue(dr["SPR1_SJ"].ToString());
                rowqz2.GetCell(13).SetCellValue("日期：");
                rowqz2.GetCell(14).SetCellValue(dr["SPR2_SJ"].ToString());
                for (int i = 0; i <= 17; i++)
                {
                    rowqz2.Cells[i].CellStyle = cells2;
                }

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}
