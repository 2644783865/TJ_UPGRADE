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
    public partial class OM_Bgyp_DingE : BasicPage
    {
        string year = "";
        string month = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            year = (DateTime.Now.Year).ToString();
            month = (DateTime.Now.Month).ToString();

            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                BindData();
                this.InitPage();
                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();

            }
            CheckUser(ControlFinder);
            InitVar();
        }



        //绑定基本信息
        private void BindData()
        {

            string Stid = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(4, Stid);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }


        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Year.SelectedIndex = 0;
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Month.SelectedIndex = 0;
        }



        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {

            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

        #region
        PagerQueryParam pager_org = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager("1=1");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string where)
        {
            pager_org.TableName = "(select a.*,b.SLJE,a.MONTH_MAX-b.SLJE as CE from dbo.TBOM_BGYP_Month_max as a left join (select sum(GET_MONEY) as SLJE,ST_DEPID,year,month from View_TBOM_BGYPAPPLY where WLBM not like '3-%' and IsCalculate='0' group by ST_DEPID,year,month)b on a.year=b.year and a.month=b.month and a.DEP_CODE=b.ST_DEPID and a.Type='1')e";
            pager_org.PrimaryKey = "DEP_CODE";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "year,month,DEP_CODE";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 20;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }


        private string strWhere()
        {
            string sqlwhere = "1=1";


            if (dplYear.SelectedIndex != 0)
            {
                sqlwhere += " and Year='" + dplYear.SelectedValue + "' ";
            }
            if (dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and Month='" + dplMoth.SelectedValue + "' ";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sqlwhere += " and DEP_CODE='" + ddl_Depart.SelectedValue + "'";
            }
            return sqlwhere;

        }
        private void bindGrid()
        {


            InitPager(strWhere());

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

            string sqlText = "select sum(cast(MONTH_MAX as float)),sum(cast(SLJE as float)),sum(cast(CE as float)) from (select a.*,b.SLJE,a.MONTH_MAX-b.SLJE as CE from dbo.TBOM_BGYP_Month_max as a left join (select sum(GET_MONEY) as SLJE,ST_DEPID,year,month from View_TBOM_BGYPAPPLY where WLBM not like '3-%' and IsCalculate='0' group by ST_DEPID,year,month)b on a.year=b.year and a.month=b.month and a.DEP_CODE=b.ST_DEPID and a.type='1')e  where  " + strWhere();
            dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                lblEDU.Text = dt.Rows[0][0].ToString();
                lblSLTotal.Text = dt.Rows[0][1].ToString();
                lblCE.Text = dt.Rows[0][2].ToString();
            }
        }
        #endregion

        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {

            bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

            bindGrid();
        }

        protected void btnCreat_Click(object sender, EventArgs e)
        {

            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择年月！！')</script>");
                return;
            }
            year = dplYear.SelectedValue;
            month = dplMoth.SelectedValue;
            string sql = "select * from TBOM_BGYP_Month_max where year='" + year + "' and month='" + month + "' and type='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                Response.Write("<script>alert('已存在本页额度，请勿重复添加')</script>");
                return;
            }
            else
            {
                sql = "insert into TBOM_BGYP_Month_max select DEP_CODE, DEP_NAME, MONTH_MAX, '" + year + "', '" + month + "', '1' from TBOM_BGYP_Month_max where type='0'";
                DBCallCommon.ExeSqlText(sql);
                bindGrid();
            }
        }

        protected void btnEditDE_Click(object sender, EventArgs e)
        {
            Response.Redirect("OM_Bgyp_Real.aspx");
        }



        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {

            string sqltext = ""; ;

            sqltext += "select * from (select a.*,b.SLJE,a.MONTH_MAX-b.SLJE as CE from dbo.TBOM_BGYP_Month_max as a left join (select sum(GET_MONEY) as SLJE,ST_DEPID,year,month from View_TBOM_BGYPAPPLY  where WLBM not like '3-%'  and IsCalculate='0' group by ST_DEPID,year,month)b on a.year=b.year and a.month=b.month and a.DEP_CODE=b.ST_DEPID and a.Type='1')e  where " + strWhere() + " ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }




        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "办公用品定额查询" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("办公用品定额查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                  
                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["Year"].ToString() + "-" + objdt.Rows[i]["Month"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["DEP_NAME"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["MONTH_MAX"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["SLJE"].ToString());

                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["CE"].ToString());
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
