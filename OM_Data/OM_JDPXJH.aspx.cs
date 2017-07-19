using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_JDPXJH : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                BindDropdownList();
                asd.username = Session["UserName"].ToString();
                asd.userid = Session["UserID"].ToString();
                bindrpt();
                MergeCells();
            }
        }

        private class asd
        {
            public static string username;
            public static string userid;
            public static string bumen;
            public static DataTable dt;
        }

        private void BindDropdownList()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlBM.Items.Add(new ListItem("-全部-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlBM.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_NAME"].ToString()));
            }


            dplYear.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int i = 0; i < 10; i++)
            {
                dplYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }

            //dpl_Year.SelectedIndex = 0;
            dplMonth.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dplMonth.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
            MergeCells();
        }

        private void bindrpt()
        {
            asd.dt = new DataTable();
            pager_org.TableName = "(select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPZT='y' and SPLX='LSPX' union all select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPZT='y'  and SPLX='NDPXJH')t";
            pager_org.PrimaryKey = "PX_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "PX_BM,PX_FS,PX_BH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptPXJH, UCPaging1, palNoData);

            asd.dt = dt;

            double xshj = 0;
            double rshj = 0;
            foreach (DataRow dr in dt.Rows)
            {
                xshj += CommonFun.ComTryDouble(dr["PX_SJXS"].ToString());
                rshj += CommonFun.ComTryDouble(dr["PX_SJRS"].ToString());
            }
            foreach (RepeaterItem item in rptPXJH.Controls)
            {
                if (item.ItemType == ListItemType.Footer)
                {
                    ((Label)item.FindControl("XSHJ")).Text = xshj.ToString();
                    ((Label)item.FindControl("RSHJ")).Text = rshj.ToString();
                    break;
                }
            }
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
            string sql = " SPZT='y' and PX_YEAR='" + DateTime.Now.Year.ToString() + "'";
            if (ddlBM.SelectedValue != "0")
            {
                sql += " and PX_BM like '%" + ddlBM.SelectedValue + "%'";
            }
            if (ddlSJ.SelectedValue != "0")
            {
                sql += " and PX_SJ like '%" + ddlSJ.SelectedValue + "%'";
            }
            if (dplYear.SelectedValue != "-请选择-" && dplMonth.SelectedValue == "-请选择-")
            {
                sql += " and PX_SJSJ like '%" + dplYear.SelectedValue + "%'";
            }
            if (dplYear.SelectedValue != "-请选择-" && dplMonth.SelectedValue != "-请选择-")
            {
                string YearMonth = dplYear.SelectedValue.ToString() + "-" + dplMonth.SelectedValue.ToString();
                sql += " and PX_SJSJ like '%" + YearMonth + "%'";
            }
            return sql;
        }

        private void MergeCells()//合并单元格
        {

        }

        private void GetXSHJ()
        { }
        #endregion

        protected void btnAdd_onserverclick(object sender, EventArgs e)
        {
            string pxid = "";
            for (int i = 0, length = rptPXJH.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptPXJH.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    pxid = ((HiddenField)rptPXJH.Items[i].FindControl("PX_ID")).Value;
                    break;
                }
            }
            if (pxid == "")
            {
                Response.Write("<script>alert('请勾选一条培训项目！！！')</script>");
                return;
            }
            else
            {
                string sql = "select PX_BH from OM_PXJH_SQ where PX_ID=" + pxid;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() != "")
                {
                    Response.Write("<script>alert('您选择的培训项目已经实施，不可重复实施！！！')</script>");
                    return;
                }
                Response.Redirect("OM_JDPXJH_SS.aspx?action=add&id=" + pxid);
            }
        }

        protected void rptPXJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                asd.bumen = string.Empty;
                return;
            }
            DataRowView drv = (DataRowView)e.Item.DataItem;
            HyperLink hplAlter = (HyperLink)e.Item.FindControl("hplAlter");
            HyperLink hplEdit = (HyperLink)e.Item.FindControl("hplEdit");
            hplAlter.Visible = false;
            hplEdit.Visible = false;
            Label bumen = (Label)e.Item.FindControl("PX_BM");
            if (bumen.Text == asd.bumen)
            {
                bumen.Visible = false;
            }
            else
            {
                asd.bumen = bumen.Text;
            }
            if (drv["PX_SSZDRID"].ToString() == asd.userid || (asd.userid == "150" && drv["PX_BH"].ToString() != ""))
            {
                hplAlter.Visible = true;
            }
            if ((asd.userid == "150" && drv["PX_BH"].ToString() != ""))
            {
                hplEdit.Visible = true;
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        #region 导出
        protected void btnDaoChu_onserverclick(object sender, EventArgs e)
        {
            string sqlexport = "select * from (select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPZT='y' and SPLX='LSPX' union all select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPZT='y'  and SPLX='NDPXJH')t where " + StrWhere() + " order by PX_BM,PX_FS,PX_BH desc";
            System.Data.DataTable dtexport = DBCallCommon.GetDTUsingSqlText(sqlexport);
            if (dtexport.Rows.Count == 0)
            {
                Response.Write("<script>alert('没有数据可导出！！！')</script>");
                return;
            }
            else
            {
                ExportDataItem(dtexport);
            }
        }

        private void ExportDataItem(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            string filename = "培训实施记录--" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("培训实施记录.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.HeightInPoints = 20;
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["PX_BM"].ToString());
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["PX_BH"].ToString());
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["SPLX"].ToString() == "NDPXJH" ? "年度" : "临时");
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["PX_FS"].ToString() == "n" ? "内部" : "外部");
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["PX_XMMC"].ToString());
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["PX_SJ"].ToString() == "1" ? "第一季度" : dt.Rows[i]["PX_SJ"].ToString() == "2" ? "第二季度" : dt.Rows[i]["PX_SJ"].ToString() == "3" ? "第三季度" : "第四季度");
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["PX_DD"].ToString());
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["PX_ZJR"].ToString());
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["PX_DX"].ToString());
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["PX_RS"].ToString());
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["PX_XS"].ToString());
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["PX_FYYS"].ToString());
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["PX_SJSJ"].ToString());
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["PX_SJDD"].ToString());
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["PX_SJRY"].ToString());
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["PX_SJRS"].ToString());
                    row.CreateCell(17).SetCellValue(dt.Rows[i]["PX_SJXS"].ToString());
                    row.CreateCell(18).SetCellValue(dt.Rows[i]["PX_SJBZ"].ToString());
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 9;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 18; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                double num = 0;
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    num += CommonFun.ComTryDouble(dt.Rows[i]["PX_SJXS"].ToString());
                }

                IRow rowhz = sheet0.CreateRow(dt.Rows.Count + 2);
                for (int i = 0; i <= 18; i++)
                {
                    rowhz.CreateCell(i);
                }
                rowhz.GetCell(0).SetCellValue("人数合计");
                rowhz.GetCell(17).SetCellValue(num);
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
                for (int i = 0; i <= 18; i++)
                {
                    rowhz.Cells[i].CellStyle = cells2;
                }
                CellRangeAddress range1 = new CellRangeAddress(dt.Rows.Count + 2, dt.Rows.Count + 2, 0, 16);
                sheet0.AddMergedRegion(range1);
                //CellRangeAddress range2 = new CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 7, 17);
                //sheet0.AddMergedRegion(range2);

                //IRow rowqz1 = sheet0.CreateRow(dt.Rows.Count + 6);
                //for (int i = 0; i <= 17; i++)
                //{
                //    rowqz1.CreateCell(i);
                //}
                //rowqz1.GetCell(0).SetCellValue("编制负责人：");
                //rowqz1.GetCell(2).SetCellValue(dr["ZDR"].ToString());
                //rowqz1.GetCell(5).SetCellValue("申报单位/部门主管：");
                //rowqz1.GetCell(7).SetCellValue(dr["SPR1"].ToString());
                //rowqz1.GetCell(8).SetCellValue(dr["SPR1_JL"].ToString() == "y" ? "同意" : dr["SPR1_JL"].ToString() == "n" ? "不同意" : "");
                //rowqz1.GetCell(12).SetCellValue("集团公司主管领导：");
                //rowqz1.GetCell(14).SetCellValue(dr["SPR2"].ToString());
                //rowqz1.GetCell(15).SetCellValue(dr["SPR2_JL"].ToString() == "y" ? "同意" : dr["SPR2_JL"].ToString() == "n" ? "不同意" : "");
                //for (int i = 0; i <= 17; i++)
                //{
                //    rowqz1.Cells[i].CellStyle = cells2;
                //}
                //CellRangeAddress range3 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 0, 1);
                //sheet0.AddMergedRegion(range3);
                ////CellRangeAddress range4 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 3, 4);
                ////sheet0.AddMergedRegion(range4);
                //CellRangeAddress range5 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 5, 6);
                //sheet0.AddMergedRegion(range5);
                ////CellRangeAddress range6 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 9, 11);
                ////sheet0.AddMergedRegion(range6);
                //CellRangeAddress range7 = new CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 12, 13);
                //sheet0.AddMergedRegion(range7);

                //IRow rowqz2 = sheet0.CreateRow(dt.Rows.Count + 7);
                //for (int i = 0; i <= 17; i++)
                //{
                //    rowqz2.CreateCell(i);
                //}
                //rowqz2.GetCell(1).SetCellValue("日期：");
                //rowqz2.GetCell(2).SetCellValue(dr["ZDR_SJ"].ToString());
                //rowqz2.GetCell(6).SetCellValue("日期：");
                //rowqz2.GetCell(7).SetCellValue(dr["SPR1_SJ"].ToString());
                //rowqz2.GetCell(13).SetCellValue("日期：");
                //rowqz2.GetCell(14).SetCellValue(dr["SPR2_SJ"].ToString());
                //for (int i = 0; i <= 17; i++)
                //{
                //    rowqz2.Cells[i].CellStyle = cells2;
                //}

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

    }
}
