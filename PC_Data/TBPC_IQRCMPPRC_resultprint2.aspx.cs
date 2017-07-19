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
    public partial class TBPC_IQRCMPPRC_resultprint2 : System.Web.UI.Page
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
                string sqltext = "";
                int i=0;
                sqltext = "select supplieranm as suppa,supplierbnm as suppb,suppliercnm as suppc," +
                          "supplierdnm as suppd,supplierenm as suppe,supplierfnm as suppf  " +
                          "from View_TBPC_IQRCMPPRICE where picno='" + gloabsheetno + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "")
                    {
                        i = 1;
                    }
                    else if (dt.Rows[0][1].ToString() == "")
                    {
                        i = 1;
                    }
                    else if (dt.Rows[0][2].ToString() == "")
                    {
                        i = 1;
                    }
                    else if (dt.Rows[0][3].ToString() == "")
                    {
                        i = 1;
                    }
                    else if (dt.Rows[0][4].ToString() == "")
                    {
                        i = 1;
                    }
                    else if (dt.Rows[0][5].ToString() == "")
                    {
                        i = 1;
                    }
                    else
                    {
                        i = 1;
                    }
                }
                if (i > 0)
                {
                    ReportViewer1.LocalReport.ReportPath = MapPath("~/PC_Data/TBPC_IQRCMPPRC_resultprint"+i.ToString()+".rdlc");
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = MapPath("~/PC_Data/TBPC_IQRCMPPRC_resultprint1.rdlc");
                }
                initpage();
            }
        }
        private void initpage()
        {
            DataTable ddt1 = GetdetailData();
            ReportDataSource rds = new ReportDataSource("orderdataset_irqtbdetailbjd", ddt1);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            //ReportViewer1.LocalReport.Refresh();
            DataTable tdt1 = GettotalData();
            rds = new ReportDataSource("orderdataset_irqtbtotalbjd", tdt1);
            //ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();

        }
        private DataTable GetdetailData()
        {
            string shape = "";
            string sqltext = "";           
            sqltext = "select picno as sheetnore,marnm as marnm,isnull(PIC_TUHAO,'')+'  '+margg+'  '+marcz+'  '+margb as marguige," +
                        "marcz as marcaizhi,margb as marguobiao,marunit as marunit," +
                        "cast(marzxnum as float) as marnum,cast(marzxfznum as float) as marfznum,ptcode as ptcode,cast(price as float) as marprice,cast(detamount as float) as maramount," +
                        "supplierresnm as supresult,supplieranm as suppa,cast(qoutelstsa as float) as suppaprice," +
                        "supplierbnm as suppb,cast(qoutelstsb as float) as suppbprice,suppliercnm as suppc,cast(qoutelstsc as float) as suppcprice," +
                        "supplierdnm as suppd,cast(qoutelstsd as float) as suppdprice,supplierenm as suppe,cast(qoutelstse as float) as suppeprice," +
                        "supplierfnm as suppf,cast(qoutelstsf as float) as suppfprice,PIC_TUHAO,length,width,"+
                        "case when PIC_MASHAPE='定尺板' then cast(length as varchar)+'*'+cast(width as varchar)+'  '+cast(cast(marzxfznum as float) as varchar)+marfzunit   " +
                        "when (PIC_MASHAPE='型材' or PIC_MASHAPE='协A' or PIC_MASHAPE='协B') then cast(cast(marzxfznum as float) as varchar)+marfzunit " +
                        "else '' end as note  "+
                        "from View_TBPC_IQRCMPPRICE where picno='" + gloabsheetno + "' order by ptcode ASC";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int rowsnum = dt.Rows.Count;
            for (int i = rowsnum + 1; i <= 1; i++)
            {
                DataRow dr = dt.NewRow();
                dr["sheetnore"] = "";
                dr["marnm"] = "";
                dr["PIC_TUHAO"] = "";
                dr["marguige"] = "";
                dr["marcaizhi"] = "";
                dr["marguobiao"] = "";
                dr["length"] = DBNull.Value;
                dr["width"] = DBNull.Value;
                dr["marunit"] = "";
                dr["marnum"] = DBNull.Value;
                dr["marfznum"] = DBNull.Value;
                dr["ptcode"] = "";
                dr["marprice"] = DBNull.Value;
                dr["maramount"] = DBNull.Value;
                dr["marunit"] = "";
                dr["supresult"] = "";
                dr["suppa"] = "";
                dr["suppaprice"] = DBNull.Value;
                dr["suppb"] = "";
                dr["suppbprice"] = DBNull.Value;
                dr["suppc"] = "";
                dr["suppcprice"] = DBNull.Value;
                dr["suppd"] = "";
                dr["suppdprice"] = DBNull.Value;
                dr["suppe"] = "";
                dr["suppeprice"] = DBNull.Value;
                dr["suppf"] = "";
                dr["suppfprice"] = DBNull.Value;
                dr["note"] = "";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable GettotalData()
        {
            string sqltext = "";
            sqltext = "select distinct picno as sheetnore,zdtime as tijiaotime,zdryj," +
                      "zdrnm as cgrnm from View_TBPC_IQRCMPPRCRVW where picno='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            return dt;
        }
    }
}
