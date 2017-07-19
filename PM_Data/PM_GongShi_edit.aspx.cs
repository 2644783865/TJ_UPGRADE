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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_GongShi_edit : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();

        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string actionstr = Request.QueryString["action"].ToString();
                if (actionstr == "edit")
                {
                    string Id = Request.QueryString["Id"].ToString();
                    sql = "select * from TBMP_GS_LIST where Id='" + Id + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        GS_CUSNAME.Text = dr["GS_CUSNAME"].ToString();
                        GS_CONTR.Text = dr["GS_CONTR"].ToString();
                        GS_TUHAO.Text = dr["GS_TUHAO"].ToString();
                        GS_TUMING.Text = dr["GS_TUMING"].ToString();
                        GS_NOTE.Text = dr["GS_NOTE"].ToString();
                        GS_HOURS.Text = dr["GS_HOURS"].ToString();
                    }
                    dr.Close();
                }
            }
        }
        
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string actionstr = Request.QueryString["action"].ToString();
            if (actionstr == "edit")
            {

                string Id = Request.QueryString["Id"].ToString();
                string sqltext = "";
                sqltext = " update  TBMP_GS_LIST  set GS_TSAID='" + GS_TSAID.Text.Trim() + "',GS_CUSNAME='" + GS_CUSNAME.Text.Trim() + "',GS_CONTR='" + GS_CONTR.Text.Trim() + "',GS_TUHAO='" + GS_TUHAO.Text.Trim() + "',GS_TUMING='" + GS_TUMING.Text.Trim() + "',GS_NOTE='" + GS_NOTE.Text.Trim() + "',GS_HOURS='" + GS_HOURS.Text.Trim() + "' where  ID = '" + Id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，返回主界面？')){window.close();window.opener.location.reload();}", true);
            }
        }

    }
}
