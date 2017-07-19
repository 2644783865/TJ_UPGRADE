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

namespace ZCZJ_DPF.OM_Data
{
    public partial class Person_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["code"];
            DataTable dt = GetData(id);
            DataTable dt1 = GetData1(id);
            DataTable dt2 = GetData2(id);
            DataTable dt3 = GetData3(id);

            //报表的数据源
            ReportDataSource rds = new ReportDataSource("DataSet_OM_Person", dt);
            ReportDataSource rds1 = new ReportDataSource("DataSet_OM_Work", dt1);
            ReportDataSource rds2 = new ReportDataSource("DataSet_OM_Educa", dt2);
            ReportDataSource rds3 = new ReportDataSource("DataSet_OM_Relation", dt3);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            ReportViewer1.LocalReport.DataSources.Add(rds2);
            ReportViewer1.LocalReport.DataSources.Add(rds3);
            ReportViewer1.LocalReport.Refresh();
        }

        private DataTable GetData(string id)
        {
            DataTable dt = new DataTable("Person");
            dt.Columns.Add("NAME");
            dt.Columns.Add("GENDER");
            dt.Columns.Add("BIRTHDAY");
            dt.Columns.Add("PEOPLE");
            dt.Columns.Add("ID");
            dt.Columns.Add("MARRY");
            dt.Columns.Add("POLITICAL");
            dt.Columns.Add("INTIME");
            dt.Columns.Add("XULIE");
            dt.Columns.Add("PARTMENT");
            dt.Columns.Add("POSITION");
            dt.Columns.Add("TELE");
            dt.Columns.Add("ADDRESS");
            dt.Columns.Add("HOMETELE");
            dt.Columns.Add("REGIST");
            dt.Columns.Add("XUELI");
            dt.Columns.Add("XUEWEI");
            dt.Columns.Add("ZHUANYE");
            dt.Columns.Add("NOTECER");
            dt.Columns.Add("ZHICHENG");
            dt.Columns.Add("DATA");
            string sql = "select a.*,b.DEP_NAME,d.DEP_NAME as DEP_POSITION from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where ST_ID='" + id + "'";
            DataTable per = DBCallCommon.GetDTUsingSqlText(sql);
            if (per.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                DataRow person = per.Rows[0];
                dr[0] = person["ST_NAME"].ToString();
                dr[1] = person["ST_GENDER"].ToString();
                dr[2] = person["ST_BIRTHDAY"].ToString();
                dr[3] = person["ST_PEOPLE"].ToString();
                dr[4] = person["ST_WORKNO"].ToString();
                dr[5] = person["ST_MARRY"].ToString();
                dr[6] = person["ST_POLITICAL"].ToString();
                dr[7] = person["ST_INTIME"].ToString();
                dr[8] = person["ST_SEQUEN"].ToString();
                dr[9] = person["DEP_NAME"].ToString();
                dr[10] = person["DEP_POSITION"].ToString();
                dr[11] = person["ST_TELE"].ToString();
                dr[12] = person["ST_ADDRESS"].ToString();
                dr[13] = person["ST_HOMETELE"].ToString();
                dr[14] = person["ST_REGIST"].ToString();
                dr[15] = person["ST_XUELI"].ToString();
                dr[16] = person["ST_XUEWEI"].ToString();
                dr[17] = person["ST_ZHUANYE"].ToString();
                dr[18] = person["ST_NOTECER"].ToString();
                dr[19] = person["ST_ZHICH"].ToString();
                dr[20] = DateTime.Now.ToLongDateString();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private DataTable GetData1(string id)
        {
            DataTable dt = new DataTable("Work");
            dt.Columns.Add("GZTIME");
            dt.Columns.Add("GZDW");
            dt.Columns.Add("POSITION");
            dt.Columns.Add("REAOUT");
            dt.Columns.Add("SALARY");
            dt.Columns.Add("INDENTITY");
            string sql = "select * from TBDS_WORKHIS where ST_ID='" + id + "'";
            DataTable work = DBCallCommon.GetDTUsingSqlText(sql);
            int j = 0;
            for (int i = 0; i < work.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = work.Rows[i]["ST_GZSTART"].ToString();
                dr[1] = work.Rows[i]["ST_GZDW"].ToString();
                dr[2] = work.Rows[i]["ST_POSITION"].ToString();
                dr[3] = work.Rows[i]["ST_REAOUT"].ToString();
                dr[4] = work.Rows[i]["ST_SALARY"].ToString();
                dr[5] = work.Rows[i]["ST_INDENTITY"].ToString();
                dt.Rows.Add(dr);
                j++;
            }
            if (j < 5)
            {
                for (int i = 0; i < 5 - work.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private DataTable GetData2(string id)
        {
            DataTable dt = new DataTable("Educa");
            dt.Columns.Add("JYTIME");
            dt.Columns.Add("SCHOOL");
            dt.Columns.Add("ZHUAN");
            dt.Columns.Add("ENGLISH");
            string sql = "select * from TBDS_EDUCA where ST_ID='" + id + "'";
            DataTable work = DBCallCommon.GetDTUsingSqlText(sql);
            int j = 0;
            for (int i = 0; i < work.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = work.Rows[i]["ST_JYTIME"].ToString();
                dr[1] = work.Rows[i]["ST_SCHOOL"].ToString();
                dr[2] = work.Rows[i]["ST_ZHUAN"].ToString();
                dr[3] = work.Rows[i]["ST_ENGLISH"].ToString();
                dt.Rows.Add(dr);
                j++;
            }
            if (j < 4)
            {
                for (int i = 0; i < 4 - work.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private DataTable GetData3(string id)
        {
            DataTable dt = new DataTable("Relation");
            dt.Columns.Add("NAME");
            dt.Columns.Add("AGE");
            dt.Columns.Add("RELATION");
            dt.Columns.Add("WORK");
            dt.Columns.Add("TELE");
            string sql = "select * from TBDS_RELATION where ST_ID='" + id + "'";
            DataTable work = DBCallCommon.GetDTUsingSqlText(sql);

            int j = 0;
            for (int i = 0; i < work.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = work.Rows[i]["ST_NAME"].ToString();
                dr[1] = work.Rows[i]["ST_AGE"].ToString();
                dr[2] = work.Rows[i]["ST_RELATION"].ToString();
                dr[3] = work.Rows[i]["ST_WORK"].ToString();
                dr[4] = work.Rows[i]["ST_TELE"].ToString();
                dt.Rows.Add(dr);
                j++;
            }
            if (j < 3)
            {
                for (int i = 0; i < 3 - work.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
