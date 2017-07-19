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
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KQTJdetail : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string stid="";
        string nianyue="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    stid = Request.QueryString["stid"].ToString().Trim();
                    nianyue = Request.QueryString["yearmonth"].ToString().Trim();
                }
                catch
                {
                    stid = "";
                    nianyue = "";
                }
                BindbmData();
                bindddlbz();
                this.BindYearMoth(ddlYear, ddlMonth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
                titlebind();
                danyuangehebing();
            }
            CheckUser(ControlFinder);
            InitVar();
        }



        /// <summary>
        /// 绑定年月

        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }


        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(8, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }

        private void bindddlbz()
        {
            string sql = "SELECT DISTINCT ST_DEPID1 FROM TBDS_STAFFINFO where ST_DEPID1!='' and ST_DEPID1 not in('0','1','2','3','4','5','6','7','8','9')";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlbz.DataTextField = "ST_DEPID1";
            ddlbz.DataValueField = "ST_DEPID1";
            ddlbz.DataSource = dt;
            ddlbz.DataBind();
            ListItem item = new ListItem("--请选择--", "--请选择--");
            ddlbz.Items.Insert(0, item);
        }

        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>

        private void InitPage()
        {
            txtName.Text = "";
            ddlYear.ClearSelection();
            foreach (ListItem li in ddlYear.Items)//显示当前年份
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            ddlMonth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)//显示当前月份
            {
                month = "0" + month;
            }
            foreach (ListItem li in ddlMonth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

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
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "View_OM_KQTJdetail";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "MX_STID,MX_YEARMONTH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 30;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and MX_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            if (stid != "" && nianyue != "")
            {
                sql += " and MX_STID='" + stid + "' and MX_YEARMONTH='" + nianyue + "'";
                ddlYear.SelectedValue = nianyue.Substring(0, 4);
                ddlMonth.SelectedValue = nianyue.Substring(5);
            }
            if (txtName.Text != "")
            {
                sql += " and ST_NAME like '%" + txtName.Text.ToString().Trim() + "%'";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (ddlbz.SelectedIndex != 0)
            {
                sql += " and ST_DEPID1='" + ddlbz.SelectedValue + "'";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptKQTJ, UCPaging1, palNoData);
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
        #endregion

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        protected void ddlbz_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        /// <summary>
        /// 年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }
        /// <summary>
        /// 月份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }



        private void titlebind()
        {
            string sqltext0 = "select BT_1,BT_2,BT_3,BT_4,BT_5,BT_6,BT_7,BT_8,BT_9,BT_10,BT_11,BT_12,BT_13,BT_14,BT_15,BT_16,BT_17,BT_18,BT_19,BT_20,BT_21,BT_22,BT_23,BT_24,BT_25,BT_26,BT_27,BT_28,BT_29,BT_30,BT_31 from OM_KQtitle where BT_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and BT_TYPE='0'";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);
            string sqltext1 = "select BT_1,BT_2,BT_3,BT_4,BT_5,BT_6,BT_7,BT_8,BT_9,BT_10,BT_11,BT_12,BT_13,BT_14,BT_15,BT_16,BT_17,BT_18,BT_19,BT_20,BT_21,BT_22,BT_23,BT_24,BT_25,BT_26,BT_27,BT_28,BT_29,BT_30,BT_31 from OM_KQtitle where BT_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and BT_TYPE='1'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            if (dt0.Rows.Count > 0 && dt1.Rows.Count > 0) 
            {
                for (int i = 0; i < dt0.Columns.Count; i++)
                {
                    ((Label)rptKQTJ.Controls[0].FindControl(dt0.Columns[i].ToString().Trim())).Text = dt0.Rows[0][dt0.Columns[i].ToString().Trim()].ToString().Trim();
                }

                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    ((Label)rptKQTJ.Controls[0].FindControl("WEEK" + dt1.Columns[i].ToString().Trim())).Text = dt1.Rows[0][dt1.Columns[i].ToString().Trim()].ToString().Trim();
                }
            }
        }



        private void danyuangehebing()
        {
            for (int i = rptKQTJ.Items.Count - 1; i > 0; i--)
            {
                HtmlTableCell oCell_previous0 = rptKQTJ.Items[i - 1].FindControl("td_ST_WORKNO") as HtmlTableCell;
                HtmlTableCell oCell0 = rptKQTJ.Items[i].FindControl("td_ST_WORKNO") as HtmlTableCell;

                HtmlTableCell oCell_previous1 = rptKQTJ.Items[i - 1].FindControl("td_ST_NAME") as HtmlTableCell;
                HtmlTableCell oCell1 = rptKQTJ.Items[i].FindControl("td_ST_NAME") as HtmlTableCell;

                HtmlTableCell oCell_previous2 = rptKQTJ.Items[i - 1].FindControl("td_DEP_NAME") as HtmlTableCell;
                HtmlTableCell oCell2 = rptKQTJ.Items[i].FindControl("td_DEP_NAME") as HtmlTableCell;

                HtmlTableCell oCell_previous3 = rptKQTJ.Items[i - 1].FindControl("td_ST_DEPID1") as HtmlTableCell;
                HtmlTableCell oCell3 = rptKQTJ.Items[i].FindControl("td_ST_DEPID1") as HtmlTableCell;

                HtmlTableCell oCell_previous4 = rptKQTJ.Items[i - 1].FindControl("td_MX_YEARMONTH") as HtmlTableCell;
                HtmlTableCell oCell4 = rptKQTJ.Items[i].FindControl("td_MX_YEARMONTH") as HtmlTableCell;

                HtmlTableCell oCell_previous5 = rptKQTJ.Items[i - 1].FindControl("td_MX_GZRCQ") as HtmlTableCell;
                HtmlTableCell oCell5 = rptKQTJ.Items[i].FindControl("td_MX_GZRCQ") as HtmlTableCell;

                oCell0.RowSpan = (oCell0.RowSpan == -1) ? 1 : oCell0.RowSpan;
                oCell_previous0.RowSpan = (oCell_previous0.RowSpan == -1) ? 1 : oCell_previous0.RowSpan;

                oCell1.RowSpan = (oCell1.RowSpan == -1) ? 1 : oCell1.RowSpan;
                oCell_previous1.RowSpan = (oCell_previous1.RowSpan == -1) ? 1 : oCell_previous1.RowSpan;

                oCell2.RowSpan = (oCell2.RowSpan == -1) ? 1 : oCell2.RowSpan;
                oCell_previous2.RowSpan = (oCell_previous2.RowSpan == -1) ? 1 : oCell_previous2.RowSpan;

                oCell3.RowSpan = (oCell3.RowSpan == -1) ? 1 : oCell3.RowSpan;
                oCell_previous3.RowSpan = (oCell_previous3.RowSpan == -1) ? 1 : oCell_previous3.RowSpan;

                oCell4.RowSpan = (oCell4.RowSpan == -1) ? 1 : oCell4.RowSpan;
                oCell_previous4.RowSpan = (oCell_previous4.RowSpan == -1) ? 1 : oCell_previous4.RowSpan;

                oCell5.RowSpan = (oCell5.RowSpan == -1) ? 1 : oCell5.RowSpan;
                oCell_previous5.RowSpan = (oCell_previous5.RowSpan == -1) ? 1 : oCell_previous5.RowSpan;

                if (oCell0.InnerText == oCell_previous0.InnerText)
                {
                    oCell0.Visible = false;
                    oCell_previous0.RowSpan += oCell0.RowSpan;

                    oCell1.Visible = false;
                    oCell_previous1.RowSpan += oCell1.RowSpan;

                    oCell2.Visible = false;
                    oCell_previous2.RowSpan += oCell2.RowSpan;

                    oCell3.Visible = false;
                    oCell_previous3.RowSpan += oCell3.RowSpan;

                    oCell4.Visible = false;
                    oCell_previous4.RowSpan += oCell4.RowSpan;

                    oCell5.Visible = false;
                    oCell_previous5.RowSpan += oCell5.RowSpan;
                }
            }
        }

    }
}
