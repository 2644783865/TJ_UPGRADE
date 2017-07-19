using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_WorkLoad : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                dataBind();
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            dataBind();
        }
        private void dataBind()
        {
            pager.TableName = "View_TM_TaskAssign as a left join  (select MS_ENGID,MS_MAP,MS_CHILDENGNAME,MS_SUBMITTM,ROW_NUMBER() over(partition by MS_ENGID,MS_MAP order by MS_SUBMITTM desc) as rows from TBPM_MSFORALLRVW) as b  on a.TSA_ID=b.MS_ENGID";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "TSA_ID,TSA_PJID,CM_PROJ,TSA_ENGNAME,TSA_CONTYPE,TSA_TCCLERKNM,TSA_REVIEWER,TSA_STARTDATE,MS_SUBMITTM,rows,MS_MAP,MS_CHILDENGNAME";
            pager.OrderField = ddlSort.SelectedValue;
            pager.StrWhere = CreateWhere();
            pager.OrderType = 0;
            pager.PageSize = 20;
            pager.PageIndex = UCPaging1.CurrentPage;
            UCPaging1.PageSize = pager.PageSize;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            //dt.Columns.Add("EndTime");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string taskId = dt.Rows[i]["TSA_ID"];
            //    string sql = "select MS_SUBMITTM from TBPM_MSFORALLRVW where MS_ENGID='"+taskId+"' order by MS_SUBMITTM";
            //    DataTable dtTime = DBCallCommon.GetDTUsingSqlText(sql);
            //    if (dtTime.Rows.Count>0)
            //    {

            //    }
            //}
            CommonFun.Paging(dt, gr, UCPaging1, NoDataPanel);
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
        private string CreateWhere()
        {
            string where = "rows<=1";

            where += " and  TSA_STATE='" + rblstatus.SelectedValue + "' ";
            if (ddlSearch.SelectedItem.Text.Trim() != "-请选择-")
            {
                where += " and " + ddlSearch.SelectedValue.Trim() + "  like '%" + txtSearch.Text.Trim() + "%'";
            }
            if (txtRecTaskDateStart.Text != "")
            {
                where += " and TSA_STARTDATE >='" + txtRecTaskDateStart.Text + "'";
            }
            if (txtRecTaskDateEnd.Text != "")
            {
                where += " and TSA_STARTDATE <='" + txtRecTaskDateEnd.Text + "'";
            }
            if (txtCompleteStart.Text != "")
            {
                where += " and MS_SUBMITTM >='" + txtCompleteStart.Text + "'";
            }
            if (txtCompleteEnd.Text != "")
            {
                where += " and MS_SUBMITTM <='" + txtCompleteEnd.Text + "'";
            }

            return where;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            dataBind();
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            dataBind();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string proId = e.Row.Cells[2].Text.Trim();

                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看数据");
                e.Row.Attributes.Add("ondblclick", "ShowOrg('" + proId + "')");
            }

            //for (int i = 1; i < 15; i++)
            //{
            //    e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
            //    e.Row.Cells[i].Attributes.Add("title", "双击修改原始数据");


            //}
        }
    }
}
