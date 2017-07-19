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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_WaitforCreate : System.Web.UI.Page
    {
        string strtable = "TBPM_STRINFODQO";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
            if (IsPostBack)
            {
                this.InitVarMS();
            }
        }
        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage()
        {
            string passedstring = Request.QueryString["NorMS"].ToString();//序号$生产制号$原始数据表名，如MSNor=1.2$1.4$1.7$MB/FER/2-1$TBPM_STRINFOQLM
            string[] querystringsplit = passedstring.Split('$');
            string[] aa = querystringsplit[0].Split('.');
            string fxuhao = aa[0].ToString();//1.2获取1
            ViewState["TaskId"] = querystringsplit[querystringsplit.Length - 2];
            this.BindPartXuhao(querystringsplit);
            if (querystringsplit.Length == 3)
            {
                string temp;
                if (querystringsplit[0] != "")
                {
                     temp = "(BM_XUHAO='" + fxuhao + "' or BM_XUHAO like  '" + querystringsplit[0] + ".%' OR BM_XUHAO='" + querystringsplit[0] + "')";

                }
                else
                {
                     temp = "1=1";
                }
                ViewState["xuhao_str"] = temp;
            }
            else
            {
                string temp = " (BM_XUHAO='" + fxuhao + "' or BM_XUHAO like  '" + querystringsplit[0] + ".%' OR BM_XUHAO='" + querystringsplit[0] + "'";
                ViewState["xuhao_str"] = temp;
                for (int i = 1; i < querystringsplit.Length - 3; i++)
                {
                    temp = "or BM_XUHAO like '" + querystringsplit[i] + ".%' OR BM_XUHAO='" + querystringsplit[i] + "'";
                    ViewState["xuhao_str"] += temp;
                }
                temp = " or BM_XUHAO like '" + querystringsplit[querystringsplit.Length - 3] + ".%' OR BM_XUHAO='" + querystringsplit[querystringsplit.Length - 3] + "')";
                ViewState["xuhao_str"] += temp;
            }
            this.BindEngType();
            GetShebeiName(ddlShebei);
            rblInMs.SelectedIndex = 1;
            ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "' and " + ViewState["xuhao_str"] + " and BM_ISMANU='Y' and BM_MSSTATE='0'  AND BM_MSSTATUS='0'";
            UCPagingMS.CurrentPage = 1;
            this.InitVarMS();
            this.bindGridMS();
            Session["MSNorSubmited"] = "";

        }

        private void GetShebeiName(DropDownList ddlShebei)
        {

            string sqlText = "select BM_TUHAO+'|'+BM_CHANAME as BM_ENGNAME from  TBPM_STRINFODQO  where BM_ENGID='" + ViewState["TaskId"] + "' and dbo.Splitnum(BM_XUHAO,'.')<=1";
            string dataText = "BM_ENGNAME";
            string dataValue = "BM_ENGNAME";
            DBCallCommon.BindDdl(ddlShebei, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定工程信息
        /// </summary>
        private void BindEngType()
        {
            tsaid.Text = ViewState["TaskId"].ToString();
            //获取项目名称，工程名称，设备类型等
            string sqltext = "select TSA_PJID,CM_PROJ,TSA_ENGNAME ";
            sqltext += "from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskId"] + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                lblContract.Text = dr[0].ToString();
                proname.Text = dr[1].ToString();
                engname.Text = dr[2].ToString();

            }
            dr.Close();
        }

        /// <summary>
        /// 是否体现在制作明细中查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblInMs_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblInMs.SelectedIndex != 0)
            {
                ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "' and " + ViewState["xuhao_str"] + " and BM_ISMANU='" + rblInMs.SelectedValue + "' and BM_MSSTATE='0' and BM_MSSTATUS='0'";
            }
            else
            {
                ViewState["sqlText"] = "BM_ENGID='" + ViewState["TaskId"] + "' and " + ViewState["xuhao_str"] + "";
            }

            if (ddlXuhao.SelectedIndex != 0)
            {
                ViewState["sqlText"] += " and (BM_XUHAO='" + ddlXuhao.SelectedValue + "' OR BM_XUHAO like '" + ddlXuhao.SelectedValue + ".%')";
            }

            ViewState["sqlText"] += " and  BM_MSSTATE='0'";

            UCPagingMS.CurrentPage = 1;
            this.InitVarMS();
            this.bindGridMS();
        }
        /// <summary>
        /// 生成制作明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMSCreate_OnClick(object sender, EventArgs e)
        {
            if (Session["MSNorSubmited"].ToString() == "已提交")
            {
                btnMSCreate.Visible = false;
                btnBack.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勿重复提交！！！\\r\\r提示:数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
            else
            {
                if (ddlShebei.SelectedIndex == 0)
                {


                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择设备，图号！')", true);

                }
                else
                {
                    List<string> list_insert_ms = new List<string>();

                    //获取正常批号
                    string sql_get_pici = "select count(*) from TBPM_MSFORALLRVW where MS_ENGID='" + tsaid.Text + "' and cast(MS_STATE as int)>1";
                    DataTable dt_pici = DBCallCommon.GetDTUsingSqlText(sql_get_pici);
                    string pici = (Convert.ToInt16(dt_pici.Rows[0][0].ToString()) + 1).ToString();//当前批次
                    string ms_no = tsaid.Text + "." + "MS" + "/" + pici.PadLeft(2, '0');
                    //更改原始数据中的明细过渡状态
                    string sql_update_org_mstemp = "update " + strtable + " set BM_MSTEMP='1' where BM_ENGID='" + tsaid.Text + "' and  " + ViewState["xuhao_str"] + " and BM_ISMANU='Y' and BM_MSSTATE='0' and BM_MSSTATUS='0'";
                    DBCallCommon.ExeSqlText(sql_update_org_mstemp);
                    //借助存储过程生成正常制作明细
                    MSCreate classMSCreate = new MSCreate();
                    classMSCreate.MS_PID = ms_no;
                    classMSCreate.StrTabeleName = strtable;
                    classMSCreate.ViewTabelName = "View_TM_DQO";
                    classMSCreate.TaskID = ViewState["TaskId"].ToString();
                    classMSCreate.MsTableName = "TBPM_MKDETAIL";
                    classMSCreate.MsRewTabelName = "TBPM_MSFORALLRVW";
                    classMSCreate.SubmitID = Session["UserID"].ToString();
                    string ret = this.ExecMsCreate(classMSCreate);
                    if (ret == "0")
                    {
                        Session["MSNorSubmited"] = "已提交";
                        string sql_select_reviewa = "select TSA_REVIEWERID  from TBPM_TCTSASSGN where TSA_ID='" + ViewState["TaskId"].ToString() + "'";
                        DataTable dt_review = DBCallCommon.GetDTUsingSqlText(sql_select_reviewa);
                        if (dt_review.Rows.Count > 0)
                        {
                            string sql_set_reviewa = "update TBPM_MSFORALLRVW set MS_REVIEWA='" + dt_review.Rows[0][0].ToString() + "',MS_MAP='" + ddlShebei.SelectedValue.Split('|')[0] + "',MS_CHILDENGNAME='"+ddlShebei.SelectedValue.Split('|')[1]+"' where MS_ID='" + ms_no + "'";
                            DBCallCommon.ExeSqlText(sql_set_reviewa);
                        }
                        Response.Redirect("TM_MS_Detail_Audit.aspx?id=" + ms_no);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.location.replace('TM_Md_Detail_List.aspx?id='+" + ms_no+");", true);
                    }
                    else
                    {

                        sql_update_org_mstemp = "update " + strtable + " set BM_MSTEMP='0' where BM_ENGID='" + tsaid.Text + "' and  " + ViewState["xuhao_str"] + "";
                        DBCallCommon.ExeSqlText(sql_update_org_mstemp);



                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序异常，请稍后再试！！！');window.close();", true);
                    }
                }

            }
        }

        private class MSCreate
        {
            private string _MS_PID;//批号
            private string _StrTabeleName;
            private string _ViewTabelName;
            private string _TaskID;
            private string _MsTableName;
            private string _MsRewTabelName;
            private string _SubmitID;
            public string StrTabeleName
            {
                get { return _StrTabeleName; }
                set { _StrTabeleName = value; }
            }
            public string ViewTabelName
            {
                get { return _ViewTabelName; }
                set { _ViewTabelName = value; }
            }
            public string TaskID
            {
                get { return _TaskID; }
                set { _TaskID = value; }
            }
            public string MS_PID
            {
                get { return _MS_PID; }
                set { _MS_PID = value; }
            }
            public string MsTableName
            {
                get { return _MsTableName; }
                set { _MsTableName = value; }
            }
            public string MsRewTabelName
            {
                get { return _MsRewTabelName; }
                set { _MsRewTabelName = value; }
            }

            public string SubmitID
            {
                get { return _SubmitID; }
                set { _SubmitID = value; }
            }
        }
        private string ExecMsCreate(MSCreate mscreate)
        {
            DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_MS_NorCreate]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrTabeleName", mscreate.StrTabeleName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@ViewTabelName", mscreate.ViewTabelName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TaskID", mscreate.TaskID, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@MS_PID", mscreate.MS_PID, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@MsTableName", mscreate.MsTableName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@MsRewTabelName", mscreate.MsRewTabelName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@SubmitID", mscreate.SubmitID, SqlDbType.Text, 1000);

                sqlConn.Open();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                return "1";
            }
            return dt.Rows[0][0].ToString();
        }
        /// <summary>
        /// 返回制作明细调整界面
        /// </summary>
        /// <param name="sendr"></param>
        /// <param name="e"></param>
        protected void btnBack_OnClick(object sendr, EventArgs e)
        {
            Response.Redirect("TM_MS_TEMP_InitData.aspx?NorMS=" + Request.QueryString["MSNor"].ToString() + "");
        }
        #region  分页
        PagerQueryParam pager_ms = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVarMS()
        {
            InitPagerMS();
            UCPagingMS.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            UCPagingMS.PageSize = pager_ms.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPagerMS()
        {
            pager_ms.TableName = "View_TM_DQO";
            pager_ms.PrimaryKey = "BM_ID";
            pager_ms.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER";
            pager_ms.OrderField = "dbo.f_formatstr(BM_XUHAO, '.')";
            pager_ms.StrWhere = ViewState["sqlText"].ToString();
            pager_ms.OrderType = 0;//升序排列
            pager_ms.PageSize = 100;
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGridMS();
        }
        private void bindGridMS()
        {
            pager_ms.PageIndex = UCPagingMS.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_ms);
            CommonFun.Paging(dt, grv, UCPagingMS, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPagingMS.Visible = false;
            }
            else
            {
                UCPagingMS.Visible = true;
                UCPagingMS.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion

        /// <summary>
        /// 导出调整项Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "select [BM_MSXUHAO],[BM_XUHAO],[BM_TUHAO],[BM_MARID],[BM_ZONGXU],[BM_CHANAME],[BM_ENGSHNAME],[BM_GUIGE],[BM_MAQUALITY],[BM_NUMBER],[BM_UNITWGHT],[BM_TOTALWGHT],[BM_MASHAPE],[BM_MASTATE],[BM_PROCESS],[BM_KU],'' as BM_BOX,[BM_NOTE],[BM_STANDARD],[BM_ISMANU] from View_TM_DQO where " + ViewState["sqlText"].ToString() + " and BM_XUHAO NOT LIKE '%.0.%' order by dbo.f_formatstr(BM_XUHAO, '.')";
            string pjname = proname.Text.Trim() + "(" + proname.Text + ")";
            string engnanme = tsaid.Text.Trim().Split('-')[0] + "(" + engname.Text.Trim() + ")";
            ExportTMDataFromDB.ExportNormalMSAdjust(sqltext, pjname, engnanme);
        }

        /// <summary>
        /// 弹出框修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grv_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)//获取总列数
                {
                    //如果是数据行则添加title
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Cells[i].Attributes.Add("title", "双击修改数据");
                }
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#EEE8AA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                string url = e.Row.Cells[2].Text.Trim() + "&strtable=" + strtable + "&taskid=" + ViewState["TaskId"].ToString() + "";
                // 双击，设置 dbl_click=true，以取消单击响应
                e.Row.Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + url + "');");
            }
        }

        /// <summary>
        /// 绑定部件序号
        /// </summary>
        /// <param name="array_xuhao"></param>
        protected void BindPartXuhao(string[] array_xuhao)
        {
            int times = array_xuhao.Length - 2;
            if (array_xuhao.Length > 3)
            {
                for (int i = 0; i < times; i++)
                {
                    ddlXuhao.Items.Add(new ListItem(array_xuhao[i], array_xuhao[i]));
                }
                ddlXuhao.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                ddlXuhao.SelectedIndex = 0;
            }
            else
            {
                if (array_xuhao[0].Length > 3)//单一部件序号
                {
                    ddlXuhao.Items.Add(new ListItem(array_xuhao[0], array_xuhao[0]));
                    ddlXuhao.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                    ddlXuhao.SelectedIndex = 0;
                }
                else//顶级部件序号
                {
                    string sqltext = "select BM_XUHAO,BM_XUHAO+'||'+BM_CHANAME as BM_CHANAME from " + strtable + " where BM_ENGID='" + ViewState["TaskId"].ToString() + "' AND (dbo.Splitnum(BM_XUHAO,'.')=1 or len(BM_XUHAO)=1) and BM_MSSTATE='0' order by dbo.f_formatstr(BM_XUHAO, '.')";
                    string dataText = "BM_CHANAME";
                    string dataValue = "BM_XUHAO";
                    DBCallCommon.BindAJAXCombox(ddlXuhao, sqltext, dataText, dataValue);
                }
            }


        }
    }
}
