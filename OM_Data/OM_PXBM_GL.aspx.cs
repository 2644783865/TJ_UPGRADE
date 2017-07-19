using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PXBM_GL : System.Web.UI.Page
    {

        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                BindDropdownList();
                asd.username = Session["UserName"].ToString();
                asd.userid = Session["UserID"].ToString();
                bindrpt();
                MergeCells();
            }
        }

        private class asd
        {
            public static string username;
            public static string userid;
            public static string name;
        }

        private void BindDropdownList()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlBM.Items.Add(new ListItem("-全部-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlBM.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_NAME"].ToString()));
            }
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
            MergeCells();
        }

        private void bindrpt()
        {
            pager_org.TableName = "OM_PXBM as a left join  OM_PXJH_SQ as b on a.BM_FATHERID = b.PX_ID";
            pager_org.PrimaryKey = "BM_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "BM_BMR,BM_TXSJ,BM_BM,BM_TXR";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptPXBM, UCPaging1, palNoData);
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
            string sql = "0=0 ";
            if (ddlBM.SelectedValue!="0")
            {
                sql += " and BM_BM like '%"+ddlBM.SelectedValue+"%'";
            }
            if (txtBMR.Text.Trim()!="")
            {
                sql += " and BM_BMR like '%"+txtBMR.Text.Trim()+"%'";
            }
            if (txtXMMC.Text.Trim()!="")
            {
                sql += " and PX_XMMC like '%"+txtXMMC.Text.Trim()+"%'";
            }
            if (txtZJR.Text.Trim()!="")
            {
                sql += " and PX_ZJR like '%" + txtZJR.Text.Trim() + "%'";
            }
            return sql;
        }

        private void MergeCells()//合并单元格
        {

        }
        #endregion

        protected void rptPXBM_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                asd.name = string.Empty;
                return;
            }
            DataRowView drv = (DataRowView)e.Item.DataItem;
            Label name = (Label)e.Item.FindControl("BM_BMR");
            if (name.Text==asd.name)
            {
                name.Visible = false;
            }
            else
            {
                asd.name = name.Text;
            }
        }

        protected void lbtnDelete_OnClick(object sender, EventArgs e)
        {
            string BM_ID = ((LinkButton)sender).CommandArgument.ToString();
            string sql = "delete from OM_PXBM where BM_ID ="+BM_ID;
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch 
            {
                Response.Write("<script>alert('删除的sql语句出现问题，请与管理员联系！！！')</script>");
                return;
            }
            bindrpt();
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

    }
}
