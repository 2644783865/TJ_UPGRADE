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
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_Period_Begin_Adjust : System.Web.UI.Page
    {

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                bindControl();
                bindGrid();
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数    
        }
        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "View_STORAGEBAL_MAR";//从视图中读取数据

            pager.PrimaryKey = "SI_MARID";

            pager.ShowFields = "SI_MARID,MNAME,GUIGE,CAIZHI,GB,SI_BEGBAL,SI_YEAR,SI_PERIOD";

            pager.OrderField = "SI_MARID";

            pager.StrWhere = CreateConStr();

            pager.OrderType = 0;//项目编号的降序排列

            pager.PageSize = 100;

           

        }

        private void bindSumAmount(string condition)
        {
            string sql = "select round(isnull(sum(SI_BEGBAL),0),2) as TotalAM from View_STORAGEBAL_MAR where " + condition;
            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                SUMAMOUNT.Value = sdr["TotalAM"].ToString();
              
            }
            sdr.Close();
 
        }



        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            bindSumAmount(pager.StrWhere);

            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        private string CreateConStr()
        {
            string strWhere = "SI_YEAR='" + DateTime.Now.Year.ToString() + "' and SI_PERIOD='" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and SI_BEGNUM=0 and SI_BEGBAL<>0";

            
            return strWhere;
        }
        protected void btn_adjust_Click(object sender, EventArgs e)
        {
            string sql = "update TBFM_STORAGEBAL set SI_BEGDIFF=-isnull(SI_BEGBAL,0) where SI_YEAR='" + DateTime.Now.Year.ToString() + "' and SI_PERIOD='" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and SI_BEGNUM=0 and SI_BEGBAL<>0";
            DBCallCommon.ExeSqlText(sql);

            string script = @"<script language='javascript' type='text/javascript'>alert('调整成功！')</script>";
            ExecuteJS(script);

            UCPaging1.CurrentPage=1;

            bindGrid();

            btn_adjust.Enabled = false;

            btn_antiadjust.Enabled = true;
        }

        protected void btn_antiadjust_Click(object sender, EventArgs e)
        {
            string sql = "update TBFM_STORAGEBAL set SI_BEGDIFF=0 where SI_YEAR='" + DateTime.Now.Year.ToString() + "' and SI_PERIOD='" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and SI_BEGNUM=0 and SI_BEGDIFF<>0";
            DBCallCommon.ExeSqlText(sql);
            string script = @"<script language='javascript' type='text/javascript'>alert('反调整成功！')</script>";
            ExecuteJS(script);

            UCPaging1.CurrentPage = 1;

            bindGrid();

            btn_adjust.Enabled = true;

            btn_antiadjust.Enabled = false;

        }

        private void bindControl()
        {

            string sql = "select count(*) from TBFM_STORAGEBAL where SI_YEAR='" + DateTime.Now.Year.ToString() + "' and SI_PERIOD='" + DateTime.Now.Month.ToString().PadLeft(2,'0') +"' and SI_BEGNUM=0 and SI_BEGBAL<>0";
            DataTable  dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) == 0)
            {
                btn_adjust.Enabled = false;
            }
            dt.Clear();

            sql = "select count(*) from TBFM_STORAGEBAL where SI_YEAR='" + DateTime.Now.Year.ToString() + "' and SI_PERIOD='" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and SI_BEGNUM=0 and SI_BEGDIFF<>0";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) == 0)
            {
                btn_antiadjust.Enabled = false;
                
            }
            else
            {
                GridView1.EmptyDataText = "期初异常金额已调整！";
            }
            dt.Clear();

            sql = "select count(*) from TBFM_HSTOTAL where HS_YEAR='" + DateTime.Now.Year.ToString() + "' and HS_MONTH='" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and HS_STATE='2'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                //期初调整和反期初调整都不能用

                btn_adjust.Enabled = false;

                btn_antiadjust.Enabled = false;

            }

            dt.Clear();

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "合计";
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = string.Format("{0:c2}", Convert.ToDouble(SUMAMOUNT.Value));
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        private void ExecuteJS(string script)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            

           this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script);

        }

    }
}
