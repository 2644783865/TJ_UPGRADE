using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Task_Manage : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            this.InitVar();

            if (!IsPostBack)
            {

                InitInfo();
            }
            CheckUser(ControlFinder);
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            zjy_bind();
            this.GetBoundData();


        }

        protected void zjy_bind()
        {
            string sqltext = "select  QSA_QCCLERKNM from View_TCTSASSGN_QTSASSGN where QSA_QCCLERKNM is not null  group by QSA_QCCLERKNM ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            drp_zjy.DataSource = dt;
            drp_zjy.DataTextField = "QSA_QCCLERKNM";
            drp_zjy.DataValueField = "QSA_QCCLERKNM";
            drp_zjy.DataBind();
            drp_zjy.Items.Insert(0, new ListItem("请选择", "请选择"));
            drp_zjy.SelectedIndex = 0;
        }

        /// <summary>
        /// 获取查询Where条件
        /// </summary>
        /// <returns></returns>
        private string CreateConStr()
        {
            string sql = " 1=1 ";

            if (rblstatus.SelectedValue != "")
            {
                sql += " and QSA_STATE='" + rblstatus.SelectedValue + "' ";
            }
            if (ddlSearch.SelectedItem.Text.Trim() != "-请选择-")
            {
                sql += "and " + ddlSearch.SelectedValue.Trim() + "  like '%" + txtSearch.Text.Trim() + "%'";
            }

            if (IsPostBack)
            {
                udqMS.ExistedConditions = sql;
                sql = sql + UserDefinedQueryConditions.ReturnQueryString((GridView)udqMS.FindControl("GridView1"), (Label)udqMS.FindControl("Label1"));
            }
            if (drp_zjy.SelectedIndex != 0 && drp_zjy.SelectedIndex != -1)
            {
                sql += "AND QSA_QCCLERKNM='" + drp_zjy.SelectedValue + "'";
            }
            return sql;
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetBoundData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            this.GetBoundData();
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            if (rblstatus.SelectedValue == "0")
            {

                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;//任务分工
            }
            else if (rblstatus.SelectedValue == "1")
            {

                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;//任务分工
            }
            else if (rblstatus.SelectedValue == "2")
            {

                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;//任务分工
            }
            else if (rblstatus.SelectedValue == "3")//停工
            {

                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;//任务分工
            }
            this.GetBoundData();
        }



        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string QSAId = ((HtmlInputHidden)e.Row.FindControl("hidQSAID")).Value;
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
                e.Row.Cells[e.Row.Cells.Count - 1].Attributes["style"] = "Cursor:hand";
                // ((HyperLink)e.Row.FindControl("hlTask")).Attributes.Add("onClick", "openLink('" + QSAId + "');");

                // ((HyperLink)e.Row.FindControl("hl_re_task")).Attributes.Add("onClick", "openLink('" + QSAId + "');");

            }

        }


        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "View_TCTSASSGN_QTSASSGN";
            pager.PrimaryKey = "QSA_ID";
            pager.ShowFields = "*";
            pager.OrderField = ddlSort.SelectedValue;
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
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

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMSClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqMS.FindControl("GridView1"));
        }

        /// <summary>
        /// 执行存储过程验证序号（计算时）
        /// </summary>
        /// <param name="psv"></param>
        private DataTable CheckTaskIDReference(string TaskID)
        {
            DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_CheckTaskIDReference]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TaskID", TaskID, SqlDbType.Text, 1000);
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
        /// 批量分工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbnPlentyAssign_Click(object sender, EventArgs e)
        {
            string taskIDS = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow grow = GridView1.Rows[i];
                HtmlInputCheckBox cbx = (HtmlInputCheckBox)grow.FindControl("cbx");
                if (cbx.Checked)
                {
                    taskIDS += grow.Cells[3].Text.Trim() + "/";

                }
            }
            if (taskIDS != "")
            {
                Response.Redirect("QC_Task_Assign_piliang.aspx?taskID=" + taskIDS);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:请勾选至少一条任务');", true);
            }


        }
    }
}
