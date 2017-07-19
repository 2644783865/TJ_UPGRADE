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
    public partial class TM_MP_TurnForLibrary : System.Web.UI.Page
    {
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }

        //初始化Panel
        private void InitPanel()
        {
            if (GridView1.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
                btnForLib.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        //初始化页面参数
        private void InitVar()
        {
            tsaid.Text = Request.QueryString["action"];
            sqlText = "select TSA_PJNAME,TSA_ENGNAME from TBPM_TCTSASSGN where TSA_ID='" + tsaid.Text + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                proname.Text = dr[0].ToString();
                engname.Text = dr[1].ToString();
            }
            dr.Close();
        }
        //初始化页面
        private void InitInfo()
        {
            InitVar();
            sqlText = "select * from TBPM_MPFORLIB where MP_ENGID='" + tsaid.Text + "' and MP_STATE='0' ";
            DBCallCommon.BindGridView(GridView1,sqlText);
            InitPanel();
        }

        protected void btnForLib_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                sqlText = "update TBPM_MPFORLIB set MP_STATE='1' ";
                sqlText += "where MP_ENGID='" + tsaid.Text + "' and MP_MARID='"+gr.Cells[1].Text.Trim()+"' and MP_STATE='0'";
                DBCallCommon.ExeSqlText(sqlText);
            }
        }
    }
}
