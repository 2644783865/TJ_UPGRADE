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
    public partial class FM_YueMo_ChuKu_Accounting : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();

            if (!IsPostBack)
            {
                bindGrid();
                bindData();//绑定日期
                bindControl();
                //CheckUser(ControlFinder);
            }
        }

        private void bindData()
        {
            lb_period.Text = getMonth();
            lb_year.Text = getYear();
        }

        private void bindControl()
        {
            string sql = "select count(*) from TBFM_HSTOTAL where HS_YEAR='" + getYear() + "' and HS_MONTH='" + getMonth() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                btn_hs.Enabled = false;
            }
            else
            {
                btn_hs.Enabled = true;
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
            /////显示本月的出库单总单
            pager.TableName = "View_SM_OUT";
            pager.PrimaryKey = "OutCode";
            pager.ShowFields = "OutCode,ROB,TotalState,HSFLAG,Dep,Warehouse,Sender,Doc,Verifier,MaterialCode,MaterialName,Standard,Attribute,cast(RealNumber as float) as RealNumber,cast(round(UnitPrice,4) as float) as UnitPrice,cast(round(Amount,2) as float) as Amount";
            pager.OrderField = "OutCode";

            //BillType!='5'表示不包含盘亏单

            pager.StrWhere = "substring(ApprovedDate,6,2) ='" + getMonth() + "' AND substring(ApprovedDate,1,4) ='" + getYear() + "' AND TotalState='2'";
            pager.OrderType = 0;
            pager.PageSize = 10;

            string sql = "select  isnull(round(sum(Amount),2),0) as TotalAmount from View_SM_OUT where " + pager.StrWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
            }

            sdr.Close();
        }

        protected string getYear()
        {
            string ymdhms = System.DateTime.Now.ToString("yyyyMMdd");
            return ymdhms.Substring(0, 4);
        }

        protected string getMonth()
        {
            string ymdhms = System.DateTime.Now.ToString("yyyyMMdd");
            return ymdhms.Substring(4, 2);
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#Efefef'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色---白色
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[13].Text = string.Format("{0:c2}",Convert.ToDouble(hfdTotalAmount.Value));

                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        /*****************************月末出库核算***************************/

        protected void btn_hs_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            DBCallCommon.openConn(sqlConn);
            SqlCommand sqlCmd = new SqlCommand("YueMoChuKuHeSuanPre", sqlConn);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add("@gmonth", SqlDbType.VarChar, 2);
            sqlCmd.Parameters["@gmonth"].Value = getMonth();

            sqlCmd.Parameters.Add("@gyear", SqlDbType.VarChar, 4);
            sqlCmd.Parameters["@gyear"].Value = getYear();

            sqlCmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal

            sqlCmd.ExecuteNonQuery();

            DBCallCommon.closeConn(sqlConn);

            if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 0)
            {
                string script = @"alert('预出库核算成功!');location.href ='FM_YueMo_ChuKu_Accounting.aspx'";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            else if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 1)
            {
                string script = @"alert('预出库核算遇到异常错误!');'";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
        }
    }
}