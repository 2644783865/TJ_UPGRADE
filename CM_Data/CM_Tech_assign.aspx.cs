using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Tech_assign : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitVar();

            if (!IsPostBack)
            {
                InitInfo();
            }
        }

        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            this.GetBoundData();
            //待分工无法进行工程拆分
            GridView1.Columns[GridView1.Columns.Count - 3].Visible = false;
        }

        /// <summary>
        /// 获取查询Where条件
        /// </summary>
        /// <returns></returns>
        private string CreateConStr()
        {
            string sql = "";
            //sql += " TSA_STATE=";
            //sql +=  + rblstatus.SelectedValue + "' ";
            if (ddlSearch.SelectedItem.Text.Trim() != "-请选择-")
            {
                sql += " TSA_STATE='" + ddlSearch.SelectedValue.Trim() + "  like '%" + txtSearch.Text.Trim() + "%'";
            }

            if (ddlDQ.SelectedIndex != 0)
            {
                sql += " and TSA_ASSIGNTOELC='" + ddlDQ.SelectedValue + "'";
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
            this.GetBoundData();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string taskid = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string find_whetherkg = "select TSA_STATE from TBPM_TCTSASSGN where TSA_ID='" + taskid + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(find_whetherkg);
                if (Convert.ToInt16(dt.Rows[0]["TSA_STATE"].ToString()) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法删除！！！\\r\\r该任务已分工！！！');", true);
                    return;
                }
                else
                {
                    if (!taskid.Contains("-"))
                    {
                        DataTable dt_reference = this.CheckTaskIDReference(taskid);
                        if (dt_reference.Rows[0][0].ToString() != "OK")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法删除，生产制号【" + taskid + "】已引用！！！');", true);
                            return;
                        }
                    }

                    List<string> list_sql = new List<string>();
                    string sqltext = "delete from TBPM_TCTSASSGN where TSA_ID='" + taskid + "'";//任务分工表
                    list_sql.Add(sqltext);
                    string sqltextgcl = "delete from TBPM_TCTSENROLL where TSA_ID='" + taskid + "'";//工程量表
                    list_sql.Add(sqltextgcl);
                    string sqlbmconfirm = "delete from TBCB_BMCONFIRM where TASK_ID='" + taskid + "'";//部门完工确认表
                    list_sql.Add(sqlbmconfirm);
                    string sqlsc = "delete from TBMP_MANUTSASSGN where MTA_ID='" + taskid + "'";//生产部表
                    list_sql.Add(sqlsc);

                    if (!taskid.Contains("-"))
                    {
                        string sqlsplit = "delete from TBPM_TCTSASSGN where TSA_ID like '" + taskid + "-%' and TSA_STATE='0'";
                        list_sql.Add(sqlsplit);
                    }

                    DBCallCommon.ExecuteTrans(list_sql);
                    this.GetBoundData();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
                }
            }
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Label)e.Row.FindControl("lblID")).Text.Contains("-") || ((Label)e.Row.FindControl("lblID")).Text.Contains("(DQ)") || ((Label)e.Row.FindControl("lblID")).Text.Contains("(DQO)"))
                {
                    //((HyperLink)e.Row.FindControl("splitTask")).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lnkDelete")).Visible = false;
                    ((Image)e.Row.FindControl("Image11")).Visible = false;
                }

                string sczh = e.Row.Cells[2].Text.Trim();
                if (e.Row.Cells[2].Text.Contains("-"))
                {
                    e.Row.Attributes.Add("ondblclick", "ShowOrg('" + sczh + "')");
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Attributes.Add("title", "双击进入原始数据查看");
                }
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
            pager.TableName = "View_TM_TaskAssign";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "TSA_ID,TSA_PJNAME+'('+TSA_PJID+')' as TSA_PJNAME,TSA_ENGNAME,TSA_CONTYPE,TSA_RECVDATE,TSA_TCCLERKNM,TSA_STARTDATE,TSA_PLANFSDATE,TSA_REALFSDATE,TSA_MANCLERKNAME,TSA_STATE,TSA_ADDTIME,TSA_NUMBER";
            pager.OrderField = ddlSort.SelectedValue;
            pager.StrWhere = CreateConStr();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 15;
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
    }
}
