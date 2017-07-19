using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_AllPersonSelect : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                action = Request.QueryString["Action"];
                GetDepartment();
                InitInfo();
            }
        }

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_POSITION=b.DEP_CODE ";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "ST_ID,ST_NAME,ST_GENDER,DEP_NAME";
            pager.OrderField = "ST_CODE";
            pager.StrWhere = string.Format("ST_PD=0 AND ST_DEPID='{0}'",action);
            pager.OrderType = 0;//按人员编号升序排列
            pager.PageSize = 8;
            UCPaging1.PageSize = pager.PageSize;
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            InitPager();
            GetTechPersonsData();
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetTechPersonsData();
        }
        protected void GetTechPersonsData()
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

        //点击查询时重新邦定GridView，添加查询条件
        private void ReGetTechPersonsData()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            GetTechPersonsData();
        }

        private string CreateConStr()
        {
            string str1 = ddlSearch.SelectedValue.Trim();
            string strWhere = string.Empty;
            if (txtName.Text.ToString().Trim() != "")
            {
                strWhere = "a.ST_NAME LIKE'%" + txtName.Text.ToString().Trim() + "%' and a.ST_PD=0 AND a.ST_DEPID ='" + str1 + "'";
                return strWhere;
            }
            else
            {
                strWhere = "a.ST_DEPID ='" + str1 + "' and a.ST_PD=0";
                return strWhere;
            }
        }

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlSearch.DataSource = dt;
            ddlSearch.DataTextField = "DEP_NAME";
            ddlSearch.DataValueField = "DEP_CODE";
            ddlSearch.DataBind(); 
            ddlSearch.SelectedValue = action;
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechPersonsData();
        }

        protected void btnName_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechPersonsData();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string code;
            string labTcName;
            string NameCode = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    labTcName = gr.Cells[3].Text;
                    code = ((Label)gr.FindControl("ST_ID")).Text;
                    NameCode = labTcName + " " + code; 
                }
            }
            Response.Write("<script>javascript:window.returnValue ='" + NameCode + "';window.close();</script>");
        }
    }
}
