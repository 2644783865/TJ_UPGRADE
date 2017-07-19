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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_CWZHIBIAO : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindGrid();
            }

            if (IsPostBack)
            {
                InitVar();
            }
            CheckUser(ControlFinder);
        }
        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "FM_CWZHIBIAO";
            pager_org.PrimaryKey = "id_cwzb";
            pager_org.ShowFields = "*,(yychengben+xsfeiyong+glfeiyong+cwfeiyong) as cbfeiyonghj,(yyshouru-yychengben-xsfeiyong-glfeiyong-cwfeiyong) as lrzonge";
            pager_org.OrderField = "cw_yearmonth";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
        }

        private string strstring()
        {
            string sqlText = "1=1";
            if (yearmonthstart.Value.Trim() != "")
            {
                sqlText += " and cw_yearmonth>='" + yearmonthstart.Value.Trim() + "'";
            }
            if (yearmonthend.Value.Trim() != "")
            {
                sqlText += " and cw_yearmonth<='" + yearmonthend.Value.Trim() + "'";
            }
            return sqlText;
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
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


        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            string id_cwzb = (sender as LinkButton).CommandArgument.ToString().Trim();
            string sqlText = "delete from FM_CWZHIBIAO where id_cwzb='" + id_cwzb + "'";
            DBCallCommon.ExeSqlText(sqlText);
            bindGrid();
        }

        protected void btnsearch_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
    }
}
