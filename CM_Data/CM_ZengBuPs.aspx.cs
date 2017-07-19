using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ZengBuPs : System.Web.UI.Page
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
                        //GridView1.Columns[9].Visible = false;
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
                con = string.Format("and ((CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            string sum = string.Format("select CH_ID from View_CM_Change where CM_TYPE='1' and CM_STATE='1' {0}", con);
            rbl_status.Items[0].Text = string.Format("待审核（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_PS1='{0}' and CM_PSYJ1='2') or (CM_PS2='{0}' and CM_PSYJ2='2' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='2' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            sum = string.Format("select CH_ID from View_CM_Change where CM_TYPE='1' and CM_STATE='2' {0}", con);
            rbl_status.Items[2].Text = string.Format("已通过（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_PS1='{0}' and CM_PSYJ1='3') or (CM_PS2='{0}' and CM_PSYJ2='3' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='3' and CM_PSYJ1='2' and CM_PSYJ2='2'))", UserID.Value);
            }
            sum = string.Format("select CH_ID from View_CM_Change where CM_TYPE='1' and CM_STATE='3' {0}", con);
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
            pager.TableName = "TBCM_CHANLIST as a left join TBCM_PLAN as b on a.ID=b.ID left join TBDS_STAFFINFO as c on a.CM_MANCLERK=c.ST_ID";
            pager.PrimaryKey = "CH_ID";
            pager.ShowFields = "b.*,a.CM_STATE,a.CH_ID,c.ST_NAME,a.CM_ZDTIME as ZDTIME";
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
            sql = "CM_TYPE='1' ";
            if (rbl_mytask.SelectedValue == "0")
            {
                sql += "and CM_STATE='" + status + "' ";
                //GridView1.Columns[9].Visible = false;
                //GridView1.Columns[10].Visible = false;
            }
            else
            {
                //GridView1.Columns[9].Visible = true;
                //GridView1.Columns[10].Visible = true;
                sql += string.Format("and ((a.CM_PS1='{0}' and a.CM_PSYJ1='{1}') or (a.CM_PS2='{0}' and a.CM_PSYJ2='{1}' and a.CM_PSYJ1='2') or (a.CM_PS3='{0}' and a.CM_PSYJ3='{1}' and a.CM_PSYJ1='2' and a.CM_PSYJ2='2')) ", userID, status);
            }
            switch (ddlSearch.SelectedValue)
            {
                case "1":
                    sql += "and CM_CONTR LIKE '%" + searchcontent.Text.Trim() + "%' ";
                    break;
                case "2":
                    sql += "and a.ID in (select ID from TBCM_CHANLIST where TSA_ENGNAME LIKE '%" + searchcontent.Text.Trim() + "%')";
                    break;
                case "3":
                    sql += "and CM_PROJ LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "4":
                    sql += "and a.ID in (select ID from TBCM_CHANLIST where TSA_MAP LIKE '%" + searchcontent.Text.Trim() + "%')";
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

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                HiddenField status = (HiddenField)e.Row.FindControl("status");
                string id = ((HiddenField)e.Row.FindControl("id")).Value;
                string sql = "select * from TBCM_CHANLIST where CH_ID='" + id + "' and CM_MANCLERK='" + UserID.Value + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count != 0)//制单人登录
                {
                    DataRow dr = dt.Rows[0];
                    if (((dr["CM_PSYJ1"].ToString() != "1" || (dr["CM_PSYJ2"].ToString() != "1" && dr["CM_PSYJ2"].ToString() != "") || (dr["CM_PSYJ3"].ToString() != "1" && dr["CM_PSYJ3"].ToString() != "")) && (dr["CM_STATE"].ToString() != "3")))
                    {
                        e.Row.Cells[11].Text = "";
                    }
                }
                if (status.Value == "2")//通过的不能修改
                {
                    e.Row.Cells[11].Text = "";
                }
                //通过驳回之后不可评审
                if (status.Value != "1")
                {
                    e.Row.Cells[10].Text = "";
                }
            }
        }

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

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql = "delete from TBCM_CHANLIST where CH_ID='" + ((LinkButton)sender).CommandArgument + "'";
            list.Add(sql);
            sql = "delete from TBCM_CHANGE where CH_ID='" + ((LinkButton)sender).CommandArgument + "'";
            list.Add(sql);
            DBCallCommon.ExecuteTrans(list);
        }
    }
}
