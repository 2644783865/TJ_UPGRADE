using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarPersons : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetDepartment();
                ReGetBoundData();
            }
        }

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct ST_DEPID,DEP_NAME from View_OMstaff where ST_DEPID LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlSearch.DataSource = dt;
            ddlSearch.DataTextField = "DEP_NAME";
            ddlSearch.DataValueField = "ST_DEPID";
            ddlSearch.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部信息";
            item.Value = "00";
            ddlSearch.Items.Insert(0, item);
            ddlSearch.SelectedValue = "00";
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }

        private string GetWhere()
        {
            string strWhere = string.Empty;
            if (ddlSearch.SelectedValue != "00")
            {
                strWhere = " ST_DEPID like '" + ddlSearch.SelectedValue.Trim() + "%' AND ST_PD='0'";
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
            pager.TableName = "View_OMstaff";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "ST_ID*1 as ST_ID,ST_NAME,ST_GENDER,ST_POSITION";
            pager.OrderField = "ST_ID";
            pager.StrWhere = GetWhere();
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
                    labTcName = ((Label)gr.FindControl("lblName")).Text.Trim();
                    code = ((Label)gr.FindControl("code")).Text.Trim();
                    NameCode = labTcName + " " + code;
                }
            }
            Response.Write("<script>javascript:window.returnValue ='" + NameCode + "';window.close();</script>");
        }
    }
}
