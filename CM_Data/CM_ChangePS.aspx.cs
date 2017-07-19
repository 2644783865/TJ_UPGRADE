using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ChangePS : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                DepID.Value = Session["UserDeptID"].ToString();
                switch (DepID.Value)
                {
                    case "07":
                        rbl_mytask.SelectedValue = "1";
                        rbl_status.SelectedValue = "1";
                        depchange.Visible = true;
                        GridView1.Columns[9].Visible = false;
                        break;
                    default:
                        rbl_confirm.Visible = true;
                        break;
                }
                this.GetBoundData();
            }
            Warn();
        }

        protected void Warn()
        {
            string con = "";
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_MANCLERK='{0}') or (CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            string sum = string.Format("select CH_ID from View_CM_Change where CM_TYPE='0' and CM_STATE='1' {0}", con);
            rbl_status.Items[0].Text = string.Format("待审核（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_MANCLERK='{0}') or (CM_PS1='{0}' and CM_PSYJ1='2') or (CM_PS2='{0}' and CM_PSYJ2='2' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='2' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            sum = string.Format("select CH_ID from View_CM_Change where CM_TYPE='0' and CM_STATE='2' {0}", con);
            rbl_status.Items[2].Text = string.Format("已通过（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_MANCLERK='{0}') or (CM_PS1='{0}' and CM_PSYJ1='3') or (CM_PS2='{0}' and CM_PSYJ2='3' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='3' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            sum = string.Format("select CH_ID from View_CM_Change where CM_TYPE='0' and CM_STATE='3' {0}", con);
            rbl_status.Items[1].Text = string.Format("已驳回（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
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
            pager.TableName = "View_CM_Change";
            pager.PrimaryKey = "CH_ID";
            pager.ShowFields = "*";
            pager.OrderField = "CH_ID";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr()
        {
            string sql = string.Empty;
            string userID = UserID.Value;
            string status = rbl_status.SelectedValue;
            switch (DepID.Value)
            {
                case "07":
                    sql = "CM_TYPE='0' ";
                    break;
                default:
                    sql = "1=1 ";
                    break;
            }
            if (rbl_mytask.SelectedValue == "0")
            {
                sql += "and CM_STATE='" + status + "' ";
                GridView1.Columns[7].Visible = false;
                GridView1.Columns[8].Visible = false;
            }
            else
            {
                GridView1.Columns[7].Visible = true;
                GridView1.Columns[8].Visible = true;
                sql += string.Format("and ((CM_MANCLERK='{0}' and CM_STATE='{1}') or (CM_PS1='{0}' and CM_PSYJ1='{1}') or (CM_PS2='{0}' and CM_PSYJ2='{1}' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='{1}' and CM_PSYJ1='2' and CM_PSYJ2='2')) ", userID, status);
            }
            switch (ddlSearch.SelectedValue)
            {
                case "1":
                    sql += "and CM_CONTR LIKE '%" + searchcontent.Text.Trim() + "%' ";
                    break;
            }
            switch (DepID.Value)
            {
                case "03":
                    sql += "and CM_BT1='" + rbl_confirm.SelectedValue + "'";
                    break;
                case "04":
                    sql += "and CM_BT2='" + rbl_confirm.SelectedValue + "'";
                    break;
                case "05":
                    sql += "and CM_BT3='" + rbl_confirm.SelectedValue + "'";
                    break;
                default:
                    break;
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }

        #endregion

        protected void rbl_mytask_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                string id = ((HiddenField)e.Row.FindControl("chid")).Value;
                Bindgv(e, id);
            }
        }

        protected void Bindgv(GridViewRowEventArgs e, string id)
        {

            #region 提醒代码

            HiddenField status = (HiddenField)e.Row.FindControl("status");
            if (rbl_mytask.SelectedValue == "1")
            {
                string sql_psyj = string.Format("select * from View_CM_Change where (CM_PS1='{0}' and CM_PSYJ1='{1}') or (CM_PS2='{0}' and CM_PSYJ2='{1}' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='{1}' and CM_PSYJ1='2' and CM_PSYJ2='2') ", UserID.Value, rbl_status.SelectedValue);
                DataTable dt_yj = DBCallCommon.GetDTUsingSqlText(sql_psyj);
                if (dt_yj.Rows.Count > 0)
                {
                    if (dt_yj.Rows[0]["CM_STATE"].ToString() == "1")
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    }

                    if (status.Value == "3")
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        e.Row.Cells[8].Text = "";//驳回之后不可审
                    }
                }
                if (status.Value == "2")
                {
                    e.Row.Cells[8].Text = "";//通过之后不可审
                }
            }

            #endregion

            //制单人登录，当申请被驳回，或者没人开始审批时可修改
            string yjs = string.Empty;
            string sql = "select * from View_CM_Change where CH_ID='" + id + "'";
            DataTable ps = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "select * from TBCM_CHANLIST where CH_ID='" + id + "' and CM_MANCLERK='" + UserID.Value + "'";
            DataTable ps1 = DBCallCommon.GetDTUsingSqlText(sql);
            if (ps1.Rows.Count != 0)
            {
                //e.Row.Cells[8].Text = "";//制单人不可评审
                if (ps.Rows.Count != 0)//在审的时候不可修改
                {
                    DataRow tr = ps.Rows[0];
                    if (((tr["CM_PSYJ1"].ToString() != "1" || (tr["CM_PSYJ2"].ToString() != "1" && tr["CM_PSYJ2"].ToString() != "") || (tr["CM_PSYJ3"].ToString() != "1" && tr["CM_PSYJ3"].ToString() != "")) && (tr["CM_STATE"].ToString() != "3")))
                    {
                        e.Row.Cells[7].Text = "";
                    }
                }
            }
            else
            {
                e.Row.Cells[7].Text = "";
            }
        }

        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            string ch_id = ((LinkButton)sender).CommandArgument;
            string sql = "";
            switch (DepID.Value)
            {
                case "03":
                    sql = string.Format("update TBCM_CHANLIST set CM_BT1='1' where CH_ID='{0}'", ch_id);
                    break;
                case "04":
                    sql = string.Format("update TBCM_CHANLIST set CM_BT2='1' where CH_ID='{0}'", ch_id);
                    break;
                case "05":
                    sql = string.Format("update TBCM_CHANLIST set CM_BT3='1' where CH_ID='{0}'", ch_id);
                    break;
                default:
                    break;
            }
            if (sql != "")
            {
                DBCallCommon.ExeSqlText(sql);
                GetBoundData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('你无权进行此操作！');", true); return;
                //Response.Write("<script>alert('你无权进行此操作！');</script>"); return;//后台验证
            }
        }
    }
}
