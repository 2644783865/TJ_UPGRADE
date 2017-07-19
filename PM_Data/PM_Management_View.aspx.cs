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
    public partial class PM_Management_View : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetManutAssignData(); //数据绑定
            }
            //GridView1_DataBound();
            CheckUser(ControlFinder); 
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
            pager.ShowFields = "*,'" + rblstatus.SelectedItem.Value + "' as state";
            pager.OrderField = "MTA_ID";
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
            pager.StrWhere = "MTA_STATUS='" + rblstatus.SelectedValue + "' ";
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
            pager.StrWhere = CreateConStr();
            GetManutAssignData();
        }
        private string CreateConStr()
        {
            string strWhere = "";
            if (txtSearch.Text.Trim().ToString()!="")
            {
                //strWhere = " MTA_ENGNAME like '%" + txtSearch.Text.Trim() + "%' ";
                strWhere = "MTA_STATUS='" + rblstatus.SelectedValue + "'";
                switch (ddl_query.SelectedValue)
                {
                    case "1": strWhere += " and MTA_ID like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "2": strWhere += " and MTA_ENGNAME like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "3": strWhere += " and TSA_TCCLERKNM like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "4": strWhere += " and QSA_QCCLERKNM like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "5": strWhere += " and MTA_DUY like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "6": strWhere += " and MTA_BANZU like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "7": strWhere += " and MTA_PJID like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                    case "8": strWhere += " and CM_PROJ like '%" + txtSearch.Text.Trim() + "%' ";
                        break;
                }
            }
            else
            {
                strWhere = " MTA_STATUS='" + rblstatus.SelectedValue + "'";
            }
            return strWhere;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lblgengji = (Label)gr.FindControl("lbldengji");
                string lblrk = ((Label)gr.FindControl("lb_rk")).Text.ToString();
                if (rblstatus.SelectedValue == "0")
                {
                    lblgengji.Text = "分工";
                }
                else if (rblstatus.SelectedValue == "1")
                {
                    lblgengji.Text = "修改";
                }
                else
                {
                    lblgengji.Text = "处理";
                }
                if (lblrk == "已入库")
                {
                    gr.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
        protected void btnDealWith_Click(object sender, EventArgs e)
        {
            string sqltext;
            List<string> list_sql = new List<string>();
            int n = 0;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gr.FindControl("chtask");
                Label lblID = (Label)gr.FindControl("lblID");
                if (cb.Checked)
                {
                    n++;
                    sqltext = "update TBMP_MANUTSASSGN set MTA_STATUS='2' where MTA_ID='" + lblID.Text + "'";
                    list_sql.Add(sqltext);
                }
            }
            if (n == 0)
            {
                Response.Write("<script>alert('请勾选项目工程！');</script>");
            }
            else
            {
                DBCallCommon.ExecuteTrans(list_sql);
            }
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
    }
}
