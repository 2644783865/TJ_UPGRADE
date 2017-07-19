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
    public partial class OM_Sta_Edit_Record : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();

            }
            InitVar();
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
            pager_org.TableName = "TBDS_STAFFINFO_EditRecord";
            pager_org.PrimaryKey = "Id";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "Id";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//降序排列
            pager_org.PageSize = 20;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlwhere = "1=1";
            if (txtBEditPer.Text != "")
            {
                sqlwhere += " and bSTNAME like '%" + txtBEditPer.Text.Trim() + "%'";
            }
            if (txtEdiePer.Text != "")
            {
                sqlwhere += " and EditPerName like '%" + txtEdiePer.Text.Trim() + "%'";
            }
            if (txtStart.Text != "")
            {
                sqlwhere += " and EditTime > '" + txtStart.Text.Trim() + "'";
            }
            if (txtEnd.Text != "")
            {
                sqlwhere += " and EditTime < '" + txtEnd.Text.Trim() + "'";
            }
            if (ddlCAOZUOType.SelectedValue != "00")
            {
                sqlwhere += " and Caozuo like '%" + ddlCAOZUOType.SelectedValue + "%'";
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            bindGrid();
        }


        #endregion
    }
}
