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

namespace ZCZJ_DPF.YS_Data
{
    public partial class ys_contract_budget_list :BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        YS_Data.BLL.ys_contract_budget_list bll = new YS_Data.BLL.ys_contract_budget_list();

        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckUser(ControlFinder);
            initPager();
            initUCPaging();
            if (!IsPostBack)
            {
                UCPaging_PageChanged(1);
            }
        }


        #region 页面加载初始化代码

        /// <summary>
        /// 初始化页面查询对象
        /// </summary>
        private void initPager()
        {
            bll.initPager(pager,  txt_contract_code.Text.Trim(), txt_project_name.Text.Trim());
        }

        /// <summary>
        /// 初始化翻页控件
        /// </summary>
        private void initUCPaging()
        {
            UCPaging.PageChanged += new UCPaging.PageHandler(UCPaging_PageChanged);
            UCPaging.PageSize = pager.PageSize;
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
            CommonFun.Paging(rpt_contract_list, CommonFun.GetDataByPagerQueryParam(pager), UCPaging, pal_container, NoDataPanel);
        } 
       
        #endregion

    }
}
