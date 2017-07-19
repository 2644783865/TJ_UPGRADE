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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_TOOL_LIST : System.Web.UI.Page
    {
        string flag;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] != null)
            { flag = Request.QueryString["FLAG"].ToString(); }
            InitVar();
            if (!IsPostBack)
            {
                if (flag == "ToStore")
                {
                    btnPush.Visible = true;
                }
                GetBoundData();
            }
        }
        private string GetWhere()
        {
            string strWhere = string.Empty;
            if (ddltooltype.SelectedValue.ToString() != "%")
            {
                strWhere = " TYPE = '"+ddltooltype.SelectedValue.ToString()+"'";
            }
            return strWhere;
        }
        protected void ddltooltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();

        }
        protected void btnPush_OnClick(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            string sql = "UPDATE TBMP_TOOL_RESTORE SET STATE=''";
            sqllist.Add(sql);
            string name = "";
            string model = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    name = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
                    model = ((Label)Repeater1.Items[i].FindControl("lblModel")).Text;
                    sql = "UPDATE TBMP_TOOL_RESTORE SET STATE='1' WHERE NAME='" + name + "' AND MODEL='" + model + "'";//到领刀具单界面
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count < 2)
            {
                string alert = "<script>alert('请选择下推条目！！！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                return;
            }
            DBCallCommon.ExecuteTrans(sqllist);
            Response.Redirect("PM_TOOL_OUTBILL.aspx?FLAG=PUSH");
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
            pager.TableName = "TBMP_TOOL_RESTORE";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 0;
            pager.PageSize = 20;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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
