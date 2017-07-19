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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_YULIAO_IN : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
            }
            CheckUser(ControlFinder);
        }

        private string GetWhere()
        {
            string strWhere = string.Empty;
            if (txtName.Text.Trim() == "")
            {
                strWhere = " 0=0";
            }
            else if (txtName.Text.Trim() != "")
            {
                strWhere = " Name like '%" + txtName.Text.Trim() + "%'";
            }
            else if (txtCaizhi.Text.Trim() != "")
            {
                strWhere = " Caizhi like '%" + txtName.Text.Trim() + "%'";
            }
            else if (txtGuige.Text.Trim() != "")
            {
                strWhere = " Guige like '%" + txtName.Text.Trim() + "%'";
            }
            return strWhere;
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
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
            pager.TableName = "TBWS_YULIAO_IN AS A LEFT JOIN  TBDS_STAFFINFO AS B ON A.RECEIVERID=B.ST_ID left join TBMA_MATERIAL as c on a.Marid=c.ID";
            pager.PrimaryKey = "A.ID";
            pager.ShowFields = "A.*,B.ST_NAME as RECEIVER,C.CAIZHI,C.GUIGE,C.MNAME as Name";
            pager.OrderField = "";
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
