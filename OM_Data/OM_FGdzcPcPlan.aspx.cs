using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_FGdzcPcPlan : System.Web.UI.Page
    {
        string carrvwa = "";
        string carrvwb = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {

                UCPaging1.PageSize = 1;
                GetBoundData();
            }
        }
        protected void rblXiaTui_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetBoundData();
        }

        protected void rblSPZT_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetBoundData();
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)grow.FindControl("CHK");
                if (chk.Checked)
                {
                    string code = ((Label)grow.FindControl("lblCode")).Text;

                    string sqltext = "delete from TBOM_GDZCPCAPPLY where CODE='" + code + "'";

                    list.Add(sqltext);
                    sqltext = "delete from TBOM_GDZCRVW where CODE='" + code + "'";
                    list.Add(sqltext);
                }
            }

            if (list.Count > 0)
            {
                DBCallCommon.ExecuteTrans(list);
                this.ReGetBoundData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！！！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要删除的行！！！');", true);
            }
        }

        private string GetWhere()
        {
            string ID = Session["UserID"].ToString();
            string spzt = rblSPZT.SelectedValue.ToString();
            //string xiatui = rblXiaTui.SelectedValue.ToString();
            string strWhere =" 1=1 ";
            //if (xiatui != "0")
            //{
            if (spzt != "")
            {
                if (rblSPZT.SelectedItem.ToString() == "审批中")
                {
                    strWhere = " (STATUS='2' or STATUS='4') ";
                }
                else if (rblSPZT.SelectedItem.ToString() == "已驳回")
                {
                    strWhere = " (STATUS='3' or STATUS='5')  ";
                }
                else if (rblSPZT.SelectedValue == "7")
                {
                    strWhere = " ((STATUS='1' and CARRVWAID='" + Session["UserId"].ToString() + "') or ( STATUS='2' and  CARRVWBID='" + Session["UserId"].ToString() + "'))";
                }
                else
                {
                    strWhere = " STATUS='" + spzt + "'";
                }

            }
      
            return strWhere;
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
            pager.TableName = "(select DISTINCT(a.CODE) AS CODE,DEPARTMENT,AGENT,AGENTID,ADDTIME,STATUS,REASON,CARRVWAID,CARRVWBID,a.NOTE,b.INCODE from View_TBOM_GDZCAPPLY as a left join TBOM_GDZCIN as b on a.code=b.code where PCTYPE='1')a ";
            pager.PrimaryKey = "CODE";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
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
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label applyer = (Label)gr.FindControl("lblAgentid");
                Label state = (Label)gr.FindControl("lblState");
                Label code = (Label)gr.FindControl("lblCode");
                Label yiji = (Label)gr.FindControl("yiji");
                Label erji = (Label)gr.FindControl("erji");
                if (applyer.Text.Trim() == Session["UserID"].ToString())
                {

                    if (state.Text.Trim() == "未提交")
                    {
                        gr.FindControl("CHK").Visible = true;
                        gr.FindControl("hplmod").Visible = true;
                    }
                    if (state.Text.Trim() == "已驳回")
                    {
                        gr.FindControl("hplmod").Visible = true;
                    }
                }
                if (yiji.Text.Trim() == Session["UserID"].ToString())
                {

                    string sqltext = "select STATUS from TBOM_GDZCRVW where CODE='" + code.Text + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                    if (dr.Read())
                    {
                        if (dr["STATUS"].ToString() == "1")
                        {
                            gr.FindControl("hplreview").Visible = true;
                        }
                    }
                    dr.Close();
                }
                if (erji.Text.Trim() == Session["UserID"].ToString())
                {
                    string sqltext = "select STATUS from TBOM_GDZCRVW where CODE='" + code.Text + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                    if (dr.Read())
                    {
                        if (dr["STATUS"].ToString() == "2")
                        {
                            gr.FindControl("hplreview").Visible = true;
                        }
                    }
                    dr.Close();
                }
                //string sqltext = "select CARRVWA,CARRVWB from TBOM_GDZCRVW where CODE='" + code.Text + "'";
                //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                //if (dr.Read())
                //{
                //    carrvwa = dr["CARRVWA"].ToString();
                //    carrvwb = dr["CARRVWB"].ToString();
                //}
                //dr.Close();
                //if (state.Text.Trim() == "未提交" || state.Text.Trim() == "已通过"|| state.Text.Trim() == "已驳回")
                //{
                //    gr.FindControl("hplreview").Visible = false;
                //}
                //else
                //{
                //    if (Session["UserName"].ToString() == carrvwa | Session["UserName"].ToString() == carrvwb)
                //    {
                //        gr.FindControl("hplreview").Visible = true;
                //    }
                //    else
                //    {
                //        gr.FindControl("hplreview").Visible = false;
                //    }
                //}

            }
        }
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string inCode = ((HtmlInputHidden)e.Row.FindControl("hidInCode")).Value;
                if (inCode != "")
                {
                    e.Row.Cells[0].BackColor = Color.Green;
                }

            }
        }
    }
}
