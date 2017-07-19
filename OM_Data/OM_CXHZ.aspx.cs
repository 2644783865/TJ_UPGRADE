using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CXHZ : System.Web.UI.Page
    {
        string txtCx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtCx = Request.QueryString["id"].ToString().Trim();
        }
        protected void btncx_nj_click(object sender, EventArgs e)
        {
            Response.Redirect("http://111.160.8.74:888/OM_Data/OM_CX.aspx?id=" + txtCx);
        }
        protected void btncx_cb_click(object sender, EventArgs e)
        {
            Response.Redirect("http://111.160.8.74:888/OM_Data/OM_CBCX.aspx?id=" + txtCx);
        }
    }
}
