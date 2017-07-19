using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_FGDZCRK_SP : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                asd.userid = Session["UserID"].ToString();
                bindrpt();
            }
            //CheckUser(ControlFinder);
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "INCODE";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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
            string strWhere = string.Empty;

            strWhere = " SPLX='FGDZC' and INTYPE='1'";
            if (rblRW.SelectedValue == "1")
            {
                strWhere += " and ((SPR1ID='" + asd.userid + "' and SPR1_JL='' and SPZT='0') or (SPR2ID='" + asd.userid + "' and SPR2_JL='' and SPZT='1y') or (SPR3ID='" + asd.userid + "' and  SPR3_JL='' and SPZT='2y'))";
            }
            if (txtName.Text.Trim() != "")
            {
                strWhere += " and  NAME like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtModel.Text.Trim() != "")
            {
                strWhere += " and  MODEL like '%" + txtModel.Text.Trim() + "%'";
            }
            if (txtType.Text.Trim() != "")
            {
                strWhere += " and  TYPE like '%" + txtType.Text.Trim() + "%'";
            }
            return strWhere;
        }

        #endregion

        private class asd
        {
            public static string userid;
            public static string bh;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                asd.bh = string.Empty;
                return;
            }
            HyperLink link_bh = e.Item.FindControl("link_bh") as HyperLink;
            link_bh.Visible = false;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            if (drv["SPZT"].ToString() == "0")
            {
                if (asd.userid == drv["SPR1ID"].ToString())
                {
                    link_bh.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "1y")
            {
                if (asd.userid == drv["SPR2ID"].ToString())
                {
                    link_bh.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "2y")
            {
                if (asd.userid == drv["SPR3ID"].ToString())
                {
                    link_bh.Visible = true;
                }
            }
            if (asd.bh == drv["INCODE"].ToString())
            {
                e.Item.FindControl("lblInCode").Visible = false;
                link_bh.Visible = false;
            }
            else
            {
                asd.bh = drv["INCODE"].ToString();
            }
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            bindrpt();
        }
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtModel.Text = "";
            bindrpt();
        }
    }
}
