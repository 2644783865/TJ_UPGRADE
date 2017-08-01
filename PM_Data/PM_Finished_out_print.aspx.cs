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
using System.Data.SqlClient;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Finished_out_print : System.Web.UI.Page
    {
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["ID"];
            if (string.IsNullOrEmpty(id))
            {
                Response.Redirect("../ErrorPage.aspx");
            }
            BindData();
        }

        private void BindData()
        {
            BindHeader();
            BindRepeater();
        }

        private void BindHeader()
        {
            txt_docnum.Text = id.ToString();
            string sqlHeader = string.Format("SELECT * FROM View_TBMP_FINISHED_OUT WHERE TFO_DOCNUM = '{0}'", id.ToString());
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlHeader);
            while (dr.Read())
            {
                Tb_shijian.Text = dr["OUTDATE"].ToString();
                txt_note.Text = dr["NOTE"].ToString();
                Tb_fuziren.Text = dr["REVIEWANAME"].ToString();
                Tb_shenqingren.Text = dr["SQRNAME"].ToString();
                tb_executor.Text = dr["DocuPersonNAME"].ToString();
            }
        }

        private void BindRepeater()
        {
            string sqlRepeater = "select B.CM_ID, B.CM_PROJ,B.CM_FHNUM,B.CM_BIANHAO,B.CM_FID,B.CM_CONTR,A.TSA_ID,B.TSA_MAP,B.TSA_ENGNAME,B.TSA_NUMBER,B.CM_JHTIME,A.*,c.* from TBMP_FINISHED_OUT AS A LEFT OUTER JOIN View_CM_FaHuo AS B ON (A.TSA_ID=B.TSA_ID AND A.TFO_ENGNAME=B.TSA_ENGNAME AND A.TFO_MAP=B.TSA_MAP AND A.TFO_FID=B.CM_FID AND A.TFO_ZONGXU=B.ID)  left join TBMP_FINISHED_STORE as c on a.TSA_ID=c.KC_TSA and a.TFO_ZONGXU=KC_ZONGXU where A.TFO_DOCNUM='" + id.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlRepeater);
            PM_Finished_PrintRepeater.DataSource = dt;
            PM_Finished_PrintRepeater.DataBind();
        }
    }
}
