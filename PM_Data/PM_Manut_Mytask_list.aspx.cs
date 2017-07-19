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
using System.Collections.Generic;
namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Manut_Mytast_list : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetManutAssignData(); //数据绑定
            }
            if (rbl_look.SelectedValue == "1")
            {
                GridView1.Columns[13].Visible = false;
            }
            else { GridView1.Columns[13].Visible = true; }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetManutAssignData();
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "View_TBMP_TASKASSIGN";
            pager.PrimaryKey = "MTA_ID";
            pager.ShowFields = "*";
            pager.OrderField = "MTA_ID";
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
            pager.StrWhere = GetMyTask();
        }

        private string GetMyTask()
        {
            string sql = "";
            sql += " MTA_DUY  like '%" + Session["UserName"] + "%'and MTA_YNLOOK='" + rbl_look.SelectedValue.ToString() + "' ";
            if (rbl_SFRK.SelectedValue == "0")
            {
                sql += "and (MTA_YNRK <>'已入库' or MTA_YNRK is null)";
            }
            if (rbl_SFRK.SelectedValue == "1")
            {
                sql += " and MTA_YNRK='已入库'";
            }
            if (txtSearch.Text.Trim()!= "")
            {
                sql += " and MTA_STATUS='1'  ";
                switch (ddl_query.SelectedValue)
                {
                    case "1": sql += " and MTA_ID like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "2": sql += " and MTA_ENGNAME like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "3": sql += " and TSA_TCCLERKNM like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "4": sql += " and QSA_QCCLERKNM like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "5": sql += " and MTA_DUY like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "6": sql += " and MTA_BANZU like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "7": sql += " and MTA_PJID like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "8": sql += " and CM_PROJ like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                }
            }
            else
            {
                sql += "and MTA_STATUS=1";
            }
            return sql;
        }
        protected void GetManutAssignData()
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
        //点击查询时重新邦定GridView，添加查询条件
        private void ReGetManutAssignData()
        {
            InitPager();
            GetManutAssignData();
        } 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void btn_check_OnClick(object sender, EventArgs e)
        { 
            string mtaid = ((Button)sender).CommandArgument;
            string sqltext ="update TBMP_MANUTSASSGN set MTA_YNLOOK='1' where MTA_ID='" + mtaid + "'";
            DBCallCommon.ExeSqlText(sqltext);

        UCPaging1.CurrentPage = 1;
        ReGetManutAssignData();
        }
        protected void rbl_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                string lblrk = ((Label)gr.FindControl("lblynrk")).Text.ToString();
                if (lblrk == "已入库")
                {
                    gr.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}
