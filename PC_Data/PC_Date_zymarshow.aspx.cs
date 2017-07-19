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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Date_zymarshow : System.Web.UI.Page
    {
        public string marid
        {
            get
            {
                object str = ViewState["marid"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["marid"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitInfo()
        {
            if (Request.QueryString["marid"] != null)
            {
                marid = Request.QueryString["marid"].ToString();
            }
            else
            {
                marid = "";
            }
            databind();
        }
        private void databind()
        {
            string sqltext="";
            sqltext = "SELECT SQ_MARID as marid, MNAME as marnm, SQ_LENGTH as length ,"+
                      "SQ_WIDTH as width, sumnum as num, sumfznum as fznum, GUIGE as margg, GB as margb, " +
                      "CAIZHI as marcz, PURCUNIT as unit, FUZHUUNIT as fzunit " +
                      "FROM View_TBPC_STOSUM where SQ_MARID='"+marid+"'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btn_concel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
    }
}
