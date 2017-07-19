using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_RYPZQD_JL : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                BindDropdownList();
                bindrpt();
            }
        }

        private void BindDropdownList()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlBM.Items.Add(new ListItem("-全部-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlBM.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }
            //年
            sql = "select distinct(substring(PZ_RQ,1,4)) as NIAN from OM_RYPZ_JL ";
            DBCallCommon.BindDDLData(ddlYear, sql, "NIAN", "NIAN");
            //月
            sql = "select distinct(substring(PZ_RQ,6,7)) as YUE from OM_RYPZ_JL";
            DBCallCommon.BindDDLData(ddlMonth, sql, "YUE", "YUE");
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "OM_RYPZ_JL";
            pager_org.PrimaryKey = "ST_POSITION";
            pager_org.ShowFields = "*,(DEP_PZRS - DEP_YDRS) as DEP_QBRS ";
            pager_org.OrderField = "ST_DEPID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptRYPZ, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            BindFooter();
        }

        private void BindFooter()
        {
            string sql = "select * from OM_RYPZ_JL where PZ_ID is not null ";
            if (ddlBM.SelectedValue != "0")
            {
                sql += " and ST_DEPID='" + ddlBM.SelectedValue + "'";
            }
            if (ddlYear.SelectedValue != "")
            {
                sql += " and substring(PZ_RQ,1,4)='" + ddlYear.SelectedValue + "' ";
            }
            if (ddlMonth.SelectedValue != "")
            {
                sql += " and substring(PZ_RQ,6,7)='" + ddlMonth.SelectedValue + "'";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int pzrs = 0;
            int ydrs = 0;
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                if (dt.Rows[i]["DEP_PZRS"].ToString() == "")
                {
                    pzrs += 0;
                }
                else
                {
                    pzrs += Convert.ToInt32(dt.Rows[i]["DEP_PZRS"].ToString());
                }
                ydrs += Convert.ToInt32(dt.Rows[i]["DEP_YDRS"].ToString());
            }
            foreach (RepeaterItem item in rptRYPZ.Controls)
            {
                if (item.ItemType == ListItemType.Footer)
                {
                    ((Label)item.FindControl("lbPZRS")).Text = pzrs.ToString();
                    ((Label)item.FindControl("lbYDRS")).Text = ydrs.ToString();
                    ((Label)item.FindControl("lbWDRS")).Text = (pzrs - ydrs).ToString();
                    break;
                }
            }
        }

        private string StrWhere()
        {
            string sql = " PZ_ID is not null";
            if (ddlBM.SelectedValue != "0")
            {
                sql += " and ST_DEPID='" + ddlBM.SelectedValue + "'";
            }
            if (ddlYear.SelectedValue != "")
            {
                sql += " and substring(PZ_RQ,1,4)='" + ddlYear.SelectedValue + "' ";
            }
            if (ddlMonth.SelectedValue != "")
            {
                sql += " and substring(PZ_RQ,6,7)='" + ddlMonth.SelectedValue + "'";
            }
            return sql;
        }
        #endregion


        protected void rptRYPZ_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }


    }
}
