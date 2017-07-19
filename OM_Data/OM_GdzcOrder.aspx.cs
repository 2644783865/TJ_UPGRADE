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
    public partial class OM_GdzcOrder : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
            }
        }
        protected void btnPush_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            string code = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    if (count == 0)
                    {
                        code = ((Label)Repeater1.Items[i].FindControl("lblCode")).Text;
                    }
                    else
                    {
                        if (((Label)Repeater1.Items[i].FindControl("lblCode")).Text != code)
                        {
                            string alert = "<script>alert('存在不同的订单，下推被阻止！')</script>";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                            return;//return: 表示语句跳转到函数末尾
                        }
                    }
                    count++;
                }
            }
            if (count < 1)
            {
                string alert = "<script>alert('请选择要下推的条目！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                return;
            }
            else
            {
                Response.Redirect("OM_GdzcOrderDetail.aspx?FLAG=PUSH&&ID=" + code + "");
            }
        }
        protected string showYg(string YgId)
        {
            return "javascript:window.showModalDialog('OM_GdzcOrderDetail.aspx?action=show&&CODE=" + YgId + "','','DialogWidth=700px;DialogHeight=650px')";
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
            pager.TableName = "View_TBOM_GDZCAPPLY";
            pager.PrimaryKey = "CODE";
            pager.ShowFields = "DISTINCT(CODE) AS CODE,AGENT,AGENTID,ADDTIME,STATUS,REASON,NAME,MODEL,NUM,DEPARTMENT,NOTE";
            pager.OrderField = "";
            pager.StrWhere = "STATUS=6";
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
