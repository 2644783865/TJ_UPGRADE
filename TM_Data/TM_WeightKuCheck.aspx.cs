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
    public partial class TM_WeightKuCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitData();
            }
        }

        protected void InitData()
        {
            if (Request.QueryString["TaskID"] != null)
            {
                ViewState["tskid"]=Request.QueryString["TaskID"].ToString().Split('-')[0];
                GridView1.DataSource = this.ExecWgtKuCheck(ViewState["tskid"].ToString(), "","1");
                GridView1.DataBind();
                if (GridView1.Rows.Count > 0)
                {
                    NoDataPanel.Visible = false;
                }
                else
                {
                    NoDataPanel.Visible = true;
                }
            }
        }

        protected DataTable ExecWgtKuCheck(string taskid,string xuhao,string jishu)
        {
            DataTable dt=null;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_WeightKuCheck]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TaskID", taskid, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BM_XUHAO", xuhao, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BM_Jishu", jishu, SqlDbType.Text, 1000);
                sqlConn.Open();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                ;
            }
            return dt;
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            GridView1.DataSource = this.ExecWgtKuCheck(ViewState["tskid"].ToString(), txtXuhao.Text.Trim(),ddlJishu.SelectedValue);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
    }
}
