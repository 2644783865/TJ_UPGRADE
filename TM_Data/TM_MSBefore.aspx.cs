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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MSBefore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitPage();
        }

        private void InitPage()
        {
            string lotNum = Request.QueryString["LotNum"].ToString();//批号
            string taskID = lotNum.Substring(0, lotNum.IndexOf('.'));
            string index = Request.QueryString["NewIndex"].ToString();
            string mstable = Request.QueryString["MsViewTableName"].ToString();
            string sqlText;

            tsaid.Text = taskID;
            //获取项目名称，工程名称，设备类型等
            sqlText = "select TSA_PJID,CM_PROJ,TSA_ENGNAME ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + taskID + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                txtContract.Text = dr[0].ToString();

                proname.Text = dr[1].ToString();
                engname.Text = dr[2].ToString();

            }
            dr.Close();
            //获取变更前后数据

            //变更前
            string sql_select_chgbef = "select *,cast(MS_UNUM as varchar)+' | '+cast(MS_NUM as varchar) AS NUMBER from " + mstable + " where MS_NEWINDEX='" + index + "' AND MS_CHGPID='" + lotNum + "'";
            DataTable dt_chgbef = DBCallCommon.GetDTUsingSqlText(sql_select_chgbef);
            grvBefore.DataSource = dt_chgbef;
            grvBefore.DataBind();

            if (grvBefore.Rows.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }

            //变更后
            string sql_select_chgaft = "select *,cast(MS_UNUM as varchar)+' | '+cast(MS_NUM as varchar) AS NUMBER from View_TM_MSCHANGE where MS_OLDINDEX='" + index + "' AND MS_PID='" + lotNum + "'";
            DataTable dt_chgaft = DBCallCommon.GetDTUsingSqlText(sql_select_chgaft);
            grvAfter.DataSource = dt_chgaft;
            grvAfter.DataBind();
            if (grvAfter.Rows.Count > 0)
            {
                NoDataPanelAfter.Visible = false;
            }
            else
            {
                NoDataPanelAfter.Visible = true;
            }

        }
    }
}
