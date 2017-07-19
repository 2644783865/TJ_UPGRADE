using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_RYXQJH_GL : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindrpt();
            }
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "OM_ZPJH";
            pager_org.PrimaryKey = "JH_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "JH_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptZPJH, UCPaging1, palNoData);
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
            string sql = " JH_LX ='ND' ";
            if (rblRW.SelectedValue == "1")
            {
                sql += " and JH_SQR like'%" + username + "%'";
            }
            if (rblHZZT.SelectedValue == "0")
            {
                sql += " and JH_SFTJ='n'";
            }
            else if (rblHZZT.SelectedValue == "1")
            {
                sql += " and JH_SFTJ='y'";
            }
            else if (rblHZZT.SelectedValue == "2")
            {
                sql += " and JH_SFHZ='y'";
            }
            if (txtBM.Text.Trim() != "")
            {
                sql += " and JH_ZPBM like'%" + txtBM.Text.Trim() + "%'";
            }
            if (txtGW.Text.Trim() != "")
            {
                sql += " and JH_GWMC like '%" + txtGW.Text.Trim() + "%'";
            }
            return sql;
        }
        #endregion

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void rptZPJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            HyperLink hplAlter = (HyperLink)e.Item.FindControl("hplAlter");
            LinkButton lbtn = (LinkButton)e.Item.FindControl("lbtnDelete");
            hplAlter.Visible = false;
            lbtn.Visible = false;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            if (drv["JH_SFTJ"].ToString() != "y" && username == drv["JH_SQR"].ToString())
            {
                hplAlter.Visible = true;
                lbtn.Visible = true;
            }
        }
        protected void lbtnDelete_OnClick(object sender, EventArgs e)
        {
            string JH_ID = ((LinkButton)sender).CommandArgument.ToString();
            string sql = " delete from OM_ZPJH where JH_ID=" + JH_ID;
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("<script>alert('删除的语句出现问题，请与管理员联系')</script>");
                return;
            }
            bindrpt();
        }

    }
}
