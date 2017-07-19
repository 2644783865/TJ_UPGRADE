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
    public partial class OM_LDBLJL : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            this.InitVar();
        }
        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
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
            pager_org.TableName = "OM_LDBXBLJL";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "ID,(BLLD_YLBXD*100) as BLLD_YLBXD,(BLLD_SYBXD*100) as BLLD_SYBXD,(BLLD_GSBXD*100) as BLLD_GSBXD,(BLLD_SYD*100) as BLLD_SYD,(BLLD_YLD*100) as BLLD_YLD,(BLLD_GJJD*100) as BLLD_GJJD,(BLLD_YLGR*100) as BLLD_YLGR,(BLLD_SYGR*100) as BLLD_SYGR,(BLLD_JBYLGR*100) as BLLD_JBYLGR,(BLLD_GJJGR*100) as BLLD_GJJGR,BLLD_XGSJ,BLLD_XGR";
            pager_org.OrderField = "BLLD_XGSJ";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 15;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (txtStartTime.Text != "")
            {
                sql += " and substring(BLLD_XGSJ,1,10)>='" + txtStartTime.Text.ToString().Trim() + "'";
            }
            if (txtEndTime.Text != "")
            {
                sql += " and substring(BLLD_XGSJ,1,10)<='" + txtEndTime.Text.ToString().Trim() + "'";
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
            CommonFun.Paging(dt, rptGDGZrecord, UCPaging1, palNoData);
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
        #endregion
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
    }
}
