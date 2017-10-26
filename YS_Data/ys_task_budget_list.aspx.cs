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
using ZCZJ_DPF;
using System.Data.SqlClient;

namespace ZCZJ_DPF.YS_Data.UI
{
    public partial class ys_task_budget_list : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        YS_Data.BLL.ys_task_budget_list bll = new YS_Data.BLL.ys_task_budget_list();

        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckUser(ControlFinder);
            initPager();
            initUCPaging();

            if (!IsPostBack)
            {
                initDdl();
                UCPaging_PageChanged(1);
            }

        }

        private void initPager()
        {
            bll.initPager(pager, txt_task_code.Text.Trim(), txt_contract_code.Text.Trim(), txt_project_name.Text.Trim(),ddl_state.SelectedValue);
        }

        private void initUCPaging()
        {
            UCPaging.PageChanged += new UCPaging.PageHandler(UCPaging_PageChanged);
            UCPaging.PageSize = pager.PageSize;
        }

        private void UCPaging_PageChanged(int i)
        {
            switch (i)
            {
                case 1: pager.PageIndex = 1; break;
                default: pager.PageIndex = UCPaging.CurrentPage; break;
            }
            CommonFun.Paging(rpt_task_list, CommonFun.GetDataByPagerQueryParam(pager), UCPaging, pal_container, NoDataPanel);
        }

        private void initDdl()
        {
            string sqltext = "SELECT DISTINCT state AS DDLVALUE,case when state='1' then '初步预算'when state='2' then '部门反馈'when state='3' then '财务调整'when state='4' then '预算审核' when state='5' then '编制完成' end  AS DDLTEXT FROM ys_task_budget WHERE state IS NOT NULL ORDER BY state";
            DBCallCommon.BindDdl(ddl_state, sqltext, "DDLTEXT", "DDLVALUE");
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging_PageChanged(1);
        }

        public string getTaskState(string type)
        {
            return bll.getTaskState(type);
        }

        protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging_PageChanged(1);
        }

    }
}
