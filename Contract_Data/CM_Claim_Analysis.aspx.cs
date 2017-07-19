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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Claim_Analysis : System.Web.UI.Page
    {
        Statistics stc = new Statistics();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindSP_Reason(cblSP);
                this.BindAllData();
            }
        }
        /// <summary>
        /// 绑定索赔原因
        /// </summary>
        /// <param name="cbl"></param>
        private void BindSP_Reason(CheckBoxList cbl)
        {
            cbl.Items.Clear();
            string sqltext = "select SPR_ID,SPR_DESCRIBLE from TBPM_REASONCONTROL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cbl.DataSource = dt;
            cbl.DataTextField = "SPR_DESCRIBLE";
            cbl.DataValueField = "SPR_ID";
            cbl.DataBind();
        }

        protected void cblSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string values = "(";
            foreach (ListItem li in cblSP.Items)
            {
                if (li.Selected)
                {
                    values += li.Value.ToString() + ",";
                }
            }

            if (values.Length > 1)
            {
                values = values.Substring(0, values.Length - 1) + ")";
            }
            if (values != "(")
            {
                string sqltext = "select * from TBPM_REASONCONTROL where SPR_ID in" + values;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                DataView firstView = new DataView(dt);
                Chart1.Series["Default"].Points.DataBindXY(firstView, "SPR_DESCRIBLE", firstView, "SPR_ACCNUM");
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                DataView firstView = new DataView(null);
                Chart1.Series["Default"].Points.DataBindXY(firstView, "", firstView, "");
            }
        }
        protected void btnAnalysis_Click(object sender, EventArgs e)
        {
            if (txtKSSJ.Text.Trim() == "")
            {
                stc.StartTime = "2000-12-12";//如果开始时间为空，默认
            }
            else
            {
                stc.StartTime = txtKSSJ.Text.Trim();
            }
            if (txtJSSJ.Text.Trim() == "")
            {
                stc.EndTime="2100-12-12";
            }
            else
            {
                stc.EndTime = txtJSSJ.Text.Trim();
            }
            //执行存储过程
            this.PagerQueryParam(stc);
            //查询数据
            string sqltext = "select * from TBPM_REASONCONTROL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            foreach (ListItem li in cblSP.Items)
            {
                li.Selected = true;
            }
            DataView firstView = new DataView(dt);
            Chart1.Series["Default"].Points.DataBindXY(firstView, "SPR_DESCRIBLE", firstView, "SPR_ACCNUM");
        }

        private void BindAllData()
        {
            stc.StartTime = "2000-1-1";
            stc.EndTime = "2100-12-12";
            //执行存储过程
            this.PagerQueryParam(stc);
            //查询数据
            string sqltext = "select * from TBPM_REASONCONTROL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            foreach (ListItem li in cblSP.Items)
            {
                li.Selected = true;
            }
            DataView firstView = new DataView(dt);
            Chart1.Series["Default"].Points.DataBindXY(firstView, "SPR_DESCRIBLE", firstView, "SPR_ACCNUM");

        }

        public class Statistics
        {
            private string _StartTime;
            private string _EndTime;
            public string StartTime
            {
                get { return _StartTime; }
                set { _StartTime = value; }
            }

            public string EndTime
            {
                get { return _EndTime; }
                set { _EndTime = value; }
            }
        }

        public void PagerQueryParam(Statistics statistcs)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "ClaimTotal");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Start_Time", statistcs.StartTime, SqlDbType.DateTime, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@End_Time", statistcs.EndTime, SqlDbType.DateTime, 1000);
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            txtJSSJ.Text = "";
            txtKSSJ.Text = "";
            this.BindAllData();
        }


    }
}
