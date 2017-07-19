using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZCZJ_DPF;
using System.Data;
using System.Data.SqlClient;

namespace testpage
{
    public partial class WebForm1 : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "任务号管理";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                this.GetBoundData();
            }
            Warn();
            CheckUser(ControlFinder);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aa", "aa()", true);
        }

        #region 分页
        protected void GetBoundData()
        {
            InitPager();
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

        private void InitPager()
        {
            pager.TableName = "TBCM_PLAN as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID left join (select * from (select no=row_number() over(partition by ID order by CM_ID),* from TBCM_BASIC) t where no=1) as c on a.ID=c.ID";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "a.*,b.ST_NAME as CM_NAME,c.TSA_ENGNAME";
            pager.OrderField = "CM_ZDTIME";
            pager.StrWhere = CreateConStr(1);
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr(int type)
        {
            string sql = string.Empty;
            string userID = UserID.Value;
            string status = rbl_status.SelectedValue;
            sql = "CM_TASKTYPE='0' and CM_ZDTIME like'%" + txt_Data.Text + "%' and CM_COMP like '%" + txt_YeZhu.Text + "%' and CM_DFCONTR like '%" + txt_HeTong.Text + "%' ";
            if (rbl_mytask.SelectedValue == "0")
            {
                if (type == 1)
                {
                    sql += "and CM_SPSTATUS='" + status + "' ";
                }
                GridView1.Columns[11].Visible = false;
                //GridView1.Columns[11].Visible = false;
            }
            else
            {
                GridView1.Columns[11].Visible = true;
                //GridView1.Columns[11].Visible = true;
                if (type == 1)
                {
                    sql += "and CM_SPSTATUS='" + status + "' ";
                    sql += string.Format("and ((CM_PS1='{0}' and CM_PSYJ1='{1}') or (CM_PS2='{0}' and CM_PSYJ2='{1}' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='{1}' and CM_PSYJ1='2' and CM_PSYJ2='2')) ", userID, status);
                }
            }
            switch (ddlSearch.SelectedValue)
            {
                case "1":
                    sql += "and CM_CONTR LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "2":
                    sql += "and a.ID in (select ID from TBCM_BASIC where TSA_ENGNAME LIKE '%" + searchcontent.Text.Trim() + "%')";
                    break;
                case "3":
                    sql += "and CM_PROJ LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "4":
                    sql += "and a.ID in (select ID from TBCM_BASIC where TSA_MAP LIKE '%" + searchcontent.Text.Trim() + "%')";
                    break;
                case "5":
                    sql += "and ST_NAME LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }

        #endregion

        protected void Warn()
        {
            string con = "";

            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            string sum = string.Format("select ID from View_CM_Task as a where CM_SPSTATUS='1' {0} and {1} group by ID", con, CreateConStr(2));
            rbl_status.Items[1].Text = string.Format("待审核（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_PS1='{0}' and CM_PSYJ1='2') or (CM_PS2='{0}' and CM_PSYJ2='2' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='2' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            sum = string.Format("select ID from View_CM_Task as a where CM_SPSTATUS='2' {0} and {1} group by ID", con, CreateConStr(2));
            rbl_status.Items[3].Text = string.Format("已通过（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_PS1='{0}' and CM_PSYJ1='3') or (CM_PS2='{0}' and CM_PSYJ2='3' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='3' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            sum = string.Format("select ID from View_CM_Task as a where CM_SPSTATUS='3' {0} and {1} group by ID", con, CreateConStr(2));
            rbl_status.Items[2].Text = string.Format("已驳回（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);

            sum = string.Format("select count(ID) from View_CM_Task as a where CM_SPSTATUS='0' and {0}", CreateConStr(2));
            rbl_status.Items[0].Text = string.Format("初始化(<font color='red'>{0}</font>)",DBCallCommon.GetDTUsingSqlText(sum).Rows[0][0]);
        }

        //protected string editYg(string YgId)
        //{
        //    string sql = "select ID from TBCM_PLAN where ID='" + YgId + "'";
        //    string psId = string.Empty;
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        psId = dt.Rows[0]["ID"].ToString();
        //    }
        //    return "javascript:window.showModalDialog('CM_AddTask.aspx?action=edit&Id=" + YgId + "&psId=" + psId + "','','dialogWidth=900px;dialogHeight=500px')";
        //}

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                #region MyRegion

                string id = ((HiddenField)e.Row.FindControl("id")).Value;

                //if (str.Count < 1)
                //{
                //    str.Add(id);
                //    str.Add(e.Row.Cells[3].Text);
                //    Bindgv(e, id);
                //}
                //else
                //{
                //    if (str.Contains(id))
                //    {
                //        e.Row.Cells[1].Text = "";//合并
                //        e.Row.Cells[2].Text = "";
                //        e.Row.Cells[4].Text = "";
                //        e.Row.Cells[5].Text = "";
                //        e.Row.Cells[10].Text = "";
                //        e.Row.Cells[11].Text = "";//制单日期
                //        e.Row.Cells[12].Text = "";//合并制单人
                //        e.Row.Cells[13].Text = "";//合并审核状态
                //        e.Row.Cells[14].Text = "";//合并查看和评审
                //        e.Row.Cells[15].Text = "";
                //        e.Row.Cells[16].Text = "";
                //        if (str.Contains(e.Row.Cells[3].Text))
                //        {
                //            e.Row.Cells[3].Text = "";
                //        }
                //        else
                //        {
                //            str.Add(e.Row.Cells[3].Text);
                //        }
                //    }
                //    else
                //    {
                //        str.Add(id);
                //        str.Add(e.Row.Cells[3].Text);
                //        Bindgv(e, id);
                //    }
                //}
                Bindgv(e, id);
                #endregion
            }
        }

        protected void Bindgv(GridViewRowEventArgs e, string id)
        {
            #region 提醒代码
            if (rbl_mytask.SelectedValue == "1")
            {
                string sql_psyj = string.Format("select * from View_CM_Task where (CM_PS1='{0}' and CM_PSYJ1='{1}') or (CM_PS2='{0}' and CM_PSYJ2='{1}' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='{1}' and CM_PSYJ1='2' and CM_PSYJ2='2') ", UserID.Value, rbl_status.SelectedValue);
                DataTable dt_yj = DBCallCommon.GetDTUsingSqlText(sql_psyj);
                if (dt_yj.Rows.Count > 0)
                {
                    if (dt_yj.Rows[0]["CM_SPSTATUS"].ToString() == "1")
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[1].Text != "")
                {
                    HiddenField status = (HiddenField)e.Row.FindControl("status");
                    if (status.Value == "3")
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        e.Row.Cells[11].Text = "";//驳回之后不可审
                    }
                    if (status.Value == "2")
                    {
                        e.Row.Cells[11].Text = "";//通过之后不可审
                    }
                    if (status.Value=="0")
                    {
                        e.Row.Cells[11].Text = "";//初始化不能审
                    }
                }
            }

            #endregion

            //制单人登录，当申请被驳回，或者没人开始审批时可修改
            string yjs = string.Empty;
            string sql = "select * from View_CM_Task where ID='" + id + "'";
            DataTable ps = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "select * from TBCM_PLAN where ID='" + id + "' and CM_MANCLERK='" + UserID.Value + "'";
            DataTable ps1 = DBCallCommon.GetDTUsingSqlText(sql);
            if (ps1.Rows.Count != 0)
            {
                //e.Row.Cells[15].Text = "";//制单人不可评审
                if (ps.Rows.Count != 0)//在审的时候不可修改
                {
                    DataRow tr = ps.Rows[0];
                    if (((tr["CM_PSYJ1"].ToString() != "1" || (tr["CM_PSYJ2"].ToString() != "1" && tr["CM_PSYJ2"].ToString() != "") || (tr["CM_PSYJ3"].ToString() != "1" && tr["CM_PSYJ3"].ToString() != "")) && (tr["CM_SPSTATUS"].ToString() != "3")))
                    {
                        e.Row.Cells[12].Text = "";
                    }
                }
            }
            else
            {
                e.Row.Cells[12].Text = "";
            }

            //待审批状态时不可修改
            if (rbl_status.SelectedValue == "1")
            {
                e.Row.Cells[12].Text = "";
            }
            if (((HiddenField)e.Row.FindControl("CM_CANCEL")).Value=="1")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            int n = 0;
            string find_whetherkg = "select TSA_STATE from TBPM_TCTSASSGN where ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(find_whetherkg);
            if (dt.Rows.Count != 0)
            {
                if (Convert.ToInt16(dt.Rows[0]["TSA_STATE"].ToString()) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法删除！！！\\r\\r该任务已分工！！！');", true);
                    return;
                }
                else
                {
                    n++;
                }
            }
            else
            {
                n++;
            }
            if (n > 0)
            {
                List<string> list_sql = new List<string>();
                string sqltext = "delete from TBCM_BASIC where ID='" + id + "'";//任务号基本信息表
                list_sql.Add(sqltext);
                sqltext = "delete from TBCM_PLAN where ID='" + id + "'";//任务号计划表
                list_sql.Add(sqltext);
                sqltext = "delete from TBCM_PSVIEW where CM_ID='" + id + "'";//评审表
                list_sql.Add(sqltext);
                sqltext = "delete from TBPM_TCTSASSGN where ID='" + id + "'";//技术部表单
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                this.GetBoundData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
            }
        }

        protected void rbl_mytask_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

    }
}
