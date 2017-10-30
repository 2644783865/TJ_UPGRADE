using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYP_PCHZ : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
            
                databind();


            }

            ControlVisible();
            CheckUser(ControlFinder);
        }


        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
            ControlVisible();
        }

        private void databind()
        {
            pager.TableName = "TBOM_BGYPPCHZ";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 30;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            if (dt.Rows.Count>0)
            {
                double jine = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dt.Rows[i]["TOTALPRICE"].ToString()))
                    {
                        jine += Convert.ToDouble(dt.Rows[i]["TOTALPRICE"].ToString().Trim());
                    }
                }
                totalprice.Text = jine.ToString();
            }
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


        private string strWhere()
        {
            string strWhere = "1=1 ";
            if (rblState.SelectedValue == "1")   //审核中
            {
                strWhere += " and State ='1'";
            }
            else if (rblState.SelectedValue == "2")   //已审核
            {
                strWhere += " and State ='2'";
            }
            else if (rblState.SelectedValue == "3")   //驳回
            {
                strWhere += " and State ='3'";
            }
            else if (rblState.SelectedValue == "4")   //我的任务
            {
                strWhere += " and ((SHRFID='" + Session["UserID"].ToString() + "' and SHRFRESULT IS NULL) or ";
                strWhere += " (SHRSID='" + Session["UserID"].ToString() + "' and SHRSRESULT IS NULL and SHRFRESULT IS NOT NULL) or ";
                strWhere += " (SHRTID='" + Session["UserID"].ToString() + "' and SHRTRESULT IS NULL and SHRFRESULT IS NOT NULL and SHRSRESULT IS NOT NULL))";
            }
            if (txt_starttime.Text != "")
            {
                strWhere += " and  DATE >='" + txt_starttime.Text + "'";
            }
            if (txt_endtime.Text != "")
            {
                strWhere += " and  DATE <='" + txt_endtime.Text + "'";
            }
            return strWhere;
        }

        #endregion


        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }

        private void ControlVisible()
        {

            if (rblState.SelectedValue == "4")
            {
                GridView1.Columns[12].Visible = true;
            }
            else
            {
                GridView1.Columns[12].Visible = false;
            }

        }

        //查询按钮
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }

    }
}
