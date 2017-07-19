using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GdzcIn : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                string sql = "select count(*) as num from TBOM_GDZCIN WHERE BIANHAO='' and INTYPE='0'";
                DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
                if (DT.Rows.Count > 0)
                {
                    num.Text = DT.Rows[0]["num"].ToString();
                }
                GetBoundData();
            }
            CheckUser(ControlFinder);
        }

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
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
                UCPaging1.InitPageInfo();
            }
        }
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }

        private string GetWhere()
        {
            string strWhere = string.Empty;

            strWhere = " SPLX='GDZC' and INTYPE='0' ";
            if (rblSPZT.SelectedValue == "1")
            {
                strWhere += " and SPZT in ('0','1y','2y')";
            }
            else if (rblSPZT.SelectedValue == "2")
            {
                strWhere += " and SPZT ='y'";
            }
            else if (rblSPZT.SelectedValue == "3")
            {
                strWhere += " and SPZT ='n'";
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Session["UserDeptID"].ToString() == "02")
                {
                    string bianhao = ((Label)e.Item.FindControl("lblb_bh")).Text.ToString();
                    if (bianhao == "")
                    {
                        ((HyperLink)e.Item.FindControl("link_bh")).Visible = true;
                    }
                }
            }

        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }

        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtModel.Text = "";
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }

    }
}
