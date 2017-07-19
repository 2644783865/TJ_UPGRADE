using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Drawing;
namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_SCDYTZSP : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            username = Session["UserName"].ToString();
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
            pager_org.TableName = "PM_SCDYTZ";
            pager_org.PrimaryKey = "TZD_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "TZD_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptDYTZSP, UCPaging1, palNoData);
            if (palNoData.Visible)
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
            string sql = "TZD_ID is not null";
            if (rblSPZT.SelectedValue == "1")
            {
                sql += " and TZD_SPZT='0'";
            }
            else if (rblSPZT.SelectedValue == "2")
            {
                sql += " and TZD_SPZT='1y'";
            }
            else if (rblSPZT.SelectedValue == "3")
            {
                sql += " and TZD_SPZT='2y'";
            }
            else if (rblSPZT.SelectedValue == "4")
            {
                sql += " and TZD_SPZT in('1n','2n')";
            }
            if (rblCLZT.SelectedValue == "1")
            {
                sql += " and TZD_CLZT='0'";
            }
            else if (rblCLZT.SelectedValue == "2")
            {
                sql += " and TZD_CLZT='1'";
            }
            if (ddlSX.SelectedValue != "0" && txtItem.Text.Trim() != "")
            {
                sql += " and " + ddlSX.SelectedValue.Trim() + " like '%" + txtItem.Text.Trim() + "%'";
            }
            if (cbxRW.Checked)
            {
                sql += " and ((TZD_SPR1='" + username + "' and TZD_SPR1_JL is null) or (TZD_SPR2='" + username + "' and TZD_SPR2_JL is null))";
            }
            return sql;
        }

        protected void rptDYTZSP_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("td1");
                HyperLink hplSP = (HyperLink)e.Item.FindControl("hplSP");
                HyperLink hplAlter = (HyperLink)e.Item.FindControl("hplAlter");
                LinkButton lbtnSolve = (LinkButton)e.Item.FindControl("lbtnSolve");
                string TZD_SPZT = ((HiddenField)e.Item.FindControl("TZD_SPZT")).Value.Trim();
                string TZD_SPR1 = ((HiddenField)e.Item.FindControl("TZD_SPR1")).Value.Trim();
                string TZD_SPR2 = ((HiddenField)e.Item.FindControl("TZD_SPR2")).Value.Trim();
                string TZD_CLZT = ((HiddenField)e.Item.FindControl("TZD_CLZT")).Value.Trim();
                string TZD_ZDR = ((HiddenField)e.Item.FindControl("TZD_ZDR")).Value;
                hplSP.Visible = false;
                hplAlter.Visible = false;
                lbtnSolve.Visible = false;
                if (TZD_SPZT == "0")
                {
                    if (username == TZD_ZDR)
                    {
                        hplAlter.Visible = true;
                    }
                    if (username == TZD_SPR1)
                    {
                        hplSP.Visible = true;
                    }
                }
                else if (TZD_SPZT == "1y")
                {
                    if (username == TZD_SPR2)
                    {
                        hplSP.Visible = true;
                    }
                }
                else if (TZD_SPZT == "1n" || TZD_SPZT == "2n")
                {
                    if (username == TZD_ZDR)
                    {
                        hplAlter.Visible = true;
                    }
                }
                if (TZD_CLZT == "0")
                {
                    if (username == TZD_ZDR)
                    {
                        lbtnSolve.Visible = true;
                    }
                }
                else if (TZD_CLZT == "1")
                {
                    td1.BgColor = "Green";
                }
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void lbtnSolve_OnClick(object sender, EventArgs e)
        {
            string TZD_ID = ((LinkButton)sender).CommandArgument.ToString().Trim();
            string sql = "update PM_SCDYTZ set TZD_CLZT ='1' where TZD_ID=" + TZD_ID;
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("<script>alert('处理出现问题，请与管理员联系！！！')</script>");
                return;
            }
            Response.Write("<script>alert('您已成功处理该单据！！！')</script>");
            bindrpt();
        }

    }
}
