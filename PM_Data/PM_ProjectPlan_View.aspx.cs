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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_ProjectPlan_View : System.Web.UI.Page
    {
        string id = "";
        string action = "";

        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["mnpid"].ToString();
            action = Request.QueryString["action"].ToString();
            if (!IsPostBack)
            {
                info();
                if (action == "view")
                {
                    btn_plan.Visible = false;
                }
                InitVarPager();
                UCPaging1.CurrentPage = 1;
                GetManutAssignData(); //数据绑定
            }
        }
        private void info()
        {
            string sqltext = "select MS_PJID,MS_ENGID,CM_JHTIME from TBPM_MSFORALLRVW as A left outer join View_CM_FaHuo as B on A.MS_ENGID=B.TSA_ID where MS_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            contact.Text = dt.Rows[0]["MS_PJID"].ToString();
            projid.Text = dt.Rows[0]["MS_ENGID"].ToString();
            pid.Text = id;
            endtime.Text = dt.Rows[0]["CM_JHTIME"].ToString();
        }
        protected void btn_plan_click(object sender, EventArgs e)
        {
            string sqltext = "";
            string PM_XIALIAO = "";
            string PM_JIJIA = "";
            string PM_JIEGOU = "";
            string PM_ZHUANGPEI = "";
            string PM_PENGQI = "";
            string PM_RUKU = "";
            string PM_NOTE = "";
            string MS_ID = "";
            foreach (RepeaterItem Reitem in pm_projectplan_view_repeater.Items)
            {

                PM_XIALIAO = ((TextBox)Reitem.FindControl("xialiaotime")).Text.ToString();
                PM_JIJIA = ((TextBox)Reitem.FindControl("jijiatime")).Text.ToString();
                PM_JIEGOU = ((TextBox)Reitem.FindControl("jiegoutime")).Text.ToString();
                PM_ZHUANGPEI = ((TextBox)Reitem.FindControl("zhuangpeitime")).Text.ToString();
                PM_PENGQI = ((TextBox)Reitem.FindControl("pengqitime")).Text.ToString();
                PM_RUKU = ((TextBox)Reitem.FindControl("rukutime")).Text.ToString();
                PM_NOTE = ((TextBox)Reitem.FindControl("txt_beizhu")).Text.Trim().ToString();
                MS_ID = ((Label)Reitem.FindControl("MS_ID")).Text.Trim().ToString();
                sqltext = "update  TBPM_PROJ_PLAN set PM_XIALIAO='" + PM_XIALIAO + "',PM_JIJIA='" + PM_JIJIA + "',PM_JIEGOU='" + PM_JIEGOU + "',PM_ZHUANGPEI='" + PM_ZHUANGPEI + "',PM_PENGQI='" + PM_PENGQI + "',PM_RUKU='" + PM_RUKU + "',PM_NOTE='" + PM_NOTE + "' where MS_ID='" + MS_ID + "'";
                DBCallCommon.ExeSqlText(sqltext);

            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('项目计划保存成功！');window.location.href='PM_ProjectPlan.aspx'", true);
        }
        private void InitVarPager()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager.TableName = "TBPM_PROJ_PLAN as A left outer join TBMP_TASKDQO as B on A.MS_ID=B.MS_ID";
            pager.PrimaryKey = "";
            pager.ShowFields = "A.MS_ID*1 as MS_ID,MS_TUHAO,MS_NAME,MS_UNUM,PM_XIALIAO,PM_JIJIA,PM_JIEGOU,PM_ZHUANGPEI,PM_PENGQI,PM_RUKU,PM_NOTE";
            pager.OrderField = "dbo.f_formatstr(MS_ZONGXU, '.')";
            pager.StrWhere = "MS_PID='" + id + "'AND MS_PLAN='1'";
            pager.OrderType = 0;//按任务名称降序排列
            pager.PageSize = 50;


        }
        private void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            GetManutAssignData();
        }
        protected void GetManutAssignData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, pm_projectplan_view_repeater, UCPaging1, NoDataPane1);
            if (NoDataPane1.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
    }
}
