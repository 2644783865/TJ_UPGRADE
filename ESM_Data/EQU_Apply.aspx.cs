using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ZCZJ_DPF.ESM
{
    public partial class EQU_Apply : System.Web.UI.Page
    {
        string equrvwa = "";
        string equrvwb = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
                foreach (GridViewRow gr in gridview1.Rows)
                {
                    Label applyer = (Label)gr.FindControl("lblApplyer");
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
                    Label spzt = (Label)gr.FindControl("spzt");
                    Label code = (Label)gr.FindControl("lblCode");
                    string sqltext = "select EQURVWA,EQURVWB from ViewEQU_Apply where Code='"+code.Text+"'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                    if (dr.Read())
                    {
                        equrvwa = dr["EQURVWA"].ToString();
                        equrvwb = dr["EQURVWB"].ToString();
                    }
                    dr.Close();
                    if (spzt.Text.Trim() == "已审批" | spzt.Text.Trim() == "已驳回")
                    {
                        gr.FindControl("hplreview").Visible = false;
                    }
                    else
                    {
                        if (Session["UserName"].ToString() == equrvwa | Session["UserName"].ToString() == equrvwb)
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
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete  from EQU_Apply where Code='" + id + "'";
                sqltext += ";delete  from EQU_ApplyRVW where Code='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.GetBoundData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
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
            pager.TableName = "EQU_Apply";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "Id*1 as Id,Code,Dep,Name,Type,Num,Applyer,SPZT";
            pager.OrderField = "";
            pager.StrWhere = "";
            pager.OrderType = 0;
            pager.PageSize = 5;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, gridview1, UCPaging1, NoDataPanel);
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
    }
}
