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
    public partial class PC_ZHAOBIAOLOGIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnlogin_click(object sender, EventArgs e)
        {

            string sqltext = "";
            sqltext = "select * from PC_USERINFO where supplyusername='" + txtusername.Text.Trim() + "' and supplypassword='" + txtpassword.Text.Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                string supplyid = dt.Rows[0]["supplyid"].ToString().Trim();
                Session["supplyid"] = supplyid;
                Response.Redirect("http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOMANAGEMENT.aspx");
                //Response.Redirect("~/PC_Data/PC_ZHAOBIAOMANAGEMENT.aspx");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('您的用户名或者密码错误！！！')</script>");
            }
        }
    }
}
