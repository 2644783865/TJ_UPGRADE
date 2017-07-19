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

namespace ZCZJ_DPF.QC_Data
{
	public partial class QC_Reject_Product_Print : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["ID"];
          
            string str_dataset = "";
            DataTable dt = new DataTable();

            dt = GetData(id);
            str_dataset = "QC_DataSet_dt_Reject_Pro";
            ReportViewer1.LocalReport.ReportPath = "QC_Data//QC_Reject_Product.rdlc";

            //报表的数据源
            ReportDataSource rds = new ReportDataSource(str_dataset, dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
            
        }
        
        private DataTable GetData(string code)
        {
            DataTable dt_print = new DataTable("dt_Reject_Pro");

            dt_print.Columns.Add(new DataColumn("ORDER_ID", typeof(string)));//单号
            dt_print.Columns.Add(new DataColumn("MAIN_DEP", typeof(string)));//主送部门
            dt_print.Columns.Add(new DataColumn("COPY_DEP", typeof(string)));//抄送部门
            dt_print.Columns.Add(new DataColumn("PJ_NAME", typeof(string)));//项目名称
            dt_print.Columns.Add(new DataColumn("TSA_ENGNAME", typeof(string)));//工程名称
            dt_print.Columns.Add(new DataColumn("BJMC", typeof(string)));//部件名称
            dt_print.Columns.Add(new DataColumn("LJMC", typeof(string)));//零件名称
            dt_print.Columns.Add(new DataColumn("REJECT_TYPE", typeof(string)));//不合格类型
            dt_print.Columns.Add(new DataColumn("RANK", typeof(string)));//判定等级
            dt_print.Columns.Add(new DataColumn("QKMS", typeof(string)));//情况描述
            dt_print.Columns.Add(new DataColumn("REASON_TXT", typeof(string)));//产生原因
            dt_print.Columns.Add(new DataColumn("DUTY_PER", typeof(string)));//负责人
            dt_print.Columns.Add(new DataColumn("DUTY_DEP", typeof(string)));//负责部门

            dt_print.Columns.Add(new DataColumn("QFR_NOTE", typeof(string)));//签发人意见 处置建议
            dt_print.Columns.Add(new DataColumn("JSB_RESULT", typeof(string)));//技术部结论（让步、返修……）
            dt_print.Columns.Add(new DataColumn("JSB_NOTE", typeof(string)));//技术部意见
            dt_print.Columns.Add(new DataColumn("JSB_PER", typeof(string)));//技术部负责人
            dt_print.Columns.Add(new DataColumn("PZR_RESULT", typeof(string)));//批准人结论（同意，不同意）
            dt_print.Columns.Add(new DataColumn("PZR_NOTE", typeof(string)));//批准人意见
            dt_print.Columns.Add(new DataColumn("PZR_PER", typeof(string)));//批准人
            dt_print.Columns.Add(new DataColumn("YZR_NOTE", typeof(string)));//验证人意见
            dt_print.Columns.Add(new DataColumn("YZR_PER", typeof(string)));//验证人

            dt_print.Columns.Add(new DataColumn("INFORM_DEP", typeof(string)));//通知部门
            dt_print.Columns.Add(new DataColumn("INFORM_TIME", typeof(string)));//通知时间
            dt_print.Columns.Add(new DataColumn("BZR", typeof(string)));//编制人
            dt_print.Columns.Add(new DataColumn("QFR_PER", typeof(string)));//签发人

            dt_print.Columns.Add(new DataColumn("JSB_TIME", typeof(string)));//技术部审批时间
            dt_print.Columns.Add(new DataColumn("PZR_TIME", typeof(string)));//审批人审批时间
            dt_print.Columns.Add(new DataColumn("YZR_TIME", typeof(string)));//验证人审批时间


            string sql = "select * from View_TBQC_RejectPro_Info where Order_id='" + code + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt_print.NewRow();
                row["ORDER_ID"] = dt.Rows[0]["ORDER_ID"].ToString();
                row["MAIN_DEP"] = dt.Rows[0]["MAIN_DEP"].ToString();
                row["COPY_DEP"] = dt.Rows[0]["COPY_DEP"].ToString();
                row["PJ_NAME"] = dt.Rows[0]["PJ_NAME"].ToString();
                row["TSA_ENGNAME"] = dt.Rows[0]["TSA_ENGNAME"].ToString();
                row["BJMC"] = dt.Rows[0]["BJMC"].ToString();
                row["LJMC"] = dt.Rows[0]["LJMC"].ToString();
                row["QKMS"] = dt.Rows[0]["QKMS"].ToString();
                row["REASON_TXT"] = dt.Rows[0]["REASON_TXT"].ToString();
                row["DUTY_PER"] = dt.Rows[0]["DUTY_PER"].ToString();                
                row["INFORM_TIME"] = dt.Rows[0]["INFORM_TIME"].ToString();
                row["BZR"] = dt.Rows[0]["ST_NAME"].ToString();

                //不合格类型
                #region
               
                string[] type = dt.Rows[0]["Reject_Type"].ToString().Split('|');
                string rejecttype = "";
                for (int i = 0; i < type.Length; i++)
                {
                    switch (type[i])
                    { 
                        case "0":
                            rejecttype += "原材料" + ",";
                            break;
                        case "1":
                            rejecttype += "下料" + ",";
                            break;
                        case "2":
                            rejecttype += "铸造" + ",";
                            break;
                        case "3":
                            rejecttype += "锻造" + ",";
                            break;
                        case "4":
                            rejecttype += "铆焊" + ",";
                            break;
                        case "5":
                            rejecttype += "机加" + ",";
                            break;
                        case "6":
                            rejecttype += "组装" + ",";
                            break;
                        case "7":
                            rejecttype += "试车" + ",";
                            break;
                        case "8":
                            rejecttype += "防腐" + ",";
                            break;
                        case "9":
                            rejecttype += "包装" + ",";
                            break;
                        case "10":
                            rejecttype += "探伤" + ",";
                            break;

                    }

                }
                rejecttype = rejecttype.Substring(0, rejecttype.Length - 1);
                row["REJECT_TYPE"] = rejecttype;
                #endregion

                //判定等级
                #region
                string rank = dt.Rows[0]["RANK"].ToString();
                string rank_txt = "";
                switch (rank)
                {
                    case "0":
                        rank_txt = "轻微不合格"; break;
                    case "1":
                        rank_txt = "一般不合格"; break;
                    case "2":
                        rank_txt = "一般质量事故"; break;
                    case "3":
                        rank_txt = "重大质量事故"; break;

                }
                row["RANK"] = rank_txt;
                #endregion 

                //通知部门
                #region
                string[] inform_dep = dt.Rows[0]["Inform_dep"].ToString().Split('|');
                string informdep_txt = "";
                for (int i = 0; i < inform_dep.Length; i++)
                {
                    string informdep = "select DEP_NAME FROM TBDS_DEPINFO WHERE DEP_CODE='" + inform_dep[i] + "'";
                    DataTable dt_informdep = DBCallCommon.GetDTUsingSqlText(informdep);
                    if (dt_informdep.Rows.Count > 0)
                    {
                        informdep_txt += dt_informdep.Rows[0]["DEP_NAME"].ToString()+",";
                    }
                }
                informdep_txt = informdep_txt.Substring(0, informdep_txt.Length - 1);
                row["INFORM_DEP"] = informdep_txt;
                #endregion

                //责任部门
                #region
                string[] duty_dep = dt.Rows[0]["Duty_dep"].ToString().Split('|');
                string dutydep_txt = "";
                for (int i = 0; i < duty_dep.Length; i++)
                {
                    string dutydep = "select DEP_NAME FROM TBDS_DEPINFO WHERE DEP_CODE='" + duty_dep[i] + "'";
                    DataTable dt_dutydep = DBCallCommon.GetDTUsingSqlText(dutydep);
                    if (dt_dutydep.Rows.Count > 0)
                    {
                        dutydep_txt += dt_dutydep.Rows[0]["DEP_NAME"].ToString() + ",";
                    }
                }
                if (dutydep_txt != "")
                {
                    dutydep_txt = dutydep_txt.Substring(0, dutydep_txt.Length - 1);
                }
                row["DUTY_DEP"] = dutydep_txt;
                #endregion

                //签发人，签发人意见
                string sql_qf = "select Per_note,Per_time,st_name from TBQC_RejectPro_Rev as a,TBDS_STAFFINFO as b where" +
                    " a.Rev_id='" + code + "' and a.Per_id=b.st_code and a.Per_type=1";
                DataTable dt_qf = DBCallCommon.GetDTUsingSqlText(sql_qf);
                if (dt_qf.Rows.Count > 0 && dt_qf.Rows[0]["Per_time"].ToString() != "")
                {
                    row["QFR_NOTE"] = dt_qf.Rows[0]["Per_note"].ToString();
                    row["QFR_PER"] = dt_qf.Rows[0]["st_name"].ToString();
                }

                //技术部意见
                string sql_js = "select Per_note,Per_result,Per_time,st_name from TBQC_RejectPro_Rev as a,TBDS_STAFFINFO as b where" +
                    " a.Rev_id='" + code + "' and a.Per_id=b.st_code and a.Per_type=2";
                DataTable dt_js = DBCallCommon.GetDTUsingSqlText(sql_js);
                if (dt_js.Rows.Count > 0 && dt_js.Rows[0]["Per_time"].ToString() != "")
                {
                    row["JSB_NOTE"] = dt_js.Rows[0]["Per_note"].ToString();
                    row["JSB_PER"] = dt_js.Rows[0]["st_name"].ToString();
                    row["JSB_TIME"] = dt_js.Rows[0]["Per_time"].ToString();
                    switch (dt_js.Rows[0]["Per_result"].ToString())
                    {
                        case "1":
                            row["JSB_RESULT"] = "报废"; break;
                        case "2":
                            row["JSB_RESULT"] = "返修/返工"; break;
                        case "3":
                            row["JSB_RESULT"] = "降级"; break;
                        case "4":
                            row["JSB_RESULT"] = "让步"; break;

                    }
                }

                //批准人意见
                string sql_pz = "select Per_note,Per_result,Per_time,st_name from TBQC_RejectPro_Rev as a,TBDS_STAFFINFO as b where" +
                    " a.Rev_id='" + code + "' and a.Per_id=b.st_code and a.Per_type=3";
                DataTable dt_pz = DBCallCommon.GetDTUsingSqlText(sql_pz);
                if (dt_pz.Rows.Count > 0 && dt_pz.Rows[0]["Per_time"].ToString() != "")
                {
                    row["PZR_NOTE"] = dt_pz.Rows[0]["Per_note"].ToString();
                    row["PZR_PER"] = dt_pz.Rows[0]["st_name"].ToString();
                    row["PZR_TIME"] = dt_pz.Rows[0]["Per_time"].ToString();
                    switch (dt_pz.Rows[0]["Per_result"].ToString())
                    {
                        case "1":
                            row["PZR_RESULT"] = "同意"; break;
                        case "2":
                            row["PZR_RESULT"] = "不同意"; break;                       

                    }
                }

                //验证人意见
                string sql_yz = "select Per_note,Per_result,Per_time,st_name from TBQC_RejectPro_Rev as a,TBDS_STAFFINFO as b where" +
                    " a.Rev_id='" + code + "' and a.Per_id=b.st_code and a.Per_type=4";
                DataTable dt_yz = DBCallCommon.GetDTUsingSqlText(sql_yz);
                if (dt_yz.Rows.Count > 0 && dt_yz.Rows[0]["Per_time"].ToString() != "")
                {
                    row["YZR_NOTE"] = dt_yz.Rows[0]["Per_note"].ToString();
                    row["YZR_PER"] = dt_yz.Rows[0]["st_name"].ToString();
                    row["YZR_TIME"] = dt_yz.Rows[0]["Per_time"].ToString();
                }
                dt_print.Rows.Add(row);
            }

            return dt_print;
        }
	}
}
