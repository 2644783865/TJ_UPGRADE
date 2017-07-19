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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Design_Bom : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询
                this.FromMyTast();
            }
            InitVar();

        }
        /// <summary>
        /// 从我的任务跳转
        /// </summary>
        protected void FromMyTast()
        {
            if (Request.QueryString["xm_gc"] != null)
            {
                string xm_gc = Server.HtmlDecode(Request.QueryString["xm_gc"].ToString());
                string[] array_xm_gc = xm_gc.Split('_');
                btnQuery_OnClick(null, null);
            }
        }

        #region 分页
        private void InitVar()
        {
            if (Session["UserDeptID"].ToString() == "06")
            {
                btn_xiatui.Visible = true;
            }
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "VWPM_PROSTRC";
            pager.PrimaryKey = "cast(BM_ID as varchar(50))+BM_ENGID";
            pager.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER";
            pager.OrderField = "dbo.f_formatstr(BM_ZONGXU, '.')";
            pager.StrWhere = GetSiftData();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 150;
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



        /// <summary>
        /// 按项目、工程名称查询(获取查询条件)
        /// </summary>
        /// <returns></returns>
        private string GetSiftData()
        {
            string str = " 1=1 ";
            //项目工程
            if (txtPJId.Text.Trim() != "")
            {
                str = " BM_PJID like ='%" + txtPJId.Text + "%' ";
            }
            if (txtEngId.Text.Trim() != "")
            {
                str += " and BM_ENGID like '%" + txtEngId.Text.Trim() + "%'";
            }
            //常规查询条件

            if (txtName.Text.Trim() != "")
            {
                str += " and  BM_CHANAME like '%" + txtName.Text.Trim() + "%'";
            }

            if (txtTuhao.Text.Trim() != "")
            {
                str += " and BM_TUHAO='" + txtTuhao.Text.Trim() + "' ";
            }
            if (ddlOrgState.SelectedValue != "0")
            {
                str += " and BM_MASHAPE='" + ddlOrgState.SelectedValue + "' ";
            }
            if (txtGuige.Text.Trim() != "")
            {
                str += " and BM_MAGUIGE='" + txtGuige.Text.Trim() + "' ";
            }

            //自定义查询条件
            if (txtQueryContent.Text.Trim() != "")
            {
                if (ddlQueryType.SelectedValue == "BM_ZONGXU")
                {
                    str += " and (" + ddlQueryType.SelectedValue + "='" + txtQueryContent.Text.Trim() + "' or " + ddlQueryType.SelectedValue + " like '" + txtQueryContent.Text.Trim() + ".%')";
                }
                else
                {
                    str += " and " + ddlQueryType.SelectedValue + " like '%" + txtQueryContent.Text.Trim() + "%'";
                }
            }
            return str;
        }


        /// <summary>
        /// 自定义查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitPager();
            GetBoundData();
        }
        protected void btn_xiatui_OnClick(object sender, EventArgs e)
        {
            //int j = 0;
            //string BJname="";
            //string PJID = ddlProName.SelectedValue.ToString();
            //string ENGID = ddlEngName.SelectedValue.ToString();
            //string sqltext = "";
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    GridViewRow gr = GridView1.Rows[i];
            //    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
            //    if (cbk.Checked)
            //    {
            //        j++;
            //    }
            //}
            //if (j == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择构件！！！');", true);
            //}
            //else
            //{
            //    for (int i = 0; i < GridView1.Rows.Count; i++)
            //    {
            //        GridViewRow gr = GridView1.Rows[i];
            //        System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
            //        if (cbk.Checked)
            //        {
            //            BJname = gr.Cells[6].Text.ToString();
            //            sqltext = "insert into TBPC_KEYCOMTZ (KC_PJID,KC_ENGID,KC_GJNM) values('" + PJID + "','" + ENGID + "','" + BJname + "')";
            //            DBCallCommon.ExeSqlText(sqltext);
            //        }
            //    }
            //    Response.Redirect("~/PC_Data/PC_TBPC_GJBJTZ.aspx");
            //}

        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_OnClick(object sender, EventArgs e)
        {

            //自定义
            ddlQueryType.SelectedIndex = 0;
            txtQueryContent.Text = "";
            //常规


            txtEngId.Text = "";
            txtPJId.Text = "";
            txtName.Text = "";
            txtGuige.Text = "";
            ddlOrgState.SelectedIndex = 0;
            UCPaging1.CurrentPage = 1;
            InitPager();
            GetBoundData();
        }

        /// <summary>
        /// 导出原始数据Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnExport_OnClick(object sender, EventArgs e)
        {

        }
    }
}
