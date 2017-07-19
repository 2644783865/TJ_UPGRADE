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
    public partial class SM_Warehouse_Allocation_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string alcode = Request.QueryString["alcode"];

                DataTable dt = GetData(alcode);

                //报表的数据源
                ReportDataSource rds = new ReportDataSource("DataSet_SM_SM_ALLOCATION", dt);

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
            dt.Columns.Add(new DataColumn("TBLX", typeof(string)));
            dt.Columns.Add(new DataColumn("WLDM", typeof(string)));
            dt.Columns.Add(new DataColumn("WLMC", typeof(string)));
            dt.Columns.Add(new DataColumn("GGXH", typeof(string)));
            dt.Columns.Add(new DataColumn("CZ", typeof(string)));
            dt.Columns.Add(new DataColumn("PH", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("SL", typeof(string)));
            dt.Columns.Add(new DataColumn("TRCK", typeof(string)));
            dt.Columns.Add(new DataColumn("TCCK", typeof(string)));
            dt.Columns.Add(new DataColumn("BZ", typeof(string)));
            dt.Columns.Add(new DataColumn("SHR", typeof(string)));
            dt.Columns.Add(new DataColumn("YSR", typeof(string)));
            dt.Columns.Add(new DataColumn("YWY", typeof(string)));
            dt.Columns.Add(new DataColumn("BGR", typeof(string)));
            dt.Columns.Add(new DataColumn("ZDR", typeof(string)));

            string sql = "select * from View_SM_Allocation where ALCode='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();

                    row["BH"] = dr["ALCode"].ToString();

                    row["RQ"] = dr["VerifyDate"].ToString();

                    row["TBLX"] = dr["PlanMode"].ToString();

                    row["WLDM"] = dr["MaterialCode"].ToString();

                    row["WLMC"] = dr["MaterialName"].ToString();

                    row["GGXH"] = dr["Standard"].ToString();

                    row["CZ"] = dr["Attribute"].ToString();

                    row["PH"] = dr["LotNumber"].ToString();

                    row["DW"] = dr["Unit"] == DBNull.Value ? "" : dr["Unit"].ToString();

                    row["SL"] = dr["KTNUM"].ToString();

                    row["TRCK"] = dr["WarehouseIn"].ToString();

                    row["TCCK"] = dr["WarehouseOut"].ToString();

                    row["BZ"] = dr["DetailNote"].ToString();




                    row["SHR"] = dr["Verifier"].ToString();

                    row["YSR"] = dr["Acceptance"].ToString();

                    row["YWY"] = dr["Clerk"].ToString();

                    row["BGR"] = dr["Keeper"].ToString();

                    row["ZDR"] = dr["Doc"].ToString();

                    dt.Rows.Add(row);

                }
            }

            dr.Close();

            return dt;
        }


    }
}
