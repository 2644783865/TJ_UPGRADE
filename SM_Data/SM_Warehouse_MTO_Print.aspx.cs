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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_MTO_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mtocode = Request.QueryString["mtocode"];

                DataTable dt = GetData(mtocode);

                //报表的数据源
                ReportDataSource rds = new ReportDataSource("DataSet_SM_SM_MTO", dt);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();

            }
        }

        private DataTable GetData(string code)
        {
            DataTable dt = new DataTable("dataname");

            dt.Columns.Add(new DataColumn("BH", typeof(string)));
            dt.Columns.Add(new DataColumn("RQ", typeof(string)));
            dt.Columns.Add(new DataColumn("BZ", typeof(string)));
            dt.Columns.Add(new DataColumn("WLDM", typeof(string)));
            dt.Columns.Add(new DataColumn("WLMC", typeof(string)));
            dt.Columns.Add(new DataColumn("GGXH", typeof(string)));
            dt.Columns.Add(new DataColumn("CZ", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("PH", typeof(string)));
            dt.Columns.Add(new DataColumn("CK", typeof(string)));
            dt.Columns.Add(new DataColumn("FROMJH", typeof(string)));
            dt.Columns.Add(new DataColumn("TOJH", typeof(string)));
            dt.Columns.Add(new DataColumn("TZSL", typeof(string)));
            dt.Columns.Add(new DataColumn("ZDR", typeof(string)));
            dt.Columns.Add(new DataColumn("JHY", typeof(string)));
            dt.Columns.Add(new DataColumn("SHR", typeof(string)));
            dt.Columns.Add(new DataColumn("BM", typeof(string)));

            string sql = "select * from View_SM_MTO where MTOCode='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();

                    row["BH"] = dr["MTOCode"].ToString();

                    row["RQ"] = dr["VerifyDate"].ToString();

                    row["BZ"] = dr["DetailNote"].ToString();

                    row["WLDM"] = dr["MaterialCode"].ToString();

                    row["WLMC"] = dr["MaterialName"].ToString();

                    row["GGXH"] = dr["Standard"].ToString();

                    row["CZ"] = dr["Attribute"].ToString();

                    row["DW"] = dr["Unit"] == DBNull.Value ? "" : dr["Unit"].ToString();

                    row["PH"] = dr["LotNumber"].ToString();

                    row["CK"] = dr["Warehouse"].ToString();

                    row["FROMJH"] = dr["PTCFrom"].ToString();

                    row["TOJH"] = dr["PTCTo"].ToString();

                    row["TZSL"] = dr["TZNUM"].ToString();

                    row["ZDR"] = dr["Doc"].ToString();

                    row["JHY"] = dr["Planer"].ToString();

                    row["SHR"] = dr["Verifier"].ToString();

                    row["BM"] = dr["Dep"].ToString();

                    dt.Rows.Add(row);

                }
            }

            dr.Close();

            return dt;
        }
    }
}
