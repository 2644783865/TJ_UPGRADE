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
    public partial class CR_NotContractOrder_Export : System.Web.UI.Page
    {
        string dq_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            dq_id = Request.QueryString["CQID"];
            CR_NotContractOrder_DataSet DataSource1 = BingCQData(dq_id);
            ReportDataSource dss = new ReportDataSource();
            dss.Name = "CR_NotContractOrder_DataSet_TBPM_ORDER_CHECKREQUEST";
            dss.Value = DataSource1.Tables["TBPM_ORDER_CHECKREQUEST"];
            this.rptView1.LocalReport.DataSources.Add(dss);
            this.rptView1.DataBind();
        }

        private CR_NotContractOrder_DataSet BingCQData(string dq_id)
        {
            string sqlText = "select DQ_ID,DQ_DATA,DQ_QKBM,DQ_BMNAME,DQ_QKR,DQ_USE,DQ_DDCODE,DQ_DDZJE,DQ_CSCODE,DQ_CSNAME,DQ_CSBANK,DQ_CSACCOUNT,DQ_AMOUNT,DQ_AMOUNTDX,DQ_ZFFS,DQ_BILLCODE,DQ_STATE,DQ_BMYJ,DQ_ZGLD,DQ_GSLD,DQ_CWSH,DQ_NOTE from TBPM_ORDER_CHECKREQUEST";
            sqlText += " where DQ_ID='" + dq_id + "'";
            CR_NotContractOrder_DataSet ds = new CR_NotContractOrder_DataSet();
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            SqlDataAdapter sda = new SqlDataAdapter(sqlText, sqlConn);
            sda.Fill(ds,"TBPM_ORDER_CHECKREQUEST");
            return ds;
        }
    }
}
