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

namespace ZCZJ_DPF
{
    public partial class MT_Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (Request.QueryString["id"] != null)     //登陆
            {
                Session["id"] = Request.QueryString["id"];
            }
            if (Request.QueryString["sq"] != null)
            {
                Response.Write("<script>alert('授权登录成功!');</script>");
            }
        }
    }
}
