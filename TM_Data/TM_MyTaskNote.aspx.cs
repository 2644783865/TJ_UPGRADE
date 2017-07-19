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
    public partial class TM_MyTaskNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
        }

        protected void InitPage()
        {
            string taskid = Request.QueryString["engid"].ToString();

            if (taskid.Contains("-"))
            {
                string sqlText = "select TSA_ID,CM_PROJ,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID,TSA_MYNOTE ";
                sqlText += "from View_TM_TaskAssign where TSA_ID='" + taskid + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                {
                    tsaid.Text = dr["TSA_ID"].ToString();
                    lab_proname.Text = dr["CM_PROJ"].ToString();
                    lab_engname.Text = dr["TSA_ENGNAME"].ToString();
                    TextArea1.Value = dr["TSA_MYNOTE"].ToString();
                }
                dr.Close();
            }
        }

        protected void btnSubmit_onclick(object sendr, EventArgs e)
        {
            string sqltext = "update TBPM_TCTSASSGN set TSA_MYNOTE='" + TextArea1.Value.Trim() + "' where TSA_ID='" + tsaid.Text.Trim() + "'";
            DBCallCommon.ExeSqlText(sqltext);
            Response.Write("<script>alert('修改成功！！！');window.close();</script>");
        }
    }
}
