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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_OrgDataInputAll_TargetShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
            }
            else
            {
                this.InitVar();
            }
        }

        protected void PageInit()
        {
           
            ViewState["TaskID"] = Request.QueryString["TaskID"].ToString();
            ViewState["TableName"] = Request.QueryString["TableName"].ToString();
            ViewState["XuHao"] = Request.QueryString["XuHao"].ToString();

            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        #region 分页
        PagerQueryParam pager = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = ViewState["TableName"].ToString();
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
            pager.OrderField = "dbo.f_formatstr('BM_ZONGXU','.')";
            pager.StrWhere = " BM_TASKID='" + ViewState["TaskID"].ToString() + "' AND ( BM_ZONGXU='" + ViewState["XuHao"] + "' OR BM_ZONGXU LIKE '" + ViewState["XuHao"] + ".%')";
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 100;
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
    }
}
