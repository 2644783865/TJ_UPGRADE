using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_TargetAnalyze_List : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
            UCPaging.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
        }
        private void InitPage()
        {

            UCPaging.CurrentPage = 1;
            this.InitPager();
            bindGrid();
        }
        private void InitPager()
        {
            //TARGET_ID, TARGET_NAME, TARGET_NOTE
            pager.TableName = "TBQC_TARGET_LIST";
            pager.PrimaryKey = "TARGET_ID";
            pager.ShowFields = "*";
            pager.OrderField = "TARGET_ID";
            pager.StrWhere = "";
            pager.OrderType = 1;//升序排列
            pager.PageSize =20;
            UCPaging.PageSize = pager.PageSize;    //每页显示的记录数
        }
        private void bindGrid()
        {

            pager.PageIndex = UCPaging.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, GridView1, UCPaging, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging.Visible = false;

            }
            else
            {
                UCPaging.Visible = true;
                UCPaging.InitPageInfo();  //分页控件中要显示的控件

            }

        }
        private void Pager_PageChangedMS(int pageNumber)
        {
            this.InitPager();
            bindGrid();
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e) 
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            sqlText = "delete from TBQC_TARGET_LIST where TARGET_ID=" + lotnum;
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据异常，请联系管理员！！！');", true);
            }
            this.InitPage();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string proId = ((HtmlInputHidden)e.Row.FindControl("hidTargetId")).Value.Trim();
               e.Row.Attributes["style"] = "Cursor:hand";
               e.Row.Attributes.Add("title", "双击修改");
               e.Row.Attributes["ondblclick"] = "javascript:return openLink('" + proId + "');";
         

            }
        }
    }
}
