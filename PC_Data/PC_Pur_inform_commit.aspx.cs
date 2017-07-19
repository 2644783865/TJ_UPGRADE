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
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Pur_inform_commit : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rblZT_OnSelectedIndexChanged(null, null);
            }
            this.InitVar();

        }

        private void ContralEnable()
        {

                for (int i = 0; i <= pc_purinform_repeater.Items.Count - 1; i++)
                {
                    HyperLink HyperLink1 = (HyperLink)pc_purinform_repeater.Items[i].FindControl("HyperLink1");

                        if (rblZT.SelectedValue == "2")
                        {
                            HyperLink1.Visible = true;
                        }
                        else
                        {
                             HyperLink1.Visible = false;
                        }
                }
            //}
        }
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "PC_purinformcommitall";
            pager.PrimaryKey = "PC_PFT_ID";
            pager.ShowFields = "PC_PFT_ID ,PC_PFT_ZDR_NAME,PC_PFT_TIME";
            pager.OrderField = "PC_PFT_ID";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;
            pager.PageSize = Convert.ToInt16(ddl_pageno_change.SelectedValue);
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, pc_purinform_repeater, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件                 
            }
            //CheckUser(ControlFinder);
        }


       //不同状态：全部、待评审
        protected void rblZT_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblZT.SelectedValue)
            {
                case "0":  //全部
                    ViewState["sqlText"] = " PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and PC_PFT_ZDR_NAME like '%" + txtpc_infor_zdr.Text + "%'";
                    break;
                //case "1":  //最近一个月审批过的
                //    ViewState["sqlText"] = " PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and ";
                //    break;
                case "2":  //待审批 上一级没审批完的，下级审批将看不到该记录
                    ViewState["sqlText"] = " ( PC_PFT_STATE='0'  and PC_PFT_SPRA_ID='" + Session["UserID"].ToString() + "' and PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and PC_PFT_ZDR_NAME like '%" + txtpc_infor_zdr.Text + "%')" +
                   " or ( PC_PFT_STATE='1' and PC_PFT_SPRB_ID='" + Session["UserID"].ToString() + "' and PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and PC_PFT_ZDR_NAME like '%" + txtpc_infor_zdr.Text + "%' )" +
                    " or ( PC_PFT_STATE='2' and PC_PFT_SPRC_ID='" + Session["UserID"].ToString() + "' and PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and PC_PFT_ZDR_NAME like '%" + txtpc_infor_zdr.Text + "%' )";
                    break;
                case "3":
                    ViewState["sqlText"] = "  PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and PC_PFT_ZDR_NAME like '%" + txtpc_infor_zdr.Text + "%' and  PC_PFT_STATE not in('4','5') ";

                    break;
                case "4":

                    ViewState["sqlText"] = "   PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and PC_PFT_ZDR_NAME like '%" + txtpc_infor_zdr.Text + "%' and  PC_PFT_STATE='5'  ";
                    break;
                case "5":
                    ViewState["sqlText"] = "   PC_PFT_ID like '%" + txtpc_infor_id.Text + "%' and PC_PFT_ZDR_NAME like '%" + txtpc_infor_zdr.Text + "%' and  PC_PFT_STATE='4' ";
                    break;

            }
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
            ContralEnable();

        }

        //审批可见性
        //protected void pc_purinform_control_vs(object sender, RepeaterItemEventArgs e)
        //{

        //    HyperLink HyperLink1 = e.Item.FindControl("HyperLink1") as HyperLink;

        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.Header)
        //    {
        //       // HtmlTableCell tdReview = pc_purinform_repeater.FindControl("tdReview") as HtmlTableCell;
        //        string sqltext_bound = "select * from PC_purinformcommitall where (PC_PFT_SPJB='1' and PC_PFT_STATE not in('4','5') and PC_PFT_SPRA_ID='" + Session["UserID"].ToString() + "')"+
        //       " or (PC_PFT_SPJB='2' and PC_PFT_STATE not in('1','4','5') and PC_PFT_SPRA_ID='" + Session["UserID"].ToString() + "' )"+
        //        " or (PC_PFT_SPJB='3' and PC_PFT_STATE not in('1','2','4','5') and PC_PFT_SPRA_ID='" + Session["UserID"].ToString() + "' )";
        //        DataTable dt_bound = DBCallCommon.GetDTUsingSqlText(sqltext_bound);
        //        if (dt_bound.Rows.Count > 0)
        //        {
        //            if (rblZT.SelectedIndex == 2)
        //            {
        //                 //HyperLink1.Visible = true;
        //            }
        //            else
        //            {
        //                 // HyperLink1.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //           //  HyperLink1.Visible = false;
        //        }
        //    }
        //}
        protected void btn_search_Click(object sender, EventArgs e)
        {
            this.rblZT_OnSelectedIndexChanged(null, null);
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
           

        }

        protected string viewSp(string PC_PFT_ID)
        {

            return "javascript:void window.open('PC_Pur_inform_commit_detial.aspx?action=View&id=" + PC_PFT_ID + "')";

        }
        protected string ReviewSp(string PC_PFT_ID)
        {

            return "javascript:void window.open('PC_Pur_inform_commit_detial.aspx?action=Review&id=" + PC_PFT_ID + "')";

        }

    }
}
