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
    public partial class PM_BANZUJS : BasicPage
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
            CheckUser(ControlFinder);
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
        PagerQueryParam pager = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "TBPM_BZWLJS";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID,BZ_YEARMONTH,BZ_MARID,BZ_MARNAME,BZ_GUIGE,BZ_RWH,BZ_ENG,BZ_UNIT,BZ_NUM,BZ_PRICE,(BZ_PRICE*1.17) as BZ_HSPRICE,BZ_CTAMOUNT,(BZ_CTAMOUNT*1.17) as BZ_HSCTAMOUNT,BZ_ZDRNAME,case when BZ_LYBZ='0' then '无' else BZ_LYBZ end as BZ_LYBZ,left(BZ_DATE,10) as BZ_DATE,BZ_NOTE";
            pager.OrderField = "BZ_YEARMONTH,BZ_LYBZ,BZ_MARID";
            pager.StrWhere = Creatconstr();
            pager.OrderType = 0;
            pager.PageSize = 50;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
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
            else if(dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
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
            if(txtrwh.Text.ToString().Trim()!="")
            {
                sqltext += " and BZ_RWH like '%" + txtrwh.Text.ToString().Trim() + "%'";
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

                if (dthj.Rows.Count>0)
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

        //生成数据
        protected void btnscsj_click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string sqlhs = "select * from TBFM_HSTOTAL where HS_STATE='2' and HS_YEAR='" + dplYear.SelectedValue.ToString().Trim() + "' and HS_MONTH='" + dplMoth.SelectedValue.ToString().Trim() + "'";
            DataTable dths = DBCallCommon.GetDTUsingSqlText(sqlhs);
            if (dths.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月未核算，不能生成数据！！！');", true);
                return;
            }
            else
            {
                string sqlscif = "select * from TBPM_BZWLJS where BZ_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "'";
                DataTable dtscif = DBCallCommon.GetDTUsingSqlText(sqlscif);
                if (dtscif.Rows.Count > 0)
                {
                    string sqldelete = "delete from TBPM_BZWLJS where BZ_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "'";
                    DBCallCommon.ExeSqlText(sqldelete);
                }
                string sqldatasource = "select * from View_SM_OUT left join TBPM_TCTSASSGN on TSAID=TSA_ID left join View_TBCR_CONTRACTREVIEW_ALL on TSA_PJID=PCON_BCODE where ApprovedDate like '" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "-%' and MaterialCode in (select MARID from TBWS_STORAGE_WARN where BZJSBZ='1')";// and WarehouseCode like '05.%',去掉在安全库存仓库里出的限制
                DataTable dtdatasource = DBCallCommon.GetDTUsingSqlText(sqldatasource);
                if (dtdatasource.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdatasource.Rows.Count; i++)
                    {
                        string sqlinsert = "insert into TBPM_BZWLJS(BZ_YEARMONTH,BZ_MARID,BZ_MARNAME,BZ_GUIGE,BZ_RWH,BZ_ENG,BZ_UNIT,BZ_NUM,BZ_PRICE,BZ_CTAMOUNT,BZ_ZDRID,BZ_ZDRNAME,BZ_LYBZ,BZ_DATE,BZ_NOTE) values('" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "','" + dtdatasource.Rows[i]["MaterialCode"].ToString().Trim() + "','" + dtdatasource.Rows[i]["MaterialName"].ToString().Trim() + "','" + dtdatasource.Rows[i]["Standard"].ToString().Trim() + "','" + dtdatasource.Rows[i]["TSAID"].ToString().Trim() + "','" + dtdatasource.Rows[i]["CR_XMMC"].ToString().Trim() + "','" + dtdatasource.Rows[i]["Unit"].ToString().Trim() + "','" + dtdatasource.Rows[i]["RealNumber"].ToString().Trim() + "','" + dtdatasource.Rows[i]["UnitPrice"].ToString().Trim() + "','" + dtdatasource.Rows[i]["Amount"].ToString().Trim() + "','" + dtdatasource.Rows[i]["DocCode"].ToString().Trim() + "','" + dtdatasource.Rows[i]["Doc"].ToString().Trim() + "','" + dtdatasource.Rows[i]["TotalNote"].ToString().Trim() + "','" + dtdatasource.Rows[i]["ApprovedDate"].ToString().Trim() + "','" + dtdatasource.Rows[i]["DetailNote"].ToString().Trim() + "')";
                        list.Add(sqlinsert);
                    }
                }
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据生成成功！！！');", true);
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
        }

        //导出数据
        protected void brnsjdc_click(object sender, EventArgs e)
        {

            string sql_dc = " select BZ_YEARMONTH,BZ_MARID,BZ_MARNAME,BZ_GUIGE,BZ_RWH,BZ_ENG,BZ_UNIT,BZ_NUM,BZ_PRICE,(BZ_PRICE*1.17) as BZ_HSPRICE,BZ_CTAMOUNT,(BZ_CTAMOUNT*1.17) as BZ_HSCTAMOUNT,BZ_ZDRNAME,case when BZ_LYBZ='0' then '无' else BZ_LYBZ end as BZ_LYBZ,left(BZ_DATE,10) as BZ_DATE,BZ_NOTE from TBPM_BZWLJS  WHERE  ";
            sql_dc += Creatconstr();
            sql_dc += " order by BZ_YEARMONTH,BZ_LYBZ,BZ_MARID ";

            exportCommanmethod.exporteasy(sql_dc, "班组低值易耗品结算.xls", "班组低值易耗品结算.xls", true, true, false);

        }
    }
}
