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
using System.Collections.Generic;
using System.IO;


namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_JXGZYESP : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                //{
                //    radio_all.Visible = false;
                //}
                this.BindYearMoth(ddlYear, ddlMonth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
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

        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>

        private void InitPage()
        {
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
            pager_org.TableName = "OM_JXGZYESP";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "ID,bh,things,yearmonth,creattime,creatstname,totalstate,jxadddepartment";
            pager_org.OrderField = "yearmonth,bh";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 50;
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
                sql += " and yearmonth='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            else if (ddlYear.SelectedIndex == 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and yearmonth like '%-" + ddlMonth.SelectedValue.ToString().Trim() + "%'";
            }
            else if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex == 0)
            {
                sql += " and yearmonth like '%" + ddlYear.SelectedValue.ToString().Trim() + "-%'";
            }
            if (drp_state.SelectedIndex != 0)
            {
                sql += " and totalstate='" + drp_state.SelectedValue.ToString().Trim() + "'";
            }
            if (radio_mytask.Checked == true)
            {
                sql = "((creatstid='" + Session["UserID"].ToString().Trim() + "' and totalstate='0') or (shrid1='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='0') or (shrid2='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='1'and shstate2='0'))";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rpt1, UCPaging1, palNoData);
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


        /// <summary>
        /// 年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        /// <summary>
        /// 月份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_mytask_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void drp_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int num = 0;
            foreach (RepeaterItem rptitem in rpt1.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx1 = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("CKBOX_SELECT");
                System.Web.UI.WebControls.Label lbid1 = (System.Web.UI.WebControls.Label)rptitem.FindControl("ID");
                if (cbx1.Checked == true)
                {
                    string sqltext = "select * from OM_JXGZYESP where ID='" + lbid1.Text.Trim() + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0 && dt.Rows[0]["totalstate"].ToString().Trim() != "0" && dt.Rows[0]["totalstate"].ToString().Trim() != "3")
                    {
                        num++;
                    }
                }
            }
            if (num > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选项中有审核中或审核通过的项，不能删除！');", true);
                return;
            }
            foreach (RepeaterItem rptitem in rpt1.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("CKBOX_SELECT");
                System.Web.UI.WebControls.Label lbid = (System.Web.UI.WebControls.Label)rptitem.FindControl("ID");
                if (cbx.Checked == true)
                {
                    string sqldelete = "delete from OM_JXGZYESP where ID='" + lbid.Text.Trim() + "'";
                    sqltextlist.Add(sqldelete);
                }
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
    }
}
