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
    public partial class SM_WarehouseOUT_Print_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

            string outcode = Request.QueryString["outcode"];
            string operstate = Request.QueryString["oper"];

            //DataTable dt = GetData(outcode);
            DataTable dt = GetData(outcode, operstate);

            //报表的数据源
            //数据源的名称==DataSet_SM_SM_OUT

            //数据源的值=dt；
            ReportDataSource rds = new ReportDataSource("DataSet_SM_SM_OUT", dt);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();

            //}
        }

        // private DataTable GetData(string code)
        private DataTable GetData(string code, string operstate)
        {
            DataTable dt = new DataTable("dataname");

            dt.Columns.Add(new DataColumn("BH", typeof(string)));
            dt.Columns.Add(new DataColumn("YDLX", typeof(string)));
            dt.Columns.Add(new DataColumn("RQ", typeof(string)));
            dt.Columns.Add(new DataColumn("LLBM", typeof(string)));
            dt.Columns.Add(new DataColumn("WLBM", typeof(string)));
            dt.Columns.Add(new DataColumn("WLMC", typeof(string)));
            dt.Columns.Add(new DataColumn("GGXH", typeof(string)));
            dt.Columns.Add(new DataColumn("CZ", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("SL", typeof(double)));
            dt.Columns.Add(new DataColumn("PH", typeof(string)));
            dt.Columns.Add(new DataColumn("FLCK", typeof(string)));
            dt.Columns.Add(new DataColumn("FLCW", typeof(string)));
            dt.Columns.Add(new DataColumn("YDDH", typeof(string)));
            dt.Columns.Add(new DataColumn("BZ", typeof(string)));
            dt.Columns.Add(new DataColumn("SHR", typeof(string)));
            dt.Columns.Add(new DataColumn("FLR", typeof(string)));
            dt.Columns.Add(new DataColumn("ZDR", typeof(string)));
            dt.Columns.Add(new DataColumn("ENGID", typeof(string)));
            dt.Columns.Add(new DataColumn("PTC", typeof(string)));
            dt.Columns.Add(new DataColumn("ZDRQ", typeof(string)));
            dt.Columns.Add(new DataColumn("GB", typeof(string)));
            dt.Columns.Add(new DataColumn("BSH", typeof(string)));

            dt.Columns.Add(new DataColumn("ID", typeof(string)));


            //string sql = "select * from dbo.MS_OUT_Print ('" + code + "') order by UniqueCode";
            string sql = "select * from dbo.MS_OUT_Print ('" + code + "','" + operstate + "') order by UniqueCode";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();

                    row["BH"] = dr["BillType"].ToString();

                    row["YDLX"] = dr["PlanMode"].ToString();

                    row["RQ"] = dr["ApprovedDate"].ToString();

                    row["ZDRQ"] = dr["ZDRQ"].ToString();

                    row["LLBM"] = dr["Dep"].ToString();

                    row["WLBM"] = dr["MaterialCode"].ToString();

                    row["WLMC"] = dr["MaterialName"].ToString();

                    row["GGXH"] = dr["Standard"].ToString();

                    row["CZ"] = dr["CZ"].ToString();

                    row["DW"] = dr["Unit"].ToString();

                    row["SL"] = dr["RealNumber"].ToString();

                    row["PH"] = dr["LotNumber"].ToString();

                    row["FLCK"] = dr["Warehouse"].ToString();

                    row["FLCW"] = dr["Location"].ToString();

                    row["YDDH"] = dr["OutCode"].ToString();

                    row["BZ"] = dr["DetailNote"].ToString();

                    row["SHR"] = dr["Verifier"].ToString();

                    row["FLR"] = dr["Sender"].ToString();

                    row["ZDR"] = dr["Doc"].ToString();

                    row["ENGID"] = dr["ENGID"].ToString();

                    row["PTC"] = dr["PTC"].ToString();

                    row["GB"] = dr["GB"].ToString();

                    row["BSH"] = dr["OP_BSH"].ToString();

                    row["ID"] = dr["ID"].ToString();

                    dt.Rows.Add(row);

                }
            }

            dr.Close();

            return dt;
        }
    }
}
