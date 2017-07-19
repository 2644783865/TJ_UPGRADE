using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_JSD : System.Web.UI.Page
    {
        string sql = "";        
        string htbh = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["condetail_id"] != null)
            {
                htbh = Request.QueryString["condetail_id"]; // 合同编号    
            }

            initpage();
        }


        private void initpage()
        {
            DataTable tdt = GetDetailData();
            ReportDataSource rds = new ReportDataSource("cmjsddataset_jsddetail", tdt);  
            Rv3.LocalReport.DataSources.Clear();
            Rv3.LocalReport.DataSources.Add(rds);

            DataTable tdt1 = GetTotalData();            
            rds = new ReportDataSource("cmjsddataset_jsdtotal", tdt1);
            Rv3.LocalReport.DataSources.Add(rds);
            Rv3.LocalReport.Refresh();

        }

        private DataTable GetDetailData()
        {
            string sqltext = "select b.mname as marnm,b.guige as margg,b.caizhi as marcz,b.gb as margb,b.purcunit as marunit," +
                                   " a.htnum as zxnum, a.htunitprice as ctprice,a.htsummoney as ctamount,(a.inputnum+a.addnum) as recgdnum,(a.jsmoney+a.balance) as sjamount,a.balance,a.note as bezhu" +
                                   " from TBCM_JSDMARINFO as a,TBMA_MATERIAL as b where a.marid=b.id and conid='" + htbh + "'";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext);
            return dt2;
        }

        private DataTable GetTotalData()
        {
            string time = "";
            
            string bankname = "";//开户行
            string bankaccount = "";//银行帐号
            sql = " select * from TBCM_JSDDETAIL where conid='"+htbh+"' ";
            DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt5.Rows.Count > 0)
            {
                time = dt5.Rows[0]["JSDDATE"].ToString();                
                bankname = dt5.Rows[0]["DEPOSITBANK"].ToString();
                bankaccount = dt5.Rows[0]["BANKACUNUM"].ToString();
            }
            
            sql = "select '" + time + "' as PCON_RIQI,PCON_NAME,";
            sql += " PCON_BCODE,PCON_CUSTMNAME,'" + bankname + "' as PCON_DEPOSITBANK,'" + bankaccount + "' as PCON_BANKACUNUM,PCON_BALANCEACNT";           
            sql += "  from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "' ";
            DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql);

            return dt3;
        }
        
    }
}
