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
    public partial class QRGetStorageAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "";
            string wlnum = Request.Form["wlnum"].ToString().Trim();
            string sql = "select sum(SQ_NUM) as SQ_NUM from TBWS_STORAGE where SQ_MARID='" + wlnum + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SQ_NUM"].ToString().Trim() != "")
                {
                    result = "{\"result\":\"" + dt.Rows[0]["SQ_NUM"].ToString().Trim() + "\"}";
                }
                else
                {
                    result = "{\"result\":\"0\"}";
                }
            }
            else
            {
                result = "{\"result\":\"0\"}";
            }
            Response.Write(result);
        }
    }
}
