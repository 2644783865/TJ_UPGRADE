using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.TM_Data
{
    public partial class test2 : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitVar();
               InitInfo();

            }
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
        }

        #region 分页
        private void InitVar()
        {
            InitPager();
            
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "View_TM_TaskAssign";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "TSA_ID,CM_PROJ,TSA_PJID,TSA_ENGNAME,TSA_BUYER,TSA_MODELCODE,TSA_DEVICECODE,TSA_DESIGNCOM,TSA_CONTYPE,TSA_RECVDATE,TSA_DRAWSTATE,TSA_DELIVERYTIME,TSA_MANCLERKNAME,TSA_STARTTIME,TSA_MSFINISHTIME,TSA_MPCOLLECTTIME,TSA_TECHTIME,TSA_TUZHUANGTIME,TSA_ZHUANGXIANGDANTIME,TSA_TCCLERKNM,TSA_STARTDATE,TSA_STATE,TSA_ADDTIME,TSA_REVIEWER,ID";
            pager.OrderField = "TSA_ID";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            GetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
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
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            this.GetBoundData();

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            InitPager();
            GetBoundData();
        }

        private string CreateConStr()
        {
            string sql = "";
            if (txtTaskId.Text!="")
            {
                sql += " TSA_ID like'%"+txtTaskId.Text.Trim()+"%'";
            }
            if (ddlPer.SelectedIndex!=0)
            {
                sql += "TSA_TCCLERK='"+ddlPer.SelectedValue+"' ";
            }
            return sql;
        
        }
    }
}
        #endregion