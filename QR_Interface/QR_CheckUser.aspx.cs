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

namespace ZCZJ_DPF.QR_Interface
{
    public partial class QR_CheckUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserName = Request.Form["UserName"].ToString().Trim();
            string password = Request.Form["password"].ToString().Trim();
            string result = "";
            string strText = "";
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            password = ed.MD5Encrypt(password, "!@#$%^&*");

            strText = "select ST_ID,ST_NAME,ST_PASSWORD,ST_DEPID from TBDS_STAFFINFO where (ST_CODE='" + UserName + "' or ST_NAME = '" + UserName + "') and ST_PASSWORD='" + password + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(strText);
            if (dt.Rows.Count > 0)
            {
                result = "{\"DEPID\":\"" + dt.Rows[0]["ST_DEPID"].ToString().Trim() + "\",\"result\":\"success\",\"msg\":\"登录成功!\"}";
            }
            else
            {
                result = "{\"result\":\"fault\",\"msg\":\"用户名或密码错误!\"}";
            }
            Response.Write(result);
        }
    }
}
