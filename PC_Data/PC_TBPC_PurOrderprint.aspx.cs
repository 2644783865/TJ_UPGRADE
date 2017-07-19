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

namespace ZCZJ_DPF.PC_Data
{
    [Serializable]
    public partial class PC_TBPC_PurOrderprint : System.Web.UI.Page
    {
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["orderno"] != null)
                {
                    gloabsheetno = Request.QueryString["orderno"].ToString();
                }
                else
                {
                    gloabsheetno = "";
                }
                initpage();
            }
        }

        private void initpage()
        {
            DataTable ddt1 = GetdetailData();
            ReportDataSource rds = new ReportDataSource("orderdataset_ordertb1", ddt1);
            ReportDataSource rds1 = new ReportDataSource("DataSet_DUTY", DBCallCommon.GetDTUsingSqlText("select ST_NAME from TBDS_STAFFINFO where ST_POSITION='0501'"));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            //ReportViewer1.LocalReport.Refresh();
            DataTable tdt1 = GettotalData();
            rds = new ReportDataSource("orderdataset_ordertbtotal", tdt1);
            //ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }
        private DataTable GetdetailData()
        {
            string sqltext = "";

            sqltext = "select orderno as orderno,marnm as marnm,(isnull(PO_TUHAO,'')+'  '+margg+'  '+marcz+'  '+margb) as marguige,marcz as caizhi," +
                        "margb as guobiao,cast(zxnum as float) as num,cast(zxfznum as float) as fznum,marunit as marunit,length,width," +
                        "cast(ctprice as float) as ctprice,cast(ctamount as float) as totamount,cgtimerq as rectime,ptcode as ptcode,PO_TUHAO,PO_MASHAPE," +
                        "case when PO_MASHAPE='定尺板' then cast(length as varchar)+'*'+cast(width as varchar)+'  '+cast(cast(zxfznum as float) as varchar)+marfzunit+'  '+detailnote " +
                        " when (PO_MASHAPE='型材' or PO_MASHAPE='协A' or PO_MASHAPE='协B') then cast(cast(zxfznum as float) as varchar)+marfzunit+'  '+detailnote  " +
                        "else isnull(detailnote,'') end as note,marid "+
                       "from View_TBPC_PURORDERDETAIL where detailcstate='0' and orderno='" + gloabsheetno + "' order by ptcode,marnm,margg asc";
           
           
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int rowsnum=dt.Rows.Count;
            for (int i = rowsnum+1; i <= 1; i++)
            {
                DataRow dr = dt.NewRow();
                dr["orderno"] = "";
                dr["marnm"] = "";
                dr["marguige"] = "";
                dr["caizhi"] = "";
                dr["guobiao"] = "";
                dr["num"] = DBNull.Value;
                dr["fznum"] = DBNull.Value;
                dr["marunit"] = "";
                dr["ctprice"] = DBNull.Value;
                dr["totamount"] = DBNull.Value;
                dr["rectime"] = "";
                dr["ptcode"] = "";
                dr["note"] = "";
                dr["PO_TUHAO"] = "";
                dr["length"] = DBNull.Value;
                dr["width"] = DBNull.Value;
                dr["marid"] = "";
                dr["PO_MASHAPE"] = "";
                dt.Rows.Add(dr);
            }
                return dt;
        }
        private DataTable GettotalData()
        {
            string sqltext = "";
            
            sqltext = "select distinct orderno as orderno,suppliernm as providernm,conname as conperson," +
                      "fax as fax,phono as phone,substring(shtime,1,10) as datetime,zgnm as zgnm,depnm as depnm,ywynm as ywynm," +
                      "zdrnm as zdrnm,abstract as abstract,PO_MASHAPE from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno='" + gloabsheetno + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            return dt;
        }
    }
}
