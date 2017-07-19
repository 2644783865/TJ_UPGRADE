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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CARCK : System.Web.UI.Page
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
        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "OM_CARCK_SP";
            pager.PrimaryKey = "SP_ID";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
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
        private string GetWhere()
        {
            string strWhere = "1=1 ";
            if (rblSPZT.SelectedValue == "1")
            {
                strWhere += " and SPZT in ('0','1y')";
            }
            else if (rblSPZT.SelectedValue == "2")
            {
                strWhere += " and SPZT ='y'";
            }
            else if (rblSPZT.SelectedValue == "3")
            {
                strWhere += " and SPZT ='n'";
            }
            if (txtName.Text.Trim() != "")
            {
                strWhere += " and  SP_MC like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtModel.Text.Trim() != "")
            {
                strWhere += " and  SP_GG like '%" + txtModel.Text.Trim() + "%'";
            }

            return strWhere;
        }
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }

    }
}
