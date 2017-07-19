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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class BD_ContractReview : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitVar();
            if (!IsPostBack)
            {
                this.bindGrid();
                this.BindData("");
            }
            CheckUser(ControlFinder);
        }
        //数据绑定
        private void BindData(string type)
        {
            string sqltext = "select * from POWER_REVIEW where REV_CATEGORY like '" + type + "' order by REV_CATEGORY,REV_RANK,REV_PERID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                grvPS.DataSource = dt;
                grvPS.DataBind();
                palNoData.Visible = false;
            }
            else
            {
                grvPS.DataSource = null;
                grvPS.DataBind();
                palNoData.Visible = true;
            }
        }
        protected void dplPSHTLB_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplPSHTLB.SelectedIndex != 0)
            {
                this.BindData(dplPSHTLB.SelectedValue.ToString());
            }
            else
            {
                grvPS.DataSource = null;
                grvPS.DataBind();
                palNoData.Visible = true;
            }
        }
        //添加合同评审人员信息
        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            if (dplPSHTLB_Select.SelectedIndex == 0)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('请选择评审合同类别！！！');", true);
            }
            else
            {
                Response.Write("<script>javascript:window.showModalDialog('BD_ContractReview_Add.aspx?Action=Add&Type=" + dplPSHTLB_Select.SelectedValue.ToString() +"','','DialogWidth=800px;DialogHeight=300px');</script>");
            }
        }
        //删除
        protected void grvPS_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string revID = e.CommandArgument.ToString();
                string sqltext = "delete from POWER_REVIEW where REV_ID='" + revID + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.BindData(dplPSHTLB.SelectedValue.ToString());
            }
        }

        #region "数据查询，分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "TBCM_HT_SETTING AS A,TBDS_STAFFINFO AS B";
            pager.PrimaryKey = "ID*2";
            pager.ShowFields = "A.ID,A.DEP_NAME,B.ST_NAME";
            pager.OrderField = "DEP_NAME";
            pager.StrWhere = " A.PER_ID=B.ST_CODE";
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 10;

        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GRV_HT_Set, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                CheckUser(ControlFinder);
            }

        }
        #endregion

        //删除记录
        protected void lnkDel_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();

            string sql_del = "delete from TBCM_HT_SETTING where ID='" + id + "'";
            DBCallCommon.ExeSqlText(sql_del);
            this.bindGrid();
            this.BindData("");
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('删除成功');", true);

        }
        protected string editHt(string category,string HtId)
        {
            return "javascript:window.showModalDialog('BD_ContractReview_Add.aspx?Action=Edit&Type=" + category + "&ID=" + HtId + "','','DialogWidth=800px;DialogHeight=300px')";
        }

    }
}
