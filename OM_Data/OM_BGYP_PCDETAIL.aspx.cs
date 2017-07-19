using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYP_PCDETAIL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();

            }
         
        }


        #region
        PagerQueryParam pager_org = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager("1=1");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string where)
        {
            pager_org.TableName = "View_TBOM_BGYPPCAPPLYINFO";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "CODE";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 20;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlwhere = "1=1";

            if (txtWLCode.Text != "")
            {

                sqlwhere += " and WLBM like '%" + txtWLCode.Text + "%'";
            }
            if (txtOrder.Text != "")
            {
                sqlwhere += " and CODE like '%" + txtOrder.Text + "%'";
            }
            if (txtName.Text != "")
            {
                sqlwhere += " and WLNAME like '%" + txtName.Text + "%'";
            }
            if (txtGuige.Text != "")
            {
                sqlwhere += " and WLMODEL like '%" + txtGuige.Text + "%'";
            }
            if (ddlIsRK.SelectedValue != "")
            {
                sqlwhere += " and STATE_rk ='" + ddlIsRK.SelectedValue + "'";
            }


            InitPager(sqlwhere);

            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindGrid();
        }
    }
}
