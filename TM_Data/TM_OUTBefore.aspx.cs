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
    public partial class TM_OUTBefore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitPage();
        }

        private void InitPage()
        {
            string lotNum = Request.QueryString["LotNum"].ToString();//批号
            string taskID = lotNum.Substring(0, lotNum.IndexOf('-'));
            string index = Request.QueryString["NewIndex"].ToString();
            string sqlText;

            tsaid.Text = taskID;
            //获取项目名称，工程名称，设备类型等
            sqlText = "select TSA_PJID,TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + taskID + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                pro_id.Value = dr[0].ToString();
                proname.Text = dr[1].ToString();
                engname.Text = dr[2].ToString();
                eng_type.Value = dr[3].ToString();
            }
            dr.Close();
            //获取变更前后数据

            //变更前
            string sql_select_chgbef = "select * from View_TM_OUTSOURCELIST where OSL_NEWXUHAO='" + index + "' AND OSL_OUTSOURCECHGNO='" + lotNum + "'";
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
            string sql_select_chgaft = "select * from View_TM_OUTSCHANGE where OSL_OLDXUHAO='" + index + "' AND OSL_OUTSOURCENO='" + lotNum + "'";
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
