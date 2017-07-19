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
    public partial class OM_GDGZSP : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                //{
                //    radio_all.Visible = false;
                //}
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
            }
            CheckUser(ControlFinder);
            InitVar();
        }

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数

        }


        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "OM_GDGZSP";
            pager_org.PrimaryKey = "TOL_BH";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "TOL_BH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 50;
        }


        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (drp_state.SelectedIndex != 0)
            {
                sql += " and TOL_TOLSTATE='" + drp_state.SelectedValue.ToString().Trim() + "'";
            }
            if (radio_mytask.Checked == true)
            {
                sql = "((XGRST_ID='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='0') or (TOL_SHRID1='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT1='0') or (TOL_SHRID2='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT1='1' and TOL_SHRZT2='0') or(TOL_SHRID3='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT2='1' and TOL_SHRZT3='0'))";
            }
            return sql;
        }


        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptscczsp, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        protected void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_mytask_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void drp_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
    }
}
