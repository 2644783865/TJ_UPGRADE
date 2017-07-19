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
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_NJCX : System.Web.UI.Page
    {
        //string baocun = "";
        //string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //bindtime(ddlYear, ddlMonth);
                //asd.first = "1";
            }
        }
        protected void btncx_click(object sender, EventArgs e)
        {

            string strText = "";

            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string password = ed.MD5Encrypt(txtpassword.Text.Trim(), "!@#$%^&*");
            strText = "select a.ST_ID,a.ST_NAME,a.ST_PASSWORD,a.ST_DEPID,b.DEP_NAME,a.R_NAME,a.ST_POSITION ";
            strText += "from TBDS_STAFFINFO as a,TBDS_DEPINFO as b where a.ST_DEPID=b.DEP_CODE ";
            strText += "and (ST_WORKNO='" + txtCx.Text + "' or ST_NAME = '" + txtCx.Text + "') ";
            strText += "and ST_PASSWORD='" + password + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strText);
            //Session["UserName"] = dt.Rows[0]["ST_NAME"].ToString();
            if (dt.Rows.Count > 0)
            {
                string st_id = dt.Rows[0]["ST_ID"].ToString().Trim();
                Response.Redirect("http://111.160.8.74:888/OM_Data/OM_CXHZ.aspx?id=" + st_id);
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('您的用户名或者密码错误！！！')</script>");
            }
        }
  

        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
