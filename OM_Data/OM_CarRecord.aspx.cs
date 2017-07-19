using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarRecord : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {




            InitVar();
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
                    if (action == "carInfoYX")
                    {
                        ddlSearch.SelectedValue = "CARNUM";
                        ddlSearch_change(null, null);
                        ddlcontent.SelectedValue = carId;
                    }
                }


                GetBoundData();
                Select_record();
                GridView1.Columns[0].HeaderStyle.CssClass = "fixed";
                GridView1.Columns[0].ItemStyle.CssClass = "fixed";
                GridView1.Columns[1].HeaderStyle.CssClass = "fixed";
                GridView1.Columns[1].ItemStyle.CssClass = "fixed";
            }
            CheckUser(ControlFinder);

        }
        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            InitVar();
            GetBoundData();
            Select_record();
        }
        protected void btn_reset_OnClick(object sender, EventArgs e)
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            ddlSearch.SelectedValue = "";
            InitVar();
            UCPaging1.CurrentPage = 1;
            //ddlcontent.SelectedValue = "";
            GetBoundData();
            Select_record();
        }
        protected void ddlSearch_change(object sender, EventArgs e)
        {
            if (ddlSearch.SelectedValue == "CARNUM")
            {
                ddlcontent.Items.Clear();
                string sql = "select CARNUM from TBOM_CARINFO";
                DBCallCommon.BindDdl(ddlcontent, sql, "CARNUM", "CARNUM");
            }
            if (ddlSearch.SelectedValue == "SJNAME")
            {
                ddlcontent.Items.Clear();
                string sql = "select ST_NAME,ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION='0202'";
                DBCallCommon.BindDdl(ddlcontent, sql, "ST_NAME", "ST_NAME");
            }

        }
        private string GetWhere()
        {
            string sql = " ID IN(SELECT MIN(ID) FROM View_TBOM_CARAPLLRVW GROUP BY(FACHEONLY) ) AND FACHE='1' ";
            string time1 = "";
            string time2 = "";
            if (txtStartTime.Text.ToString() != "" && txtEndTime.Text.ToString() != "")
            {
                time1 = txtStartTime.Text.Substring(0, 4) + '/' + txtStartTime.Text.Substring(5, 2) + '/' + txtStartTime.Text.Substring(8, 2);
                time2 = txtEndTime.Text.Substring(0, 4) + '/' + txtEndTime.Text.Substring(5, 2) + '/' + txtEndTime.Text.Substring(8, 2);
            }

            string startTime = txtStartTime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : time1.ToString();
            string endTime = txtEndTime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : time2.ToString();
            sql += " and (YDTIME>='" + startTime + "' AND YDTIME<='" + endTime + "') ";
            if (ddlSearch.SelectedValue != "")
            {
                sql += "and patindex('%" + ddlcontent.SelectedValue.Trim() + "%'," + ddlSearch.SelectedValue.Trim() + ")>0";
            }
            return sql;
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
            pager.TableName = "TBOM_CARAPPLY";
            pager.PrimaryKey = "CARNUM";
            pager.ShowFields = "CARNUM,SFPLACE,DESTINATION,YDTIME,TIME2,LICHENG1,LICHENG2,SJNAME,APPLYER,NOTE,WHOLETIME,WHOLEJULI,SJNOTE";
            pager.OrderField = "YDTIME";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }
        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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
        //private void ReGetBoundData()
        //{
        //    InitPager();
        //    GetBoundData();
        //    Select_record();
        //}
        private void Select_record()
        {
            string WHERE = GetWhere();
            string sqltext;
            if (WHERE == "")
            {
                sqltext = "select CARNUM,WHOLEJULI,WHOLETIME from View_TBOM_CARAPLLRVW";
            }
            else
            {
                sqltext = "select CARNUM,WHOLEJULI,WHOLETIME from View_TBOM_CARAPLLRVW where " + WHERE.ToString();
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num.Text = dt.Rows.Count.ToString();

            decimal tot_money = 0;
            //TimeSpan tm1;
            TimeSpan tm2 = new TimeSpan(0, 0, 0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //DateTime1 = DateTime1.AddHours(DateTime2.Hour);
                //DateTime1 = DateTime1.AddMinutes(DateTime2.Minute);
                //DateTime1 = DateTime1.AddSeconds(DateTime2.Second);
                //return DateTime1;
                string je = dt.Rows[i]["WHOLETIME"].ToString();
                string length1 = je.Length.ToString();
                string h = je.Substring(0, Convert.ToInt32(length1) - 6).ToString();
                string m = je.Substring(Convert.ToInt32(length1) - 5, 2).ToString();
                string s = je.Substring(Convert.ToInt32(length1) - 2, 2).ToString();


                TimeSpan tm1 = new TimeSpan(Convert.ToInt32(h), Convert.ToInt32(m), Convert.ToInt32(s));
                tm2 += tm1;
            }
            string dd = tm2.Days.ToString();
            string hh = tm2.Hours.ToString();
            string mm = tm2.Minutes.ToString();
            string ss = tm2.Seconds.ToString();
            //if (dd == "")
            //{
            //    lb_oil.Text = Convert.ToString(tm2);
            //}
            //else
            //{
            hh = Convert.ToString(Convert.ToInt32(dd) * 24 + Convert.ToInt32(hh));
            string tm22 = hh + ':' + mm + ':' + ss;
            lb_oil.Text = Convert.ToString(tm22);
            //}

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string je = dt.Rows[i]["WHOLEJULI"].ToString() == "" ? "0" : dt.Rows[i]["WHOLEJULI"].ToString();
                tot_money += Convert.ToDecimal(je);


            }
            lb_select_money.Text = tot_money.ToString();

        }
        #endregion

    }
}
