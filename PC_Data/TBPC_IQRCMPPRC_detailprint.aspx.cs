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
    public partial class TBPC_IQRCMPPRC_detailprint : System.Web.UI.Page
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

                if (Request.QueryString["sheetno"] != null)
                {
                    gloabsheetno = Request.QueryString["sheetno"].ToString();
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
            ReportDataSource rds = new ReportDataSource("orderdataset_irqtbdetail", ddt1);
            ReportDataSource rds1 = new ReportDataSource("DataSet_DUTY", DBCallCommon.GetDTUsingSqlText("select ST_NAME from TBDS_STAFFINFO where ST_POSITION='0501'"));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            //ReportViewer1.LocalReport.Refresh();
            DataTable tdt1 = GettotalData();
            rds = new ReportDataSource("orderdataset_irqtbtotal", tdt1);
            //ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }
        private DataTable GetdetailData()
        {
            string shape = "";
            string sqltext = "";
            sqltext = "select picno as sheetno,marnm as marnm,isnull(PIC_TUHAO,'')+'  '+margg+'  '+marcz+'  '+margb as marguige,"+
                        "case when PIC_MASHAPE='定尺板' then cast(length as varchar)+'*'+cast(width as varchar)+'  '+cast(cast(marzxfznum as float) as varchar)+marfzunit+'  '+detailnote  " +
                        " when (PIC_MASHAPE='型材' or PIC_MASHAPE='协A' or PIC_MASHAPE='协B') then cast(cast(marzxfznum as float) as varchar)+marfzunit+'  '+detailnote " +
                        "else detailnote end as note , " +
                        "marcz as caizhi,margb as guobiao,marunit as marunit," +
                        "cast(marzxnum as float) as num,cast(marzxfznum as float) as fznum,ptcode as ptcode,PIC_TUHAO,length,width  " +
                        "from View_TBPC_IQRCMPPRICE where picno='" + gloabsheetno + "' order by ptcode ASC";
                   
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int rowsnum = dt.Rows.Count;
            for (int i = rowsnum + 1; i <= 1; i++)
            {
                DataRow dr = dt.NewRow();
                dr["sheetno"] = "";
                dr["marnm"] = "";
                dr["PIC_TUHAO"] = "";
                dr["marguige"] = "";
                dr["caizhi"] = "";
                dr["guobiao"] = "";
                dr["length"] = DBNull.Value;
                dr["width"] = DBNull.Value;
                dr["marunit"] = "";
                dr["num"] = DBNull.Value;
                dr["fznum"] = DBNull.Value;
                dr["ptcode"] = "";
                dr["note"] = "";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable GettotalData()
        {
            string sqltext = "";
            sqltext = "select distinct picno as sheetno,CONVERT(varchar(12) , irqdata, 102 ) as bjdtime,ST_TEL as tel,ST_FAX as fax,ST_EMAIL as email," +
                      "zdrnm as cgrnm from View_TBPC_IQRCMPPRCRVW where picno='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            return dt;
        }
    }
}
