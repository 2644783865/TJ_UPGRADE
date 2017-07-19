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
    public partial class TM_Paint_Task : System.Web.UI.Page
    {
        string sqlText = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
                this.GetProNameData();
            }
            this.InitVar();
            
        }
        private string GetMyTast()
        {
            string sql = "";
            sql += " TSA_YQHZR ='" + Session["UserName"] + "' ";


            if (rblstatus.SelectedIndex == 0)
            {
                sql += " and TSA_STATE in(" + rblstatus.SelectedValue + ")";
            }
            else
            {
                sql += " and TSA_STATE='" + rblstatus.SelectedValue + "'";
            }
            //项目
            if (ddlProName.SelectedIndex != 0 && ddlProName.SelectedIndex != -1)
            {
                sql += " and TSA_PJID='" + ddlProName.SelectedValue.ToString() + "'";
            }
            //工程
            if (ddlEngName.SelectedIndex != 0 && ddlEngName.SelectedIndex != -1)
            {
                sql += " and TSA_ID='" + ddlEngName.SelectedValue.ToString() + "' OR TSA_ID LIKE '" + ddlEngName.SelectedValue + "-%'";
            }
            return sql;
        }


        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        //protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string sczh = e.Row.Cells[1].Text.Trim();

        //        if (e.Row.Cells[1].Text.Contains("-"))
        //        {
        //            ((HyperLink)e.Row.FindControl("hlTask1")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask2")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask3")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask4")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask5")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask6")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask7")).Visible = false;

        //            this.SignReject(e.Row);

        //            e.Row.Attributes.Add("ondblclick", "ShowAllOrg('" + sczh + "')");
        //            e.Row.Attributes["style"] = "Cursor:hand";
        //            e.Row.Attributes.Add("title", "双击进入查看界面");
        //        }
        //        else
        //        {
        //            ((HyperLink)e.Row.FindControl("hlTask1")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask2")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask3")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask4")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask5")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask6")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask7")).Visible = true;
        //            //为总项添加查看原始数据界面
        //            string xm = sczh.Split('/')[1];
        //            string xm_gc = Server.HtmlEncode(xm + "_" + sczh);

        //            e.Row.Attributes.Add("ondblclick", "ShowAllData('" + xm_gc + "')");
        //            e.Row.Attributes["style"] = "Cursor:hand";
        //            for (int i = 0; i < 7; i++)
        //            {
        //                e.Row.Cells[i].Attributes.Add("title", "双击查看原始数据");
        //            }
        //        }
        //    }
        //}
        protected void SignReject(GridViewRow grow)
        {
            string taskid = grow.Cells[1].Text.Trim();
            //材料计划驳回
            string mp_reject = "select count(MP_STATE) from View_TM_MPFORALLRVW where MP_ENGID='" + taskid + "' and  MP_STATE in('3','5','7')";
            string mp_reject_bg = "select count(MP_STATE) from View_TM_MPCHANGERVW where MP_ENGID='" + taskid + "' and  MP_STATE in('3','5','7')";
            int num_mp = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(mp_reject).Rows[0][0].ToString());
            int num_mp_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(mp_reject_bg).Rows[0][0].ToString());
            if (num_mp != 0 || num_mp_bg != 0)
            {
                ((Label)grow.FindControl("lblMP")).Text = "(" + num_mp + ") (" + num_mp_bg + ")";
            }

            //执行明细驳回
            string ms_reject = "select COUNT(MS_STATE) from TBPM_MSFORALLRVW where MS_ENGID='" + taskid + "' AND MS_STATE in('3','5','7')";
            string ms_reject_bg = "select COUNT(MS_STATE) from TBPM_MSCHANGERVW where MS_ENGID='" + taskid + "' AND MS_STATE in('3','5','7')";
            int num_ms = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(ms_reject).Rows[0][0].ToString());
            int num_ms_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(ms_reject_bg).Rows[0][0].ToString());
            if (num_ms != 0 || num_ms_bg != 0)
            {
                ((Label)grow.FindControl("lblMS")).Text = "(" + num_ms + ") (" + num_ms_bg + ")";
            }

            //外协驳回
            string out_reject = "select COUNT(OST_STATE) from TBPM_OUTSOURCETOTAL where OST_ENGID='" + taskid + "' AND OST_STATE in('3','5','7')";
            string out_reject_bg = "select COUNT(OST_STATE) from TBPM_OUTSCHANGERVW where OST_ENGID='" + taskid + "' AND OST_STATE in('3','5','7')";
            int num_out = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(out_reject).Rows[0][0].ToString());
            int num_out_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(out_reject_bg).Rows[0][0].ToString());
            if (num_out != 0 || num_out_bg != 0)
            {
                ((Label)grow.FindControl("lblOUT")).Text = "(" + num_out + ") (" + num_out_bg + ")";
            }

            //油漆方案
            string paint_reject = "select COUNT(PS_STATE) from TBPM_PAINTSCHEME where PS_ENGID='" + taskid + "' AND PS_STATE in('3','5','7')";
            string paint_reject_bg = "select COUNT(PS_STATE) from TBPM_PAINTSCHEME where PS_ENGID='" + taskid + "' AND PS_STATE in('3','5','7')";
            int num_paint = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(paint_reject).Rows[0][0].ToString());
            int num_paint_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(paint_reject_bg).Rows[0][0].ToString());
            if (num_paint != 0 || num_paint_bg != 0)
            {
                ((Label)grow.FindControl("lblPAINT")).Text = "(" + num_paint + ") (" + num_paint_bg + ")";
            }
        }

        /// <summary>
        /// 绑定合同号
        /// </summary>
        private void GetProNameData()
        {
            sqlText = "select distinct TSA_PJID from View_TM_TaskAssign where TSA_YQHZR='" + Session["UserName"].ToString() + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "TSA_PJID";
            ddlProName.DataValueField = "TSA_PJID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定设备名称
        /// </summary>
        private void GetEngNameData()
        {
            sqlText = "select TSA_ID,TSA_ID+'‖'+TSA_ENGNAME as TSA_ENGNAME from View_TM_TaskAssign ";
            sqlText += "where TSA_PJID='" + ddlProName.SelectedValue + "' and TSA_YQHZR='" + Session["UserName"].ToString() + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlEngName.DataSource = dt;
            ddlEngName.DataTextField = "TSA_ENGNAME";
            ddlEngName.DataValueField = "TSA_ID";
            ddlEngName.DataBind();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
        }
        /// <summary>
        /// 项目名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEngNameData();
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }
        /// <summary>
        /// 工程名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
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
            pager.TableName = "View_TM_TaskAssign";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "TSA_ID,CM_PROJ,TSA_PJID,TSA_ENGNAME,TSA_YQHZR,TSA_STARTDATE,TSA_MANCLERKNAME,TSA_STATE,TSA_CONTYPE,ID";
            pager.OrderField = "TSA_STARTDATE";
            pager.StrWhere = this.GetMyTast();
            pager.OrderType = 1;
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
    }
}
