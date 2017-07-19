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
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_ToolTip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
            if (IsPostBack)
            {
                this.InitVarMS();
            }
        }
        private void InitPage()
        {
            string passedstring = Request.QueryString["MSNor"].ToString();//序号$生产制号$原始数据表名，如MSNor=1.2$1.4$1.7$MB/FER/2-1$TBPM_STRINFOQLM
            string[]  querystringsplit= passedstring.Split('$');
            string strtable = querystringsplit[querystringsplit.Length - 1];
            ViewState["str_table"]=strtable;
            ViewState["TaskId"] = querystringsplit[querystringsplit.Length - 2];
            if (querystringsplit.Length == 3)
            {
                ViewState["xuhao_str"] = "(BM_XUHAO like  '" + querystringsplit[0] + ".%' OR BM_XUHAO='" + querystringsplit[0] + "')";
            }
            else
            {
                ViewState["xuhao_str"] = " (BM_XUHAO like  '" + querystringsplit[0] + ".%' OR BM_XUHAO='" + querystringsplit[0] + "'";
                for (int i = 1; i < querystringsplit.Length - 3; i++)
                {
                    ViewState["xuhao_str"] += "or BM_XUHAO like '" + querystringsplit[i] + ".%' OR BM_XUHAO='" + querystringsplit[i] + "'";
                }
                ViewState["xuhao_str"] += " or BM_XUHAO like '" + querystringsplit[querystringsplit.Length - 3] + ".%' OR BM_XUHAO='" + querystringsplit[querystringsplit.Length - 3] + "')";
            }
            this.GetTableName(strtable);
            this.BindPartIndex();
            rblMSAdjust.SelectedIndex = 0;
            rblInMs.SelectedIndex = 0;
            ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "' and " + ViewState["xuhao_str"] + "";
            UCPagingMS.CurrentPage = 1;
            this.InitVarMS();
            this.bindGridMS();
        }
        /// <summary>
        /// 绑定部件序号
        /// </summary>
        private void BindPartIndex()
        {
            string sql_select_partindex = "select BM_XUHAO from " + ViewState["str_table"] + " where BM_ENGID='" + ViewState["TaskId"] + "' and (BM_XUHAO like '[1-9].[0-9]' or BM_XUHAO like '[1-9].[1-9][0-9]' or BM_XUHAO like '[1-9][0-9].[0-9]' or BM_XUHAO like '[1-9][0-9].[1-9][0-9]') order by dbo.f_formatstr(BM_XUHAO, '.')";
            string datavale = "BM_XUHAO";
            string datatext = "BM_XUHAO";
            DBCallCommon.BindDdl(dplAllXuhao, sql_select_partindex, datatext, datavale);
        }
        /// <summary>
        /// 获取查询视图名
        /// </summary>
        private void GetTableName(string str_table)
        {
            switch (str_table)
            {
                case "TBPM_STRINFOHZY":
                    ViewState["ms_table"] = "TBPM_MSOFHZY";
                    ViewState["view_table"] = "View_TM_HZY";
                    break;
                case "TBPM_STRINFOQLM":
                    ViewState["ms_table"] = "TBPM_MSOFQLM";
                    ViewState["view_table"] = "View_TM_QLM";
                    break;
                case "TBPM_STRINFOBLJ":
                    ViewState["ms_table"] = "TBPM_MSOFBLJ";
                    ViewState["view_table"] = "View_TM_BLJ";
                    break;
                case "TBPM_STRINFODQLJ":
                    ViewState["ms_table"] = "TBPM_MSOFDQJ";
                    ViewState["view_table"] = "View_TM_DQLJ";
                    break;
                case "TBPM_STRINFOGFB":
                    ViewState["ms_table"] = "TBPM_MSOFGFB";
                    ViewState["view_table"] = "View_TM_GFB";
                    break;
                case "TBPM_STRINFODQO":
                    ViewState["ms_table"] = "TBPM_MSOFDQO";
                    ViewState["view_table"] = "View_TM_DQO";
                    break;
                default: break;
            }
        }
        
        /// <summary>
        /// 切换查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblMSAdjust_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMSAdjust.SelectedValue == "0")//当前调整
            {
                rblInMs.Visible = true;
                lalInMS.Visible = true;
                lblQueryXuhao.Visible = true;
                dplAllXuhao.Visible = true;
                lblQueryXuhao.Visible = false;
                dplAllXuhao.Visible = false;
                grvProb.DataSource = null;
                grvProb.DataBind();

                ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "' and " + ViewState["xuhao_str"] + "";
                if (rblInMs.SelectedIndex != 0)
                {
                    ViewState["sqlText"] += " and BM_ISMANU='" + rblInMs.SelectedValue.ToString() + "'";
                }
                UCPagingMS.CurrentPage = 1;
                this.InitVarMS();
                this.bindGridMS();
            }
            else if (rblMSAdjust.SelectedValue == "1")//全部
            {
                rblInMs.Visible = true;
                lalInMS.Visible = true;
                lblQueryXuhao.Visible = true;
                dplAllXuhao.Visible = true;
                lblQueryXuhao.Visible = true;
                dplAllXuhao.Visible = true;
                grvProb.DataSource = null;
                grvProb.DataBind();

                dplAllXuhao.SelectedIndex = 0;

                ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "'";
                if (rblInMs.SelectedIndex != 0)
                {
                    ViewState["sqlText"] += " and BM_ISMANU='" + rblInMs.SelectedValue.ToString() + "'";
                }
                UCPagingMS.CurrentPage = 1;
                this.InitVarMS();
                this.bindGridMS();
            }
            else if (rblMSAdjust.SelectedValue == "2")//问题序号
            {
                rblInMs.Visible = false;
                lalInMS.Visible = false;

                lblQueryXuhao.Visible = false;
                dplAllXuhao.Visible = false;

                lblQueryXuhao.Visible = false;
                dplAllXuhao.Visible = false;

                grv.DataSource = null;
                grv.DataBind();
                UCPagingMS.Visible = false;
                NoDataPanel.Visible = false;
                this.ProblemIndexDataBind();
            }
        }
        protected void rblInMs_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMSAdjust.SelectedValue == "0")//当前调整
            {
                ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "' and " + ViewState["xuhao_str"] + "";
                if (rblInMs.SelectedIndex != 0)
                {
                    ViewState["sqlText"] += " and BM_ISMANU='" + rblInMs.SelectedValue.ToString() + "'";
                }
                UCPagingMS.CurrentPage = 1;
                this.InitVarMS();
                this.bindGridMS();
            }
            else if (rblMSAdjust.SelectedValue == "1")//全部
            {
                ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "'";
                if (rblInMs.SelectedIndex != 0)
                {
                    ViewState["sqlText"] += " and BM_ISMANU='" + rblInMs.SelectedValue.ToString() + "'";
                }
                if (dplAllXuhao.SelectedIndex != 0)
                {
                    ViewState["sqlText"] += " and BM_XUHAO like '" + dplAllXuhao.SelectedValue.ToString() + "%'";
                }
                UCPagingMS.CurrentPage = 1;
                this.InitVarMS();
                this.bindGridMS();
            }
        }
        /// <summary>
        /// 绑定存在问题的序号
        /// </summary>
        private void ProblemIndexDataBind()
        {
            ProbIndex prob = new ProbIndex();
            prob.StrTabeleName = ViewState["str_table"].ToString();
            prob.TaskID=ViewState["TaskId"].ToString();
            DataTable dt = this.ExecRetProbTable(prob);
            grvProb.DataSource = dt;
            grvProb.DataBind();
            if (grvProb.Rows.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
        private class ProbIndex
        {
            private string _StrTabeleName;
            private string _TaskID;
            public string StrTabeleName
            {
                get { return _StrTabeleName; }
                set { _StrTabeleName = value; }
            }
            public string TaskID
            {
                get { return _TaskID; }
                set { _TaskID = value; }
            }
        }
        /// <summary>
        /// 执行存储过程验证序号（保存时）
        /// </summary>
        /// <param name="psv"></param>
        private DataTable ExecRetProbTable(ProbIndex probindex)
        {
            DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_FindNoFatherIndex]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrTable",probindex.StrTabeleName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BM_ENGID", probindex.TaskID, SqlDbType.Text, 1000);
                sqlConn.Open();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        /// <summary>
        /// 序号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplAllXuhao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            rblMSAdjust.SelectedIndex = 1;//显示内容：全部
            ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "'";
            if (rblInMs.SelectedIndex != 0)
            {
                ViewState["sqlText"] += " and BM_ISMANU='" + rblInMs.SelectedValue.ToString() + "'";
            }
            if (dplAllXuhao.SelectedIndex != 0)
            {
                ViewState["sqlText"] += " and BM_XUHAO like '" + dplAllXuhao.SelectedValue.ToString() + "%'";
            }
            UCPagingMS.CurrentPage = 1;
            this.InitVarMS();
            this.bindGridMS();
        }
        #region  分页
        PagerQueryParam pager_ms = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVarMS()
        {
            InitPagerMS();
            UCPagingMS.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            UCPagingMS.PageSize = pager_ms.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPagerMS()
        {
            pager_ms.TableName = ViewState["view_table"].ToString();
            pager_ms.PrimaryKey = "BM_ID";
            pager_ms.ShowFields = "";
            pager_ms.OrderField = "dbo.f_formatstr(BM_XUHAO, '.')";
            pager_ms.StrWhere = ViewState["sqlText"].ToString();
            pager_ms.OrderType = 0;//升序排列
            pager_ms.PageSize = 500;
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGridMS();
        }
        private void bindGridMS()
        {
            pager_ms.PageIndex = UCPagingMS.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_ms);
            CommonFun.Paging(dt, grv, UCPagingMS, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPagingMS.Visible = false;
            }
            else
            {
                UCPagingMS.Visible = true;
                UCPagingMS.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion

    }
}
