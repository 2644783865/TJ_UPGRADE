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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_GJBJTZ : BasicPage
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

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "View_TBPC_GJBJTZdetail";
            pager.PrimaryKey = "KC_PJID";
            pager.ShowFields = "*";
            pager.OrderField = "KC_PJID,KC_ENGID";
            pager.StrWhere = GetSiftData();
            pager.OrderType = 0;
            pager.PageSize = 100;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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

        private string GetSiftData()
        {
            string str = "";
            str = "1=1 ";
            if (tb_pjnm.Text != "")
            {
                str += "and PJ_NAME like '%" + tb_pjnm.Text.Trim() + "%'";
            }
            if (tb_engnm.Text != "")
            {
                str += "and TSA_ENGNAME like '%" + tb_engnm.Text.Trim() + "%'";
            }
            if (tb_gjnm.Text != "")
            {
                str += "and KC_GJNM like '%" + tb_gjnm.Text.Trim() + "%'";
            }
            return str;
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            ReGetBoundData();
        }

        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            tb_pjnm.Text = "";
            tb_engnm.Text = "";
            tb_gjnm.Text = "";
            ReGetBoundData();
        }

        protected void delete_Click(object sender, CommandEventArgs e)
        {
            int Pid = Convert.ToInt32(e.CommandName);
            string sqltext = "delete from TBPC_KEYCOMTZ where KC_ID='" + Pid + "'";
            DBCallCommon.ExeSqlText(sqltext);
            ReGetBoundData();
        }
    }
}
