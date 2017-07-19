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
    public partial class SM_Warehouse_ProjTemp_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ptcode = Request.QueryString["ptcode"];

               DataTable dt = GetData(ptcode);

                //报表的数据源
                ReportDataSource rds = new ReportDataSource("DataSet_SM_SM_PROJTEMP", dt);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();

        }

        private DataTable GetData(string code)
        {
            DataTable dt = new DataTable("dataname");

            dt.Columns.Add(new DataColumn("BH", typeof(string)));
            dt.Columns.Add(new DataColumn("WLCODE", typeof(string)));
            dt.Columns.Add(new DataColumn("WLNAME", typeof(string)));
            
            dt.Columns.Add(new DataColumn("GGXH", typeof(string)));
            dt.Columns.Add(new DataColumn("CAIZHI", typeof(string)));
            dt.Columns.Add(new DataColumn("CPTC", typeof(string)));
            dt.Columns.Add(new DataColumn("PH", typeof(string)));
           
            dt.Columns.Add(new DataColumn("DPTC", typeof(string)));
            dt.Columns.Add(new DataColumn("UNIT", typeof(string)));
            dt.Columns.Add(new DataColumn("TNUM", typeof(string)));
            dt.Columns.Add(new DataColumn("ZDR", typeof(string)));
            dt.Columns.Add(new DataColumn("SHDATE", typeof(string)));
            dt.Columns.Add(new DataColumn("SHR", typeof(string)));
            dt.Columns.Add(new DataColumn("BM", typeof(string)));

            string sql = "select * from View_SM_PROJTEMP where PT_CODE='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();

                    row["BH"] = dr["PT_CODE"].ToString();

                    row["WLCODE"] = dr["MaterialCode"].ToString();

                    row["WLNAME"] = dr["MaterialName"].ToString();

                    row["GGXH"] = dr["Standard"].ToString();

                    row["CAIZHI"] = dr["Attribute"].ToString();

                    row["PH"] = dr["LotNumber"].ToString();

                    row["TNUM"] = dr["TNUM"].ToString();

                    row["UNIT"] = dr["Unit"] == DBNull.Value ? "" : dr["Unit"].ToString();

                    row["CPTC"] = dr["PTCFrom"].ToString();

                    row["DPTC"] = dr["PTCTo"].ToString();

                    row["SHR"] = dr["verifername"].ToString();

                    row["ZDR"] = dr["docname"].ToString();

                    row["SHDATE"] = dr["PT_VERIFYDATE"].ToString();

                    row["BM"] = dr["depname"].ToString();

                    

                    dt.Rows.Add(row);

                }
            }

            dr.Close();

            return dt;
        }
    }
}
