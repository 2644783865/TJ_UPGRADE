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
using System.Collections.Generic;
using System.Xml.Linq;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_BANZUHZ : System.Web.UI.Page
    {
        double totalnum = 0;
        double totalmoney = 0;
        double totalhsmoney = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                UCPaging1.CurrentPage = 1;
                this.InitPage();
                this.InitVar();
                this.bindGrid();
            }
            this.InitVar();
        }


        //初始化页面
        private void InitPage()
        {
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }
            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month).ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }


        #region 分页
        PagerQueryParamGroupBy pager_org = new PagerQueryParamGroupBy();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager_org.TableName = "TBPM_BZWLJS";
            pager_org.PrimaryKey = "";
            pager_org.ShowFields = "BZ_YEARMONTH,BZ_MARID,BZ_MARNAME,BZ_GUIGE,BZ_UNIT,sum(BZ_NUM) as BZ_NUM,sum(BZ_CTAMOUNT) as BZ_CTAMOUNT,sum(BZ_CTAMOUNT*1.17) as BZ_HSCTAMOUNT,BZ_LYBZ";
            pager_org.OrderField = "BZ_YEARMONTH,BZ_LYBZ,BZ_MARID";
            pager_org.StrWhere = Creatconstr();
            pager_org.OrderType = 0;
            pager_org.PageSize = 50;
            pager_org.GroupField = "BZ_YEARMONTH,BZ_LYBZ,BZ_MARID,BZ_MARNAME,BZ_GUIGE,BZ_UNIT";
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion


        private string Creatconstr()
        {
            string sqltext = "1=1";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sqltext += " and BZ_YEARMONTH like '" + dplYear.SelectedValue.ToString().Trim() + "-%'";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sqltext += " and BZ_YEARMONTH like '%-" + dplMoth.SelectedValue.ToString().Trim() + "'";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                sqltext += " and BZ_YEARMONTH like '%" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "%'";
            }
            if (tbname.Text.ToString().Trim() != "")
            {
                sqltext += " and BZ_MARNAME like '%" + tbname.Text.ToString().Trim() + "%'";
            }
            if (tbguige.Text.ToString().Trim() != "")
            {
                sqltext += " and BZ_GUIGE like '%" + tbguige.Text.ToString().Trim() + "%'";
            }
            if (tbbanzu.Text.ToString().Trim() != "")
            {
                sqltext += " and BZ_LYBZ like '%" + tbbanzu.Text.ToString().Trim() + "%'";
            }
            return sqltext;
        }


        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void dplyearmonth_changed_click(object sneder, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select sum(BZ_NUM) as BZ_NUMHJ,sum(BZ_CTAMOUNT) as BZ_CTAMOUNTHJ,sum(BZ_CTAMOUNT*1.17) as BZ_HSCTAMOUNTHJ from TBPM_BZWLJS where " + Creatconstr();

                DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);

                if (dthj.Rows.Count > 0)
                {
                    try
                    {
                        totalnum = Convert.ToDouble(dthj.Rows[0]["BZ_NUMHJ"].ToString().Trim());
                        totalmoney = Convert.ToDouble(dthj.Rows[0]["BZ_CTAMOUNTHJ"].ToString().Trim());
                        totalhsmoney = Convert.ToDouble(dthj.Rows[0]["BZ_HSCTAMOUNTHJ"].ToString().Trim());
                    }
                    catch
                    {
                        totalnum = 0;
                        totalmoney = 0;
                        totalhsmoney = 0;
                    }

                }
                System.Web.UI.WebControls.Label lbtotalnum = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbtotalnum");
                System.Web.UI.WebControls.Label lbtotalmny = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbtotalmny");
                System.Web.UI.WebControls.Label lbtotalhsmny = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbtotalhsmny");
                lbtotalnum.Text = totalnum.ToString();
                lbtotalmny.Text = totalmoney.ToString();
                lbtotalhsmny.Text = totalhsmoney.ToString();
            }
        }

        ////导出
        protected void btn_huidc_OnClick(object sender, EventArgs e)
        {
            string sql_dc = "  select BZ_YEARMONTH,BZ_MARID,BZ_MARNAME,BZ_GUIGE,BZ_UNIT,sum(BZ_NUM) as BZ_NUM,sum(BZ_CTAMOUNT) as BZ_CTAMOUNT,sum(BZ_CTAMOUNT*1.17) as BZ_HSCTAMOUNT,BZ_LYBZ from  TBPM_BZWLJS where  ";
            sql_dc += Creatconstr();
            sql_dc += " GROUP BY BZ_YEARMONTH,BZ_LYBZ,BZ_MARID,BZ_MARNAME,BZ_GUIGE,BZ_UNIT ";
            sql_dc += " order by BZ_YEARMONTH,BZ_LYBZ,BZ_MARID";

            exportCommanmethod.exporteasy(sql_dc, "班组低值易耗结算（按班组日期物料汇总）.xls", "班组低值易耗结算（按班组日期物料汇总）.xls", true, true, false);

        }
    }
}
