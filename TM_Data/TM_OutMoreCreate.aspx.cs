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
    public partial class TM_OutMoreCreate : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                Session["Mp_Count"] = 0;

                ViewState["tablename"] = Server.HtmlDecode(Request.QueryString["tablename"].ToString());
                ViewState["strwhere"] = Server.HtmlDecode(Request.QueryString["strwhere"].ToString());
                ViewState["mptype"] = Server.HtmlDecode(Request.QueryString["mptype"].ToString());
                ViewState["mpchange"] = Server.HtmlDecode(Request.QueryString["mpchange"].ToString());

                ViewState["mpno"] = Server.HtmlDecode(Request.QueryString["mpno"].ToString());
                ViewState["pjid"] = Server.HtmlDecode(Request.QueryString["pjid"].ToString());
                ViewState["engtype"] = Server.HtmlDecode(Request.QueryString["engtype"].ToString());
                ViewState["engid"] = Server.HtmlDecode(Request.QueryString["engid"].ToString());
                ViewState["orgtable"] = Server.HtmlDecode(Request.QueryString["orgtable"].ToString());

                this.BindData();
            }
            this.InitVar();
        }

        protected void BindData()
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void btnMpCreate_OnClick(object sender, EventArgs e)
        {
            if (GridView2.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录，无法生成！！！');window.close();", true);
                return;
            }

            if (Session["Mp_Count"].ToString() == "0")
            {
                Session["Mp_Count"] = 1;
                ParamsMpCreate pmc = new ParamsMpCreate();
                pmc.MpChange = ViewState["mpchange"].ToString();
                pmc.MpType = ViewState["mptype"].ToString();
                pmc.TableName = ViewState["tablename"].ToString();
                pmc.StrWhere = ViewState["strwhere"].ToString();
                pmc.Pjid = ViewState["pjid"].ToString();
                pmc.Mpno = ViewState["mpno"].ToString();
                pmc.Engid = ViewState["engid"].ToString();
                pmc.Engtype = ViewState["engtype"].ToString();
                pmc.Userid = Session["UserID"].ToString();
                pmc.OrgTable = ViewState["orgtable"].ToString();

                DataTable dt = this.ExecMpCreate(pmc);
                if (dt.Rows[0][0].ToString() == "OK")
                {
                    Response.Redirect("TM_Out_Source.aspx?id=" + pmc.Mpno);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序出错，请与管理员联系！！！');window.close();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勿重复提交！！！');window.close();", true);
            }
        }

        /// <summary>
        /// 执行存储过生成材料计划
        /// </summary>
        /// <param name="psv"></param>
        protected DataTable ExecMpCreate(ParamsMpCreate pmc)
        {
            DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_OutCreate]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Engid", pmc.Engid, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Engtype", pmc.Engtype, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@MpChange", pmc.MpChange, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Mpno", pmc.Mpno, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@MpType", pmc.MpType, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Pjid", pmc.Pjid, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrWhere", pmc.StrWhere, SqlDbType.Text, 3000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TableName", pmc.TableName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@OrgTable", pmc.OrgTable, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Userid", pmc.Userid, SqlDbType.Text, 1000);
                sqlConn.Open();
                sqlCmd.CommandTimeout = 1000;
                ////sqlCmd.ExecuteNonQuery();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        protected class ParamsMpCreate
        {
            string _TableName;
            string _MpType;
            string _MpChange;
            string _StrWhere;
            string _Pjid;
            string _Engid;
            string _Engtype;
            string _Mpno;
            string _Userid;
            string _OrgTable;

            public string TableName
            {
                get { return _TableName; }
                set { _TableName = value; }
            }

            public string MpType
            {
                get { return _MpType; }
                set { _MpType = value; }
            }

            public string MpChange
            {
                get { return _MpChange; }
                set { _MpChange = value; }
            }

            public string StrWhere
            {
                get { return _StrWhere; }
                set { _StrWhere = value; }
            }

            public string Pjid
            {
                get { return _Pjid; }
                set { _Pjid = value; }
            }

            public string Engid
            {
                get { return _Engid; }
                set { _Engid = value; }
            }

            public string Engtype
            {
                get { return _Engtype; }
                set { _Engtype = value; }
            }

            public string Mpno
            {
                get { return _Mpno; }
                set { _Mpno = value; }
            }

            public string Userid
            {
                get { return _Userid; }
                set { _Userid = value; }
            }

            public string OrgTable
            {
                get { return _OrgTable; }
                set { _OrgTable = value; }
            }

        }


        #region  分页  UCPaging


        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager_org.TableName = ViewState["tablename"].ToString();
            pager_org.PrimaryKey = "BM_ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "dbo.f_formatstr(BM_ZONGXU,'.')";
            pager_org.StrWhere = ViewState["strwhere"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 100;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, GridView2, UCPaging1, NoDataPanel2);
            if (NoDataPanel2.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion
    }
}
