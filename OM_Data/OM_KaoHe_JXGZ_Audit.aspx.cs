using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHe_JXGZ_Audit : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {


                databind();
                ControlVisible();

            }
        }

        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "TBDS_KaoHe_JXList as a left join TBDS_DEPINFO as b on a.DepId=b.DEP_CODE";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "ZDTIME";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rep_Kaohe, UCPaging1, NoDataPanel);
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




        private string strWhere()
        {
            string strWhere = "1=1 ";
            if (rblState.SelectedValue == "")
            {
                strWhere += " and SPRID='" + Session["UserId"].ToString() + "'";
            }

            else   if (rblState.SelectedValue == "1")
            {
                strWhere += " and State ='1' and SPRID='" + Session["UserId"].ToString() + "'";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and State ='2' and SPRID='" + Session["UserId"].ToString() + "'";
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and State ='3' and SPRID='" + Session["UserId"].ToString() + "'";
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and State ='1' and SPRID='" + Session["UserId"].ToString() + "'";
            }
            return strWhere;
        }

        private void ControlVisible()
        {
            foreach (RepeaterItem item in rep_Kaohe.Items)
            {

                HyperLink hlkAudit = item.FindControl("HyperLink3") as HyperLink;
                if (rblState.SelectedValue == "4")
                {
                    hlkAudit.Visible = true;
                }
                else
                {
                    hlkAudit.Visible = false;
                }

            }
        }

        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }

        #endregion
    }
}
