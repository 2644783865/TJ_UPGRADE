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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_YULIAO_LIST : System.Web.UI.Page
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

            return strWhere;
        }
        protected void ddlyuliaotype_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();

        }
        protected void btnPush_OnClick(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            string sql = "UPDATE TBWS_YULIAO_RESTORE SET STATE=''";
            sqllist.Add(sql);
            string Id = "";

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {

                    Id = ((HtmlInputHidden)Repeater1.Items[i].FindControl("hidId")).Value;
                    sql = "UPDATE TBWS_YULIAO_RESTORE SET STATE='1' WHERE Id='" + Id + "'";//到领余料单界面
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
            Response.Redirect("SM_YULIAO_OUTBILL.aspx?FLAG=PUSH");
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
            pager.TableName = "TBWS_YULIAO_RESTORE as a left join TBMA_MATERIAL as b on a.Marid=b.ID ";
            pager.PrimaryKey = "a.ID";
            pager.ShowFields = "a.*,b.CAIZHI,b.GUIGE,b.MNAME as Name";
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
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
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
