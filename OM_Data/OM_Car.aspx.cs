using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_Car : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
            }
            CheckUser(ControlFinder);
        }

        protected void rblstate_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBoundData();
        }
        protected void grid_databound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["UserID"].ToString() != "3" && Session["POSITION"].ToString() != "0202")
                {

                    e.Row.Cells[12].Visible = false;

                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;
                    e.Row.Cells[16].Visible = false;
                    e.Row.Cells[17].Visible = false;
                    hlAdd.Visible = false;
                }
                string carNum = e.Row.Cells[2].Text;
                string time = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

                string sql = "select count(1) from (select a.CARNUM,BXNAME,STARTDATE,ENDDATE,IsDel,ROW_NUMBER() OVER(PARTITION by a.CARNUM,BXNAME ORDER BY STARTDATE DESC) AS rownum  from TBOM_CARBX as a left join TBOM_CARINFO as b on a.CARNUM=b.CARNUM)c where IsDel='正常' and rownum<=1 and ENDDATE<='" + time + "' and CARNUM='" + carNum + "'";

             
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    e.Row.Cells[11].BackColor = Color.Red;
                    e.Row.Cells[11].Text = "该车需要补交保险";
                }


                string sqlWH = "select count(1) from TBOM_CARINFO as a left join (select CARID, cast(isnull(BYAFTER,0) as float)-cast(isnull(BYYJ,0) as float) as YJGL,PLACEDATE,ROW_NUMBER() OVER(PARTITION by CARID ORDER BY PLACEDATE DESC) AS rownum from TBOM_CARSAFE where TYPEID='by' group by CARID,BYAFTER,BYYJ,PLACEDATE)b on a.CARNUM=b.CARID where b.rowNUM<=1 and MILEAGE>YJGL  and a.CARNUM='" + carNum + "' and IsDel='正常'";
                DataTable dtWH = DBCallCommon.GetDTUsingSqlText(sqlWH);
                if (dtWH.Rows[0][0].ToString() != "0")
                {
                    e.Row.Cells[10].BackColor = Color.Red;
                    e.Row.Cells[10].Text = "该车需要保养";
                }
            }
        }

        private string GetWhere()
        {
            string state = rblstate.SelectedValue.ToString();
            string strWhere = string.Empty;
            if (state != "")
            {
                strWhere = " STATE='" + state + "'";
            }
            else
            {
                strWhere = " 0=0";
            }
            return strWhere;
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
            pager.TableName = "TBOM_CARINFO";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID*1 as ID,CARNUM,CARTYPE,CARCAPACITY,MILEAGE,OIL,COLOR,NOTE,STATE,CARD,CARDYE,FZR,IsDel";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 0;
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
        //protected void link_change(object sender, EventArgs e)
        //{
        //    string id = ((LinkButton)sender).CommandArgument.ToString();
        //    if (((LinkButton)sender).CommandName == "back")
        //    {
        //        string sqltext = "update  TBOM_CARINFO set STATE=0 where ID='" + id + "'";
        //        DBCallCommon.ExeSqlText(sqltext);
        //        //string ss = "select CARNUM from TBOM_CARINFO where ID='" + id + "'";
        //        //DataTable dt = DBCallCommon.GetDTUsingSqlText(ss);
        //        //if(dt.Rows.Count>0)
        //        //{
        //        //string sql = "update TBOM_CARAPPLY set TIME2='"+DateTime.Today.ToString()+"' where ";
        //        //}
        //        this.GetBoundData();
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:车辆回厂成功！！！');", true);
        //    }
        //}

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete from TBOM_CARINFO where ID='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.GetBoundData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
            }
        }
    }
}
