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
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_WorkAmount : System.Web.UI.Page
    {
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
            }
            if (IsPostBack)
            {
                this.PagePostBack();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitInfo()
        {
            this.GetProNameData();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;

            this.GetTecName();
            this.GetFY();
            this.BindYear();
            this.BindTabPanelData();

        }

        /// <summary>
        /// 页面回发分页初始化
        /// </summary>
        private void PagePostBack()
        {
            string tapindex = TabContainer1.ActiveTab.ID.ToString();
            switch (tapindex)
            {
                case "TabPanel1"://基本信息
                    ViewState["CurrentUCPaging"] = "UCPageBasic";
                    this.InitVar(UCPageBasic, "View_TM_WorkAmount", "TSA_NO", "", "TSA_ID,TSA_NO", ViewState["Basic"].ToString(), 0, 50);
                    break;
            }
        }

        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void GetProNameData()
        {
            sqlText = "select distinct TSA_PJID,TSA_PJID+'‖'+PJ_NAME as TSA_PJNAME from View_TM_WorkAmount";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "TSA_PJNAME";
            ddlProName.DataValueField = "TSA_PJID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定工程名称
        /// </summary>
        private void GetEngNameData()
        {
            sqlText = "select distinct TSA_ID,TSA_ID+'‖'+TSA_ENGNAME AS TSA_ENGNAME from View_TM_WorkAmount ";
            sqlText += "where TSA_PJID='" + ddlProName.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlEngName.DataSource = dt;
            ddlEngName.DataTextField = "TSA_ENGNAME";
            ddlEngName.DataValueField = "TSA_ID";
            ddlEngName.DataBind();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定技术员
        /// </summary>
        protected void GetTecName()
        {
            ddlTecName.Items.Clear();
            StringBuilder str_sql = new StringBuilder();
            str_sql.Append("select distinct TSA_TCCLERK,isnull(ST_NAMECODE,' ')+'||'+ST_NAME as ST_NAME from View_TM_WorkAmount where 1=1");
            if (ddlProName.SelectedIndex != 0)
            {
                str_sql.Append("AND TSA_PJID='" + ddlProName.SelectedValue.ToString() + "'");
            }

            if (ddlEngName.SelectedIndex != 0)
            {
                str_sql.Append(" AND TSA_ID='" + ddlEngName.SelectedValue + "'");
            }

            string dataText = "ST_NAME";
            string dataValue = "TSA_TCCLERK";

            DBCallCommon.BindAJAXCombox(ddlTecName, str_sql.ToString(), dataText, dataValue);
        }
        /// <summary>
        /// 绑定发运标志
        /// </summary>
        protected void GetFY()
        {
            string sql_text = "select distinct TSA_FY from View_TM_WorkAmount order by TSA_FY";
            string dataText="TSA_FY";
            string dataVaue="TSA_FY";
            DBCallCommon.BindAJAXCombox(ddlFY, sql_text, dataText, dataVaue);
        }
        /// <summary>
        /// 绑定年份
        /// </summary>
        protected void BindYear()
        {
            string sql = "select distinct (case when TSA_ADDTIME is null then '' else substring(TSA_ADDTIME,1,4)+'年' end) as YearText,(case when TSA_ADDTIME is null then '%' else substring(TSA_ADDTIME,1,4) end) as YearValue from View_TM_WorkAmount order by YearText";
            string dataText = "YearText";
            string dataValue = "YearValue";
            DBCallCommon.BindAJAXCombox(ddlYear, sql, dataText, dataValue);

            ddlYear.Items.RemoveAt(0);
            ddlYear.Items.Insert(0,new ListItem("-年份-","-年份-"));
            ddlYear.SelectedIndex=0;
        }

        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEngNameData();
            this.BindTabPanelData();

        }

        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindTabPanelData();
        }

        protected void ddlFY_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindTabPanelData();
        }

        protected void BindTabPanelData()
        {
            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(" 1=1");
            if (ddlProName.SelectedIndex != 0)
            {
                strb_sql.Append("AND TSA_PJID='" + ddlProName.SelectedValue.ToString() + "'");
            }

            if (ddlEngName.SelectedIndex != 0)
            {
                strb_sql.Append(" AND TSA_ID='"+ddlEngName.SelectedValue+"'");
            }

            if (ddlTecName.SelectedIndex != 0)
            {
                if (ddlTecName.SelectedValue == "")
                {
                    strb_sql.Append(" AND (TSA_TCCLERK='" + ddlTecName.SelectedValue + "' or TSA_TCCLERK is null)");
                }
                else
                {
                    strb_sql.Append(" AND TSA_TCCLERK='" + ddlTecName.SelectedValue + "'");
                }
            }

            if (ddlFY.SelectedIndex != 0)
            {
                if (ddlFY.SelectedValue == "")
                {
                    strb_sql.Append(" AND (TSA_FY='" + ddlFY.SelectedValue + "' or TSA_FY is null)");
                }
                else
                {
                    strb_sql.Append(" AND TSA_FY='" + ddlFY.SelectedValue + "'");
                }
            }

            if (ddlYear.SelectedIndex != 0 || ddlMonth.SelectedIndex!=0)
            {
                if (ddlYear.SelectedValue == "%")
                {
                    ddlMonth.SelectedIndex = 0;
                    ddlMonth.Enabled = false;
                    strb_sql.Append(" AND (TSA_ADDTIME IS NULL OR TSA_ADDTIME='')");
                }
                else
                {
                    ddlMonth.Enabled = true;
                    strb_sql.Append(" AND TSA_ADDTIME LIKE  '"+ddlYear.SelectedValue+"-"+ddlMonth.SelectedValue+"-%'");
                } 
            }

            if (ddlFinish.SelectedIndex != 0)
            {
                strb_sql.Append(" AND TSA_STATUS='" + ddlFinish.SelectedValue + "'");
            }

            ViewState["Basic"] = strb_sql.ToString();
            UCPageBasic.CurrentPage = 1;
            this.InitVar(UCPageBasic, "View_TM_WorkAmount", "TSA_NO", "", "TSA_ID,TSA_NO", ViewState["Basic"].ToString(), 0, 50);
            this.bindGrid(UCPageBasic, GridView1, Panel1);
            if (TabContainer1.ActiveTabIndex == 0)
            {
                ViewState["CurrentUCPaging"] = "UCPageBasic";
            }
            else
            {
                ;
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ddlProName.SelectedIndex = 0;

            ddlEngName.Items.Clear();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;

            ddlTecName.SelectedIndex = 0;
            ddlFY.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;

            this.BindTabPanelData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)//获取总列数
            {
                //如果是数据行则添加title
                if (e.Row.RowType == DataControlRowType.DataRow)
                {//设置title为gridview的head的text
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Cells[i].Attributes.Add("title", "双击可以查看生产制号" + e.Row.Cells[1].Text + "的详细信息");
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#EEE8AA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                string url = e.Row.Cells[1].Text.Trim();
                //e.Row.Attributes.Add("onclick", "return openLink('" + url + "')");
                e.Row.Attributes["onclick"] = String.Format("javascript:setTimeout(\"if(dbl_click){{dbl_click=false;}}else{{{0}}};\", 100000);", ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString(), true));
                // 双击，设置 dbl_click=true，以取消单击响应
                e.Row.Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + url + "');", GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

                if (((Label)e.Row.FindControl("lbltcid")).Text == Session["UserID"].ToString() && e.Row.Cells[2].Text.Trim() == "&nbsp;")
                {
                    ((HyperLink)e.Row.FindControl("hlTask")).Visible = true;
                }
                else
                {
                    ((HyperLink)e.Row.FindControl("hlTask")).Visible = false;
                }

            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "select TSA_ID,TSA_FY,TSA_STATUS,TSA_ENGTYPE,TSA_STFORCODE,PJ_NAME+'('+TSA_PJID+')' AS PJ_NAME,TSA_ENGNAME,ST_NAME,TSA_TOTALWGHT,TSA_RECVDATE,TSA_ADDTIME from View_TM_WorkAmount where " + ViewState["Basic"].ToString() + " and (TSA_SHIP IS NULL OR TSA_SHIP='') order by TSA_STFORCODE";
            ExportTMDataFromDB.ExportTaskID(sqltext);
        }

        protected void GridView1_OnPreRender(object sender, EventArgs e)
        {
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 1);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 5);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 6);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 7);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 8);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 9);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 12);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 14);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 15);
        }

        #region  分页  UCPaging

        PagerQueryParam pager_org = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar(UCPaging ParamUCPaging, string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            InitPager(_tablename, _primarykey, _showfields, _orderfield, _strwhere, _ordertype, _pagesize);
            ParamUCPaging.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            ParamUCPaging.PageSize = pager_org.PageSize; //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            pager_org.TableName = _tablename;
            pager_org.PrimaryKey = _primarykey;
            pager_org.ShowFields = _showfields;
            pager_org.OrderField = _orderfield;
            pager_org.StrWhere = _strwhere;
            pager_org.OrderType = _ordertype; //升序排列
            pager_org.PageSize = _pagesize;
        }

        void Pager_PageChanged(int pageNumber)
        {
            Control[] CRL = this.BindGridParamsRecord(ViewState["CurrentUCPaging"].ToString());
            bindGrid((UCPaging)CRL[0], (GridView)CRL[1], (Panel)CRL[2]);
        }

        private void bindGrid(UCPaging ParamUCPaging, GridView ParamGridView, Panel ParamPanel)
        {
            pager_org.PageIndex = ParamUCPaging.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, ParamGridView, ParamUCPaging, ParamPanel);
            if (ParamPanel.Visible)
            {
                ParamUCPaging.Visible = false;
            }
            else
            {
                ParamUCPaging.Visible = true;
                ParamUCPaging.InitPageInfo();  //分页控件中要显示的控件
            }

        }

        /// <summary>
        /// 当前UCPaging
        /// </summary>
        /// <param name="_UCPaging"></param>
        /// <returns></returns>
        protected Control[] BindGridParamsRecord(string _UCPaging)
        {
            Control[] contrl = new Control[3];
            switch (_UCPaging)
            {
                case "UCPageBasic":
                    contrl[0] = (UCPaging)UCPageBasic;//基本信息
                    contrl[1] = (GridView)GridView1;
                    contrl[2] = (Panel)Panel1;
                    break;
                default:
                    contrl[0] = (UCPaging)UCPageBasic;
                    contrl[1] = (GridView)GridView1;
                    contrl[2] = (Panel)Panel1;
                    break;
            }
            return contrl;
        }
        #endregion

    }
}

