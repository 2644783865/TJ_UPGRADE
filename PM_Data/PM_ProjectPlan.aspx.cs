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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_ProjectPlan : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetManutAssignData(); //数据绑定

            }
            CheckUser(ControlFinder);
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {

            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lblplan = (Label)gr.FindControl("lblplan");
                if (rblproplan.SelectedValue == "0")
                {
                    lblplan.Text = "制定";
                }
                else if (rblproplan.SelectedValue == "1")
                {
                    lblplan.Text = "查看";
                }
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetManutAssignData();
        }
        //初始化分页信息

        private void InitPager()
        {
            string sqltxt = "MS_PLAN='" + rblproplan.SelectedValue.ToString() + "'";
            pager.TableName = "View_TM_MSFORALLRVW";
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = "MS_ID,MS_ENGID,MS_PJID,MS_ENGNAME,MS_PLAN='" + rblproplan.SelectedValue + "'";
            pager.OrderField = "MS_ID";
            pager.StrWhere = sqltxt;
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
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
        //点击查询时重新邦定GridView，添加查询条件
        private void ReGetManutAssignData()
        {
            InitPager();
            GetManutAssignData();
        }
        protected void rblproplan_SelectedIndexChanged(object sender, EventArgs e)
        {   
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void ddlpjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void ddlengname_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
       
    }
}
