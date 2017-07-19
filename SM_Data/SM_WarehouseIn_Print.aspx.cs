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
    public partial class SM_WarehouseIn_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //if (!IsPostBack)
            //{

                string incode = Request.QueryString["incode"];

               
                string operstate = Request.QueryString["oper"];


                DataTable dt = GetData(incode, operstate);

                //报表的数据源
                ReportDataSource rds = new ReportDataSource("DataSet_SM_SM_IN", dt);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
 
            //}

        }

        private DataTable GetData(string code, string operstate)
        {
            DataTable dt = new DataTable("dataname");

            dt.Columns.Add(new DataColumn("DYBH", typeof(string)));
            dt.Columns.Add(new DataColumn("GYS", typeof(string)));
            dt.Columns.Add(new DataColumn("BH", typeof(string)));
            dt.Columns.Add(new DataColumn("SLCK", typeof(string)));
            dt.Columns.Add(new DataColumn("RQ", typeof(string)));
            dt.Columns.Add(new DataColumn("YDLX", typeof(string)));
            dt.Columns.Add(new DataColumn("YDDH", typeof(string)));
            dt.Columns.Add(new DataColumn("WLBM", typeof(string)));
            dt.Columns.Add(new DataColumn("WLMC", typeof(string)));
            dt.Columns.Add(new DataColumn("GGXH", typeof(string)));
            dt.Columns.Add(new DataColumn("CZ", typeof(string)));
            dt.Columns.Add(new DataColumn("PH", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("YSSL", typeof(string)));
            dt.Columns.Add(new DataColumn("SSSL", typeof(double)));
            dt.Columns.Add(new DataColumn("DJ", typeof(string)));
            dt.Columns.Add(new DataColumn("JE", typeof(double)));
            dt.Columns.Add(new DataColumn("BZ", typeof(string)));
            dt.Columns.Add(new DataColumn("SHR", typeof(string)));
            dt.Columns.Add(new DataColumn("ZDR", typeof(string)));
            dt.Columns.Add(new DataColumn("SLR", typeof(string)));
            dt.Columns.Add(new DataColumn("JBR", typeof(string)));
            dt.Columns.Add(new DataColumn("DYR", typeof(string)));
            dt.Columns.Add(new DataColumn("PTC", typeof(string)));
            dt.Columns.Add(new DataColumn("BILLTYPE", typeof(string)));
            dt.Columns.Add(new DataColumn("OrderCode", typeof(string)));
            //入库单号
            string sql = "select * from dbo.MS_IN_Print ('" + code + "','"+ operstate +"') order by UNIQUEID";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if(dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();


                    row["DYBH"] = dr["ID"].ToString();

                    row["GYS"] = dr["SupplierName"].ToString();

                    row["BH"] = dr["WG_HDBH"].ToString();

                    row["SLCK"] = dr["WS_NAME"].ToString();

                    row["RQ"] = dr["WG_VERIFYDATE"].ToString();

                    row["YDLX"] = dr["WG_PMODE"].ToString();

                    row["YDDH"] = dr["WG_CODE"].ToString();

                    row["WLBM"] = dr["WG_MARID"].ToString();

                    row["WLMC"] = dr["MNAME"].ToString();

                    row["GGXH"] = dr["GUIGE"].ToString();

                    row["CZ"] = dr["CAIZHI"].ToString();//材质

                    row["PH"] = dr["WG_LOTNUM"].ToString();

                    row["DW"] = dr["CGDW"].ToString();

                    row["YSSL"] = dr["WG_RSNUM"].ToString();

                    row["SSSL"] = dr["WG_RSNUM"].ToString();

                    row["DJ"] = dr["UPRICE"].ToString();//单价

                    row["JE"] = dr["WG_AMOUNT"].ToString();//金额

                    row["BZ"] = dr["WG_NOTE"].ToString();

                    row["SHR"] = dr["VerfierName"].ToString();

                    row["ZDR"] = dr["DocName"].ToString();

                    row["SLR"] = dr["ReveicerName"].ToString();

                    row["JBR"] = dr["ClerkName"].ToString();

                    row["DYR"] = Session["UserName"].ToString();

                    row["PTC"] = dr["PTC"].ToString();

                    row["BILLTYPE"] = dr["BILLTYPE"].ToString();

                    row["OrderCode"] = dr["WG_ORDERID"].ToString();
                    
                    dt.Rows.Add(row);

                }
            }

            dr.Close();

            return dt;
        }
    }
}
