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

namespace ZCZJ_DPF.PM_Data
{
    public partial class TBPC_ShowSupply : System.Web.UI.Page
    {
        string name = "";
        string session = "";
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            session = Session["UserID"].ToString();
            if (Request.QueryString["name"] != null)
            {
                name = Server.UrlDecode(Request.QueryString["name"].ToString());

            }
            Showsupply();
            InitVar();
            
            if (!IsPostBack)
            {               
              
                GetManutAssignData(); //数据绑定
            }
        }


        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetManutAssignData();
        }
        
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "ShowSupply";
            pager.PrimaryKey = "CS_CODE ";
            pager.ShowFields = "CS_code,cs_name,cs_hrcode,cs_scope,cs_session";
            pager.OrderField = "CS_NAME";
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
            pager.StrWhere = "CS_SESSION='" + session + "'";
           
        }


        protected void GetManutAssignData()
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变 

               
            }
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
        
       
        

        private DataTable Showsupply()
        {
            DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[Search_Supply]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@name", name, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@session", session, SqlDbType.Text, 1000); 
                sqlConn.Open();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
    }
}
