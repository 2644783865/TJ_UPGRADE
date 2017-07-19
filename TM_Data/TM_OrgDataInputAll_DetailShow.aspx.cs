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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_OrgDataInputAll_DetailShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
            }
            else
            {
                this.InitVar();
            }
        }

        protected void PageInit()
        {
            ViewState["CopyType"] = Request.QueryString["CopyType"].ToString();
            ViewState["Sc_TaskID"] = Request.QueryString["Sc_TaskID"].ToString();
            ViewState["Sc_TableName"] = Request.QueryString["Sc_TableName"].ToString();
            ViewState["Sc_XuHao"] = Request.QueryString["Sc_XuHao"].ToString();

            ViewState["Tg_TaskID"] = Request.QueryString["Tg_TaskID"].ToString();
            ViewState["Tg_TableName"] = Request.QueryString["Tg_TableName"].ToString();
            ViewState["Tg_XuHao"] = Request.QueryString["Tg_XuHao"].ToString();
            ViewState["array"] = Request.QueryString["array"].ToString().Replace("-", "'");
            string A = ViewState["array"].ToString();
            this.SetLabelTip();

            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }
        /// <summary>
        /// 设置Label标签的值：如：数据源XXX条，可导入XXX条，不可导入XXX条
        /// </summary>
        protected void SetLabelTip()
        {
            //数据源条数
            string sql_find_sc = "select count(*) as Num from View_TM_DQO where BM_ENGID='" + ViewState["Sc_TaskID"] + "' AND (BM_ZONGXU='" + ViewState["Sc_XuHao"] + "' OR BM_ZONGXU LIKE '" + ViewState["Sc_XuHao"] + ".%')";
            int src_numbers = Convert.ToInt32(DBCallCommon.GetDTUsingSqlText(sql_find_sc).Rows[0]["Num"].ToString());
            lblSourceNumber.Text = src_numbers.ToString();

            string sql_find_ok;

            if (ViewState["array"].ToString().Trim() != "")
            {
                sql_find_ok = "select count(*) as Num from View_TM_DQO as A where BM_ENGID='" + ViewState["Sc_TaskID"] + "' AND (BM_ZONGXU='" + ViewState["Sc_XuHao"] + "' OR BM_ZONGXU LIKE '" + ViewState["Sc_XuHao"] + ".%') and  not exists(select A1.BM_ZONGXU from View_TM_DQO as A1 where A1.BM_ENGID = '" + ViewState["Tg_TaskID"].ToString() + "'  AND (A1.BM_ZONGXU=STUFF(A.BM_ZONGXU,1,len('" + ViewState["Sc_XuHao"] + "'),'" + ViewState["Tg_XuHao"] + "')))  AND A.BM_XUHAO NOT IN(" + ViewState["array"].ToString() + ")";
            }
            else
            {
                sql_find_ok = "select count(*) as Num from View_TM_DQO as A where BM_ENGID='" + ViewState["Sc_TaskID"] + "' AND (BM_ZONGXU='" + ViewState["Sc_XuHao"] + "' OR BM_ZONGXU LIKE '" + ViewState["Sc_XuHao"] + ".%') and   not exists(select A1.BM_ZONGXU from View_TM_DQO as A1 where A1.BM_ENGID ='" + ViewState["Tg_TaskID"].ToString() + "'  AND (A1.BM_ZONGXU=STUFF(A.BM_ZONGXU,1,len('" + ViewState["Sc_XuHao"] + "'),'" + ViewState["Tg_XuHao"] + "'))) ";
            }

            int src_canimpt_numbers = Convert.ToInt32(DBCallCommon.GetDTUsingSqlText(sql_find_ok).Rows[0]["Num"].ToString());
            lblEnableImport.Text = src_canimpt_numbers.ToString();

            //不导入项
            int notimportin = 0;
            if (ViewState["array"].ToString().Trim() != "")
            {
                notimportin = ViewState["array"].ToString().Trim().Split(',').Length;
            }
            lblNotImport.Text = notimportin.ToString();
            //不可导入条数
            int tg_unable_numbers = src_numbers - src_canimpt_numbers - notimportin;
            lblUnableImport.Text = tg_unable_numbers.ToString();

        }

        protected void rblImport_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetLabelTip();
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        protected string GetStrWhere()
        {
            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(" BM_ENGID='" + ViewState["Sc_TaskID"] + "' AND (BM_ZONGXU='" + ViewState["Sc_XuHao"] + "' OR  BM_ZONGXU  LIKE '" + ViewState["Sc_XuHao"] + ".%') and ");
            if (rblImport.SelectedValue == "Y")//可导入项
            {
                string sql_find_ok;
                if (ViewState["array"].ToString().Trim() != "")
                {
                    sql_find_ok = " not exists(select A1.BM_ZONGXU from View_TM_DQO as A1 where A1.BM_ENGID= '" + ViewState["Tg_TaskID"].ToString() + "' AND (A1.BM_ZONGXU=STUFF(A.BM_ZONGXU,1,len('" + ViewState["Sc_XuHao"] + "'),'" + ViewState["Tg_XuHao"] + "')))  AND A.BM_ZONGXU NOT IN(" + ViewState["array"].ToString() + ")";
                }
                else
                {
                    sql_find_ok = " not exists(select A1.BM_ZONGXU from View_TM_DQO as A1 where A1.BM_ENGID = '" + ViewState["Tg_TaskID"].ToString() + "' AND (A1.BM_ZONGXU=STUFF(A.BM_ZONGXU,1,len('" + ViewState["Sc_XuHao"] + "'),'" + ViewState["Tg_XuHao"] + "'))) ";
                }

                strb_sql.Append(sql_find_ok);
            }
            else if (rblImport.SelectedValue == "N")//不可导入项
            {
                string sql_find_notok;
                if (ViewState["array"].ToString().Trim() != "")
                {
                    sql_find_notok = " BM_ZONGXU NOT IN(select BM_ZONGXU from View_TM_DQO as B where BM_ENGID='" + ViewState["Sc_TaskID"] + "' AND (BM_ZONGXU='" + ViewState["Sc_XuHao"] + "' OR  BM_ZONGXU  LIKE '" + ViewState["Sc_XuHao"] + ".%') and  not exists(select A1.BM_XUHAO from View_TM_DQO as A1 where A1.BM_ENGID = '" + ViewState["Tg_TaskID"].ToString() + "'  AND (A1.BM_ZONGXU=STUFF(B.BM_ZONGXU,1,len('" + ViewState["Sc_XuHao"] + "'),'" + ViewState["Tg_XuHao"] + "')))) AND BM_ZONGXU NOT IN(" + ViewState["array"].ToString() + ")";
                }
                else
                {
                    sql_find_notok = " BM_ZONGXU NOT IN(select BM_ZONGXU from View_TM_DQO as B where BM_ENGID='" + ViewState["Sc_TaskID"] + "' AND (BM_ZONGXU='" + ViewState["Sc_XuHao"] + "' OR  BM_ZONGXU  LIKE '" + ViewState["Sc_XuHao"] + ".%') and  not exists(select A1.BM_ZONGXU from View_TM_DQO as A1 where A1.BM_ENGID LIKE '" + ViewState["Tg_TaskID"].ToString() + "'  AND (A1.BM_XUHAO=STUFF(B.BM_XUHAO,1,len('" + ViewState["Sc_XuHao"] + "'),'" + ViewState["Tg_XuHao"] + "')))) ";
                }
                strb_sql.Append(sql_find_notok);
            }
            else
            {
                string sql_not_import = "";
                if (ViewState["array"].ToString().Trim() != "")
                {
                    sql_not_import = " BM_XUHAO IN(" + ViewState["array"].ToString() + ")";
                }
                else
                {
                    sql_not_import = " 1=0";
                }
                strb_sql.Append(sql_not_import);
            }
            return strb_sql.ToString();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (rblImport.SelectedValue == "Y")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[1].ToolTip = "可导入项";
                }
                else
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].ToolTip = "不可导入项";
                }
            }
        }

        #region 分页
        PagerQueryParam pager = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }

        private void InitPager()
        {
            pager.TableName = ViewState["Sc_TableName"].ToString() + " as A ";
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
            pager.OrderField = "dbo.f_formatstr('" + ViewState["CopyType"] + "','.')";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 100;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
    }
}

