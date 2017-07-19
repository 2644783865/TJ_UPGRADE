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
using System.IO;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_External_Audit : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);

            if (!IsPostBack)
            {
                bindGrid();
            }
            CheckUser(ControlFinder);
        }
        private void bindGrid()
        {
            pager.TableName = "View_TBQC_EXTERNAL_AUDIT";
            pager.PrimaryKey = "PRO_ID";
            pager.ShowFields = "*";
            pager.OrderField = "PRO_ID";
            pager.StrWhere = ConStr();
            pager.OrderType = 0;//升序排列
            pager.PageSize = 15;
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGrid();
        }

        private string ConStr()
        {
            string sql = " 1=1 ";

            if (ddl_state.SelectedValue == "1")
            {
                sql += " and PRO_STATE in ('1','2','4','5')";
            }
            if (ddl_state.SelectedValue == "2")
            {
                sql += " and PRO_STATE='7'";
            }
            if (ddl_state.SelectedValue == "3")
            {
                sql += " and ((PRO_STATE='1' and PRO_SPR='" + Session["UserID"].ToString() + "' ) or( PRO_STATE='2' and PRO_ZGR='" + Session["UserID"].ToString() + "') or (PRO_STATE='4' and PRO_SPR='" + Session["UserID"].ToString() + "') or (PRO_STATE='5' and PRO_SHY='" + Session["UserID"].ToString() + "'))";
            }
            if (ddl_state.SelectedValue == "4")
            {
                sql += " and PRO_STATE in ('3','6')";
            }
            if (ddl_state.SelectedValue == "5")
            {
                sql += " and PRO_SHY='" + Session["UserID"].ToString() + "'";
            }

            return sql;
        }
        protected void LinkButDel_Click(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqltext = "delete from TBQC_EXTERNAL_AUDIT where Id=" + lotnum;
            DBCallCommon.ExeSqlText(sqltext);
            bindGrid();

        }

        protected void LinkButBack_Click(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqltext = "update TBQC_INTERNAL_AUDIT set PRO_STATE='1'  where Id=" + lotnum;
            DBCallCommon.ExeSqlText(sqltext);
            Response.Redirect("~/QC_Data/QC_Internal_Audit_Edit.aspx?action=edit&Id=" + lotnum);
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {

            bindGrid();
        }

        protected void Purordertotal_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ((System.Web.UI.Control)e.Item.FindControl("hpyAudit")).Visible = false;
                ((System.Web.UI.Control)e.Item.FindControl("LinkButDel")).Visible = false;
                if (ddl_state.SelectedValue == "1")
                {
                    ((System.Web.UI.Control)e.Item.FindControl("hpyAudit")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("LinkButDel")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("linButBack")).Visible = false;
                }
                if (ddl_state.SelectedValue == "2")
                {
                    ((System.Web.UI.Control)e.Item.FindControl("hpyAudit")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("LinkButDel")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("linButBack")).Visible = false;
                }
                if (ddl_state.SelectedValue == "3")
                {
                    ((System.Web.UI.Control)e.Item.FindControl("hpyAudit")).Visible = true;
                    ((System.Web.UI.Control)e.Item.FindControl("LinkButDel")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("linButBack")).Visible = false;
                }
                if (ddl_state.SelectedValue == "4")
                {
                    ((System.Web.UI.Control)e.Item.FindControl("hpyAudit")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("LinkButDel")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("linButBack")).Visible = false;
                }
                if (ddl_state.SelectedValue == "5")
                {
                    ((System.Web.UI.Control)e.Item.FindControl("hpyAudit")).Visible = false;
                    ((System.Web.UI.Control)e.Item.FindControl("LinkButDel")).Visible = true;
                    ((System.Web.UI.Control)e.Item.FindControl("linButBack")).Visible = true;
                }
            }
        }
    }
}
