﻿using System;
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
            if (rblState.SelectedValue == "1")
            {
                strWhere += " and State ='1'";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and State ='2'";
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and State ='3'";
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and State ='1' and SHRFID='" + Session["UserId"].ToString() + "'";
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
                GridView1.Columns[7].Visible = true;
            }
            else
            {
                GridView1.Columns[7].Visible = false;
            }

        }


    }
}
