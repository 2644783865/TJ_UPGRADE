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

namespace ZCZJ_DPF.ESM_Data
{
    public partial class EQU_Part_Store : System.Web.UI.Page
    {
        string flag;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] != null)
            flag = Request.QueryString["FLAG"].ToString();
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
            if (txtName.Text.Trim() == "")
            {
                strWhere = " 0=0";
            }
            else if (txtName.Text.Trim()!="")
            {
                strWhere = " ParName like '%" + txtName.Text.Trim() + "%'";
            }
            return strWhere;
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtName.Text = "";
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        protected void btnPush_OnClick(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            string sql = "UPDATE EQU_Part_Store SET State=''";
            sqllist.Add(sql);
            string name = "";
            string Type = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    name = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
                    Type = ((Label)Repeater1.Items[i].FindControl("lblType")).Text;
                    sql = "UPDATE EQU_Part_Store SET State='1' WHERE ParName='" + name + "' AND ParType='" + Type + "'";//到备件领料单界面
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
            Response.Redirect("EQU_Part_Outbill.aspx?FLAG=PUSH");
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
            pager.TableName = "EQU_Part_Store";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "Id*1 as Id,ParName,ParType,ParnumSto";
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
