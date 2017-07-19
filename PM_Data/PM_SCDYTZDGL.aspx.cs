using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_SCDYTZDGL : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindrpt();
            }
        }

        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "View_TM_MSFORALLRVW";
            pager_org.PrimaryKey = "MS_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "MS_ADATE,MS_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, rptDYTZ, UCPaging1, palNoData);
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
        private string StrWhere()
        {
            string sql = "MS_STATE='8'";
            //sql += " and MS_ID not in (select distinct TZD_PH from PM_SCDYTZ ) ";
            if (ddlSX.SelectedValue!="0"&&txtItem.Text.Trim()!="")
            {
                sql += " and "+ddlSX.SelectedValue+" like '%"+txtItem.Text.Trim()+"%'";
            }
            return sql;
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }
    }
}
