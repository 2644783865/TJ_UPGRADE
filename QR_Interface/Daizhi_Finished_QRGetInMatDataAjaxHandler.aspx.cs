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
    public partial class Daizhi_Finished_QRGetInMatDataAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request.Form["param"].ToString().Trim();
            List<string> list = new List<string>();
            string result = "";
            string sql = "";
            sql = "select * from midTable_DzFinished_management where MapCode+';'+InProdCode='" + param + "'";
            string[] paramarr = param.Split(';');
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["OldYzName"].ToString().Trim() + ";" + dt.Rows[0]["OldHtCode"].ToString().Trim() + ";" + dt.Rows[0]["OldTaskCode"].ToString().Trim() + ";" + dt.Rows[0]["OldYzHtCode"].ToString().Trim() + ";" + dt.Rows[0]["OldProjName"].ToString().Trim() + ";" + dt.Rows[0]["ProdName"].ToString().Trim() + ";" + dt.Rows[0]["MapCode"].ToString().Trim() + ";" + dt.Rows[0]["Caizhi"].ToString().Trim() + ";" + dt.Rows[0]["InProdCode"].ToString().Trim() + ";;" + dt.Rows[0]["InUnit"].ToString().Trim() + ";;" + dt.Rows[0]["SingleWeight"].ToString().Trim() + ";" + dt.Rows[0]["ZnZw"].ToString().Trim() + ";" + dt.Rows[0]["IfERP"].ToString().Trim() + ";";
            }
            else
            {
                result = ";;;;;;" + paramarr[0].ToString().Trim() + ";;" + paramarr[1].ToString().Trim() + ";;;;;;;";
            }
            Response.Write(result);
        }
    }
}
