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
    public partial class TM_Packing_List_View : System.Web.UI.Page
    {
        string pk_id;
        string sqlText;
        string fields;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitParameter();
            InitVar();
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetTechPkData();
        }
        //初始化参数
        private void InitParameter()
        {
            pk_id = Request.QueryString["packlist_id"];
            #region
            if (rblstatus.SelectedItem.Text.Trim() == "未提交")
            {
                sqlText = "PLT_ENGID='" + pk_id + "' and PLT_STATE in ('0','1') ";
            }
            else if (rblstatus.SelectedItem.Text.Trim() == "审核中")
            {
                sqlText = "PLT_ENGID='" + pk_id + "' and PLT_STATE in ('4','6')";
            }
            else if (rblstatus.SelectedItem.Text.Trim() == "驳回")
            {
                sqlText = "PLT_ENGID='" + pk_id + "' and PLT_STATE in ('3','5','7')";
            }
            else
            {
                sqlText = "PLT_ENGID='" + pk_id + "' and PLT_STATE='" + rblstatus.SelectedValue + "'";
            }
            #endregion
            fields = " PLT_PACKLISTNO,PLT_PJNAME,PLT_ENGNAME,PLT_SUBMITTM,PLT_ADATE,PLT_STATE,PLT_PACKLISTNO+'.'+PLT_STATE as PLT_ID  ";
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "TBPM_PACKLISTTOTAL";
            pager.PrimaryKey = "PLT_PACKLISTNO";
            pager.ShowFields = fields;
            pager.OrderField = "PLT_PACKLISTNO";
            pager.StrWhere = sqlText;
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 10;
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            GetTechPkData();
        }

        protected void GetTechPkData()
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
            GetChangeData();
        }
        //判断是否变更
        private void GetChangeData()
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                string status = ((Label)gr.FindControl("lab_state")).Text;
                if (status == "驳回")
                {
                    ((HyperLink)gr.FindControl("hlEdit")).Enabled = true;
                }
                if (status == "通过" || status == "已下推")
                {
                    ((HyperLink)gr.FindControl("hlPlan")).Enabled = true;
                }
            }
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTechPkData();
        }
    }
}
