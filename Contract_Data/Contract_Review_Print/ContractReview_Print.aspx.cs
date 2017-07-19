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
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace ZCZJ_DPF.Contract_Data.Contract_Review_Print
{
    public partial class ContractReview_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cr_id = Request.QueryString["CR_ID"];
            string type = Request.QueryString["type"];
            //if (!IsPostBack)
            //{
                string str_dataset = "";
                DataTable dt=new DataTable();
                switch (type)
                {                   
                    case "0":                    
                        dt = GetData_XS(cr_id);
                        str_dataset = "DataSet_CM_CONREV_dtXS";
                        ReportViewer1.LocalReport.ReportPath = "Contract_Data\\Contract_Review_Print\\ContractReview_XS.rdlc";
                        break;
                    case "1":
                        dt = GetData_CG(cr_id);
                        str_dataset = "DataSet_CM_CONREV_dtCG";
                        ReportViewer1.LocalReport.ReportPath = "Contract_Data\\Contract_Review_Print\\ContractReview_CG.rdlc";
                        break;
                    case "2":
                        dt = GetData_JS(cr_id);
                        str_dataset = "DataSet_CM_CONREV_dtJS";
                        ReportViewer1.LocalReport.ReportPath = "Contract_Data\\Contract_Review_Print\\ContractReview_JS.rdlc";
                        break;
                    case "3":
                        dt = GetData_CY(cr_id);
                        str_dataset = "DataSet_CM_CONREV_dtCY";
                        ReportViewer1.LocalReport.ReportPath = "Contract_Data\\Contract_Review_Print\\ContractReview_CY.rdlc";
                        break;
                    case "4":
                    case "5":
                        dt = GetData_FB(cr_id);
                        str_dataset = "DataSet_CM_CONREV_dtFB";
                        ReportViewer1.LocalReport.ReportPath = "Contract_Data\\Contract_Review_Print\\ContractReview_FB.rdlc";
                        break;
                    case "6":
                        dt = GetData_QT(cr_id);
                        str_dataset = "DataSet_CM_CONREV_dtQT";
                        ReportViewer1.LocalReport.ReportPath = "Contract_Data\\Contract_Review_Print\\ContractReview_QT.rdlc";
                        break;
                    case "7":
                        dt = GetData_DQZZ(cr_id);
                        str_dataset = "DataSet_CM_CONREV_dtXS";
                        ReportViewer1.LocalReport.ReportPath = "Contract_Data\\Contract_Review_Print\\ContractReview_XS.rdlc";
                        break;

                }                
                //报表的数据源
                ReportDataSource rds = new ReportDataSource(str_dataset, dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();

            //}
        }
        //销售
        private DataTable GetData_XS(string code)
        {
            DataTable dt = new DataTable("dtXS");

            dt.Columns.Add(new DataColumn("CR_XMMC", typeof(string)));//项目名称
            dt.Columns.Add(new DataColumn("CR_SBMC", typeof(string)));//设备名称
            dt.Columns.Add(new DataColumn("CR_FBSMC", typeof(string)));//分包商名称
            dt.Columns.Add(new DataColumn("CR_FBFW", typeof(string)));//分包范围

            dt.Columns.Add(new DataColumn("CR_XSBYJ", typeof(string)));//市场部意见：经办人意见+负责人意见
            dt.Columns.Add(new DataColumn("CR_XSJBR", typeof(string)));//市场部经办人
            dt.Columns.Add(new DataColumn("CR_XSFZR", typeof(string)));//市场负责人

            dt.Columns.Add(new DataColumn("CR_JSBYJ", typeof(string)));//技术部意见
            dt.Columns.Add(new DataColumn("CR_JSFZR", typeof(string)));//技术负责人

            dt.Columns.Add(new DataColumn("CR_ZLBYJ", typeof(string)));//质量部意见
            dt.Columns.Add(new DataColumn("CR_ZLFZR", typeof(string)));//质量负责人

            dt.Columns.Add(new DataColumn("CR_SCBYJ", typeof(string)));//生产部意见
            dt.Columns.Add(new DataColumn("CR_SCFZR", typeof(string)));//生产负责人

            dt.Columns.Add(new DataColumn("CR_CWBYJ", typeof(string)));//财务部意见
            dt.Columns.Add(new DataColumn("CR_CWFZR", typeof(string)));//财务负责人

            dt.Columns.Add(new DataColumn("CR_LDYJ", typeof(string)));//领导意见，用分隔符隔开
            dt.Columns.Add(new DataColumn("CR_LD", typeof(string)));//领导签字，用分隔符隔开


            string sql = "select CR_XMMC,CR_SBMC,CR_FBSMC,CR_FBFW from TBCR_CONTRACTREVIEW where cr_id='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CR_XMMC"] = dr["CR_XMMC"].ToString();
                    row["CR_SBMC"] = dr["CR_SBMC"].ToString();
                    row["CR_FBSMC"] = dr["CR_FBSMC"].ToString();
                    row["CR_FBFW"] = dr["CR_FBFW"].ToString();

                    string[] XS_JBYJ = Get_BMYJ(code, "12", "0");//市场经办人
                    string[] XS_FZYJ = Get_BMYJ(code, "12", "1");//市场负责人
                    row["CR_XSJBR"] = XS_JBYJ[1];
                    row["CR_XSFZR"] = XS_FZYJ[1];
                    row["CR_XSBYJ"] = XS_JBYJ[0] + "\r\n" + XS_FZYJ[0];

                    string[] JS_FZYJ = Get_BMYJ(code, "03", "1");//技术负责人
                    row["CR_JSFZR"] = JS_FZYJ[1];
                    row["CR_JSBYJ"] = JS_FZYJ[0];

                    string[] ZL_FZYJ = Get_BMYJ(code, "05", "1");//质量负责人
                    row["CR_ZLFZR"] = ZL_FZYJ[1];
                    row["CR_ZLBYJ"] = ZL_FZYJ[0];

                    string[] SC_FZYJ = Get_BMYJ(code, "04", "1");//生产负责人
                    row["CR_SCFZR"] = SC_FZYJ[1];
                    row["CR_SCBYJ"] = SC_FZYJ[0];                  

                    string[] cw_ld_yj = Get_CW_LD_YJ(code); //财务和领导意见

                    row["CR_CWBYJ"] = cw_ld_yj[0];
                    row["CR_CWFZR"] = cw_ld_yj[1];
                    row["CR_LDYJ"] = cw_ld_yj[2];
                    row["CR_LD"] = cw_ld_yj[3];
                    dt.Rows.Add(row);

                }
            }

            dr.Close();
            return dt;
        }
        //技术
        private DataTable GetData_JS(string code)
        {
            DataTable dt = new DataTable("dtJS");

            dt.Columns.Add(new DataColumn("CR_XMMC", typeof(string)));//项目名称
            dt.Columns.Add(new DataColumn("CR_SBMC", typeof(string)));//设备名称
            dt.Columns.Add(new DataColumn("CR_FBSMC", typeof(string)));//分包商名称
            dt.Columns.Add(new DataColumn("CR_FBFW", typeof(string)));//分包范围

            dt.Columns.Add(new DataColumn("CR_JSBYJ", typeof(string)));//技术部意见：经办人意见+负责人意见
            dt.Columns.Add(new DataColumn("CR_JSJBR", typeof(string)));//技术部经办人
            dt.Columns.Add(new DataColumn("CR_JSFZR", typeof(string)));//技术负责人


            dt.Columns.Add(new DataColumn("CR_ZLBYJ", typeof(string)));//质量部意见
            dt.Columns.Add(new DataColumn("CR_ZLFZR", typeof(string)));//质量负责人

            dt.Columns.Add(new DataColumn("CR_CWBYJ", typeof(string)));//财务部意见
            dt.Columns.Add(new DataColumn("CR_CWFZR", typeof(string)));//财务负责人

            dt.Columns.Add(new DataColumn("CR_LDYJ", typeof(string)));//领导意见，用分隔符隔开
            dt.Columns.Add(new DataColumn("CR_LD", typeof(string)));//领导签字，用分隔符隔开


            string sql = "select CR_XMMC,CR_SBMC,CR_FBSMC,CR_FBFW from TBCR_CONTRACTREVIEW where cr_id='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CR_XMMC"] = dr["CR_XMMC"].ToString();
                    row["CR_SBMC"] = dr["CR_SBMC"].ToString();
                    row["CR_FBSMC"] = dr["CR_FBSMC"].ToString();
                    row["CR_FBFW"] = dr["CR_FBFW"].ToString();

                    string[] JS_JBYJ = Get_BMYJ(code, "03", "0");//技术经办人
                    string[] JS_FZYJ = Get_BMYJ(code, "03", "1");//技术负责人
                    row["CR_JSJBR"] = JS_JBYJ[1];
                    row["CR_JSFZR"] = JS_FZYJ[1];
                    row["CR_JSBYJ"] = JS_JBYJ[0] + "\r\n" + JS_FZYJ[0];

                    string[] ZL_FZYJ = Get_BMYJ(code, "05", "1");//质量负责人
                    row["CR_ZLFZR"] = ZL_FZYJ[1];
                    row["CR_ZLBYJ"] = ZL_FZYJ[0];

                    string[] cw_ld_yj = Get_CW_LD_YJ(code); //财务和领导意见

                    row["CR_CWBYJ"] = cw_ld_yj[0];
                    row["CR_CWFZR"] = cw_ld_yj[1];
                    row["CR_LDYJ"] = cw_ld_yj[2];
                    row["CR_LD"] = cw_ld_yj[3];
                    dt.Rows.Add(row);

                }
            }

            dr.Close();
            return dt; 
        }
        //采购
        private DataTable GetData_CG(string code)
        {
            DataTable dt = new DataTable("dtCG");

            dt.Columns.Add(new DataColumn("CR_XMMC", typeof(string)));//项目名称
            dt.Columns.Add(new DataColumn("CR_SBMC", typeof(string)));//设备名称
            dt.Columns.Add(new DataColumn("CR_FBSMC", typeof(string)));//分包商名称
            dt.Columns.Add(new DataColumn("CR_FBFW", typeof(string)));//分包范围

            dt.Columns.Add(new DataColumn("CR_CGBYJ", typeof(string)));//采购部意见：经办人意见+负责人意见
            dt.Columns.Add(new DataColumn("CR_CGJBR", typeof(string)));//采购部经办人
            dt.Columns.Add(new DataColumn("CR_CGFZR", typeof(string)));//采购负责人

            dt.Columns.Add(new DataColumn("CR_JSBYJ", typeof(string)));//技术部意见
            dt.Columns.Add(new DataColumn("CR_JSFZR", typeof(string)));//技术负责人

            dt.Columns.Add(new DataColumn("CR_ZLBYJ", typeof(string)));//质量部意见
            dt.Columns.Add(new DataColumn("CR_ZLFZR", typeof(string)));//质量负责人

            dt.Columns.Add(new DataColumn("CR_SCBYJ", typeof(string)));//生产部意见
            dt.Columns.Add(new DataColumn("CR_SCFZR", typeof(string)));//生产负责人

            dt.Columns.Add(new DataColumn("CR_CWBYJ", typeof(string)));//财务部意见
            dt.Columns.Add(new DataColumn("CR_CWFZR", typeof(string)));//财务负责人

            dt.Columns.Add(new DataColumn("CR_LDYJ", typeof(string)));//领导意见，用分隔符隔开
            dt.Columns.Add(new DataColumn("CR_LD", typeof(string)));//领导签字，用分隔符隔开


            string sql = "select CR_XMMC,CR_SBMC,CR_FBSMC,CR_FBFW from TBCR_CONTRACTREVIEW where cr_id='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CR_XMMC"] = dr["CR_XMMC"].ToString();
                    row["CR_SBMC"] = dr["CR_SBMC"].ToString();
                    row["CR_FBSMC"] = dr["CR_FBSMC"].ToString();
                    row["CR_FBFW"] = dr["CR_FBFW"].ToString();

                    string[] CG_JBYJ = Get_BMYJ(code, "06", "0");//采购经办人
                    string[] CG_FZYJ = Get_BMYJ(code, "06", "1");//采购负责人
                    row["CR_CGJBR"] = CG_JBYJ[1];
                    row["CR_CGFZR"] = CG_FZYJ[1];
                    row["CR_CGBYJ"] = CG_JBYJ[0] + "\r\n" + CG_FZYJ[0];

                    string[] JS_FZYJ = Get_BMYJ(code, "03", "1");//技术负责人
                    row["CR_JSFZR"] = JS_FZYJ[1];
                    row["CR_JSBYJ"] = JS_FZYJ[0];

                    string[] ZL_FZYJ = Get_BMYJ(code, "05", "1");//质量负责人
                    row["CR_ZLFZR"] = ZL_FZYJ[1];
                    row["CR_ZLBYJ"] = ZL_FZYJ[0];

                    string[] SC_FZYJ = Get_BMYJ(code, "04", "1");//生产负责人
                    row["CR_SCFZR"] = SC_FZYJ[1];
                    row["CR_SCBYJ"] = SC_FZYJ[0];

                    string[] cw_ld_yj = Get_CW_LD_YJ(code); //财务和领导意见

                    row["CR_CWBYJ"] = cw_ld_yj[0];
                    row["CR_CWFZR"] = cw_ld_yj[1];
                    row["CR_LDYJ"] = cw_ld_yj[2];
                    row["CR_LD"] = cw_ld_yj[3];
                    dt.Rows.Add(row);

                }
            }

            dr.Close();
            return dt;
        }
        //储运
        private DataTable GetData_CY(string code)
        {
            DataTable dt = new DataTable("dtCY");

            dt.Columns.Add(new DataColumn("CR_XMMC", typeof(string)));//项目名称
            dt.Columns.Add(new DataColumn("CR_SBMC", typeof(string)));//设备名称
            dt.Columns.Add(new DataColumn("CR_FBSMC", typeof(string)));//分包商名称
            dt.Columns.Add(new DataColumn("CR_FBFW", typeof(string)));//分包范围

            dt.Columns.Add(new DataColumn("CR_CYBYJ", typeof(string)));//储运部意见：经办人意见+负责人意见
            dt.Columns.Add(new DataColumn("CR_CYJBR", typeof(string)));//储运部经办人
            dt.Columns.Add(new DataColumn("CR_CYFZR", typeof(string)));//储运负责人
            dt.Columns.Add(new DataColumn("CR_SCBYJ", typeof(string)));//生产部意见
            dt.Columns.Add(new DataColumn("CR_SCFZR", typeof(string)));//生产负责人

            dt.Columns.Add(new DataColumn("CR_CWBYJ", typeof(string)));//财务部意见
            dt.Columns.Add(new DataColumn("CR_CWFZR", typeof(string)));//财务负责人

            dt.Columns.Add(new DataColumn("CR_LDYJ", typeof(string)));//领导意见，用分隔符隔开
            dt.Columns.Add(new DataColumn("CR_LD", typeof(string)));//领导签字，用分隔符隔开


            string sql = "select CR_XMMC,CR_SBMC,CR_FBSMC,CR_FBFW from TBCR_CONTRACTREVIEW where cr_id='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CR_XMMC"] = dr["CR_XMMC"].ToString();
                    row["CR_SBMC"] = dr["CR_SBMC"].ToString();
                    row["CR_FBSMC"] = dr["CR_FBSMC"].ToString();
                    row["CR_FBFW"] = dr["CR_FBFW"].ToString();

                    string [] CY_JBYJ = Get_BMYJ(code,"07", "0");//储运经办人
                    string[] CY_FZYJ = Get_BMYJ(code, "07", "1");//储运负责人
                    row["CR_CYJBR"] = CY_JBYJ[1];
                    row["CR_CYFZR"] = CY_FZYJ[1];
                    row["CR_CYBYJ"] = CY_JBYJ[0] + "\r\n" + CY_FZYJ[0];

                    string[] SC_FZYJ = Get_BMYJ(code,"04","1");//生产负责人
                    row["CR_SCFZR"] =SC_FZYJ[1];
                    row["CR_SCBYJ"] = SC_FZYJ[0];
                    #region
                    //SqlDataReader dr_CYJB = GetDR_YJ(code,"07", "0");
                    //SqlDataReader dr_CYFZ = GetDR_YJ(code,"07", "1");
                    //if (dr_CYJB.Read()&&dr_CYFZ.Read())
                    //{
                    //    if (dr_CYJB["CRD_PSYJ"] != "0")//已审批
                    //    {
                    //        row["CR_CYJBR"] = dr_CYJB["CRD_PIDNAME"].ToString();
                    //    }
                    //    else
                    //    {
                    //        row["CR_CYJBR"] = "";//若还没有审批，则不添加该审批人的姓名，表示未审批
                    //    }
                    //    if (dr_CYFZ["CRD_PSYJ"] != "0")
                    //    {
                    //        row["CR_CYFZR"] = dr_CYFZ["CRD_PIDNAME"].ToString();
                    //    }
                    //    else
                    //    {
                    //        row["CR_CYFZR"] = "";
                    //    }
                    //    row["CR_CYBYJ"] = dr_CYJB["CRD_NOTE"].ToString() + "\\" + dr_CYFZ["CRD_NOTE"].ToString();//经办人负责人备注意见合并
                    //    dr_CYJB.Close();
                    //    dr_CYFZ.Close();
                    //}

                    //SqlDataReader dr_SCFZ = GetDR_YJ(code, "04", "1");
                    //if (dr_SCFZ.Read() && dr_SCFZ["CRD_PSYJ"].ToString() != "0")
                    //{
                    //    row["CR_SCFZR"] = dr_SCFZ["CRD_PIDNAME"].ToString();
                    //    row["CR_SCBYJ"] = dr_SCFZ["CRD_NOTE"].ToString();
                    //    dr_SCFZ.Close();
                    //}
                    //else
                    //{
                    //    row["CR_SCFZR"] = row["CR_SCBYJ"] = "";

                    //}
                    #endregion

                    string[] cw_ld_yj = Get_CW_LD_YJ(code); //财务和领导意见

                    row["CR_CWBYJ"] = cw_ld_yj[0];
                    row["CR_CWFZR"] = cw_ld_yj[1];
                    row["CR_LDYJ"] = cw_ld_yj[2];
                    row["CR_LD"] = cw_ld_yj[3];
                    dt.Rows.Add(row);

                }
            }

            dr.Close();
            return dt;
        }
        //分包、生产外协
        private DataTable GetData_FB(string code)
        {
            DataTable dt = new DataTable("dtFB");

            dt.Columns.Add(new DataColumn("CR_XMMC", typeof(string)));//项目名称
            dt.Columns.Add(new DataColumn("CR_SBMC", typeof(string)));//设备名称
            dt.Columns.Add(new DataColumn("CR_FBSMC", typeof(string)));//分包商名称
            dt.Columns.Add(new DataColumn("CR_FBFW", typeof(string)));//分包范围

            dt.Columns.Add(new DataColumn("CR_SCBYJ", typeof(string)));//生产部意见：经办人意见+负责人意见
            dt.Columns.Add(new DataColumn("CR_SCJBR", typeof(string)));//生产部经办人
            dt.Columns.Add(new DataColumn("CR_SCFZR", typeof(string)));//生产负责人

            dt.Columns.Add(new DataColumn("CR_JSBYJ", typeof(string)));//技术部意见
            dt.Columns.Add(new DataColumn("CR_JSFZR", typeof(string)));//技术负责人

            dt.Columns.Add(new DataColumn("CR_ZLBYJ", typeof(string)));//质量部意见
            dt.Columns.Add(new DataColumn("CR_ZLFZR", typeof(string)));//质量负责人
            
            dt.Columns.Add(new DataColumn("CR_CWBYJ", typeof(string)));//财务部意见
            dt.Columns.Add(new DataColumn("CR_CWFZR", typeof(string)));//财务负责人

            dt.Columns.Add(new DataColumn("CR_LDYJ", typeof(string)));//领导意见，用分隔符隔开
            dt.Columns.Add(new DataColumn("CR_LD", typeof(string)));//领导签字，用分隔符隔开


            string sql = "select CR_XMMC,CR_SBMC,CR_FBSMC,CR_FBFW from TBCR_CONTRACTREVIEW where cr_id='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CR_XMMC"] = dr["CR_XMMC"].ToString();
                    row["CR_SBMC"] = dr["CR_SBMC"].ToString();
                    row["CR_FBSMC"] = dr["CR_FBSMC"].ToString();
                    row["CR_FBFW"] = dr["CR_FBFW"].ToString();

                    string[] SC_JBYJ = Get_BMYJ(code, "04", "0");//生产经办人
                    string[] SC_FZYJ = Get_BMYJ(code, "04", "1");//生产负责人
                    row["CR_SCJBR"] = SC_JBYJ[1];
                    row["CR_SCFZR"] = SC_FZYJ[1];
                    row["CR_SCBYJ"] = SC_JBYJ[0] + "\r\n" + SC_FZYJ[0];

                    string[] JS_FZYJ = Get_BMYJ(code, "03", "1");//技术负责人
                    row["CR_JSFZR"] = JS_FZYJ[1];
                    row["CR_JSBYJ"] = JS_FZYJ[0];

                    string[] ZL_FZYJ = Get_BMYJ(code, "05", "1");//质量负责人
                    row["CR_ZLFZR"] = ZL_FZYJ[1];
                    row["CR_ZLBYJ"] = ZL_FZYJ[0];

                    string[] cw_ld_yj = Get_CW_LD_YJ(code); //财务和领导意见

                    row["CR_CWBYJ"] = cw_ld_yj[0];
                    row["CR_CWFZR"] = cw_ld_yj[1];
                    row["CR_LDYJ"] = cw_ld_yj[2];
                    row["CR_LD"] = cw_ld_yj[3];
                    dt.Rows.Add(row);

                }
            }

            dr.Close();
            return dt;
        }
        //电气制造
        private DataTable GetData_DQZZ(string code)
        {
            DataTable dt = new DataTable("dtXS");

            dt.Columns.Add(new DataColumn("CR_XMMC", typeof(string)));//项目名称
            dt.Columns.Add(new DataColumn("CR_SBMC", typeof(string)));//设备名称
            dt.Columns.Add(new DataColumn("CR_FBSMC", typeof(string)));//分包商名称
            dt.Columns.Add(new DataColumn("CR_FBFW", typeof(string)));//分包范围

            dt.Columns.Add(new DataColumn("CR_XSBYJ", typeof(string)));//市场部意见：经办人意见+负责人意见
            dt.Columns.Add(new DataColumn("CR_XSJBR", typeof(string)));//市场部经办人
            dt.Columns.Add(new DataColumn("CR_XSFZR", typeof(string)));//市场负责人

            dt.Columns.Add(new DataColumn("CR_JSBYJ", typeof(string)));//技术部意见
            dt.Columns.Add(new DataColumn("CR_JSFZR", typeof(string)));//技术负责人

            dt.Columns.Add(new DataColumn("CR_ZLBYJ", typeof(string)));//质量部意见
            dt.Columns.Add(new DataColumn("CR_ZLFZR", typeof(string)));//质量负责人

            dt.Columns.Add(new DataColumn("CR_SCBYJ", typeof(string)));//生产部意见
            dt.Columns.Add(new DataColumn("CR_SCFZR", typeof(string)));//生产负责人

            dt.Columns.Add(new DataColumn("CR_CWBYJ", typeof(string)));//财务部意见
            dt.Columns.Add(new DataColumn("CR_CWFZR", typeof(string)));//财务负责人

            dt.Columns.Add(new DataColumn("CR_LDYJ", typeof(string)));//领导意见，用分隔符隔开
            dt.Columns.Add(new DataColumn("CR_LD", typeof(string)));//领导签字，用分隔符隔开


            string sql = "select CR_XMMC,CR_SBMC,CR_FBSMC,CR_FBFW from TBCR_CONTRACTREVIEW where cr_id='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CR_XMMC"] = dr["CR_XMMC"].ToString();
                    row["CR_SBMC"] = dr["CR_SBMC"].ToString();
                    row["CR_FBSMC"] = dr["CR_FBSMC"].ToString();
                    row["CR_FBFW"] = dr["CR_FBFW"].ToString();

                    string[] XS_JBYJ = Get_BMYJ(code, "电气市场", "0");//市场经办人
                    string[] XS_FZYJ = Get_BMYJ(code, "电气市场", "1");//市场负责人
                    row["CR_XSJBR"] = XS_JBYJ[1];
                    row["CR_XSFZR"] = XS_FZYJ[1];
                    row["CR_XSBYJ"] = XS_JBYJ[0] + "\r\n" + XS_FZYJ[0];

                    string[] JS_FZYJ = Get_BMYJ(code, "电气技术", "1");//技术负责人
                    row["CR_JSFZR"] = JS_FZYJ[1];
                    row["CR_JSBYJ"] = JS_FZYJ[0];

                    string[] ZL_FZYJ = Get_BMYJ(code, "电气质量", "1");//质量负责人
                    row["CR_ZLFZR"] = ZL_FZYJ[1];
                    row["CR_ZLBYJ"] = ZL_FZYJ[0];

                    string[] SC_FZYJ = Get_BMYJ(code, "电气生产", "1");//生产负责人
                    row["CR_SCFZR"] = SC_FZYJ[1];
                    row["CR_SCBYJ"] = SC_FZYJ[0];

                    string[] cw_ld_yj = Get_CW_LD_YJ(code); //财务和领导意见

                    row["CR_CWBYJ"] = cw_ld_yj[0];
                    row["CR_CWFZR"] = cw_ld_yj[1];
                    row["CR_LDYJ"] = cw_ld_yj[2];
                    row["CR_LD"] = cw_ld_yj[3];
                    dt.Rows.Add(row);

                }
            }

            dr.Close();
            return dt;
        }
        //其他
        private DataTable GetData_QT(string code)
        {
            DataTable dt = new DataTable("dtCY");

            dt.Columns.Add(new DataColumn("CR_XMMC", typeof(string)));//项目名称
            dt.Columns.Add(new DataColumn("CR_SBMC", typeof(string)));//设备名称
            dt.Columns.Add(new DataColumn("CR_FBSMC", typeof(string)));//分包商名称
            dt.Columns.Add(new DataColumn("CR_FBFW", typeof(string)));//分包范围

            dt.Columns.Add(new DataColumn("CR_BMYJ", typeof(string)));//提出部门意见：经办人意见+负责人意见
            dt.Columns.Add(new DataColumn("CR_BMJBR", typeof(string)));//提出部门经办人
            dt.Columns.Add(new DataColumn("CR_BMFZR", typeof(string)));//提出部门责人
            dt.Columns.Add(new DataColumn("DEP_NAME", typeof(string)));//提出部门名称

            dt.Columns.Add(new DataColumn("CR_CWBYJ", typeof(string)));//财务部意见
            dt.Columns.Add(new DataColumn("CR_CWFZR", typeof(string)));//财务负责人

            dt.Columns.Add(new DataColumn("CR_LDYJ", typeof(string)));//领导意见，用分隔符隔开
            dt.Columns.Add(new DataColumn("CR_LD", typeof(string)));//领导签字，用分隔符隔开


            string sql = "select CR_XMMC,CR_SBMC,CR_FBSMC,CR_FBFW from TBCR_CONTRACTREVIEW where cr_id='" + code + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CR_XMMC"] = dr["CR_XMMC"].ToString();
                    row["CR_SBMC"] = dr["CR_SBMC"].ToString();
                    row["CR_FBSMC"] = dr["CR_FBSMC"].ToString();
                    row["CR_FBFW"] = dr["CR_FBFW"].ToString();

                    string[] BM_JBYJ = Get_QTBMYJ(code, "0");//部门经办人
                    string[] BM_FZYJ = Get_QTBMYJ(code, "1");//部门负责人
                    row["CR_BMJBR"] = BM_JBYJ[1];
                    row["CR_BMFZR"] = BM_FZYJ[1];
                    row["CR_BMYJ"] = BM_JBYJ[0] + "\r\n" + BM_FZYJ[0];
                    row["DEP_NAME"] = BM_JBYJ[2];//提出部门
                    
                    string[] cw_ld_yj = Get_CW_LD_YJ(code); //财务和领导意见

                    row["CR_CWBYJ"] = cw_ld_yj[0];
                    row["CR_CWFZR"] = cw_ld_yj[1];
                    row["CR_LDYJ"] = cw_ld_yj[2];
                    row["CR_LD"] = cw_ld_yj[3];
                    dt.Rows.Add(row);

                }
            }

            dr.Close();
            return dt;
        }
        
        private SqlDataReader GetDR_YJ(string code,string dep_id,string pid_type)//获取部门意见
        {
            string sqlstr = "select CRD_PSYJ,CRD_NOTE,CRD_PIDNAME FROM View_TBCR_View_Detail WHERE CR_ID='"+code+"' and CRD_PIDTYPE='"+pid_type+"' and CRD_DEP='"+dep_id+"'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            return dr;
        }

        private string[] Get_BMYJ(string code, string dep_id, string pid_type)
        {
            string StrWhere_depid = "";
            switch (dep_id)
            {
                case "03":
                    StrWhere_depid = "( CRD_DEP ='03' or CRD_DEP='电气技术')"; break;
                case "04":
                    StrWhere_depid = "(CRD_DEP ='04' or CRD_DEP='电气生产')"; break;
                case "05":
                    StrWhere_depid = "(CRD_DEP ='05' or CRD_DEP='电气质量')"; break;
                case "12":
                    StrWhere_depid = "(CRD_DEP ='12' or CRD_DEP='公司领导')"; break;
                default:
                    StrWhere_depid = "CRD_DEP='" + dep_id + "'"; break;
            }
            string[] BMYJ = new string[2] { "",""};
            string cr_note="";
            string cr_pidname="";
            string sqlstr = "select CRD_PSYJ,CRD_NOTE,CRD_PIDNAME FROM View_TBCR_View_Detail WHERE CR_ID='" + code + "' and CRD_PIDTYPE='" + pid_type + "' and "+StrWhere_depid;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.Read()&&dr["CRD_PSYJ"].ToString()!="0")
            {
                cr_note = dr["CRD_NOTE"].ToString();
                cr_pidname = dr["CRD_PIDNAME"].ToString();
                dr.Close();
            }
            BMYJ[0] = cr_note;
            BMYJ[1] = cr_pidname;
            return BMYJ;
        }

        private string[] Get_QTBMYJ(string code, string pid_type)  //其他合同-提出部门意见
        {
            string[] BMYJ = new string[3] { "", "","" };
            string cr_note = "";
            string cr_pidname = "";
            string tcbm = "";
            string sqlstr = "select CRD_PSYJ,CRD_NOTE,CRD_PIDNAME,DEP_NAME FROM View_TBCR_View_Detail a,TBDS_DEPINFO b WHERE CR_ID='" + code + "' and CRD_PIDTYPE='" + pid_type + "' and CRD_DEP NOT IN ('01','08') and a.CRD_DEP=b.DEP_CODE";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.Read() && dr["CRD_PSYJ"].ToString() != "0")
            {
                cr_note = dr["CRD_NOTE"].ToString();
                cr_pidname = dr["CRD_PIDNAME"].ToString();
                tcbm = dr["DEP_NAME"].ToString();
                dr.Close();
            }
            BMYJ[0] = cr_note;
            BMYJ[1] = cr_pidname;
            BMYJ[2] = tcbm;
            return BMYJ;
        }

        private string[] Get_CW_LD_YJ(string code) //财务领导意见
        {
            string[] CW_LD_YJ =new string[4]{"","","",""};
            string cwfzr = "";
            string cwbyj = "";
            string ld = "";
            string ldyj = "";
            string sqlcw = "select CRD_PSYJ,CRD_NOTE,CRD_PIDNAME FROM View_TBCR_View_Detail WHERE CR_ID='" + code + "' and CRD_DEP='08'";
            string sqlld = "select CRD_PSYJ,CRD_NOTE,CRD_PIDNAME FROM View_TBCR_View_Detail WHERE CR_ID='" + code + "' and CRD_DEP='01'";
            SqlDataReader drcw = DBCallCommon.GetDRUsingSqlText(sqlcw);
            if (drcw.Read() && drcw["CRD_PSYJ"].ToString() != "0")
            {
                cwfzr = drcw["CRD_PIDNAME"].ToString();
                cwbyj = drcw["CRD_NOTE"].ToString();
                drcw.Close();
            }
            DataTable dt_ld = DBCallCommon.GetDTUsingSqlText(sqlld);
            if (dt_ld.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_ld.Rows)
                {
                    if (dr["CRD_PSYJ"].ToString() != "0")
                    {
                        ld += dr["CRD_PIDNAME"].ToString()+" ";
                        ldyj += dr["CRD_NOTE"].ToString() + "\r\n"; 
                    }
                }
            }
            CW_LD_YJ[0] = cwbyj;
            CW_LD_YJ[1] = cwfzr;
            CW_LD_YJ[2] = ldyj;
            CW_LD_YJ[3] = ld;
            return CW_LD_YJ;
        }
    }
}
