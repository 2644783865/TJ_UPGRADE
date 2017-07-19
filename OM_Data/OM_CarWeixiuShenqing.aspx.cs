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
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarWeixiuShenqing : BasicPage
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        PagerQueryParam pager2 = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            InitVar2();
            if (!IsPostBack)
            {
               
                GetBoundData();
              
                GetBoundData2();
           
              
                gridview1.Columns[0].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[0].ItemStyle.CssClass = "fixed";
                gridview1.Columns[1].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[1].ItemStyle.CssClass = "fixed";
                gridview1.Columns[2].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[2].ItemStyle.CssClass = "fixed";
                gridview1.Columns[3].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[3].ItemStyle.CssClass = "fixed";
                gridview1.Columns[4].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[4].ItemStyle.CssClass = "fixed";

                gridview2.Columns[0].HeaderStyle.CssClass = "fixed";
                gridview2.Columns[0].ItemStyle.CssClass = "fixed";
                gridview2.Columns[1].HeaderStyle.CssClass = "fixed";
                gridview2.Columns[1].ItemStyle.CssClass = "fixed";
                gridview2.Columns[2].HeaderStyle.CssClass = "fixed";
                gridview2.Columns[2].ItemStyle.CssClass = "fixed";
                gridview2.Columns[3].HeaderStyle.CssClass = "fixed";
                gridview2.Columns[3].ItemStyle.CssClass = "fixed";
                gridview2.Columns[4].HeaderStyle.CssClass = "fixed";
                gridview2.Columns[4].ItemStyle.CssClass = "fixed";
            }
            CheckUser(ControlFinder);
        }
        #region 分页1
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBoundData();
        }
        protected void gridview1_change(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = ((Label)e.Row.FindControl("lblCode")).Text.Trim();
                e.Row.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=view&type=wx&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看详细信息！");

                Label applyerID = (Label)e.Row.FindControl("lblApplyerID");
                //string code=((Label)e.Row.FindControl("lblCode")).ToString();
                string sql="select STATE FROM TBOM_CARWXSQ WHERE CODE='"+code+"'";
                DataTable DT=DBCallCommon.GetDTUsingSqlText(sql);
                if(DT.Rows.Count>0)
                {
                    if ((DT.Rows[0]["STATE"].ToString() == "1" || DT.Rows[0]["STATE"].ToString() == "3") && applyerID.Text.Trim() == Session["UserID"].ToString())
                    {
                        ((HyperLink)e.Row.FindControl("hplmod")).Visible = true;

                    }
                }
            }
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete from TBOM_CARWXSQ where CODE='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.GetBoundData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
            }
        }
        private string GetSqlWhere()
        {

            string sqlwhere = "TYPEID='wx'";
            if (rblstatus.SelectedValue == "0") //全部
            {
                sqlwhere += "and 0=0";
            }
            else if (rblstatus.SelectedValue == "1") //审核中
            {
                sqlwhere += "and STATE in ('0','2')";

            }
            else if (rblstatus.SelectedValue == "2") //驳回
            {
                sqlwhere += "and STATE in ('1','3')";
            }
            else if (rblstatus.SelectedValue == "3")
            {
                sqlwhere += "and STATE='4'";
            }
            else if (rblstatus.SelectedValue=="4")
            {
                sqlwhere += " and ((STATE='0' and MANAGERID='" + Session["UserId"].ToString() + "') or ( STATE='2' and CONTROLLERID='" + Session["UserId"].ToString() + "'))";
            }
            return sqlwhere;
        }


        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "TBOM_CARWXSQ";
            pager.PrimaryKey = "CODE";
            pager.ShowFields = "CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,";
            pager.ShowFields += "GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,";
            pager.ShowFields += "MANAGERNAME,MANAGERID,STATE,CONTROLLERNAME,CONTROLLERID,CARNAME,CARID";
            pager.OrderField = "CODE";
            pager.StrWhere = GetSqlWhere();
            pager.OrderType = 1;//按任务名称升序排列
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
            CommonFun.Paging(dt, gridview1, UCPaging1, NoDataPanel);
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


        #region 分页2
        protected void rblstatus2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBoundData2();
        }
        protected void gridview2_change(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = ((Label)e.Row.FindControl("lblCode2")).Text.Trim();
                e.Row.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=view&type=by&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看详细信息！");

                Label applyerID = (Label)e.Row.FindControl("lblApplyerID2");
                //string code = ((Label)e.Row.FindControl("lblCode2")).ToString();
                string sql = "select STATE FROM TBOM_CARWXSQ WHERE CODE='" + code + "'";
                DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
                if (DT.Rows.Count > 0)
                {
                    if ((DT.Rows[0]["STATE"].ToString() == "1" || DT.Rows[0]["STATE"].ToString() == "3") && applyerID.Text.Trim() == Session["UserID"].ToString())
                    {
                        ((HyperLink)e.Row.FindControl("hplmod2")).Visible = true;

                    }
                }
            }
        }
        protected void lnkDelete2_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete from TBOM_CARWXSQ where CODE='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.GetBoundData2();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
            }
        }
        private string GetSqlWhere2()
        {

            string sqlwhere = "TYPEID='by'";
            if (rblstatus2.SelectedValue == "0") //全部
            {
                sqlwhere += " and 0=0";
            }
            else if (rblstatus2.SelectedValue == "1") //审核中
            {
                sqlwhere += " and STATE in ('0','2')";

            }
            else if (rblstatus2.SelectedValue == "2") //驳回
            {
                sqlwhere += "and STATE in ('1','3')";
            }
            else if (rblstatus2.SelectedValue == "3")
            {
                sqlwhere += "and STATE='4'";
            }
            else if (rblstatus2.SelectedValue=="4")
            {
                sqlwhere += " and ((STATE='0' and MANAGERID='" + Session["UserId"].ToString() + "') or ( STATE='2' and CONTROLLERID='" + Session["UserId"].ToString() + "'))";
            }
            return sqlwhere;
        }


        private void InitVar2()
        {
            InitPager2();
            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged2);
            UCPaging2.PageSize = pager2.PageSize;
        }
        private void InitPager2()
        {
            pager2.TableName = "TBOM_CARWXSQ";
            pager2.PrimaryKey = "CODE";
            pager2.ShowFields = "CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,";
            pager2.ShowFields += "GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,";
            pager2.ShowFields += "MANAGERNAME,MANAGERID,STATE,CONTROLLERNAME,CONTROLLERID,CARNAME,CARID";
            pager2.OrderField = "CODE";
            pager2.StrWhere = GetSqlWhere2();
            pager2.OrderType = 1;//按任务名称升序排列
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
        }
  
        #endregion
    }
}
