using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_InspectDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                UCPaging1.CurrentPage = 1;

                bindGrid();

            }
            InitVar();

        }




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
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string where)
        {
            pager_org.TableName = "TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID left join dbo.TBMA_MATERIAL as c on b.Marid=c.ID";
            pager_org.PrimaryKey = "AFI_ID";
            pager_org.ShowFields = "AFI_ID,PTC,AFI_TSDEP,AFI_MANNM,AFI_RQSTCDATE,AFI_ENDDATE,AFI_QCMANNM,PARTNM,Marid,GUIGE,CAIZHI,RESULT,AFI_ASSGSTATE ";
            pager_org.OrderField = "AFI_ENDDATE";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 30;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindGrid();
        }


        private void bindGrid()
        {
            string sqlwhere = "1=1";

            if (txtCode.Text.Trim() != "")
            {
                sqlwhere += " and AFI_ID like '%" + txtCode.Text.Trim() + "%'";
            }

            if (txtPTC.Text.Trim() != "")
            {
                sqlwhere += " and PTC like '%" + txtPTC.Text.Trim() + "%'";
            }
            if (txtDep.Text.Trim() != "")
            {
                sqlwhere += " and AFI_TSDEP like '%" + txtDep.Text.Trim() + "%'";
            }
            if (txtBJR.Text.Trim() != "")
            {
                sqlwhere += " and AFI_MANNM like '%" + txtBJR.Text.Trim() + "%'";
            }
            if (txtStart.Text.Trim() != "")
            {
                sqlwhere += " and AFI_RQSTCDATE > '" + txtStart.Text.Trim() + "'";
            }
            if (txtEnd.Text.Trim() != "")
            {
                sqlwhere += " and AFI_RQSTCDATE < '" + txtEnd.Text.Trim() + "'";
            }
            if (txtZJR.Text.Trim() != "")
            {
                sqlwhere += " and AFI_QCMANNM like '%" + txtZJR.Text.Trim() + "%'";
            }
            if (txtCS.Text.Trim() != "")
            {
                sqlwhere += " and AFI_ASSGSTATE like '%" + txtCS.Text.Trim() + "%'";
            }
            if (ddlResult.SelectedValue != "")
            {
                sqlwhere += " and AFI_ENDRESLUT = '" + ddlResult.SelectedValue + "'";
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
    }
}