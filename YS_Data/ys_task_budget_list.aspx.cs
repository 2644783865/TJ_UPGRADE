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

namespace ZCZJ_DPF.YS_Data.UI
{
    public partial class ys_task_budget_list : BasicPage
    {
        
        PagerQueryParam pager = new PagerQueryParam();
        YS_Data.BLL.ys_task_budget_list bll = new YS_Data.BLL.ys_task_budget_list();
        string contract_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            contract_code = Request.QueryString["contract_code"];
            //CheckUser(ControlFinder);
            
            initPager();
            initUCPaging();

            if (!IsPostBack)
            {
                initControl();                
                UCPaging_PageChanged(1);
            }
        }

       

        #region 页面加载初始化代码
        /// <summary>
        /// 初始化页面查询对象
        /// </summary>
        private void initPager()
        {
            if (string.IsNullOrEmpty(contract_code))
            {
                bll.initPager(pager, txt_task_code.Text.Trim(), txt_contract_code.Text.Trim(), txt_project_name.Text.Trim(), ddl_state.SelectedValue);                
            }
            else
            {
                bll.initPager(pager,null, contract_code, null, null);
            }
        }
        /// <summary>
        /// 初始化翻页控件
        /// </summary>
        private void initUCPaging()
        {
            UCPaging.PageChanged += new UCPaging.PageHandler(UCPaging_PageChanged);
            UCPaging.PageSize = pager.PageSize;
        }


        private void initControl()
        {
            if (string.IsNullOrEmpty(contract_code))
            {
                initDdl();
                pal_contract.Visible = false;
            }
            else
            {
                lb_contract_code.Text = contract_code;
                pal_search.Visible = false;
            }
        }
        /// <summary>
        /// 初始化状态下拉框
        /// </summary>
        private void initDdl()
        {
            string sqltext = "SELECT DISTINCT state AS DDLVALUE,case when state='1' then '初步预算'when state='2' then '部门反馈'when state='3' then '财务调整'when state='4' then '预算审核' when state='5' then '编制完成' end  AS DDLTEXT FROM ys_task_budget ORDER BY state";
            DBCallCommon.BindDdl(ddl_state, sqltext, "DDLTEXT", "DDLVALUE");
        }
        #endregion

        


        #region 操作事件处理程序

        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging_PageChanged(1);
        }      

        /// <summary>
        /// 翻页事件处理程序
        /// </summary>
        /// <param name="i">判读是否用代码触发，如果i=1,则是用代码触发</param>
        private void UCPaging_PageChanged(int i)
        {
            switch (i)
            {
                case 1: pager.PageIndex = 1; break;
                default: pager.PageIndex = UCPaging.CurrentPage; break;
            }
            CommonFun.Paging(rpt_task_list, CommonFun.GetDataByPagerQueryParam(pager), UCPaging, pal_container, NoDataPanel);
        }


        protected void rpt_task_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //if ((e.Item.ItemIndex + 1) % 2 == 0)

                if (((HtmlTableCell)e.Item.FindControl("td_state")).InnerText.Equals("初步预算"))
                {
                    ((HtmlTableCell)e.Item.FindControl("td_state")).BgColor = "#f00";
                }
                
               
            }

        }

        #endregion


    }
}
