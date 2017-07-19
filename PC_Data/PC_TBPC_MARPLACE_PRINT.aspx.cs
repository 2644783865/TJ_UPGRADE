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
    public partial class PC_TBPC_MARPLACE_PRINT : System.Web.UI.Page
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
            ReportDataSource rds = new ReportDataSource("orderdataset_dyddetail", ddt1);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            //ReportViewer1.LocalReport.Refresh();

            DataTable tdt1 = GettotalData();
            rds = new ReportDataSource("orderdataset_dydtotal", tdt1);
            //ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
            string sql = "select MP_SFDY from TBPC_MARREPLACETOTAL where MP_CODE='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                string sfdy = dt.Rows[0]["MP_SFDY"].ToString();
                if (sfdy != "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('此单据已经打印过"+sfdy+"次！');", true);
                }
            }
        }
        private DataTable GetdetailData()
        {
            string shape = "";
            string sqltext = "";
            sqltext = "SELECT mpcode,ptcode,marid,marnm,marguige+'/'+marcaizhi+'/'+marguobiao as marxinxi, cast(cast(num as float) as varchar)+'('+marcgunit+')' as num, cast(cast(fznum as float) as varchar)+'('+fzunit+')' as fznum, " +
                   " allnote,detailmarid, detailmarnm, detailmarguige+'/'+detailmarcaizhi+'/'+detailmarguobiao as demarxinxi, " +
                   " cast(cast(detailmarnuma as float) as varchar)+'('+detailmarunit+')' as detailnum,cast(cast(detailmarnumb as float) as varchar)+'('+detailfzunit+')' as detailfznum, " +
                   " detailnote, length, width, detaillength, detailwidth  " +
                   "FROM View_TBPC_MARREPLACE_total_all_detail " +
                   "WHERE mpcode='" + gloabsheetno + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int rowsnum = dt.Rows.Count;
            for (int i = rowsnum + 1; i <= 1; i++)
            {
                DataRow dr = dt.NewRow();
                dr["mpcode"] = "";
                dr["ptcode"] = "";
                dr["marid"] = "";
                dr["marnm"] = "";
                dr["marxinxi"] = "";
                dr["num"] = "";
                dr["fznum"] = "";
                dr["allnote"] = "";
                dr["detailmarid"] = "";
                dr["detailmarnm"] = "";
                dr["demarxinxi"] = "";
                dr["detailnum"] = "";
                dr["detailfznum"] = "";
                dr["detailnote"] = "";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable GettotalData()
        {
            string sqltext = "";
            sqltext = "SELECT mpcode, zdreson, zdtime," +
                     " totalstate,totalnote, " +
                     "zdrnm, shranm, shrbnm, engid, pjid,pjnm, engnm  " +
                     "FROM View_TBPC_MARREPLACE_total_planrvw where mpcode='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            return dt;
        }
    }
}
