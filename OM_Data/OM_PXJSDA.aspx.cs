using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PXJSDA : System.Web.UI.Page
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
            public static string name;
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

            //2016.12.22添加，根据年月进行筛选
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
            pager_org.ShowFields = "*";
            pager_org.OrderField = "PX_ZJR";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptPXJH, UCPaging1, palNoData);
            asd.dt = dt;
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
            string sql = " SPLX in ('NDPXJH','LSPX') ";
            sql += "and PX_BH is not null and PX_BH<>''";
            if (ddlBM.SelectedValue != "0")
            {
                sql += " and PX_BM = '" + ddlBM.SelectedValue + "'";
            }
            if (txtName.Text.Trim() != "")
            {
                sql += " and PX_ZJR like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtXMBH.Text.Trim() != "")
            {
                sql += "and PX_BH like '%" + txtXMBH.Text.Trim() + "%'";
            }

            //2016.12.22添加，根据年月进行筛选
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
        #endregion

        protected void rptPXJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                asd.name = string.Empty;
                return;
            }
            DataRowView drv = (DataRowView)e.Item.DataItem;
            Label name = (Label)e.Item.FindControl("PX_ZJR");
            if (name.Text == asd.name)
            {
                name.Visible = false;
            }
            else
            {
                asd.name = name.Text;
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        #region 导出
        protected void btnDaoChu_onserverclick(object sender, EventArgs e)
        {
            string sqlexport = "select * from (select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPZT='y' and SPLX='LSPX' union all select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPZT='y'  and SPLX='NDPXJH')t where " + StrWhere() + " order by PX_ZJR desc";
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
            string filename = "培训讲师档案--" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("培训讲师档案.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.HeightInPoints = 20;
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["PX_ZJR"].ToString());
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["PX_BM"].ToString());
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PX_BH"].ToString());
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["PX_FS"].ToString() == "n" ? "内部" : "外部");
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["PX_XMMC"].ToString());
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["PX_SJSJ"].ToString());
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["PX_SJDD"].ToString());
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["PX_ZJR"].ToString());
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["PX_SJXS"].ToString());
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["PX_ZJRDF"].ToString());
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["PX_SJBZ"].ToString());
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 9;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 11; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

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
                //CellRangeAddress range1 = new CellRangeAddress(dt.Rows.Count + 2, dt.Rows.Count + 2, 0, 16);
                //sheet0.AddMergedRegion(range1);
                //CellRangeAddress range2 = new CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 7, 17);
                //sheet0.AddMergedRegion(range2);

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        protected void btnbaocun_click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (RepeaterItem item in rptPXJH.Items)
            {
                if (((CheckBox)item.FindControl("cbxXuHao")).Checked)
                {
                    string PX_ZJR = ((Label)item.FindControl("PX_ZJR")).Text;
                    string txtJHBM = ((TextBox)item.FindControl("txtJHBM")).Text;
                    string txtJHGW = ((TextBox)item.FindControl("txtJHGW")).Text;
                    string txtJHXL = ((TextBox)item.FindControl("txtJHXL")).Text;
                    string txtJHZY = ((TextBox)item.FindControl("txtJHZY")).Text;
                    string sql = "";
                    sql = "update OM_PXJH_SQ set PX_ZJRBM='" + txtJHBM + "', PX_ZJRGW='" + txtJHGW + "', PX_ZJRXL='" + txtJHXL + "', PX_ZJRZY='" + txtJHZY + "' where PX_ZJR='" + PX_ZJR + "'";
                    list.Add(sql);
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script type='text/javascript'>alert('请勾选序号！！！')</script>");
            }
            else
            {
                DBCallCommon.ExecuteTrans(list);
            }
            bindrpt();
        }

    }
}
