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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_ProjectPlan_Detail : System.Web.UI.Page
    {
        string id = "";
        string MS_PLAN = "";
       
        string engid = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            MS_PLAN = Request.QueryString["Plan"];
            id = Request.QueryString["mnpid"];
            Info();
            InitVar(id);
            InitVarPager();
            if (!IsPostBack)
            {
                initinfo();
                UCPaging1.CurrentPage = 1;
                GetManutAssignData(); //数据绑定

            }

        }

        private void initinfo()
        {    
            string sqltext = "";
            sqltext = "select ST_ID,ST_NAME from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "'and ST_PD='0'order by ST_ID DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_fuziren.DataSource = dt;
            cob_fuziren.DataTextField = "ST_NAME";
            cob_fuziren.DataValueField = "ST_ID";
            cob_fuziren.DataBind();
            cob_sqren.DataSource = dt;
            cob_sqren.DataTextField = "ST_NAME";
            cob_sqren.DataValueField = "ST_ID";
            cob_sqren.DataBind();
            cob_fuziren.SelectedIndex = 0;
            cob_sqren.SelectedValue = Session["UserID"].ToString();
            TextBoxexecutor.Text = Session["UserName"].ToString();
            TextBoxexecutorid.Text = Session["UserID"].ToString();
            TextBoxexecutor.Enabled = false;
        }
        protected void Info()
        { 
            if (MS_PLAN == "1")
            {
                Response.Redirect("PM_ProjectPlan_View.aspx?mnpid="+id+"&&action=view");
            }       
        }
        protected void InitVar(string str)
        {
            string sqlselect = "select MS_ID,MS_ENGNAME,MS_PJID,MS_ENGID,MS_SUBMITNM,MS_SUBMITTM,MS_ADATE from View_TM_MSFORALLRVW where MS_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlselect);
            if (dt.Rows.Count > 0)
            {
                tsa_id.Text = dt.Rows[0]["MS_ENGID"].ToString();
                engid = dt.Rows[0]["MS_ENGID"].ToString();
                lab_proname.Text = dt.Rows[0]["MS_PJID"].ToString();
                lab_engname.Text = dt.Rows[0]["MS_ENGNAME"].ToString();
                ms_no.Text = dt.Rows[0]["MS_ID"].ToString();
                txt_plandate.Text = dt.Rows[0]["MS_SUBMITTM"].ToString();
                lbltcname.Text = dt.Rows[0]["MS_SUBMITNM"].ToString();
            }
            string sqltext = "select CM_JHTIME from TBPM_MSFORALLRVW as A left outer join View_CM_FaHuo as B on A.MS_ENGID=B.TSA_ID where MS_ID='" + id + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt1.Rows.Count > 0)
            {
                endTime.Text = dt1.Rows[0]["CM_JHTIME"].ToString();
            }
        }
        private void InitVarPager()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager.TableName = "View_TM_TASKDQO";
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = "";
            pager.OrderField = "dbo.f_formatstr(MS_ZONGXU, '.')";
            pager.StrWhere = "MS_PID='" + id + "'";
            pager.OrderType = 0;//按任务名称降序排列
            pager.PageSize = 50;
        }
        private void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            GetManutAssignData();
        }
        protected void GetManutAssignData()
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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblkeycoms = (Label)e.Row.FindControl("lblkeycoms");
            //    Label lblwaixie = (Label)e.Row.FindControl("lblwaixie");
            //    if (lblkeycoms.Text.ToString() == "关键部件")
            //    {
            //        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
            //    }
            //    if (lblwaixie.Text.ToString() == "1")
            //    {
            //        e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
            //        e.Row.Cells[1].Attributes.Add("title", "已生成生产外协计划!");
            //    }

            //}
        }


        protected void btnproplan_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string sqlText = "";
            string sqltxt = "";
            int n = 0;
            string id = ms_no.Text.Trim().ToString();
            List<string> sqlstr = new List<string>();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chbxcheck = (CheckBox)gr.FindControl("chbxcheck");
                Label lblzhujian = (Label)gr.FindControl("lblmsid");
                if (chbxcheck.Checked)
                {
                    n++;
                    sqlText = "insert into TBPM_PROJ_PLAN (PM_PID,PM_SUBMITID,PM_SUBMITTM,MS_ID) values ('" + ms_no.Text.ToString().Trim() + "','" + TextBoxexecutorid.Text.ToString().Trim() + "','" + DateTime.Now.ToString() + "','" + lblzhujian.Text + "')";
                    sqltext = "update View_TM_TASKDQO set MS_PLAN='1' where MS_ID='" + lblzhujian.Text + "'";//需要制定项目计划
                    sqlstr.Clear();
                    sqlstr.Add(sqltext);
                    sqlstr.Add(sqlText);
                    DBCallCommon.ExecuteTrans(sqlstr);
                }
            }
            if (n == 0)
            {
                Response.Write("<script>alert('请勾选生成项目计划的任务！');</script>");
                return;
            }
            else 
            {
                sqltxt = "update TBPM_MSFORALLRVW set MS_PLAN='1'where MS_ID='" + id + "'";//制定项目计划中
                sqlstr.Clear();
                sqlstr.Add(sqltxt);
                DBCallCommon.ExecuteTrans(sqlstr);
                Response.Redirect("PM_ProjectPlan_View.aspx?mnpid=" + id + "&&action=plan");
            }
           
        }
    }
}
