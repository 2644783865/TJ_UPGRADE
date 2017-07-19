using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_Dor : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
                ControlVisible();
            }
            CheckUser(ControlFinder);
        }

        private void ControlVisible()
        {
            string state = rblstate.SelectedValue.ToString();

            GridView1.Columns[10].Visible = false;
            GridView1.Columns[11].Visible = false;
            GridView1.Columns[12].Visible = false;
            if (state == "0")
            {
                GridView1.Columns[10].Visible = true;
                GridView1.Columns[12].Visible = true;
            }
            if (state=="5")
            {
                GridView1.Columns[11].Visible = true;
            }
        }
        private string GetWhere()
        {
            string state = rblstate.SelectedValue.ToString();
            string strWhere = string.Empty;
            if (state == "0")
            {
                strWhere = " DORSTATE='0'";
            }
            else if (state == "1")
            {
                strWhere = " DORSTATE='1'";
              
            }
            else if (state == "2")
            {
                strWhere = " DORSTATE='2'";
            }
            else if (state == "3")
            {
                strWhere = " DORSTATE='3'";
            }
            else if (state == "4")
            {
                strWhere = " DORSTATE='4'";
            }
            else if (state=="5")
            {
                strWhere = " (DORSTATE='0' and MP_REVIEWA='" + Session["UserName"].ToString() + "') or (DORSTATE='1' and MP_REVIEWB='" + Session["UserName"].ToString() + "') ";
            }
            else
            {
                strWhere = " 1=1 ";
            } 
            return strWhere;
        }
        protected void rblstate_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetBoundData();
            ControlVisible();
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "";
                List<string> list_sql = new List<string>();
                sqltext = "delete from TBOM_DORAPPLY where DORCODE='" + id + "'";
                list_sql.Add(sqltext);
                sqltext = "delete from TBOM_DORRVW where DORCODE='" + id + "'";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                this.GetBoundData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
            }
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
            pager.TableName = "View_TBOM_DORAPPLY";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID*1 as ID,DORCODE,DORNAME,DORWNO,DORDEP,DORPOS,DORROOM,DORADDTIME,DORSTATE";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 0;
            pager.PageSize = 5;
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
    }
}
