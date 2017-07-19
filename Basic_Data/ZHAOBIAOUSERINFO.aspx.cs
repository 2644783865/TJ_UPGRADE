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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class ZHAOBIAOUSERINFO : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();
            }
        }

        #region "数据分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "PC_USERINFO as a left join TBCS_CUSUPINFO as b on a.supplyid=b.CS_CODE";
            pager.PrimaryKey = "CS_CODE";
            pager.ShowFields = "CS_CODE,CS_NAME,CS_HRCODE,supplyusername,supplypassword";
            pager.OrderField = "CS_CODE";
            pager.StrWhere = Strwhere();
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 50;
        }
        void Pager_PageChanged(int pageNumber)
        {
            GetQueryData();
        }

        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rptTBCS_CUSUPINFO, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
                Panel1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                Panel1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        private string Strwhere()
        {
            string strwhere = "1=1";
            if (tb_csname.Text.ToString().Trim()!="")
            {
                strwhere += " and (CS_NAME like '%" + tb_csname.Text.ToString().Trim() + "%' or CS_HRCODE like '%" + tb_csname.Text.ToString().Trim() + "%')";
            }
            return strwhere;
        }

        private void GetQueryData()
        {
            InitPager();
            bindGrid();
        }
        #endregion


        protected void btn_select_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetQueryData();
        }

        //删除
        protected void deleteuser_Click(object sender, CommandEventArgs e)
        {
            string cscode = e.CommandName;
            string sql = "delete from PC_USERINFO where supplyid='" + cscode + "'";
            try
            {
                DBCallCommon.ExeSqlText(sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('删除成功！！！');", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('操作出错！！！');", true);
                return;
            }
            UCPaging1.CurrentPage = 1;
            GetQueryData();
        }
    }
}
