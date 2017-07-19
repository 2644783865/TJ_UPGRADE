using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarWeihu : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        PagerQueryParam pager2 = new PagerQueryParam();
        PagerQueryParam pager3 = new PagerQueryParam();
        PagerQueryParam pager4 = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string action = "";
                string carId = "";
                if (Request["action"] != null)
                {
                    action = Request["action"].ToString();

                }
                if (Request["carId"] != null)
                {
                    carId = Request["carId"].ToString();

                }
                if (action != "" && carId != "")
                {
                    if (action == "carInfoWx")
                    {
                        ddlSearch.SelectedValue = "CARID";
                        txtSearch.Text = carId;
                    }
                    if (action == "carInfoBy")
                    {

                        TabContainer1.ActiveTabIndex = 1;
                        ddlSearch3.SelectedValue = "CARID";
                        txtSearch3.Text = carId;
                    }
                    if (action == "carInfoJy")
                    {
                        TabContainer1.ActiveTabIndex = 2;
                        ddlSearch2.SelectedValue = "CARNUM";
                        txtSearch2.Text = carId;
                    }
                }
            }
            InitVar1();
            InitVar2();
            InitVar3();
            InitVar4();
            if (!IsPostBack)
            {
           
                ddl1();
                Select_record1();
                Select_record2();
                GetBoundData1();
                GetBoundData2();
                ddl3();
                Select_record3();
                GetBoundData3();
                Select_record4();
                GetBoundData4();

            }
            CheckUser(ControlFinder);
        }
        //protected void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        //{
        //    for (int i = 0; i < 30; i++)
        //    {
        //        dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
        //    }
        //    dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        //    for (int k = 1; k <= 12; k++)
        //    {
        //        string j = k.ToString();
        //        if (k < 10)
        //        {
        //            j = "0" + k.ToString();
        //        }
        //        dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
        //    }
        //    dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        //}
        //private void InitPage()
        //{
        //    ddl_Year.ClearSelection();
        //    foreach (ListItem li in ddl_Year.Items)
        //    {
        //        if (li.Value.ToString() == DateTime.Now.Year.ToString())
        //        {
        //            li.Selected = true; break;
        //        }
        //    }

        //    ddl_Month.ClearSelection();
        //    string month = (DateTime.Now.Month - 1).ToString();
        //    if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)
        //    {
        //        month = "0" + month;
        //    }
        //    foreach (ListItem li in ddl_Month.Items)
        //    {
        //        if (li.Value.ToString() == month)
        //        {
        //            li.Selected = true; break;
        //        }
        //    }
        //}
        //protected void ddlSearch_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //this.GetBoundData();
        //    GetBoundData1();
        //    InitPager1();
        //    GetBoundData2();
        //    InitPager2();
        //}


        #region 分页1
        private void ddl1()
        {
            ddl_CODE.Items.Clear();
            string datatext;
            string datavalue;
            string sqltext = "select distinct(CODE) AS CODE from TBOM_CARWXSQ WHERE TYPEID='wx'  and state='4' and APPLYID='" + Session["UserID"].ToString() + "' and CODE not in (select distinct(CODE) AS CODE from TBOM_CARSAFE WHERE TYPEID='wx'  and state='4' and APPLYID='" + Session["UserID"].ToString() + "' )";
            datatext = "CODE";
            datavalue = "CODE";
            DBCallCommon.BindDdl(ddl_CODE, sqltext, datatext, datavalue);
            ddl_CODE.SelectedIndex = 1;
        }
        protected void add1_click(object sender, EventArgs e)
        {
            string code = ddl_CODE.SelectedValue.ToString();
            string alert = "<script>window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=jlwx&type=wx&id=" + code + "','','DialogWidth=1200px;DialogHeight=700px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", alert, false);
            Response.Write("<script>opener.location.href=opener.location.href;</script>");
        }
        private void InitVar1()
        {
            InitPager1();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged1);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager1()
        {
            pager.TableName = "TBOM_CARSAFE";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID*1 as ID,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,CARNAME,CARID,CODE";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
        }
        void Pager_PageChanged1(int pageNumber)
        {
            GetBoundData1();
        }
        protected void GetBoundData1()
        {
            InitPager1();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, gridview1, UCPaging1, NoDataPanel1);
            if (NoDataPanel1.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            InitVar1();
            GetBoundData1();
            Select_record1();
        }
        protected void btn_reset_OnClick(object sender, EventArgs e)
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            txtSearch.Text = "";
            ddlSearch.SelectedValue = "";
            InitVar1();
            UCPaging1.CurrentPage = 1;
            GetBoundData1();
            Select_record1();
        }

        private string GetWhere()
        {
            string sql = "TYPEID='wx'";
            string startTime = txtStartTime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtStartTime.Text.Trim();
            string endTime = txtEndTime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtEndTime.Text.Trim();
            sql += " and (PLACEDATE>='" + startTime + "' AND PLACEDATE<='" + endTime + "') ";
            if (ddlSearch.SelectedValue != "")
            {
                sql += "and  " + ddlSearch.SelectedValue.Trim() + "  like '%" + txtSearch.Text.Trim() + "%'";
            }
            return sql;
        }
        protected void lnkDelete1_OnClick(object sender, EventArgs e)
        {
            string ID1 = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete from TBOM_CARSAFE where ID='" + ID1 + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.GetBoundData1();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');window.location=window.location;", true);
            }
        }
        private void Select_record1()
        {
            string WHERE = GetWhere();
            string sqltext;
            if (WHERE == "")
            {
                sqltext = "select MONEYONE from TBOM_CARSAFE";
            }
            else
            {
                sqltext = "select MONEYONE from TBOM_CARSAFE where " + WHERE.ToString();
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_money = 0;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string je = dt.Rows[i]["MONEYONE"].ToString() == "" ? "0" : dt.Rows[i]["MONEYONE"].ToString();
                tot_money += Convert.ToDecimal(je);


            }
            lb_select_money.Text = string.Format("{0:c}", tot_money);

        }
        protected void gridview1_databound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = ((Label)e.Row.FindControl("lblCode")).Text.Trim();
                e.Row.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=modwx&type=wx&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看详细信息！");
            }
            e.Row.Attributes.Add("onmouseover", "this.oldcolor=this.style.backgroundColor;this.style.backgroundColor='#C8F7FF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldcolor");
        }
        #endregion

        #region 分页2
        private void InitVar2()
        {
            InitPager2();
            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged2);
            UCPaging2.PageSize = pager2.PageSize;
        }
        private void InitPager2()
        {
            pager2.TableName = "TBOM_CAROIL";
            pager2.PrimaryKey = "ID";
            pager2.ShowFields = "ID*1 as ID,CARNUM,RQ,YEAR,MONTH,DRIVER,UPRICE,OILNUM,MONEY,NOTE,CARDID,CARDYE,TYPE,OILTYPE,OILWEAR,CARLICHENG";
            pager2.OrderField = "RQ";
            pager2.StrWhere = GetWhere2();
            pager2.OrderType = 1;
            pager2.PageSize = 10;
        }
        void Pager_PageChanged2(int pageNumber)
        {
            ReGetBoundData2();
        }
        protected void GetBoundData2()
        {
            pager2.PageIndex = UCPaging2.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager2);
            CommonFun.Paging(dt, gridview2, UCPaging2, NoDataPanel2);
            if (NoDataPanel2.Visible)
            {
                UCPaging2.Visible = false;
            }
            else
            {
                UCPaging2.Visible = true;
                UCPaging2.InitPageInfo();
            }
        }
        private void ReGetBoundData2()
        {
            InitPager2();
            GetBoundData2();
            Select_record2();
        }
        protected void btnSearch2_OnClick(object sender, EventArgs e)
        {
            InitVar2();
            GetBoundData2();
            Select_record2();

        }
        protected void btn_reset2_OnClick(object sender, EventArgs e)
        {
            txtStartTime2.Text = "";
            txtEndTime2.Text = "";
            txtSearch2.Text = "";
            ddlSearch2.SelectedValue = "";
            InitVar2();
            UCPaging2.CurrentPage = 1;
            GetBoundData2();
            Select_record2();
        }

        private string GetWhere2()
        {
            string sql = "";
            string startTime = txtStartTime2.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtStartTime2.Text.Trim();
            string endTime = txtEndTime2.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtEndTime2.Text.Trim();
            sql += " (RQ>='" + startTime + "' AND RQ<='" + endTime + "') ";
            sql += " and CARNUM<>'' ";
            if (ddlSearch2.SelectedValue != "")
            {
                sql += "and  " + ddlSearch2.SelectedValue.Trim() + "  like '%" + txtSearch2.Text.Trim() + "%'";
            }
            return sql;
        }
        protected void lnkDelete2_OnClick(object sender, EventArgs e)
        {
            string ID2 = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete from TBOM_CAROIL where ID='" + ID2 + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.GetBoundData2();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');window.location=window.location;", true);
                //Response.Redirect("OM_CarWeihu.aspx");
            }
        }
        protected void gridview2_databound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("onmouseover", "this.oldcolor=this.style.backgroundColor;this.style.backgroundColor='#C8F7FF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldcolor");
        }
        private void Select_record2()
        {
            string WHERE = GetWhere2();
            string sqltext;
            if (WHERE == "")
            {
                sqltext = "select ID,OILNUM,MONEY from TBOM_CAROIL";
            }
            else
            {
                sqltext = "select ID,OILNUM,MONEY from TBOM_CAROIL where " + WHERE.ToString();
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num2.Text = dt.Rows.Count.ToString();

            decimal tot_money = 0;
            decimal OILNUM = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string je = dt.Rows[i]["OILNUM"].ToString() == "" ? "0" : dt.Rows[i]["OILNUM"].ToString();
                OILNUM += Convert.ToDecimal(je);


            }
            lb_oil.Text = OILNUM.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string je = dt.Rows[i]["MONEY"].ToString() == "" ? "0" : dt.Rows[i]["MONEY"].ToString();
                tot_money += Convert.ToDecimal(je);


            }
            lb_select_money2.Text = string.Format("{0:c}", tot_money);

        }
        #endregion

        #region 分页3
        private void ddl3()
        {
            ddl_CODE3.Items.Clear();
            string datatext;
            string datavalue;
            string sqltext = "select distinct(CODE) AS CODE from TBOM_CARWXSQ WHERE TYPEID='by' and APPLYID='" + Session["UserID"].ToString() + "' and state='4'";
            datatext = "CODE";
            datavalue = "CODE";
            DBCallCommon.BindDdl(ddl_CODE3, sqltext, datatext, datavalue);
            ddl_CODE3.SelectedIndex = 1;
        }
        protected void add3_click(object sender, EventArgs e)
        {
            string code = ddl_CODE3.SelectedValue.ToString();
            string alert = "<script>window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=jlby&type=by&id=" + code + "','','DialogWidth=1200px;DialogHeight=700px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", alert, false);
            Response.Write("<script>opener.location.href=opener.location.href;</script>");
        }
        private void InitVar3()
        {
            InitPager3();
            UCPaging3.PageChanged += new UCPaging.PageHandler(Pager_PageChanged3);
            UCPaging3.PageSize = pager3.PageSize;
        }
        private void InitPager3()
        {
            pager3.TableName = "TBOM_CARSAFE";
            pager3.PrimaryKey = "ID";
            pager3.ShowFields = "ID*1 as ID,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,CARNAME,CARID,CODE";
            pager3.OrderField = "";
            pager3.StrWhere = GetWhere3();
            pager3.OrderType = 1;
            pager3.PageSize = 10;
        }
        void Pager_PageChanged3(int pageNumber)
        {
            GetBoundData3();
        }
        protected void GetBoundData3()
        {
            InitPager3();
            pager3.PageIndex = UCPaging3.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager3);
            CommonFun.Paging(dt, gridview3, UCPaging3, NoDataPanel3);
            if (NoDataPanel3.Visible)
            {
                UCPaging3.Visible = false;
            }
            else
            {
                UCPaging3.Visible = true;
                UCPaging3.InitPageInfo();
            }
        }
        protected void btnSearch3_OnClick(object sender, EventArgs e)
        {
            InitVar3();
            GetBoundData3();
            Select_record3();
        }
        protected void btn_reset3_OnClick(object sender, EventArgs e)
        {
            txtStartTime3.Text = "";
            txtEndTime3.Text = "";
            txtSearch3.Text = "";
            ddlSearch3.SelectedValue = "";
            InitVar3();
            UCPaging3.CurrentPage = 1;
            GetBoundData3();
            Select_record3();
        }

        private string GetWhere3()
        {
            string sql = "TYPEID='by'";
            string startTime = txtStartTime3.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtStartTime3.Text.Trim();
            string endTime = txtEndTime3.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtEndTime3.Text.Trim();
            sql += " and (PLACEDATE>='" + startTime + "' AND PLACEDATE<='" + endTime + "') ";
            if (ddlSearch3.SelectedValue != "")
            {
                sql += "and  " + ddlSearch3.SelectedValue.Trim() + "  like '%" + txtSearch3.Text.Trim() + "%'";
            }
            return sql;
        }
        protected void lnkDelete3_OnClick(object sender, EventArgs e)
        {
            string ID3 = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete from TBOM_CARSAFE where ID='" + ID3 + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.GetBoundData1();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');window.location=window.location;", true);
            }
        }
        private void Select_record3()
        {
            string WHERE = GetWhere3();
            string sqltext;
            if (WHERE == "")
            {
                sqltext = "select MONEYONE from TBOM_CARSAFE";
            }
            else
            {
                sqltext = "select MONEYONE from TBOM_CARSAFE where " + WHERE.ToString();
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num3.Text = dt.Rows.Count.ToString();
            decimal tot_money = 0;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string je = dt.Rows[i]["MONEYONE"].ToString() == "" ? "0" : dt.Rows[i]["MONEYONE"].ToString();
                tot_money += Convert.ToDecimal(je);


            }
            lb_select_money3.Text = string.Format("{0:c}", tot_money);

        }
        protected void gridview3_databound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = ((Label)e.Row.FindControl("lblCode")).Text.Trim();
                e.Row.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=modby&type=by&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看详细信息！");
            }
            e.Row.Attributes.Add("onmouseover", "this.oldcolor=this.style.backgroundColor;this.style.backgroundColor='#C8F7FF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldcolor");
        }
        #endregion

        #region 分页4
        private void InitVar4()
        {
            InitPager4();
            UCPaging4.PageChanged += new UCPaging.PageHandler(Pager_PageChanged4);
            UCPaging4.PageSize = pager4.PageSize;
        }
        private void InitPager4()
        {
            pager4.TableName = "TBOM_CAROIL";
            pager4.PrimaryKey = "ID";
            pager4.ShowFields = "ID*1 as ID,CARNUM,RQ,YEAR,MONTH,DRIVER,CARDID,CARDYE,CARDBYE,CARDCZ,TYPE";
            pager4.OrderField = "";
            pager4.StrWhere = GetWhere4();
            pager4.OrderType = 1;
            pager4.PageSize = 10;
        }
        void Pager_PageChanged4(int pageNumber)
        {
            ReGetBoundData4();
        }
        protected void GetBoundData4()
        {
            pager4.PageIndex = UCPaging4.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager4);
            CommonFun.Paging(dt, gridview4, UCPaging4, NoDataPanel4);
            if (NoDataPanel4.Visible)
            {
                UCPaging4.Visible = false;
            }
            else
            {
                UCPaging4.Visible = true;
                UCPaging4.InitPageInfo();
            }
        }
        private void ReGetBoundData4()
        {
            InitPager4();
            GetBoundData4();
            Select_record4();
        }
        protected void btnSearch4_OnClick(object sender, EventArgs e)
        {
            InitVar4();
            GetBoundData4();
            Select_record4();

        }
        protected void btn_reset4_OnClick(object sender, EventArgs e)
        {
            txtStartTime4.Text = "";
            txtEndTime4.Text = "";
            txtSearch4.Text = "";
            ddlSearch4.SelectedValue = "";
            InitVar4();
            UCPaging4.CurrentPage = 1;
            GetBoundData4();
            Select_record4();
        }

        private string GetWhere4()
        {
            string sql = "TYPE='card'";
            string startTime = txtStartTime4.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtStartTime4.Text.Trim();
            string endTime = txtEndTime4.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtEndTime4.Text.Trim();
            sql += " and (RQ>='" + startTime + "' AND RQ<='" + endTime + "') ";
            sql += " and CARDCZ<>'' ";
            if (ddlSearch4.SelectedValue != "")
            {
                sql += "and  " + ddlSearch4.SelectedValue.Trim() + "  like '%" + txtSearch4.Text.Trim() + "%'";
            }
            return sql;
        }

        protected void gridview4_databound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("onmouseover", "this.oldcolor=this.style.backgroundColor;this.style.backgroundColor='#C8F7FF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldcolor");
        }
        private void Select_record4()
        {
            string WHERE = GetWhere4();
            string sqltext;
            if (WHERE == "")
            {
                sqltext = "select ID,CARDCZ from TBOM_CAROIL";
            }
            else
            {
                sqltext = "select ID,CARDCZ from TBOM_CAROIL where " + WHERE.ToString();
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num4.Text = dt.Rows.Count.ToString();

            decimal tot_money = 0;
            //decimal OILNUM = 0;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string je = dt.Rows[i]["OILNUM"].ToString() == "" ? "0" : dt.Rows[i]["OILNUM"].ToString();
            //    OILNUM += Convert.ToDecimal(je);


            //}
            //lb_oil.Text = OILNUM.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string je = dt.Rows[i]["CARDCZ"].ToString() == "" ? "0" : dt.Rows[i]["CARDCZ"].ToString();
                tot_money += Convert.ToDecimal(je);


            }
            lb_select_money4.Text = string.Format("{0:c}", tot_money);

        }
        #endregion
    }
}
