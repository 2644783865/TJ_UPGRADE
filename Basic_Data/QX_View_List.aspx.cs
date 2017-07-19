using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class QX_View_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindData();

                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();

            }
            InitVar();
        }


        //绑定基本信息
        private void BindData()
        {
            string sql = "select DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE like'[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
            ListItem list = new ListItem("全部", "00");
            ddl_Depart.Items.Insert(0,list);
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
            pager_org.TableName = "View_TBDS_STAFFINFO";
            pager_org.PrimaryKey = "ST_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "ST_ID";
            pager_org.StrWhere = where;
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlwhere = "1=1 and ST_PD in('0','2','4')";


            if (txtname.Text != "")
            {
                sqlwhere += " and (ST_NAME like '%" + txtname.Text.ToString().Trim() + "%' or ST_WORKNO like '%" + txtname.Text.ToString().Trim() + "%') ";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sqlwhere += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
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



        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }


    }
}
