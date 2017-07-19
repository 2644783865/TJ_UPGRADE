using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_JHDQXSP : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        string depid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            depid = Session["UserDeptID"].ToString();
            if (!IsPostBack)
            {
                bindrpt();
            }
        }


        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }
        private void bindrpt()
        {
            pager_org.TableName = "TBCM_PLAN as a right join CM_SP as b on (a.ID=b.SPFATHERID and b.SPLX='JHDQX')";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, gvQX, UCPaging1, NoDataPanel);
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

        private string StrWhere()
        {
            string sql = " ID is not null  ";
            if (rblmytask.SelectedValue=="1")
            {
                sql += " and (ZDR='"+username+"' or SPR1='"+username+"')" ;
            }
            if (rblstatus.SelectedValue=="0")
            {
                sql += " and SPZT='0'";
            }
            else if (rblstatus.SelectedValue=="1")
            {
                sql += " and SPZT='y'";
            }
            else if (rblstatus.SelectedValue=="2")
            {
                sql += " and SPZT='n'";
            }
            if (ddlSearch.SelectedValue!="0"&&searchcontent.Text.Trim()!="")
            {
                sql += " and " + ddlSearch.SelectedValue + " like '%" + searchcontent.Text.Trim() + "%'";
            }
            return sql;
        }

        protected void gvQX_OnRowDataBound(object sender,GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                string CM_CANCEL = ((HiddenField)e.Row.FindControl("CM_CANCEL")).Value;
                string SPZT = ((HiddenField)e.Row.FindControl("SPZT")).Value;
                string SPR1 = ((HiddenField)e.Row.FindControl("SPR1")).Value;
                HyperLink hplSP = (HyperLink)e.Row.FindControl("Task_ps");
                Label lbSPZT = (Label)e.Row.FindControl("lb_status");
                hplSP.Visible = false;
                if (CM_CANCEL=="1")
                {
                     e.Row.BackColor = System.Drawing.Color.Red;
                }
                if (SPR1==username&&SPZT=="0")
                {
                    hplSP.Visible = true;
                }
                if (SPZT=="0")
                {
                    lbSPZT.Text = "未审批";
                }
                else if (SPZT=="y")
                {
                    lbSPZT.Text = "已通过";
                }
                else if (SPZT=="n")
                {
                    lbSPZT.Text = "未通过";
                }
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }


    }
}
