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
    public partial class OM_GdzcOut : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
            }
        }
        private string GetWhere()
        {
            string strWhere = string.Empty;
            if (txtName.Text.Trim() == "" && txtModel.Text.Trim() == "")
            {
                strWhere = " 0=0";
            }
            else if (txtName.Text.Trim() != "" && txtModel.Text.Trim() == "")
            {
                strWhere = " OUTNAME like '%" + txtName.Text.Trim() + "%'";
            }
            else if (txtName.Text.Trim() == "" && txtModel.Text.Trim() != "")
            {
                strWhere = " OUTMODEL like '%" + txtModel.Text.Trim() + "%'";
            }
            else
            {
                strWhere = " OUTNAME like '%" + txtName.Text.Trim() + "%' and OUTMODEL like '%" + txtModel.Text.Trim() + "%'";
            }
            return strWhere;
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtModel.Text = "";
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
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
            pager.TableName = "TBOM_GDZCOUT";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID*1 as ID,OUTCODE,OUTDEP,OUTNAME,OUTMODEL,OUTNUM,OUTSENDER,OUTDATE,OUTDOC,OUTNOTE";
            pager.OrderField = "";
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
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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
