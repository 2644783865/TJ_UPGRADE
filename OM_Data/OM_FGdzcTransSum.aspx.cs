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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_FGdzcTransSum : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
            }
            //  CheckUser(ControlFinder);
        }
        private string GetWhere()
        {

            string strWhere = " TRANSFTYPE='1' and SPFATHERID is not null ";
            if (rblSPZT.SelectedValue == "1")
            {
                strWhere += " and SPZT in ('0','1y','2y')";
            }
            else if (rblSPZT.SelectedValue == "2")
            {
                strWhere += " and SPZT='y'";
            }
            else if (rblSPZT.SelectedValue == "3")
            {
                strWhere += " and SPZT='n'";
            }
            if (txtName.Text.Trim() != "")
            {
                strWhere += " and  NAME like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtModel.Text.Trim() != "")
            {
                strWhere += " and   MODEL like '%" + txtModel.Text.Trim() + "%'";
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
            pager.TableName = "TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "*";
            pager.OrderField = "SPFATHERID";
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
