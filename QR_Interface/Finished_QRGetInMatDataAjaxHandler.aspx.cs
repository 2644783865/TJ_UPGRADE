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
    public partial class Finished_QRGetInMatDataAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request.Form["param"].ToString().Trim();
            string result = "";
            string sql = "";
            sql = "select c.CM_COMP as CM_COMP,c.CM_CONTR as CM_CONTR,a.BM_ENGID as BM_ENGID,a.BM_ZONGXU as BM_ZONGXU,c.CM_DFCONTR as CM_DFCONTR,c.CM_PROJ as CM_PROJ,a.BM_CHANAME as BM_CHANAME,a.BM_TUHAO as BM_TUHAO,a.BM_TUUNITWGHT as BM_TUUNITWGHT,a.BM_TUTOTALWGHT as BM_TUTOTALWGHT,a.BM_NUMBER as BM_NUMBER from View_TM_DQO as a left join View_CM_TSAJOINPROJ as b on a.BM_ENGID=b.TSA_ID left join TBCM_PLAN as c on b.TSA_PJID=c.CM_CONTR where BM_ENGID+BM_ZONGXU='" + param + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["CM_COMP"].ToString().Trim() + ";" + dt.Rows[0]["CM_CONTR"].ToString().Trim() + ";" + dt.Rows[0]["BM_ENGID"].ToString().Trim() + ";" + dt.Rows[0]["BM_ZONGXU"].ToString().Trim() + ";" + dt.Rows[0]["CM_DFCONTR"].ToString().Trim() + ";" + dt.Rows[0]["CM_PROJ"].ToString().Trim() + ";" + dt.Rows[0]["BM_CHANAME"].ToString().Trim() + ";" + dt.Rows[0]["BM_TUHAO"].ToString().Trim() + ";" + dt.Rows[0]["BM_TUUNITWGHT"].ToString().Trim() + ";" + dt.Rows[0]["BM_TUTOTALWGHT"].ToString().Trim() + ";" + dt.Rows[0]["BM_NUMBER"].ToString().Trim() + ";;";
            }
            else
            {
                result = "error";
            }
            Response.Write(result);
        }
    }
}
