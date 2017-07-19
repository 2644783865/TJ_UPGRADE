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

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_External_Audit : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);

            if (!IsPostBack)
            {

                bindGrid();
            }
        }
        private void bindGrid()
        {
            pager.TableName = "TBQC_INTERNAL_AUDIT";
            pager.PrimaryKey = "PRO_ID";
            pager.ShowFields = "*";
            pager.OrderField = "PRO_ID";
            pager.StrWhere = "PRO_TYPE='2'";
            pager.OrderType = 0;//升序排列
            pager.PageSize = 3;
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
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
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGrid();
        }
        protected void LinkButDel_Click(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqltext = "delete from TBQC_INTERNAL_AUDIT where PRO_ID=" + lotnum;
            DBCallCommon.ExeSqlText(sqltext);
            bindGrid();

        }
    }
}
