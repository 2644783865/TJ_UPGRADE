using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Date
{
    public partial class OM_SEAL : System.Web.UI.Page
    {
        string check1 = "";
        string check2= "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitVar();

            if (!IsPostBack)
            {
                GetBoundData();
            }
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument;
            string sqltext = "delete from ViewOM_SEAL where CODE='" + id + "'";
            DBCallCommon.ExeSqlText(sqltext);
            this.ReGetBoundData();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        }

        protected void tcstate_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetBoundData();
        }

        private string GetWhere()
        {
            string state = rbl_tcstate.SelectedValue.ToString();
            string strWhere = string.Empty;
            if (state != "0")
            {
                strWhere = " SM_TCSTATE='" + state + "'";
            }
            else
            {
                strWhere = " 0=0";
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
            pager.TableName = "TBOM_SEAL";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID*1 as ID,CODE,SM_DEPM,SM_MANAPL,SM_MANUSE,SM_FNAME,SM_TIME,case when SM_TYPE='0' then '行政章' when SM_TYPE='1' then '合同章' when SM_TYPE='2' then '公司领导名章' else '其他用章' end as SM_TYPE,SM_TCSTATE,SM_NOTE";
            pager.OrderField = "ID";
            pager.StrWhere = GetWhere();
            pager.OrderType = 0;
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
                Label applyer = (Label)gr.FindControl("lblManapl");
                if (applyer.Text.Trim() == Session["UserName"].ToString())
                {
                    gr.FindControl("hplmod").Visible = true;
                    gr.FindControl("lnkDelete").Visible = true;
                }
                else
                {
                    gr.FindControl("hplmod").Visible = false;
                    gr.FindControl("lnkDelete").Visible = false;
                }
                Label tcstate = (Label)gr.FindControl("tcstate");
                Label code = (Label)gr.FindControl("lblCode");
                string sqltext = "select SC_CHECK1,SC_CHECK2 from ViewOM_SEAL where CODE='" + code.Text + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (dr.Read())
                {
                    check1 = dr["SC_CHECK1"].ToString();
                    check2 = dr["SC_CHECK2"].ToString();
                }
                dr.Close();
                if (tcstate.Text.Trim() == "已审批" | tcstate.Text.Trim() == "已驳回")
                {
                    gr.FindControl("hplreview").Visible = false;
                }
                else
                {
                    if (Session["UserName"].ToString() == check1 | Session["UserName"].ToString() == check2)
                    {
                        gr.FindControl("hplreview").Visible = true;
                    }
                    else
                    {
                        gr.FindControl("hplreview").Visible = false;
                    }
                }

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
