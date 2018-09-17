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
    public partial class Finished_QRGetOutMatDataAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request.Form["param"].ToString().Trim();
            string result = "";
            string sql = "";
            sql = "select CM_BIANHAO,CM_CUSNAME,C.CM_CONTR as CM_CONTR,BM_TASKID,BM_ZONGXU,d.CM_DFCONTR as CM_DFCONTR,C.CM_PROJ as CM_PROJ,BM_CHANAME,BM_TUHAO,BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_YFNUM,KC_KCNUM,BM_NUMBER,C.CM_SH as CM_SH,C.CM_JH as CM_JH,C.CM_CUSNAME as CM_CUSNAME,C.CM_BEIZHU as CM_BEIZHU from TBPM_STRINFODQO as  A left join TBMP_FINISHED_STORE as B ON (A.BM_ENGID=B.KC_TSA AND A.BM_ZONGXU=B.KC_ZONGXU) left join View_CM_FaHuo as C  on A.BM_ENGID=C.TSA_ID and A.BM_ZONGXU=C.ID left join TBCM_PLAN as d on C.CM_CONTR=d.CM_CONTR where BM_TASKID+BM_ZONGXU='" + param + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            string kcnum = "0";
            string yf = "0";
            if (dt.Rows[0]["KC_KCNUM"].ToString().Trim() != "")
            {
                kcnum = dt.Rows[0]["KC_KCNUM"].ToString().Trim();
            }
            if (dt.Rows[0]["BM_YFNUM"].ToString().Trim() != "")
            {
                yf = dt.Rows[0]["BM_YFNUM"].ToString().Trim();
            }

            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["CM_BIANHAO"].ToString().Trim() + ";" + dt.Rows[0]["CM_CUSNAME"].ToString().Trim() + ";" + dt.Rows[0]["CM_CONTR"].ToString().Trim() + ";" + dt.Rows[0]["BM_TASKID"].ToString().Trim() + ";" + dt.Rows[0]["BM_ZONGXU"].ToString().Trim() + ";" + dt.Rows[0]["CM_DFCONTR"].ToString().Trim() + ";" + dt.Rows[0]["CM_PROJ"].ToString().Trim() + ";" + dt.Rows[0]["BM_CHANAME"].ToString().Trim() + ";" + dt.Rows[0]["BM_TUHAO"].ToString().Trim() + ";" + dt.Rows[0]["BM_TUUNITWGHT"].ToString().Trim() + ";" + dt.Rows[0]["BM_TUTOTALWGHT"].ToString().Trim() + ";" + yf + ";" + kcnum + ";" + dt.Rows[0]["CM_SH"].ToString().Trim() + ";" + dt.Rows[0]["CM_JH"].ToString().Trim() + ";" + dt.Rows[0]["CM_CUSNAME"].ToString().Trim() + ";" + dt.Rows[0]["CM_BEIZHU"].ToString().Trim().Replace(";",".").Replace("\r", "").Replace("\n", "") +";;";
            }
            else
            {
                result = "error";
            }
            Response.Write(result);
        }
    }
}
