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
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_CHECKREQUEST_VIEW : System.Web.UI.Page
    {
        string cr_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           cr_id = Request.QueryString["CRid"];
           //DataSetCheckRequest DataSource1 = BindCRData(cr_id);
           ReportDataSource dss = new ReportDataSource();
           dss.Name = "DataSetCheckRequest_TBPM_CHECKREQUEST"; //这一句名称要一样
           //dss.Value = DataSource1.Tables["TBPM_CHECKREQUEST"];
           this.rptView1.LocalReport.DataSources.Add(dss);
           this.rptView1.DataBind();
        }

        //private DataSetCheckRequest BindCRData(string CR_ID)
        //{
        //    string sqlText = "select a.*,b.PCON_BCODE,";
        //    sqlText += " (case when b.PCON_BALANCEACNT!=0 then b.PCON_BALANCEACNT  else  b.PCON_JINE end ) as PCON_JINE,";
        //    sqlText += " b.PCON_NAME,b.PCON_PJID,b.PCON_PJNAME,b.PCON_RSPDEPID,b.PCON_ENGID,b.PCON_ENGNAME,b.PCON_TYPE,";
        //    sqlText += " b.PCON_ENGTYPE,'元:RMB' as PCON_MONUNIT,b.PCON_FILLDATE,b.PCON_VALIDDATE,b.PCON_CUSTMNAME,b.PCON_CUSTMID,b.PCON_ARCHVDATE,";
        //    sqlText += " b.PCON_ADUITDATE,b.PCON_CONTEXT,b.PCON_RESPONSER,b.PCON_PHONENUM,";
        //    sqlText += " b.PCON_DELIVERYDATE,b.PCON_COST,b.PCON_NOTE,b.PCON_FORM,b.PCON_STATE,b.PCON_YFK,b.PCON_YFKLJ,b.PCON_SFKLJ,b.PCON_YFWK,";
        //    sqlText += " b.PCON_ERROR,b.PCON_SPJE,b.PCON_JECHG,b.PCON_QTCHG,b.PCON_FHSJ,b.PCON_YZHTH,";
        //        sqlText += " C.DEP_NAME ";
        //       sqlText+="from TBPM_CHECKREQUEST a  INNER JOIN TBPM_CONPCHSINFO b ON a.CR_HTBH = b.PCON_BCODE ";
        //       sqlText+= " inner join TBDS_DEPINFO c on a.CR_QKBM=c.DEP_CODE where a.CR_ID='" + CR_ID + "'";
        //    DataSetCheckRequest ds = new DataSetCheckRequest();
        //    SqlConnection sqlConn = new SqlConnection();
        //    sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
        //    SqlDataAdapter sda = new SqlDataAdapter(sqlText, sqlConn);
        //    sda.Fill(ds,"TBPM_CHECKREQUEST");
        //    return ds;
        //}
    }
}
