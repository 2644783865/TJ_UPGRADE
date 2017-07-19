using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_NDPXJH_GL : BasicPage
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
            CheckUser(ControlFinder);
        }

        private class asd
        {
            public static string username;
            public static string userid;
            public static string sjid;
            public static string bumen;
            public static string time;
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

            string sql1 = "select distinct PX_YEAR from OM_PXJH_SQ order by PX_YEAR desc";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            for (int i = 0, length = dt1.Rows.Count; i < length; i++)
            {
                ddlYear.Items.Add(new ListItem(dt1.Rows[i][0].ToString(), dt1.Rows[i][0].ToString()));
            }
            ddlYear.Items.Add(new ListItem("-全部-", "0"));
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
            MergeCells();
        }

        private void bindrpt()
        {
            pager_org.TableName = "OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID ";
            pager_org.PrimaryKey = "PX_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "PX_SJID,PX_BM,PX_FS";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptPXJH, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            if (IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aa", "aa()", true);
            }
        }

        private string StrWhere()
        {
            string sql = " SPLX='NDPXJH'";
            if (rblRW.SelectedValue == "1")
            {
                sql += " and (ZDRID ='" + asd.userid + "' or (SPR1ID='" + asd.userid + "' and SPZT='1') or (SPR2ID ='" + asd.userid + "' and SPZT='1y') or (SPR3ID ='" + asd.userid + "' and SPZT='2y'))";
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
                sql += " and SPZT in ('1y','2y')";
            }
            else if (ddlSPZT.SelectedValue == "4")
            {
                sql += " and SPZT='y'";
            }
            else if (ddlSPZT.SelectedValue == "5")
            {
                sql += " and SPZT='n'";
            }
            if (ddlBM.SelectedValue != "0")
            {
                sql += " and PX_BM like '%" + ddlBM.SelectedValue + "%'";
            }
            if (ddlSJ.SelectedValue != "0")
            {
                sql += " and PX_SJ ='%" + ddlSJ.SelectedValue + "%'";
            }
            if (ddlYear.SelectedValue != "0")
            {
                sql += " and PX_YEAR like '%" + ddlYear.SelectedValue + "%'";
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
                asd.bumen = string.Empty;
                asd.time = string.Empty;
                return;
            }
            DataRowView drv = (DataRowView)e.Item.DataItem;
            HyperLink hplCheck = (HyperLink)e.Item.FindControl("hplCheck");
            HyperLink hplAlter = (HyperLink)e.Item.FindControl("hplAlter");
            hplAlter.Visible = false;
            hplCheck.Visible = false;

            //去除重复显示
            Label bumen = (Label)e.Item.FindControl("PX_BM");
            if (bumen.Text == asd.bumen && asd.time == drv["PX_SJID"].ToString())
            {
                //bumen.Visible = false;
            }
            else
            {
                asd.bumen = bumen.Text;
                asd.time = drv["PX_SJID"].ToString();
            }

            //审批
            if (drv["SPZT"].ToString() == "0" || drv["SPZT"].ToString() == "n")
            {
                if (asd.userid == drv["ZDRID"].ToString())
                {
                    hplAlter.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "1")
            {
                if (asd.userid == drv["SPR1ID"].ToString())
                {
                    hplCheck.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "1y")
            {
                if (asd.userid == drv["SPR2ID"].ToString())
                {
                    hplCheck.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "2y")
            {
                if (asd.userid == drv["SPR3ID"].ToString())
                {
                    hplCheck.Visible = true;
                }
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();

        }
        #region 导出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sqltext = "select PX_BM,case PX_FS when 'n' then '内' when 'w'then '外' else '' end,PX_XMMC,case PX_SJ when '1' then '第一季度'when '2' then '第二季度'when '3' then '第三季度'when '4' then '第四季度' else '' end,PX_DD,PX_ZJR,PX_DX,PX_RS,PX_XS,PX_FYYS,PX_BZ from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where" + StrWhere() + "order by PX_SJID,PX_BM,PX_FS";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "年度培训计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("年度培训计划.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
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
