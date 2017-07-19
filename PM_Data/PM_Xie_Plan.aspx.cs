using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_Plan : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqltext;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PCON_BCODE = Request["PCON_BCODE"];
                if (!string.IsNullOrEmpty(PCON_BCODE))
                {
                    txt_engid.Text = PCON_BCODE.Trim();
                }
                DataPJname();
                this.InitPage();
            }
            UCPaging.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
        }
        private void InitPage()
        {

            UCPaging.CurrentPage = 1;
            this.InitPager();
            bindGrid();
        }
        private void InitPager()
        {

            pager.TableName = "(select A.MS_ENGNAME,A.MS_ENGID,A.MS_WXTYPE,A.CM_PROJ,ISNULL(A.WXNUM,0)as wxnum, ISNULL(B.BJNUM,0)as bjnum, isnull(convert(decimal(10,2), B.bjnum/convert(float, A.wxnum )*100),0)as jindu from (select count(*) as wxnum,MS_ENGID ,MS_ENGNAME,MS_WXTYPE,CM_PROJ from View_TM_WXDetail where MS_scwaixie<>'5' GROUP BY MS_ENGID ,MS_WXTYPE,CM_PROJ,MS_ENGNAME ) as A left join ( select PIC_ENGID, count(*) AS bjnum,ICL_WXTYPE from View_TBMP_IQRCMPPRICE_RVW1 where PIC_CFSTATE<>'2' and totalstate='4' group by PIC_ENGID,ICL_WXTYPE ) AS B  on (A.MS_ENGID=B.PIC_ENGID and A.MS_WXTYPE=B.ICL_WXTYPE) ) AS C LEFT JOIN (select count(*) as jsnum,TA_ENGID,TA_WXTYPE,sum(TA_MONEY) as js_money from VIEW_TBMP_ACCOUNTS group by TA_ENGID,TA_WXTYPE) AS D on ( C.MS_ENGID=D.TA_ENGID and C.MS_WXTYPE=D.TA_WXTYPE)";//未拆分和拆分之前
            pager.PrimaryKey = "";
            pager.ShowFields = "C.MS_ENGID,C.MS_WXTYPE,C.wxnum,C.CM_PROJ,C.MS_ENGNAME,C.BJNUM,JINDU,ISNULL(D.JSNUM,0)AS JSNUM , D.JS_MONEY ,isnull(convert(decimal(10,2), D.jsnum/convert(float, C.wxnum )*100),0)as js_jindu ";
            pager.OrderField = "MS_ENGID";
            pager.StrWhere = ConstrWhere();
            pager.OrderType = 1;//升序排列
            pager.PageSize = 20;
            UCPaging.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private string ConstrWhere()
        {
            string conStr = "1=1" ;
            if (ddl_proj.SelectedValue != "-请选择-")
            {
                conStr += " and CM_PROJ='"+ddl_proj.SelectedValue.ToString()+"'";
            }
            if (rbl_wxtype.SelectedIndex != 0)
            {
                conStr += " and MS_WXTYPE='"+rbl_wxtype.SelectedValue.Trim().ToString()+"'";
            }
            if (txt_engid.Text.Trim().ToString() != "")
            {
                conStr += "and MS_ENGID like '%" + txt_engid.Text.Trim().ToString() + "%'";
            }
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
        private void DataPJname()  //绑定项目
        {
            sqltext = "select distinct CM_PROJ from View_TM_WXDetail";
            string DataText = "CM_PROJ";
            string DataValue = "CM_PROJ";
            DBCallCommon.BindDdl(ddl_proj, sqltext, DataText, DataValue);
        }
        private void Pager_PageChangedMS(int pageNumber)
        {
            this.InitPager();
            bindGrid();
        }
        protected void rbl_wxtype_osic(object sender, EventArgs e)
        {
           // this.DataPJname();
            InitPage();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double bjjindu = Convert.ToDouble(e.Row.Cells[7].Text.ToString());//比价进度
                double jsjindu = Convert.ToDouble(e.Row.Cells[9].Text.ToString());//结算进度
                string proj = e.Row.Cells[1].Text;//项目名称
                string taskId = e.Row.Cells[2].Text;//任务号
                string style = e.Row.Cells[4].Text;//外协类型
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看具体进度情况");
                e.Row.Attributes.Add("ondblclick", "Show('" + taskId + "','" + style + "','"+proj+"')");
                if (bjjindu >= 100.00)
                {
                    e.Row.Cells[7].Text += "%";
                    e.Row.Cells[7].Font.Bold = true;
                    e.Row.Cells[7].BackColor = Color.LawnGreen;
                }
                else if (bjjindu < 100.0 && bjjindu >=80)
                {
                    e.Row.Cells[7].Text += "%";
                    e.Row.Cells[7].Font.Bold = true;
                    e.Row.Cells[7].BackColor = Color.LightBlue;
                }
                else if (bjjindu < 80.0 && bjjindu >=60)
                {
                    e.Row.Cells[7].Text += "%";
                    e.Row.Cells[7].Font.Bold = true;
                    e.Row.Cells[7].BackColor = Color.Yellow;
                }
                else if (bjjindu < 60.0 && bjjindu >= 0)
                {
                    e.Row.Cells[7].Text += "%";
                    e.Row.Cells[7].Font.Bold = true;
                    e.Row.Cells[7].BackColor = Color.OrangeRed;
                }
                if (jsjindu >= 100.00)
                {
                    e.Row.Cells[9].Text += "%";
                    e.Row.Cells[9].Font.Bold = true;
                    e.Row.Cells[9].BackColor = Color.LawnGreen;
                }
                else if (jsjindu < 100.0 && jsjindu >= 80)
                {
                    e.Row.Cells[9].Text += "%";
                    e.Row.Cells[9].Font.Bold = true;
                    e.Row.Cells[9].BackColor = Color.LightBlue;
                }
                else if (jsjindu < 80.0 && jsjindu >= 60)
                {
                    e.Row.Cells[9].Text += "%";
                    e.Row.Cells[9].Font.Bold = true;
                    e.Row.Cells[9].BackColor = Color.Yellow;
                }
                else if (jsjindu < 60.0 && jsjindu >= 0)
                {
                    e.Row.Cells[9].Text += "%";
                    e.Row.Cells[9].Font.Bold = true;
                    e.Row.Cells[9].BackColor = Color.OrangeRed;
                }
            }
        }
    }
}