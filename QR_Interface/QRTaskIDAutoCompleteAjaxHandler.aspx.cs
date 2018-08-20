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
    public partial class QRTaskIDAutoCompleteAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string term = Request["term"].ToString().Trim();
            string sql = "select top(10) TSA_ID from (SELECT TSA_ID FROM View_TM_TaskAssign union select TSA_ID from TBPM_DETAIL)t where TSA_ID like '%" + term + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string result = "";
            if (dt.Rows.Count > 0)
            {
                result += "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result += "\"" + dt.Rows[i]["TSA_ID"].ToString().Trim() + "\",";
                }
                result = result.Substring(0, result.Length - 1);
                result += "]";
            }
            else
            {
                result = "[\"暂无匹配项!\"]";
            }
            Response.Write(result);
        }
    }
}
