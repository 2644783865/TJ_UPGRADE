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
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KQTOTAL : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindbmData();
                bindgouxuanbm();
                bindddlbz();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            CheckUser(ControlFinder);
            InitVar();
        }




        #region 分页
        PagerQueryParam pager_org = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager_org.TableName = "(select KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where " + StrWhere() + " group by KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1)t";
            pager_org.PrimaryKey = "KQ_ST_ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "DEP_NAME,KQ_ST_ID";
            pager_org.StrWhere = Creatconstr();
            pager_org.OrderType = 0;
            pager_org.PageSize = 20;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion

        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (tb_CXstarttime.Text.Trim() != "" && tb_CXendtime.Text.Trim() != "")
            {
                sql += " and KQ_DATE>='" + tb_CXstarttime.Text.Trim() + "' and KQ_DATE<='" + tb_CXendtime.Text.Trim() + "'";
            }
            else
            {
                sql += " and KQ_DATE like '" + DateTime.Now.Year.ToString().Trim() + "-%'";
            }
            return sql;
        }


        private string Creatconstr()
        {
            int num = 0;
            string sqltext = "1=1";
            if (DropDownListtype.SelectedIndex != 0)
            {
                sqltext += " and " + DropDownListtype.SelectedValue.ToString().Trim() + ">=" + CommonFun.ComTryDecimal(tb_tsorxs.Text.ToString().Trim()) + "";
            }
            if (txtName.Text != "")
            {
                sqltext += " and ST_NAME like '%" + txtName.Text.ToString().Trim() + "%'";
            }
            //if (ddl_Depart.SelectedValue != "00")
            //{
            //    sqltext += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            //}
            if (ddlbz.SelectedIndex != 0)
            {
                sqltext += " and ST_DEPID1='" + ddlbz.SelectedValue.ToString().Trim() + "'";
            }


            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                if (listdepartment.Items[i].Selected == true)
                {
                    num++;
                }
            }
            if (num > 0)
            {
                sqltext += " and (ST_DEPID=''";
                for (int i = 0; i < listdepartment.Items.Count; i++)
                {
                    if (listdepartment.Items[i].Selected == true)
                    {
                        sqltext += " or ST_DEPID like '%" + listdepartment.Items[i].Value + "%'";
                    }
                }
                sqltext += ")";
            }


            int num2 = 0;
            for (int j = 0; j < CheckBoxListhj.Items.Count; j++)
            {
                if (CheckBoxListhj.Items[j].Selected == true)
                {
                    num2++;
                }
            }
            if (num2 > 0)
            {
                string strgetziduan = "(0";
                for (int j = 0; j < CheckBoxListhj.Items.Count; j++)
                {
                    if (CheckBoxListhj.Items[j].Selected == true)
                    {
                        strgetziduan += "+" + CheckBoxListhj.Items[j].Value.ToString().Trim() + "";
                    }
                }
                strgetziduan += ")";
                if (txt_hjmin.Text.Trim() != "")
                {
                    sqltext += " and " + strgetziduan.ToString().Trim() + ">=" + CommonFun.ComTryDecimal(txt_hjmin.Text.Trim()) + "";
                }
                if (txt_hjmax.Text.Trim() != "")
                {
                    sqltext += " and " + strgetziduan.ToString().Trim() + "<=" + CommonFun.ComTryDecimal(txt_hjmax.Text.Trim()) + "";
                }
            }
            return sqltext;
        }


        //private void BindbmData()
        //{

        //    string stId = Session["UserId"].ToString();
        //    System.Data.DataTable dt = DBCallCommon.GetPermeision(9, stId);
        //    ddl_Depart.DataSource = dt;
        //    ddl_Depart.DataTextField = "DEP_NAME";
        //    ddl_Depart.DataValueField = "DEP_CODE";
        //    ddl_Depart.DataBind();
        //}


        //勾选部门绑定
        private void bindgouxuanbm()
        {
            string sqlbumen = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            System.Data.DataTable dtbumen = DBCallCommon.GetDTUsingSqlText(sqlbumen);
            listdepartment.DataTextField = "DEP_NAME";
            listdepartment.DataValueField = "DEP_CODE";
            listdepartment.DataSource = dtbumen;
            listdepartment.DataBind();
            ListItem item = new ListItem("全部", "");
            listdepartment.Items.Insert(0, item);
        }

        private void bindddlbz()
        {
            string sql = "SELECT DISTINCT ST_DEPID1 FROM TBDS_STAFFINFO where ST_DEPID1!='' and ST_DEPID1 not in('0','1','2','3','4','5','6','7','8','9')";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlbz.DataTextField = "ST_DEPID1";
            ddlbz.DataValueField = "ST_DEPID1";
            ddlbz.DataSource = dt;
            ddlbz.DataBind();
            ListItem item = new ListItem("--请选择--", "--请选择--");
            ddlbz.Items.Insert(0, item);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        //protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UCPaging1.CurrentPage = 1;
        //    this.bindGrid();
        //}


        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }


        protected void btn_clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                listdepartment.Items[i].Selected = false;
            }
            for (int j = 0; j < CheckBoxListhj.Items.Count; j++)
            {
                CheckBoxListhj.Items[j].Selected = false;
            }
            txt_hjmin.Text = "";
            txt_hjmax.Text = "";
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        protected void ddlbz_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        //导出汇总
        protected void btnexport_Click(object sender, EventArgs e)
        {
            string sqlkqtj = "select ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,KQ_CHUQIN,KQ_GNCC,KQ_GWCC,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_BEIYONG1,KQ_BEIYONG2,KQ_BEIYONG3,KQ_BEIYONG4,KQ_BEIYONG5,KQ_BEIYONG6,KQ_QTJIA,KQ_JIEDIAO,KQ_ZMJBAN,KQ_JRJIAB,KQ_ZHIBAN,KQ_YEBAN,KQ_ZHONGB,KQ_CBTS,KQ_YSGZ from (select KQ_ST_ID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,ST_DEPID,sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where " + StrWhere() + " group by ST_DEPID,KQ_ST_ID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1)t where " + Creatconstr();
            System.Data.DataTable dtkqtj = DBCallCommon.GetDTUsingSqlText(sqlkqtj);
            string filename = "考勤统计导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("考勤统计导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtkqtj.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtkqtj.Columns.Count; j++)
                    {
                        string str = dtkqtj.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtkqtj.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }


        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int num = 0;
                for (int i = 0; i < CheckBoxListhj.Items.Count; i++)
                {
                    if (CheckBoxListhj.Items[i].Selected == true)
                    {
                        num++;
                    }
                }
                if (num > 0)
                {
                    string kqstid = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ST_ID")).Text.ToString().Trim();
                    string sqlsxhj = "select (0";
                    for (int i = 0; i < CheckBoxListhj.Items.Count; i++)
                    {
                        if (CheckBoxListhj.Items[i].Selected == true)
                        {
                            sqlsxhj += "+" + CheckBoxListhj.Items[i].Value.ToString().Trim() + "";
                        }
                    }
                    sqlsxhj += ") as searchhj from (select KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where " + StrWhere() + " group by KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1)t where KQ_ST_ID='" + kqstid + "'";
                    System.Data.DataTable dtsxhj = DBCallCommon.GetDTUsingSqlText(sqlsxhj);
                    if (dtsxhj.Rows.Count > 0)
                    {
                        System.Web.UI.WebControls.Label lbsearchhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsearchhj");
                        lbsearchhj.Text = dtsxhj.Rows[0]["searchhj"].ToString().Trim();
                    }
                }
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from (select KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where " + StrWhere() + " group by KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1)t where " + Creatconstr() + "";
                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);
                if (dthj.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lbKQ_CHUQINhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CHUQINhj");
                    System.Web.UI.WebControls.Label lbKQ_GNCChj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GNCChj");
                    System.Web.UI.WebControls.Label lbKQ_GWCChj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GWCChj");
                    System.Web.UI.WebControls.Label lbKQ_BINGJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BINGJhj");
                    System.Web.UI.WebControls.Label lbKQ_SHIJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_SHIJhj");
                    System.Web.UI.WebControls.Label lbKQ_KUANGGhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_KUANGGhj");
                    System.Web.UI.WebControls.Label lbKQ_DAOXIUhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_DAOXIUhj");
                    System.Web.UI.WebControls.Label lbKQ_CHANJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CHANJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_PEICHANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_PEICHANhj");
                    System.Web.UI.WebControls.Label lbKQ_HUNJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_HUNJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_SANGJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_SANGJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_GONGShj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GONGShj");
                    System.Web.UI.WebControls.Label lbKQ_NIANXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_NIANXhj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG1hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG1hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG2hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG2hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG3hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG3hj");

                    System.Web.UI.WebControls.Label lbKQ_BEIYONG4hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG4hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG5hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG5hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG6hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG6hj");

                    System.Web.UI.WebControls.Label lbKQ_QTJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_QTJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_JIEDIAOhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_JIEDIAOhj");
                    System.Web.UI.WebControls.Label lbKQ_ZMJBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZMJBANhj");
                    System.Web.UI.WebControls.Label lbKQ_JRJIABhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_JRJIABhj");
                    System.Web.UI.WebControls.Label lbKQ_ZHIBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZHIBANhj");
                    System.Web.UI.WebControls.Label lbKQ_YEBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_YEBANhj");
                    System.Web.UI.WebControls.Label lbKQ_ZHONGBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZHONGBhj");
                    System.Web.UI.WebControls.Label lbKQ_CBTShj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CBTShj");
                    System.Web.UI.WebControls.Label lbKQ_YSGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_YSGZhj");

                    lbKQ_CHUQINhj.Text = dthj.Rows[0]["KQ_CHUQIN"].ToString().Trim();
                    lbKQ_GNCChj.Text = dthj.Rows[0]["KQ_GNCC"].ToString().Trim();
                    lbKQ_GWCChj.Text = dthj.Rows[0]["KQ_GWCC"].ToString().Trim();
                    lbKQ_BINGJhj.Text = dthj.Rows[0]["KQ_BINGJ"].ToString().Trim();
                    lbKQ_SHIJhj.Text = dthj.Rows[0]["KQ_SHIJ"].ToString().Trim();
                    lbKQ_KUANGGhj.Text = dthj.Rows[0]["KQ_KUANGG"].ToString().Trim();
                    lbKQ_DAOXIUhj.Text = dthj.Rows[0]["KQ_DAOXIU"].ToString().Trim();
                    lbKQ_CHANJIAhj.Text = dthj.Rows[0]["KQ_CHANJIA"].ToString().Trim();
                    lbKQ_PEICHANhj.Text = dthj.Rows[0]["KQ_PEICHAN"].ToString().Trim();
                    lbKQ_HUNJIAhj.Text = dthj.Rows[0]["KQ_HUNJIA"].ToString().Trim();
                    lbKQ_SANGJIAhj.Text = dthj.Rows[0]["KQ_SANGJIA"].ToString().Trim();
                    lbKQ_GONGShj.Text = dthj.Rows[0]["KQ_GONGS"].ToString().Trim();
                    lbKQ_NIANXhj.Text = dthj.Rows[0]["KQ_NIANX"].ToString().Trim();
                    lbKQ_BEIYONG1hj.Text = dthj.Rows[0]["KQ_BEIYONG1"].ToString().Trim();
                    lbKQ_BEIYONG2hj.Text = dthj.Rows[0]["KQ_BEIYONG2"].ToString().Trim();
                    lbKQ_BEIYONG3hj.Text = dthj.Rows[0]["KQ_BEIYONG3"].ToString().Trim();

                    lbKQ_BEIYONG4hj.Text = dthj.Rows[0]["KQ_BEIYONG4"].ToString().Trim();
                    lbKQ_BEIYONG5hj.Text = dthj.Rows[0]["KQ_BEIYONG5"].ToString().Trim();
                    lbKQ_BEIYONG6hj.Text = dthj.Rows[0]["KQ_BEIYONG6"].ToString().Trim();

                    lbKQ_QTJIAhj.Text = dthj.Rows[0]["KQ_QTJIA"].ToString().Trim();
                    lbKQ_JIEDIAOhj.Text = dthj.Rows[0]["KQ_JIEDIAO"].ToString().Trim();
                    lbKQ_ZMJBANhj.Text = dthj.Rows[0]["KQ_ZMJBAN"].ToString().Trim();
                    lbKQ_JRJIABhj.Text = dthj.Rows[0]["KQ_JRJIAB"].ToString().Trim();
                    lbKQ_ZHIBANhj.Text = dthj.Rows[0]["KQ_ZHIBAN"].ToString().Trim();
                    lbKQ_YEBANhj.Text = dthj.Rows[0]["KQ_YEBAN"].ToString().Trim();
                    lbKQ_ZHONGBhj.Text = dthj.Rows[0]["KQ_ZHONGB"].ToString().Trim();
                    lbKQ_CBTShj.Text = dthj.Rows[0]["KQ_CBTS"].ToString().Trim();
                    lbKQ_YSGZhj.Text = dthj.Rows[0]["KQ_YSGZ"].ToString().Trim();
                }
            }
        }
    }
}
