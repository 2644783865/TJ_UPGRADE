using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_PlanDetail : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string style;
        string taskId;
        string proj;
        protected void Page_Load(object sender, EventArgs e)
        {
            taskId = Request.QueryString["Id"];
            style = Request.QueryString["style"];
            proj = Request.QueryString["proj"];
            if (!IsPostBack)
            {
                this.InitPage();
                lab_engid.Text = taskId;
                lab_proj.Text = proj;
            }
            UCPaging.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
        }
        public string get_js(string i)
        {
            string statestr = "";
            if (i != "")
            {
                statestr = "<span style='color:red'>是</span>";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        private void InitPage()
        {

            UCPaging.CurrentPage = 1;
            this.InitPager();
            bindGrid();
        }
        private void InitPager()
        {
            pager.TableName = "(select A.MS_ENGID,A.MS_CODE, A.MS_TUHAO,A.MS_ZONGXU,A.MS_NAME,MS_GUIGE,MS_CAIZHI,A.MS_UWGHT,MS_NUM,A.MS_PROCESS,MS_WXTYPE,MS_XHBZ,picno,PIC_JGNUM,price,zdrnm,supplierresnm,PIC_SUPPLYTIME,pic_bjstatus,B.TA_TOTALNOTE ,B.PTC,B.marzxnum from  (select * from  TBPM_WXDETAIL where MS_scwaixie<>'5') as A   left join (select * from View_TBMP_IQRCMPPRICE_RVW1 where PIC_CFSTATE<>'1' and totalstate='4') as B on A.MS_CODE=B.PIC_CODE) as C LEFT JOIN (select PTC,BJSJ,rn from (select *,row_number() over(partition by PTC order by ISAGAIN) as rn from View_TBQM_APLYFORITEM) as a where rn<=1 ) AS D ON C.PTC=D.PTC";//未拆分和拆分之后的
            pager.PrimaryKey = "";
            pager.ShowFields = "C.MS_CODE,C.MS_ENGID, C.MS_TUHAO,C.MS_ZONGXU,C.MS_NAME,C.MS_GUIGE,C.MS_CAIZHI,C.MS_UWGHT,C.MS_NUM,C.MS_PROCESS,C.MS_WXTYPE,C.MS_XHBZ,C.picno,C.PIC_JGNUM,C.price,C.zdrnm,C.supplierresnm,C.PIC_SUPPLYTIME,C.pic_bjstatus,C.TA_TOTALNOTE ,C.PTC,D.BJSJ,C.marzxnum";
            pager.OrderField = "dbo.f_formatstr(MS_ZONGXU, '.')";
            pager.StrWhere = ConstrWhere();
            pager.OrderType = 0;//升序排列
            pager.PageSize = 20;
            UCPaging.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private string ConstrWhere()
        {
            string conStr = "MS_ENGID='"+taskId+"'and MS_WXTYPE='"+style+"'";
            return conStr;
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, GridView1, UCPaging, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging.Visible = false;
            }
            else
            {
                UCPaging.Visible = true;
                UCPaging.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        private void Pager_PageChangedMS(int pageNumber)
        {
            this.InitPager();
            bindGrid();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string m="";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 4; i <= 19; i++)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {
                        e.Row.Cells[i].Text = "--";
                    }
                }
                for (int j=0; j <= GridView1.Rows.Count - 1; j++)
                {
                    string mscode = ((Label)GridView1.Rows[j].FindControl("MS_CODE")).Text;
                    if (j == 0)
                    {
                        m = mscode;
                    }
                    else
                    {
                        if (m == mscode)
                        {
                            for (int i = 1; i <= 7; i++)
                            {
                                GridView1.Rows[j].Cells[i].Text = "";
                            }
                            for (int i = 9; i <= 11; i++)
                            {
                                GridView1.Rows[j].Cells[i].Text = "";
                            }
                        }
                        else
                        {
                            m = mscode;
                        }
                    }
                }
            }
        }

        protected void btn_shangcha_onclick(object sender, EventArgs e)
        {
            int i = 0;
            string ms_code="";
            string sqltext;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("cb");
                if (cb.Checked)
                {
                    i++;
                     ms_code = ((Label)gvr.FindControl("MS_CODE")).Text;
                }
            }
            if (i == 1)
            {
                sqltext = "select MS_WSID from TBPM_WXDETAIL where MS_CODE='" + ms_code + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    string msid = dt.Rows[0]["MS_WSID"].ToString();
                    Response.Redirect("PM_Xie_Audit.aspx?action=view&id=" + msid + "");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据或选择多条数据,本次操作无效！');", true);
            }
        }

        protected void btn_xiacha_onclick(object sender, EventArgs e)
        {
            int i = 0;
            string ms_code = "";
            string sqltext;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("cb");
                if (cb.Checked)
                {
                    i++;
                    ms_code = ((Label)gvr.FindControl("MS_CODE")).Text;
                }
            }
            if (i == 1)
            {
                sqltext = "select PIC_SHEETNO from TBMP_IQRCMPPRICE where PIC_CODE='" + ms_code + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    string msid = dt.Rows[0]["PIC_SHEETNO"].ToString();
                    Response.Redirect("PM_Xie_check_detail.aspx?sheetno=" + msid + "");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该数据没有相关比价单信息！');", true);
                
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据或选择多条数据,本次操作无效！');", true);
            }
        }


    }
}