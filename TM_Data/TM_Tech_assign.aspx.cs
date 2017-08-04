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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Tech_assign : BasicPage
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
            this.GetBoundData();

        }

        /// <summary>
        /// 获取查询Where条件
        /// </summary>
        /// <returns></returns>
        private string CreateConStr()
        {
            string sql = "";
            sql += " TSA_STATE='" + rblstatus.SelectedValue + "' ";
            if (ddlSearch.SelectedItem.Text.Trim() != "-请选择-")
            {
                sql += "and " + ddlSearch.SelectedValue.Trim() + "  like '%" + txtSearch.Text.Trim() + "%'";//下拉
            }

            if (IsPostBack)
            {
                udqMS.ExistedConditions = sql;
                sql = sql + UserDefinedQueryConditions.ReturnQueryString((GridView)udqMS.FindControl("GridView1"), (Label)udqMS.FindControl("Label1"));
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

                GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;//任务分工
            }
            else if (rblstatus.SelectedValue == "1")
            {

                GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;//任务分工
            }
            else if (rblstatus.SelectedValue == "2")
            {

                GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;//任务分工
            }
            else if (rblstatus.SelectedValue == "3")//停工
            {

                GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;//任务分工
            }
            this.GetBoundData();
            CheckUser(ControlFinder);
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string proId = e.Row.Cells[2].Text.Trim();

                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看数据");
                e.Row.Attributes.Add("ondblclick", "ShowOrg('" + proId + "')");
            }

            //for (int i = 1; i < 15; i++)
            //{
            //    e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
            //    e.Row.Cells[i].Attributes.Add("title", "双击修改原始数据");


            //}
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
            pager.TableName = "View_TM_TaskAssign";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "TSA_ID,CM_PROJ,TSA_PJID,TSA_ENGNAME,TSA_BUYER,TSA_MODELCODE,TSA_DEVICECODE,TSA_DESIGNCOM,TSA_CONTYPE,TSA_RECVDATE,TSA_DRAWSTATE,TSA_DELIVERYTIME,TSA_MANCLERKNAME,TSA_STARTTIME,TSA_MSFINISHTIME,TSA_MPCOLLECTTIME,TSA_TECHTIME,TSA_TUZHUANGTIME,TSA_ZHUANGXIANGDANTIME,TSA_TCCLERKNM,TSA_STARTDATE,TSA_STATE,TSA_ADDTIME,TSA_REVIEWER,ID";
            pager.OrderField = ddlSort.SelectedValue;
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
            CheckUser(ControlFinder);
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

        protected void lbnPlentyAssign_Click(object sender, EventArgs e)
        {
            string taskIDS = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow grow = GridView1.Rows[i];
                CheckBox cbx = (CheckBox)grow.FindControl("CheckBox1");
                if (cbx.Checked)
                {
                    taskIDS += grow.Cells[2].Text.Trim() + "/";

                }
            }
            if (taskIDS != "")
            {
                Response.Redirect("TM_Tech_assign_piliang.aspx?taskID=" + taskIDS);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:请勾选至少一条任务');", true);
            }


        }
        protected void btnExpotOrg_Click(object sender, EventArgs e)
        {
            string taskIDS = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow grow = GridView1.Rows[i];
                CheckBox cbx = (CheckBox)grow.FindControl("CheckBox1");
                if (cbx.Checked)
                {
                    taskIDS += grow.Cells[2].Text.Trim() + "/";

                }
            }
          
            if (taskIDS == ""||taskIDS.Trim('/').Split('/').Count()>1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:请勾选至少一条任务');", true);
            }
            else
            {
                ExportTMDataFromDB.ExportMsFromOrg(taskIDS.Trim('/'));
            }
        }


        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(GridView1, "CheckBox1");
        }
        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(GridView smartgridview, string ckbname)
        {
            int startindex = -1;
            int endindex = -1;
            for (int i = 0; i < smartgridview.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)smartgridview.Rows[i].FindControl(ckbname);
                if (cbx.Checked)
                {
                    startindex = i;
                    break;
                }
            }

            for (int j = smartgridview.Rows.Count - 1; j > -1; j--)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)smartgridview.Rows[j].FindControl(ckbname);
                if (cbx.Checked)
                {
                    endindex = j;
                    break;
                }
            }

            if (startindex < 0 || endindex < 0 || startindex == endindex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('需要勾选2条记录！！！');", true);
            }
            else
            {
                for (int k = startindex; k <= endindex; k++)
                {
                    System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)smartgridview.Rows[k].FindControl(ckbname);
                    cbx.Checked = true;
                }
            }
        }


    }
}
