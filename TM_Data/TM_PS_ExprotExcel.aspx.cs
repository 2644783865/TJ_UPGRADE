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
    public partial class TM_PS_ExprotExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
            }
        }
        /// <summary>
        /// 初始化页面(基本信息)
        /// </summary>
        private void InitInfo()
        {
            string tsa_id = "";
            string[] fields;
            string sqlText = "";
            tsa_id = Request.QueryString["action"];
            fields = tsa_id.Split('-');
            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;

                proname.Text = dr[0].ToString();
              
            }
            engname.Text = dr[1].ToString();
            dr.Close();
            this.BindLotNums();
        }
        /// <summary>
        /// 导出涂装方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMsExport_OnClick(object sender, EventArgs e)
        {
            if (ddlLotNumList.SelectedIndex != 0)
            {
                ExportTMDataFromDB.ExportPSData(ddlLotNumList.SelectedValue);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('请选择批号！！！');self.close();", true);
            }
        }
        /// <summary>
        /// 绑定批号数据
        /// </summary>
        private void BindLotNums()
        {
            string sql_select_lotnum = "select PS_ID from  VIEW_TM_PAINTSCHEME where PS_ENGID='" + tsaid.Text + "'";
            string dataText = "PS_ID";
            string dataValue = "PS_ID";
            DBCallCommon.BindDdl(ddlLotNumList, sql_select_lotnum, dataText, dataValue);
        }

        public string GetTaskID
        {
            get
            {
                return Request.QueryString["action"];
            }
        }
    }
}
