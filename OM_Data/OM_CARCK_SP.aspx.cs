using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CARCK_SP : System.Web.UI.Page
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
            pager_org.TableName = "OM_CARCK_SP";
            pager_org.PrimaryKey = "SP_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "ZDR_SJ";
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

            strWhere = " 1=1";
            if (rblRW.SelectedValue == "1")
            {
                strWhere += " and ((SPR1_ID='" + asd.userid + "' and SPR1_JL='' and SPZT='0') or (SPR2_ID='" + asd.userid + "' and SPR2_JL='' and SPZT='1y'))";
            }
            if (txtName.Text.Trim() != "")
            {
                strWhere += " and  SP_MC like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtModel.Text.Trim() != "")
            {
                strWhere += " and  SP_GG like '%" + txtModel.Text.Trim() + "%'";
            }
            return strWhere;
        }

        #endregion

        private class asd
        {
            public static string userid;

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {

                return;
            }
            HyperLink link_bh = e.Item.FindControl("link_bh") as HyperLink;
            link_bh.Visible = false;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            if (drv["SPZT"].ToString() == "0")
            {
                if (asd.userid == drv["SPR1_ID"].ToString())
                {
                    link_bh.Visible = true;
                }
            }
            else if (drv["SPZT"].ToString() == "1y")
            {
                if (asd.userid == drv["SPR2_ID"].ToString())
                {
                    link_bh.Visible = true;
                }
            }
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            bindrpt();
        }
    }
}
