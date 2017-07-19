using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PXXQ_GL : System.Web.UI.Page
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
            }

        }

        private class asd
        {
            public static string username;
            public static string userid;
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
            pager_org.TableName = "OM_PXDC as a left join OM_PXDC_NR as b on a.DC_SJID=b.FATHERID";
            pager_org.PrimaryKey = "DC_ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "DC_TXRBM,DC_TXRID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptPXDC, UCPaging1, palNoData);
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
                sql += " and DC_TXRBM like '%"+ddlBM.SelectedValue+"%'";
            }
            if (txtNAME.Text.Trim()!="")
            {
                sql += " and DC_TXR like '%" + txtNAME.Text.Trim() + "%'";
            }
            return sql;
        }

        private void MergeCells()//合并单元格
        {

        }
        #endregion

        protected void rptPXDC_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            HyperLink hplAlter = (HyperLink)e.Item.FindControl("hplAlter");
            hplAlter.Visible = false;
            if (asd.userid==drv["DC_TXRID"].ToString())
            {
                hplAlter.Visible = true;
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }
    }
}
