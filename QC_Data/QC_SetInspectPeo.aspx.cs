using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_SetInspectPeo : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitVar();

            if (!IsPostBack)
            {
                this.GetBoundData();
            }
            CheckUser(ControlFinder);
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
            pager.TableName = "View_TBQC_SetInspectPerson";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID, num, InspectPerson, Addtime, AddPerson, IsDiret, TY_ID, TY_NAME,ST_NAME,AddName";
            pager.OrderField = "ID";
            pager.StrWhere ="";
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion

    }
}
