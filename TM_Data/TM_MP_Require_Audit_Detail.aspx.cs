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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MP_Require_Audit_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindDataByPassString();
            }
        }

        protected void BindDataByPassString()
        {
            string tracknum_table =Server.HtmlDecode(Request.QueryString["tracknum_table"].ToString());
            string[] arraystring = tracknum_table.Split('$');
            string sqltext = "select * from "+arraystring[1]+"  where MP_TRACKNUM='"+arraystring[0]+"'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}
