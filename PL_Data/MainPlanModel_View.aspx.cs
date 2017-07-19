using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PL_Data
{
    public partial class MainPlanModel_View : System.Web.UI.Page
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
            //MP_CODE, MP_PJID, MP_PJNAME, MP_ENGNAME, MP_NOTE, MP_STATE
            pager.TableName = "dbo.TBMP_MAINPLANMODEL left join dbo.TBDS_STAFFINFO on TBMP_MAINPLANMODEL.MODEL_EDITUSERID = TBDS_STAFFINFO.ST_ID";
            pager.PrimaryKey = "MODEL_ID";
            pager.ShowFields = "TBMP_MAINPLANMODEL.*,ST_NAME";
            pager.OrderField = "MODEL_ID";
            pager.StrWhere = "1=1";
            pager.OrderType = 0;//升序排列
            pager.PageSize = 20;
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
            List<string> sqlist = new List<string>();
            string sqlText = "";
            sqlText = "delete from dbo.TBMP_MAINPLANMODEL where MODEL_ID='"+lotnum+"'"; 
            sqlist.Add(sqlText);
            sqlText = "delete from TBMP_MAINPLANMODELDETAIL where MP_MODELID='" + lotnum + "'";
            sqlist.Add(sqlText);

            try
            {
                DBCallCommon.ExecuteTrans(sqlist);
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据异常，请联系管理员！！！');", true);
            }
            this.InitPage();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        }
    }
}
