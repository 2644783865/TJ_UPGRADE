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
    public partial class TM_All_Tasts : System.Web.UI.Page
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTMPerson();
                GetTMPro();
                this.BindAllYearMonth();
                this.InitVar();
                InitInfo();
            }
            this.InitVar();
            this.ReturnckbToolTip();
        }

        private string GetMyTast()
        {
            string sql = " TSA_ID LIKE '%" + txtTaskID.Text.Trim() + "%'";
            if (ddlpro.SelectedIndex != 0)
            {
                sql += " and TSA_PJID='" + ddlpro.SelectedValue + "'";
            }

            if(ddlstatus.SelectedIndex!=0)
            {
                sql += " and TSA_STATE='" + ddlstatus.SelectedValue + "' ";
            }

            if (ddlselect.SelectedIndex != 0)
            {
                sql += " and TSA_TCCLERK='" + ddlselect.SelectedValue + "'";
            }

            if (ddlType.SelectedIndex != 0)
            {
                sql += " and TSA_ASSIGNTOELC='" +ddlType.SelectedValue+ "'";
            }

            if (ckbTaskId.Checked)
            {
                sql += " and charindex('-',TSA_ID)=0 ";
            }

            sql += this.SqlTimeString(ddlDJYear, ddlDJMonth, "TSA_ADDTIME");
            sql += this.SqlTimeString(ddlKSYear, ddlKSMonth, "TSA_STARTDATE");
            sql += this.SqlTimeString(ddlJHYear, ddlJHMonth, "TSA_PLANFSDATE");
            sql += this.SqlTimeString(ddlWCYear, ddlWCMonth, "TSA_REALFSDATE");

            sql += " and TSA_PJNAME like '%" + txtPjName.Text.Trim() + "%'";
            sql += " and TSA_ENGNAME LIKE '%" + txtEngName.Text.Trim() + "%'";
            return sql;
        }

        protected string SqlTimeString(DropDownList ddlYear, DropDownList ddlMonth, string Field)
        {
            string retValue = "";
            if (ddlYear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0)
            {
                if (ddlYear.SelectedValue == "%")
                {
                    ddlMonth.SelectedIndex = 0;
                    ddlMonth.Enabled = false;
                    retValue=" AND ("+Field+" IS NULL OR "+Field+"='')";
                }
                else
                {
                    ddlMonth.Enabled = true;
                    retValue=" AND "+Field+" LIKE  '" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "-%'";
                }
            }
            return retValue;
        }

        protected void ReturnckbToolTip()
        {
            if (ckbTaskId.Checked)
            {
                ckbTaskId.ToolTip = "点击 显示生产制号和任务号";
            }
            else
            {
                ckbTaskId.ToolTip = "点击 只显示生产制号";
            }
        }

        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            this.GetBoundData();
        }
        /// <summary>
        /// 绑定技术员
        /// </summary>
        private void GetTMPerson()
        {
            sqlText = "select distinct TSA_TCCLERKNM collate  Chinese_PRC_CS_AS_KS_WS AS TSA_TCCLERKNM,TSA_TCCLERK  from View_TM_TaskAssign ";
            sqlText += "where TSA_TCCLERK is not null order by TSA_TCCLERKNM collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "TSA_TCCLERKNM";
            string dataValue = "TSA_TCCLERK";
            DBCallCommon.BindDdl(ddlselect, sqlText, dataText, dataValue);
        }

        /// <summary>
        /// 绑定项目下的技术员
        /// </summary>
        private void BindPerson()
        {
            sqlText = "select distinct TSA_TCCLERKNM collate  Chinese_PRC_CS_AS_KS_WS AS TSA_TCCLERKNM,TSA_TCCLERK from View_TM_TaskAssign ";
            sqlText += "where TSA_PJID='" + ddlpro.SelectedValue + "' and TSA_TCCLERK is not null order by TSA_TCCLERKNM collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "TSA_TCCLERKNM";
            string dataValue = "TSA_TCCLERK";
            DBCallCommon.BindDdl(ddlselect, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定所有年、月
        /// </summary>
        protected void BindAllYearMonth()
        {
            this.BindYear(ddlDJYear, "TSA_ADDTIME");//登记
            this.BindMonth(ddlDJMonth);

            this.BindYear(ddlKSYear, "TSA_STARTDATE");//开始
            this.BindMonth(ddlKSMonth);

            this.BindYear(ddlJHYear, "TSA_PLANFSDATE");//计划
            this.BindMonth(ddlJHMonth);

            this.BindYear(ddlWCYear, "TSA_REALFSDATE");//完成
            this.BindMonth(ddlWCMonth);
        }

        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void GetTMPro()
        {
            sqlText = "select distinct TSA_PJID,TSA_PJID+'||'+TSA_PJNAME as TSA_PJNAME   from View_TM_TaskAssign order by TSA_PJID";
            string dataText = "TSA_PJNAME";
            string dataValue = "TSA_PJID";
            this.BindCombox(ddlpro, sqlText, dataText, dataValue);
        }

        //绑定技术员下的项目名称
        private void BindPro()
        {
            sqlText = "select distinct TSA_PJID,TSA_PJNAME collate  Chinese_PRC_CS_AS_KS_WS as TSA_PJNAME from View_TM_TaskAssign where TSA_TCCLERK='" + ddlselect.SelectedValue + "' ORDER BY TSA_PJNAME collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "TSA_PJNAME";
            string dataValue = "TSA_PJID";
            this.BindCombox(ddlpro, sqlText, dataText, dataValue);
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        protected void ddlpro_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();;
            if (ddlpro.SelectedItem.Text.Trim() != "-请选择-")
            {
                if (ddlselect.SelectedItem.Text.Trim() == "-请选择-")
                {
                    BindPerson();
                }
            }
            else
            {
                GetTMPerson();
            }
        }

        protected void ckbTaskId_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ckbTaskId.Checked)
            {
                GridView1.HeaderRow.Cells[1].Text = "生产制号";
            }
            else
            {
                GridView1.HeaderRow.Cells[1].Text = "生产制号/任务号";
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlselect.SelectedIndex = 0;
            ddlpro.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 1;
            ddlType.SelectedIndex = 0;
            ddlDJYear.SelectedIndex = 0;
            ddlDJMonth.SelectedIndex = 0;
            ddlKSYear.SelectedIndex = 0;
            ddlKSMonth.SelectedIndex = 0;
            ddlJHYear.SelectedIndex = 0;
            ddlJHMonth.SelectedIndex = 0;
            ddlWCYear.SelectedIndex = 0;
            ddlWCMonth.SelectedIndex = 0;
            txtPjName.Text = "";
            txtEngName.Text = "";
            this.btnQuery_Click(null, null);
        }

        protected void ddlFY_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        protected void ddlselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
            if (ddlselect.SelectedItem.Text.Trim() != "-请选择-")
            {
                if (ddlpro.SelectedItem.Text.Trim() == "-请选择-")
                {
                    BindPro();
                }
            }
            else
            {
                GetTMPro();
            }
        }

        /// <summary>
        /// 绑定年
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="YearField"></param>
        protected void BindYear(DropDownList ddlYear, string YearField)
        {
            string sql = "select distinct (case when " + YearField + " is null then '' when " + YearField + "='' then '' else substring(" + YearField + ",1,4)+'年' end) as YearText,(case when " + YearField + " is null then '%' when " + YearField + "='' then '' else substring(" + YearField + ",1,4) end) as YearValue from View_TM_TaskAssign order by YearText";
            string dataText = "YearText";
            string dataValue = "YearValue";
            DBCallCommon.BindDdl(ddlYear, sql, dataText, dataValue);

            ddlYear.Items.RemoveAt(0);
            ddlYear.Items.Insert(0, new ListItem("-年份-", "-年份-"));
            ddlYear.SelectedIndex = 0;
        }

        protected void BindMonth(DropDownList ddl)
        {
            for (int i = 1; i <= 12; i++)
            {
                string value = i < 10 ? ("0" + i.ToString()) : i.ToString();
                string text = i.ToString() + "月";
                ddl.Items.Add(new ListItem(text,value));
            }
            ddl.Items.Insert(0, new ListItem("-月份-", "%"));
            ddl.SelectedIndex = 0;
        }


        protected  void BindCombox(AjaxControlToolkit.ComboBox ddl, string sqlText,
                           string dataText, string dataValue)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddl.DataSource = dt;
            ddl.DataTextField = dataText;
            ddl.DataValueField = dataValue;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddl.SelectedIndex = 0;
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
            pager.ShowFields = "TSA_ID,TSA_PJNAME+'('+TSA_PJID+')' AS TSA_PJNAME,TSA_ENGNAME,TSA_CONTYPE,TSA_RECVDATE,TSA_TCCLERKNM,TSA_STARTDATE,TSA_PLANFSDATE,TSA_REALFSDATE,TSA_MANCLERKNAME,TSA_STATE,TSA_ENGSTRSMTYPE,TSA_ADDTIME,TSA_NUMBER";
            pager.OrderField = ddlSort.SelectedValue;
            pager.StrWhere = this.GetMyTast();
            pager.OrderType = 0;
            pager.PageSize = 20;
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
        /// 导出生产制号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnExport_OnClick(object sender, EventArgs e)
        {
            string sql = "select TSA_ID,TSA_PJNAME+'('+TSA_PJID+')' AS TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRSMTYPE,TSA_TCCLERKNM from View_TM_TaskAssign where " + this.GetMyTast() + " and charindex('-',TSA_ID)=0 order by TSA_PJID,TSA_ID";
            ExportTMDataFromDB.ExportTaskIDContainsOld(sql);
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sczh = e.Row.Cells[1].Text.Trim();
                if (e.Row.Cells[1].Text.Contains("-"))
                {
                    e.Row.Attributes.Add("ondblclick", "ShowOrg('" + sczh + "')");
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Attributes.Add("title", "双击进入原始数据查看");
                }
            }
        }

    }
}
