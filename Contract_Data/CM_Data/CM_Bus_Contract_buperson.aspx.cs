﻿using System;
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

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Bus_Contract_buperson : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                InitInfo();
            }

        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "TBDS_STAFFINFO";
            pager.PrimaryKey = "ST_CODE";
            pager.ShowFields = "";
            pager.OrderField = "ST_CODE";
            pager.StrWhere = "ST_CODE LIKE '0302%'";
            pager.OrderType = 0;//按人员编号升序排列
            pager.PageSize = 10;
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            GetTechPersonsData();
        }
        void Pager_PageChanged(int pageNumber)
        {
            GetTechPersonsData();
        }
        protected void GetTechPersonsData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
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
            protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string TcName_id = "";
            string labTcName = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                TcName_id = ((Label)gr.FindControl("lblID")).Text.Trim();
                if (chk.Checked)
                {
                    string strsql = "select ST_NAME from TBDS_STAFFINFO where ST_CODE='" + TcName_id + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(strsql);
                    while (dr.Read())
                    {
                        labTcName = dr[0].ToString();
                    }
                    dr.Close();
                }
            }
            
            Response.Write("<script>javascript:window.returnValue ='" + labTcName + "';window.close();</script>");
        }
    }
}