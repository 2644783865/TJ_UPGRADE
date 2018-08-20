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
using System.Collections.Generic;

namespace ZCZJ_DPF.QR_Interface
{
    public partial class QRGetOutMatDataAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request.Form["param"].ToString().Trim();
            List<string> list = new List<string>();
            string result = "";
            string sql = "";
            sql = "select top(1) marid,marnm,margg,marcz,margb,marunit from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where marid='" + param + "' order by orderno desc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["marid"].ToString().Trim() + ";" + dt.Rows[0]["marnm"].ToString().Trim() + ";" + dt.Rows[0]["margg"].ToString().Trim() + ";" + dt.Rows[0]["marcz"].ToString().Trim() + ";" + dt.Rows[0]["margb"].ToString().Trim() + ";;;" + dt.Rows[0]["marunit"].ToString().Trim() + ";";
            }
            else
            {
                result = "error";
            }
            Response.Write(result);
        }
    }
}
