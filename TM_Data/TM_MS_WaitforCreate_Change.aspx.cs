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
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_WaitforCreate_Change : System.Web.UI.Page
    {
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
            Session["MSNorSubmited"] = "";
            tsaid.Text = Request.QueryString["TaskID"].ToString();
            rblChange.SelectedIndex=0;//变更后
            this.BindEngType();
            GetShebeiName(ddlShebei);
            rblInMs.SelectedIndex = 0;
            //分页数据
            ViewState["QueryTable"] = "View_TM_DQO";
            ViewState["ID"]="BM_ID";
            ViewState["ShowFields"] = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
            ViewState["OrderField"] = "dbo.f_formatstr(BM_XUHAO, '.')";
            ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and BM_MSSTATUS!='0' and BM_MSSTATE='0' "+this.strWhereXuhao()+"";
            UCPagingMS.CurrentPage = 1;
            this.InitVarMS();
            this.bindGridMS();
        }
        private void GetShebeiName(DropDownList ddlShebei)
        {

            string sqlText = "select BM_TUHAO+'|'+BM_CHANAME as BM_ENGNAME from  TBPM_STRINFODQO  where BM_ENGID='" + tsaid.Text + "' and dbo.Splitnum(BM_XUHAO,'.')<=1";
            string dataText = "BM_ENGNAME";
            string dataValue = "BM_ENGNAME";
            DBCallCommon.BindDdl(ddlShebei, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定工程信息
        /// </summary>
        private void BindEngType()
        {
            //获取项目名称，工程名称，设备类型等
            string sqltext = "select TSA_PJID,CM_PROJ,TSA_ENGNAME ";
            sqltext += "from View_TM_TaskAssign where TSA_ID='" + tsaid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                lblContract.Text = dr[0].ToString();
                proname.Text = dr[1].ToString();
              //  engname.Text = dr[2].ToString();
                
            }
            dr.Close();
        }
        /// <summary>
        /// 是否体现在制作明细中及变更前后查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblInMs_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblChange.SelectedValue == "B")//变更前，从制作明细正常表中读取
            {
                rblInMs.SelectedIndex = 1;
                rblInMs.Enabled = false;
               
                grv.Columns[19].Visible = false;

                ViewState["QueryTable"] = "TBPM_MKDETAIL";
                ViewState["ID"] = "MS_ID";
                ViewState["ShowFields"] = " MS_NEWINDEX as BM_XUHAO,MS_TUHAO as BM_TUHAO,MS_ZONGXU as BM_ZONGXU,MS_NAME as BM_CHANAME,MS_GUIGE as BM_MAGUIGE,MS_CAIZHI as BM_MAQUALITY,cast(MS_UNUM as varchar)+' | '+cast(MS_NUM as varchar) AS NUMBER,MS_TUWGHT as BM_TUUNITWGHT,MS_TUTOTALWGHT as BM_TUTOTALWGHT,'0' as BM_MSSTATUS,MS_MASHAPE as BM_MASHAPE,MS_MASTATE as BM_MASTATE,MS_PROCESS as BM_PROCESS,'Y' as BM_ISMANU,MS_LENGTH as BM_MALENGTH,MS_WIDTH as BM_MAWIDTH,MS_NOTE as BM_NOTE,MS_TECHUNIT  as BM_TECHUNIT,MS_XIALIAO as BM_XIALIAO,MS_YONGLIANG as BM_YONGLIANG,MS_ALLBEIZHU as BM_ALLBEIZHU,MS_MATOTALWGHT as BM_MATOTALWGHT,MS_KU as BM_KU";
                ViewState["OrderField"] = "dbo.f_formatstr(MS_NEWINDEX, '.')";
                ViewState["sqlText"] = "MS_ENGID='" + tsaid.Text + "' and MS_STATUS='0' and MS_NEWINDEX in (select BM_XUHAO from View_TM_DQO where BM_ENGID='" + tsaid.Text + "' and BM_MSSTATUS!='0' and BM_MSSTATE='0' " + this.strWhereXuhao() + ") AND MS_PID in(select MS_PID from View_TM_MKDETAIL where MS_ENGID='" + tsaid.Text.Trim() + "' and MS_REWSTATE='8' and MS_STATUS='0' )";
            }
            else if (rblChange.SelectedValue == "A")//变更后，从原始数据中读取
            {
                rblInMs.Enabled = true;
              
                grv.Columns[19].Visible = true;

                ViewState["QueryTable"] = "View_TM_DQO";
                ViewState["ID"] = "BM_ID";
                ViewState["ShowFields"] = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
                ViewState["OrderField"] = "dbo.f_formatstr(BM_XUHAO, '.')";

                if (rblInMs.SelectedIndex == 0)
                {
                    ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and BM_MSSTATUS!='0' and BM_MSSTATE='0' "+this.strWhereXuhao()+"";
                }
                else
                {
                    ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and BM_MSSTATUS!='0' and BM_ISMANU='" + rblInMs.SelectedValue + "' and BM_MSSTATE='0' "+this.strWhereXuhao()+"";
                }
            }

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
             //   btnBack.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勿重复提交！！！\\r\\r提示:数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
            else
            {
                if (ddlShebei.SelectedIndex == 0)
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择设备，图号！');", true);
                    return;

                }
                List<string> list_insert_ms = new List<string>();

                //获取变更批号(从正常审核表中找出，变更批号次数)
                string sql_get_pici = "select count(*) from TBPM_MSCHANGERVW where MS_ENGID='" + tsaid.Text + "' and cast(MS_STATE as int)>1 and MS_ID like '%JSB MSBG%'";
                DataTable dt_pici = DBCallCommon.GetDTUsingSqlText(sql_get_pici);
                string pici = (Convert.ToInt16(dt_pici.Rows[0][0].ToString()) + 1).ToString();//当前批次
                string ms_no = tsaid.Text + "." + "JSB MSBG" + "/" +  pici.PadLeft(2, '0');

                //更改原始数据中的明细过渡状态
                string sql_update_org_mstemp = "update TBPM_STRINFODQO set BM_MSTEMP='1' where BM_ENGID='" + tsaid.Text + "' and  BM_MSSTATUS!='0' and BM_XUHAO<>'1.0' and BM_XUHAO not like '%.0.%'  AND BM_MSSTATE='0'  and BM_MSSTATUS<>'0'";
                sql_update_org_mstemp += this.strWhereXuhao();
                
                DBCallCommon.ExeSqlText(sql_update_org_mstemp);
                //借助存储过程生成制作明细
                MSCreate classMSCreate = new MSCreate();
                classMSCreate.MS_PID = ms_no;
                classMSCreate.StrTabeleName = "TBPM_STRINFODQO";
                classMSCreate.ViewTabelName = "View_TM_DQO";
                classMSCreate.TaskID = tsaid.Text;
                classMSCreate.MsTableName = "TBPM_MKDETAIL";
                classMSCreate.MsRewTabelName = "TBPM_MSCHANGERVW";
                classMSCreate.SubmitID = Session["UserID"].ToString();
                string ret = this.ExecMsCreate(classMSCreate);
                if (ret == "0")
                {
                    Session["MSNorSubmited"] = "已提交";
                    string sql_select_reviewa = "select TSA_REVIEWERID  from TBPM_TCTSASSGN where TSA_ID='" + tsaid.Text + "'";
                    DataTable dt_review = DBCallCommon.GetDTUsingSqlText(sql_select_reviewa);
                    if (dt_review.Rows.Count > 0)
                    {
                        string sql_set_reviewa = "update TBPM_MSCHANGERVW set MS_REVIEWA='" + dt_review.Rows[0][0].ToString() + "',MS_MAP='" + ddlShebei.SelectedValue.Split('|')[0] + "',MS_CHILDENGNAME='" + ddlShebei.SelectedValue.Split('|')[1] + "' where MS_ID='" + ms_no + "'";
                        DBCallCommon.ExeSqlText(sql_set_reviewa);
                    }
                    Response.Redirect("TM_MS_Detail_Audit.aspx?id=" + ms_no);
                }
                else
                {
                    sql_update_org_mstemp = "update TBPM_STRINFODQO set BM_MSTEMP='0' where BM_ENGID='" + tsaid.Text + "' and  BM_MSSTATUS!='0'";
                    sql_update_org_mstemp += this.strWhereXuhao();
                    DBCallCommon.ExeSqlText(sql_update_org_mstemp);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序异常，请稍后再试！！！');window.close();", true);
                }
            }
        }

        protected string strWhereXuhao()
        {
            string str_xuhao = Request.QueryString["Xuhao"].ToString();
            string strsqlxuho = "";
            if (str_xuhao!="0")
            {
                string[] arrayxuhao = str_xuhao.Split('$');


                foreach (string str in arrayxuhao)
                {
                    strsqlxuho += " BM_XUHAO='" + str + "' OR BM_XUHAO LIKE '" + str + ".%' OR ";
                }
                strsqlxuho = "  AND (" + strsqlxuho.Substring(0, strsqlxuho.Length - 3) + ")";

               
            }
            return strsqlxuho;
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
            Response.Redirect("TM_MS_TEMP_InitData_Change.aspx?TaskID=" + tsaid.Text + "&Xuhao=" + Request.QueryString["Xuhao"].ToString() + "");
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
            pager_ms.TableName = ViewState["QueryTable"].ToString();
            pager_ms.PrimaryKey = ViewState["ID"].ToString();
            pager_ms.ShowFields = ViewState["ShowFields"].ToString();
            pager_ms.OrderField = ViewState["OrderField"].ToString();
            pager_ms.StrWhere = ViewState["sqlText"].ToString();
            pager_ms.OrderType = 0;//升序排列
            pager_ms.PageSize = 200;
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
    }
}
